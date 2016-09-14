
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

namespace XamProjectTest.service
{
    // This is a class with a static constructor which will be a single point of reference for every call to different end points in the restful web service.
    public class RestClient
    {
        private static RestServiceInterface REST_CLIENT, REST_CLIENT_LOGIN;
        private static string REST_CLIENT_URL = "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/";
        private static string REST_CLIENT_LOGIN_URL = "http://userservice.staging.tangentmicroservices.com:80/";

        
        static RestClient()
        {
            // Initialize.
            REST_CLIENT = RestService.For<RestServiceInterface>(REST_CLIENT_URL);
            REST_CLIENT_LOGIN = RestService.For<RestServiceInterface>(REST_CLIENT_LOGIN_URL);
        }

        private RestClient() { }

        public static RestServiceInterface getRestClient()
        {
            return REST_CLIENT;
        }

        public static void setRestClient(RestServiceInterface restClient)
        {
            REST_CLIENT = restClient;
        }

        public static RestServiceInterface getRestClientLogin()
        {
            return REST_CLIENT_LOGIN;
        }

        public static void setRestClientLogin(RestServiceInterface restClient)
        {
            REST_CLIENT_LOGIN = restClient;
        }

    }



}