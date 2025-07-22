using System;
using System.Data;
using TShockAPI.DB;

namespace Permabuffs
{
    public class DB
    {
        private readonly IDbConnection _db;

        public DB(IDbConnection db)
        {
            _db = db;

            var table = new SqlTable("Permabuffs",
                new SqlColumn("UserID", MySqlDbType.Int32) { Primary = true },
                new SqlColumn("Enabled", MySqlDbType.Int32)
            );

            var creator = new SqlTableCreator(_db,
                db.GetSqlType() == SqlType.Sqlite
                    ? new SqliteQueryCreator()
                    : new MysqlQueryCreator());

            creator.EnsureTableStructure(table);
        }

        public void SetState(int userId, bool enabled)
        {
            var exists = _db.QueryReader("SELECT * FROM Permabuffs WHERE UserID = @0", userId).Read();

            if (exists)
            {
                _db.Query("UPDATE Permabuffs SET Enabled = @0 WHERE UserID = @1", enabled ? 1 : 0, userId);
            }
            else
            {
                _db.Query("INSERT INTO Permabuffs (UserID, Enabled) VALUES (@0, @1)", userId, enabled ? 1 : 0);
            }
        }

        public bool GetState(int userId)
        {
            using var reader = _db.QueryReader("SELECT Enabled FROM Permabuffs WHERE UserID = @0", userId);
            if (reader.Read())
            {
                return reader.Get<int>("Enabled") == 1;
            }

            return false;
        }
    }
}