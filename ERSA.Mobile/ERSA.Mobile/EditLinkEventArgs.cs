using ERSA.Mobile.AdminApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile
{
    public class EditLinkEventArgs : EventArgs
    {
        public readonly AdminApi.LinkWithOpengraph LinkToEdit;

        public EditLinkEventArgs(LinkWithOpengraph linkToEdit)
        {
            LinkToEdit = linkToEdit;
        }
    }
}
