using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicker
{
    public partial class ClickerScreen : ContentPage
    {
        public static bool canHoldTheButton = Preferences.Get("Hold", false);
        bool isHoldingThebutton = false;
        public ClickerScreen()
        {
            InitializeComponent();
            pointLabel.Text = ((int)App.TotalPointsOfTheUser).ToString() + "p";
            multLabel.Text = App.MultiplierOfPoints.ToString() + "x";
            this.Appearing += ClickerScreen_Appearing;
        }

        private void ClickerScreen_Appearing(object sender, EventArgs e)
        {
            pointLabel.Text = ((int)App.TotalPointsOfTheUser).ToString()+"p";
            multLabel.Text = App.MultiplierOfPoints.ToString() + "x";
        }

        private void ImageButton_Clicked(object sender, EventArgs e) { AddPoints();       }

        private void ImageButton_Pressed(object sender, EventArgs e)
        {
            if (canHoldTheButton)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                isHoldingThebutton = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
                {
                    AddPoints();
                    return isHoldingThebutton;
                });
                stopwatch.Stop();
            }
        }
        public void AddPoints()
        {
            App.TotalPointsOfTheUser += 1 * App.MultiplierOfPoints;
            pointLabel.Text = ((int)App.TotalPointsOfTheUser).ToString() + "p";
        }

        public void MultUpdate(Label multLabel)
        {
            multLabel.Text = App.MultiplierOfPoints.ToString() + "x";
        }
        private void imgBtn_Released(object sender, EventArgs e)
        {
            isHoldingThebutton = false;
        }
    }
}
