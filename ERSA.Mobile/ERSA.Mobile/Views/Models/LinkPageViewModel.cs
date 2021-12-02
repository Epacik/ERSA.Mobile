using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.Views.Models
{
    internal class LinkPageViewModel : ViewModelBase
    {

        private int linkID;

        public int LinkID { get => linkID; set => SetProperty(ref linkID, value); }

        private string linkPath;

        public string LinkPath { get => linkPath; set => SetProperty(ref linkPath, value); }

        private string linkTarget;

        public string LinkTarget { get => linkTarget; set => SetProperty(ref linkTarget, value); }

        private bool hideLink;

        public bool HideLink { get => hideLink; set => SetProperty(ref hideLink, value); }

        private IList<Xamarin.Forms.View> openGraphTokens;

        public IList<Xamarin.Forms.View> OpenGraphTokens { get => openGraphTokens; set => SetProperty(ref openGraphTokens, value); }
    }
}
