using System.Windows;
using System.Windows.Controls;

namespace SmartClinic
{
    public partial class AddPatient : Window
    {
        public AddPatient()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the submit button click event here
            // You can access the entered data and perform any necessary actions
            string name = nameTextBox.Text;
            string age = ageTextBox.Text;
            string phone = phoneTextBox.Text;
            string address = addressTextBox.Text;
            string bloodGroup = (string)((ComboBoxItem)bloodGroupComboBox.SelectedItem).Content;

            // Perform actions with the collected data as needed
        }
    }
}


