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
using Newtonsoft.Json;

namespace XamProjectTest.model
{
    public class UserToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        //This field is to display error message.
        [JsonProperty("non_field_errors")]
        public string[] ErrorMessage { get; set; }

    }
}