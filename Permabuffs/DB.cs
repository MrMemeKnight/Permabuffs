using System;
using System.Collections.Generic;
using System.Data;
using TShockAPI;
using TShockAPI.DB;

namespace Permabuffs
{
    public class DB
    {
        private static IDbConnection db;

        public static void Initialize()
        {
            db = TShock.DB;
        }

        public static void LoadPlayerBuffs(int playerID, List<int> buffs)
        {
            using var reader = db.QueryReader("SELECT BuffID FROM Permabuffs WHERE PlayerID=@0", playerID);
            while (reader.Read())
            {
                buffs.Add(reader.Get<int>("BuffID"));
            }
        }

        public static void SavePlayerBuffs(int playerID, List<int> buffs)
        {
            db.Query("DELETE FROM Permabuffs WHERE PlayerID=@0", playerID);
            foreach (int buffID in buffs)
            {
                db.Query("INSERT INTO Permabuffs (PlayerID, BuffID) VALUES (@0, @1)", playerID, buffID);
            }
        }
    }
}