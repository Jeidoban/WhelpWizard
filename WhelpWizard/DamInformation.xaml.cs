using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class DamInformation : ContentPage
    {
        string dogName;
        DateTime breedingDate;

        public DamInformation(string dogName, DateTime breedingDate)
        {
            InitializeComponent();

            this.dogName = dogName;
            this.breedingDate = breedingDate;
            damName.Text = dogName;
            pregDateLabel.Text = "Due " + CalculateDate.NumberOfDays(breedingDate, 63);
			stepper.Value = getCurrentDate();
			stepper.Minimum = 1;
			stepper.Maximum = 6;
        }

        public DamInformation() { }

        public void SetDates(object sender, Xamarin.Forms.ValueChangedEventArgs e) 
        {
			if ((int)stepper.Value == 1)
			{
                pregInfo.Text = PregnancyInfo.firstStage;
                pregDate.Text = breedingDate.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(breedingDate, 14);
			}
			else if ((int)stepper.Value == 2)
			{
				pregInfo.Text = PregnancyInfo.secondStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 15) + " - " + CalculateDate.NumberOfDays(breedingDate, 21);
			}
			else if ((int)stepper.Value == 3)
			{
				pregInfo.Text = PregnancyInfo.thirdStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 22) + " - " + CalculateDate.NumberOfDays(breedingDate, 28);
			}
			else if ((int)stepper.Value == 4)
			{
				pregInfo.Text = PregnancyInfo.fourthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 29) + " - " + CalculateDate.NumberOfDays(breedingDate, 35);
			}
			else if ((int)stepper.Value == 5)
			{
				pregInfo.Text = PregnancyInfo.fifthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 36) + " - " + CalculateDate.NumberOfDays(breedingDate, 49);
			}
			else if ((int)stepper.Value == 6)
			{
				pregInfo.Text = PregnancyInfo.sixthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 50) + " - " + CalculateDate.NumberOfDays(breedingDate, 63);
			}
        }

        public int getCurrentDate()
        {
            int stepperSet = 0;
            if (DateTime.Today >= breedingDate.AddDays(0d) && DateTime.Today <= breedingDate.AddDays(14d))
            {
				pregInfo.Text = PregnancyInfo.firstStage;
				pregDate.Text = breedingDate.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(breedingDate, 14d);
                stepperSet = 1;
            } else if (DateTime.Today >= breedingDate.AddDays(15d) && DateTime.Today <= breedingDate.AddDays(21d))
            {
				pregInfo.Text = PregnancyInfo.secondStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 15d) + " - " + CalculateDate.NumberOfDays(breedingDate, 21d);
                stepperSet = 2;
			} else if (DateTime.Today >= breedingDate.AddDays(22d) && DateTime.Today <=breedingDate.AddDays(28d))
			{
                pregInfo.Text = PregnancyInfo.thirdStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 22d) + " - " + CalculateDate.NumberOfDays(breedingDate, 28d);
                stepperSet = 3;
			} else if (DateTime.Today >= breedingDate.AddDays(29d) && DateTime.Today <= breedingDate.AddDays(35d))
			{
                pregInfo.Text = PregnancyInfo.fourthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 29d) + " - " + CalculateDate.NumberOfDays(breedingDate, 35d);
                stepperSet = 4;
			} else if (DateTime.Today >= breedingDate.AddDays(36d) && DateTime.Today <= breedingDate.AddDays(49d))
			{
                pregInfo.Text = PregnancyInfo.fifthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 36d) + " - " + CalculateDate.NumberOfDays(breedingDate, 49d);
                stepperSet = 5;
			} else if (DateTime.Today >= breedingDate.AddDays(50d) && DateTime.Today <= breedingDate.AddDays(63d))
			{
                pregInfo.Text = PregnancyInfo.sixthStage;
				pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 50d) + " - " + CalculateDate.NumberOfDays(breedingDate, 63d);
                stepperSet = 6;
			}

            return stepperSet;
        }
    }
}
