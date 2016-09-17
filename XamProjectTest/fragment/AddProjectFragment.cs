using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using XamProjectTest.model;
using XamProjectTest.controller;
using XamProjectTest.utils;

namespace XamProjectTest.fragment
{
    public class AddProjectFragment : Fragment, DatePickerDialog.IOnDateSetListener
    {
        EditText edtStartDate, edtEndDate, edtProjectTitle, edtProjectDescription;
        CheckBox chkIsBillable, chkIsActive;
        Button btnSave;
        Android.Content.Res.Resources res;

        //This is used to know if start date or end date was selected.
        int dateWidgetSelected = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Manage_Project_Fragment, container, false);
            edtProjectTitle = view.FindViewById<EditText>(Resource.Id.edtProjectTitle);
            edtProjectDescription = view.FindViewById<EditText>(Resource.Id.edtProjectDescription);
            edtStartDate = view.FindViewById<EditText>(Resource.Id.edtStartDate);
            edtEndDate = view.FindViewById<EditText>(Resource.Id.edtEndDate);
            chkIsBillable = view.FindViewById<CheckBox>(Resource.Id.chkIsBillable);
            chkIsActive = view.FindViewById<CheckBox>(Resource.Id.chkIsActive);
            btnSave = view.FindViewById<Button>(Resource.Id.btnSave);
            res = Activity.Resources;

            btnSave.Click += delegate
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

                Project project = new Project(0, edtProjectTitle.Text.ToString(),
                     edtProjectDescription.Text.ToString(),
                     FormValidationHelper.ParseDateValue(edtStartDate.Text.ToString()),
                     FormValidationHelper.ParseDateValue(edtEndDate.Text.ToString()),
                     chkIsBillable.Checked,
                     chkIsActive.Checked,
                     taskSet,
                     resourceSet
                    );
                //Call Save Project Method
                SaveProject(project);
            };

            edtStartDate.Click += delegate
            {
                dateWidgetSelected = 1;
                var dialog = new DatePickerFragment(Activity, DateTime.Now, this);
                dialog.Show(FragmentManager, "date");
            };

            edtEndDate.Click += delegate
            {
                dateWidgetSelected = 2;
                var dialog = new DatePickerFragment(Activity, DateTime.Now, this);
                dialog.Show(FragmentManager, "date");
            };

            return view;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            if(dateWidgetSelected==1)
            {
                edtStartDate.Text = selectedDate.ToString("yyyy-MM-dd");
            }else
            {
                edtEndDate.Text = selectedDate.ToString("yyyy-MM-dd");
            }
        }

        private async void SaveProject(Project project)
        {
            ProgressDialog progressDialog = new ProgressDialog(Activity);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage("Saving project, Please Wait...");
            progressDialog.Show();

            try
            {
                ProjectController projectController = new ProjectController();
                // project output variable can also be used to cache project data saved successfully on the remote server
                Project projectOutput = await projectController.SaveProject(project);

                //Check if data was saved successfully
                if (projectOutput != null)
                  Toast.MakeText(Activity, res.GetString(Resource.String.msg_project_saved), ToastLength.Long).Show();

                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();

                //Call method to cache data to local database (SQLlite)

                //Destroy object
                projectOutput = null;

                //Reset widgets to default
                edtProjectTitle.Text="";
                edtProjectDescription.Text = "";
                edtStartDate.Text = "";
                edtEndDate.Text = "";
                chkIsBillable.Checked = false;
                chkIsActive.Checked = false;
            }
            catch (Exception ex)
            {
                if (progressDialog.IsShowing)
                    progressDialog.Dismiss();
                //Display error message
                Toast.MakeText(Activity, ex.Message + " "+ res.GetString(Resource.String.msg_project_saved_error), ToastLength.Long).Show();
                //Call method to log error on server. This is only neccessary if there is no audit system on remote web service 
            }
            
        }
        


    }
}