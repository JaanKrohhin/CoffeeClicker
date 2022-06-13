using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Upgrades : ContentPage
    {
        public ObservableCollection<Upgrade> StoreUpgrades { get; set; } //must sort out the purchased
        public ObservableCollection<Colour> Colours { get; set; }

        public Upgrades()
        {
            InitializeComponent();
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            { 
                StoreUpgrades = new ObservableCollection<Upgrade>(App.db.Table<Upgrade>().ToList());
                Colours = new ObservableCollection<Colour>(App.db.Table<Colour>().ToList());
            }
            upgradeList.ItemsSource = StoreUpgrades;
            colourList.ItemsSource = Colours;
            //this.Appearing += Upgrades_Appearing;
        }

        private void Upgrades_Appearing(object sender, EventArgs e)
        {
            UpdateLists();
        }
        public void UpdateLists()
        {
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                StoreUpgrades = (ObservableCollection<Upgrade>)(from u in App.db.Table<Upgrade>().ToList() where u.OneTimePurchase < 2 select u);
                Colours = (ObservableCollection<Colour>)(from c in App.db.Table<Colour>().ToList() where c.Purchased=false select c);
            }
            upgradeList.ItemsSource = StoreUpgrades;
            colourList.ItemsSource = Colours;
        }
        private async void Upgrade_Clicked(object sender, EventArgs e)
        {
            Upgrade down = (Upgrade)((Button)sender).BindingContext;
            if (App.TotalPointsOfTheUser >= down.Price)
            {
                var DoubleCorrectionVariable = down.Value;
                if (down.Value == 0.5)
                {
                    DoubleCorrectionVariable = down.Value + 1;
                }
                App.MultiplierOfPoints *= DoubleCorrectionVariable;
                App.TotalPointsOfTheUser -= down.Price;
                down.Price *= 2;
                if (down.OneTimePurchase == 1)
                {
                    down.OneTimePurchase = 2;
                }
                await DisplayAlert("Purchase", down.Name, "Ok");
                //save to db
                //update list
                using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
                {
                    App.db.Update(down);
                    StoreUpgrades = new ObservableCollection<Upgrade>(App.db.Table<Upgrade>().ToList());
                    upgradeList.ItemsSource = StoreUpgrades;
                }
            }
            else
            {
                await DisplayAlert("Purchase", "Not enough points", "Ok");
            }
        }
        private void ColorPurchase(object sender, EventArgs e)
        {
            Colour col = (Colour)((Button)sender).BindingContext;
            if (App.TotalPointsOfTheUser >= col.Price)
            {
                col.Purchased = true;
                //save to db
                //update list
                using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
                {
                    App.db.Update(col);
                    StoreUpgrades = new ObservableCollection<Upgrade>(App.db.Table<Upgrade>().ToList());
                    upgradeList.ItemsSource = StoreUpgrades;
                }
            }
        }
    }
}