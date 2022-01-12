using ERSA.Mobile.Views;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERSA.Mobile
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();//new AppShell();//new NavigationPage(new MainPage());
            //((AppShell)MainPage).Navigating += App_Navigating;
            //((AppShell)MainPage).
        }

        private void App_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            //e.Cancel();
            //throw new NotImplementedException();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void CleanUp()
        {
            base.CleanUp();

            Log.CloseAndFlush();
        }

        

        private Stack<Func<Task>> HardwareBackButtonFuncs = new Stack<Func<Task>>();

        public void AddHardwareBackButtonFunc(Func<Task> func)
        {
            HardwareBackButtonFuncs.Push(func);
        }
        public void RemoveHardwareLastBackButtonFunc()
        {
            HardwareBackButtonFuncs.Pop();
        }

        public async void OnHardwareBackButtonPressed()
        {
            if(HardwareBackButtonFuncs.Count == 0)
            {
                return;
            }

            await HardwareBackButtonFuncs.Pop()().ConfigureAwait(false);
        }
    }
}
