using System;
using System.Collections.Generic;

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
        public bool VaccineInfoAdded { get; set; }
        public List<string> vaccineList { get; set; }

        public Dog(string dogName, DateTime breedingDate, int placeInList)
        {
            //int daysLeft = CalculateDate.DaysSubtracted(CalculateDate.NumberOfDays(breedingDate, 63));
            this.DogName = dogName;
            this.BreedingDate = breedingDate;
            this.PlaceInList = placeInList;
            this.VaccineInfoAdded = false;
        }
    }
}
