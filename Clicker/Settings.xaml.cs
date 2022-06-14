using Android.Preferences;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public static ObservableCollection<Colour> Colours { get; set; }
        public Settings()
        {
            InitializeComponent();
            Colours = GetList();
            colourList.ItemsSource = Colours;
            this.Focused += Settings_Focused;
        }

        private void Settings_Focused(object sender, FocusEventArgs e)
        {
            this.BindingContext = (Colour)App.Current.MainPage.BindingContext;
        }

        public ObservableCollection<Colour> GetList()
        {
            var a = new ObservableCollection<Colour>();
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                a = new ObservableCollection<Colour>((from c in App.db.Table<Colour>() where c.Purchased == true select c).ToList<Colour>()) ;
            }
            return a;
        }
        private void Reload(object sender, EventArgs e)
        {
            colourList.ClearValue(ListView.ItemsSourceProperty);
            colourList.ItemsSource = GetList();
        }
            private void Button_Clicked(object sender, EventArgs e)
        {
            var a = colourList.SelectedItem as Colour;
            if (a != null) 
            { 
                var b = (Colour)App.Current.MainPage.BindingContext;
                b.IsActive = false;
                a.IsActive = true;
                using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
                {
                    App.db.Update(b);
                    App.db.Update(a);
                }
                App.Current.MainPage.BindingContext = a;
            }

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                App.db.DropTable<Upgrade>();
                App.db.DropTable<Colour>();
            }
            Preferences.Clear();
            App.TotalPointsOfTheUser = 0;
            App.MultiplierOfPoints = 1;
            ClickerScreen.canHoldTheButton = false;
            App.CheckDbTablesExistance();
            GetList();
        }
    }
    
}