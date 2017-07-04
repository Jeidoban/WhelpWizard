using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WhelpWizard
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Calculator());
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
