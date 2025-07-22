using System;
using System.Data;
using Microsoft.Data.Sqlite;
using TShockAPI;

namespace Permabuffs
{
    public class DB
    {
        private static string connectionString = "Data Source=permabuffs.sqlite;";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Permabuffs (
                    UUID TEXT PRIMARY KEY,
                    Enabled INTEGER NOT NULL
                );";
            command.ExecuteNonQuery();
        }

        public static void SetEnabled(string uuid, bool enabled)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Permabuffs (UUID, Enabled)
                VALUES ($uuid, $enabled)
                ON CONFLICT(UUID) DO UPDATE SET Enabled = $enabled;";
            command.Parameters.AddWithValue("$uuid", uuid);
            command.Parameters.AddWithValue("$enabled", enabled ? 1 : 0);
            command.ExecuteNonQuery();
        }

        public static bool GetEnabled(string uuid)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Enabled FROM Permabuffs WHERE UUID = $uuid;";
            command.Parameters.AddWithValue("$uuid", uuid);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetInt32(0) == 1;
            }

            return false;
        }
    }
}