using System;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Alkometer.Screen;
using Alkometer.Shared;
using Alkometer.ApplicationLayer;
using System.Collections.Generic;

namespace Alkometer.Activities
{

    class BaseTabFragment : Fragment
    {
        public int IconId = Resource.Drawable.icon;
    }

    class ContactsTabFragment : BaseTabFragment
    {
        Button addButton;
        ListView contactListView;
        IList<ContactItem> contacts;
        ContactItemListAdapter contactList;

        public ContactsTabFragment() : base()
        {
            IconId = Resource.Drawable.ic_camera;
        }

        void setupListView(View view)
        {
            contactListView = view.FindViewById<ListView>(Resource.Id.TaskList);
            if (contactListView != null)
            {
                contactListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    var contactsDetails = new Intent(Activity, typeof(NewContactEditScreen));
                    contactsDetails.PutExtra("ContactID", contacts[e.Position].ID);
                    StartActivity(contactsDetails);
                };
            }
        }

        void setupButton(View view)
        {
            addButton = (Button)view.FindViewById<TextView>(Resource.Id.AddButton);
            addButton.Click += (sender, e) =>
            {
                var newContact = new Intent(Activity, typeof(NewContactTakePhotoScreen));
                StartActivity(newContact);
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ContactsLayout, container, false);
            setupButton(view);
            setupListView(view);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            contacts = ContactManager.GetContacts();
            contactList = new ContactItemListAdapter(Activity, contacts);
            contactListView.Adapter = contactList;
        }
    }

    class MeterSpirt : BaseTabFragment
    {
        int bodyMass;
        double curAlcoVolume;
        double maxAlcoProgress;
        double curAlcoProgress;

		TextView weightText;
		SeekBar weightSeekBar;
        ProgressBar progressBar;

        ListView drinksListView;
        IList<DrinkItem> drinks;
        DrinkItemListAdapter drinksList;

        public MeterSpirt() : base()
        {
			bodyMass = 50;
            curAlcoProgress = 0;
            maxAlcoProgress = 34;
            IconId = Resource.Drawable.ic_pie;
        }

        void setAlcoProgress()
        {
            curAlcoProgress = curAlcoVolume * 789 / (bodyMass * 0.65);
            progressBar.Progress = Convert.ToInt32(curAlcoProgress);
        }

		void weightChanged(object sender, EventArgs e)
        {
			if (curAlcoVolume == 0)
			{
				if (((SeekBar)sender).Progress == 0)
				{
					((SeekBar)sender).Progress = 1;
				}
				else
				{
					bodyMass = ((SeekBar)sender).Progress;
					weightText.Text = ((SeekBar)sender).Progress.ToString() + " Kg";
					setAlcoProgress();
				}
			}
			else
			{
				((SeekBar)sender).Progress = bodyMass;
			}
        }

        void setupSeekBar(View view)
        {
			weightSeekBar = view.FindViewById<SeekBar>(Resource.Id.SeekBarStrong);
            weightText = view.FindViewById<TextView>(Resource.Id.TextStrong);
            weightSeekBar.Max = 150;
            weightSeekBar.Progress = bodyMass;
            weightText.Text = weightSeekBar.Progress + " Kg";
            weightSeekBar.ProgressChanged += weightChanged;
        }

        void setupProgressBar(View view)
        {
            progressBar = (ProgressBar)view.FindViewById(Resource.Id.AlkoProgressBar);
            progressBar.ScaleY = 5;
            progressBar.Max = Convert.ToInt32(maxAlcoProgress);
            setAlcoProgress();
        }

        void stopDrinkAlert()
        {
			var vibrator = (Vibrator)Activity.GetSystemService(Context.VibratorService);
            vibrator.Vibrate(1000);
			var builder = new AlertDialog.Builder(this.Activity);
			var w = new TextView(this.Activity);
            w.Text = "Go home!";
            w.TextSize = 30;
            builder.SetView(w);
            builder.SetPositiveButton("Ok", (senderalert, args) => { });
            builder.Show();

        }

		void haveDrink(object sender, AdapterView.ItemClickEventArgs e)
		{
			if (curAlcoProgress > maxAlcoProgress)
			{
				stopDrinkAlert();
			}
			else
			{
				DrinkItem item = DrinkManager.GetDrink(drinks[e.Position].ID);
				curAlcoVolume += item.Volume * item.AlcoholByVolume;
				setAlcoProgress();
			}
		}

		void showAboutDrinkAlert(object sender, AdapterView.ItemLongClickEventArgs e)
        {
			var alert = new AlertDialog.Builder(Activity);
			alert.SetTitle("About drink");
			alert.SetMessage(DrinkManager.GetDrink(drinks[e.Position].ID).About);
			alert.SetPositiveButton("Ok", (senderAlert, args) =>
			{
				Toast.MakeText(Activity, "Ok", ToastLength.Short).Show();
			});

			var dialog = alert.Create();
			dialog.Show();
        }

		void setupNewDrinkButton(View view)
		{
			var button = view.FindViewById<Button>(Resource.Id.AddNewDrinkButton);
			button.Click += (sender, e) =>
			{
				var newDrink = new Intent(Activity, typeof(NewDrinkScreen));
				StartActivity(newDrink);
			};
		}

    	void setupDrinksListView(View view)
        {
			drinksListView = view.FindViewById<ListView>(Resource.Id.DrinkList);
            drinksListView.ItemClick += haveDrink;
			drinksListView.ItemLongClick += showAboutDrinkAlert;
			drinksListView.SetMinimumHeight(100);
			var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
			var footerLayout = inflater.Inflate(Resource.Layout.DrinksListFooter, null);
			setupNewDrinkButton(footerLayout);
			drinksListView.AddFooterView(footerLayout);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Alko, container, false);
            setupSeekBar(view);
            setupProgressBar(view);
			setupDrinksListView(view);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            drinks = DrinkManager.GetDrinks();
            drinksList = new DrinkItemListAdapter(Activity, drinks);
            drinksListView.Adapter = drinksList;
        }
    }
}