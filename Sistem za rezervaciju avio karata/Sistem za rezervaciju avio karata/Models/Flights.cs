using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Flights
    {
        private static readonly string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/flights.json");
        public static List<Flight> FlightsList { get; set; }
        public static List<Flight> LoadFlights()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<Flight>();
            }

            var json = File.ReadAllText(jsonFilePath);
            var retVal = JsonConvert.DeserializeObject<List<Flight>>(json) ?? new List<Flight>();
            return retVal;
        }

        public static void SaveFlights()
        {
            var json = JsonConvert.SerializeObject(FlightsList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public static Flight AddFlight(Flight flight)
        {
            FlightsList.Add(flight);
            Airline a = Airlines.FindAirline(flight.Airline.Name);
            a.AddFlight(flight);
            SaveFlights();
            return flight;
        }

        public static void RemoveFlight(Flight flight)
        {
            FlightsList.Remove(flight);
            Airline a = Airlines.FindAirline(flight.Airline.Name);
            a.RemoveFlight(flight);
            SaveFlights();
        }

        public static Flight UpdateFlight(Flight updatedFlight)
        {
            var existingFlight = FlightsList.FirstOrDefault(f =>
                f.Airline.Name == updatedFlight.Airline.Name &&
                f.From == updatedFlight.From &&
                f.Destination == updatedFlight.Destination &&
                f.DepartureDateTime == updatedFlight.DepartureDateTime
            );

            if (existingFlight != null)
            {
                existingFlight.AvailableSeats = updatedFlight.AvailableSeats;
                existingFlight.BookedSeats = updatedFlight.BookedSeats;
                existingFlight.Status = updatedFlight.Status;
                existingFlight.Price = updatedFlight.Price;
                SaveFlights();
            }

            return existingFlight;
        }
    }
}