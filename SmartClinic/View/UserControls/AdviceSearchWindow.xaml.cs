using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using static SmartClinic.DatabaseHelper;

namespace SmartClinic.View.UserControls
{
    public partial class AdviceSearchWindow : Window
    {
        private List<Advice> initialAdvices;
        private ObservableCollection<Advice> displayedAdvices;
        private ObservableCollection<Advice> selectedAdvices;

        public ObservableCollection<Advice> SelectedAdvices => selectedAdvices;

        public AdviceSearchWindow()
        {
            InitializeComponent();

            // Load initial advices
            initialAdvices = DatabaseHelper.GetInitialAdvices();
            displayedAdvices = new ObservableCollection<Advice>(initialAdvices);
            selectedAdvices = new ObservableCollection<Advice>();

            UpdateAdviceItems();
        }

        private void UpdateAdviceItems()
        {
            adviceItemsControl.ItemsSource = displayedAdvices;
        }

        private void SearchAdvices(string keyword)
        {
            displayedAdvices = new ObservableCollection<Advice>(
                initialAdvices
                .Where(advice => advice.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(advice => advice.Occurrence)
                .Take(20)
                .ToList());

            UpdateAdviceItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAdvices(searchTextBox.Text);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Advice selectedAdvice = toggleButton.DataContext as Advice;

            if (toggleButton.IsChecked == true)
            {
                // Set the DisplayIndex for the selected advice
                selectedAdvice.DisplayIndex = selectedAdvices.Count + 1;

                // Add the selected advice to the selectedAdvices collection
                selectedAdvices.Add(selectedAdvice);
            }
            else
            {
                // Remove the advice if unchecked
                selectedAdvices.Remove(selectedAdvice);
            }

            UpdateSelectedAdvicesListView();
        }

        private void UpdateSelectedAdvicesListView()
        {
            // Set the ItemsSource to the new collection
            selectedAdvicesListView.ItemsSource = null;  // Set it to null first
            selectedAdvicesListView.ItemsSource = selectedAdvices;
        }


        public void AddToSelectedAdvices(Advice newAdvice)
        {
            // Add the new advice to the selected advices collection
            selectedAdvices.Add(newAdvice);

            // Update the selected advices ListView
            UpdateSelectedAdvicesListView();
        }

        private void addToRx_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected advice from the selectedAdvicesListView
            Advice selectedAdvice = (Advice)selectedAdvicesListView.SelectedItem;

            // Check if an advice is selected
            if (selectedAdvice != null)
            {
                // Add the selected advice to the ObservableCollection
                selectedAdvices.Add(selectedAdvice);

                // Update the ListView directly from the collection
                UpdateSelectedAdvicesListView();

                // Create an instance of the medicine UserControl
                medicine medicineUserControl = new medicine();

                // Add the selected advice to the list in the medicine UserControl
                medicineUserControl.AddToSelectedAdvices(selectedAdvice);

                // Close the AdviceSearchWindow
                this.Close();
            }
            else
            {
                // Optionally, display a message or take any other actions if no advice is selected
                this.Close();
            }
        }
        




        private void selectedMedicinesListView_Loaded(object sender, RoutedEventArgs e)
        {
            // You may not need to manually set AlternationIndex in the code-behind.
            // The AlternationCount="{Binding Path=Items.Count, RelativeSource={RelativeSource Self}}"
            // in XAML should automatically assign serial numbers based on the item index.
        }
    }
}
