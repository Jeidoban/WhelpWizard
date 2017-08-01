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

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Vaccine getInfo = (Vaccine)mi.BindingContext;
            int index = getInfo.itemInList;

            var decision = await DisplayActionSheet("Are you sure you want to delete " + getInfo.VaccineNameString + "?", "Cancel", "Delete");

            if (decision == "Delete")
            {
                for (int i = index + 1; i < currentDog.vaccineList.Count; i++)
                {
                    currentDog.vaccineList[i].itemInList--;
				}
                //Almost there
				currentDog.vaccineList.RemoveAt(index);
                currentDog.TotalVaccines = currentDog.vaccineList.Count;
			}
        }
    }
}
