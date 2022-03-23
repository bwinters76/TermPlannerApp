using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using TermPlannerApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermPlannerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourse : ContentPage
    {
        private int _termId;
        private Course course;
        public AddCourse(int termId)
        {
            InitializeComponent();
            SetPickerItems();
            statusPicker.SelectedIndex = 1;
            _termId = termId;
            course = new Course();

        }

        public AddCourse(Course existingCourse)
        {
            InitializeComponent();
            course = existingCourse;
            SetPickerItems();
            statusPicker.SelectedItem = course.Status;
            _termId = course.TermId;
            courseNameEntry.Text = course.Name;
            instructorNameEntry.Text = course.InstructorName;
            instructorEmailEntry.Text = course.InstructorEmail;
            instructorPhoneEntry.Text = course.InstructorPhone;
            startDatePicker.Date = course.Start;
            endDatePicker.Date = course.End;
            notesEntry.Text = course.Notes;
            notificationsCheckBox.IsChecked = course.EnableNotifications;
            

        }

        private void SetPickerItems()
        {
            statusPicker.Items.Add("Registered");
            statusPicker.Items.Add("In Progress");
            statusPicker.Items.Add("Completed");
        }

        private void SaveCourseButton_OnClicked(object sender, EventArgs e)
        {

            course.TermId = _termId;
            course.Name = courseNameEntry.Text;
            course.Start = startDatePicker.Date;
            course.End = endDatePicker.Date;
            course.Status = statusPicker.SelectedItem.ToString();
            course.InstructorName = instructorNameEntry.Text;
            course.InstructorEmail = instructorEmailEntry.Text;
            course.InstructorPhone = instructorPhoneEntry.Text;
            course.Notes = notesEntry.Text;
            course.EnableNotifications = notificationsCheckBox.IsChecked;



            if (course.EnableNotifications)
            {
                int notificationId = int.Parse($"{course.TermId}{course.Id}1");
                CrossLocalNotifications.Current.Show(
                    $"{course.Name} starting", 
                    $"The first day of class is {course.Start}",
                    notificationId,
                    course.Start.AddDays(-1));
                notificationId++;
                CrossLocalNotifications.Current.Show(
                    $"{course.Name} ending soon!",
                    $"Course will end tomorrow {course.End}",
                    notificationId,
                    course.End.AddDays(-1)
                );
            }
            else
            {
                try
                {
                    CrossLocalNotifications.Current.Cancel(int.Parse($"{course.TermId}{course.Id}1"));
                    CrossLocalNotifications.Current.Cancel(int.Parse($"{course.TermId}{course.Id}2"));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Course>();
                if (course.Id != 0)
                {
                    try
                    {
                        var result = conn.Update(course);
                        DisplayAlert("Success", "Course Saved", "ok");
                    }
                    catch (Exception exception)
                    {
                        DisplayAlert("Error", "Failed to update course","ok");
                    }
                }
                else conn.Insert(course);
            }
            Navigation.PopAsync();
        }
    }
}