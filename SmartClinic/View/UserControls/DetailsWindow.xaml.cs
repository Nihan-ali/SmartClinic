using System.Windows;
using System.Windows.Controls;

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
            detailsTextBlock.Text = displayText.MedicineName;
        }

        private void DetailsOkButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a note based on the selected dosage
            string note = CreateDosageNote();

            // Set the note in the Medicine object
            newMedicine.Note = note;

            // Add the medicine to the selected medicines collection
            ParentMainWindow?.AddToSelectedMedicines(newMedicine);

            // Close the DetailsWindow
            Close();
        }

        private string CreateDosageNote()
        {
            string selectedUnit = (unitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "piece";

            // Create a note based on the selected dosage
            string morningDosage = morningTextBox.Text.Trim() != "0" ? $"সকালে  {morningTextBox.Text}  {selectedUnit}" : "";
            string noonDosage = noonTextBox.Text.Trim() != "0" ? $"দুপুরে  {noonTextBox.Text}   {selectedUnit}" : "";
            string nightDosage = nightTextBox.Text.Trim() != "0" ? $"রাতে  {nightTextBox.Text}   {selectedUnit}" : "";
            string duration = durationTextBox.Text.Trim() != "0" ? $"{durationTextBox.Text} দিন " : "";
            string secheduleText = $"{morningDosage} {noonDosage} {nightDosage}";

            // Check if "After Eating" is selected
            string afterEatingNote = afterEatingCheckBox.IsChecked == true ? " খাবার পরে " : "";
            string beforeEatingNote = beforeEatingCheckBox.IsChecked == true ? " খাবার আগে " : "";

            // Combine all dosages and notes
            string note = $"{(secheduleText != null ? secheduleText + " করে " : "")}{afterEatingNote}{beforeEatingNote}{duration}{detailsTextBox.Text}";


            return note;
        }

        private void IncrementMorningButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue(morningTextBox);
        }

        private void DecrementMorningButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue(morningTextBox);
        }

        private void IncrementNoonButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue(noonTextBox);
        }

        private void DecrementNoonButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue(noonTextBox);
        }

        private void IncrementNightButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue(nightTextBox);
        }

        private void DecrementNightButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue(nightTextBox);
        }
        private void IncrementDurationButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue(durationTextBox);

        }
        private void DecrementDurationButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue(durationTextBox);
        }

        private void IncrementTextBoxValue(TextBox textBox)
        {
            int value;
            if (int.TryParse(textBox.Text, out value))
            {
                textBox.Text = (value + 1).ToString();
            }
        }

        private void DecrementTextBoxValue(TextBox textBox)
        {
            int value;
            if (int.TryParse(textBox.Text, out value) && value > 0)
            {
                textBox.Text = (value - 1).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}