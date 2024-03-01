﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace SmartClinic
{
    internal class ComponentsClasses { }
    public class variables
    {
        public static string docname = "DR. ABU NOYEM MOHAMMAD";
        public static string docdegree = "MBBS, (Endocrinology & Metabolism)";
        public static string docname_bangla = "ডা.আবু নঈম মোহাম্মাদ";
        public static string docdegree_bangla = "এমবিবিএস, (এন্ডোক্রাইনোলজি ও মেটাবোলিজম)";
        public static string docdetail = "Consultant-Diabetologist, Endocrionologist & Metabolic Disorder Specialist" ;
        public static string docdetail_bangla = "ডায়াবেটিস, হরমোন ও মেডিসিন বিশেষজ্ঞ";
        public static string moredetail_bangla = "আবাসিক চিকিৎসক - আর.পি (মেডিসিন), এম.এ.জি ওসমানী মেডিকেল কলেজ হাসপাতাল, সিলেট";
        public static string chamber = "চেম্বারঃ এবিসি ডায়াগনস্টিক সেন্টার";
        public static string chamber_location = "চৌহাট্টা পয়েট, সদর, সিলেট";
        public static string visit_date = "রোগী দেখার সময়ঃ প্রতি শনি, সোম, মঙ্গল ও বুধবার";
        public static string visit_time = "বিকাল ৫:৩০ থেকে রাত ৮ টা পর্যন্ত";
        public static string chamber_phone = "যোগাযোগঃ 01914-478747 (সকাল ১০টা - ১২টা) রবি, বৃহস্পতি ও শুক্রবার বন্ধ";
        public static string outro = "শরীরের যত্ন নিবেন। নিয়মিত ওষুধ খাবেন। পরবর্তী সাক্ষাতের সময় বাবস্থাপত্র আনবেন। প্রয়োজনে- ০১৮১৯-৮০০৩৩৩ (দুপুর ২টা-৩টা)";
    }


    public class Advice : INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string Content { get; set; }
        public int Occurrence { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

    public class MedicineGroup
    {
        public string GroupName { get; set; }
        public string MedicineList { get; set; }
        public int Occurrence { get; set; }
        public bool IsSelected { get; set; }

    }

    public class Medicine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Occurrence { get; set; }
        private double morningDose;
        private double noonDose;
        private double nightDose;
        private bool afterEatingCheckBox;
        private bool beforeEatingCheckBox;
        private string details;
        private string selectedUnit;
        private string selectedDuration;

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; }
        public string ManufacturerName { get; set; }
        public string MedicineType { get; set; }
        public string DosageDescription { get; set; }
        public string Note { get; set; }
        public string Schedule { get; set; }
        public string Unit { get; set; }
        public int Duration { get; set; }
        public string Details { get; set; }




        public double MorningDose
        {
            get { return morningDose; }
            set
            {
                if (morningDose != value)
                {
                    morningDose = value;
                    OnPropertyChanged(nameof(MorningDose));
                    OnPropertyChanged(nameof(MakeNote));
                    OnPropertyChanged(nameof(formatedDose));
                }
            }
        }

        public double NoonDose
        {
            get { return noonDose; }
            set
            {
                if (noonDose != value)
                {
                    noonDose = value;
                    OnPropertyChanged(nameof(NoonDose));
                    OnPropertyChanged(nameof(MakeNote));
                    OnPropertyChanged(nameof(formatedDose));
                }
            }
        }

        public double NightDose
        {
            get { return nightDose; }
            set
            {
                if (nightDose != value)
                {
                    nightDose = value;
                    OnPropertyChanged(nameof(NightDose));
                    OnPropertyChanged(nameof(MakeNote));
                    OnPropertyChanged(nameof(formatedDose));
                }
            }
        }

        public bool AfterEatingCheckBox
        {
            get { return afterEatingCheckBox; }
            set
            {
                if (afterEatingCheckBox != value)
                {
                    afterEatingCheckBox = value;
                    OnPropertyChanged(nameof(AfterEatingCheckBox));
                    OnPropertyChanged(nameof(MakeNote));
                }
            }
        }

        public bool BeforeEatingCheckBox
        {
            get { return beforeEatingCheckBox; }
            set
            {
                if (beforeEatingCheckBox != value)
                {
                    beforeEatingCheckBox = value;
                    OnPropertyChanged(nameof(BeforeEatingCheckBox));
                    OnPropertyChanged(nameof(MakeNote));
                }
            }
        }



        private ComboBoxItem selectedUnitItem;

        public ComboBoxItem SelectedUnitItem
        {
            get { return selectedUnitItem; }
            set
            {
                if (selectedUnitItem != value)
                {
                    selectedUnitItem = value;
                    OnPropertyChanged(nameof(SelectedUnitItem));
                    OnPropertyChanged(nameof(SelectedUnit));
                    OnPropertyChanged(nameof(formatedDose));
                }
            }
        }

        private ComboBoxItem selectedDurationItem;
        public ComboBoxItem SelectedDurationItem
        {
            get { return selectedDurationItem; }
            set
            {
                if (selectedDurationItem != value)
                {
                    selectedDurationItem = value;
                    OnPropertyChanged(nameof(SelectedDurationItem));
                    OnPropertyChanged(nameof(MakeNote));

                }
            }
        }

        public string SelectedUnit
        {
            get { return (SelectedUnitItem?.Content)?.ToString() ?? "piece"; }
        }

        public string SelectedDuration
        {
            get { return (SelectedDurationItem?.Content)?.ToString() ?? "day"; }
        }

        private string _formatdosage;
        public string formatedDose
        {
            get
            {
                string formattedDosage = FormatDosage(MorningDose) + " + " + FormatDosage(NoonDose) + " + " + FormatDosage(NightDose);
                return formattedDosage;
            }
            set
            {
                _formatdosage = value;
                OnPropertyChanged(nameof(FormatDosage));
            }
        }

        private string _MakeNote;
        public string MakeNote
        {
            get
            {
                string selectedUnit = string.IsNullOrEmpty(SelectedUnit) ? "" : SelectedUnit.ToString();
                string selectedDuration = string.IsNullOrEmpty(SelectedDuration) ? "" : SelectedDuration.ToString();

                // Create a note based on the selected dosage
                string morningDosage = MorningDose > 0 ? $"সকালে  {FormatDosage(MorningDose)} {selectedUnit}" : "";
                string noonDosage = NoonDose > 0 ? $"দুপুরে  {FormatDosage(NoonDose)} {selectedUnit}" : "";
                string nightDosage = NightDose > 0 ? $"রাতে  {FormatDosage(NightDose)} {selectedUnit}" : "";
                string duration = Duration > 0 ? $"{Duration} {selectedDuration} " : "";
                string secheduleText = $"{morningDosage} {noonDosage} {nightDosage}";

                // Check if "After Eating" is selected
                string afterEatingNote = AfterEatingCheckBox ? " খাবার পরে " : "";
                string beforeEatingNote = BeforeEatingCheckBox ? " খাবার আগে " : "";

                string Note = string.IsNullOrEmpty(Details) ? "" : $" {Details}";
                //string formattedDosage = FormatDosage(MorningDose) + " + " + FormatDosage(NoonDose) + " + " + FormatDosage(NightDose);


                // Combine all dosages and notes
                return $"{(MorningDose + NoonDose + NightDose > 0 ? secheduleText + " করে " : "")}{afterEatingNote}{beforeEatingNote}{duration}{Note}";
            }
            set
            {
                _MakeNote = value;
                OnPropertyChanged(nameof(MakeNote));
            }
        }
        private string FormatDosage(double dosage)
        {
            int wholePart = (int)dosage;
            double fractionalPart = dosage - wholePart;

            // Format the fractional part as ½ or ¼
            string fractionalPartString;
            if (fractionalPart == 0.5)
                fractionalPartString = "½";
            else if (fractionalPart == 0.25)
                fractionalPartString = "¼";
            else
                fractionalPartString = fractionalPart.ToString();

            // Combine the whole part and formatted fractional part
            string wholePartString = wholePart == 0 ? "0" : wholePart.ToString();

            // Adjust formatting based on the value of fractionalPart
            string formattedDosage = fractionalPart == 0 ? wholePartString : $"{wholePartString}{fractionalPartString}";

            return formattedDosage;
        }

        private string _MedicineName;
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
            set
            {
                _MedicineName = value;
                OnPropertyChanged(nameof(MedicineName));
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

        public bool IsSelected { get; set; }

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
        public DateTime LastVisit { get; set; } // New property for LastVisit date

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
            public PatientVisit SelectedPatientVisit { get; internal set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class DummyMedicine
    {
        public string MedicineName { get; set; }
        public string formatedDose { get; set; }
        public string MakeNote { get; set; }
    }

    public class PatientVisit:INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string medicine { get; set; }
        public string advice { get; set; }
        public string followUp { get; set; }
        public string notes { get; set; }
        public string complaint { get; set; }
        public string hhistory { get; set; }
        public string onExamination { get; set; }
        public string investigation { get; set; }
        public string diagnosis { get; set; }
        public string treatmentPlan { get; set; }

        private DateTime _visit;
        public Int64 prescriptionId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime visit
        {
            get { return _visit; }
            set
            {
                _visit = value;
                OnPropertyChanged(nameof(visit));
                OnPropertyChanged(nameof(FormattedVisit)); // Notify about the change in the formatted property
            }
        }

        public string FormattedVisit
        {
            get { return _visit.ToString("dd MMM yyyy"); }
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
