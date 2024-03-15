using System;
using System.Diagnostics;
using System.Windows;

namespace SmartClinic.View.UserControls
{
    public partial class Developer : Window
    {
        public Developer()
        {
            InitializeComponent();
        }

       /* private void DeveloperButton1_Click(object sender, RoutedEventArgs e)
        {
            // Open the LinkedIn page for Md. Nihan Ali
            OpenLinkedInPage("https://www.linkedin.com/in/md-nihan-ali-7476732b0/");
        }

        private void DeveloperButton2_Click(object sender, RoutedEventArgs e)
        {
            // Open the LinkedIn page for Shohanurzaman
            OpenLinkedInPage("https://www.linkedin.com/in/shohanuzzaman-shishir-ba55892b5/");
        }

        private void DeveloperButton3_Click(object sender, RoutedEventArgs e)
        {
            // Open the LinkedIn page for Mostahid Hasan Fahim
            OpenLinkedInPage("https://www.linkedin.com/in/mostahid-fahim-55873720b/");
        }

        private void OpenLinkedInPage(string url)
        {
            try
            {
                // Open the specified URL in the default web browser
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the LinkedIn page: " + ex.Message);
            }
        }*/
    }
}
