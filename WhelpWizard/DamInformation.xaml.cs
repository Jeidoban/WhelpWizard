using System;
using System.Collections.Generic;
using Plugin.Share;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class DamInformation : ContentPage
    {
        string dogName;
        DateTime breedingDate;
        int stepperValue;

        public DamInformation(string dogName, DateTime breedingDate)
        {
            InitializeComponent();

            this.dogName = dogName;
            this.breedingDate = breedingDate;
            damName.Text = dogName;
            pregDateLabel.Text = CalculateDate.NumberOfDays(breedingDate, 63);
            breedingDateLabel.Text = CalculateDate.NumberOfDays(breedingDate, 0);
            DaysLeft();
            stepperValue = getCurrentDate();

            if (stepperValue == 1)
            {
                stepperLeft.IsEnabled = false;
            } else if (stepperValue == 6)
            {
                stepperRight.IsEnabled = false;
            }
            ToolbarItem thing = new ToolbarItem();

            ToolbarItems.Add(new ToolbarItem("", "ShareSymbolXam.png", HandleActionAsync, ToolbarItemOrder.Default));
            ToolbarItems.Add(new ToolbarItem("", "EditSymbolXam.png", () => DisplayAlert("Clicked", "Clicked Share", "ok"), ToolbarItemOrder.Default));


        
			//stepper.Value = getCurrentDate();
			//stepper.Minimum = 1;
			//stepper.Maximum = 6;
        }
        public DamInformation() { }

        async void HandleActionAsync()
        {
            //TODO: make this so it shares dog info.
            var messageToSend = new Plugin.Share.Abstractions.ShareMessage
            {
                Title = "YAY",
                Text = "WOO"
            };

            await CrossShare.Current.Share(messageToSend);
        }

        //This works the same way as the one in the calculator class.
        public void PregnancyCases() 
        {
            switch (stepperValue)
            {
                case 1:
                    pregInfo.Text = PregnancyInfo.firstStage;
                    pregDate.Text = breedingDate.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(breedingDate, 14);
                    break;
                case 2:
                    pregInfo.Text = PregnancyInfo.secondStage;
                    pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 15) + " - " + CalculateDate.NumberOfDays(breedingDate, 21);
                    break;
                case 3:
                    pregInfo.Text = PregnancyInfo.thirdStage;
                    pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 22) + " - " + CalculateDate.NumberOfDays(breedingDate, 28);
                    break;
                case 4:
                    pregInfo.Text = PregnancyInfo.fourthStage;
                    pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 29) + " - " + CalculateDate.NumberOfDays(breedingDate, 35);
                    break;
                case 5:
                    pregInfo.Text = PregnancyInfo.fifthStage;
                    pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 36) + " - " + CalculateDate.NumberOfDays(breedingDate, 49);
                    break;
                case 6:
                    pregInfo.Text = PregnancyInfo.sixthStage;
                    pregDate.Text = CalculateDate.NumberOfDays(breedingDate, 50) + " - " + CalculateDate.NumberOfDays(breedingDate, 63);
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

        // Same as the one above, except will take today's date and display the correct stage of pregnancy the dog is in when this
        // view is triggered.
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
			} else 
            {
				pregInfo.Text = PregnancyInfo.firstStage;
				pregDate.Text = breedingDate.ToString("ddd, MMM d, yyyy") + " - " + CalculateDate.NumberOfDays(breedingDate, 14d);
				stepperSet = 1;
            }
            return stepperSet;
        }

        // Calculates the days until the dog delivers.
        public void DaysLeft()
        {
            int daysLeft = CalculateDate.DaysSubtracted(CalculateDate.NumberOfDays(breedingDate, 63));
			if (daysLeft > 1)
                daysLeftLabel.Text = daysLeft + " days until due";
			else if (daysLeft == 0)
                daysLeftLabel.Text = "Due today";
			else if (daysLeft < 0)
                daysLeftLabel.Text = "Was due " + Math.Abs(daysLeft) + " days ago";
            else if (daysLeft == 1)
                daysLeftLabel.Text = daysLeft + " day until due";
        }
    }
}
