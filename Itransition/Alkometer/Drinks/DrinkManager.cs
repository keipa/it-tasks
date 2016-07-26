using System.Collections.Generic;

namespace Alkometer.Shared 
{
	public static class DrinkManager 
	{
		static DrinkManager () { }
		
		public static DrinkItem GetDrink(int id)
		{
			return DrinkItemRepositoryADO.GetDrinks(id);
		}
		
		public static IList<DrinkItem> GetDrinks ()
		{
			return new List<DrinkItem>(DrinkItemRepositoryADO.GetDrinks());
		}
		
		public static int SaveDrink (DrinkItem item)
		{
			return DrinkItemRepositoryADO.SaveDrink(item);
		}
		
		public static int DeleteDrink(int id)
		{
			return DrinkItemRepositoryADO.DeleteDrink(id);
		}
	}
}