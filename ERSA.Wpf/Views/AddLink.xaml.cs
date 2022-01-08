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
        Models.AddLinkViewModel model = new Models.AddLinkViewModel();
        public AddLink()
        {
            DataContext = model;
            InitializeComponent();
        }

        public event EventHandler AddingCancelled;
        public event EventHandler AddingCompleted;
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Czy na pewno chcesz anulować?", "Anuluj", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                AddingCancelled?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(model.LinkPath) || string.IsNullOrWhiteSpace(model.LinkTarget))
            {
                return;
            }
            var client = new Mobile.AdminApi.Client(((App)Application.Current).ApiKey);
            var response = await client.AddLinkAsync(new Mobile.AdminApi.LinkToAdd(model.LinkPath, model.LinkTarget, model.LinkHidden == true)).ConfigureAwait(false);

            if (!response.Success)
            {
                MessageBox.Show(response.Message, "Błąd dodawania linku");
                return;
            }

            AddingCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
