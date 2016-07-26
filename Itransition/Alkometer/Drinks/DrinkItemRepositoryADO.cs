using System;
using System.Collections.Generic;
using System.IO;

namespace Alkometer.Shared
{
	public class DrinkItemRepositoryADO 
	{
		DrinkDatabase db = null;
		protected static string dbLocation;		
		protected static DrinkItemRepositoryADO me;		

		static DrinkItemRepositoryADO ()
		{
			me = new DrinkItemRepositoryADO();
		}

		protected DrinkItemRepositoryADO ()
		{
			dbLocation = DatabaseFilePath;
			db = new DrinkDatabase(dbLocation);
		}

		public static string DatabaseFilePath 
		{
			get 
			{ 
				var sqliteFilename = "DrinksDatabase.db3";
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				var path = Path.Combine (libraryPath, sqliteFilename);
				return path;	
			}
		}

		public static DrinkItem GetDrinks(int id)
		{
			return me.db.GetItem(id);
		}

		public static IEnumerable<DrinkItem> GetDrinks ()
		{
			return me.db.GetItems();
		}

		public static int SaveDrink (DrinkItem item)
		{
			return me.db.SaveItem(item);
		}

		public static int DeleteDrink(int id)
		{
			return me.db.DeleteItem(id);
		}
	}
}

