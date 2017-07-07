using System;
namespace WhelpWizard
{
    public class CalculateDate
    {
        public CalculateDate()
        {
        }

        // This will calculate the date the number of days ahead of the date passed in. 
        // Returns a formatted string of the result.
        public static String NumberOfDays(DateTime dateText, double gestationDays)
		{
            DateTime date = dateText.AddDays(gestationDays);
			return date.ToString("ddd, MMM d, yyyy");
		}

        //Link the above method but returns a datetime instead
        public static DateTime NumberOfDaysDateTime(DateTime dateText, double gestationDays)
		{
			return dateText.AddDays(gestationDays);
		}

		public static int DaysSubtracted(string endDateString)
		{
			DateTime endDate = DateTime.Parse(endDateString);
			DateTime currentDate = DateTime.Now;
			TimeSpan dates = endDate.Subtract(currentDate);
			int number = dates.Days;
			return number;
		}

		public static int DaysSubtractedBegin(string startDateString)
		{
			DateTime startDate = DateTime.Parse(startDateString);
			DateTime currentDate = DateTime.Now;
			TimeSpan dates = currentDate.Subtract(startDate);
			int number = dates.Days;
			return number;
		}
    }
}
