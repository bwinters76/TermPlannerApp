using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971Application.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTermPage : ContentPage
    {
        public AddTermPage()
        {
            InitializeComponent();
            endDatePicker.Date = startDatePicker.Date.AddMonths(6).AddDays(-1);
        }

        private void StartDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            endDatePicker.Date = startDatePicker.Date.AddMonths(6).AddDays(-1);
        }

        private void SaveTermButton_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}