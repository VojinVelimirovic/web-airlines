using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Airline
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public List<Flight> Flights { get; set; }
        public List<Review> Reviews { get; set; }
    }
}