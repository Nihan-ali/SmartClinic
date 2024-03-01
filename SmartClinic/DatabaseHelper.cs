﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;

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
                                                    CREATE TABLE IF NOT EXISTS Medicine (ID INTEGER PRIMARY KEY AUTOINCREMENT, ManufacturerName TEXT, BrandName TEXT, GenericName TEXT, Strength TEXT, MedicineType TEXT,Occurrence INTEGER NOT NULL, DosageDescription TEXT);
                                                    CREATE TABLE IF NOT EXISTS MedicineGroup(GroupName TEXT, MedicineList TEXT, Occurrence INTEGER NOT NULL);                                                    

                                                    CREATE TABLE IF NOT EXISTS Advices (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS FollowUp (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS SpecialNotes (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS ChiefComplaint (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS History (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS OnExamination (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Investigation (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS Diagnosis (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);
                                                    CREATE TABLE IF NOT EXISTS TreatmentPlan (Content TEXT NOT NULL PRIMARY KEY, Occurrence INTEGER NOT NULL);

                                                    CREATE TABLE IF NOT EXISTS Patient (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age TEXT, Address TEXT, Phone TEXT,Blood TEXT, LastVisit DATE);
                                                    CREATE TABLE IF NOT EXISTS PatientVisit (
                                                                                                ID INTEGER, VISIT DATE, PRESCRIPTIONID INTEGER PRIMARY KEY,
                                                                                                MEDICINE TEXT, ADVICE TEXT, FOLLOWUP TEXT, NOTES TEXT, NAME TEXT,
                                                                                                COMPLAINT TEXT, HISTORY TEXT, ONEXAMINATION TEXT, INVESTIGATION TEXT,
                                                                                                DIAGNOSIS TEXT, TREATMENTPLAN TEXT,
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

                string query = "SELECT Content, Occurrence FROM ChiefComplaint ORDER BY Occurrence DESC LIMIT 30";

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
        public static List<history> GetInitialHistory()
        {
            List<history> initialHistory = new List<history>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM History ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        history historyitem = new history
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialHistory.Add(historyitem);
                    }
                }
            }

            return initialHistory;
        }
        public static List<Examination> GetInitialExaminations()
        {
            List<Examination> initialExaminations = new List<Examination>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM OnExamination ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Examination examination = new Examination
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialExaminations.Add(examination);
                    }
                }
            }

            return initialExaminations;
        }
        public static List<Investigation> GetInitialInvestigations()
        {
            List<Investigation> initialInvestigations = new List<Investigation>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM Investigation ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Investigation investigation = new Investigation
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialInvestigations.Add(investigation);
                    }
                }
            }

            return initialInvestigations;
        }
        public static List<Diagnosis> GetInitialDiagnoses()
        {
            List<Diagnosis> initialDiagnoses = new List<Diagnosis>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM Diagnosis ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Diagnosis diagnosis = new Diagnosis
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialDiagnoses.Add(diagnosis);
                    }
                }
            }

            return initialDiagnoses;
        }
        public static List<Treatment> GetInitialTreatments()
        {
            List<Treatment> initialTreatments = new List<Treatment>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM TreatmentPlan ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Treatment treatment = new Treatment
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialTreatments.Add(treatment);
                    }
                }
            }

            return initialTreatments;
        }
        public static List<Medicine> GetInitialMedicines()
        {
            List<Medicine> initialMedicines = new List<Medicine>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Medicine ORDER BY Occurrence DESC LIMIT 30;", connection))
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
        public static List<MedicineGroup> GetInitialMedicineGroups()
        {
            List<MedicineGroup> initialMedicineGroups = new List<MedicineGroup>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM MedicineGroup ORDER BY Occurrence DESC LIMIT 30;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MedicineGroup medicineGroup = new MedicineGroup
                            {
                                GroupName = reader["GroupName"].ToString(),
                                MedicineList = reader["MedicineList"].ToString()
                            };

                            initialMedicineGroups.Add(medicineGroup);
                        }
                    }
                }
            }

            return initialMedicineGroups;
        }
        public static List<Advice> GetInitialAdvices()
        {
            List<Advice> initialAdvices = new List<Advice>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM Advices ORDER BY Occurrence DESC LIMIT 5";

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
        public static List<FollowUp> GetInitialFollowUps()
        {
            List<FollowUp> initialFollowUps = new List<FollowUp>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM FollowUp ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FollowUp followUp = new FollowUp
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialFollowUps.Add(followUp);
                    }
                }
            }

            return initialFollowUps;
        }
        public static List<SpecialNote> GetInitialSpecialNotes()
        {
            List<SpecialNote> initialSpecialNotes = new List<SpecialNote>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Content, Occurrence FROM SpecialNotes ORDER BY Occurrence DESC LIMIT 30";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpecialNote specialNote = new SpecialNote
                        {
                            Content = reader["Content"].ToString(),
                            Occurrence = Convert.ToInt32(reader["Occurrence"])
                        };

                        initialSpecialNotes.Add(specialNote);
                    }
                }
            }

            return initialSpecialNotes;
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
                                    visit = Convert.ToDateTime(reader["VISIT"]),
                                    prescriptionId = Convert.ToInt64(reader["PRESCRIPTIONID"]),
                                    medicine = reader["MEDICINE"].ToString(),
                                    advice = reader["ADVICE"].ToString(),
                                    followUp = reader["FOLLOWUP"].ToString(),
                                    notes = reader["NOTES"].ToString(),
                                    complaint = reader["COMPLAINT"].ToString(),
                                    hhistory = reader["HISTORY"].ToString(),
                                    onExamination = reader["ONEXAMINATION"].ToString(),
                                    investigation = reader["INVESTIGATION"].ToString(),
                                    diagnosis = reader["DIAGNOSIS"].ToString(),
                                    treatmentPlan = reader["TREATMENTPLAN"].ToString(),
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

        public static Patient GetPatientById(int Id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Patient WHERE ID = @Id;", connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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

                                return patient;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching patient: {ex}");
                throw;
            }
        }

        public static List<Patient> GetPatientByIdNear(int id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Patient WHERE ID LIKE @Id;", connection))
                    {
                        command.Parameters.AddWithValue("@Id", $"{id}%");

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
                                    Blood = reader["Blood"].ToString(),
                                    LastVisit = Convert.ToDateTime(reader["LastVisit"])
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
                Console.WriteLine($"Error fetching patient: {ex}");
                throw;
            }
        }

        public static List<Patient> GetPatientsByName(string name)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Patient WHERE Name LIKE @Name;", connection))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{name}%");

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
                                    Blood = reader["Blood"].ToString(),
                                    LastVisit = Convert.ToDateTime(reader["LastVisit"])
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

        public static int GetPatientVisitsByVisit(string startdate, string enddate)
        {
            try
            {
                // Prepare the query to count patients within the specified date range
                string query = "SELECT COUNT(ID) AS TotalPatients " +
                               "FROM PatientVisit " +
                               "WHERE VISIT BETWEEN @StartDate AND @EndDate;";

                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Creating a command object with the SQL query and the connection
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Adding parameters for start and end dates
                        command.Parameters.AddWithValue("@StartDate", startdate);
                        command.Parameters.AddWithValue("@EndDate", enddate);

                        // Executing the query and retrieving the total count of patients
                        object result = command.ExecuteScalar();

                        // Checking if the result is not null and converting it to an integer
                        if (result != null && result != DBNull.Value)
                        {
                            int totalPatients = Convert.ToInt32(result);
                            Console.WriteLine($"Total number of patients visiting between {startdate} and {enddate}: " + totalPatients);
                            return totalPatients;
                        }
                        else
                        {
                            Console.WriteLine("No patients visited within the specified date range.");
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient visits: {ex}");
                throw;
            }
        }
        public static Medicine GetMedicineById(int medicineId)
        {

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Medicine WHERE ID = @MedicineId;", connection))
                    {
                        command.Parameters.AddWithValue("@MedicineId", medicineId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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

                                return medicine;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching medicine: {ex}");
                throw;
            }
        }

        public static List<Patient> GetPatientsByLastVisitedDate(int n)
        {
            List<Patient> patients = new List<Patient>();

            // Calculate the offset based on the provided integer 'n'
            int offset = (n - 1) * 25;

            // SQL query to select patients sorted by last visited date and limit the result to 25 records starting from the calculated offset
            string query = $"SELECT * FROM Patient ORDER BY LastVisit DESC LIMIT 25 OFFSET {offset}";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming Patient class has appropriate properties and constructor
                                Patient patient = new Patient
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Age = Convert.ToString(reader["Age"]),
                                    Phone = Convert.ToString(reader["Phone"]),
                                    Address = Convert.ToString(reader["Address"]),
                                    Blood = Convert.ToString(reader["Blood"]),
                                    LastVisit = Convert.ToDateTime(reader["LastVisit"])
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return patients;
        }


        public static void AddMedicine(string brandName, string manufacturerName, string genericName, string strength, string medicineType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Medicine WHERE BrandName = @BrandName AND GenericName = @GenericName AND Strength = @Strength;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@BrandName", brandName);
                        checkCommand.Parameters.AddWithValue("@GenericName", genericName);
                        checkCommand.Parameters.AddWithValue("@Strength", strength);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO Medicine (BrandName, ManufacturerName, GenericName, Strength, MedicineType, Occurrence) VALUES (@BrandName, @ManufacturerName, @GenericName, @Strength, @MedicineType, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@BrandName", brandName);
                                insertCommand.Parameters.AddWithValue("@ManufacturerName", manufacturerName);
                                insertCommand.Parameters.AddWithValue("@GenericName", genericName);
                                insertCommand.Parameters.AddWithValue("@Strength", strength);
                                insertCommand.Parameters.AddWithValue("@MedicineType", medicineType);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 0);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting medicine: {ex}");
                throw;
            }
        }
        public static void AddMedicineGroup(string groupName, string medicineList)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM MedicineGroup WHERE GroupName = @GroupName;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@GroupName", groupName);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO MedicineGroup (GroupName, MedicineList, Occurrence) VALUES (@GroupName, @MedicineList, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@GroupName", groupName);
                                insertCommand.Parameters.AddWithValue("@MedicineList", medicineList);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting medicine group: {ex}");
                throw;
            }
        }
        public static void AddComplaint(string complaint)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM ChiefComplaint WHERE Content = @Complaint;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Complaint", complaint);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO ChiefComplaint (Content, Occurrence) VALUES (@Complaint, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Complaint", complaint);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting complaint: {ex}");
                throw;
            }
        }
        public static void AddHistory(string history)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM History WHERE Content = @History;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@History", history);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO History (Content, Occurrence) VALUES (@History, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@History", history);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting history: {ex}");
                throw;
            }
        }
        public static void AddExamination(string examination)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM OnExamination WHERE Content = @Examination;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Examination", examination);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO OnExamination (Content, Occurrence) VALUES (@Examination, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Examination", examination);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting examination: {ex}");
                throw;
            }
        }
        public static void AddInvestigation(string investigation)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Investigation WHERE Content = @Investigation;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Investigation", investigation);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO Investigation (Content, Occurrence) VALUES (@Investigation, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Investigation", investigation);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting investigation: {ex}");
                throw;
            }
        }
        public static void AddDiagnosis(string diagnosis)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Diagnosis WHERE Content = @Diagnosis;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Diagnosis", diagnosis);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO Diagnosis (Content, Occurrence) VALUES (@Diagnosis, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Diagnosis", diagnosis);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting diagnosis: {ex}");
                throw;
            }
        }
        public static void AddTreatment(string treatment)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM TreatmentPlan WHERE Content = @Treatment;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Treatment", treatment);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO TreatmentPlan (Content, Occurrence) VALUES (@Treatment, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Treatment", treatment);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting treatment: {ex}");
                throw;
            }
        }
        public static void AddAdvice(string advice)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Advices WHERE Content = @Advice;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Advice", advice);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO Advices (Content, Occurrence) VALUES (@Advice, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Advice", advice);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting advice: {ex}");
                throw;
            }
        }
        public static void AddFollowUp(string followUp)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM FollowUp WHERE Content = @FollowUp;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@FollowUp", followUp);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO FollowUp (Content, Occurrence) VALUES (@FollowUp, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@FollowUp", followUp);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting follow-up: {ex}");
                throw;
            }
        }
        public static void AddSpecialNote(string specialNote)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM SpecialNotes WHERE Content = @SpecialNote;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@SpecialNote", specialNote);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCommand = new SQLiteCommand("INSERT INTO SpecialNotes (Content, Occurrence) VALUES (@SpecialNote, @Occurrence);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@SpecialNote", specialNote);
                                insertCommand.Parameters.AddWithValue("@Occurrence", 1);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting special note: {ex}");
                throw;
            }
        }


        public static void IncreaseComplaintOccurrence(ObservableCollection<Complaint> complaint)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var complaintItem in complaint)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE ChiefComplaint SET Occurrence = Occurrence + 1 WHERE Content = @Complaint;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Complaint", complaintItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing complaint occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseHistoryOccurrence(ObservableCollection<history> history)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var historyItem in history)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE History SET Occurrence = Occurrence + 1 WHERE Content = @History;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@History", historyItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing history occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseExaminationOccurrence(ObservableCollection<Examination> examination)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var examinationItem in examination)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE OnExamination SET Occurrence = Occurrence + 1 WHERE Content = @Examination;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Examination", examinationItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing examination occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseInvestigationOccurrence(ObservableCollection<Investigation> investigation)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var investigationItem in investigation)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE Investigation SET Occurrence = Occurrence + 1 WHERE Content = @Investigation;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Investigation", investigationItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing investigation occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseDiagnosisOccurrence(ObservableCollection<Diagnosis> diagnosis)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var diagnosisItem in diagnosis)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE Diagnosis SET Occurrence = Occurrence + 1 WHERE Content = @Diagnosis;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Diagnosis", diagnosisItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing diagnosis occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseTreatmentOccurrence(ObservableCollection<Treatment> treatment)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var treatmentItem in treatment)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE TreatmentPlan SET Occurrence = Occurrence + 1 WHERE Content = @Treatment;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Treatment", treatmentItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing treatment occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseAdviceOccurrence(ObservableCollection<Advice> advice)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var adviceItem in advice)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE Advices SET Occurrence = Occurrence + 1 WHERE Content = @Advice;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Advice", adviceItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing advice occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseFollowUpOccurrence(ObservableCollection<FollowUp> followUp)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var followUpItem in followUp)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE FollowUp SET Occurrence = Occurrence + 1 WHERE Content = @FollowUp;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@FollowUp", followUpItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing follow-up occurrence: {ex}");
                throw;
            }
        }
        public static void IncreaseSpecialNoteOccurrence(ObservableCollection<SpecialNote> specialNote)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var specialNoteItem in specialNote)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE SpecialNotes SET Occurrence = Occurrence + 1 WHERE Content = @SpecialNote;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@SpecialNote", specialNoteItem.Content);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing special note occurrence: {ex}");
                throw;
            }
        }

        public static void IncreaseMedicineOccurrence(ObservableCollection<Medicine> medicine)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var medicineItem in medicine)
                    {
                        using (var updateCommand = new SQLiteCommand("UPDATE Medicine SET Occurrence = Occurrence + 1 WHERE BrandName = @BrandName AND GenericName = @GenericName AND Strength = @Strength;", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@BrandName", medicineItem.BrandName);
                            updateCommand.Parameters.AddWithValue("@GenericName", medicineItem.GenericName);
                            updateCommand.Parameters.AddWithValue("@Strength", medicineItem.Strength);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing medicine occurrence: {ex}");
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
        public static List<PatientVisit> SearchPrescriptionByPrescriptionId(Int64 prescriptionid)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM PatientVisit WHERE PRESCRIPTIONID LIKE @PrescriptionId;", connection))
                    {
                        // Constructing the search pattern to match partially
                        command.Parameters.AddWithValue("@PrescriptionId", "%" + prescriptionid + "%");

                        using (var reader = command.ExecuteReader())
                        {
                            List<PatientVisit> patientVisits = new List<PatientVisit>();

                            while (reader.Read())
                            {
                                PatientVisit visit = new PatientVisit
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    visit = Convert.ToDateTime(reader["VISIT"]),
                                    Name = reader["NAME"].ToString(),
                                    prescriptionId = Convert.ToInt64(reader["PRESCRIPTIONID"]),
                                    medicine = reader["MEDICINE"].ToString(),
                                    advice = reader["ADVICE"].ToString(),
                                    followUp = reader["FOLLOWUP"].ToString(),
                                    notes = reader["NOTES"].ToString(),
                                    complaint = reader["COMPLAINT"].ToString(),
                                    hhistory = reader["HISTORY"].ToString(),
                                    onExamination = reader["ONEXAMINATION"].ToString(),
                                    investigation = reader["INVESTIGATION"].ToString(),
                                    diagnosis = reader["DIAGNOSIS"].ToString(),
                                    treatmentPlan = reader["TREATMENTPLAN"].ToString(),
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
                Console.WriteLine($"Error searching patients: {ex}");
                throw;
            }
        }
        public static List<Patient> SearchPatients(string searchTerm)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Patient WHERE Name LIKE @SearchTerm OR Phone LIKE @SearchTerm;", connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

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
                Console.WriteLine($"Error searching patients: {ex}");
                throw;
            }
        }
        public static List<Advice> SearchAdvices(string searchTerm)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SQLiteCommand("SELECT * FROM Advices WHERE Content LIKE @SearchTerm;", connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                        using (var reader = command.ExecuteReader())
                        {
                            List<Advice> advices = new List<Advice>();

                            while (reader.Read())
                            {
                                Advice advice = new Advice
                                {
                                    Content = reader["Content"].ToString(),
                                    Occurrence = Convert.ToInt32(reader["Occurrence"])
                                };

                                advices.Add(advice);
                            }

                            return advices;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching advices: {ex}");
                throw;
            }
        }



        public static int InsertPatientInfo(string name, string age, string phone, string address, string blood)
        {
            try
            {
                int insertedId = 0;

                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var insertCommand = new SQLiteCommand("INSERT INTO Patient (Name, Age, Phone, Address, Blood) VALUES (@Name, @Age, @Phone, @Address, @Blood); SELECT last_insert_rowid();", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Age", age);
                        insertCommand.Parameters.AddWithValue("@Phone", phone);
                        insertCommand.Parameters.AddWithValue("@Address", address);
                        insertCommand.Parameters.AddWithValue("@Blood", blood);

                        // Execute the query and retrieve the last inserted ID
                        insertedId = Convert.ToInt32(insertCommand.ExecuteScalar());

                        Console.WriteLine("Patient information inserted successfully. ID: " + insertedId);
                    }
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting patient information: {ex}");
                throw;
            }
        }

        public static List<PatientVisit> GetPatientVisits()
        {
            List<PatientVisit> patientVisits = new List<PatientVisit>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM PatientVisit";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientVisit visit = new PatientVisit
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                visit = Convert.ToDateTime(reader["VISIT"]),
                                medicine = Convert.ToString(reader["MEDICINE"]),
                                advice = Convert.ToString(reader["ADVICE"]),
                                followUp = Convert.ToString(reader["FOLLOWUP"]),
                                notes = Convert.ToString(reader["NOTES"]),
                                complaint = Convert.ToString(reader["COMPLAINT"]),
                                hhistory = Convert.ToString(reader["HISTORY"]),
                                onExamination = Convert.ToString(reader["ONEXAMINATION"]),
                                investigation = Convert.ToString(reader["INVESTIGATION"]),
                                diagnosis = Convert.ToString(reader["DIAGNOSIS"]),
                                treatmentPlan = Convert.ToString(reader["TREATMENTPLAN"]),
                            };

                            patientVisits.Add(visit);
                        }
                    }
                }
            }

            return patientVisits;
        }

        public static bool DeletePatient(int patientId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Use a parameterized query to avoid SQL injection
                    string query = "DELETE FROM Patient WHERE ID = @PatientId";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientId", patientId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting patient: {ex.Message}");
                return false;
            }
        }
        public static bool DeletePatientByPrescriptionId(Int64 prescriptionId)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this patient?", "Delete Patient", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var connection = GetConnection())
                    {
                        connection.Open();

                        // Use a parameterized query to avoid SQL injection
                        string query = "DELETE FROM PatientVisit WHERE PRESCRIPTIONID = @prescriptionid";

                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@prescriptionid", prescriptionId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Patient deleted successfully.");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("Patient not found or deletion failed.");
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting patient: {ex.Message}");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }


        public static List<Complaint> ExtractComplaint(string combinedString)
        {
            string[] complaintPairs = combinedString.Split("$$");

            List<Complaint> ccomplaints = new List<Complaint>();

            foreach (string complaintPair in complaintPairs)
            {
                string[] pairValues = complaintPair.Split('@');
                if (pairValues.Length == 2)
                {
                    Complaint complaint = new Complaint
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    ccomplaints.Add(complaint);
                }
            }
            return ccomplaints;
        }
        public static List<history> ExtractHistory(string combinedString)
        {
            string[] historyPairs = combinedString.Split("$$");

            List<history> hhistory = new List<history>();

            foreach (string historyPair in historyPairs)
            {
                string[] pairValues = historyPair.Split('@');
                if (pairValues.Length == 2)
                {
                    history history = new history
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    hhistory.Add(history);
                }
            }
            return hhistory;
        }
        public static List<Examination> ExtractExamination(string combinedString)
        {
            string[] examinationPairs = combinedString.Split("$$");

            List<Examination> eexaminations = new List<Examination>();

            foreach (string examinationPair in examinationPairs)
            {
                string[] pairValues = examinationPair.Split('@');
                if (pairValues.Length == 2)
                {
                    Examination examination = new Examination
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    eexaminations.Add(examination);
                }
            }
            return eexaminations;
        }
        public static List<Investigation> ExtractInvestigation(string combinedString)
        {
            string[] investigationPairs = combinedString.Split("$$");

            List<Investigation> iinvestigations = new List<Investigation>();

            foreach (string investigationPair in investigationPairs)
            {
                string[] pairValues = investigationPair.Split('@');
                if (pairValues.Length == 2)
                {
                    Investigation investigation = new Investigation
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    iinvestigations.Add(investigation);
                }
            }
            return iinvestigations;
        }
        public static List<Diagnosis> ExtractDiagnosis(string combinedString)
        {
            string[] diagnosisPairs = combinedString.Split("$$");

            List<Diagnosis> ddiagnoses = new List<Diagnosis>();

            foreach (string diagnosisPair in diagnosisPairs)
            {
                string[] pairValues = diagnosisPair.Split('@');
                if (pairValues.Length == 2)
                {
                    Diagnosis diagnosis = new Diagnosis
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    ddiagnoses.Add(diagnosis);
                }
            }
            return ddiagnoses;
        }
        public static List<Treatment> ExtractTreatment(string combinedString)
        {
            string[] treatmentPairs = combinedString.Split("$$");

            List<Treatment> ttreatments = new List<Treatment>();

            foreach (string treatmentPair in treatmentPairs)
            {
                string[] pairValues = treatmentPair.Split('@');
                if (pairValues.Length == 2)
                {
                    Treatment treatment = new Treatment
                    {
                        Content = pairValues[0],
                        Note = pairValues[1]
                    };
                    ttreatments.Add(treatment);
                }
            }
            return ttreatments;
        }
        public static List<DummyMedicine> ExtractMedicine(string combinedString)
        {
            string[] medicinePairs = combinedString.Split("$$");

            List<DummyMedicine> medicines = new List<DummyMedicine>();

            foreach (string medicinePair in medicinePairs)
            {
                string[] pairValues = medicinePair.Split('@');
                if (pairValues.Length == 2)
                {
                    string[] doseAndNote = pairValues[1].Split('&');
                    if (doseAndNote.Length == 2)
                    {
                        DummyMedicine medicine = new DummyMedicine
                        {
                            MedicineName = pairValues[0],
                            formatedDose = doseAndNote[0],
                            MakeNote = doseAndNote[1]
                        };

                        medicines.Add(medicine);
                    }
                }
            }
            return medicines;
        }
        public static List<Advice> ExtractAdvice(string combinedString)
        {
            string[] adviceValues = combinedString.Split("$$");

            List<Advice> extractedAdviceList = new List<Advice>();

            foreach (string value in adviceValues)
            {
                if(value != "")
                {
                    Advice advice = new Advice
                    {
                        Content = value
                    };
                    extractedAdviceList.Add(advice);
                }
            }
            return extractedAdviceList;
        }
        public static List<FollowUp> ExtractFollowUp(string combinedString)
        {
            string[] followUpValues = combinedString.Split("$$");

            List<FollowUp> extractedFollowUpList = new List<FollowUp>();

            foreach (string value in followUpValues)
            {
                if(value != "")
                {
                    FollowUp followUp = new FollowUp
                    {
                        Content = value
                    };
                    extractedFollowUpList.Add(followUp);
                }
            }
            return extractedFollowUpList;
        }
        public static List<SpecialNote> ExtractSpecialNotes(string combinedString)
        {
            string[] specialNoteValues = combinedString.Split("$$");

            List<SpecialNote> extractedSpecialNoteList = new List<SpecialNote>();

            foreach (string value in specialNoteValues)
            {
                if(value != "")
                {
                    SpecialNote specialNote = new SpecialNote
                    {
                        Content = value
                    };
                    extractedSpecialNoteList.Add(specialNote);
                }

            }
            return extractedSpecialNoteList;
        }

        public static void SavePrescription(PatientVisit prescription)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Check if there are any existing prescriptions with matching attributes
                    using (var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM PatientVisit WHERE ID = @ID AND NAME = @NAME AND COMPLAINT = @COMPLAINT AND HISTORY = @HISTORY AND ONEXAMINATION = @ONEXAMINATION AND INVESTIGATION = @INVESTIGATION AND DIAGNOSIS = @DIAGNOSIS AND TREATMENTPLAN = @TREATMENTPLAN AND MEDICINE = @MEDICINE AND ADVICE = @ADVICE AND FOLLOWUP = @FOLLOWUP AND NOTES = @NOTES;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@ID", prescription.Id);
                        checkCommand.Parameters.AddWithValue("@NAME", prescription.Name);
                        checkCommand.Parameters.AddWithValue("@COMPLAINT", prescription.complaint);
                        checkCommand.Parameters.AddWithValue("@HISTORY", prescription.hhistory);
                        checkCommand.Parameters.AddWithValue("@ONEXAMINATION", prescription.onExamination);
                        checkCommand.Parameters.AddWithValue("@INVESTIGATION", prescription.investigation);
                        checkCommand.Parameters.AddWithValue("@DIAGNOSIS", prescription.diagnosis);
                        checkCommand.Parameters.AddWithValue("@TREATMENTPLAN", prescription.treatmentPlan);
                        checkCommand.Parameters.AddWithValue("@MEDICINE", prescription.medicine);
                        checkCommand.Parameters.AddWithValue("@ADVICE", prescription.advice);
                        checkCommand.Parameters.AddWithValue("@FOLLOWUP", prescription.followUp);
                        checkCommand.Parameters.AddWithValue("@NOTES", prescription.notes);

                        Int64 count = (Int64)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            // If no existing prescription found, proceed to insert
                            prescription.prescriptionId = GeneratePrescriptionId(prescription.Id);

                            // Insert the prescription into the database
                            using (var insertCommand = new SQLiteCommand("INSERT INTO PatientVisit (ID, VISIT, NAME, PRESCRIPTIONID, COMPLAINT, HISTORY, ONEXAMINATION, INVESTIGATION, DIAGNOSIS, TREATMENTPLAN, MEDICINE, ADVICE, FOLLOWUP, NOTES) VALUES (@ID, @VISIT, @NAME, @PRESCRIPTIONID, @COMPLAINT, @HISTORY, @ONEXAMINATION, @INVESTIGATION, @DIAGNOSIS, @TREATMENTPLAN, @MEDICINE, @ADVICE, @FOLLOWUP, @NOTES);", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@ID", prescription.Id);
                                insertCommand.Parameters.AddWithValue("@VISIT", prescription.visit);
                                insertCommand.Parameters.AddWithValue("@NAME", prescription.Name);
                                insertCommand.Parameters.AddWithValue("@PRESCRIPTIONID", prescription.prescriptionId);
                                insertCommand.Parameters.AddWithValue("@COMPLAINT", prescription.complaint);
                                insertCommand.Parameters.AddWithValue("@HISTORY", prescription.hhistory);
                                insertCommand.Parameters.AddWithValue("@ONEXAMINATION", prescription.onExamination);
                                insertCommand.Parameters.AddWithValue("@INVESTIGATION", prescription.investigation);
                                insertCommand.Parameters.AddWithValue("@DIAGNOSIS", prescription.diagnosis);
                                insertCommand.Parameters.AddWithValue("@TREATMENTPLAN", prescription.treatmentPlan);
                                insertCommand.Parameters.AddWithValue("@MEDICINE", prescription.medicine);
                                insertCommand.Parameters.AddWithValue("@ADVICE", prescription.advice);
                                insertCommand.Parameters.AddWithValue("@FOLLOWUP", prescription.followUp);
                                insertCommand.Parameters.AddWithValue("@NOTES", prescription.notes);

                                insertCommand.ExecuteNonQuery();
                            }
                            UpdatePatientLastVisit(prescription.Id, prescription.visit);
                        }
                        else
                        {
                            // If a matching prescription exists, do not insert to avoid duplication
                            MessageBox.Show("Same Prescription already exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving prescription: {ex}");
                throw;
            }
        }

        private static void UpdatePatientLastVisit(int id, DateTime visit)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var updateCommand = new SQLiteCommand("UPDATE Patient SET LastVisit = @LastVisit WHERE ID = @ID;", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@LastVisit", visit);
                        updateCommand.Parameters.AddWithValue("@ID", id);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Update last visit");
            }
        }


         private static Int64 GeneratePrescriptionId(int id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Check if there are any existing prescription IDs matching the pattern id + ddmmyyyy
                    using (var checkCommand = new SQLiteCommand("SELECT PRESCRIPTIONID FROM PatientVisit WHERE PRESCRIPTIONID LIKE @Pattern;", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Pattern", $"{id}{DateTime.Today.ToString("ddMMyyyy")}%");
                        var reader = checkCommand.ExecuteReader();

                        // If no matches found, assign id + ddmmyyyy + 1 as the prescription ID
                        if (!reader.HasRows)
                        {
                            return Int64.Parse($"{id}{DateTime.Today.ToString("ddMMyyyy")}1");
                        }
                        else
                        {
                            // If matches found, extract the last digit and increment it by 1
                            List<long> ids = new List<long>();
                            while (reader.Read())
                            {
                                Int64 existingPrescriptionId = reader.GetInt64(0);
                                Int64 lastDigit = existingPrescriptionId % 10;
                                ids.Add(lastDigit);
                            }
                            long maxDigit = ids.Max();
                            long nextDigit = maxDigit + 1;

                            // Assign id + ddmmyyyy + (digit + 1) as the prescription ID
                            return Int64.Parse($"{id}{DateTime.Today.ToString("ddMMyyyy")}{nextDigit}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating prescription ID: {ex}");
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

    }

    }



