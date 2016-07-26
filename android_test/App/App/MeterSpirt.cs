using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


namespace App
{
    public class MeterSpirt : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            double norm = 2000;
            double cur = 0;
            var view = inflater.Inflate(Resource.Layout.alco, container, false);
            LinearLayout layout = (LinearLayout)view.FindViewById(Resource.Id.meterlayout);

            SeekBar strongness = (SeekBar)view.FindViewById(Resource.Id.seekBarStrong);
            TextView strongtext = (TextView)view.FindViewById(Resource.Id.textStrong);
            SeekBar volumeness = (SeekBar)view.FindViewById(Resource.Id.seekBarVolume);
            TextView volumetext = (TextView)view.FindViewById(Resource.Id.textVolume);
            Button drink = (Button)view.FindViewById(Resource.Id.drinkbutton);
            Button clearb = (Button)view.FindViewById(Resource.Id.clearbutton);
            ProgressBar drinkprogress = (ProgressBar)view.FindViewById(Resource.Id.progressBar1);
            clearb.Click += (sender, e) =>
            {
                drinkprogress.Progress = 0;
            };
            strongness.ProgressChanged += (sender, e) =>
            {
                strongtext.Text = strongness.Progress.ToString()+"*";
            };
            volumeness.ProgressChanged += (sender, e) =>
            {
                volumetext.Text = (volumeness.Progress*20).ToString()+ "ml";
            };

            drink.Click += (sender, e) =>
            {
                cur = volumeness.Progress * strongness.Progress / 10;
                drinkprogress.Progress += Convert.ToInt32(cur);
                if (cur > norm||drinkprogress.Progress>99)
                {


                    Vibrator vibrator = (Vibrator)Activity.GetSystemService(Context.VibratorService);
                    vibrator.Vibrate(1000);

                    AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);

                    TextView w = new TextView(this.Activity);
                    w.Text = "stop to drink";
                    w.TextSize = 30;
                    //image.SetImageBitmap(instanceImage);
                    builder.SetView(w);
                    builder.SetPositiveButton("ok", (senderalert, args) =>
                    {

                    });
                    builder.Show();
                }
            };




            return view;
        }
    }
}