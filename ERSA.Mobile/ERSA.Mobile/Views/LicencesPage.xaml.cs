using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LicencesPage : ContentPage
    {
        public LicencesPage()
        {
            InitializeComponent();
            
            foreach(Licenses.License license in Licenses.Licenses.GetAll())
            {
                LicenseList.Children.Add(new Definitions.Controls.LicenseListItem(license));
            }
        }

        protected override void OnAppearing()
        {
            ((App)Application.Current).AddHardwareBackButtonFunc(async () =>
            {
                await Shell.Current.GoToAsync("///Informations/About");
            });
            base.OnAppearing();
        }
    }
}