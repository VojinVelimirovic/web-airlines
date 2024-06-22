using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Airlines
    {
        private static readonly string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/airlines.json");
        public static List<Airline> AirlinesList { get; set; } = LoadAirlines();
        private static List<Airline> LoadAirlines()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<Airline>();
            }

            var json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<Airline>>(json) ?? new List<Airline>();
        }

        public static void SaveAirlines()
        {
            var json = JsonConvert.SerializeObject(AirlinesList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public static Airline FindAirline(string airlineName)
        {
            return AirlinesList.FirstOrDefault(a => a.Name == airlineName);
        }

        public static Airline AddAirline(Airline airline)
        {
            if(airline.Reviews == null)
            {
                airline.Reviews = new List<Review>();
            }
            if (airline.Flights == null)
            {
                airline.Flights = new List<Flight>();
            }
            AirlinesList.Add(airline);
            SaveAirlines();
            return airline;
        }

        public static void RemoveAirline(Airline airline)
        {
            AirlinesList.Remove(airline);
            SaveAirlines();
        }
    }
}