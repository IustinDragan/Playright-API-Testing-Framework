using Microsoft.Data.Sqlite;

namespace RestfulBooker.ApiTests.Data;

public class SqliteService
{
      private readonly string _connectionString;


      public SqliteService(string dbPath)
      {
            _connectionString = $"Data source={dbPath}";
      }

      public void Initialize()
      {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS Bookings(
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Response TEXT)
            ;";

            cmd.ExecuteNonQuery();
      }

      public void Insert(string response)
      {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Bookings(Response) VALUES ($response)";
            cmd.Parameters.AddWithValue("$response", response);

            cmd.ExecuteNonQuery();

      }
}