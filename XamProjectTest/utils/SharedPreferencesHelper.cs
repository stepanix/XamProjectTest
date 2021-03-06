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
using Android.Preferences;

namespace XamProjectTest.utils
{
    public class SharedPreferencesHelper
    {
        public static void storeUserToken(model.UserToken userToken)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("token", userToken.Token);
            editor.Apply();
        }

        public static string retrieveUserToken()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            string userToken = prefs.GetString("token", "");
            return userToken;
        }

        public static void clearUserToken()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("token", "");
            editor.Apply();
        }

    }
}