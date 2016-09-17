
using Newtonsoft.Json;
using Android.OS;
using Android.Runtime;
using System;

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
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("is_billable")]
        public bool IsBillable { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("task_set")]
        public Task[] TaskSet { get; set; }

        [JsonProperty("resource_set ")]
        public Resource[] ResourceSet { get; set; }

        public Project(int Pk, string Title, string Description,
            string StartDate, string EndDate, bool IsBillable,
            bool IsActive, Task[] TaskSet, Resource[] ResourceSet)
        {
            this.Pk = Pk;
            this.Title = Title;
            this.Description = Description;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.IsBillable = IsBillable;
            this.IsActive = IsActive;
            this.TaskSet = TaskSet;
            this.ResourceSet = ResourceSet;
        }

    }
}