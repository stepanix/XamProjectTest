

using Android.App;

using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android;

namespace XamProjectTest.activity
{
    [Activity(Label = "XAM Project Test")]
    public class BaseActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Base);
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

    }
}