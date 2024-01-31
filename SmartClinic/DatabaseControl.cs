using System.Data;
using System.Data.SQLite;
using System.IO;

public static class DatabaseControl
{
    private const string DatabaseFileName = "D:\\AcademicProject\\SmartClinic\\datastorage.sqlite";
    private const string ConnectionString = "Data Source=D:\\AcademicProject\\SmartClinic\\datastorage.sqlite;";

    public static void CreateDatabase()
    {
        //if (!File.Exists(DatabaseFileName))
        //{
        SQLiteConnection.CreateFile(DatabaseFileName);
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            // Create a sample table    
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS PatientInfo (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Age TEXT,
                    Phone TEXT,
                    Address TEXT,
                    BloodGroup TEXT
                )";

            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        //}
    }
    public static void InsertData(string name, string age, string phone,string address,string bloodgroup)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string insertDataQuery = "INSERT INTO SampleTable (Name, Age,Phone,Address,BloodGroup) VALUES (@Name, @Age, @Phone,@Address,@BloodGroup)";

            using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@BloodGroup",bloodgroup);

                    command.ExecuteNonQuery();

            }
        }
    }

    public static DataTable GetData()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string getDataQuery = "SELECT * FROM PatientInfo";

            using (SQLiteCommand command = new SQLiteCommand(getDataQuery, connection))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
    }


}

