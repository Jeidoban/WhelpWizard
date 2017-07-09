using Xamarin.Forms;
using Plugin.LocalNotifications;

namespace WhelpWizard
{
    public partial class WhelpWizardPage : ContentPage
    {
        public WhelpWizardPage()
        {
            InitializeComponent();

        }

        void GoToCalculator(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Calculator());
        }
    }
}
