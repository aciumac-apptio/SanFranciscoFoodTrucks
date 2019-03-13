using Newtonsoft.Json;
using System;

namespace FoodTrucks.Models
{
    public class FoodTruck
    {
        [JsonProperty("applicant")]
        public string Applicant { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonConverter(typeof(CustomDateConverter))]
        [JsonProperty("start24")]
        public DateTime Start24 { get; set; }
        [JsonConverter(typeof(CustomDateConverter))]
        [JsonProperty("end24")]
        public DateTime End24 { get; set; }
        [JsonProperty("dayorder")]
        public int Dayorder { get; set; }
    }
}
