using SmartClinic.View.UserControls;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TodayDate { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper init = new DatabaseHelper();

        }

        // MainWindow.xaml.cs
        //public static implicit operator MainWindow(MedicineSearchWindow v)
        //{
        //    throw new NotImplementedException();
        //}
        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of HomeWindow
            HomeWindow homeWindow = new HomeWindow();

            // Set the HomeWindow as the main window
            Application.Current.MainWindow = homeWindow;

            // Show the HomeWindow
            homeWindow.Show();

            // Close the current window
            this.Close();
        }



        private void searchMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}