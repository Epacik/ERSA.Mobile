using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERSA.Wpf.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditLink.xaml
    /// </summary>
    public partial class EditLink : UserControl
    {
        Models.EditLinkViewModel viewModel = new Models.EditLinkViewModel();
        public EditLink()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public event EventHandler Cancelled;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            viewModel.Clear();
        }

        public event EventHandler Saved;

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Client(((App)Application.Current).ApiKey);
            var response = await client.UpdateLinkAsync(new Link(viewModel.LinkId, viewModel.LinkPath, viewModel.LinkTarget, viewModel.LinkHidden ?? false)).ConfigureAwait(false);

            foreach(var tag in viewModel.TagsToRemove.Where(t => (t.ID ?? -1) != -1))
            {
                response = await client.RemoveOpenGraphTagAsync(tag.ID ?? -1).ConfigureAwait(false);
            }

            foreach(var tag in viewModel.Tags.Where(t => (t.ID ?? -1) == -1)){
                response = await client.AddOpenGraphTagAsync(new OpengraphTagToAdd(tag.LinkID, tag.Tag, tag.Content)).ConfigureAwait(false);
            }

            foreach (var tag in viewModel.Tags.Where(t => (t.ID ?? -1) != -1))
            {
                response = await client.UpdateOpengraphTagAsync(tag).ConfigureAwait(false);
            }


            Saved?.Invoke(this, EventArgs.Empty);
            viewModel.Clear();
        }

        internal void SetLink(LinkWithOpengraph linkToEdit)
        {
            Dispatcher.Invoke(() =>
            {
                viewModel.LinkId = linkToEdit.Id;
                viewModel.LinkPath = linkToEdit.Path;
                viewModel.LinkTarget = linkToEdit.Target;
                viewModel.LinkHidden = linkToEdit.HideTarget;
                viewModel.TagsToRemove.Clear();
                viewModel.Tags.Clear();
                foreach(var tag in linkToEdit.OpengraphTags)
                {
                    viewModel.Tags.Add(tag);
                }
            });
            //throw new NotImplementedException();
        }


        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Tags.Add(new OpengraphTag(-1, viewModel.LinkId ?? throw new NullReferenceException(), "tag", "wartość"));
            Tags.Items.Refresh();
        }

        private void RemoveTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedTag is null || MessageBox.Show("Na pewno?", "Na pewno?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            viewModel.Tags.Remove((OpengraphTag)viewModel.SelectedTag);
            viewModel.TagsToRemove.Add((OpengraphTag)viewModel.SelectedTag);
            Tags.Items.Refresh();
        }

        private void EditTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedTag is null)
                return;

            var tag = (OpengraphTag)viewModel.SelectedTag;

            viewModel.EditTag = tag.Tag;
            viewModel.EditContent = tag.Content;

            EditModal.Visibility = Visibility.Visible;

            Tags.Items.Refresh();
        }

        private void EditCancelButton_Click(object sender, RoutedEventArgs e)
        {
            EditModal.Visibility = Visibility.Collapsed;
            Tags.Items.Refresh();
        }

        private void EditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedTag is null)
                return;

            var index = viewModel.SelectedIndex;
            var tag = (OpengraphTag)viewModel.SelectedTag;
            tag.Tag = viewModel.EditTag;
            tag.Content = viewModel.EditContent;
            viewModel.Tags[index] = tag;


            EditModal.Visibility = Visibility.Collapsed;
            Tags.Items.Refresh();
        }
    }
}
