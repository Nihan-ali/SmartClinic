using SmartClinic.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SelectedItemsViewModel SelectedItemsViewModel { get; } = new SelectedItemsViewModel();
    }

}
