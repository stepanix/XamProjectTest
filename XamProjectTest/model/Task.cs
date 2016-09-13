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
    public class Task
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("estimated_hours")]
        public decimal EstimatedHours { get; set; }

        [JsonProperty("project")]
        public object Project { get; set; }

        [JsonProperty("project_data")]
        public TaskProject ProjectData { get; set; }

    }
}