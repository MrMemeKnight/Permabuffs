using System.Collections.Generic;
using System.Data;
using TShockAPI;
using Mono.Data.Sqlite;

namespace Permabuffs
{
	public class DB
	{
		private static string dbPath = TShock.SavePath + "/permabuffs.sqlite";
		private static SqliteConnection db;

		public static void Connect()
		{
			db = new SqliteConnection($"URI=file:{dbPath}");
			db.Open();

			using var cmd = db.CreateCommand();
			cmd.CommandText =
				@"CREATE TABLE IF NOT EXISTS permabuffs (
					UserID INTEGER NOT NULL,
					BuffID INTEGER NOT NULL
				);";
			cmd.ExecuteNonQuery();
		}

		public static void CreatePlayer(int userId)
		{
			// No-op: nothing to pre-insert
		}

		public static List<int> GetBuffs(int userId)
		{
			var result = new List<int>();

			using var cmd = db.CreateCommand();
			cmd.CommandText = "SELECT BuffID FROM permabuffs WHERE UserID=@0";
			cmd.Parameters.AddWithValue("@0", userId);

			using var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				result.Add(reader.GetInt32(0));
			}
			return result;
		}

		public static void AddBuff(int userId, int buffId)
		{
			using var cmd = db.CreateCommand();
			cmd.CommandText = "INSERT INTO permabuffs (UserID, BuffID) VALUES (@0, @1)";
			cmd.Parameters.AddWithValue("@0", userId);
			cmd.Parameters.AddWithValue("@1", buffId);
			cmd.ExecuteNonQuery();
		}

		public static void RemoveBuff(int userId, int buffId)
		{
			using var cmd = db.CreateCommand();
			cmd.CommandText = "DELETE FROM permabuffs WHERE UserID=@0 AND BuffID=@1";
			cmd.Parameters.AddWithValue("@0", userId);
			cmd.Parameters.AddWithValue("@1", buffId);
			cmd.ExecuteNonQuery();
		}
	}
}