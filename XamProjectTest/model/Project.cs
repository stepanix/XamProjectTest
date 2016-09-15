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
using Newtonsoft.Json;

namespace XamProjectTest.model
{
    public class Project
    {

        


        [JsonProperty("pk")]
        public int Pk { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start_date")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [JsonProperty("end_date")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [JsonProperty("is_billable")]
        public bool IsBillable { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("task_set")]
        public List<Task> ProjectData { get; set; }

        [JsonProperty("resource_set ")]
        public List<Resource> ResourceSet { get; set; }

        public Project(int Pk, string Title, string Description,
            Nullable<System.DateTime> StartDate, Nullable<System.DateTime> EndDate, bool IsBillable,
            bool IsActive, List<Task> ProjectData, List<Resource>ResourceSet)
        {
            this.Pk = Pk;
            this.Title = Title;
            this.Description = Description;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.IsBillable = IsBillable;
            this.IsActive = IsActive;
            this.ProjectData = ProjectData;
            this.ResourceSet = ResourceSet;
        }
    }
}