using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MetaWeatherDemo.Models;
using MetaWeatherDemo.ViewModels.Weather;
using System.Configuration;
using Newtonsoft.Json;

namespace MetaWeatherDemo.Helpers
{
    public static class WeatherHelper
    {

        // Retrieve data through MetaWeather API to get 5-day forecast for specific location
        private static string GetData(int woeid)
        {
            string str = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create(ConfigurationManager.AppSettings["MetaWeatherBaseURL"] + woeid);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                str = reader.ReadLine();
            }
            catch (Exception e)
            {
                throw new Exception("Error:", e.InnerException);
            }
            return str;
        }

        // Populate a model with the JSON data containing the 5-day forecast for specific location
        private static ConsolidatedWeatherModel Fetch5DaysForecast(int woeid)
        {
            // Get JSON data
            string jsonData = GetData(woeid);

            // Extract the data
            var jsonObject = JsonConvert.DeserializeObject<Models.ConsolidatedWeatherModel>(jsonData);

            return jsonObject;
        }

        // Return the model containing 5-day forecast for specific location
        public static ConsolidatedWeatherModel GetForecast(int woeid)
        {               
            return woeid > 0 ? Fetch5DaysForecast(woeid) : null;
        }
    }
}