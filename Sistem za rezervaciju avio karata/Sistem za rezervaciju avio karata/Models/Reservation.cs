using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public enum ReservationStatus
    {
        Created, Approved, Cancelled, Completed
    }
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Flight Flight { get; set; }
        public int NumberOfPassengers { get; set; }
        public int TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
    }
}