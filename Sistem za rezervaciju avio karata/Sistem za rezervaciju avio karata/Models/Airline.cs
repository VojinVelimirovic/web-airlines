using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        
        public bool IsDeleted { get; set; }
        public List<Flight> Flights { get; set; }
        public List<Review> Reviews { get; set; }

        public void AddFlight(Flight flight)
        {
            if(Flights == null)
            {
                Flights = new List<Flight>();
            }
            Flights.Add(flight);
            Airlines.SaveAirlines();
        }

        public void RemoveFlight(Flight flight)
        {
            Flights.Remove(flight);
            Airlines.SaveAirlines();
        }
    }
}