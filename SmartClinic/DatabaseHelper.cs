// DatabaseHelper.cs
using SmartClinic.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using static SmartClinic.PatientList;

namespace SmartClinic
{
    public class PatientInfo
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }


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
                    using (var checkCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Patient';", connection))
                    {
                        var result = checkCommand.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            // Create the "Medicine" table if it doesn't exist
                            using (var createCommand = new SQLiteCommand("CREATE TABLE Patient ( ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Age TEXT ,  Phone TEXT,  Address TEXT, Blood TEXT);", connection))
                            {
                                createCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }


                Console.WriteLine($"Database file created at: {databaseFilePath}");
            }
            else
            {
                Console.WriteLine($"Database file already exists at: {databaseFilePath}");
            }
        }

        public static void AddPatientIntoDB(string Name, string Age, string phone, string Address, string Blood)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Patient (Name, Age, Phone, Address, Blood) VALUES (@Name, @Age, @Phone, @Address, @Blood)";

                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                     //   command.Parameters.AddWithValue("@ID", 1);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Age", Age);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Blood", Blood);

                        // Execute the query
                        command.ExecuteNonQuery();

                        Console.WriteLine("Patient added successfully to the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static List<PatientInfo> GetAllPatients()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM PatientInfo;", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            List<PatientInfo> patients = new List<PatientInfo>();

                            while (reader.Read())
                            {
                                PatientInfo patient = new PatientInfo
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    PatientName = reader["PatientName"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    BloodGroup = reader["BloodGroup"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString()
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
                Console.WriteLine($"Error getting all patients: {ex}");
                throw;
            }
        }


        // Modify your constructor to call the LoadPatientsFromDatabase function
        // Add this function to your PatientList class


        // Modify your constructor to call the LoadPatientsFromDatabase function







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
                                Medicine medicine = new Medicine
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    BrandName = reader["BrandName"].ToString(),
                                    GenericName = reader["GenericName"].ToString(),
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
                            Medicine medicine = new Medicine
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                BrandName = reader["BrandName"].ToString(),
                                GenericName = reader["GenericName"].ToString(),
                            };

                            initialMedicines.Add(medicine);
                        }
                    }
                }
            }

            return initialMedicines;
        }
    }

    public class Medicine
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        // Add more properties as needed
    }
}
