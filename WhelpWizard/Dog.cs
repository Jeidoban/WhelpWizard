using System;

namespace WhelpWizard
{
    public class Dog
    {
        public string DogName{ get; set; }
        public DateTime BreedingDate { get; set; }
        public string DueDate { get; set; }

        public Dog(string dogName, DateTime breedingDate)
        {
            this.DogName = dogName;
            this.BreedingDate = breedingDate;
            this.DueDate = DogName + " is due " + CalculateDate.NumberOfDays(BreedingDate, 63);
        }

        public Dog()
        {
            
        }
    }
}
