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
    public partial class Upgrades : ContentPage
    {
        public ObservableCollection<Upgrade> StoreUpgrades { get; set; } //must sort out the purchased
        public ObservableCollection<Colour> Colours { get; set; }

        public Upgrades()
        {
            InitializeComponent();
            UpdateLists();
            this.Appearing += Upgrades_Appearing;
        }

        private void Upgrades_Appearing(object sender, EventArgs e)
        {
            UpdateLists();
            this.BindingContext = (Colour)App.Current.MainPage.BindingContext;
        }
        public void UpdateLists()
        {
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                StoreUpgrades = UpgradeSort();
                Colours = ColourSort();
            }
            upgradeList.ItemsSource = StoreUpgrades;
            colourList.ItemsSource = Colours;
        }
        private ObservableCollection<Colour> ColourSort()
        {
            var a = (from c in App.db.Table<Colour>().ToList() where c.Purchased == false select c).ToList<Colour>();
            return new ObservableCollection<Colour>(a);
        }
        private ObservableCollection<Upgrade> UpgradeSort()
        {
            var a = (from c in App.db.Table<Upgrade>() where c.OneTimePurchase < 2 select c).ToList<Upgrade>();
            return new ObservableCollection<Upgrade>(a);
        }
        private async void Upgrade_Clicked(object sender, EventArgs e)
        {
            Upgrade down = (Upgrade)((Button)sender).BindingContext;
            if (App.TotalPointsOfTheUser >= down.Price)
            {
                Random rnd = new Random();
                var DoubleCorrectionVariable = down.Value;
                if (down.Value == 0.5)
                {
                    DoubleCorrectionVariable = down.Value + 1;
                }
                App.MultiplierOfPoints += DoubleCorrectionVariable;
                App.TotalPointsOfTheUser -= down.Price;
                down.Price *= rnd.Next(2, 6); ;
                if (down.OneTimePurchase == 1)
                {
                    down.OneTimePurchase = 2;
                }
                if (down.Name == "Golden Hand")
                {
                    ClickerScreen.canHoldTheButton = true;
                    Preferences.Set("Hold", true);
                }
                await DisplayAlert("Purchase", down.Name, "Ok");
                using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
                {
                    App.db.Update(down);
                    StoreUpgrades = UpgradeSort();
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
                using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
                {
                    App.db.Update(col);
                    Colours = ColourSort();
                    colourList.ItemsSource = Colours;
                }
            }
        }
    }
}