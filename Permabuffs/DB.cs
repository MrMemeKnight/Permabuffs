using System.Data;
using TShockAPI.DB;

namespace Permabuffs
{
    public class DB
    {
        private IDbConnection db;

        public DB()
        {
            db = TShock.DB;
            var table = new SqlTable("Permabuffs",
                new SqlColumn("UserID", MySql.Data.MySqlClient.MySqlDbType.Int32) { Primary = true },
                new SqlColumn("Enabled", MySql.Data.MySqlClient.MySqlDbType.Int32));
            var creator = new SqlTableCreator(db,
                db.GetSqlType() == SqlType.Sqlite
                    ? new SqliteQueryCreator(db, TShock.Config.Settings.TablePrefix)
                    : new MysqlQueryCreator(db, TShock.Config.Settings.TablePrefix));
            creator.EnsureTableStructure(table);
        }

        public bool PlayerExists(int userId)
        {
            using var reader = db.QueryReader("SELECT * FROM Permabuffs WHERE UserID = @0", userId);
            return reader.Read();
        }

        public void AddPlayer(int userId)
        {
            db.Query("INSERT INTO Permabuffs (UserID, Enabled) VALUES (@0, @1)", userId, 0);
        }

        public bool IsEnabled(int userId)
        {
            using var reader = db.QueryReader("SELECT Enabled FROM Permabuffs WHERE UserID = @0", userId);
            if (reader.Read())
                return reader.Get<int>("Enabled") == 1;
            return false;
        }

        public void SetEnabled(int userId, bool enabled)
        {
            db.Query("UPDATE Permabuffs SET Enabled = @0 WHERE UserID = @1", enabled ? 1 : 0, userId);
        }
    }
}