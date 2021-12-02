using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ERSA.Mobile.UWP
{
    public sealed partial class MainPage
    {
        private ERSA.Mobile.App app;
        public MainPage()
        {
            this.InitializeComponent();
            app = new ERSA.Mobile.App();
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            LoadApplication(app);
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            app.OnHardwareBackButtonPressed();
            //throw new NotImplementedException();
        }
    }
}
