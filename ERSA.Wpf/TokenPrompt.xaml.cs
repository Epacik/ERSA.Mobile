using System.Windows;

namespace ERSA.Wpf
{
    /// <summary>
    /// Logika interakcji dla klasy TokenPrompt.xaml
    /// </summary>
    public partial class TokenPrompt : Window
    {
        public TokenPrompt()
        {
            InitializeComponent();
        }

        public string? Result { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = TokenBox.Password;
            Close();
        }
    }
}
