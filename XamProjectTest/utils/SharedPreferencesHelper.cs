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
        private static string BEARER_TOKEN_SETTINGS = "BearerTokenSettings";
        public static void storeUserToken(Context context, model.UserToken userToken)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("token", userToken.Token);
            editor.Apply();
        }

        public static string retrieveUserToken(Context context)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            string userToken = prefs.GetString("token", "");
            return userToken;
        }

    }
}