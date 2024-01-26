// DatabaseHelper.cs
using System;
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
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS PatientInfo (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER);", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Database file created at: {databaseFilePath}");
            }
            else
            {
                Console.WriteLine($"Database file already exists at: {databaseFilePath}");
            }
        }

        public static void InsertPatient(string name, int age)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("INSERT INTO PatientInfo (Name, Age) VALUES (@Name, @Age);", connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Patient '{name}' inserted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting patient: {ex}");
                throw;
            }
        }
    }
}
