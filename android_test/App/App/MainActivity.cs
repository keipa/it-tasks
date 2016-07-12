
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
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            Addtab("Camera", new Camera());
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
           


            // Get our button from the layout resource,
            // and attach an event to it
            //SetTitle();

            //TabHost tabHost = (TabHost)FindViewById(Resource.Id.tabHost);

            //tabHost.Setup();

            //TabHost.TabSpec tabSpec = tabHost.NewTabSpec("tag1");

            //tabSpec.SetContent(Resource.Id.linearLayout);
            //tabSpec.SetIndicator("Кот");
            //tabHost.AddTab(tabSpec);

            //tabSpec = tabHost.NewTabSpec("tag2");
            //tabSpec.SetContent(Resource.Id.linearLayout2);
            //tabSpec.SetIndicator("Кошка");
            //tabHost.AddTab(tabSpec);

            //tabSpec = tabHost.NewTabSpec("tag3");
            //tabSpec.SetContent(new Camera())
            //tabSpec.SetIndicator("Котёнок");
            //tabHost.AddTab(tabSpec);

            
            //tabHost.SetCurrentTab(0);
            //tabHost.SetCurrentTabByTag("0");



            //Button button = FindViewById<Button>(Resource.Id.MyButton);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            //this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            //var cam = this.ActionBar.NewTab();
            //cam.SetText("camera");
            //var met = this.ActionBar.NewTab();
            //met.SetText("meter");
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

