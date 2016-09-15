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
using System.Threading.Tasks;
using XamProjectTest.model;

namespace XamProjectTest.service
{
    //Interface where all the endpoints are defined.
    public interface RestServiceInterface
    {
        //End point to login by posting x-www-form-urlencoded parameters and return auth token as a string
        [Post("/api-token-auth/")]
        Task<UserToken> postLogin([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> loginDetails);

        //End point to get all projects
        [Get("/projects/")]
        Task<List<Project>> getProjects([Header("Authorization")] string authorization);

        //End point to insert or create a new project
        [Post("/projects/")]
        Task<Project> createProject([Header("Authorization")] string authorization, [Body] Project project);

        //End point to update an existing project
        [Put("/projects/")]
        Task<Project> updateProject([Header("Authorization")] string authorization, [Body] Project project);
    }
}