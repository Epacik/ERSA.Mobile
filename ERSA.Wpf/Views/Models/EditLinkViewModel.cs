using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERSA.Wpf.Views.Models
{
    internal class EditLinkViewModel : ViewModelBase
    {

        public void Clear()
        {
            LinkPath = "";
            Tags.Clear();
            LinkTarget = "";
            LinkHidden = false;
        }

        private string linkPath = "";

        public string LinkPath { get => linkPath; set => SetProperty(ref linkPath, value); }



        private List<OpengraphTag> tags = new List<OpengraphTag>();

        public List<OpengraphTag> Tags { get => tags; set => SetProperty(ref tags, value); }

        private bool? linkHidden;

        public bool? LinkHidden { get => linkHidden; set => SetProperty(ref linkHidden, value); }

        private string linkTarget = "";

        public string LinkTarget { get => linkTarget; set => SetProperty(ref linkTarget, value); }
        public int? LinkId { get; internal set; }

        private OpengraphTag? selectedTag;

        public OpengraphTag? SelectedTag { get => selectedTag; set => SetProperty(ref selectedTag, value); }

        private string? editTag;

        public string? EditTag { get => editTag; set => SetProperty(ref editTag, value); }

        private string? editContent;

        public string? EditContent { get => editContent; set => SetProperty(ref editContent, value); }

        private int selectedIndex;

        public int SelectedIndex { get => selectedIndex; set => SetProperty(ref selectedIndex, value); }
        public List<OpengraphTag> TagsToRemove { get; internal set; } = new List<OpengraphTag>();
    }
}
