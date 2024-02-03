using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Windows.Controls;
using System.Xml.Linq;

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
                                                    CREATE TABLE IF NOT EXISTS Medicine (ID INTEGER PRIMARY KEY AUTOINCREMENT, ManufacturerName TEXT NOT NULL, BrandName TEXT NOT NULL, GenericName TEXT NOT NULL, Strength TEXT, MedicineType TEXT, DosageDescription TEXT);
                                                    CREATE TABLE IF NOT EXISTS Advices (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS FollowUp (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS SpecialNotes (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS ChiefComplaint (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS History (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS OnExamination (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Investigation (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Diagnosis (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS TreatmentPlan (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS Patient (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT,  Age TEXT, Address TEXT, Phone TEXT,Blood TEXT);
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
                                ImportMed.Import();
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

        public static List<Complaint> GetInitialComplaint()
        {
            List<Complaint> initialComplaint = new List<Complaint>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM ChiefComplaint ORDER BY Occurrence DESC LIMIT 20";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Complaint comp = new Complaint
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialComplaint.Add(comp);
                    }
                }
            }

            return initialComplaint;
        }

        public static void AddComplaint(string complaint)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var insertCommand = new SQLiteCommand("INSERT INTO ChiefComplaint (Content, Occurrence) VALUES (@Complaint, @Occurrence);", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Complaint", complaint);
                        insertCommand.Parameters.AddWithValue("@Occurrence", 0);

                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting complaint {ex}");
                throw;
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
                                Medicine medicine = new Medicine
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    BrandName = reader["BrandName"].ToString(),
                                    GenericName = reader["GenericName"].ToString(),
                                    Strength = reader["Strength"].ToString(),
                                    ManufacturerName = reader["ManufacturerName"].ToString(),
                                    DosageDescription = reader["DosageDescription"].ToString(),
                                    MedicineType = reader["MedicineType"].ToString()
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
        }

        public static List<Medicine> GetInitialMedicines()
        {
            List<Medicine> initialMedicines = new List<Medicine>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Medicine LIMIT 45;", connection))
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
                                Strength = reader["Strength"].ToString(),
                                ManufacturerName = reader["ManufacturerName"].ToString(),
                                DosageDescription = reader["DosageDescription"].ToString(),
                                MedicineType = reader["MedicineType"].ToString()
                            };

                            initialMedicines.Add(medicine);
                        }
                    }
                }
            }

            return initialMedicines;
        }
        public static List<Advice> GetInitialAdvices()
        {
            List<Advice> initialAdvices = new List<Advice>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM Advices ORDER BY Occurrence DESC LIMIT 20";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Advice advice = new Advice
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialAdvices.Add(advice);
                    }
                }
            }

            return initialAdvices;
        }
        public static List<Advice> SearchAdvices(string keyword)
        {
            List<Advice> searchResults = new List<Advice>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM Advices WHERE Content LIKE @keyword ORDER BY Occurrence";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Advice advice = new Advice
                            {
                                Content = reader["Content"].ToString(),
                                Occurrence = Convert.ToInt32(reader["Occurrence"])
                            };

                            searchResults.Add(advice);
                        }
                    }
                }
            }

            return searchResults;
        }

        public static List<PatientVisit> GetPatientVisitsById(int patientId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM PatientVisit WHERE ID = @PatientId;", connection))
                    {
                        command.Parameters.AddWithValue("@PatientId", patientId);


                        using (var reader = command.ExecuteReader())
                        {
                            List<PatientVisit> patientVisits = new List<PatientVisit>();

                            while (reader.Read())
                            {
                                PatientVisit visit = new PatientVisit
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    Visit = Convert.ToDateTime(reader["VISIT"]),
                                    Medicine = reader["MEDICINE"].ToString(),
                                    Advice = reader["ADVICE"].ToString(),
                                    FollowUp = reader["FOLLOWUP"].ToString(),
                                    Notes = reader["NOTES"].ToString(),
                                    Complaint = reader["COMPLAINT"].ToString(),
                                    History = reader["HISTORY"].ToString(),
                                    OnExamination = reader["ONEXAMINATION"].ToString(),
                                    Investigation = reader["INVESTIGATION"].ToString(),
                                    Diagnosis = reader["DIAGNOSIS"].ToString(),
                                    TreatmentPlan = reader["TREATMENTPLAN"].ToString(),
                                };

                                patientVisits.Add(visit);
                            }

                            return patientVisits;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching patient visits: {ex}");
                throw;
            }
        }

    }

    }



