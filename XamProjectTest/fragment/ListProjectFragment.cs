

using Android.App;

using Android.OS;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using XamProjectTest.adapter.data;
using XamProjectTest.model;
using System.Collections.Generic;
using XamProjectTest.service;
using XamProjectTest.utils;


namespace XamProjectTest.fragment
{
    public class ListProjectFragment : Fragment
    {
        public RecyclerView recyclerView;
        public ProjectDataAdapter adapter;
        public RecyclerView.LayoutManager layoutManager;
        List<Project> lstProject;
        
        Handler handler;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.List_Project_Fragment, container, false);
            recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            lstProject = new List<Project>();
            handler = new Handler();

            layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.HasFixedSize = true;
            
            adapter = new ProjectDataAdapter(lstProject);
            recyclerView.SetAdapter(adapter);

            //Populate Projects Method
            ListProjects();
            return rootView;
        }

        private async void ListProjects()
        {
            var arrProjects =  await RestClient.getRestClient().getProjects(SharedPreferencesHelper.retrieveUserToken());
                        
            foreach (Project project in arrProjects)
            {
                lstProject.Add(
                    new Project(project.Pk,
                            project.Title, project.Description,
                            project.StartDate, project.EndDate,
                            project.IsBillable, project.IsActive,
                            project.ProjectData, project.ResourceSet
                    ));
            }
            handler.Post(new Java.Lang.Runnable(() =>
            {
                adapter.NotifyItemInserted(lstProject.Count);
            }));
            adapter.NotifyDataSetChanged();
        }

    }

    

}