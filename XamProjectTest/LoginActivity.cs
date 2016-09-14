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
using Refit;
using XamProjectTest.service;
using XamProjectTest.model;
using XamProjectTest.utils;
using XamProjectTest.activity;

namespace XamProjectTest
{
    [Activity(Label = "Xam Project Test")]   
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            EditText edtUserName = FindViewById<EditText>(Resource.Id.edtUserName);
            EditText edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);

            btnLogin.Click += delegate 
            {
                //Call to Restful service to login
                Login(edtUserName.Text.ToString(), edtPassword.Text.ToString());
            };
        }

        
        private async void Login(string username,string password)
        {
            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage("Verifying login details, Please Wait...");
            progressDialog.Show();

            var loginDetails = new Dictionary<string, object>
            {
                {"username", username},
                {"password", password},
            };
            try
            {
                UserToken userToken = await RestClient.getRestClientLogin().postLogin(loginDetails);
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();

                //Store Bearer Token then proceed to next screen
                SharedPreferencesHelper.storeUserToken(this, userToken);
                StartActivity(typeof(BaseActivity));
            }
            catch (Exception ex)
            {
                //Bad request indicates that wrong login details were provided
                if (progressDialog.IsShowing)
                    progressDialog.Cancel();
                Android.Content.Res.Resources res = this.Resources;
                Toast.MakeText(this, res.GetString(Resource.String.error_message), ToastLength.Long).Show();
            }

        }

    }
}