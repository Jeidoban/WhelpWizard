using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WhelpWizard
{
    public partial class Calculator : ContentPage
    {
        public Calculator()
        {
            InitializeComponent();
            calculatedDate.Text = DateTime.Today.ToString("ddd, MMM d, yyyy");
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            calculatedDate.Text = CalculateDate.NumberOfDays(picker.Date, 52);
        }
    }
}
