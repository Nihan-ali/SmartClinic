using System.Windows;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for SearchResultsWindow.xaml
    /// </summary>
    public partial class SearchResultWindow : Window
    {
        private string _name;
        /*
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        */
        public SearchResultWindow()
        {
           // InitializeComponent();
            DataContext = this;
        }
    }
  
}

