using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WhelpWizard
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            SaveAndLoad.LoadNotificationId();
            MainPage = new NavigationPage(new WhelpWizardPage());
           //MainPage.BackgroundColor = Color.FromHex("#D9DCD6");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
