using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


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
            Loaded += AdviceSearchWindow_Loaded;
        }

        private void AdviceSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }

        private void UpdateAdviceItems()
        {
            adviceItemsControl.ItemsSource = displayedAdvices;
        }

        private void SearchAdvices(string Content)
        {
            if(Content != "")
            {
                var searchedAdvices = DatabaseHelper.SearchAdvices(Content);
                displayedAdvices.Clear(); // Clear the existing items in displayedAdvices
                foreach (var advice in searchedAdvices)
                {
                    displayedAdvices.Add(advice); // Add each advice from the search result
                }
                UpdateAdviceItems();
            }
            else
            {
                displayedAdvices.Clear(); // Clear the existing items in displayedAdvices
                foreach (var advice in initialAdvices)
                {
                    displayedAdvices.Add(advice); // Add each advice from the initial advices
                }
                UpdateAdviceItems();
            }

        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAdvices(searchTextBox.Text);
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string adviceContent = searchTextBox.Text;

                Advice newAdvice = new Advice() { Content = adviceContent, IsSelected = true };
                initialAdvices.Add(newAdvice);
                selectedAdvices.Add(newAdvice);
                DatabaseHelper.AddAdvice(newAdvice.Content);

                foreach (var selectedAdvice in selectedAdvices)
                {
                    if (selectedAdvice.Content == newAdvice.Content)
                    {
                        selectedAdvice.IsSelected = true;
                        break;
                    }
                }

                SearchAdvices(searchTextBox.Text);

                // Update the adviceItemsControl.ItemsSource with the updated displayedAdvices
                UpdateAdviceItems();

                // Clear the search text box
                searchTextBox.Text = "";
            }
        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Advice selectedAdvice = toggleButton.DataContext as Advice;

            if (toggleButton.IsChecked == true)
            {
                selectedAdvices.Add(selectedAdvice);
            }
            else
            {
                selectedAdvices.Remove(selectedAdvice);
            }

        }


        public void AddToSelectedAdvices(Advice newAdvice)
        {
            selectedAdvices.Add(newAdvice);
        }

        private void addToAdvice_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseAdviceOccurrence(selectedAdvices);
        }

    }
}
