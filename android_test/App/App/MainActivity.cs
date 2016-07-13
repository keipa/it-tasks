
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
namespace App
{
    [Activity(Label = "App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            Addtab("Camera", new Camera());
            Addtab("Places", new PlaceItem());
            Addtab("Meter", new MeterSpirt());

            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
           

        }



        private void Addtab(string t, Fragment f)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(t);
            tab.TabSelected += (sender, e) =>
            {
                e.FragmentTransaction.Replace(Resource.Id.FragmentContainer, f);
            };
            this.ActionBar.AddTab(tab);
        }
    }
}

