using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WhelpWizard
{
    // This class just holds dog information.
    public class Dog
    {
        public string DogName { get; set; }
        public DateTime BreedingDate { get; set; }
        public string DueDate
        {
            get
            {
				if (DateTime.Today > BreedingDate.AddDays(63))
					return "Was due " + CalculateDate.NumberOfDays(BreedingDate, 63);
                else
                    return "Due " + CalculateDate.NumberOfDays(BreedingDate, 63);
			}
        }
        public int PlaceInList { get; set; }
        public ObservableCollection<Vaccine> vaccineList { get; set; }
        public int TotalVaccines { get; set; }
        public int[] notificationIds { get; set; }

        public Dog(string dogName, DateTime breedingDate, int placeInList)
        {
            notificationIds = new int[6];
            this.DogName = dogName;
            this.BreedingDate = breedingDate;
            this.PlaceInList = placeInList;
            vaccineList = new ObservableCollection<Vaccine>();
        }
    }
}
