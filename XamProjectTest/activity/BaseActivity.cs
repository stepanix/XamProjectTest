

using Android.App;

using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using XamProjectTest.utils;
using Android.Views;
using Android.Content;
using XamProjectTest.fragment;

namespace XamProjectTest.activity
{
    [Activity(Label = "XAM Project Test" , AlwaysRetainTaskState = false, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance, MainLauncher = true, Icon = "@drawable/icon")]
    public class BaseActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Base);
            //Load Initial Fragment
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            ListProjectFragment fragment = new ListProjectFragment();

            //The fragment will have the ID of Resource.Id.fragment_container.
           fragmentTx.Add(Resource.Id.fragment_container, fragment);

            //Commit the transaction.
           fragmentTx.Commit();

            //Check if user logged in successfully previously by checking if token is present
            if (SharedPreferencesHelper.retrieveUserToken(this).Trim().Length<2)
            {
                logOut();
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
                    //FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                    //AddProjectFragment fragment = new AddProjectFragment();

                    // The fragment will have the ID of Resource.Id.fragment_container.
                    //fragmentTx.Add(Resource.Id.fragment_container, aDifferentDetailsFrag);

                    // Commit the transaction.
                    //fragmentTx.Commit();
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
                    //Call logout method
                    logOut();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void logOut()
        {
            //Clear stored user token
            SharedPreferencesHelper.clearUserToken(this);
            //Return to Login Screen
            var IntentLogin = new Intent(this, typeof(LoginActivity));
            IntentLogin.AddFlags(ActivityFlags.ClearTop);
            IntentLogin.AddFlags(ActivityFlags.ClearTask);
            IntentLogin.AddFlags(ActivityFlags.NewTask);
            IntentLogin.AddFlags(ActivityFlags.NoHistory);
            StartActivity(IntentLogin);            
            this.Finish();

        }

    }
}