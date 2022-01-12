using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.Views.Models
{
    internal class AddLinkViewModel : ViewModelBase
    {

        private string linkPath;

        public string LinkPath { get => linkPath; set => SetProperty(ref linkPath, value); }

        private string linkTarget;

        public string LinkTarget { get => linkTarget; set => SetProperty(ref linkTarget, value); }

        private bool linkHide;

        public bool LinkHide { get => linkHide; set => SetProperty(ref linkHide, value); }

    }
}
