using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class ListOfDams : ContentPage
    {
        ObservableCollection<Dog> dogs;
        ObservableCollection<Dog> dogsTemp;
        int searchBarText = 0;

		public ListOfDams(ObservableCollection<Dog> dogs)
        {
            InitializeComponent();
            this.dogs = dogs;
            damsList.ItemsSource = dogs; // IMPORTANT! The listview in the XAML has to be connected to an Observable list
            dogsTemp = new ObservableCollection<Dog>(dogs);
        }

        public ListOfDams()
        {
            
        }

        public void addDog(Dog dog)
        {
            this.dogs.Add(dog);   
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

            SaveAndLoad.DeleteCell(dogs, index);
		}

        void Handle_ItemSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            string dogName = ((Dog)e.Item).DogName; //This is how you get data from a cell!!!!
            DateTime breedingDate = ((Dog)e.Item).BreedingDate;
			Navigation.PushAsync(new DamInformation(dogName, breedingDate));
        }

        //TODO: Make it so when you add a letter a remove loop runs and when you remove a letter an add loop runs.
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            damsList.ItemsSource = dogsTemp;

            if (searchBarText < searchBar.Text.Length)
            {
                for (int i = 0; i < dogsTemp.Count; i++)
                {
                    if (!dogsTemp[i].DogName.Contains(searchBar.Text))
                    {
                        dogsTemp.RemoveAt(i);
                        i--;
                    }
                }
                searchBarText++;
            }
            else
            {
                //TODO: For some reason it's deleting it one behind figure it out.
				dogsTemp = new ObservableCollection<Dog>(dogs);

                if (searchBar.Text.Length == 0)
                {
                    damsList.ItemsSource = dogs;
                    searchBarText = 0;
                }
                else
                {
					for (int i = 0; i < dogsTemp.Count; i++)
					{
						if (!dogsTemp[i].DogName.Contains(searchBar.Text))
						{
							dogsTemp.RemoveAt(i);
							i--;
						}
					}
                }
                searchBarText--;
            }
        }
    }
}
