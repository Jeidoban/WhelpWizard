/****************************************************
 * This was created by Jade Westover for BreederZoo *
 ****************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;

namespace WhelpWizard
{
    public partial class Calculator : ContentPage
    {
        string max; // This makes the user doesn't enter a name more that 30 characters long.
		public ListOfDams list; // This is the list of Dams page.
        Dog dog; // Holds information on a dog.
        SaveAndLoad saveLoad; // File writing and reading class.
        ObservableCollection<Dog> dogList; // A list of dogs. Used for populating the List of dams page.

        // I'm just initializing most of the XAML elemnts here.
        public Calculator()
        {
            InitializeComponent();
            saveLoad = new SaveAndLoad();
            dogList = new ObservableCollection<Dog>(); // This needs to be initialized on app startup, regardless on what page it starts on.
            list = new ListOfDams(PopulateList(dogList));
			dogIsDue.Text = "Dam is Due: ";
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            pregnancyInfo.Text = PregnancyInfo.firstStage;
            stepper.Value = 1;
            stepper.Minimum = 1;
            stepper.Maximum = 6;
            stepper.Increment = 1;
        }

        // Pretty much a shell function because it used to be an async method. I didn't feel like changing it after it worked lol.
        public ObservableCollection<Dog> PopulateList(ObservableCollection<Dog> dog)
        {
            var thing = saveLoad.LoadFromfile(dog);
            return thing.Result; // APPEND RESULT ON THE END OF TASKS TO GET THE ACTUAL OBJECT. God that took me way to long to figure out lol. 
                                 // There goes 4 hours of my life >:(
        }

        // This fires when the date is changed in the picker.
        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            timeSpan.Text = picker.Date.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(picker.Date, 14);
            stepper.Value = 1;
        }

        //This fires when the user clicks the plus or minus buttons. The if else
        //statments decide which date range and pregnancy info is displayed.
        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if ((int)stepper.Value == 1) 
            {
                pregnancyInfo.Text = PregnancyInfo.firstStage;
                timeSpan.Text = picker.Date.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(picker.Date, 14);
            } else if ((int)stepper.Value == 2)
            {
                pregnancyInfo.Text = PregnancyInfo.secondStage;
                timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 15) + " - " + CalculateDate.NumberOfDays(picker.Date, 21);
            }
			else if ((int)stepper.Value == 3)
			{
                pregnancyInfo.Text = PregnancyInfo.thirdStage;
                timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 22) + " - " + CalculateDate.NumberOfDays(picker.Date, 28);
			}
			else if ((int)stepper.Value == 4)
			{
                pregnancyInfo.Text = PregnancyInfo.fourthStage;
                timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 29) + " - " + CalculateDate.NumberOfDays(picker.Date, 35);
			}
			else if ((int)stepper.Value == 5)
			{
                pregnancyInfo.Text = PregnancyInfo.fifthStage;
                timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 36) + " - " + CalculateDate.NumberOfDays(picker.Date, 49);
			}
			else if ((int)stepper.Value == 6)
			{
                pregnancyInfo.Text = PregnancyInfo.sixthStage;
                timeSpan.Text = CalculateDate.NumberOfDays(picker.Date, 50) + " - " + CalculateDate.NumberOfDays(picker.Date, 63);
			}
        }

        //This will push the user to the dams list page.
		void GoToListOfDams(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(list);
		}

        //Fires when the dog name is changed. Changes the "Dam is Due" label to "'dog name' is due".
        //Also restricts the user from typing more than 30 characters.
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            dogIsDue.Text = dogName.Text + " is due: ";

            if (dogName.Text.Length == 0)
            {
                dogIsDue.Text = "Dam is due: ";
            }

            if (dogName.Text.Length == 30)
            {
                max = dogName.Text;	
            }

            if (dogName.Text.Length > 30)
			{
				dogName.Text = max;
			}
        }

        // Saves a dog and adds it to the dams list.
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            dog = new Dog(dogName.Text, picker.Date, Counter.totalDogs++);
            list.addDog(dog);
            await saveLoad.WriteToFile(dog);
        }
    }
}
