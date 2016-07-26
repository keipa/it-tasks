using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Alkometer.Shared;
using Android.Content;

namespace Alkometer
{
    [Activity(Label = "NewDrinkActivity")]
    public class NewDrinkScreen : Activity
    {
        double volume;
        DrinkItem newDrink;
        double alcoholByVolume;

		AlertDialog alert;
        SeekBar volumeSeekBar;
        SeekBar alcoholByVolumeSeekBar;
        TextView volumeEdit;
        TextView alcoholByVolumeEdit;
        ImageButton selectedButton;
        List<ImageButton> allDrinkButtons;
        Dictionary<int, int> buttonIdToIconId;

        public NewDrinkScreen()
        {
            allDrinkButtons = new List<ImageButton>();
            buttonIdToIconId = new Dictionary<int, int>();
            newDrink = new DrinkItem();
            volume = 0.5;
            alcoholByVolume = 0.5;
        }

        AlertDialog.Builder initializeAlertBuilder()
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
			var alertView = inflater.Inflate(Resource.Layout.NewDrinkAcceptAlert, null);
            var alertDialogBuilder = new AlertDialog.Builder(this)
                .SetTitle("Accept drink")
                .SetView(alertView)
                .SetCancelable(true)
                .SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null)
                .SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);
            return alertDialogBuilder;
        }

		void saveNewDrink(object sender, EventArgs e)
		{
			newDrink.IconId = selectedButton != null ? buttonIdToIconId[selectedButton.Id] : 0;
			newDrink.AlcoholByVolume = alcoholByVolume;
			newDrink.Volume = volume;

			var name = alert.FindViewById<EditText>(Resource.Id.NewDrinkNameEdit).Text;
			newDrink.Name = name == "" ? "Something" : name;
			var about = alert.FindViewById<EditText>(Resource.Id.NewDrinkAboutEdit).Text;
			newDrink.About = about == "" ? "Very interesting" : about;

			DrinkManager.SaveDrink(newDrink);
			Finish();
		}

        void showAlert(AlertDialog.Builder builder)
        {
            alert = builder.Create();
			alert.Show();
            var acceptButton = alert.GetButton((int)DialogButtonType.Positive);
			acceptButton.Click += saveNewDrink;
            var cancelButton = alert.GetButton((int)DialogButtonType.Negative);
            cancelButton.Click += (sender, e) => { alert.Dismiss(); };
        }

        void acceptDrink(object sender, EventArgs e)
        {
            var alertBuilder = initializeAlertBuilder();
            showAlert(alertBuilder);
        }

        void bindAcceptElements()
        {
            var acceptButton = FindViewById<ImageButton>(Resource.Id.DrinkAcceptSaveButton);
            acceptButton.Click += acceptDrink;
            var cancelButton = FindViewById<ImageButton>(Resource.Id.DrinkAcceptCancelButton);
            cancelButton.Click += (sender, e) => { base.OnBackPressed(); };
        }

        void getImageButtons()
        {
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.BeerButton));
            buttonIdToIconId.Add(Resource.Id.BeerButton, 0);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.BeerBottleButton));
            buttonIdToIconId.Add(Resource.Id.BeerBottleButton, 1);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.ShampoonGlassButton));
            buttonIdToIconId.Add(Resource.Id.ShampoonGlassButton, 2);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.WineGlassButton));
            buttonIdToIconId.Add(Resource.Id.WineGlassButton, 3);

            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.BeerKekButton));
            buttonIdToIconId.Add(Resource.Id.BeerKekButton, 4);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.BloodButton));
            buttonIdToIconId.Add(Resource.Id.BloodButton, 5);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.DrinkButton));
            buttonIdToIconId.Add(Resource.Id.DrinkButton, 6);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.FruitButton));
            buttonIdToIconId.Add(Resource.Id.FruitButton, 7);

            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.GlassButton));
            buttonIdToIconId.Add(Resource.Id.GlassButton, 8);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.KonjakButton));
            buttonIdToIconId.Add(Resource.Id.KonjakButton, 9);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.PintButton));
            buttonIdToIconId.Add(Resource.Id.PintButton, 10);
            allDrinkButtons.Add(FindViewById<ImageButton>(Resource.Id.ShampoonBottleButton));
            buttonIdToIconId.Add(Resource.Id.ShampoonBottleButton, 11);
        }

        void bindImageButtons()
        {
            getImageButtons();
            foreach (var button in allDrinkButtons)
            {
                button.Click += (sender, e) =>
                {
                    selectedButton?.ClearColorFilter();
                    ((ImageButton)sender).SetColorFilter(Android.Graphics.Color.Gray,
                                                         Android.Graphics.PorterDuff.Mode.Darken);
                    selectedButton = (ImageButton)sender;
                };
            }
        }

        private void bindAlcoholByVolumeSeekBar()
        {
            alcoholByVolumeEdit = FindViewById<TextView>(Resource.Id.TextAlcoholByVolume);
            alcoholByVolumeSeekBar = FindViewById<SeekBar>(Resource.Id.SeekBarAlcoholByVolume);
            alcoholByVolumeSeekBar.ProgressChanged += (sender, e) =>
            {
                alcoholByVolume = (double)((SeekBar)sender).Progress / 100;
                alcoholByVolumeEdit.Text = alcoholByVolume * 100 + " %";
            };
            alcoholByVolumeSeekBar.Progress = (int)(alcoholByVolume * 100);
            alcoholByVolumeEdit.Text = alcoholByVolume + " %";
        }

        private void bindVolumeSeekBar()
        {
            volumeEdit = FindViewById<TextView>(Resource.Id.TextVolume);
            volumeSeekBar = FindViewById<SeekBar>(Resource.Id.SeekBarVolume);
            volumeSeekBar.ProgressChanged += (sender, e) =>
            {
                volume = (double)((SeekBar)sender).Progress / 10;
                volumeEdit.Text = volume + " L";
            };
            volumeSeekBar.Progress = (int)(volume * 10);
            volumeEdit.Text = volume + " L";
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.NewAlcoDrink);
            bindAcceptElements();
            bindImageButtons();
            bindVolumeSeekBar();
            bindAlcoholByVolumeSeekBar();
        }
    }
}