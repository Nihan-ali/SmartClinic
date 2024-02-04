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
        }

        private void UpdateSpecialNoteItems()
        {
            specialNoteItemsControl.ItemsSource = displayedSpecialNotes;
        }

        private void SearchSpecialNotes(string keyword)
        {
            displayedSpecialNotes = new ObservableCollection<SpecialNote>(
                initialSpecialNotes
                .Where(specialNote => specialNote.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(specialNote => specialNote.Occurrence)
                .Take(20)
                .ToList());

            UpdateSpecialNoteItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSpecialNotes(searchTextBox.Text);
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string specialNote = searchTextBox.Text;
                SpecialNote selectedSpecialNote = new SpecialNote();
                selectedSpecialNote.Content = specialNote;
                SelectedSpecialNotes.Add(selectedSpecialNote);
                DatabaseHelper.AddSpecialNote(specialNote);
                searchTextBox.Text = "";
            }
        }

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