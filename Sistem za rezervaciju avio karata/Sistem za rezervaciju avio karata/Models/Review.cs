using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public enum ReviewStatus
    {
        Created, Approved, Rejected
    }
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Airline Airline { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ReviewStatus Status { get; set; }
        public string Image { get; set; }
    }
}