using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TermPlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsPage : ContentPage
    {
        private Assessment assessment;
        public AssessmentsPage(Course existingCourse)
        {
            InitializeComponent();
            assessment = new Assessment();
            assessment.CourseId = existingCourse.Id;
            RefreshAssessmentList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshAssessmentList();
        }

        private void RefreshAssessmentList()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Assessment>();
                var assessments = (from a in conn.Table<Assessment>()
                    where a.CourseId == assessment.CourseId
                    select a).ToList();
                assessmentListView.ItemsSource = assessments;
                AddAssessmentMenuItem.IsEnabled = assessments.Count < 2;
            }

        }

        private void AddAssessmentMenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAssessment(assessment.CourseId));
            RefreshAssessmentList();
        }

        private void EditButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAssessment((sender as Button).CommandParameter as Assessment));
            RefreshAssessmentList();
        }

        private void DeleteButton_OnClicked(object sender, EventArgs e)
        { 
            assessment = (sender as Button).CommandParameter as Assessment;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Assessment>();
                conn.Delete<Assessment>(((sender as Button).CommandParameter as Assessment).Id);
            }
            RefreshAssessmentList();
        }
    }
}