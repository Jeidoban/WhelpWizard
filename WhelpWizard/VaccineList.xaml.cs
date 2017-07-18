using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class VaccineList : ContentPage
    {
        ObservableCollection<Vaccine> vaccineList;
        Dog currentDog;

        public VaccineList(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            vaccineList = new ObservableCollection<Vaccine>();
            vaccineListShow.ItemsSource = vaccineList;
            string plus = "+";
            ToolbarItems.Add(new ToolbarItem(plus, null, HandleAction, ToolbarItemOrder.Default));
        }

		public VaccineList()
		{
		}

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        void HandleAction()
        {
            Navigation.PushModalAsync(new Vaccinations(currentDog, this));
        }
    }
}
