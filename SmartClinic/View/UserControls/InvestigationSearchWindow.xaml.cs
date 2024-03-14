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
    public partial class InvestigationSearchWindow : Window
    {
        private List<Investigation> initialInvestigations;
        private ObservableCollection<Investigation> displayedInvestigations;
        private ObservableCollection<Investigation> selectedInvestigations;

        public ObservableCollection<Investigation> SelectedInvestigations => selectedInvestigations;

        public InvestigationSearchWindow()
        {
            InitializeComponent();
            initialInvestigations = DatabaseHelper.GetInitialInvestigations();
            displayedInvestigations = new ObservableCollection<Investigation>(initialInvestigations);
            selectedInvestigations = new ObservableCollection<Investigation>();
            UpdateInvestigationItems();
            Loaded += InvestigationSearchWindow_Loaded;
        }

        private void InvestigationSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }
        public void UpdateInvestigationItems()
        {
            investigationItemsControl.ItemsSource = displayedInvestigations;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string investigation = searchTextBox.Text;
                Investigation selectedInvestigation = new Investigation();
                selectedInvestigation.Content = investigation;
                SelectedInvestigations.Add(selectedInvestigation);
                UpdateSelectedInvestigationListView();
                DatabaseHelper.AddInvestigation(investigation);
                searchTextBox.Text = "";
            }
        }

        private void SearchInvestigation(string Content)
        {
            if (Content != "")
            {
                var searchedInvestigations = DatabaseHelper.SearchInvestigations(Content);
                displayedInvestigations.Clear(); // Clear the existing items in displayedAdvices
                foreach (var investigation in searchedInvestigations)
                {
                    displayedInvestigations.Add(investigation); // Add each advice from the search result
                }
                UpdateInvestigationItems();
            }
            else
            {
                displayedInvestigations.Clear(); // Clear the existing items in displayedAdvices
                foreach (var investigation in initialInvestigations)
                {
                    displayedInvestigations.Add(investigation); // Add each advice from the initial advices
                }
                UpdateInvestigationItems();
            }
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

            SearchInvestigation(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Investigation selectedInvestigation = toggleButton.DataContext as Investigation;

            if (toggleButton.IsChecked == true)
            {
                SelectedInvestigations.Add(selectedInvestigation);
            }
            else
            {
                SelectedInvestigations.Remove(selectedInvestigation);
            }

            UpdateSelectedInvestigationListView();
        }

        private void UpdateSelectedInvestigationListView()
        {
            selectedInvestigationListView.ItemsSource = null;
            selectedInvestigationListView.ItemsSource = selectedInvestigations;
        }

        private void addToInvestigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseInvestigationOccurrence(selectedInvestigations);
        }

        private void RemoveInvestigationFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Investigation selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedInvestigations.Remove(selectedItem);
                UpdateSelectedInvestigationListView();
            }
        }

        private ToggleButton FindToggleButton(Investigation investigation)
        {
            foreach (var item in investigationItemsControl.Items)
            {
                if (investigationItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == investigation)
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
