using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WhelpWizard
{
    public partial class Calculator : ContentPage
    {
        string max;
		public ListOfDams list;
        Dog dog;

        public Calculator()
        {
            InitializeComponent();
            list = new ListOfDams();
			dogIsDue.Text = "Dam is Due: ";
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            pregnancyInfo.Text = PregnancyInfo.firstStage;
            stepper.Value = 1;
            stepper.Minimum = 1;
            stepper.Maximum = 6;
            stepper.Increment = 1;
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 63);
            timeSpan.Text = picker.Date.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(picker.Date, 14);
            stepper.Value = 1;
        }

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

		void GoToListOfDams(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(list);
		}

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

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            dog = new Dog(dogName.Text, picker.Date);
            list.addDog(dog);
        }
    }
}
