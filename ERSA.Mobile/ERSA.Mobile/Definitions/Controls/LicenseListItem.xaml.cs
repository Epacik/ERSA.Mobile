using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Definitions.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LicenseListItem : Expander
    {
        public LicenseListItem(Licenses.License license)
        {
            InitializeComponent();
            LicenseHeader.Text = license.library;
            LicenseText.Text = $"{license.link}\n\n{license.license}";
        }
    }
}