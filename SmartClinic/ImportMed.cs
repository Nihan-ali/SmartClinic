using System;
using System.Threading;
using System.Data.SQLite;
using System.IO;

namespace SmartClinic
{
    public class ImportMed
    {
        private const string DatabaseFileName = "patientinfo.db";
        private static readonly string ConnectionString = $"Data Source={DatabaseFileName};Version=3;";

        static ImportMed()
        {
        }

        public static void Import()
        {
            ImportFile("hello.txt");
        }

        private static void SleepInSeconds(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        private static void ImportFile(string fileName)
        {
            string databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DatabaseFileName);
            string queriesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    if (File.Exists(queriesFilePath))
                    {
                        string[] queries = File.ReadAllLines(queriesFilePath);

                        foreach (string query in queries)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                        Console.WriteLine($"Data from {fileName} imported successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Insert queries file {fileName} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing data: {ex.Message}");
            }
        }
    }
}
