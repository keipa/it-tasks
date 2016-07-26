using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;

namespace Alkometer.Shared
{
    public class DrinkDatabase
    {
		public string path;
        public SqliteConnection connection;
		static object locker = new object();

		void initializeValues()
        {
            SaveItem(new DrinkItem("Glass of beer", 0.3, 0.3, "Glass of beer 0.3L", 0));
            SaveItem(new DrinkItem("Bottle of beer", 0.2, 0.35, "Bottle of beer 0.1L", 1));
            SaveItem(new DrinkItem("Champagne glass", 0.2, 0.12, "Champagne glass 2L", 2));
            SaveItem(new DrinkItem("Wine glass", 0.2, 0.14, "Wine glass 0.2L", 3));

            SaveItem(new DrinkItem("Beer keg", 3, 0.5, "Beer keg 3L", 4));
            SaveItem(new DrinkItem("Negroni", 0.2, 0.3, "Negroni 02L", 5));
            SaveItem(new DrinkItem("Caipirinha", 02, 0.2, "Caipirinha 0.2L", 6));
            SaveItem(new DrinkItem("Blood Mary", 0.2, 0.5, "Blood Mary 0.2L", 7));

            SaveItem(new DrinkItem("Rom with Cola", 0.2, 0.4, "Rom with Cola 0.2L", 8));
            SaveItem(new DrinkItem("Konjak", 0.3, 0.5, "Konjak 0.3L", 9));
            SaveItem(new DrinkItem("Ale", 0.7, 0.5, "Ale 0.7L", 10));
            SaveItem(new DrinkItem("Wine bottle", 0.2, 0.17, "Wine bottle 0.2L", 11));
        }

        public DrinkDatabase(string dbPath)
        {
            path = dbPath;
            // create the tables
            bool exists = File.Exists(dbPath);

            if (!exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);

                connection.Open();
                var commands = new[] {
                    "CREATE TABLE [Items] (_id INTEGER PRIMARY KEY ASC, Name NTEXT, About NTEXT, Volume REAL, AlcoholByVolume REAL, IconNumber INTEGER);"
                };
                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        c.ExecuteNonQuery();
                    }
                }

				initializeValues();
            }
        }

        DrinkItem FromReader(SqliteDataReader r)
        {
            var t = new DrinkItem();
            t.ID = Convert.ToInt32(r["_id"]);
            t.Name = r["Name"].ToString();
            t.AlcoholByVolume = Convert.ToDouble(r["AlcoholByVolume"]);
            t.Volume = Convert.ToDouble(r["Volume"]);
            t.IconId = Convert.ToInt32(r["IconNumber"]);
            t.About = r["About"].ToString();
            return t;
        }

        public IEnumerable<DrinkItem> GetItems()
        {
            var tl = new List<DrinkItem>();

            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT [_id], [Name], [About], [Volume], [AlcoholByVolume], [IconNumber] from [Items]";
                    var r = contents.ExecuteReader();
                    while (r.Read())
                    {
                        tl.Add(FromReader(r));
                    }
                }
                connection.Close();
            }
            return tl;
        }

        public DrinkItem GetItem(int id)
        {
            var t = new DrinkItem();
            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // "CREATE TABLE [Items] (_id INTEGER PRIMARY KEY ASC, Name NTEXT, 
                    // About NTEXT, Volume REAL, AlcoholByVolume REAL, IconNumber INTEGER);"
                    command.CommandText = "SELECT [_id], [Name], [About], [Volume], [AlcoholByVolume], [IconNumber] from [Items] WHERE [_id] = ?";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    var r = command.ExecuteReader();
                    while (r.Read())
                    {
                        t = FromReader(r);
                        break;
                    }
                }
                connection.Close();
            }
            return t;
        }

        public int SaveItem(DrinkItem item)
        {
            int r;
            lock (locker)
            {
                if (item.ID != 0)
                {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        // SELECT [_id], [Name], [About], [Volume], [AlcoholByVolume], [IconNumber]
                        command.CommandText = "UPDATE [Items] SET [Name] = ?, [About] = ?, [Volume] = ?, [AlcoholByVolume] = ?, [IconNumber] = ? WHERE [_id] = ?;";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Name });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.About });
                        command.Parameters.Add(new SqliteParameter(DbType.Double) { Value = item.Volume });
                        command.Parameters.Add(new SqliteParameter(DbType.Double) { Value = item.AlcoholByVolume });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.IconId });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }
                else
                {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Items] ( [Name], [About], [Volume], [AlcoholByVolume], [IconNumber]) VALUES (? ,?, ?, ?, ?)";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Name });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.About });
                        command.Parameters.Add(new SqliteParameter(DbType.Double) { Value = item.Volume });
                        command.Parameters.Add(new SqliteParameter(DbType.Double) { Value = item.AlcoholByVolume });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.IconId });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }

            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                int r;
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Items] WHERE [_id] = ?;";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    r = command.ExecuteNonQuery();
                }
                connection.Close();
                return r;
            }
        }
    }
}