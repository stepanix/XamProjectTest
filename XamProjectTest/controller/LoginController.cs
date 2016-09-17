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
using System.Threading.Tasks;
using XamProjectTest.model;
using XamProjectTest.service;

namespace XamProjectTest.controller
{
    public class LoginController
    {
        public LoginController()
        {
        }

        public async Task<UserToken> PostLogin(Dictionary<string, object> loginDetails)
        {
            return await RestClient.getRestClientLogin().postLogin(loginDetails);
        }


    }
}