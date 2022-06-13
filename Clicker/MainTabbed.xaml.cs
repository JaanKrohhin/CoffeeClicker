using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbed : TabbedPage
    {
        public MainTabbed()
        {
            using (App.db = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.Name_of_Database)))
            {
                this.BindingContext = (from s in App.db.Table<Colour>() where s.IsActive == true select s).First();
            }
            InitializeComponent();
        }
    }
}