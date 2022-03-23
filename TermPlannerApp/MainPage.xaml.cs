using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermPlannerApp.Models;
using SQLite;
using Xamarin.Forms;

namespace TermPlannerApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NewTermItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTermPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh_TermsListView();
        }

        private void Refresh_TermsListView()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                var terms = conn.Table<Term>().ToList();
                termsListView.ItemsSource = terms;
            }
        }

        private void TermsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var term = e.SelectedItem as Term;
            Navigation.PushAsync(new TermDetail(term.Id));
        }

        private void Delete_OnClicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Term>();
                var term = (sender as Button).CommandParameter as Term;
                conn.Delete<Term>(term.Id);
                Refresh_TermsListView();
            }

        }


        private void EditButton_OnClicked(object sender, EventArgs e)
        {
            var term = (sender as Button).CommandParameter as Term;
            Navigation.PushAsync(new TermDetail(term.Id));
        }
    }
}
