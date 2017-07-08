using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class ListOfDams : ContentPage
    {
        ObservableCollection<Dog> dog;

		public ListOfDams(ObservableCollection<Dog> dog)
        {
            InitializeComponent();
            this.dog = dog;
            damsList.ItemsSource = dog; // IMPORTANT! The listview in the XAML has to be connected to an Observable list
        }

        public ListOfDams()
        {
            
        }

        public void addDog(Dog dog)
        {
            this.dog.Add(dog);   
        }

        // I needed to get the index of the item in the list so I could delete it from the list. 
        // After like 4 hours of trying to figure it out, I found out I could get the object the
        // menu item is attatched to. So I decided to add a list counter variable in the dog class.
        // Now this code will pull the right index from the list.
		void OnDelete(object sender, System.EventArgs e)
		{
            var mi = ((MenuItem)sender); 
            Dog getInfo = (Dog)mi.BindingContext;
            int index = getInfo.PlaceInList;

            //TODO: You need to update the PlaceInList variable in all dog objects
            //when one is deleted. If you don't terrible things are gonna happen lol.

            DisplayAlert("dfdf", index.ToString(), "dfdf");
		}

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            string dogName = ((Dog)e.SelectedItem).DogName; //This is how you get data from a cell!!!!
            DateTime breedingDate = ((Dog)e.SelectedItem).BreedingDate;
			Navigation.PushAsync(new DamInformation(dogName, breedingDate));
        }
    }
}
