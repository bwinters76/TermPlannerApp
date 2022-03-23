using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using SQLite;
using TermPlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssessment : ContentPage
    {
        private Assessment assessment;
        public AddAssessment(int courseId)
        {
            InitializeComponent();
            assessment = new Assessment();
            assessment.CourseId = courseId;
            CheckAssessmentTypeAvailable();

        }

        public AddAssessment(Assessment existingAssessment)
        {
            InitializeComponent();
            assessment = existingAssessment;
            CheckAssessmentTypeAvailable();
            assessmentNameEntry.Text = assessment.Name;
            assessmentTypePicker.SelectedItem = assessment.Type;
            startPicker.Date = assessment.Start;
            endPicker.Date = assessment.End;
        }

        private void CheckAssessmentTypeAvailable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Assessment>();
                var assessments = (from a in conn.Table<Assessment>()
                    where a.CourseId == assessment.CourseId
                    select a).ToList();
                bool addObjectiveAssessment = assessments.Any(a => a.Type == "Objective Assessment");
                if (!addObjectiveAssessment) assessmentTypePicker.Items.Add("Objective Assessment");
                bool addPerformanceAssessment = assessments.Any(a => a.Type == "Performance Assessment");
                if (!addPerformanceAssessment) assessmentTypePicker.Items.Add("Performance Assessment");
            }
        } 


        private void Save_OnClicked(object sender, EventArgs e)
        {
            assessment.Name = assessmentNameEntry.Text;
            assessment.Type = assessmentTypePicker.SelectedItem.ToString();
            assessment.Start = startPicker.Date;
            assessment.End = endPicker.Date;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Assessment>();
                if (assessment.Id != 0)
                {
                    conn.Update(assessment);
                }
                else
                {
                    conn.Insert(assessment);
                }
            }

            if (notificationsEnabledCheckBox.IsChecked)
            {
                int notificationId = int.Parse($"{assessment.CourseId}{assessment.Id}1");
                CrossLocalNotifications.Current.Show(
                    $"{assessment.Name} due soon",
                    $"Assessment begins: {assessment.Start}",
                    notificationId,
                    assessment.Start.AddDays(-1));
                notificationId++;
                CrossLocalNotifications.Current.Show(
                    $"{assessment.Name} ending soon!",
                    $"assessment ends {assessment.End}",
                    notificationId,
                    assessment.End
                );
            }
            else
            {
                try
                {
                    CrossLocalNotifications.Current.Cancel(int.Parse($"{assessment.CourseId}{assessment.Id}1"));
                    CrossLocalNotifications.Current.Cancel(int.Parse($"{assessment.CourseId}{assessment.Id}2"));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            Navigation.PopAsync();
        }
    }
}