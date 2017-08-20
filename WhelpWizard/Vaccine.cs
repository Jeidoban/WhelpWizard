using System;
namespace WhelpWizard
{
    public class Vaccine
    {

        public string VaccineNameString
        {
            get
            {
                return VaccineName.ToString();
            }
        }
        public object VaccineName { get; set; }
        public DateTime VaccineDate { get; set; }
        public string VaccineDateString
        {
            get
            {
                return VaccineDate.ToString("ddd, MMM d, yyyy");
            }
        }
        public DateTime VaccineRemind { get; set; }
        public String Notes { get; set; }
        public int itemInList { set; get; }
        public int notificationId { get; set; }

        public Vaccine()
        {
            itemInList = 0;
            VaccineName = new object();
        }
    }
}
