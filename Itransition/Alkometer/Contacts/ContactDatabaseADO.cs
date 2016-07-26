using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;

namespace Alkometer.Shared
{
	public class ContactDatabase 
	{
		static object locker = new object ();
		public SqliteConnection connection;
		public string path;

		public ContactDatabase (string dbPath) 
		{
			path = dbPath;
			// create the tables
			bool exists = File.Exists (dbPath);

			if (!exists) {
				connection = new SqliteConnection ("Data Source=" + dbPath);

				connection.Open ();
				var commands = new[] {
					"CREATE TABLE [Items] (_id INTEGER PRIMARY KEY ASC, Name NTEXT, Surname NTEXT, Number NTEXT, Path NTEXT);"
				};
				foreach (var command in commands) {
					using (var c = connection.CreateCommand ()) {
						c.CommandText = command;
						c.ExecuteNonQuery ();
					}
				}
			}
		}

		/// <summary>Convert from DataReader to Task object</summary>
		ContactItem FromReader (SqliteDataReader r) 
		{
			var t = new ContactItem();
			t.ID = Convert.ToInt32(r ["_id"]);
			t.Name = r ["Name"].ToString();
			t.Surname = r["Surname"].ToString();
			t.Number = r ["Number"].ToString();
			t.Path = r["Path"].ToString();
			return t;
		}

		public IEnumerable<ContactItem> GetItems ()
		{
			var tl = new List<ContactItem> ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ())
				{
					contents.CommandText = "SELECT [_id], [Name], [Surname], [Number], [Path] from [Items]";
					var r = contents.ExecuteReader ();
					while (r.Read ())
					{
						tl.Add (FromReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}

		public ContactItem GetItem (int id) 
		{
			var t = new ContactItem ();
			lock (locker)
			{
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ())
				{
					command.CommandText = "SELECT [_id], [Name], [Surname], [Number], [Path] from [Items] WHERE [_id] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ())
					{
						t = FromReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

		public int SaveItem (ContactItem item) 
		{
			int r;
			lock (locker)
			{
				if (item.ID != 0)
				{
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) 
					{
						command.CommandText = "UPDATE [Items] SET [Name] = ?, [Surname] = ?, [Number] = ?, [Path] = ? WHERE [_id] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Surname });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Number });
						command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Path });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.ID });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} 
				else
				{
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ())
					{
						command.CommandText = "INSERT INTO [Items] ([Name], [Surname], [Number], [Path]) VALUES (? ,?, ?, ?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Surname });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Number });
						command.Parameters.Add (new SqliteParameter(DbType.String) { Value = item.Path });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}

		public int DeleteItem(int id) 
		{
			lock (locker)
			{
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ())
				{
					command.CommandText = "DELETE FROM [Items] WHERE [_id] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}
	}
}