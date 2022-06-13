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
        public ObservableCollection<Colour> Colours { get; set; }
        public Settings()
        {
            InitializeComponent();
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                Colours = new ObservableCollection<Colour>(App.db.Table<Colour>().ToList());
            }
            colourList.ItemsSource = Colours;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                App.db.DropTable<Upgrade>();
                App.db.DropTable<Colour>();
            }
            Preferences.Remove("Hold");
            Preferences.Remove("Multiplier");
            Preferences.Remove("TotalPoints");
            App.CheckDbTablesExistance();
        }
    }
    
}