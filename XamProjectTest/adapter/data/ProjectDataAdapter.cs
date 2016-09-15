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

namespace XamProjectTest.adapter.data
{
    public class ProjectDataAdapter : RecyclerView.Adapter
    {
        
        private List<Project> lstProject;

        
        private int VIEW_ITEM = 1;
        private int VIEW_PROG = 0;

        public ProjectDataAdapter(List<Project> lstProject)
        {
            this.lstProject = lstProject;
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
            var itemHolder = (ViewProjectHolder)holder;
            itemHolder.TxtProjectName.Text = lstProject[position].Title.ToUpper();
        }

        public class ViewProjectHolder : RecyclerView.ViewHolder
        {
            Button btnViewDetails, btnEditProject, btnDeleteProject;
            TextView txtProjectName;

            public ViewProjectHolder(View v) : base(v)
            {
                txtProjectName = (TextView)v.FindViewById(Resource.Id.txtProjectName);
                btnViewDetails = (Button)v.FindViewById(Resource.Id.btnViewDetails);
                btnEditProject = (Button)v.FindViewById(Resource.Id.btnEditProject);
                btnDeleteProject = (Button)v.FindViewById(Resource.Id.btnDeleteProject);

                btnViewDetails.SetTypeface(FontUtilityHelper.getFont(), TypefaceStyle.Normal);
                btnEditProject.SetTypeface(FontUtilityHelper.getFont(), TypefaceStyle.Normal);
                btnDeleteProject.SetTypeface(FontUtilityHelper.getFont(), TypefaceStyle.Normal);
            }

            public TextView TxtProjectName
            {
                get { return txtProjectName; }
            }

           

        }

    }
}