﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public enum FlightStatus
    {
        Active, Cancelled, Completed
    }
    public class Flight
    {
        public Airline Airline { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
        public decimal Price { get; set; }
        public FlightStatus Status { get; set; }
    }
}