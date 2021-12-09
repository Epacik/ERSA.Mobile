using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(LinkId), "link")]
    public partial class LinkPage : ContentPage
    {

        Models.LinkPageViewModel model = new Models.LinkPageViewModel();
        public LinkPage()
        {
            BindingContext = model;
            InitializeComponent();

            
        }
        private int linkId;
        public int LinkId 
        { 
            get => linkId;
            set
            {
                linkId = value;
                LoadLinkData();
            }
        }

        protected override void OnAppearing()
        {
            ((App)Application.Current).AddHardwareBackButtonFunc(async () =>
            {
                await Shell.Current.GoToAsync("///Links/MainPage");
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //((App)Application.Current).RemoveHardwareLastBackButtonFunc();
        }


        private async void LoadLinkData()
        {
            //throw new NotImplementedException();
            Console.WriteLine(LinkId);
            var api = new AdminApi.Client();

            var linkData = await api.GetLinkDataAsync(LinkId.ToString());

            if(linkData is null)
            {
                //Current_Navigating(null, null);
                GoBackButton_Clicked(null, null);
                return;
            }
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                model.LinkID = (int)linkData.Id;
                model.LinkPath = linkData.Path;
                model.LinkTarget = linkData.Target;
                model.HideLink = linkData.HideTarget;
                
            });
        }

        private async void GoBackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Links/MainPage");
        }
    }
}