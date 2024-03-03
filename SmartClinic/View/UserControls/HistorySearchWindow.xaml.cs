using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class HistorySearchWindow : Window
    {
        private List<history> initialHistoryItems;
        private ObservableCollection<history> displayedHistoryItems;
        private ObservableCollection<history> selectedHistoryItems;

        public ObservableCollection<history> SelectedHistoryItems => selectedHistoryItems;

        public HistorySearchWindow()
        {
            InitializeComponent();
            initialHistoryItems = DatabaseHelper.GetInitialHistory();
            displayedHistoryItems = new ObservableCollection<history>(initialHistoryItems);
            selectedHistoryItems = new ObservableCollection<history>();
            UpdateHistoryItems();
            Loaded += HistorySearchWindow_Loaded;
        }

        private void HistorySearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }

        public void UpdateHistoryItems()
        {
            historyItemsControl.ItemsSource = displayedHistoryItems;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string historyContent = searchTextBox.Text;
                history selectedHistoryItem = new history();
                selectedHistoryItem.Content = historyContent;
                SelectedHistoryItems.Add(selectedHistoryItem);
                UpdateSelectedHistoryListView();
                DatabaseHelper.AddHistory(historyContent);
                searchTextBox.Text = "";
            }
        }

        private void addToHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseHistoryOccurrence(selectedHistoryItems);
        }



        private void SearchHistory(string Content)
        {
            if (Content != "")
            {
                var searchedHistoryItems = DatabaseHelper.SearchHistories(Content);
                displayedHistoryItems.Clear(); // Clear the existing items in displayedAdvices
                foreach (var historyItem in searchedHistoryItems)
                {
                    displayedHistoryItems.Add(historyItem); // Add each advice from the search result
                }
                UpdateHistoryItems();
            }
            else
            {
                displayedHistoryItems.Clear(); // Clear the existing items in displayedAdvices
                foreach (var historyItem in initialHistoryItems)
                {
                    displayedHistoryItems.Add(historyItem); // Add each advice from the initial advices
                }
                UpdateHistoryItems();
            }
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchHistory(searchTextBox.Text);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            history selectedHistoryItem = toggleButton.DataContext as history;

            if (toggleButton.IsChecked == true)
            {
                SelectedHistoryItems.Add(selectedHistoryItem);
            }
            else
            {
                // Remove the history item if unchecked
                SelectedHistoryItems.Remove(selectedHistoryItem);
            }

            UpdateSelectedHistoryListView();
        }

        private void UpdateSelectedHistoryListView()
        {
            // Set the ItemsSource to the new collection
            selectedHistoryListView.ItemsSource = null;  // Set it to null first
            selectedHistoryListView.ItemsSource = selectedHistoryItems;
        }

        private void RemoveHistoryFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is history selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedHistoryItems.Remove(selectedItem);

                UpdateSelectedHistoryListView();
            }
        }

        private ToggleButton FindToggleButton(history historyItem)
        {
            foreach (var item in historyItemsControl.Items)
            {
                if (historyItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == historyItem)
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
