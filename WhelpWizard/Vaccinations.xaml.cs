using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Acr.UserDialogs;

namespace WhelpWizard
{
    public partial class Vaccinations : ContentPage
    {
        Dog currentDog;
        bool editMode;
        Vaccine vac;

        public Vaccinations(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            vac = new Vaccine();
            saveButton.Text = "Save information to " + currentDog.DogName;
			pickerRemind.MaximumDate = picker.Date;
            pickerRemind.MinimumDate = DateTime.Today;
            picker.MinimumDate = DateTime.Today;
            vaccineName.ItemsSource = AddedVaccineList.AddedVaccines;
			AddedVaccineList.AddedVaccines.Add("Create New");
            AddedVaccineList.AddedVaccines.Add("Delete");
			AddedVaccineList.AddedVaccines.Add("Vaccine 1");
			AddedVaccineList.AddedVaccines.Add("Vaccine 2");
		}

        public Vaccinations(Dog currentDog, Vaccine vac)
        {
			InitializeComponent();
            editMode = true;
			this.currentDog = currentDog;
            this.vac = vac;
            vaccineName.ItemsSource = AddedVaccineList.AddedVaccines;
			saveButton.Text = "Save information to " + currentDog.DogName;
            picker.Date = vac.VaccineDate;
            picker.MinimumDate = DateTime.Today;
            pickerRemind.MaximumDate = picker.Date;
			pickerRemind.MinimumDate = DateTime.Today;

			if (vac.VaccineRemind != DateTime.MinValue) 
            {
                switchForRemind.IsToggled = true;
                pickerRemind.Date = vac.VaccineRemind;
            }

			vaccineName.SelectedItem = vac.VaccineName;
            notes.Text = vac.Notes;
		}
         
        public Vaccinations() {}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        void AddButtonClicked(object sender, System.EventArgs e)
        {
            
            if (hideElements.IsVisible)
                vac.VaccineRemind = pickerRemind.Date;
            else
                vac.VaccineRemind = DateTime.MinValue;

            vac.VaccineDate = picker.Date;
            vac.VaccineName = vaccineName.SelectedItem;
            vac.Notes = notes.Text;
            vac.itemInList = currentDog.TotalVaccines;
            if (!editMode)
            {
                currentDog.vaccineList.Add(vac);
                currentDog.TotalVaccines++;
            }
            SaveAndLoad.OverwriteFile(currentDog);
            Navigation.PopModalAsync(true);
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (switchForRemind.IsToggled)
                hideElements.IsVisible = true;
            else
                hideElements.IsVisible = false;
        }

        //TODO: Make these buttons instead!
        async void Handle_SelectedIndexChangedAsync(object sender, System.EventArgs e)
        {
            if (vaccineName.SelectedItem.ToString().Equals("Create New"))
            {
                UserDialogs.Instance.Prompt(new PromptConfig
                {
                    Title = "Please enter a vaccine or medication name.",
                    OnAction = new Action<PromptResult>((obj) =>
                   {
                       if (obj.Text.Length != 0)
                       {
                           AddedVaccineList.AddedVaccines.Add(obj.Text);
                       }
                   })
                });
            }
            else if (vaccineName.SelectedItem.ToString().Equals("Delete"))
            {
                var result = await DisplayAlert("Delete", "Please select a vaccine or mediaction to delete.", "Ok", "Cancel");

                if (result == true)
                {
                    vaccineName.Focus();

                }
            }
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            pickerRemind.MaximumDate = picker.Date;
		}
    }
}
