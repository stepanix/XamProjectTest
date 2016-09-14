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
                var restInterface = RestService.For<RestServiceInterface>("http://userservice.staging.tangentmicroservices.com:80/");
                UserToken userToken = await restInterface.postLogin(loginDetails);
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();

                //Store Bearer Token then proceed to next screen (Work in progress)
                SharedPreferencesHelper.storeUserToken(this, userToken);
                Toast.MakeText(this, userToken.Token, ToastLength.Long).Show();
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