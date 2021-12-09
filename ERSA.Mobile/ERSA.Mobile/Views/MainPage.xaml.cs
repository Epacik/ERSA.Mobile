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
        public MainPage()
        {
            Serilog.Log.Information("Initializing app");
            InitializeComponent();
            if(Device.RuntimePlatform == Device.UWP)
            {
                UwpSearchBarHeader.Height = new GridLength(40);
                
            }

            _ = PrepareApiClient();
        }

        bool preparingApi = true;
        private async Task PrepareApiClient()
        {
            if (!Preferences.ContainsKey(Constants.Preferences.ApiToken))
            {
                await AskForNewToken();
            }

            var api = new AdminApi.Client(Preferences.Get(Constants.Preferences.ApiToken, null));
             
            if (!await api.TestConnectionAsync())
            {
                await AskForNewToken();
                api = new AdminApi.Client(Preferences.Get(Constants.Preferences.ApiToken, null));
                if (!await api.TestConnectionAsync())
                    throw new InvalidOperationException();
            }
            preparingApi = false;
            PerformSearch();
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

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            //PerformSearch();
        }

        private void PerformSearch()
        {
            if (preparingApi)
                return;

            var searchString = SearchInput.Text;
            if (Device.RuntimePlatform == Device.UWP)
            {
                searchString = SearchInputForUwp.Text;
            }

            new Task(async () =>
            {
                var api = new AdminApi.Client();
                var data = await api.ListLinksAsync(searchString).ConfigureAwait(false);
                if (data != null)
                    Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        Items.Children.Clear();
                        foreach(Link link in data)
                        {
                            var item = new Definitions.Controls.LinkListItem(link);
                            item.Clicked += Item_Clicked;
                            Items.Children.Add(item);
                        }
                    });
            }).Start();
        }

        private async void Item_Clicked(object sender, EventArgs e)
        {
            var _sender = sender as Definitions.Controls.LinkListItem;
            await Shell.Current.GoToAsync($"///EditLink?link={_sender.Link.Id}");
            //var linkview = new Views.LinkView(_sender.Link);
            //MainGrid.Children.Add(linkview);
            //await DisplayAlert(_sender.Link.Path, $"Id: {_sender.Link.Id}\nPath: {_sender.Link.Path}\nTarget: {_sender.Link.Target}\nHide: {_sender.Link.HideTarget == 1}", "Close");
        }

        private void SearchInput_SearchButtonPressed(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            PerformSearch();
            RefreshView.IsRefreshing = false;
        }
    }
}
