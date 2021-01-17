using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetaWeatherDemo.Models;
using MetaWeatherDemo.ViewModels.Weather;
using MetaWeatherDemo.Helpers;
using System.Globalization;

namespace MetaWeatherDemo.Controllers
{
    public class WeatherController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            // Get 5-day weather forecast for Belfast, use the correct Where-On-Earth ID
            int woeid = 44544;

            // Setup view model 
            ConsolidatedWeatherViewModel model = new ConsolidatedWeatherViewModel();

            // Get the weather data for this location
            ConsolidatedWeatherModel consolidatedWeatherData = new ConsolidatedWeatherModel();
            consolidatedWeatherData = WeatherHelper.GetForecast(woeid);

            // Copy data to view model
            if (consolidatedWeatherData != null)
            {
                model.consolidated_weather = consolidatedWeatherData.consolidated_weather;
                model.latt_long = consolidatedWeatherData.latt_long;
                model.location_type = consolidatedWeatherData.location_type;
                model.parent = consolidatedWeatherData.parent;
                model.sources = consolidatedWeatherData.sources;
                model.sun_rise = consolidatedWeatherData.sun_rise;
                model.sun_set = consolidatedWeatherData.sun_set;
                model.time = consolidatedWeatherData.time;
                model.timezone = consolidatedWeatherData.timezone;
                model.timezone_name = consolidatedWeatherData.timezone_name;
                model.title = consolidatedWeatherData.title;
                model.woeid = consolidatedWeatherData.woeid;
            }

            // Setup the date variables to look nice on the view
            model.consolidated_weather.ElementAt(0).applicable_date = "Today";
            model.consolidated_weather.ElementAt(1).applicable_date = "Tomorrow";
            for (int i = 2; i < model.consolidated_weather.Count; i++)
            {
                // Convert to DateTime
                DateTime dt = DateTime.ParseExact(model.consolidated_weather.ElementAt(i).applicable_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                // Setup a nice readable date format
                model.consolidated_weather.ElementAt(i).applicable_date = dt.ToString("ddd dd MMM", DateTimeFormatInfo.InvariantInfo);
            }

            // Setup the wind direction
            for (int i = 0; i < model.consolidated_weather.Count; i++)
            {
                string windDirection = "";

                // Check wind direction abbrevation and setup HTML symbol
                switch (model.consolidated_weather.ElementAt(i).wind_direction_compass.ToUpper())
                {
                    case "N":
                        windDirection = "&#8593;"; //up arrow
                        break;

                    case "NNW":
                    case "NW":
                    case "WNW":
                        windDirection = "&#8598;"; // up-left arrow
                        break;

                    case "W":
                        windDirection = "&#8592;"; // left arrow
                        break;

                    case "WSW":
                    case "SW":
                    case "SSW":
                        windDirection = "&#8601;"; // down-left arrow
                        break;

                    case "S":
                        windDirection = "&#8595;"; // down arrow
                        break;

                    case "ESE":
                    case "SE":
                    case "SSE":
                        windDirection = "&#8600;"; // down-right arrow
                        break;

                    case "E":
                        windDirection = "&#8594;"; // right arrow
                        break;

                    case "NNE":
                    case "NE":
                    case "ENE":
                        windDirection = "&#8599;"; // up-right arrow
                        break;
                }

                // Set wind direction HTML symbol
                model.consolidated_weather.ElementAt(i).wind_direction_compass = windDirection;
            }

            // Setup the wind SVG picture URL                       
            for (int i = 0; i < model.consolidated_weather.Count; i++)
            {
                model.consolidated_weather.ElementAt(i).weather_SVG_URL = String.Format("https://www.metaweather.com/static/img/weather/png/{0}.png", model.consolidated_weather.ElementAt(i).weather_state_abbr.ToLower());
            }

            return View(model);
        }
    }
}