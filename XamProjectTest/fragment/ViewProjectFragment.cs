using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamProjectTest.controller;
using XamProjectTest.model;
using XamProjectTest.utils;

namespace XamProjectTest.fragment
{
    public class ViewProjectFragment : Fragment
    {
        EditText edtStartDate, edtEndDate, edtProjectTitle, edtProjectDescription;
        CheckBox chkIsBillable, chkIsActive;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.View_Project_Fragment, container, false);
            edtProjectTitle = view.FindViewById<EditText>(Resource.Id.edtProjectTitle);
            edtProjectDescription = view.FindViewById<EditText>(Resource.Id.edtProjectDescription);
            edtStartDate = view.FindViewById<EditText>(Resource.Id.edtStartDate);
            edtEndDate = view.FindViewById<EditText>(Resource.Id.edtEndDate);
            chkIsBillable = view.FindViewById<CheckBox>(Resource.Id.chkIsBillable);
            chkIsActive = view.FindViewById<CheckBox>(Resource.Id.chkIsActive);

            //Get pk argument to use for fetching project detail
            int pk = Arguments.GetInt("PK");
            
            //Call Method to fetch project details fro viewing
            FetchProject(pk);
            return view;
        }

        private async void FetchProject(int pk)
        {
            ProgressDialog progressDialog = new ProgressDialog(Activity);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage("Fetching project details, Please Wait...");
            progressDialog.Show();

            try
            {
                ProjectController projectController = new ProjectController();
                Project project = await projectController.GetProject(pk);
                edtProjectTitle.Text = project.Title;
                edtProjectDescription.Text = project.Description;
                edtStartDate.Text = project.StartDate;
                edtEndDate.Text = FormValidationHelper.ParseEmptyDate(project.EndDate);
                chkIsBillable.Checked = project.IsBillable;
                chkIsActive.Checked = project.IsActive;
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message , ToastLength.Long).Show();
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();
            }
            

        }

    }
}