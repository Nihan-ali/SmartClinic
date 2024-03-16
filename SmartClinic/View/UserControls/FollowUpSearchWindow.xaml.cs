using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using static SmartClinic.DatabaseHelper;

namespace SmartClinic.View.UserControls
{
    public partial class FollowUpSearchWindow : Window
    {
        private List<FollowUp> initialFollowUps;
        private ObservableCollection<FollowUp> displayedFollowUps;
        private ObservableCollection<FollowUp> selectedFollowUps;

        public ObservableCollection<FollowUp> SelectedFollowUps => selectedFollowUps;

        public FollowUpSearchWindow()
        {
            InitializeComponent();

            // Load initial follow-ups
            initialFollowUps = DatabaseHelper.GetInitialFollowUps();
            displayedFollowUps = new ObservableCollection<FollowUp>(initialFollowUps);
            selectedFollowUps = new ObservableCollection<FollowUp>();

            UpdateFollowUpItems();
            Loaded += FollowUpSearchWindow_Loaded;
        }

        private void FollowUpSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }
        private void UpdateFollowUpItems()
        {
            followUpItemsControl.ItemsSource = displayedFollowUps;
        }

        private void SearchFollowUps(string Content)
        {
            if (Content != "")
            {
                var searchedFollowUps = DatabaseHelper.SearchFollowUps(Content);
                displayedFollowUps.Clear(); // Clear the existing items in displayedAdvices
                foreach (var followUp in searchedFollowUps)
                {
                    displayedFollowUps.Add(followUp); // Add each advice from the search result
                }
                UpdateFollowUpItems();
            }
            else
            {
                displayedFollowUps.Clear(); // Clear the existing items in displayedAdvices
                foreach (var followUp in initialFollowUps)
                {
                    displayedFollowUps.Add(followUp); // Add each advice from the initial advices
                }
                UpdateFollowUpItems();
            }
        }

        private bool isFirstCharacterProcessed = false;

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTextBox.TextChanged -= SearchTextBox_TextChanged; // Unsubscribe from the event

            string search = searchTextBox.Text;

            // Process the 0th index value only if it hasn't been processed before
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

            SearchFollowUps(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
        }


        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string FollowUpContent = searchTextBox.Text;

                FollowUp newFollowUp = new FollowUp() { Content = FollowUpContent, IsSelected = true };
                initialFollowUps.Add(newFollowUp);
                selectedFollowUps.Add(newFollowUp);
                DatabaseHelper.AddFollowUp(newFollowUp.Content);

                foreach (var selectedFollowUp in selectedFollowUps)
                {
                    if (selectedFollowUp.Content == newFollowUp.Content)
                    {
                        selectedFollowUp.IsSelected = true;
                        break;
                    }
                }

                SearchFollowUps(searchTextBox.Text);

                // Update the adviceItemsControl.ItemsSource with the updated displayedAdvices
                UpdateFollowUpItems();

                // Clear the search text box
                searchTextBox.Text = "";
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            FollowUp selectedFollowUp = toggleButton.DataContext as FollowUp;

            if (toggleButton.IsChecked == true)
            {
                selectedFollowUps.Add(selectedFollowUp);
            }
            else
            {
                selectedFollowUps.Remove(selectedFollowUp);
            }

        }

        public void AddToSelectedFollowUps(FollowUp newFollowUp)
        {
            selectedFollowUps.Add(newFollowUp);
        }

        private void addToFollowUp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseFollowUpOccurrence(selectedFollowUps);
        }

    }
}