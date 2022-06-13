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
            if (Preferences.Get("TotalPoints", null) == null || Preferences.Get("Multiplier", null) == null)
            {
                Preferences.Set("TotalPoints", 0.ToString());
                Preferences.Set("Multiplier", 1.ToString());
                Preferences.Set("Hold", false);
            }
            TotalPointsOfTheUser = Preferences.Get("TotalPoints", TotalPointsOfTheUser);
            MultiplierOfPoints = Preferences.Get("Multiplier", MultiplierOfPoints);
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
                db.CreateTable<Colour>();
                db.CreateTable<Upgrade>();
                if (db.Table<Colour>().Count() == 0)
                {
                    foreach (var item in new Colour[] { new Colour { id = 1, NavBar = "Red", Background = "Green", TextColour = "Blue", Price = 2 }, new Colour { id = 2, NavBar = "Green", Background = "Blue", TextColour = "Red", Price = 2 }, new Colour { id = 3, NavBar = "Blue", Background = "Red", TextColour = "Green", Price = 2 } })
                    {
                        db.Insert(item);
                    }

                }
                if (db.Table<Upgrade>().Count() == 0)
                {
                    foreach (var item in new Upgrade[] { new Upgrade { id = 1, Name = "Milk", Description = "Coffee & Milk, a classic", OneTimePurchase = 0, Value = 2, Picture = "clicker.png", Price = 10 }, new Upgrade { id = 2, Name = "Sugar", Description = "Spice up your life with some sweet sugar", OneTimePurchase = 0, Value = 2, Picture = "clicker.png", Price = 10 }, new Upgrade { id = 3, Name = "Coffee beans", Description = "Imagine the flavor", OneTimePurchase = 0, Value = 0.5, Picture = "clicker.png", Price = 10 }, new Upgrade { id = 4, Name = "Premium cup", Description = "Bigger, better, stronger cup", OneTimePurchase = 1, Value = 2, Price = 10 }, new Upgrade { id = 5, Name = "Golden Hand", Description = "Do you wanna stop clicking?", Picture = "clicker.png", Price = 10, OneTimePurchase = 1} })
                    {
                        db.Insert(item);
                    }
                }
            }
        }
    }
}
