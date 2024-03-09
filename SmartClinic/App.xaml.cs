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
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("You are already logged in");
                HomeWindow homeWindow = new HomeWindow();
                homeWindow.Show();
            }
        }
    }
}
