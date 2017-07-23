using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class VaccineList : ContentPage
    {
        public Dog currentDog;

        public VaccineList(Dog currentDog)
        {
            InitializeComponent();
            this.currentDog = currentDog;
            vaccineListShow.ItemsSource = currentDog.vaccineList;
            string plus = "+";
            ToolbarItems.Add(new ToolbarItem(plus, null, HandleAction, ToolbarItemOrder.Default));
        }

		public VaccineList()
		{
		}

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Vaccine currentVaccine = ((Vaccine)e.Item);
            Navigation.PushModalAsync(new Vaccinations(currentDog, currentVaccine));
        }

        void HandleAction()
        {
            Navigation.PushModalAsync(new Vaccinations(currentDog));
        }
    }
}
