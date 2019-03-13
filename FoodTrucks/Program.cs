using System;

namespace FoodTrucks
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string apiEndpoint = "http://data.sfgov.org/resource/";
                string requestUri = "bbb8-hzi6.json";

                FoodTruckFinder.ShowOpenFoodTrucksAsync(apiEndpoint, requestUri).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failled to find Food Tracks. Internal Error: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}
