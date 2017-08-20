/****************************************************
 * This was created by Jade Westover for BreederZoo *
 ****************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Plugin.LocalNotifications;
using PCLStorage;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

namespace WhelpWizard
{
    public partial class Calculator : ContentPage
    {
        string max; // This makes the user doesn't enter a name more that 30 characters long.
        int stepperValue = 1;
		ListOfDams list; // This is the list of Dams page.
        Dog dog; // Holds information on a dog.
        bool editMode;
        DamInformation damInfo;
        //ObservableCollection<Dog> dogList; // A list of dogs. Used for populating the List of dams page.


        // I'm just initializing most of the XAML elemnts here.
        public Calculator(ListOfDams list)
        {
            InitializeComponent();
            this.list = list;
            dogName.Text = "";
			dogIsDue.Text = "Dam is Due: ";
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            pregnancyInfo.Text = PregnancyInfo.firstStage;
            stepperLeft.IsEnabled = false;
            PregnancyCases();
        }

        public void RefreshPage() {
			InitializeComponent();
		}

        public Calculator(Dog currentDog, DamInformation damInfo)
        {
			InitializeComponent();
            editBar.IsVisible = true;
            this.damInfo = damInfo;
			dog = currentDog;
            editMode = true;
            picker.Date = dog.BreedingDate;
            calculatedDate.Text = CalculateDate.NumberOfDays(dog.BreedingDate, 63);
            milestonesLabel.IsVisible = false;
            pregnancyInfo.IsVisible = false;
            stepperLeft.IsVisible = false;
            stepperRight.IsVisible = false;
            timeSpan.IsVisible = false;
			dogName.Text = dog.DogName;
        }

        public Calculator() {}

        // Pretty much a shell function because it used to be an async method. I didn't feel like changing it after it worked lol.
        public ObservableCollection<Dog> PopulateList(ObservableCollection<Dog> dog)
        {
            var thing = SaveAndLoad.LoadFromfile(dog);
            return thing.Result; // APPEND RESULT ON THE END OF TASKS TO GET THE ACTUAL OBJECT. God that took me way to long to figure out lol. 
                                 // There goes 4 hours of my life >:(
        }

        // This fires when the date is changed in the picker.
        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            timeSpan.Text = picker.Date.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(picker.Date, 14);
           // stepper.Value = 1;
        }

        //This fires when the user clicks the plus or minus buttons. The if else
        //statments decide which date range and pregnancy info is displayed.
        public void PregnancyCases()
        {
            switch (stepperValue)
            {
                case 1:
                    pregnancyInfo.Text = PregnancyInfo.firstStage;
                    timeSpan.Text = picker.Date.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(picker.Date, 14);
                    break;
                case 2:
                    pregnancyInfo.Text = PregnancyInfo.secondStage;
                    timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 15) + " - " + CalculateDate.NumberOfDays(picker.Date, 21);
                    break;
                case 3:
                    pregnancyInfo.Text = PregnancyInfo.thirdStage;
                    timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 22) + " - " + CalculateDate.NumberOfDays(picker.Date, 28);
                    break;
                case 4:
                    pregnancyInfo.Text = PregnancyInfo.fourthStage;
                    timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 29) + " - " + CalculateDate.NumberOfDays(picker.Date, 35);
                    break;
                case 5:
                    pregnancyInfo.Text = PregnancyInfo.fifthStage;
                    timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 36) + " - " + CalculateDate.NumberOfDays(picker.Date, 49);
                    break;
                case 6:
                    pregnancyInfo.Text = PregnancyInfo.sixthStage;
                    timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 50) + " - " + CalculateDate.NumberOfDays(picker.Date, 63);
                    break;
            }
        }

        void StepperPressedLeft(object sender, System.EventArgs e)
        {
            stepperValue--;

			if (stepperValue == 1)
			{   
                stepperLeft.IsEnabled = false;
                stepperRight.IsEnabled = true;
            }
            else 
			{
                stepperLeft.IsEnabled = true;
                stepperRight.IsEnabled = true;
			}

            PregnancyCases();
        }

		void StepperPressedRight(object sender, System.EventArgs e)
		{
			stepperValue++;

			if (stepperValue == 6)
			{
                stepperRight.IsEnabled = false;
                stepperLeft.IsEnabled = true;
			}
			else
			{
                stepperRight.IsEnabled = true;
                stepperLeft.IsEnabled = true;
			}

			PregnancyCases();
		}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        //This will push the user to the Vaccinations page
        async void GoToMoreAsync(object sender, System.EventArgs e)
		{
            //await Navigation.PushModalAsync(new Vaccinations(this));
		}

        //Fires when the dog name is changed. Changes the "Dam is Due" label to "'dog name' is due".
        //Also restricts the user from typing more than 30 characters.
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            dogIsDue.Text = dogName.Text + " is due: ";

            switch (dogName.Text.Length)
            {
                case 0:
                    dogIsDue.Text = "Dam is due: ";
                    break;
                case 50:
                    max = dogName.Text;
                    break;
            }

            if (dogName.Text.Length > 50)
			{
				dogName.Text = max;
			}
        }

        // Saves a dog and adds it to the dams list.
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if (dogName.Text.Length == 0)
            {
                await DisplayAlert("No name inserted!", "Please enter a name to save.", "Ok");
            }
            else if (dogName.Text.Length != 0 && !editMode)
            {
                dog = new Dog(dogName.Text, picker.Date, SaveAndLoad.fileNumber);
                Notifications(picker.Date, dogName.Text, dog);
                SaveAndLoad.SaveNotificationId();
				list.addDog(dog);
                await SaveAndLoad.WriteToFile(dog);
                await DisplayAlert("Dam Saved", dogName.Text + " has been saved into your phone.", "Ok");
                picker.Date = DateTime.Today;
                dogName.Text = "";
            } else {
                //TODO: Need to cancel notifications!
                dog.DogName = dogName.Text;

                if (dog.BreedingDate != picker.Date)
                {
                    Notifications(picker.Date, dogName.Text, dog);
                }

                dog.BreedingDate = picker.Date;
                await SaveAndLoad.OverwriteFile(dog);
				await DisplayAlert("Dam Edited", "Your changes to " + dogName.Text + " have been saved into your phone.", "Ok");
                damInfo.Setup(this.dog);
                await Navigation.PopModalAsync(true);
			}
        }

        //TODO: You need to think of a better way to do notifications. You need to be able to cancel them!
        public void Notifications(DateTime breedingDate, string name, Dog dog)
        {
            if (!editMode)
            {
                for (int i = 0; i < dog.notificationIds.Length; i++)
                {
                    dog.notificationIds[i] = SaveAndLoad.notificationId + i;
                }
            }

            var notif = CrossLocalNotifications.Current;
            notif.Show("test", "test", dog.notificationIds[5] + 1, DateTime.Now);
            notif.Show("New Milestone Achieved", name + " has 47 days until due! See what's happening with her pregnancy.", dog.notificationIds[0], breedingDate.AddDays(15).AddHours(12));
			notif.Show("New Milestone Achieved", name + " has 40 days until due! See what's happening with her pregnancy.", dog.notificationIds[1], breedingDate.AddDays(22).AddHours(12));
			notif.Show("New Milestone Achieved", name + " has 33 days until due! See what's happening with her pregnancy.", dog.notificationIds[2], breedingDate.AddDays(29).AddHours(12));
			notif.Show("New Milestone Achieved", name + " has 26 days until due! See what's happening with her pregnancy.", dog.notificationIds[3], breedingDate.AddDays(36).AddHours(12));
			notif.Show("New Milestone Achieved", name + " has 12 days until due! See what's happening with her pregnancy.", dog.notificationIds[4], breedingDate.AddDays(50).AddHours(12));
            notif.Show(name + " is almost due!", name + " is due any day now.", dog.notificationIds[5], breedingDate.AddDays(61).AddHours(12));
        }
    }
}
