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
        public static List<Airline> AirlinesList { get; set; }
        public static List<Airline> LoadAirlines()
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

        public static Airline FindAirline(int airlineId)
        {
            return AirlinesList.FirstOrDefault(a => a.Id == airlineId);
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
            if (AirlinesList == null || AirlinesList.Count == 0)
            {
                airline.Id = 1;
            }
            else
            {
                airline.Id = AirlinesList.Max(r => r.Id) + 1;
            }
            airline.IsDeleted = false;
            AirlinesList.Add(airline);
            SaveAirlines();
            return airline;
        }

        public static void RemoveAirline(Airline air)
        {
            var airline = Airlines.AirlinesList.FirstOrDefault(a => a.Id == air.Id);
            if (airline == null)
            {
                throw new Exception("Airline not found");
            }
            airline.IsDeleted = true;
            foreach (var flight in airline.Flights)
            {
                flight.Airline.IsDeleted = true;
            }
            foreach (var reservation in Reservations.ReservationsList)
            {
                if (reservation.Flight.Airline.Id == air.Id)
                {
                    reservation.Flight.Airline.IsDeleted = true;
                }
            }
            foreach (var flight in Flights.FlightsList)
            {
                if (flight.Airline.Id == air.Id)
                {
                    flight.Airline.IsDeleted = true;
                }
            }
            foreach (var review in Reviews.ReviewsList)
            {
                if (review.Airline.Id == air.Id)
                {
                    review.Airline.IsDeleted = true;
                }
            }
            foreach (var user in Users.UsersList)
            {
                foreach (var reservation in user.Reservations)
                {
                    if (reservation.Flight.Airline.Id == air.Id)
                    {
                        reservation.Flight.Airline.IsDeleted = true;
                    }
                }
            }
            Airlines.SaveAirlines();
            Reservations.SaveReservations();
            Flights.SaveFlights();
            Reviews.SaveReviews();
            Users.SaveUsers();
        }

        public static void UpdateAirline(Airline air)
        {
            var airline = Airlines.AirlinesList.FirstOrDefault(a => a.Id == air.Id);
            if (airline == null)
            {
                throw new Exception("Airline not found");
            }
            airline.Name = air.Name;
            airline.Address = air.Address;
            airline.ContactInfo = air.ContactInfo;
            foreach (var flight in airline.Flights)
            {
                flight.Airline.Name = air.Name;
                flight.Airline.Address = air.Address;
                flight.Airline.ContactInfo = air.ContactInfo;
            }
            foreach (var reservation in Reservations.ReservationsList)
            {
                if (reservation.Flight.Airline.Id == air.Id)
                {
                    reservation.Flight.Airline.Name = air.Name;
                    reservation.Flight.Airline.Address = air.Address;
                    reservation.Flight.Airline.ContactInfo = air.ContactInfo;
                }
            }
            foreach (var flight in Flights.FlightsList)
            {
                if (flight.Airline.Id == air.Id)
                {
                    flight.Airline.Name = air.Name;
                    flight.Airline.Address = air.Address;
                    flight.Airline.ContactInfo = air.ContactInfo;
                }
            }
            foreach (var review in Reviews.ReviewsList)
            {
                if (review.Airline.Id == air.Id)
                {
                    review.Airline.Name = air.Name;
                    review.Airline.Address = air.Address;
                    review.Airline.ContactInfo = air.ContactInfo;
                }
            }
            foreach (var user in Users.UsersList)
            {
                foreach (var reservation in user.Reservations)
                {
                    if (reservation.Flight.Airline.Id == air.Id)
                    {
                        reservation.Flight.Airline.Name = air.Name;
                        reservation.Flight.Airline.Address = air.Address;
                        reservation.Flight.Airline.ContactInfo = air.ContactInfo;
                    }
                }
            }
            Airlines.SaveAirlines();
            Reservations.SaveReservations();
            Flights.SaveFlights();
            Reviews.SaveReviews();
            Users.SaveUsers();
        }
    }
}