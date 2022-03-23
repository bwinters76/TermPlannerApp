using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TermPlannerApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetail : ContentPage
    {
        private int _termId;
        public TermDetail(int termId)
        {
            InitializeComponent();
            _termId = termId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCourseListView();

        }

        private void RefreshCourseListView()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Course>();
                var courses = (from c in conn.Table<Course>()
                    where c.TermId == _termId
                    select c).ToList();
                courseListView.ItemsSource = courses;
                AddCourseItem.IsEnabled = courses.Count <= 5;
            }
        }

        private void AddCourseItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCourse(_termId));
        }

        private void Delete_OnClicked(object sender, EventArgs e)
        {
            var course = (sender as Button).CommandParameter as Course;
            using (SQLiteConnection conn  = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Course>();
                conn.Delete<Course>(course.Id);
            }
            RefreshCourseListView();
        }

        private async void Share_OnClicked(object sender, EventArgs e)
        {
            var course = (sender as Button).CommandParameter as Course;
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = course.Notes,
                Title = "Sharing course notes:"
            });
        }
 

        private void Edit_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCourse((sender as Button).CommandParameter as Course));
        }

        private void AssessmentButton_OnClicked(object sender, EventArgs e)
        {
            var course = (sender as Button).CommandParameter as Course;
            Navigation.PushAsync(new AssessmentsPage(course));
        }
    }
}