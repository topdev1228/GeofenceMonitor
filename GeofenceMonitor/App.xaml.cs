using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeofenceMonitor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            System.Console.WriteLine("DEBUG - ## App.xaml.cs ## Start from here !!!");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
