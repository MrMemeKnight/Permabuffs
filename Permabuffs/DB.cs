using System;
using System.Collections.Generic;
using System.Data;
using TShockAPI.DB;

namespace Permabuffs
{
    public class DB
    {
        private IDbConnection _db;

        public DB(IDbConnection db)
        {
            _db = db;

            var table = new SqlTable("Permabuffs",
                new SqlColumn("UserID", DbType.Int32) { Primary = true },
                new SqlColumn("Enabled", DbType.Int32)
            );

            var creator = new SqlTableCreator(_db,
                _db.GetSqlType() == SqlType.Sqlite
                    ? new SqliteQueryCreator(_db)
                    : new MysqlQueryCreator(_db));

            creator.EnsureTableStructure(table);
        }

        public bool IsEnabled(int userId)
        {
            using var reader = _db.QueryReader("SELECT Enabled FROM Permabuffs WHERE UserID = @0", userId);
            if (reader.Read())
                return reader.Get<int>("Enabled") == 1;

            return false;
        }

        public void SetEnabled(int userId, bool enabled)
        {
            int val = enabled ? 1 : 0;
            if (HasUser(userId))
                _db.Query("UPDATE Permabuffs SET Enabled = @0 WHERE UserID = @1", val, userId);
            else
                _db.Query("INSERT INTO Permabuffs (UserID, Enabled) VALUES (@0, @1)", userId, val);
        }

        private bool HasUser(int userId)
        {
            using var reader = _db.QueryReader("SELECT 1 FROM Permabuffs WHERE UserID = @0", userId);
            return reader.Read();
        }
    }
}