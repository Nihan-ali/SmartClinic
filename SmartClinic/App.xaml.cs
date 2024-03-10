using System.Windows;

namespace SmartClinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool condition = DatabaseHelper.CheckStatus("Nihan"); // Change this to your actual condition
            if (condition==false)
            {
                StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            }
            else
            {
                StartupUri = new System.Uri("HomeWindow.xaml", System.UriKind.Relative);
            }
        }
    }
}
