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
            //thing();
        }

        public ListOfDams()
        {
            
        }

        public void addDog(Dog dog)
        {
            this.dog.Add(dog);   
        }

        void OnMore(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
			DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

		void OnDelete(object sender, System.EventArgs e)
		{
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            string dogName = ((Dog)e.Item).DogName; //This is how you get data from a cell!!!!
            DateTime breedingDate = ((Dog)e.Item).BreedingDate;

            //DisplayAlert("titld", thing, "ok"); 
            Navigation.PushAsync(new DamInformation(dogName, breedingDate));
        }
    }
}
