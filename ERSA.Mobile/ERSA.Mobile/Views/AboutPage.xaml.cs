using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ((App)Application.Current).AddHardwareBackButtonFunc(async () =>
            {
                await Shell.Current.GoToAsync("///Links/MainPage");
            });
            base.OnAppearing();
        }
    }
}