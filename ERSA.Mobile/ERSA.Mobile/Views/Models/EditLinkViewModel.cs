using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ERSA.Mobile.Views.Models
{
    internal class EditLinkViewModel : AddLinkViewModel
    {
        public int? LinkId { get; internal set; }

        private string editTag;

        public string EditTag { get => editTag; set => SetProperty(ref editTag, value); }

        private string editContent;

        public string EditContent { get => editContent; set => SetProperty(ref editContent, value); }

        private ObservableList<AdminApi.OpengraphTag> tags = new ObservableList<AdminApi.OpengraphTag>();

        public ObservableList<AdminApi.OpengraphTag> Tags { get => tags; set => SetProperty(ref tags, value); }

        private AdminApi.OpengraphTag? selectedTag;

        public AdminApi.OpengraphTag? SelectedTag { get => selectedTag; set => SetProperty(ref selectedTag, value); }
    }
}
