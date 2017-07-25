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
        Vaccine vac;

        public Vaccinations(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            vac = new Vaccine();
            saveButton.Text = "Save information to " + currentDog.DogName;
            vacList = new ObservableCollection<String>();
			pickerRemind.MaximumDate = picker.Date;
            pickerRemind.MinimumDate = DateTime.Today;
			picker.MinimumDate = DateTime.Today;
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

			vaccineName.ItemsSource = vacList;
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
            if (!editMode) currentDog.vaccineList.Add(vac);
            SaveAndLoad.OverwriteFile(currentDog);
            currentDog.TotalVaccines++;
            Navigation.PopModalAsync(true);
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (switchForRemind.IsToggled)
                hideElements.IsVisible = true;
            else
                hideElements.IsVisible = false;
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            pickerRemind.MaximumDate = picker.Date;
		}
    }
}
