using System;
using System.Collections.Generic;
using System.IO;

namespace Alkometer.Shared 
{
	public class ContactItemRepositoryADO 
	{
		ContactDatabase db = null;
		protected static string dbLocation;		
		protected static ContactItemRepositoryADO me;		

		static ContactItemRepositoryADO ()
		{
			me = new ContactItemRepositoryADO();
		}

		protected ContactItemRepositoryADO ()
		{
			dbLocation = DatabaseFilePath;
			db = new ContactDatabase(dbLocation);
		}

		public static string DatabaseFilePath 
		{
			get 
			{ 
				var sqliteFilename = "ContactsDatabase.db3";
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				var path = Path.Combine (libraryPath, sqliteFilename);
				return path;	
			}
		}

		public static ContactItem GetTask(int id)
		{
			return me.db.GetItem(id);
		}

		public static IEnumerable<ContactItem> GetTasks ()
		{
			return me.db.GetItems();
		}

		public static int SaveTask (ContactItem item)
		{
			return me.db.SaveItem(item);
		}

		public static int DeleteTask(int id)
		{
			return me.db.DeleteItem(id);
		}
	}
}

