using SmartClinic.View.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;


namespace SmartClinic
{
    public partial class MedicineSearchWindow : Window
    {
        private ObservableCollection<Medicine> searchedMedicines;
        private ObservableCollection<Medicine> selectedMedicines;
        private ObservableCollection<MedicineGroup> searchedMedicineGroups;
        private ObservableCollection<MedicineGroup> selectedMedicineGroups;
        public ObservableCollection<Medicine> SelectedMedicines => selectedMedicines;
        public ObservableCollection<MedicineGroup> SelectedMedicineGroups => selectedMedicineGroups;

        

        public MedicineSearchWindow()
        {
            InitializeComponent();
            searchedMedicines = new ObservableCollection<Medicine>();
            selectedMedicines = new ObservableCollection<Medicine>();
            searchedMedicineGroups = new ObservableCollection<MedicineGroup>();
            selectedMedicineGroups = new ObservableCollection<MedicineGroup>();
            InitializeMedicineListView();
        }

        private void InitializeMedicineListView()
        {
            List<Medicine> initialMedicines = DatabaseHelper.GetInitialMedicines();
            foreach (Medicine medicine in initialMedicines)
            {
                searchedMedicines.Add(medicine);
            }
            medicineItemsControl.ItemsSource = null;
            medicineItemsControl.ItemsSource = searchedMedicines;
        }

        private void InitializeMedicineGroupListView()
        {
            List<MedicineGroup> initialMedicineGroups = DatabaseHelper.GetInitialMedicineGroups();
            foreach (MedicineGroup medicineGroup in initialMedicineGroups)
            {
                searchedMedicineGroups.Add(medicineGroup);
            }
            medicineItemsControl.ItemsSource = null;
            medicineItemsControl.ItemsSource = searchedMedicineGroups;

            // Update the ToggleButton bindings
            UpdateToggleButtonBindings();
        }

        private void UpdateToggleButtonBindings()
        {
            foreach (var item in medicineItemsControl.Items)
            {
                MessageBox.Show(item.ToString());
                var container = medicineItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                var toggleButton = FindVisualChild<ToggleButton>(container);

                if (toggleButton != null)
                {
                    toggleButton.SetBinding(ToggleButton.ContentProperty, new Binding("GroupName") { Source = item });
                }
            }
        }
        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
        }
        private void UpdateSelectedMedicineGroupListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicineGroups;
        }


        // MedicineSearchWindow.xaml.cs
        private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            DatabaseHelper.MedicineSearchCriteria searchCriteria = (DatabaseHelper.MedicineSearchCriteria)searchComboBox.SelectedIndex;

            List<Medicine> searchResults = DatabaseHelper.SearchMedicines(searchTerm, searchCriteria);

            searchResultsListBox.ItemsSource = searchResults;

            if (searchResults.Count > 0)
            {
                searchResultsPopup.IsOpen = true;
            }
            else
            {
                searchResultsPopup.IsOpen = false;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                if (toggleButton.DataContext is Medicine selectedMedicine)
                {
                    selectedMedicine.IsSelected = toggleButton.IsChecked == true;

                    if (selectedMedicine.IsSelected)
                    {
                        if (!selectedMedicines.Contains(selectedMedicine))
                        {
                            DetailsWindow detailsWindow = new DetailsWindow(selectedMedicine);
                            detailsWindow.ParentMainWindow = this;
                            detailsWindow.Show();
                        }
                    }
                    else
                    {
                        selectedMedicines.Remove(selectedMedicine);
                    }

                    UpdateSelectedMedicinesListView();
                }
            }
        }



        private void MedicineButton_Click(object sender, RoutedEventArgs e)
        {
            medicinerx.Background = Brushes.LightBlue;
            medicineGroup.Background = Brushes.White;
            InitializeMedicineListView();
        }

        private void MedicineGroupButton_Click(object sender, RoutedEventArgs e)
        {
            medicineGroup.Background = Brushes.LightBlue;
            medicinerx.Background = Brushes.White;
    
            InitializeMedicineGroupListView();
        }

        // Helper method to find a child element of a specific type in a visual tree
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }
                else
                {
                    var result = FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        private void SearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchResultsListBox.SelectedItem != null)
            {
                Medicine selectedMedicine = (Medicine)searchResultsListBox.SelectedItem;

                // In your main window code
                searchResultsPopup.IsOpen = false;
                DetailsWindow detailsWindow = new DetailsWindow(selectedMedicine);
                detailsWindow.ParentMainWindow = this;
                detailsWindow.Show();
            }
        }
        public void AddToSelectedMedicines(Medicine newMedicine)
        {
            selectedMedicines.Add(newMedicine);
            UpdateSelectedMedicinesListView();
        }

        private void AddToSelectedMedicineGroups(MedicineGroup newMedicineGroup)
        {
            selectedMedicineGroups.Add(newMedicineGroup);
            UpdateSelectedMedicineGroupListView();
        }


        private void addToRx_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)searchResultsListBox.SelectedItem;

             if (selectedMedicine != null)
             {

                medicine medicineUserControl = new medicine();
                medicineUserControl.AddToSelectedMedicines(selectedMedicine);
                this.Close();
             }
             else
             {
                this.Close();
             }
        }



        private void rx_loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
        }
        private void createMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (medicinerx.Background == Brushes.LightBlue)
            {
                this.Close();
                MedicineCreateWindow medicineCreateWindow = new MedicineCreateWindow();
                // Set the window startup location to center screen
                medicineCreateWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                medicineCreateWindow.Show();
            }
            else
            {
                this.Close();
                CreateMedicineGroupWindow createMedicineGroupWindow = new CreateMedicineGroupWindow();
                createMedicineGroupWindow.Show();
            }
        }

    }
}