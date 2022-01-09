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

namespace ERSA.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            ShowLinksList();
        }

        private void Links_AddNewLink(object sender, EventArgs e)
        {
            ShowAddNew();
        }

        private async void AddLink_AddingCancelled(object sender, EventArgs e)
        {
            await FinishAddingAndReload();
        }

        private async void AddLink_AddingCompleted(object sender, EventArgs e)
        {
            await FinishAddingAndReload();
        }

        private async Task FinishAddingAndReload()
        {
            Dispatcher.Invoke(() =>
            {
                ShowLinksList();
            });
            await Links.Reload();
        }

        private void Links_EditLink(object sender, Views.EdidButtonEventArgs e)
        {
            EditLink.SetLink(e.LinkToEdit);
            ShowEditLink();
        }

        private async void EditLink_Saved(object sender, EventArgs e)
        {
            await FinishAddingAndReload();
        }

        private async void EditLink_Cancelled(object sender, EventArgs e)
        {
            await FinishAddingAndReload();
        }


        internal void ShowLinksList()
        {
            Links.Visibility = Visibility.Visible;
            AddLink.Visibility = Visibility.Collapsed;
            EditLink.Visibility = Visibility.Collapsed;
        }

        internal void ShowAddNew()
        {
            Links.Visibility = Visibility.Collapsed;
            AddLink.Visibility = Visibility.Visible;
            EditLink.Visibility = Visibility.Collapsed;
        }

        internal void ShowEditLink()
        {
            Links.Visibility = Visibility.Collapsed;
            AddLink.Visibility = Visibility.Collapsed;
            EditLink.Visibility = Visibility.Visible;
        }
    }
}
