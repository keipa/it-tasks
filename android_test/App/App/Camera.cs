using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace App
{
    public class Camera : Fragment, TextureView.ISurfaceTextureListener
    {
        Android.Hardware.Camera _camera;
        TextureView _textureView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            _textureView = new TextureView(Application.Context);
            _textureView.SurfaceTextureListener = this;
            _textureView.Click += (sender, e) =>
            {

                AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
                EditText input = new EditText(this.Activity);
                builder.SetView(input);
                builder.SetPositiveButton("ok", (senderalert, args) =>
                {
                    var bitmap = _textureView.GetBitmap(500, 500);
                    var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                    var filePath = System.IO.Path.Combine(sdCardPath, input.Text+ ".png");
                    var stream = new FileStream(filePath, FileMode.Create);
                    bitmap.Compress(Bitmap.CompressFormat.Png, 75, stream);
                    stream.Close();
                });

                this.Activity.RunOnUiThread(() =>
                {
                    builder.Show();
                });


            };

            return _textureView;
        }







        public void OnSurfaceTextureAvailable(
       Android.Graphics.SurfaceTexture surface, int w, int h)
        {
            _camera = Android.Hardware.Camera.Open();
            _camera.SetDisplayOrientation(90);
            _textureView.LayoutParameters =
                   new FrameLayout.LayoutParams(w, h);

            try
            {
                _camera.SetPreviewTexture(surface);
                _camera.StartPreview();

            }
            catch (Java.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool OnSurfaceTextureDestroyed(
       Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview();
            _camera.Release();

            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }

    }
}