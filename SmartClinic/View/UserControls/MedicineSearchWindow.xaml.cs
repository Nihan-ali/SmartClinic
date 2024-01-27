// MedicineSearchWindow.xaml.cs
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartClinic
{
    public partial class MedicineSearchWindow : Window
    {
        private ObservableCollection<Medicine> medicines;

        public MedicineSearchWindow()
        {
            InitializeComponent();
            medicines = new ObservableCollection<Medicine>();
            InitializeListView();
        }

        private void InitializeListView()
        {
            // Fetch and display the initial 15 medicines
            List<Medicine> initialMedicines = DatabaseHelper.GetInitialMedicines();
            foreach (Medicine medicine in initialMedicines)
            {
                medicines.Add(medicine);
            }

            // Set the ObservableCollection as the ListView's ItemsSource
            medicineListView.ItemsSource = medicines;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            DatabaseHelper.MedicineSearchCriteria searchCriteria = (DatabaseHelper.MedicineSearchCriteria)searchComboBox.SelectedIndex;

            medicines.Clear();
            List<Medicine> searchResults = DatabaseHelper.SearchMedicines(searchTerm, searchCriteria);
            foreach (Medicine result in searchResults)
            {
                medicines.Add(result);
            }
        }

        // Event handler for searchTextBox.TextChanged
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            DatabaseHelper.MedicineSearchCriteria searchCriteria = (DatabaseHelper.MedicineSearchCriteria)searchComboBox.SelectedIndex;

            medicines.Clear();
            List<Medicine> searchResults = DatabaseHelper.SearchMedicines(searchTerm, searchCriteria);
            foreach (Medicine result in searchResults)
            {
                medicines.Add(result);
            }
        }


    }
}
