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
        private List<FollowUp> selectedFollowUps = new List<FollowUp>();
        private List<SpecialNote> selectedSpecialNotes = new List<SpecialNote>();

        public List<Medicine> SelectedMedicines => selectedMedicines;
        public List<Advice> SelectedAdvices => selectedAdvices;
        public List<FollowUp> SelectedFollowUps => selectedFollowUps;
        public List<SpecialNote> SelectedSpecialNotes => selectedSpecialNotes;


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
            selectedAdvices.Add(newAdvice);
        }
        public void AddToSelectedFollowUps(FollowUp newFollowUp)
        {
            selectedFollowUps.Add(newFollowUp);
        }
        public void AddToSelectedSpecialNotes(SpecialNote newSpecialNote)
        {
            selectedSpecialNotes.Add(newSpecialNote);
        }

        private void UpdateSelectedAdvicesListView()
        {
            selectedAdvicesListView.ItemsSource = null;
            selectedAdvicesListView.ItemsSource = selectedAdvices;
        }

        private void UpdateSelectedFollowUpsListView()
        {
            selectedFollowUpListView.ItemsSource = null;
            selectedFollowUpListView.ItemsSource = selectedFollowUps;
        }

        private void UpdateSelectedSpecialNotesListView()
        {
            selectedSpecialNoteListView.ItemsSource = null;
            selectedSpecialNoteListView.ItemsSource = selectedSpecialNotes;
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
            UpdateSelectedAdvicesListView();
        }
        private void AddFollowUp_Click(object sender, RoutedEventArgs e)
        {
            FollowUpSearchWindow followUpWindow = new FollowUpSearchWindow();
            followUpWindow.ShowDialog();
            foreach (var selectedFollowUp in followUpWindow.SelectedFollowUps)
            {
                AddToSelectedFollowUps(selectedFollowUp);
            }
            UpdateSelectedFollowUpsListView();

        }
        private void AddSpecialNote_Click(object sender, RoutedEventArgs e)
        {
            SpecialNoteSearchWindow notewindow = new SpecialNoteSearchWindow();
            notewindow.ShowDialog();
            foreach (var selectedNote in notewindow.SelectedSpecialNotes)
            {
                AddToSelectedSpecialNotes(selectedNote);
            }
            UpdateSelectedSpecialNotesListView();
        }

        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
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
                    selectedAdvices.Remove(selectedAdvice);
                }

                else if (removeButton.DataContext is FollowUp selectedFollowUp)
                {
                    selectedFollowUps.Remove(selectedFollowUp);
                }

                else if (removeButton.DataContext is SpecialNote selectedNote)
                {
                    selectedSpecialNotes.Remove(selectedNote);
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
