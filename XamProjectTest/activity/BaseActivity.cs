

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
        FragmentManager fragmentManager = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Base);

            //Check if user logged in successfully previously by checking if token is present
            if (SharedPreferencesHelper.retrieveUserToken().Trim().Length<2)
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

            fragmentManager = this.FragmentManager;

            //Load Initial Fragment
            Fragment fragment = new ListProjectFragment();
            fragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();

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
                    //Call url to app in Google Play Store
                    break;
                case (Resource.Id.nav_listprojects):
                    //Load Fragment to list all Project
                    Fragment frgList = new ListProjectFragment();
                    fragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, frgList).Commit();
                    break;
                case (Resource.Id.nav_addproject):
                    //Load Fragment to Add or Create a New Project
                    Fragment frgAdd = new AddProjectFragment();
                    fragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, frgAdd).Commit();
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
            SharedPreferencesHelper.clearUserToken();
            //Return to Login Screen
            var IntentLogin = new Intent(Android.App.Application.Context, typeof(LoginActivity));
            IntentLogin.AddFlags(ActivityFlags.ClearTop);
            IntentLogin.AddFlags(ActivityFlags.ClearTask);
            IntentLogin.AddFlags(ActivityFlags.NewTask);
            IntentLogin.AddFlags(ActivityFlags.NoHistory);
            StartActivity(IntentLogin);            
            this.Finish();

        }

    }
}