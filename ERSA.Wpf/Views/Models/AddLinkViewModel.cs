using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSA.Wpf.Views.Models
{
    internal class AddLinkViewModel :ViewModelBase
    {

        private string? linkPath;

        public string? LinkPath { get => linkPath; set => SetProperty(ref linkPath, value); }

        private string? linkTarget;

        public string? LinkTarget { get => linkTarget; set => SetProperty(ref linkTarget, value); }

        private bool? linkHidden = false;

        public bool? LinkHidden { get => linkHidden; set => SetProperty(ref linkHidden, value); }

        private ObservableCollection<OpengraphTag> tags = new ObservableCollection<OpengraphTag>();

        public ObservableCollection<OpengraphTag> Tags { get => tags; set => SetProperty(ref tags, value); }

        internal void Clear()
        {
            LinkPath = null; 
            LinkTarget = null;
            LinkHidden = false;
            Tags.Clear();
        }
    }
}
