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
            if (FlightsList == null || FlightsList.Count == 0)
            {
                flight.Id = 1;
            }
            else
            {
                flight.Id = FlightsList.Max(r => r.Id) + 1;
            }
            flight.IsDeleted = false;
            flight.Status = FlightStatus.Active;
            flight.BookedSeats = 0;
            flight.Airline.Flights.Clear();
            flight.Airline.Reviews.Clear();
            FlightsList.Add(flight);
            Airline a = Airlines.FindAirline(flight.Airline.Id);
            a.AddFlight(flight);
            SaveFlights();
            return flight;
        }

        public static void RemoveFlight(Flight flightToRemove)
        {
            var flight = FlightsList.FirstOrDefault(f => f.Id == flightToRemove.Id);
            flight.IsDeleted = true;
            foreach (var user in Users.UsersList)
            {
                foreach (var reservation in user.Reservations)
                {
                    if (reservation.Flight.Id == flightToRemove.Id)
                    {
                        reservation.Flight.IsDeleted = true;
                    }
                }
            }
            foreach (var reservation in Reservations.ReservationsList)
            {
                if (reservation.Flight.Id == flightToRemove.Id)
                {
                    reservation.Flight.IsDeleted = true;
                }
            }
            foreach (var airline in Airlines.AirlinesList)
            {
                foreach (var airlineFlight in airline.Flights)
                {
                    if (airlineFlight.Id == flightToRemove.Id)
                    {
                        airlineFlight.IsDeleted = true;
                    }
                }
            }
            SaveFlights();
            Users.SaveUsers();
            Reservations.SaveReservations();
            Airlines.SaveAirlines();
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