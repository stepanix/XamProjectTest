using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using XamProjectTest.adapter.data;
using XamProjectTest.model;
using System.Collections.Generic;
using XamProjectTest.controller;

namespace XamProjectTest.fragment
{
    public class ListProjectFragment : Fragment
    {
        public RecyclerView recyclerView;
        public ProjectDataAdapter adapter;
        public RecyclerView.LayoutManager layoutManager;
        List<Project> lstProject;
        
        Handler handler;
        ProgressDialog progressDialog;


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

            progressDialog = new ProgressDialog(Activity);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage("Loading projects, Please Wait...");
            progressDialog.Show();

            //Populate Projects Method
            ListProjects();
            return rootView;
        }

        private async void ListProjects()
        {
            //Call project controller to retrieve projects
            ProjectController projectController = new ProjectController();
            var arrProjects = await projectController.GetProjects();

            foreach (Project project in arrProjects)
            {
                lstProject.Add(
                    new Project(project.Pk,
                            project.Title, project.Description,
                            project.StartDate, project.EndDate,
                            project.IsBillable, project.IsActive,
                            project.TaskSet, project.ResourceSet
                    ));
            }
            handler.Post(new Java.Lang.Runnable(() =>
            {
                adapter.NotifyItemInserted(lstProject.Count);
            }));
            adapter.NotifyDataSetChanged();
            if (progressDialog.IsShowing)
                progressDialog.Dismiss();
        }

    }

    

}