using Android.Content;
using Android.Preferences;
using SQLite;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicker
{
    public partial class App : Application
    {
        public static double TotalPointsOfTheUser;
        public static double MultiplierOfPoints;
        public const string Name_of_Database = "clicker_game.db";
        public static SQLiteConnection db;
        public App()
        {
            InitializeComponent();
            CheckDbTablesExistance();
            MainPage = new MainTabbed();
        }

        protected override void OnStart()
        {
            CheckPrefExistance();
            TotalPointsOfTheUser = Preferences.Get("TotalPoints", TotalPointsOfTheUser);
            MultiplierOfPoints = Preferences.Get("Multiplier", MultiplierOfPoints);
            ClickerScreen.canHoldTheButton = Preferences.Get("Hold", false);
        }

        protected override void OnSleep()
        {
            Preferences.Set("TotalPoints", TotalPointsOfTheUser.ToString());
            Preferences.Set("Multiplier", MultiplierOfPoints.ToString());
        }

        protected override void OnResume()
        {
            TotalPointsOfTheUser = Preferences.Get("TotalPoints", TotalPointsOfTheUser);
            MultiplierOfPoints = Preferences.Get("Multiplier", MultiplierOfPoints);

        }
        public static void CheckDbTablesExistance()
        {
            using (db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name_of_Database)))
            {
                db.DropTable<Colour>();
                db.DropTable<Upgrade>();
                db.CreateTable<Colour>();
                db.CreateTable<Upgrade>();
                if (db.Table<Colour>().Count() == 0)
                {
                    foreach (var item in new Colour[] { new Colour { id = 1, NavBar = "#A48E59", Background = "#598AA6", TextColour = "#5C59A6", Price = 2 }, new Colour { id = 2, NavBar = "#2B72D5", Background = "#D5552A", TextColour = "#D5C72A", Price = 2 }, new Colour { id = 3, NavBar = "#075C0C", Background = "#44075A", TextColour = "#5A0739", Price = 2 }, new Colour { id = 4, NavBar = "#BA0029", Background = "#00B853", TextColour = "#00A2B8" }, new Colour { id = 5, NavBar = "Blue", Background = "White", TextColour = "Gray", Price = 2, Purchased = true, IsActive = true } })
                    {
                        db.Insert(item);
                    }

                }
                if (db.Table<Upgrade>().Count() == 0)
                {
                    foreach (var item in new Upgrade[] { new Upgrade { id = 1, Name = "Milk", Description = "Coffee & Milk, a classic", OneTimePurchase = 0, Value = 2, Price = 10, Picture = "loop.png" }, new Upgrade { id = 2, Name = "Sugar", Description = "Spice up your life with some sweet sugar", OneTimePurchase = 0, Value = 2, Picture = "loop.png", Price = 10 }, new Upgrade { id = 3, Name = "Coffee beans", Description = "Imagine the flavor", OneTimePurchase = 0, Value = 0.5, Picture = "loop.png", Price = 10 }, new Upgrade { id = 4, Name = "Premium cup", Description = "Bigger, better, stronger cup", OneTimePurchase = 1, Value = 2, Price = 10, Picture = "star.png" }, new Upgrade { id = 5, Name = "Golden Hand", Description = "Do you wanna stop clicking?", Picture = "star.png", Price = 10, OneTimePurchase = 1, Value = 1} })
                    {
                        db.Insert(item);
                    }
                }
            }
        }
        public static void CheckPrefExistance()
        {
            if (Preferences.Get("TotalPoints", null) == null || Preferences.Get("Multiplier", null) == null)
            {
                Preferences.Set("TotalPoints", 0.ToString());
                Preferences.Set("Multiplier", 1.ToString());
                Preferences.Set("Hold", false);
            }
        }
    }
}
