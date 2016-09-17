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
using XamProjectTest.model;
using XamProjectTest.service;
using XamProjectTest.utils;
using System.Threading.Tasks;

namespace XamProjectTest.controller
{
    public class ProjectController
    {
        public ProjectController()
        {
        }

        //Create new project
        public Task<Project> SaveProject(Project project)
        {
            return RestClient.getRestClient().createProject(SharedPreferencesHelper.retrieveUserToken(), project);
        }

        //Get All Projects
        public async Task<List<Project>> GetProjects()
        {
            return await RestClient.getRestClient().getProjects(SharedPreferencesHelper.retrieveUserToken());
        }

        //Get One Project
        public async Task<Project> GetProject(int pk)
        {
            return await RestClient.getRestClient().getProject(SharedPreferencesHelper.retrieveUserToken(),pk);
        }

    }
}