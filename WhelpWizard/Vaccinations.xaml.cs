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
using Plugin.LocalNotifications;

namespace WhelpWizard
{
    public partial class Vaccinations : ContentPage
    {
        Dog currentDog; // Passed in from where this class is called from.
        bool editMode; // This decides if this class was triggered but clicking a new vaccine, or editing an existing one. This is why there are two constructors.
        Vaccine vac; // The vaccine object.
        ObservableCollection<string> medsList;


        public Vaccinations(Dog currentDog)
        {
            InitializeComponent();
            SetMeds();
            this.currentDog = currentDog;
            vac = new Vaccine();
            saveButton.Text = "Save information to " + currentDog.DogName;
			pickerRemind.MaximumDate = picker.Date;
            pickerRemind.MinimumDate = DateTime.Today;
            picker.MinimumDate = DateTime.Today;
		}

        public Vaccinations(Dog currentDog, Vaccine vac)
        {
			InitializeComponent();
            SetMeds();
            editMode = true;
			this.currentDog = currentDog;
            this.vac = vac;
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

        void SetMeds() {
            medsList = new ObservableCollection<string>();
            medsList.Add("medication 1");
            medsList.Add("medication 2");
            vaccineName.ItemsSource = medsList;
            vaccineName.SelectedItem = medsList[0];
        }
         
        public Vaccinations() {}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        void AddButtonClicked(object sender, System.EventArgs e)
        {
            DateTime notifDateTemp = vac.VaccineRemind;

            if (hideElements.IsVisible)
            {
                vac.VaccineRemind = pickerRemind.Date;
            }
            else
                vac.VaccineRemind = DateTime.MinValue;
            
            vac.VaccineDate = picker.Date;
            vac.VaccineName = vaccineName.SelectedItem;
            vac.Notes = notes.Text;

            if (!editMode)
            {
                vac.itemInList = currentDog.TotalVaccines;
                currentDog.vaccineList.Add(vac);
				currentDog.TotalVaccines++;

                if (hideElements.IsVisible)
                {
					CrossLocalNotifications.Current.Show("Reminder", "Reminder that " + vaccineName.SelectedItem + " for " +
													currentDog.DogName + " is due " + picker.Date.ToString("D") +
													", which is in " + (picker.Date - pickerRemind.Date).Days +
														" days.", SaveAndLoad.notificationId, pickerRemind.Date.AddHours(12));

					vac.notificationId = SaveAndLoad.notificationId;
					SaveAndLoad.SaveVaccineNotificationId();
                }
                   

            } else if (editMode && pickerRemind.Date != notifDateTemp)
            {
                if (hideElements.IsVisible && notifDateTemp == DateTime.MinValue)
                {
					vac.notificationId = SaveAndLoad.notificationId;
                    SaveAndLoad.SaveVaccineNotificationId();
				}
    
                if (vac.VaccineRemind != DateTime.MinValue)
                {
					CrossLocalNotifications.Current.Show("Reminder", "Reminder that " + vaccineName.SelectedItem + " for " +
													 currentDog.DogName + " is due " + picker.Date.ToString("D") +
													 ", which is in " + (picker.Date - pickerRemind.Date).Days +
													 " days.", vac.notificationId, pickerRemind.Date.AddHours(12));
                } 
            }

            if (vac.VaccineRemind.Date == DateTime.MinValue && vac.notificationId != 0)
            {
				CrossLocalNotifications.Current.Cancel(vac.notificationId);
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
