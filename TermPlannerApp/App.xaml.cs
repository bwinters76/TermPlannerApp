using System;
using TermPlannerApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    public partial class App : Application
    {
        public static string FilePath;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            InitializeComponent();
            FilePath = filePath;
            TestingData.LoadTestData();
            MainPage = new NavigationPage(new MainPage());

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
