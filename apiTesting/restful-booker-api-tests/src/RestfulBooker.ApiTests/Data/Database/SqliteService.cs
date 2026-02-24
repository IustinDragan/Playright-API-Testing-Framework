using Microsoft.Data.Sqlite;

namespace RestfulBooker.ApiTests.Data
{
      /// <summary>
      /// SQLite database service for persisting booking API responses.
      /// Used for test data storage and verification of API response persistence.
      /// </summary>
      public class SqliteService
      {
            private readonly string _connectionString;

            /// <summary>
            /// Initialize SQLite service with database file path.
            /// </summary>
            /// <param name="dbPath">Full or relative path to the SQLite database file</param>
            public SqliteService(string dbPath)
            {
                  _connectionString = $"Data Source={dbPath}";
            }

            /// <summary>
            /// Creates the Bookings table if it doesn't exist.
            /// Called once during test initialization to ensure schema is ready.
            /// </summary>
            public void Initialize()
            {
                  using var connection = new SqliteConnection(_connectionString);
                  connection.Open();

                  var cmd = connection.CreateCommand();
                  cmd.CommandText =
                  @"CREATE TABLE IF NOT EXISTS Bookings(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Response TEXT
        );";
                  cmd.ExecuteNonQuery();
            }

            /// <summary>
            /// Inserts a booking API response JSON string into the database.
            /// Used to persist booking responses for later verification.
            /// </summary>
            /// <param name="response">JSON string of the API booking response</param>
            public void Insert(string response)
            {
                  using var connection = new SqliteConnection(_connectionString);
                  connection.Open();

                  var cmd = connection.CreateCommand();
                  cmd.CommandText = "INSERT INTO Bookings(Response) VALUES ($response)";
                  cmd.Parameters.AddWithValue("$response", response);

                  cmd.ExecuteNonQuery();
            }

            /// <summary>
            /// Checks if a specific booking response exists in the database.
            /// Used for assertions to verify that API responses have been persisted correctly.
            /// </summary>
            /// <param name="response">JSON string to search for in the database</param>
            /// <returns>True if the response exists in the Bookings table, false otherwise</returns>
            public bool ContainsResponse(string response)
            {
                  using var connection = new SqliteConnection(_connectionString);
                  connection.Open();

                  var cmd = connection.CreateCommand();
                  cmd.CommandText = "SELECT COUNT(1) FROM Bookings WHERE Response = $response";
                  cmd.Parameters.AddWithValue("$response", response);

                  var result = cmd.ExecuteScalar();
                  if (result == null) return false;

                  try
                  {
                        var count = Convert.ToInt32(result);
                        return count > 0;
                  }
                  catch
                  {
                        return false;
                  }
            }
      }
}