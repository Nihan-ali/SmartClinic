using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using static SmartClinic.DatabaseHelper;

namespace SmartClinic.View.UserControls
{
    public partial class SpecialNoteSearchWindow : Window
    {
        private List<SpecialNote> initialSpecialNotes;
        private ObservableCollection<SpecialNote> displayedSpecialNotes;
        private ObservableCollection<SpecialNote> selectedSpecialNotes;

        public ObservableCollection<SpecialNote> SelectedSpecialNotes => selectedSpecialNotes;

        public SpecialNoteSearchWindow()
        {
            InitializeComponent();

            // Load initial special notes
            initialSpecialNotes = DatabaseHelper.GetInitialSpecialNotes();
            displayedSpecialNotes = new ObservableCollection<SpecialNote>(initialSpecialNotes);
            selectedSpecialNotes = new ObservableCollection<SpecialNote>();

            UpdateSpecialNoteItems();
            Loaded += SpecialNoteSearchWindow_Loaded;
        }

        private void SpecialNoteSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }
        private void UpdateSpecialNoteItems()
        {
            specialNoteItemsControl.ItemsSource = displayedSpecialNotes;
        }



        private void SearchSpecialNotes(string Content)
        {
            if (Content != "")
            {
                var searchedSpecialNotes = DatabaseHelper.SearchSpecialNotes(Content);
                displayedSpecialNotes.Clear(); // Clear the existing items in displayedAdvices
                foreach (var specialNote in searchedSpecialNotes)
                {
                    displayedSpecialNotes.Add(specialNote); // Add each advice from the search result
                }
                UpdateSpecialNoteItems();
            }
            else
            {
                displayedSpecialNotes.Clear(); // Clear the existing items in displayedAdvices
                foreach (var specialNote in initialSpecialNotes)
                {
                    displayedSpecialNotes.Add(specialNote); // Add each advice from the initial advices
                }
                UpdateSpecialNoteItems();
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

            SearchSpecialNotes(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string SpecialNoteContent = searchTextBox.Text;

                SpecialNote newNote = new SpecialNote() { Content = SpecialNoteContent, IsSelected = true };
                initialSpecialNotes.Add(newNote);
                selectedSpecialNotes.Add(newNote);
                DatabaseHelper.AddSpecialNote(newNote.Content);

                foreach (var selectedNote in selectedSpecialNotes)
                {
                    if (selectedNote.Content == newNote.Content)
                    {
                        selectedNote.IsSelected = true;
                        break;
                    }
                }

                SearchSpecialNotes(searchTextBox.Text);

                // Update the adviceItemsControl.ItemsSource with the updated displayedAdvices
                UpdateSpecialNoteItems();

                // Clear the search text box
                searchTextBox.Text = "";
            }
        }

        // make another enterpressed for followup

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            SpecialNote selectedSpecialNote = toggleButton.DataContext as SpecialNote;

            if (toggleButton.IsChecked == true)
            {
                selectedSpecialNotes.Add(selectedSpecialNote);
            }
            else
            {
                selectedSpecialNotes.Remove(selectedSpecialNote);
            }

        }

        public void AddToSelectedSpecialNotes(SpecialNote newSpecialNote)
        {
            selectedSpecialNotes.Add(newSpecialNote);
        }

        private void addToSpecialNote_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseSpecialNoteOccurrence(selectedSpecialNotes);
        }

    }
}