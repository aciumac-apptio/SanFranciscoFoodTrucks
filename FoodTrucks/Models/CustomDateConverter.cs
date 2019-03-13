using Newtonsoft.Json;
using System;

namespace FoodTrucks.Models
{
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
}
