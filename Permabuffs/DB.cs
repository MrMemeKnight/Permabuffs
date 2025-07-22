using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using TShockAPI;

namespace Permabuffs
{
    public class DB
    {
        private readonly string connectionString;
        private SqliteConnection db;

        public DB()
        {
            string path = Path.Combine(TShock.SavePath, "Permabuffs.sqlite");
            connectionString = $"URI=file:{path}";
            db = new SqliteConnection(connectionString);
            db.Open();
            CreatePermabuffTable();
        }

        // ✅ Constructor with 1 argument added for compatibility
        public DB(string path)
        {
            connectionString = $"URI=file:{path}";
            db = new SqliteConnection(connectionString);
            db.Open();
            CreatePermabuffTable();
        }

        private void CreatePermabuffTable()
        {
            using var cmd = new SqliteCommand("CREATE TABLE IF NOT EXISTS permabuffs (UserID INTEGER NOT NULL, BuffID INTEGER NOT NULL, PRIMARY KEY(UserID, BuffID));", db);
            cmd.ExecuteNonQuery();
        }

        public List<int> GetPermabuffs(int userId)
        {
            var list = new List<int>();
            using var cmd = new SqliteCommand("SELECT BuffID FROM permabuffs WHERE UserID=@0", db);
            cmd.Parameters.AddWithValue("@0", userId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            return list;
        }

        public void AddPermabuff(int userId, int buffId)
        {
            using var cmd = new SqliteCommand("INSERT OR IGNORE INTO permabuffs (UserID, BuffID) VALUES (@0, @1);", db);
            cmd.Parameters.AddWithValue("@0", userId);
            cmd.Parameters.AddWithValue("@1", buffId);
            cmd.ExecuteNonQuery();
        }

        public void RemovePermabuff(int userId, int buffId)
        {
            using var cmd = new SqliteCommand("DELETE FROM permabuffs WHERE UserID=@0 AND BuffID=@1;", db);
            cmd.Parameters.AddWithValue("@0", userId);
            cmd.Parameters.AddWithValue("@1", buffId);
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            db.Close();
            db.Dispose();
        }
    }
}