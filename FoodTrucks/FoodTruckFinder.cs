using FoodTrucks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodTrucks
{
    class FoodTruckFinder
    {
        public static async Task ShowOpenFoodTrucksAsync(string apiEndpoint, string requestUri)
        {
            List<FoodTruck> foodTrucks;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiEndpoint);
                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    foodTrucks = JsonConvert.DeserializeObject<List<FoodTruck>>(result);
                }
            }

            // Keep only trucks that are open (current time is between the start and end time) on the current day of the week
            foodTrucks = foodTrucks
                .Where(f => (int)DateTime.Now.DayOfWeek == f.Dayorder && f.Start24.CompareTo(DateTime.Now) <= 0 && f.End24.CompareTo(DateTime.Now) >= 0)
                .OrderBy(f => f.Applicant)
                .ThenBy(f => f.Location)
                .ToList();

            // Print results out
            Console.WriteLine($"Total # of food trucks open: {foodTrucks.Count}");
            int currentCursor = 0;
            do
            {
                Console.WriteLine($"\n#\t{"NAME",-50:D}\tADDRESS");
                int count = 0;
                while (currentCursor < foodTrucks.Count && count < 10)
                {
                    Console.WriteLine($"{currentCursor + 1}\t{foodTrucks[currentCursor].Applicant,-50:D}\t{foodTrucks[currentCursor].Location}");
                    currentCursor++;
                    count++;
                }

                if (currentCursor < foodTrucks.Count)
                {
                    Console.WriteLine("\nPlease press any key to see more food trucks that are open right now or ESCAPE to exit ...");
                }
                else
                {
                    Console.WriteLine("\nDone. No more food trucks open for business can be found.");
                }                

            } while (currentCursor < foodTrucks.Count && Console.ReadKey().Key != ConsoleKey.Escape);            
        }
    }
}
