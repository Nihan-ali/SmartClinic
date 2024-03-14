using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    public partial class TreatmentSearchWindow : Window
    {
        private List<Treatment> initialTreatments;
        private ObservableCollection<Treatment> displayedTreatments;
        private ObservableCollection<Treatment> selectedTreatments;

        public ObservableCollection<Treatment> SelectedTreatments => selectedTreatments;

        public TreatmentSearchWindow()
        {
            InitializeComponent();
            initialTreatments = DatabaseHelper.GetInitialTreatments();
            displayedTreatments = new ObservableCollection<Treatment>(initialTreatments);
            selectedTreatments = new ObservableCollection<Treatment>();
            UpdateTreatmentItems();
            Loaded += TreatmentSearchWindow_Loaded;
        }

        private void TreatmentSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }
        public void UpdateTreatmentItems()
        {
            treatmentItemsControl.ItemsSource = displayedTreatments;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string treat = searchTextBox.Text;
                Treatment selectedTreatment = new Treatment();
                selectedTreatment.Content = treat;
                SelectedTreatments.Add(selectedTreatment);
                UpdateSelectedTreatmentListView();
                DatabaseHelper.AddTreatment(treat);
                searchTextBox.Text = "";
            }
        }


        private void SearchTreatment(string Content)
        {
            if (Content != "")
            {
                var searchedTreatments = DatabaseHelper.SearchTreatments(Content);
                displayedTreatments.Clear(); // Clear the existing items in displayedAdvices
                foreach (var treatment in searchedTreatments)
                {
                    displayedTreatments.Add(treatment); // Add each advice from the search result
                }
                UpdateTreatmentItems();
            }
            else
            {
                displayedTreatments.Clear(); // Clear the existing items in displayedAdvices
                foreach (var treatment in initialTreatments)
                {
                    displayedTreatments.Add(treatment); // Add each advice from the initial advices
                }
                UpdateTreatmentItems();
            }
        }

        private void addToTreatment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseTreatmentOccurrence(selectedTreatments);
        }

        private bool isFirstCharacterProcessed = false;

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTextBox.TextChanged -= SearchTextBox_TextChanged; // Unsubscribe from the event

            string search = searchTextBox.Text;
            if (search == "")
            {
                isFirstCharacterProcessed = false;
            }

            if (!isFirstCharacterProcessed && search != "")
            {
                if (char.IsUpper(search[0]))
                {
                    search = char.ToLower(search[0]) + search.Substring(1);
                    isFirstCharacterProcessed = true; // Mark as processed
                }
                else if (char.IsLower(search[0]))
                {
                    search = char.ToUpper(search[0]) + search.Substring(1);
                    isFirstCharacterProcessed = true; // Mark as processed
                }
            }

            searchTextBox.Text = search;

            // Restore the cursor position
            searchTextBox.SelectionStart = searchTextBox.Text.Length;

            SearchTreatment(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Treatment selectedTreatment = toggleButton.DataContext as Treatment;

            if (toggleButton.IsChecked == true)
            {
                SelectedTreatments.Add(selectedTreatment);
            }
            else
            {
                // Remove the treatment if unchecked
                SelectedTreatments.Remove(selectedTreatment);
            }

            UpdateSelectedTreatmentListView();
        }

        private void UpdateSelectedTreatmentListView()
        {
            // Set the ItemsSource to the new collection
            selectedTreatmentListView.ItemsSource = null;
            selectedTreatmentListView.ItemsSource = selectedTreatments;
        }


        private void RemoveTreatmentFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Treatment selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedTreatments.Remove(selectedItem);

                UpdateSelectedTreatmentListView();
            }
        }

        private ToggleButton FindToggleButton(Treatment treatment)
        {
            foreach (var item in treatmentItemsControl.Items)
            {
                if (treatmentItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == treatment)
                    {
                        return toggleButton;
                    }
                }
            }

            return null;
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T foundChild)
                {
                    return foundChild;
                }

                T childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }
    }
}