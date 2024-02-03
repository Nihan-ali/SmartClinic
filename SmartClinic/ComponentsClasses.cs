using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic
{
    internal class ComponentsClasses { }

    public class Advice
    {
        public string Content { get; set; }
        public int Occurrence { get; set; }
        public int DisplayIndex { get; set; }
    }

    public class Complaint
    {
        public string Content { get; set; }
        public string Note { get; set; }
        public int Occurrence { get; set; }
        public int DisplayIndex { get; set; }
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
        public string note { get; set; }

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

}

