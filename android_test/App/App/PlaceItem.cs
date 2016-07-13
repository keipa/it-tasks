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
using System.IO;
using Android.Graphics;

namespace App
{
    public class PlaceItem : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.places, container, false);
            LinearLayout layout = (LinearLayout)view.FindViewById(Resource.Id.place);
            DirectoryInfo d = new DirectoryInfo(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
            FileInfo[] f = d.GetFiles();
            foreach (var file in f)
            {
                if (file.Name == ".png")
                {
                    continue;
                }
                if (file.Name.Contains(".png"))
                {
                    ImageView image = new ImageView(this.Activity);
                    Bitmap instanceImage = Bitmaping.LoadAndResizeBitmap(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath+"/"+file.Name, 250, 250);
                    image.SetImageBitmap(instanceImage);
                    image.Click += (sender, e) =>
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);

                        TextView w = new TextView(this.Activity);
                        w.Text = file.Name.Replace(".png", "");
                    //image.SetImageBitmap(instanceImage);
                    builder.SetView(w);
                        builder.SetPositiveButton("ok", (senderalert, args) =>
                        {

                        });
                        builder.Show();
                    };
                    layout.AddView(image);
                }

            }

            //LinearLayout layer = new LinearLayout(this.Activity);
            //layer.AddVi
            //view.Add
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);


            return view;
        }

    }

}