using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Alkometer.Shared;
using Android.Views;

namespace Alkometer.Screen
{
	[Activity(Theme = "@android:style/Theme.Material.NoActionBar.Fullscreen")]
	public class NewContactEditScreen : Activity
	{
		Bitmap photo;
		ContactItem contact;
		EditText nameEdit;
		EditText surnameEdit;
		EditText numberEdit;
		ImageView imageView;

		void loadContact()
		{
			int contactID = Intent.GetIntExtra("ContactID", 0);
			if (contactID > 0)
			{
				contact = ContactManager.GetContact(contactID);
				nameEdit.Text = contact.Name;
				surnameEdit.Text = contact.Surname;
				numberEdit.Text = contact.Number;
			}
			else
			{
				contact = new ContactItem();
			}
		}

		async void loadPhoto()
		{
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InPreferredConfig = Bitmap.Config.Argb8888;

			if (contact.Path != "")
			{
				photo = await BitmapFactory.DecodeFileAsync(contact.Path, options);
				Console.WriteLine(contact.Path);
			}
			else if (DataSend.Photo != null)
			{
				photo = DataSend.Photo;
				DataSend.Photo = null;
			}
			else
			{
				photo = await BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.kek, options);
			}

			imageView.SetImageBitmap(photo);
		}

		void connectElements()
		{
			FindViewById<ImageButton>(Resource.Id.ContactEditSaveButton).Click += (sender, e) => { saveContact(photo); };
			nameEdit = FindViewById<EditText>(Resource.Id.EditNameText);
			surnameEdit = FindViewById<EditText>(Resource.Id.EditSurnameText);
			numberEdit = FindViewById<EditText>(Resource.Id.EditNumberText);
			imageView = FindViewById<ImageView>(Resource.Id.ContactEditImageView);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Window.RequestFeature(WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.NewContactEdit);
			connectElements();
			loadContact();
			loadPhoto();
		}

		protected string genPhotoPath()
		{
			string sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Alkometer";
			Directory.CreateDirectory(sdCardPath);
			string photoName = "TopPhoto_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
			var filePath = System.IO.Path.Combine(sdCardPath, photoName);
			return sdCardPath + "/" + photoName;
		}

		protected async void exportBitmapAsPNG(Bitmap bitmap, string path)
		{
			using (var stream =  new FileStream(path, FileMode.Create))
			{
				await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, stream);
			}
		}

		protected void saveContact(Bitmap photo)
		{
			contact.Name = nameEdit.Text;
			contact.Surname = surnameEdit.Text; 
			contact.Number = numberEdit.Text;
			if (contact.Path == "")
			{
				string path = genPhotoPath();
				exportBitmapAsPNG(photo, path);
				contact.Path = path;
			}

			ContactManager.SaveContact(contact);
			Finish();
		}
	}
}

