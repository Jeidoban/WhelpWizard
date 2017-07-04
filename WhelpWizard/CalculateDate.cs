using System;
namespace WhelpWizard
{
    public class CalculateDate
    {
        public CalculateDate()
        {
        }

        public static String NumberOfDays(DateTime dateText, double gestationDays)
		{
            DateTime date = dateText.AddDays(gestationDays);
			string actualDate = date.ToString("ddd, MMM d, yyyy");
			return actualDate;
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
