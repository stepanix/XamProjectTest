using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamProjectTest
{
    [Activity(Label = "Xam Project Test", MainLauncher = true, Icon = "@drawable/icon")]   
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += delegate 
            {
                Login();
            };
        }


        private void Login()
        {
            Toast.MakeText(this, "Hello World", ToastLength.Long).Show();
        }

    }
}