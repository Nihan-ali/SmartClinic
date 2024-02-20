﻿using SmartClinic.View.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace SmartClinic
{
    public partial class MedicineSearchWindow : Window
    {
        private ObservableCollection<Medicine> searchedMedicines;
        private ObservableCollection<Medicine> selectedMedicines;
        public ObservableCollection<Medicine> SelectedMedicines => selectedMedicines;

        public MedicineSearchWindow()
        {
            InitializeComponent();
            //medicineControl.DataContext = App.SelectedItemsViewModel;
            searchedMedicines = new ObservableCollection<Medicine>();
            selectedMedicines = new ObservableCollection<Medicine>();
            //DataContext = new Medicine();
            InitializeMedicineListView();
        }

        private void InitializeMedicineListView()
        {
            // For demonstration purposes, you can replace this with actual data retrieval logic
            List<Medicine> initialMedicines = DatabaseHelper.GetInitialMedicines();
            foreach (Medicine medicine in initialMedicines)
            {
                searchedMedicines.Add(medicine);
            }

            // Set the ObservableCollection as the DataGrid's ItemsSource
            //medicineDataGrid.ItemsSource = searchedMedicines;

            // Set the ObservableCollection as the ItemsControl's ItemsSource
            medicineItemsControl.ItemsSource = searchedMedicines;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            DatabaseHelper.MedicineSearchCriteria searchCriteria = (DatabaseHelper.MedicineSearchCriteria)searchComboBox.SelectedIndex;

            searchedMedicines.Clear();
            List<Medicine> searchResults = DatabaseHelper.SearchMedicines(searchTerm, searchCriteria);
            foreach (Medicine result in searchResults)
            {
                searchedMedicines.Add(result);
            }
        }

        // MedicineSearchWindow.xaml.cs
        private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            DatabaseHelper.MedicineSearchCriteria searchCriteria = (DatabaseHelper.MedicineSearchCriteria)searchComboBox.SelectedIndex;

            // Filter the medicines based on the search term and criteria
            List<Medicine> searchResults = DatabaseHelper.SearchMedicines(searchTerm, searchCriteria);

            // Update the Popup content with the filtered results
            searchResultsListBox.ItemsSource = searchResults;

            // Open or close the Popup based on whether there are search results
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
                            //DetailsWindow detailsWindow = new DetailsWindow(selectedMedicine);
                            //detailsWindow.ParentMainWindow = this;
                            //detailsWindow.Show();
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



        private void MedicineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes, add selected medicine to the selectedMedicines list
            foreach (Medicine selectedItem in e.AddedItems)
            {
                if (!selectedMedicines.Any(m => m.Id == selectedItem.Id))
                {
                    selectedMedicines.Add(selectedItem);
                }
            }

            // Update the selected medicines ListView
            UpdateSelectedMedicinesListView();
        }

        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
        }

        private void MedicineButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MedicineGroupButton_Click(object sender, RoutedEventArgs e)
        {

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
        public void AddToSelectedMedicines(Medicine newMedicine)
        {
            // Add the new medicine to the selectedMedicines collection
            selectedMedicines.Add(newMedicine);

            // Update the selectedMedicinesListView
            UpdateSelectedMedicinesListView();
        }


        private void addToRx_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected medicine from the searchResultsListBox
            Medicine selectedMedicine = (Medicine)searchResultsListBox.SelectedItem;

            // Check if a medicine is selected
            if (selectedMedicine != null)
            {

                // Create an instance of the medicine UserControl
                medicine medicineUserControl = new medicine();

                // Add the selected medicine to the list in the medicine UserControl
                medicineUserControl.AddToSelectedMedicines(selectedMedicine);

                // Close the MedicineSearchWindow
                this.Close();
            }
            else
            {
                this.Close();
                // Optionally, display a message or take any other actions if no medicine is selected
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


        //private void IncrementNoonButton_Click(object sender, RoutedEventArgs e)
        //{
        //    IncrementTextBoxValue(noonTextBox);
        //}

        //private void DecrementNoonButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DecrementTextBoxValue(noonTextBox);
        //}

        //private void IncrementNightButton_Click(object sender, RoutedEventArgs e)
        //{
        //    IncrementTextBoxValue(nightTextBox);
        //}

        //private void DecrementNightButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DecrementTextBoxValue(nightTextBox);
        //}

        //private void IncrementDurationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    IncrementTextBoxValue(durationTextBox);
        //}

        //private void DecrementDurationButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DecrementTextBoxValue(durationTextBox);
        //}

        //private void IncrementTextBoxValue(TextBox textBox)
        //{
        //    int value;
        //    if (int.TryParse(textBox.Text, out value))
        //    {
        //        value++;
        //        textBox.Text = value.ToString();
        //    }
        //}

        //private void DecrementTextBoxValue(TextBox textBox)
        //{
        //    int value;
        //    if (int.TryParse(textBox.Text, out value) && value > 1)
        //    {
        //        value--;
        //        textBox.Text = value.ToString();
        //    }
        //}









        // Add other methods or event handlers as needed
    }

}