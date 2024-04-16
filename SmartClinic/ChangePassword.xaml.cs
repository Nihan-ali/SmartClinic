using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        //implement the Submit_Click method
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //check if the password is empty
            if (string.IsNullOrEmpty(NewPassword.Password))
            {
                MessageBox.Show("Please enter a password");
                return;
            }
            //check if the password is less than 6 characters
            if (NewPassword.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters");
                return;
            }
            //check if the password is less than 6 characters
            if (NewPassword.Password != ConfirmPassword.Password)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window != Application.Current.MainWindow)
            //    {
            //        window.Close();
            //    }
            //}
            //update the password
            DatabaseHelper.UpdatePassword(Username.Text,NewPassword.Password);
            DatabaseHelper.SetLoginStatus(Username.Text, 1);
            MessageBox.Show("Password updated successfully");
            HomeWindow homeWindow = new HomeWindow();
            Application.Current.MainWindow = homeWindow;
            

            homeWindow.Show();
            this.Close();

        }
    }
}
