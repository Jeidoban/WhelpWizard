using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Plugin.LocalNotifications;
using PCLStorage;

namespace WhelpWizard
{
    public partial class WhelpWizardPage : ContentPage
    {
        public ListOfDams list; // This is the list of Dams page.
        public Calculator calc;
        ObservableCollection<Dog> dogList; // A list of dogs. Used for populating the List of dams page.

		public WhelpWizardPage()
        {
            InitializeComponent();
			dogList = new ObservableCollection<Dog>(); // This needs to be initialized on app startup, regardless on what page it starts on.
			list = new ListOfDams(PopulateList(dogList));
            calc = new Calculator(list);

        }

        void GoToCalculator(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(calc);
        }

		public ObservableCollection<Dog> PopulateList(ObservableCollection<Dog> dog)
		{
			var thing = SaveAndLoad.LoadFromfile(dog);
			return thing.Result; // APPEND RESULT ON THE END OF TASKS TO GET THE ACTUAL OBJECT. God that took me way to long to figure out lol. 
								 // There goes 4 hours of my life >:(
		}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(list);
        }
    }
}
