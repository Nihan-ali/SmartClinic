using System.Windows;
using System.Windows.Controls;

namespace SmartClinic.View.UserControls
{
    public partial class searchMenu : UserControl
    {
        public searchMenu()
        {
            InitializeComponent();

            // Set the placeholder text and opacity initially
            searchTextBox.Text = "search here";
            searchTextBox.Opacity = 0.5;
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the placeholder text when the TextBox gets focus
            if (searchTextBox.Text == "search here")
            {
                searchTextBox.Text = "";
                searchTextBox.Opacity = 1.0;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Restore the placeholder text when the TextBox loses focus and is empty
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "search here";
                searchTextBox.Opacity = 0.5;
            }
        }
    }
}


