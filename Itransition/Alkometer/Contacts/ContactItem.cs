using System;

namespace Alkometer.Shared 
{
	/// <summary>
	/// Todo Item business object
	/// </summary>
	public class ContactItem 
	{
		int id;
		string name;
		string surname;
		string number;
		string path;

		public ContactItem()
		{
			id = 0;
			name = "";
			surname = "";
			number = "";
			path = "";	
		}

		public int ID { get { return id; } set { id = value; } }
		public string Name { get { return name; } set { name = value; } }
		public string Surname { get; set; }
		public string Number { get; set; }
		public string Path { get { return path; } set { path = value; } }
	}
}