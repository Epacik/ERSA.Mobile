using ERSA.Mobile.AdminApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ERSA.Wpf.Views
{
    /// <summary>
    /// Logika interakcji dla klasy LinkList.xaml
    /// </summary>
    public partial class LinkList : UserControl
    {
        private readonly Models.LinkListViewModel model = new();
        public LinkList()
        {
            DataContext = model;
            InitializeComponent();
            var key = ((App)Application.Current).ApiKey;
            client = new Client(key);

            _ = LoadLinks();
        }

        Client client;

        public async Task Reload()
        {
            await LoadLinks(model.SearchString).ConfigureAwait(false);
        }

        private async Task LoadLinks(string? searchString = null)
        {
            var links = await client.ListLinksAsync(searchString ?? "");
            Dispatcher.Invoke(() =>
            {
                model.Links.Clear();
                foreach (Link link in links.OrderBy(l => l.Path))
                {
                    model.Links.Add(link);
                }
            });
        }

        public event EventHandler AddNewLink;

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewLink?.Invoke(this, EventArgs.Empty);
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if( model.SelectedLink is null ||
                model.SelectedLink?.Id is null ||
                MessageBox.Show("Czy na pewno chcesz usunąć ten link?", "Usuń link", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                return;
            }

            var link = (Link)model.SelectedLink;

            await client.RemoveLinkAsync((int)link.Id);

            await LoadLinks(model.SearchString).ConfigureAwait(false);
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
            await LoadLinks(model.SearchString).ConfigureAwait(false);
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadLinks(model.SearchString).ConfigureAwait(false);
        }

        public event EventHandler<EdidButtonEventArgs> EditLink;

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (model.SelectedLink is null ||
                model.SelectedLink?.Id is null)
            {
                return;
            }

            var link = await client.GetLinkDataAsync(model?.SelectedLink?.Id ?? -1);

            EditLink?.Invoke(this, new EdidButtonEventArgs(link));
        }
    }

    public class EdidButtonEventArgs : EventArgs
    {
        public readonly LinkWithOpengraph LinkToEdit;

        public EdidButtonEventArgs(LinkWithOpengraph linkToEdit)
        {
            LinkToEdit = linkToEdit;
        }
    }
}
