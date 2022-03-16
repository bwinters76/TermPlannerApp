using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
