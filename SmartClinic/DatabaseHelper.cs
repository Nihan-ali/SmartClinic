using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;

namespace SmartClinic
{
    public class DatabaseHelper
    {
        private const string DatabaseFileName = "patientInfo.db";
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
                            using (var createCommand = new SQLiteCommand("CREATE TABLE Medicine ( ID INTEGER PRIMARY KEY AUTOINCREMENT,  ManufacturerName TEXT NOT NULL, BrandName TEXT NOT NULL,  GenericName TEXT NOT NULL,  Strength TEXT,   DosageDescription TEXT,    RetailPrice DECIMAL(10, 2),   UseFor TEXT,DAR TEXT);", connection))
                            {
                                createCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Create the "Patient" table if it doesn't exist
                    using (var checkPatientCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Patient';", connection))
                    {
                        var patientResult = checkPatientCommand.ExecuteScalar();
                        if (patientResult == null || patientResult == DBNull.Value)
                        {
                            using (var createPatientCommand = new SQLiteCommand("CREATE TABLE Patient (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Age TEXT, Phone TEXT, Address TEXT, Blood TEXT);", connection))
                            {
                                createPatientCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    Console.WriteLine($"Database file created at: {databaseFilePath}");
                }
            }
            else
            {
                Console.WriteLine($"Database file already exists at: {databaseFilePath}");
            }
        }

        public static void InsertPatientInfo(string name, string age, string phone, string address, string blood)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var insertCommand = new SQLiteCommand("INSERT INTO Patient (Name, Age, Phone, Address, Blood) VALUES (@Name, @Age, @Phone, @Address, @Blood);", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Age", age);
                        insertCommand.Parameters.AddWithValue("@Phone", phone);
                        insertCommand.Parameters.AddWithValue("@Address", address);
                        insertCommand.Parameters.AddWithValue("@Blood", blood);

                        insertCommand.ExecuteNonQuery();

                        Console.WriteLine("Patient information inserted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting patient information: {ex}");
                throw;
            }
        }

        public static List<Patient> GetAllPatients()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Patient;", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            List<Patient> patients = new List<Patient>();

                            while (reader.Read())
                            {
                                Patient patient = new Patient
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Age = reader["Age"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Blood = reader["Blood"].ToString()
                                };

                                patients.Add(patient);
                            }

                            return patients;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching patient information: {ex}");
                throw;
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


    public class Medicine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; }
        public string DosageDescription { get; set; }

        public Medicine(string genericName, string dosageDescription)
        {
            GenericName = genericName;
            DosageDescription = dosageDescription;
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
}
