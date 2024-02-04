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

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string advice = searchTextBox.Text;
                Advice selectedAdvice = new Advice();
                selectedAdvice.Content = advice;
                SelectedAdvices.Add(selectedAdvice);
                DatabaseHelper.AddAdvice(advice);
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
