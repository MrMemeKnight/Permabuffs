using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using TShockAPI;

namespace Permabuffs
{
    public static class DB
    {
        public static Dictionary<int, bool> ToggleStatus = new Dictionary<int, bool>();

        public static bool GetStatus(int userID)
        {
            using (var db = new SqliteConnection($"uri=file={TShock.SavePath}/permabuffs.sqlite"))
            {
                db.Open();
                var cmd = db.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS PermabuffStatus (UserID INTEGER PRIMARY KEY, Enabled BOOLEAN);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT Enabled FROM PermabuffStatus WHERE UserID = @0;";
                cmd.Parameters.AddWithValue("@0", userID);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(0);
                    }
                }

                // Default disabled
                return false;
            }
        }

        public static void SetStatus(int userID, bool enabled)
        {
            using (var db = new SqliteConnection($"uri=file={TShock.SavePath}/permabuffs.sqlite"))
            {
                db.Open();
                var cmd = db.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS PermabuffStatus (UserID INTEGER PRIMARY KEY, Enabled BOOLEAN);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT OR REPLACE INTO PermabuffStatus (UserID, Enabled) VALUES (@0, @1);";
                cmd.Parameters.AddWithValue("@0", userID);
                cmd.Parameters.AddWithValue("@1", enabled);
                cmd.ExecuteNonQuery();
            }
        }
    }
}