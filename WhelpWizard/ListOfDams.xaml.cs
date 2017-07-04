using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class ListOfDams : ContentPage
    {
        ObservableCollection<Dog> dog = new ObservableCollection<Dog>();
		public ListOfDams()
        {
            InitializeComponent();
            damsList.ItemsSource = dog;
        }

        public void addDog(Dog dog)
        {
            this.dog.Add(dog);   
        }
    }
}
