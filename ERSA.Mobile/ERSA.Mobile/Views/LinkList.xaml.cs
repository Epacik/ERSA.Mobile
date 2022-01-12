using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Views
{
    public partial class LinkList : ContentView
    {
        Models.LinkListViewModel viewModel = new Models.LinkListViewModel();

        public event EventHandler AddNew;
        public event EventHandler Delete;
        public event EventHandler<EditLinkEventArgs> Edit;
        public LinkList()
        {
            BindingContext = viewModel;
            InitializeComponent();
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

        private void PerformSearch()
        {
            if (preparing)
                return;

            var searchString = SearchInput.Text;

            new Task(async () =>
            {
                var api = new AdminApi.Client();
                var data = await api.ListLinksAsync(searchString ?? "").ConfigureAwait(false);
                if (data != null)
                    Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.Links.Clear();
                        //Items.Children.Clear();
                        foreach (Link link in data.OrderBy(l => l.Path))
                        {
                            viewModel.Links.Add(link);
                            //var item = new Definitions.Controls.LinkListItem(link);
                            //item.Clicked += Item_Clicked;
                            //Items.Children.Add(item);
                        }
                    });
            }).Start();
        }

        bool preparing = true;
        internal void Prepared()
        {
            preparing = false;
            PerformSearch();
        }

        private void SearchInput_Unfocused(object sender, FocusEventArgs e)
        {
            PerformSearch();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            AddNew?.Invoke(this, EventArgs.Empty);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.SelectedLink is null)
                return;

            var client = new Client();
            var link = await client.GetLinkDataAsync(((Link)viewModel.SelectedLink).Path);
            Edit?.Invoke(this, new EditLinkEventArgs(link));
        }

        private void RemoveButton_Clicked(object sender, EventArgs e)
        {
            Delete?.Invoke(this, EventArgs.Empty);
        }

        internal void Refresh()
        {
            PerformSearch();
        }
    }
}