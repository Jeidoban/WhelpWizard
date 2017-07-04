using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class Calculator : ContentPage
    {
        public Calculator()
        {
            InitializeComponent();
            calculatedDate.Text = DateTime.Today.ToString("ddd, MMM d, yyyy");
            pregnancyInfo.Text = PregnancyInfo.firstStage;
            stepper.Value = 1;
            stepper.Minimum = 1;
            stepper.Maximum = 6;
            stepper.Increment = 1;
            timeSpan.Text = DateTime.Today.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(DateTime.Today, 14);
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 52);
        }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if ((int)stepper.Value == 1) 
            {
                pregnancyInfo.Text = PregnancyInfo.firstStage;
            } else if ((int)stepper.Value == 2)
            {
                pregnancyInfo.Text = PregnancyInfo.secondStage;
            }
			else if ((int)stepper.Value == 3)
			{
                pregnancyInfo.Text = PregnancyInfo.thirdStage;
			}
			else if ((int)stepper.Value == 4)
			{
                pregnancyInfo.Text = PregnancyInfo.fourthStage;
			}
			else if ((int)stepper.Value == 5)
			{
                pregnancyInfo.Text = PregnancyInfo.fifthStage;
			}
			else if ((int)stepper.Value == 6)
			{
                pregnancyInfo.Text = PregnancyInfo.sixthStage;
			}
        }
    }
}
