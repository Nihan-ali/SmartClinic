using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using static SmartClinic.DatabaseHelper;

namespace SmartClinic.ViewModel
{
    public class SelectedItemsViewModel : ViewModelBase
    {
        public ObservableCollection<Medicine> SelectedMedicines { get; } = new ObservableCollection<Medicine>();
        public ObservableCollection<Advice> SelectedAdvices { get; } = new ObservableCollection<Advice>();
    }
}
