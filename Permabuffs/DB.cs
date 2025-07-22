using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TShockAPI;
using Mono.Data.Sqlite;

namespace Permabuffs
{
	public class DB
	{
		private static string path = Path.Combine(TShock.SavePath, "permabuffs.sqlite");

		private static SqliteConnection db;

		public static void Connect()
		{
			try
			{
				bool newdb = !File.Exists(path);

				db = new SqliteConnection("URI=file:" + path + ",Version=3");
				db.Open();

				if (newdb)
				{
					using (var cmd = new SqliteCommand(db))
					{
						cmd.CommandText = "CREATE TABLE IF NOT EXISTS permabuffs (UserID INTEGER NOT NULL, BuffID INTEGER NOT NULL);";
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				TShock.Log.ConsoleError($"[Permabuffs] DB Connection failed: {ex}");
			}
		}

		public static void CreatePlayer(int userID)
		{
			// Intentionally left blank; this DB version does not store users by default
		}

		public static List<int> GetBuffs(int userID)
		{
			List<int> buffs = new();

			using (var cmd = new SqliteCommand("SELECT BuffID FROM permabuffs WHERE UserID=@0", db))
			{
				cmd.Parameters.AddWithValue("@0", userID);

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						buffs.Add(reader.GetInt32(0));
					}
				}
			}
			return buffs;
		}

		public static void AddBuff(int userID, int buffID)
		{
			using (var cmd = new SqliteCommand("INSERT INTO permabuffs (UserID, BuffID) VALUES (@0, @1)", db))
			{
				cmd.Parameters.AddWithValue("@0", userID);
				cmd.Parameters.AddWithValue("@1", buffID);
				cmd.ExecuteNonQuery();
			}
		}

		public static void RemoveBuff(int userID, int buffID)
		{
			using (var cmd = new SqliteCommand("DELETE FROM permabuffs WHERE UserID=@0 AND BuffID=@1", db))
			{
				cmd.Parameters.AddWithValue("@0", userID);
				cmd.Parameters.AddWithValue("@1", buffID);
				cmd.ExecuteNonQuery();
			}
		}
	}
}