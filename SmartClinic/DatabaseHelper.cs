// DatabaseHelper.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data.SQLite;
using System.IO;

namespace SmartClinic
{
    public class DatabaseHelper
    {
        private const string DatabaseFileName = "patientinfo.db";
        private static readonly string ConnectionString = $"Data Source={DatabaseFileName};Version=3;";

        static DatabaseHelper()
        {
            InitializeDatabaseFile();
        }

        private static void InitializeDatabaseFile()
        {
            string databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DatabaseFileName);

            if (!File.Exists(databaseFilePath))
            {
                try
                {
                    SQLiteConnection.CreateFile(databaseFilePath); // Create an empty database file

                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        connection.Open();

                        // Check if the "Medicine" table exists
                        using (var checkCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Medicine';", connection))
                        {
                            var result = checkCommand.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                            {
                                // Create the "Medicine" table if it doesn't exist
                                using (var command = new SQLiteCommand(connection))
                                {
                                    // Concatenate all the queries into a single string
                                    string allQueries = @"
                                                    CREATE TABLE IF NOT EXISTS Medicine (ID INTEGER PRIMARY KEY AUTOINCREMENT, ManufacturerName TEXT NOT NULL, BrandName TEXT NOT NULL, GenericName TEXT NOT NULL, Strength TEXT, DosageDescription TEXT, RetailPrice DECIMAL(10, 2), UseFor TEXT, DAR TEXT);
                                                    CREATE TABLE IF NOT EXISTS Advices (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS FollowUp (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS SpecialNotes (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS ChiefComplaint (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS History (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS OnExamination (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Investigation (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Diagnosis (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS TreatmentPlan (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS Patient (ID INTEGER PRIMARY KEY AUTOINCREMENT, AGE TEXT, ADDRESS TEXT, MOBILE TEXT,BLOOD TEXT,);
                                                    CREATE TABLE IF NOT EXISTS PatientVisit (
                                                                                                ID INTEGER, VISIT DATE,
                                                                                                MEDICINE TEXT, ADVICE TEXT, FOLLOWUP TEXT, NOTES TEXT,
                                                                                                COMPLAINT TEXT, HISTORY TEXT, ONEXAMINATION TEXT, INVESTIGATION TEXT,
                                                                                                DIAGNOSIS TEXT, TREATMENTPLAN TEXT,

                                                                                                PRIMARY KEY (ID, VISIT)
                                                                                            );";

                                    command.CommandText = allQueries;

                                    // Execute the concatenated queries
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    Console.WriteLine($"Database file created at: {databaseFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating the database file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Database file already exists at: {databaseFilePath}");
            }
        }


        public static List<Medicine> SearchMedicines(string searchTerm, MedicineSearchCriteria searchCriteria)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = string.Empty;

                    switch (searchCriteria)
                    {
                        case MedicineSearchCriteria.BrandName:
                            query = "SELECT * FROM Medicine WHERE BrandName LIKE @SearchTerm";
                            break;
                        case MedicineSearchCriteria.GenericName:
                            query = "SELECT * FROM Medicine WHERE GenericName LIKE @SearchTerm";
                            break;
                            // Add more cases if needed
                    }

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                        using (var reader = command.ExecuteReader())
                        {
                            List<Medicine> medicines = new List<Medicine>();

                            while (reader.Read())
                            {
                                Medicine medicine = new Medicine(
                                   reader["GenericName"].ToString(),
                                   reader["DosageDescription"].ToString()
                               )
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    BrandName = reader["BrandName"].ToString(),
                                    Strength = reader["Strength"].ToString()
                                };

                                medicines.Add(medicine);
                            }

                            return medicines;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching medicines: {ex}");
                throw;
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public enum MedicineSearchCriteria
        {
            BrandName,
            GenericName,
            // Add more criteria if needed
        }

        public static List<Medicine> GetInitialMedicines()
        {
            List<Medicine> initialMedicines = new List<Medicine>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Medicine LIMIT 15;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medicine medicine = new Medicine(
                             reader["GenericName"].ToString(),
                             reader["DosageDescription"].ToString()
                         )
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        BrandName = reader["BrandName"].ToString(),
                                        Strength = reader["Strength"].ToString()
                                    };

                                    initialMedicines.Add(medicine);
                        }
                    }
                }
            }

            return initialMedicines;
        }
    }

    // Replace the existing Medicine class definition in DatabaseHelper with this one
    public class Medicine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; }
        public string DosageDescription { get; set; }

        // Constructor to set GenericName and DosageDescription
        public Medicine(string genericName, string dosageDescription)
        {
            GenericName = genericName;
            DosageDescription = dosageDescription;
        }

        // Calculated property
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



}
