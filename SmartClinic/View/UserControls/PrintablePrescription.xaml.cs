using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SmartClinic.View.UserControls
{
    public partial class PrintablePrescription : UserControl, INotifyPropertyChanged
    {
        public PrintablePrescription()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                OnPropertyChanged(nameof(PatientName));
            }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
            }
        }

        private string _TodayDate;
        public string TodayDate
        {
            get => _TodayDate;
            set
            {
                _TodayDate = value;
                OnPropertyChanged(nameof(TodayDate));
            }
        }

        private string _Age;
        public string Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }

        private ObservableCollection<AdviceItem> _advices1;
        private ObservableCollection<Medicines> _medicines;
        public ObservableCollection<Medicines> medicines
        {
            get { return _medicines; }
            set
            {
                _medicines = value;
                OnPropertyChanged(nameof(medicines));
            }
        }
        public ObservableCollection<AdviceItem> Advices1
        {
            get { return _advices1; }
            set
            {
                _advices1 = value;
                OnPropertyChanged(nameof(Advices1));
            }
        }

        public void UpdateAdvices(List<Advice> adviceList)
        {
            Advices1 = new ObservableCollection<AdviceItem>(
                adviceList.Select(a => new AdviceItem { Content = a.Content, Occurrence = a.Occurrence })
            );
        }

        public void UpdateMedicines(List<DummyMedicine> medicineList)
        {
            medicines = new ObservableCollection<Medicines>(
                    medicineList.Select(a => new Medicines { MedicineName = a.MedicineName,formatedDose=a.formatedDose, MakeNote=a.MakeNote })
                );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _isFollowUpVisible;
        public bool IsFollowUpVisible
        {
            get { return _isFollowUpVisible; }
            set
            {
                _isFollowUpVisible = value;
                OnPropertyChanged(nameof(IsFollowUpVisible));
            }
        }
        public class AdviceItem
        {   
            public string Content { get; set; }
            public int Occurrence { get; set; }
        }
        public class Medicines
        {
            public string MedicineName { get; set; }
            public string formatedDose { get; set; }
            public string MakeNote { get; set; }
        }


    }


    public class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return string.IsNullOrEmpty(stringValue);
            }

            return true; // Default to true if not a string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        
    }


}
