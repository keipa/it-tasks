using System.Collections.Generic;

namespace Alkometer.Shared 
{
	public static class ContactManager 
	{
		static ContactManager ()
		{
		}
		
		public static ContactItem GetContact(int id)
		{
			return ContactItemRepositoryADO.GetTask(id);
		}
		
		public static IList<ContactItem> GetContacts ()
		{
			return new List<ContactItem>(ContactItemRepositoryADO.GetTasks());
		}
		
		public static int SaveContact (ContactItem item)
		{
			return ContactItemRepositoryADO.SaveTask(item);
		}
		
		public static int DeleteContact(int id)
		{
			return ContactItemRepositoryADO.DeleteTask(id);
		}
	}
}