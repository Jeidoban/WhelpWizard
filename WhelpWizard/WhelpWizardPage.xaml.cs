using Xamarin.Forms;

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
