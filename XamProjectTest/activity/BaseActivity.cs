

using Android.App;

using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android;
using XamProjectTest.utils;
using Android.Views;

namespace XamProjectTest.activity
{
    [Activity(Label = "XAM Project Test" , MainLauncher = true, Icon = "@drawable/icon")]
    public class BaseActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Base);
            //Check if user logged in successfully previously by checking if token is present
            if (SharedPreferencesHelper.retrieveUserToken(this).Trim().Length<2)
            {
                StartActivity(typeof(LoginActivity));
            }

             drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_rateapp):
                    // React on 'Home' selection
                    break;
                case (Resource.Id.nav_listprojects):
                    // React on 'Messages' selection
                    break;
                case (Resource.Id.nav_addproject):
                    // React on 'Friends' selection
                    break;
            }

            // Close drawer
            drawerLayout.CloseDrawers();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.nav_settings:
                    //do something
                    return true;
                case Resource.Id.nav_logout:
                    //do something
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        //private vo

    }
}