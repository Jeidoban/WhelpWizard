using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WhelpWizard
{
    public partial class Vaccinations : ContentPage
    {
        Dog currentDog;
        ObservableCollection<string> test;

        //TODO: I would reccomend creating a list that holds vaccines with a plus button 
        // in the top right corner. Also create a vaccine class that holds all info 
        // needed for a vaccine and/or medication.

        public Vaccinations(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            saveButton.Text = "Save information to " + currentDog.DogName;
            test = new ObservableCollection<string>();
            test.Add("first vaccine");
            test.Add("second vaccine");
            //vaccineList.ItemsSource = test;
        }

        public Vaccinations() {}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        void AddButtonClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Info Added to (dog name)", "Please press save to save this information", "Ok");
            Navigation.PopModalAsync(true);
        }
    }
}
