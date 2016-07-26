using System;
using Android.App;
using Android.Widget;
using Alkometer.Shared;
using System.Collections.Generic;

namespace Alkometer.ApplicationLayer
{
    public class DrinkItemListAdapter : BaseAdapter<DrinkItem>
    {
        Activity context = null;
        IList<DrinkItem> Drinks = new List<DrinkItem>();
		Dictionary<int, int> iconIdToResource;

		void initIconIdToResource()
		{
			iconIdToResource.Add(0, Resource.Drawable.beerglass);
			iconIdToResource.Add(1, Resource.Drawable.beerbottle);
			iconIdToResource.Add(2, Resource.Drawable.shampoonbottle);
			iconIdToResource.Add(3, Resource.Drawable.wineglass);

			iconIdToResource.Add(4, Resource.Drawable.beerkek);
			iconIdToResource.Add(5, Resource.Drawable.bloodmary);
			iconIdToResource.Add(6, Resource.Drawable.drink);
			iconIdToResource.Add(7, Resource.Drawable.fruit);

			iconIdToResource.Add(8, Resource.Drawable.glass);
			iconIdToResource.Add(9, Resource.Drawable.konjakglass);
			iconIdToResource.Add(10, Resource.Drawable.pint);
			iconIdToResource.Add(11, Resource.Drawable.shampoonbottle);
		}
		
        public DrinkItemListAdapter(Activity context, IList<DrinkItem> Drinks) : base()
        {
            this.context = context;
            this.Drinks = Drinks;
			iconIdToResource = new Dictionary<int, int>();
			initIconIdToResource();
        }
		
        public override DrinkItem this[int position]
        {
            get { return Drinks[position]; }
        }
		
        public override long GetItemId(int position)
        {
            return position;
        }
		
        public override int Count
        {
            get { return Drinks.Count; }
        }
		
        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var item = Drinks[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(
							Android.Resource.Layout.ActivityListItem, parent, false));
			// Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;
			var text = String.Format("{0}  -  {1} %, {2} L", item.Name,
									 Math.Round(item.AlcoholByVolume, 2, MidpointRounding.AwayFromZero),
									 Math.Round(item.Volume, 2, MidpointRounding.AwayFromZero));
			var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			textView.Text = text;
			textView.SetMinHeight(100);
			//view.SetText(text, TextView.BufferType.Normal);
			view.SetMinimumHeight(100);
			view.FindViewById<ImageView>(Android.Resource.Id.Icon).SetImageResource(iconIdToResource[item.IconId]);
            return view;
        }
    }
}
