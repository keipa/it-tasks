using Android.App;
using Android.OS;

using Alkometer.Activities;
using Android.Views;

namespace Alkometer
{
    [Activity(MainLauncher = true)]
    public class MainScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Contacts", Resource.Drawable.ic_tab_white, new ContactsTabFragment());
			AddTab("Statistics", Resource.Drawable.ic_tab_white, new MeterSpirt());

            if (savedInstanceState != null)
                ActionBar.SelectTab(ActionBar.GetTabAt(savedInstanceState.GetInt("tab")));

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, int iconResourceId, BaseTabFragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(view.IconId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                    var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                    if (fragment != null)
                        e.FragmentTransaction.Remove(fragment);
                    e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
			{
				e.FragmentTransaction.Remove(view);
			};

            ActionBar.AddTab(tab);
        }
    }
}


