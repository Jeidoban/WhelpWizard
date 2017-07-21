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
        ObservableCollection<String> vacList;
        VaccineList list;
        Vaccine vac;

        //TODO: I would reccomend creating a list that holds vaccines with a plus button 
        // in the top right corner. Also create a vaccine class that holds all info 
        // needed for a vaccine and/or medication.

        public Vaccinations(Dog currentDog, VaccineList list)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            this.list = list;
            vac = new Vaccine();
            saveButton.Text = "Save information to " + currentDog.DogName;
            vacList = new ObservableCollection<String>();
            //ToolbarItems.Add(new ToolbarItem("", "EditSymbolXam.png", () => DisplayAlert("Clicked", "Clicked Share", "ok"), ToolbarItemOrder.Default));
            //vaccineList.ItemsSource = test;
            vacList.Add("Create New");
            vacList.Add("Heart Disease");
            vaccineName.ItemsSource = vacList;
        }
         
        public Vaccinations() {}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        void AddButtonClicked(object sender, System.EventArgs e)
        {
            if (pickerRemind.IsVisible)
                vac.VaccineRemind = pickerRemind.Date;

            vac.VaccineDate = picker.Date;
            vac.VaccineName = vaccineName.SelectedItem.ToString();
            vac.Notes = notes.Text;
            currentDog.vaccineList.Add(vac);
            list.vaccineList.Add(vac);
            Navigation.PopModalAsync(true);
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (switchForRemind.IsToggled)
            {
                hideElements.IsVisible = true;
            } else {
                hideElements.IsVisible = false;
            }
        }
    }
}
