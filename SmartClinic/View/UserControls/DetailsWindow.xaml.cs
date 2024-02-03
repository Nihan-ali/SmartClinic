using System.Windows;

namespace SmartClinic.View.UserControls
{
    public partial class DetailsWindow : Window
    {
        public MedicineSearchWindow ParentMainWindow { get; set; }
        public Medicine newMedicine;
        public DetailsWindow(Medicine displayText)
        {
            InitializeComponent();
            newMedicine = displayText;
            // Set the display text in the DetailsWindow
            detailsTextBlock.Text = displayText.GenericName;
        }

        private void DetailsOkButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from the DetailsWindow
            string medicineName = detailsTextBlock.Text;
            string additionalText = detailsTextBox.Text;

            // Create a new Medicine object with the selected values
            //Medicine newMedicine = new Medicine;

            // Check if ParentMainWindow is not null before calling the method
            ParentMainWindow?.AddToSelectedMedicines(newMedicine);

            // Close the DetailsWindow
            Close();
        }
    }
}
