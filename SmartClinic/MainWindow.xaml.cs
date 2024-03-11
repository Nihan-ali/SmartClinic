using SmartClinic.View.UserControls;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartClinic
{

    public partial class MainWindow : Window
    {
        public string TodayDate { get; set; } 
        public MainWindow()
        {

            InitializeComponent();
            DatabaseHelper init = new DatabaseHelper();
            Password.GotFocus += Password_GotFocus;
            Password.LostFocus += Password_LostFocus;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //check if the username and password are correct from database
            bool isauthenticated = DatabaseHelper.CheckLogin(Username.Text, Password.Password);
            if (isauthenticated)
            {
                // Create an instance of HomeWindow
                HomeWindow homeWindow = new HomeWindow();

                // Set the HomeWindow as the main window
                Application.Current.MainWindow = homeWindow;

                // Show the HomeWindow
                homeWindow.Show();

                // Close the current window
                this.Close();
            }
            else
            {
                //error textblock will be visible
                Error.Visibility = Visibility.Visible;
                MessageBox.Show("Invalid Username or Password");
            }

        }
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Owner = this;
            forgotPasswordWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            forgotPasswordWindow.ShowDialog();
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Visibility = Visibility.Collapsed;
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Password.Password))
            {
                PasswordWatermark.Visibility = Visibility.Visible;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var watermark = (TextBlock)textBox.Template.FindName("Watermark", textBox);

            if (watermark != null)
            {
                watermark.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var watermark = (TextBlock)textBox.Template.FindName("Watermark", textBox);

            if (watermark != null && string.IsNullOrEmpty(textBox.Text))
            {
                watermark.Visibility = Visibility.Visible;
            }
        }


    }
}