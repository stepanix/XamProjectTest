
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

namespace XamProjectTest.service
{
    // This class is a single point of reference for every call to different end points in the restful web service.
    public class RestClient
    {
        private static RestServiceInterface REST_CLIENT;
        private static string REST_SERVICE_URL = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
        //private static string REST_SERVICE_LOGIN_URL = "http://userservice.staging.tangentmicroservices.com:80/";

        //Static constructor. This is necessary because of the need to create this class only once.
        

        public static RestServiceInterface getRestClient()
        {
            return REST_CLIENT;
        }

        public static void setRestClient(RestServiceInterface restClient)
        {
            REST_CLIENT = restClient;
        }

    }



}