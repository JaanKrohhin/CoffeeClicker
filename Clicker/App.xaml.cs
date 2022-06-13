using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainTabbed();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
