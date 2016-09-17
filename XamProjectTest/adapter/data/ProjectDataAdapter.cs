using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using XamProjectTest.model;
using Android.App;
using Android.Support.V7.Widget;
using XamProjectTest.utils;
using Android.Graphics;
using Android.OS;
using XamProjectTest.fragment;
using XamProjectTest.controller;

namespace XamProjectTest.adapter.data
{
    public class ProjectDataAdapter : RecyclerView.Adapter
    {
        
        private List<Project> lstProject;
        //private int VIEW_ITEM = 1;
        //private int VIEW_PROG = 0;

        public ProjectDataAdapter(List<Project> lstProject)
        {
            this.lstProject = lstProject;
        }

        public void RefreshData()
        {
            NotifyDataSetChanged();
        }

        public override int ItemCount
        {
            get
            {
                if(lstProject!=null)
                    return lstProject.Count;

                return 0;
            }
        }

        //public override int GetItemViewType(int position)
        //{
        //    return lstProject[position] != null ? VIEW_ITEM : VIEW_PROG;
        //}

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.CardView_Projects, parent, false);
            ViewProjectHolder vh = new ViewProjectHolder(v);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Project singleProject = lstProject[position];
            ((ViewProjectHolder)holder).TxtProjectName.Text = singleProject.Title.ToUpper();
            ((ViewProjectHolder)holder).ProjectData = singleProject;
        }

        public class ViewProjectHolder : RecyclerView.ViewHolder
        {
            Button btnEditProject, btnDeleteProject;
            TextView txtProjectName;
            Project project;

            public ViewProjectHolder(View v) : base(v)
            {
                txtProjectName = (TextView)v.FindViewById(Resource.Id.txtProjectName);
                btnEditProject = (Button)v.FindViewById(Resource.Id.btnEditProject);
                btnDeleteProject = (Button)v.FindViewById(Resource.Id.btnDeleteProject);

                btnEditProject.SetTypeface(FontUtilityHelper.getFont(), TypefaceStyle.Normal);
                btnDeleteProject.SetTypeface(FontUtilityHelper.getFont(), TypefaceStyle.Normal);

                btnDeleteProject.Click += delegate
                {
                    DeleteProject(v.Context, project.Pk);
                };

                btnEditProject.Click += delegate
                {
                    Bundle arguments = new Bundle();
                    arguments.PutInt("PK", project.Pk);
                    Fragment fragment = new ViewProjectFragment();
                    fragment.Arguments = arguments;
                   
                    ((Activity)v.Context).FragmentManager.BeginTransaction()
                               .Replace(Resource.Id.fragment_container, fragment)
                               .Commit();
                };
            }

            public TextView TxtProjectName
            {
                get { return txtProjectName; }
            }

            public Project ProjectData
            {
                get
                {
                    return project;
                }
                set
                {
                    project = value;
                }
            }

            private async void DeleteProject(Context context,int pk)
            {
                Android.Content.Res.Resources res;
                res = context.ApplicationContext.Resources;

                ProgressDialog progressDialog = new ProgressDialog(context);
                progressDialog.SetCancelable(false);
                progressDialog.SetMessage("Deleting project details, Please Wait...");
                progressDialog.Show();
                
                try
                {
                    ProjectController projectController = new ProjectController();
                    await projectController.DeleteProject(pk);
                   
                    Toast.MakeText(context, res.GetString(Resource.String.msg_project_deleted), ToastLength.Long).Show();
                    if (progressDialog.IsShowing)
                        progressDialog.Dismiss();
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(context, res.GetString(Resource.String.msg_project_delete_error), ToastLength.Long).Show();
                    if (progressDialog.IsShowing)
                        progressDialog.Dismiss();
                }
                
            }



        }

    }
}