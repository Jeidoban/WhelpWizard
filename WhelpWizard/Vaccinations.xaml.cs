using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;



namespace WhelpWizard
{
    public partial class Vaccinations : ContentPage
    {
        //TODO: Fill this with text boxes. Also create a method where you can pass the info in here back to calculator.
        public Vaccinations(Calculator calc)
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Vaccine Information Added", "Press save to save dog information","Ok");
            Navigation.PopModalAsync(true);
        }
    }
}
