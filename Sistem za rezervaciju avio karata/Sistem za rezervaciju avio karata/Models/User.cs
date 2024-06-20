using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public enum UserType { Traveler, Admin}
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public UserType UserType { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}