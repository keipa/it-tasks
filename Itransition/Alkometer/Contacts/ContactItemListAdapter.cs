using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Alkometer.Shared;

namespace Alkometer.ApplicationLayer 
{
	/// <summary>
	/// Adapter that presents Tasks in a row-view
	/// </summary>
	public class ContactItemListAdapter : BaseAdapter<ContactItem> 
	{
		Activity context = null;
		IList<ContactItem> contacts = new List<ContactItem>();
		
		public ContactItemListAdapter (Activity context, IList<ContactItem> contacts) : base ()
		{
			this.context = context;
			this.contacts = contacts;
		}
		
		public override ContactItem this[int position]
		{
			get { return contacts[position]; }
		}
		
		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override int Count
		{
			get { return contacts.Count; }
		}
		
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get our object for position
			var item = contacts[position];
			var view = (convertView ??
				context.LayoutInflater.Inflate(
							Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;
			view.SetText (item.Name == "" ? "<New Contact>" : item.Name + "      " + item.Surname, TextView.BufferType.Normal);

			return view;
		}
	}
}