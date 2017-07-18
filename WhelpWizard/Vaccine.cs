using System;
namespace WhelpWizard
{
    public class Vaccine
    {

        public string VaccineName { get; set; }
        public DateTime VaccineDate { get; set; }
        public string VaccineDateString
        {
            get
            {
                return VaccineDate.ToString("ddd, MMM d, yyyy");
            }
        }

        public Vaccine()
        {
        }
    }
}
