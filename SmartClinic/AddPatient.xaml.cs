using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;

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

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(age) && !string.IsNullOrWhiteSpace(phone)
                && !string.IsNullOrWhiteSpace(address) && !string.IsNullOrWhiteSpace(bloodGroup))
            {
                DatabaseHelper.InsertPatientInfo(name, age, phone, address, bloodGroup);

                // Create an instance of your UserControl


                MessageBox.Show("Data submitted successfully!");
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }

            // Perform actions with the collected data as needed
        }
        

    }
}