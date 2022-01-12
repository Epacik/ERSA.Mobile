using ERSA.Mobile.AdminApi;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ERSA.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        Models.MainPageViewModel viewModel = new Models.MainPageViewModel();
        public MainPage()
        {
            Serilog.Log.Information("Initializing app");

            BindingContext = viewModel;
            InitializeComponent();
            //if(Device.RuntimePlatform == Device.UWP)
            //{
            //    //UwpSearchBarHeader.Height = new GridLength(40);
            //}

            _ = PrepareApiClient();
        }

        private async Task PrepareApiClient()
        {
            if (!Preferences.ContainsKey(Constants.Preferences.ApiToken))
            {
                await AskForNewToken();
            }

            var client = new AdminApi.Client(Preferences.Get(Constants.Preferences.ApiToken, null));
             
            if (!await client.TestConnectionAsync())
            {
                await AskForNewToken();
                client = new AdminApi.Client(Preferences.Get(Constants.Preferences.ApiToken, null));
                if (!await client.TestConnectionAsync())
                    throw new InvalidOperationException();
            }
            LinkList.Prepared();
        }

        private async Task AskForNewToken()
        {
            var result = await DisplayPromptAsync("Podaj token", "Aby połączyć się z API musisz podać nowy token");
            if (string.IsNullOrWhiteSpace(result))
            {
                throw new Exception("Cannot be empty");
            }

            Preferences.Set(Constants.Preferences.ApiToken, result);
        }

        internal void ShowList()
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                AddLink.IsVisible = false;
                EditLink.IsVisible = false;
                LinkList.IsVisible = true;
            });
        }

        internal void ShowAddNew()
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                AddLink.IsVisible = true;
                EditLink.IsVisible = false;
                LinkList.IsVisible = false;
            });
            
        }

        internal void ShowEditLink()
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                AddLink.IsVisible = false;
                EditLink.IsVisible = true;
                LinkList.IsVisible = false;
            });

        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            //PerformSearch();
        }

        private async void Item_Clicked(object sender, EventArgs e)
        {
            var _sender = sender as Definitions.Controls.LinkListItem;
            await Shell.Current.GoToAsync($"///EditLink?link={_sender.Link.Id}");
            //var linkview = new Views.LinkView(_sender.Link);
            //MainGrid.Children.Add(linkview);
            //await DisplayAlert(_sender.Link.Path, $"Id: {_sender.Link.Id}\nPath: {_sender.Link.Path}\nTarget: {_sender.Link.Target}\nHide: {_sender.Link.HideTarget == 1}", "Close");
        }

        private void LinkList_AddNew(object sender, EventArgs e)
        {
            AddLink.Clear();
            ShowAddNew();

        }

        private void LinkList_Edit(object sender, EditLinkEventArgs e)
        {
            EditLink.SetLink(e.LinkToEdit);
            ShowEditLink();
        }

        private async void LinkList_Delete(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Usuń", "Czy na pewno chcesz usunąć?", "Usuń", "Anuluj");
        }

        private void AddLink_Saved(object sender, EventArgs e)
        {
            ShowList();
            LinkList.Refresh();
        }

        private void AddLink_Cancelled(object sender, EventArgs e)
        {
            ShowList();
            LinkList.Refresh();
        }

        private async void AddLink_ErrorOccured(object sender, ErrorOccuredEventArgs e)
        {
            await DisplayAlert("Błąd dodawania linku", e.ErrorMessage, "ok");
        }

        
    }
}
