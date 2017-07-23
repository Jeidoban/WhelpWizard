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
        bool editMode;
        VaccineList list;
        Vaccine vac;

        //TODO: I would reccomend creating a list that holds vaccines with a plus button 
        // in the top right corner. Also create a vaccine class that holds all info 
        // needed for a vaccine and/or medication.

        public Vaccinations(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            vac = new Vaccine();
            saveButton.Text = "Save information to " + currentDog.DogName;
            vacList = new ObservableCollection<String>();
            //ToolbarItems.Add(new ToolbarItem("", "EditSymbolXam.png", () => DisplayAlert("Clicked", "Clicked Share", "ok"), ToolbarItemOrder.Default));
            //vaccineList.ItemsSource = test;
            pickerRemind = null;
            vacList.Add("Create New");
            vacList.Add("test 1");
            vaccineName.ItemsSource = vacList;
        }

        public Vaccinations(Dog currentDog, Vaccine vac)
        {
			InitializeComponent();
            editMode = true;
			this.currentDog = currentDog;
            this.vac = vac;
			vacList = new ObservableCollection<String>();
			vacList.Add("Create New");
			vacList.Add("test 1");


			if (vac.VaccineRemind != null) switchForRemind.IsToggled = true;
			vaccineName.ItemsSource = vacList;
			saveButton.Text = "Save information to " + currentDog.DogName;
            picker.Date = vac.VaccineDate;
            vaccineName.SelectedItem = vac.VaccineName;
            pickerRemind.Date = vac.VaccineRemind;
            notes.Text = vac.Notes;
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
            vac.VaccineName = vaccineName.SelectedItem;
            vac.Notes = notes.Text;
            if (!editMode) currentDog.vaccineList.Add(vac);
            SaveAndLoad.OverwriteFile(currentDog);
           //list.vaccineList.Add(vac);
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
