using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ERSA.Mobile.Views.Models
{
    internal class LinkListViewModel : ViewModelBase
    {
        private ObservableCollection<Link> links = new ObservableCollection<Link>();
        public ObservableCollection<Link> Links { get => links; set => SetProperty(ref links, value); }

        private Link? selectedLink;

        public Link? SelectedLink { get => selectedLink; set => SetProperty(ref selectedLink, value); }
    }
}
