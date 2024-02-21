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
        public static string docdegree = "MBBS, (Endocrinology &amp; Metabolism)";
        public static string docname_bangla = "ডা.আবু নঈম মোহাম্মাদ";
        public static string docdegree_bangla = "এমবিবিএস, (এন্ডোক্রাইনোলজি &amp; মেটাবোলিজম)";
    }


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

    public class MedicineGroup 
    {
        public string GroupName { get; set; }
        public string MedicineList { get; set; }
        public int Occurrence { get; set; }
    }

    public class Medicine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int morningDose;
        private int noonDose;
        private int nightDose;
        private bool afterEatingCheckBox;
        private bool beforeEatingCheckBox;
        private string details;
        private string selectedUnit;

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

        public int MorningDose
        {
            get { return morningDose; }
            set
            {
                if (morningDose != value)
                {
                    morningDose = value;
                    OnPropertyChanged(nameof(MorningDose));
                    OnPropertyChanged(nameof(MakeNote));
                }
            }
        }

        public int NoonDose
        {
            get { return noonDose; }
            set
            {
                if (noonDose != value)
                {
                    noonDose = value;
                    OnPropertyChanged(nameof(NoonDose));
                    OnPropertyChanged(nameof(MakeNote));
                }
            }
        }

        public int NightDose
        {
            get { return nightDose; }
            set
            {
                if (nightDose != value)
                {
                    nightDose = value;
                    OnPropertyChanged(nameof(NightDose));
                    OnPropertyChanged(nameof(MakeNote));
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

        public string Details
        {
            get { return details; }
            set
            {
                if (details != value)
                {
                    details = value;
                    OnPropertyChanged(nameof(Details));
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
                }
            }
        }

        public string SelectedUnit
        {
            get { return (SelectedUnitItem?.Content)?.ToString() ?? "piece"; }
        }



        public string MakeNote
        {
            get
            {
                string selectedUnit = (SelectedUnit ?? "piece").ToString();

                // Create a note based on the selected dosage
                string morningDosage = MorningDose > 0 ? $"সকালে  {MorningDose}  {selectedUnit}" : "";
                string noonDosage = NoonDose > 0 ? $"দুপুরে  {NoonDose}   {selectedUnit}" : "";
                string nightDosage = NightDose > 0 ? $"রাতে  {NightDose}   {selectedUnit}" : "";
                string duration = Duration > 0 ? $"{Duration} দিন " : "";
                string secheduleText = $"{morningDosage} {noonDosage} {nightDosage}";

                // Check if "After Eating" is selected
                string afterEatingNote = AfterEatingCheckBox ? " খাবার পরে " : "";
                string beforeEatingNote = BeforeEatingCheckBox ? " খাবার আগে " : "";

                // Combine all dosages and notes
                return $"{(secheduleText != null ? secheduleText + " করে " : "")}{afterEatingNote}{beforeEatingNote}{duration}{Note}";
            }
        }

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

