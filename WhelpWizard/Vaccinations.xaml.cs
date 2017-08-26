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
        string defaultMed1 = "medication 1";
        string defaultMed2 = "medication 2";
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
			vaccineName.SelectedItem = medsList[0];
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
            if (vaccineName.SelectedItem != null) 
                vaccineName.SelectedItem = vac.VaccineName;
            else
                vaccineName.SelectedItem = medsList[0];

            notes.Text = vac.Notes;
        }

        void SetMeds()
        {
            medsList = SaveAndLoad.LoadVaccines();
            medsList.Add(defaultMed1);
            medsList.Add(defaultMed2);
            vaccineName.ItemsSource = medsList;
        }

        public Vaccinations() { }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync(true);
            medsList.Remove(defaultMed1);
            medsList.Remove(defaultMed2);
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


            }
            else if (editMode && pickerRemind.Date != notifDateTemp)
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

            medsList.Remove(defaultMed1);
            medsList.Remove(defaultMed2);

            SaveAndLoad.OverwriteFile(currentDog);
            Navigation.PopModalAsync(true);
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            if (switchForRemind.IsToggled)
                hideElements.IsVisible = true;
            else
                hideElements.IsVisible = false;
        }

        void PlusButtonClicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.Prompt(new PromptConfig
            {
                Title = "Please enter a vaccine or medication name.",
                OnAction = new Action<PromptResult>((obj) =>
                {
                    if (obj.Ok)
                    {
                        if (obj.Text.Length != 0 && obj.Text.ToLower() != defaultMed1.ToLower() && obj.Text.ToLower() != defaultMed2.ToLower())
                        {
                            medsList.Remove(defaultMed1);
                            medsList.Remove(defaultMed2);
                            medsList.Insert(0, obj.Text);
                            SaveAndLoad.SaveVaccines(medsList);
                            medsList.Add(defaultMed1);
                            medsList.Add(defaultMed2);
                            vaccineName.SelectedItem = medsList[0];
                        }
                        else if (obj.Text.ToLower() == defaultMed1.ToLower() || obj.Text.ToLower() == defaultMed2.ToLower())
                        {
                            DisplayAlert("Invalid name", "Default vaccines can't be re-entered.", "Ok");
                        }
                        else
                        {
                            DisplayAlert("Invalid name", "Text box needs to be filled.", "Ok");
                        }
                    }
                })
            });
        }

        async void MinusButtonClicked(object sender, EventArgs e)
        {
            if (vaccineName.SelectedItem.ToString() != defaultMed1 && vaccineName.SelectedItem.ToString() != defaultMed2)
            {
                var result = await DisplayActionSheet("Are you sure you want to delete " + vaccineName.SelectedItem + "?", "Cancel", "Delete");

                if (result == "Delete")
                {
					medsList.Remove(defaultMed1);
					medsList.Remove(defaultMed2);
                    medsList.Remove(vaccineName.SelectedItem.ToString());
                    SaveAndLoad.SaveVaccines(medsList);
					medsList.Add(defaultMed1);
					medsList.Add(defaultMed2);
                    vaccineName.SelectedItem = medsList[0];
                }
            }
            else
            {
                await DisplayAlert("Can't Delete", "Default medications and vaccines cannot be deleted.", "Ok");
            }
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            pickerRemind.MaximumDate = picker.Date;
        }
    }
}
