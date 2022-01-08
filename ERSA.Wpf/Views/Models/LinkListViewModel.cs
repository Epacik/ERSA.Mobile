using ERSA.Mobile.AdminApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSA.Wpf.Views.Models
{
    internal class LinkListViewModel : ViewModelBase
    {

        private ObservableCollection<Link> links = new ObservableCollection<Link>();

        public ObservableCollection<Link> Links { get => links; set => SetProperty(ref links, value); }

        private string searchString;

        public string SearchString { get => searchString; set => SetProperty(ref searchString, value); }

        private Link? selectedLink;

        public Link? SelectedLink { get => selectedLink; set => SetProperty(ref selectedLink, value); }
    }
}
