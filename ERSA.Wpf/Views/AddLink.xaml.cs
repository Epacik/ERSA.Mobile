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
    /// Logika interakcji dla klasy AddLink.xaml
    /// </summary>
    public partial class AddLink : UserControl
    {
        Models.AddLinkViewModel viewModel = new Models.AddLinkViewModel();
        public AddLink()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public event EventHandler AddingCancelled;
        public event EventHandler AddingCompleted;
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Czy na pewno chcesz anulować?", "Anuluj", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AddingCancelled?.Invoke(this, EventArgs.Empty);
                viewModel.Clear();
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(viewModel.LinkPath) || string.IsNullOrWhiteSpace(viewModel.LinkTarget))
            {
                return;
            }
            var client = new Mobile.AdminApi.Client(((App)Application.Current).ApiKey);
            var response = await client.AddLinkAsync(new Mobile.AdminApi.LinkToAdd(viewModel.LinkPath, viewModel.LinkTarget, viewModel.LinkHidden == true)).ConfigureAwait(false);

            if (!response.Success)
            {
                MessageBox.Show(response.Message, "Błąd dodawania linku");
                return;
            }

            AddingCompleted?.Invoke(this, EventArgs.Empty);
            viewModel.Clear();
        }
    }
}
