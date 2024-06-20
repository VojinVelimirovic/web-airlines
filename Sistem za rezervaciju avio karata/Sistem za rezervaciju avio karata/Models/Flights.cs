using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Flights
    {
        public static List<Flight> FlightsList { get; set; } = new List<Flight>() {
            new Flight { Airline = new Airline { Name = "Nikola Tesla Airlines"}, From = "Belgrade", Destination = "Dubai", DepartureDateTime = DateTime.Now,
            ArrivalDateTime = DateTime.Now, AvailableSeats = 70, BookedSeats = 32, Price = "7000RSD", Status = FlightStatus.Active},
            new Flight { Airline = new Airline { Name = "Budapest Airlines"}, From = "Budapest", Destination = "New York", DepartureDateTime = DateTime.Now,
            ArrivalDateTime = DateTime.Now, AvailableSeats = 70, BookedSeats = 32, Price = "7500RSD", Status = FlightStatus.Cancelled},
            new Flight { Airline = new Airline { Name = "Dubai Airlines"}, From = "Dubai", Destination = "Belgrade", DepartureDateTime = DateTime.Now,
            ArrivalDateTime = DateTime.Now, AvailableSeats = 70, BookedSeats = 32, Price = "8000RSD", Status = FlightStatus.Active}
        };
        public static Flight AddFlight(Flight flight)
        {
            FlightsList.Add(flight);
            return flight;
        }
        public static void RemoveFlight(Flight flight)
        {
            FlightsList.Remove(flight);
        }
    }
}