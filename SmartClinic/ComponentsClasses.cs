using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace SmartClinic
{
    internal class ComponentsClasses { }

    public class Advice
    {
        public string Content { get; set; }
        public int Occurrence { get; set; }
    }

    public class Complaint
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class history
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class Examination
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class Investigation
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class Diagnosis
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class Treatment
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
    }

    public class FollowUp
    {
        public string Content { get; set; }
        public int Occurrence { get; set; }
    }

    public class SpecialNote
    {
        public string Content { get; set; }
        public int Occurrence { get; set; }
    }


    public class Medicine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; }
        public string ManufacturerName { get; set; }
        public string MedicineType { get; set; }
        public string DosageDescription { get; set; }
        public string Note { get; set; }
        public string MedicineName
        {
            get
            {
                // Extract the first 3 letters of MedicineType
                string typePrefix = MedicineType?.Length >= 3 ? MedicineType.Substring(0, 3) : MedicineType;

                // Remove numeric values from GenericName
                string brandNameWithoutDigits = new string(BrandName?.Where(char.IsLetter).ToArray());

                // Combine the components to form MedicineName
                return $"{typePrefix}. {brandNameWithoutDigits} {Strength}";
            }
        }
        public string Type
        {
            get
            {
                // Get the first 3 letters of MedicineType
                return MedicineType?.Length >= 3 ? MedicineType.Substring(0, 3) : MedicineType;
            }
        }

        public string DisplayText => $"{GenericName} - {DosageDescription}";

        public string AdditionalText => $"Take this 3 times";

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Patient : INotifyPropertyChanged
    {
        private bool isSelected;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Blood { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        public class PatientEventArgs : EventArgs
        {
            public Patient NewPatient { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class PatientVisit
    {
        public int Id { get; set; }
        public DateTime Visit { get; set; }
        public string Medicine { get; set; }
        public string Advice { get; set; }
        public string FollowUp { get; set; }
        public string Notes { get; set; }
        public string Complaint { get; set; }
        public string History { get; set; }
        public string OnExamination { get; set; }
        public string Investigation { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
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

