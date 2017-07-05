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
            damsList.ItemsSource = dog; // IMPORTANT! The listview in the XAML has to be connected to a dams list.
        }

        public ListOfDams()
        {
            
        }

        public void addDog(Dog dog)
        {
            this.dog.Add(dog);   
        }
    }
}
