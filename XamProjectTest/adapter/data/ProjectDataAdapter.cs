using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using XamProjectTest.model;
using Android.App;
using Android.Support.V7.Widget;

namespace XamProjectTest.adapter.data
{
    public class ProjectDataAdapter : RecyclerView.Adapter
    {
        private Context context;
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
                throw new NotImplementedException();
            }
        }

        public override int GetItemViewType(int position)
        {
            return lstProject[position] != null ? VIEW_ITEM : VIEW_PROG;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

    }
}