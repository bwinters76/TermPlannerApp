using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermPlannerApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTermPage : ContentPage
    {
        private Term term;
        public AddTermPage()
        {
            InitializeComponent();
            term = new Term();
            endDatePicker.Date = startDatePicker.Date.AddMonths(6).AddDays(-1);
        }

        public AddTermPage(Term existingTerm)
        {
            InitializeComponent();
            term = existingTerm;
            termName.Text = term.Name;
            startDatePicker.Date = term.Start;
            endDatePicker.Date = term.End;
        }

        private void StartDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            endDatePicker.Date = startDatePicker.Date.AddMonths(6).AddDays(-1);
        }

        private void SaveTermButton_OnClicked(object sender, EventArgs e)
        {
            term.Name = termName.Text;
            term.Start = startDatePicker.Date;
            term.End = endDatePicker.Date;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                if (term.Id != 0) conn.Update(term);
                else conn.Insert(term);
            }

            Navigation.PopAsync();
        }
    }
}