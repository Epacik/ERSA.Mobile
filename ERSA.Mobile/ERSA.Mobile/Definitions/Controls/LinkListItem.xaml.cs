using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile.Definitions.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListItem : ContentView
    {
        public LinkListItem(AdminApiClient.Link link)
        {
            Link = link;
            InitializeComponent();
            Path.Text = link.Path;
            Target.Text = link.Target;
            var tap = new TapGestureRecognizer();
            tap.Command = new Command(OnClicked);
            this.GestureRecognizers.Add(tap);

            var click = new ClickGestureRecognizer();
            click.Command = new Command(OnClicked);
            this.GestureRecognizers.Add(click);
        }
        public AdminApiClient.Link Link { get; }

        public event EventHandler Clicked;

        public void OnClicked(object sender)
        {
            if (Clicked != null)
            {
                // Call the custom event handler (assuming one has been set)
                //
                // You could always subclass EventArgs to send something more useful than
                // EventArgs.Empty here but more often than not that's not necessary
                this.Clicked(this, EventArgs.Empty);
            }
        }

    }
}