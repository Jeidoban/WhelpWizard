using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class ListOfDams : ContentPage
    {
        ObservableCollection<Dog> dogs;
        ObservableCollection<Dog> dogsTemp;
        //int searchBarText = 0;

		public ListOfDams(ObservableCollection<Dog> dogs)
        {
            InitializeComponent();
            this.dogs = dogs;
            damsList.ItemsSource = dogs; // IMPORTANT! The listview in the XAML has to be connected to an Observable list
            dogsTemp = new ObservableCollection<Dog>(dogs);
        }

        public ListOfDams(Dog currentDog)
        {
            damsList.BeginRefresh();
            damsList.EndRefresh();
        }
        //TODO: This needs to be initialized again when an edit is complete.

        public ListOfDams()
        {}

        public void addDog(Dog dog)
        {
            this.dogs.Add(dog);   
        }

        async void OnDeleteAsync(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Dog getInfo = (Dog)mi.BindingContext;
            int index = getInfo.PlaceInList;

            var decision = await DisplayActionSheet("Are you sure you want to delete " + getInfo.DogName + "?", "Cancel", "Delete");

            if (decision == "Delete")
            {
                await SaveAndLoad.DeleteCell(dogs, index);
                damsList.ItemsSource = dogs;
                searchBar.Text = "";
            }
        }

        public void RefreshList()
        {
            damsList.ItemsSource = null;
            damsList.ItemsSource = dogs;
        }

        void Handle_Appearing(object sender, System.EventArgs e)
        {
            RefreshList();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            //string dogName = ((Dog)e.Item).DogName; 
            //DateTime breedingDate = ((Dog)e.Item).BreedingDate;
            Dog currentDog = ((Dog)e.Item); //This is how you get data from a cell!!!!
            Navigation.PushAsync(new DamInformation(currentDog));
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                if (searchBar.Text.Length != 0)
                {
                    List<Dog> tempDogList = new List<Dog>(dogs);
                    tempDogList = tempDogList.FindAll(dog =>
                    {
                        if (dog.DogName.ToUpper().Contains(searchBar.Text.ToUpper()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });

                    dogsTemp = new ObservableCollection<Dog>(tempDogList);
                    damsList.ItemsSource = dogsTemp;
                }
                else
                {
                    damsList.ItemsSource = dogs;
                }
            }
            catch (Exception ex)
            {
                searchBar.Text = "";
            }
        }
    }
}
