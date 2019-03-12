using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodTrucks
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
                ShowOpenFoodTrucksAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async Task<int> ShowOpenFoodTrucksAsync()
        {
            List<FoodTruck> foodTrucks;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://data.sfgov.org/resource/");
                
                using (HttpResponseMessage response = await client.GetAsync("bbb8-hzi6.json"))
                {
                    response.EnsureSuccessStatusCode();
                    using (var content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync().ConfigureAwait(false);
                        foodTrucks = JsonConvert.DeserializeObject<List<FoodTruck>>(result);                                            
                    }
                }  
            }

            // Keep only relevant results
            foodTrucks = foodTrucks
                .Where(f => (int) DateTime.Now.DayOfWeek == f.Dayorder && f.Start24.CompareTo(DateTime.Now) <= 0 && f.End24.CompareTo(DateTime.Now) >= 0)
                .OrderBy(f => f.Applicant)
                .ThenBy(f => f.Location).ToList();

            // Print results out
            Console.WriteLine($"Total # of food trucks open: {foodTrucks.Count}");
            Console.WriteLine();
            int currentCursor = 0;
            do
            {
                Console.WriteLine($"{"NAME",-50:D}\t{"ADDRESS"}");
                int count = 0;
                while (currentCursor < foodTrucks.Count && count < 10)
                {                    
                    Console.WriteLine($"{foodTrucks[currentCursor].Applicant,-50:D}\t{foodTrucks[currentCursor].Location}");
                    currentCursor++; count++;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to see more food trucks that are open right now...");

            } while (Console.ReadLine() != null && currentCursor < foodTrucks.Count);

            return await Task.FromResult(0);
        }
    }

    public class CustomDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = reader.Value as string;
            DateTime dateTime;
            if (result.Equals("24:00"))
            {
                dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            }
            else
            {
                dateTime = DateTime.ParseExact(result, "H:mm", null, System.Globalization.DateTimeStyles.None);
            }
            return dateTime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

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

    //public class Location2
    //{
    //    public string type { get; set; }
    //    public List<double> coordinates { get; set; }
    //}

    public class RootObject
    {
        //[JsonProperty("@computed_region_ajp5_b2md")]
        //public string __invalid_name__computed_region_ajp5_b2md { get; set; }
        //[JsonProperty("@computed_region_bh8s_q3mv")]
        //public string __invalid_name__computed_region_bh8s_q3mv { get; set; }
        //[JsonProperty("@computed_region_jx4q_fizf")]
        //public string __invalid_name__computed_region_jx4q_fizf { get; set; }
        //[JsonProperty("@computed_region_rxqg_mtj9")]
        //public string __invalid_name__computed_region_rxqg_mtj9 { get; set; }
        //[JsonProperty("@computed_region_yftq_j783")]
        //public string __invalid_name__computed_region_yftq_j783 { get; set; }

        //public DateTime addr_date_create { get; set; }
        //public DateTime addr_date_modified { get; set; }
        //public string applicant { get; set; }
        //public string block { get; set; }
        //public string cnn { get; set; }
        //public string coldtruck { get; set; }
        //public string dayofweekstr { get; set; }
        //public string dayorder { get; set; }
        //public string end24 { get; set; }
        //public string endtime { get; set; }
        //public string latitude { get; set; }
        //public string location { get; set; }
        //public Location2 location_2 { get; set; }
        //public string locationdesc { get; set; }
        //public string locationid { get; set; }
        //public string longitude { get; set; }
        //public string lot { get; set; }
        //public string optionaltext { get; set; }
        //public string permit { get; set; }
        //public string start24 { get; set; }
        //public string starttime { get; set; }
        //public string x { get; set; }
        //public string y { get; set; }

        [JsonProperty("applicant")]
        public string Applicant { get; set; }

        [JsonProperty("block", NullValueHandling = NullValueHandling.Ignore)]
        public string Block { get; set; }
    }
}
