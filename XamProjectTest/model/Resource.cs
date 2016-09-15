using System;
using Newtonsoft.Json;

namespace XamProjectTest.model
{
    public class Resource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("start_date")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [JsonProperty("end_date")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [JsonProperty("rate")]
        public float Rate { get; set; }

        [JsonProperty("agreed_hours_per_month")]
        public decimal AgreedHoursPerMonth { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("project")]
        public object Project { get; set; }

    }
}