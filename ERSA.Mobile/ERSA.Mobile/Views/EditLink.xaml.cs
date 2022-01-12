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
    public partial class EditLink : ContentView
    {
        readonly Models.EditLinkViewModel viewModel = new Models.EditLinkViewModel();
        public EditLink()
        {
            BindingContext = viewModel;
            InitializeComponent();

            switch (Application.Current.RequestedTheme)
            {
                case OSAppTheme.Dark:
                    PopupDialogFrame.Background = new SolidColorBrush(new Color(0.1, 0.1, 0.1));
                    break;
                default:
                    PopupDialogFrame.Background = new SolidColorBrush(new Color(0.9, 0.9, 0.9));
                    break;
            }


        }


        public event EventHandler Cancelled;
        public event EventHandler Saved;
        public event EventHandler<ErrorOccuredEventArgs> ErrorOccured;

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(viewModel.LinkPath) || string.IsNullOrWhiteSpace(viewModel.LinkTarget))
            {
                return;
            }

            var client = new Client();
            var response = await client.UpdateLinkAsync(new Link(viewModel.LinkId, viewModel.LinkPath, viewModel.LinkTarget, viewModel.LinkHide));

            if (!response.Success)
            {
                ErrorOccured?.Invoke(this, new ErrorOccuredEventArgs(response.Message));
                return;
            }

            Saved?.Invoke(this, EventArgs.Empty);
        }

        internal void SetLink(LinkWithOpengraph linkToEdit)
        {
            viewModel.LinkId = linkToEdit.Id;
            viewModel.LinkPath = linkToEdit.Path;
            viewModel.LinkTarget = linkToEdit.Target;
            viewModel.LinkHide = linkToEdit.HideTarget;
            viewModel.Tags.Clear();
            viewModel.Tags.AddRange(linkToEdit.OpengraphTags);
        }

        private void EditCancelButton_Clicked(object sender, EventArgs e)
        {
            PopupDialog.IsVisible = false;
        }

        private void EditSaveButton_Clicked(object sender, EventArgs e)
        {
            PopupDialog.IsVisible = false;

            if (viewModel.SelectedTag is null)
                return;

            var tag = (OpengraphTag)viewModel.SelectedTag;
            var index = viewModel.Tags.IndexOf(tag);

            //tag = viewModel.Tags[index];

            tag.Tag = viewModel.EditTag;
            tag.Content = tag.Content;

            viewModel.Tags[index] = tag;
        }

        private void AddTagButton_Clicked(object sender, EventArgs e)
        {
            viewModel.Tags.Add(new OpengraphTag(null, viewModel.LinkId ?? -1, "tag", "content"));
        }

        private void RemoveTagButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.SelectedTag is null)
                return;

            viewModel.Tags.Remove((OpengraphTag)viewModel.SelectedTag);
        }

        private void EditTagButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.SelectedTag is null)
                return;

            var tag = (OpengraphTag)viewModel.SelectedTag;
            viewModel.EditTag = tag.Tag;
            viewModel.EditContent = tag.Content;

            PopupDialog.IsVisible = false;
        }
    }
}