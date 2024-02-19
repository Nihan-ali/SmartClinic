using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading.Tasks;


namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for MedicineCreateWindow.xaml
    /// </summary>
    public partial class MedicineCreateWindow : Window
    {
        public MedicineCreateWindow()
        {
            InitializeComponent();
        }
        private async void AddMedicineToDB_Click(object sender, RoutedEventArgs e)
        {
            if (MedicineNameTextBox.Text == "")
            {
                MessageTextBlock.Text = "Please enter medicine name";
                MessageTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                try
                {
                    DatabaseHelper.AddMedicine(MedicineNameTextBox.Text, ManufacturerNameTextBox.Text, GenericNameTextBox.Text, StrengthTextBox.Text, MedicineTypeTextBox.Text);
                    MessageTextBlock.Foreground = Brushes.Green;
                    MessageTextBlock.Text = "Medicine Added Successfully!";
                    await Task.Delay(2000);
                    MessageTextBlock.Text = "";
                    
                }
                catch (Exception ex)
                {
                    MessageTextBlock.Foreground = Brushes.Red;
                    MessageTextBlock.Text = "Something Error Occurred. Try Again Later.";
                    await Task.Delay(2000);
                    MessageTextBlock.Text = "";
                }

            }
        }
    }
}
