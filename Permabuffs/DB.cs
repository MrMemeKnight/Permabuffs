using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using Terraria;
using TShockAPI;

namespace Permabuffs
{
    public class DB : IDisposable
    {
        private readonly SqliteConnection db;

        public DB()
        {
            string path = Path.Combine(TShock.SavePath, "Permabuffs.sqlite");
            db = new SqliteConnection($"Data Source={path}");
            db.Open();
            CreatePermabuffTable();
        }

        private void CreatePermabuffTable()
        {
            using var cmd = db.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS permabuffs (UserID INTEGER NOT NULL, BuffID INTEGER NOT NULL, PRIMARY KEY(UserID, BuffID));";
            cmd.ExecuteNonQuery();
        }

        public List<int> GetPermabuffs(int userId)
        {
            var list = new List<int>();
            using var cmd = db.CreateCommand();
            cmd.CommandText = "SELECT BuffID FROM permabuffs WHERE UserID=$uid;";
            cmd.Parameters.AddWithValue("$uid", userId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(reader.GetInt32(0));
            return list;
        }

        public void AddPermabuff(int userId, int buffId)
        {
            using var cmd = db.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO permabuffs (UserID, BuffID) VALUES ($uid, $bid);";
            cmd.Parameters.AddWithValue("$uid", userId);
            cmd.Parameters.AddWithValue("$bid", buffId);
            cmd.ExecuteNonQuery();
        }

        public void RemovePermabuff(int userId, int buffId)
        {
            using var cmd = db.CreateCommand();
            cmd.CommandText = "DELETE FROM permabuffs WHERE UserID=$uid AND BuffID=$bid;";
            cmd.Parameters.AddWithValue("$uid", userId);
            cmd.Parameters.AddWithValue("$bid", buffId);
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            db.Close();
            db.Dispose();
        }
    }
}