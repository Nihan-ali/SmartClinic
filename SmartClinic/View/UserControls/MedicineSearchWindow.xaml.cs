using SmartClinic.View.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
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
            medicinegroupscrollviewer.Visibility = Visibility.Hidden;
            medicinescrollviewer.Visibility = Visibility.Visible;
        }

        private void InitializeMedicineListView()
        {
            List<Medicine> initialMedicines = DatabaseHelper.GetInitialMedicines();
            searchedMedicines.Clear();
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
            searchedMedicineGroups.Clear();
            foreach (MedicineGroup medicineGroup in initialMedicineGroups)
            {
                searchedMedicineGroups.Add(medicineGroup);
            }
            medicineGroupItemsControl.ItemsSource = null;
            medicineGroupItemsControl.ItemsSource = searchedMedicineGroups;
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
                // Retrieve the associated Medicine object from DataContext
                if (toggleButton.DataContext is Medicine selectedMedicine)
                {
                    // Toggle the IsSelected property
                    selectedMedicine.IsSelected = toggleButton.IsChecked == true;

                    // Update the selected medicines collection
                    if (selectedMedicine.IsSelected)
                    {
                        if (!selectedMedicines.Contains(selectedMedicine))
                        {
                            selectedMedicines.Add(selectedMedicine);
                        }
                    }
                    else
                    {
                        selectedMedicines.Remove(selectedMedicine);
                    }

                    // Update the selected medicines ListView
                    UpdateSelectedMedicinesListView();
                }
            }
        }



        private void MedicineButton_Click(object sender, RoutedEventArgs e)
        {
            medicinerx.Background = Brushes.LightBlue;
            medicineGroup.Background = Brushes.White;
            medicinegroupscrollviewer.Visibility = Visibility.Hidden;
            medicinescrollviewer.Visibility = Visibility.Visible;
            InitializeMedicineListView();
        }

        private void MedicineGroupButton_Click(object sender, RoutedEventArgs e)
        {
            medicineGroup.Background = Brushes.LightBlue;
            medicinerx.Background = Brushes.White;
            medicinegroupscrollviewer.Visibility = Visibility.Visible;
            medicinescrollviewer.Visibility = Visibility.Hidden;
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

                //// In your main window code
                searchResultsPopup.IsOpen = false;
                //DetailsWindow detailsWindow = new DetailsWindow(selectedMedicine);
                //detailsWindow.ParentMainWindow = this;
                //detailsWindow.Show();
                selectedMedicines.Add(selectedMedicine);

                // Update the selectedMedicinesListView
                UpdateSelectedMedicinesListView();
            }
        }
        private void selectedMedicinesListView_Loaded(object sender, RoutedEventArgs e)
        {
            // You may not need to manually set AlternationIndex in the code-behind.
            // The AlternationCount="{Binding Path=Items.Count, RelativeSource={RelativeSource Self}}"
            // in XAML should automatically assign serial numbers based on the item index.
        }


        private void IncrementMorningButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This is runnig");
            IncrementTextBoxValue("MorningDose");
        }

        private void DecrementMorningButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue("MorningDose");
        }

        // Similarly for other buttons...

        private void IncrementTextBoxValue(string propertyName)
        {
            MessageBox.Show("comes here but not going into the if block");
            if (selectedMedicinesListView.SelectedItem is Medicine selectedItem)
            {
                // Use reflection to get and set the property value
                MessageBox.Show("Coming here ");
                PropertyInfo property = typeof(Medicine).GetProperty(propertyName);
                if (property != null && property.PropertyType == typeof(int))
                {
                    int value = (int)property.GetValue(selectedItem);
                    value++;
                    property.SetValue(selectedItem, value);

                    // Manually update the binding source
                    selectedMedicinesListView.GetBindingExpression(ListView.ItemsSourceProperty)?.UpdateTarget();
                }
            }
        }

        private void DecrementTextBoxValue(string propertyName)
        {
            if (selectedMedicinesListView.SelectedItem is Medicine selectedItem)
            {
                PropertyInfo property = typeof(Medicine).GetProperty(propertyName);
                if (property != null && property.PropertyType == typeof(int))
                {
                    int value = (int)property.GetValue(selectedItem);
                    if (value > 1)
                    {
                        value--;
                        property.SetValue(selectedItem, value);
                    }
                }
            }
        }


        private void IncrementNoonButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue("NoonDose");
        }

        private void DecrementNoonButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue("NoonDose");
        }

        private void IncrementNightButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue("NightDose");
        }

        private void DecrementNightButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue("NightDose");
        }

        private void IncrementDurationButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementTextBoxValue("Duration");
        }

        private void DecrementDurationButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementTextBoxValue("Duration");
        }

        private void IncrementTextBoxValue(TextBox textBox)
        {
            int value;
            if (int.TryParse(textBox.Text, out value))
            {
                value++;
                textBox.Text = value.ToString();
            }
        }

        private void DecrementTextBoxValue(TextBox textBox)
        {
            int value;
            if (int.TryParse(textBox.Text, out value) && value > 1)
            {
                value--;
                textBox.Text = value.ToString();
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

                AddToSelectedMedicines(selectedMedicine);
                this.Close();
             }
             else
             {
                this.Close();
             }
        }

        private void CreateMedicineGroup_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicines != null)
            {
                MedicineGroupNamePopup.IsOpen = true;
                //DatabaseHelper.AddMedicineGroup(selectedMedicines);
                //this.Close();
            }
            else
            {
                //this.Close();
            }

        }

        
        private void GroupNameEntered(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                MedicineGroupNamePopup.IsOpen = false;
                //return GroupNameBox.Text;
            }
            //return "lol";
        }

        private void DeleteMedicineFromListview_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicinesListView.SelectedItem is Medicine selectedItem)
            {
                selectedMedicines.Remove(selectedItem);
                UpdateSelectedMedicinesListView();
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