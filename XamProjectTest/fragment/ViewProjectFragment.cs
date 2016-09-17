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
        Button btnUpdate;
        Android.Content.Res.Resources res;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.View_Project_Fragment, container, false);
            btnUpdate = view.FindViewById<Button>(Resource.Id.btnUpdate);
            edtProjectTitle = view.FindViewById<EditText>(Resource.Id.edtProjectTitle);
            edtProjectDescription = view.FindViewById<EditText>(Resource.Id.edtProjectDescription);
            edtStartDate = view.FindViewById<EditText>(Resource.Id.edtStartDate);
            edtEndDate = view.FindViewById<EditText>(Resource.Id.edtEndDate);
            chkIsBillable = view.FindViewById<CheckBox>(Resource.Id.chkIsBillable);
            chkIsActive = view.FindViewById<CheckBox>(Resource.Id.chkIsActive);
            res = Activity.Resources;

            //Get pk argument to use for fetching project detail
            int pk = Arguments.GetInt("PK");
            
            //Call Method to fetch project details fro viewing
            FetchProject(pk);

            btnUpdate.Click += delegate
            {
                //validate project data
                if (FormValidationHelper.IsValueEmpty(edtProjectTitle.Text.ToString()))
                {
                    Toast.MakeText(Activity, res.GetString(Resource.String.msg_project_title_error), ToastLength.Long).Show();
                    return;
                }

                if (FormValidationHelper.IsValueEmpty(edtProjectDescription.Text.ToString()))
                {
                    Toast.MakeText(Activity, res.GetString(Resource.String.msg_project_desc_error), ToastLength.Long).Show();
                    return;
                }

                if (FormValidationHelper.IsValueEmpty(edtStartDate.Text.ToString()))
                {
                    Toast.MakeText(Activity, res.GetString(Resource.String.msg_project_startdate_error), ToastLength.Long).Show();
                    return;
                }

                XamProjectTest.model.Resource[] resourceSet = new XamProjectTest.model.Resource[] { };
                XamProjectTest.model.Task[] taskSet = new XamProjectTest.model.Task[] { };

                Project project = new Project(pk, edtProjectTitle.Text.ToString(),
                     edtProjectDescription.Text.ToString(),
                     FormValidationHelper.ParseDateValue(edtStartDate.Text.ToString()),
                     FormValidationHelper.ParseDateValue(edtEndDate.Text.ToString()),
                     chkIsBillable.Checked,
                     chkIsActive.Checked,
                     taskSet,
                     resourceSet
                    );

                //Call Save Project Method
                UpdateProject(view.Context,pk,project);
            };

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
            }catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message , ToastLength.Long).Show();
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();
            }
        }

        private async void UpdateProject(Context context,int pk,Project project)
        {
            ProgressDialog progressDialog = new ProgressDialog(Activity);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage("Updating project, Please Wait...");
            progressDialog.Show();

            try
            {
                ProjectController projectController = new ProjectController();
                // project output variable can also be used to cache project data saved successfully on the remote server
                Project projectOutput = await projectController.UpdateProject(pk,project);

                //Check if data was saved successfully
                if (projectOutput != null)
                    Toast.MakeText(Activity, res.GetString(Resource.String.msg_project_saved), ToastLength.Long).Show();

                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();

                //Call method to cache data to local database (SQLlite)

                //Destroy object
                projectOutput = null;
            }
            catch (Exception ex)
            {
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();
                //Display error message
                Toast.MakeText(Activity, ex.Message + " " + res.GetString(Resource.String.msg_project_saved_error), ToastLength.Long).Show();
                //Call method to log error on server. This is only neccessary if there is no audit system on remote web service 
            }

        }

    }
}