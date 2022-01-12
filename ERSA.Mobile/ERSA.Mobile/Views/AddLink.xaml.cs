using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Views
{
    public partial class AddLink : ContentView
    {
        readonly Models.AddLinkViewModel viewModel = new Models.AddLinkViewModel();
        public AddLink()
        {
            BindingContext = viewModel;
            InitializeComponent();
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

            var client = new AdminApi.Client();

            var response = await client.AddLinkAsync(new AdminApi.LinkToAdd(viewModel.LinkPath, viewModel.LinkTarget, viewModel.LinkHide)).ConfigureAwait(false);

            if (!response.Success)
            {
                ErrorOccured?.Invoke(this, new ErrorOccuredEventArgs(response.Message));
                return;
            }

            Saved?.Invoke(this, EventArgs.Empty);
        }

        internal void Clear()
        {
            viewModel.LinkTarget = "";
            viewModel.LinkPath = "";
            viewModel.LinkHide = false;
        }
    }
}