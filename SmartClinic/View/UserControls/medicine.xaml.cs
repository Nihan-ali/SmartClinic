using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SmartClinic.DatabaseHelper;

namespace SmartClinic.View.UserControls
{
    public partial class medicine : UserControl
    {
        private List<Medicine> selectedMedicines = new List<Medicine>();
        private List<Advice> selectedAdvices = new List<Advice>();

        public List<Medicine> SelectedMedicines => selectedMedicines;
        public List<Advice> SelectedAdvices => selectedAdvices;

        public medicine()
        {
            InitializeComponent();
        }

        public void AddToSelectedMedicines(Medicine newMedicine)
        {
            // Add the new medicine to the selectedMedicines collection
            selectedMedicines.Add(newMedicine);

            // Update the selectedMedicinesListView
            UpdateSelectedMedicinesListView();
        }

        public void AddToSelectedAdvices(Advice newAdvice)
        {
            // Set the DisplayIndex for the new advice
            newAdvice.DisplayIndex = selectedAdvices.Count + 1;

            // Add the new advice to the selectedAdvices collection
            selectedAdvices.Add(newAdvice);

            // Update the selectedAdvicesListView
            UpdateSelectedAdvicesListView();
        }


        private void Rx_Click(object sender, RoutedEventArgs e)
        {
            MedicineSearchWindow searchWindow = new MedicineSearchWindow();
            searchWindow.ShowDialog();

            // Handle the selected medicine from the search window as needed
            foreach (var selectedMedicine in searchWindow.SelectedMedicines)
            {
                AddToSelectedMedicines(selectedMedicine);
            }
        }

        private void Advices_Click(object sender, RoutedEventArgs e)
        {
            AdviceSearchWindow searchWindow = new AdviceSearchWindow();
            searchWindow.ShowDialog();

            // Handle the selected advices from the search window as needed
            foreach (var selectedAdvice in searchWindow.SelectedAdvices)
            {
                AddToSelectedAdvices(selectedAdvice);
            }
        }

        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
        }

        private void UpdateSelectedAdvicesListView()
        {
            // Update the selectedAdvicesListView directly from the collection
            selectedAdvicesListView.ItemsSource = null;
            selectedAdvicesListView.ItemsSource = selectedAdvices;
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button removeButton)
            {
                if (removeButton.DataContext is Medicine selectedMedicine)
                {
                    // Remove the selected medicine from the collection
                    selectedMedicines.Remove(selectedMedicine);

                    // Update the selectedMedicinesListView
                    UpdateSelectedMedicinesListView();
                }
                else if (removeButton.DataContext is Advice selectedAdvice)
                {
                    // Remove the selected advice from the collection
                    selectedAdvices.Remove(selectedAdvice);

                    // Update the selectedAdvicesListView
                    UpdateSelectedAdvicesListView();
                }
            }
        }

        private void ListViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                int index = selectedMedicinesListView.Items.IndexOf(item.DataContext) + 1;
                TextBlock serialNumberTextBlock = FindVisualChild<TextBlock>(item, "serialNumberTextBlock");
                if (serialNumberTextBlock != null)
                {
                    serialNumberTextBlock.Text = index.ToString();
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject depObj, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T && (child as FrameworkElement).Name == name)
                    return child as T;
                else
                {
                    T childItem = FindVisualChild<T>(child, name);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }


        private void SpecialNotes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FollowUp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
