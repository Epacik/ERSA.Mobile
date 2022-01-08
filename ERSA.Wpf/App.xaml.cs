using ERSA.Mobile.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ERSA.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private async  void Application_Startup(object sender, StartupEventArgs e)
        {
            var key = Environment.GetEnvironmentVariable("ERSA_API_KEY", EnvironmentVariableTarget.User);

            var client = new Mobile.AdminApi.Client(key);
            
            if (string.IsNullOrWhiteSpace(key))
            {
                key = AskForKey();
            }

            while(!await client.TestConnectionAsync())
            {
                key = AskForKey();
                if (string.IsNullOrWhiteSpace(key))
                {
                    Environment.Exit(0);
                    return;
                }
            }

            ApiKey = key;

            MainWindow = new MainWindow();
            MainWindow.Show();
        }
        
        public string? ApiKey { get; private set; }
        private string? AskForKey()
        {
            var tokenPrompt = new TokenPrompt();
            tokenPrompt.ShowDialog();
            if (string.IsNullOrWhiteSpace(tokenPrompt.Result))
            {
                return null;
            }

            return tokenPrompt.Result;
        }
    }
}
