using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Graphics;
using Android.Runtime;
using Android.Hardware;
using System;

namespace Alkometer.Screen 
{
	[Activity(Theme = "@android:style/Theme.Material.NoActionBar.Fullscreen")]
	public class NewContactTakePhotoScreen : Activity, ISurfaceHolderCallback, 
        Android.Hardware.Camera.IPictureCallback, Android.Hardware.Camera.IAutoFocusCallback
	{
		Bitmap photo;
		bool previewing = false;
		bool isPhotoTaken = false;
		ISurfaceHolder surfaceHolder;
		ImageView imageView;
		SurfaceView surfaceView;
		ImageButton acceptButton;
		ImageButton cancelButton;
		LinearLayout buttonLayout;
		Android.Hardware.Camera camera;

		protected void startCamera()
		{
			surfaceView.Visibility = ViewStates.Visible;
			imageView.Visibility = ViewStates.Invisible;
			acceptButton.Visibility = ViewStates.Invisible;
			cancelButton.Visibility = ViewStates.Invisible;
			isPhotoTaken = false;
            surfaceView.Click += (sender, e) => { if (!isPhotoTaken) camera.AutoFocus(this); };
			buttonLayout.Background = null;
			try
		 	{
				Android.Hardware.Camera.Parameters parameters = camera.GetParameters();
				parameters.FocusMode = Android.Hardware.Camera.Parameters.FocusModeAuto;
				camera.SetParameters(parameters);
				camera.SetPreviewDisplay(surfaceHolder);
				camera.StartPreview();
				previewing = true;
			}
			catch (Java.IO.IOException e)
			{
				e.PrintStackTrace();
			}
		}

		protected void bindTakePhotoElements()
		{
			surfaceView = FindViewById<SurfaceView>(Resource.Id.ContactNewSurfaceView);
			surfaceHolder = surfaceView.Holder;
			surfaceHolder.AddCallback(this);
			surfaceHolder.SetType(SurfaceType.PushBuffers);
		}

		protected void bindAcceptElements()
		{
			buttonLayout = FindViewById<LinearLayout>(Resource.Id.ContactNewButtonLayout);

			acceptButton = FindViewById<ImageButton>(Resource.Id.ContactAcceptSaveButton);
			acceptButton.Click += (sender, e) =>
			{
				var newContactEdit = new Intent(this, typeof(NewContactEditScreen));
				DataSend.Photo = photo;
				StartActivity(newContactEdit);
				Finish();
			};

			cancelButton = FindViewById<ImageButton>(Resource.Id.ContactAcceptCancelButton);
			cancelButton.Click += (sender, e) => { photo = null; startCamera(); };

			imageView = FindViewById<ImageView>(Resource.Id.ContactNewImageView);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Window.RequestFeature(WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.NewContactTakePhoto);
			bindTakePhotoElements();
			bindAcceptElements();
		}

		public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
		{
			if (previewing)
			{
				camera.StopPreview();
				previewing = false;
			}
			if (camera != null)
			{
				startCamera();
			}
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			camera = Android.Hardware.Camera.Open();
			camera.SetDisplayOrientation(90);
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			if (!isPhotoTaken)
			{
				camera.StopPreview();
				camera.Release();
				previewing = false;
			}
		}

		public void OnPictureTaken(byte[] data, Android.Hardware.Camera camera)
		{
			camera.StopPreview();
			camera.Release();
			previewing = false;
			isPhotoTaken = true;

			photo = BitmapFactory.DecodeByteArray(data, 0, data.Length);
			Matrix matrix = new Matrix();
			matrix.PostRotate(90);
			photo = Bitmap.CreateBitmap(photo, 0, 0, photo.Width, photo.Height, matrix, true);

			surfaceView.Visibility = ViewStates.Invisible;
			acceptButton.Visibility = ViewStates.Visible;
			cancelButton.Visibility = ViewStates.Visible;
			imageView.Visibility = ViewStates.Visible;
			buttonLayout.SetBackgroundColor(Color.ParseColor("#80000000"));
			imageView.SetImageBitmap(photo);
		}

        public void OnAutoFocus(bool success, Android.Hardware.Camera camera)
        {
            if (success)
            {
                camera.TakePicture(null, null, null, this);
            }
        }
    }
}