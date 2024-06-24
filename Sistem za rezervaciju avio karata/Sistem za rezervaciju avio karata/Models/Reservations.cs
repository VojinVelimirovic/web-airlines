using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Reservations
    {
        private static readonly string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/reservations.json");
        public static List<Reservation> ReservationsList { get; set; }
        public static List<Reservation> LoadReservations()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<Reservation>();
            }

            var json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<Reservation>>(json) ?? new List<Reservation>();
        }

        public static void SaveReservations()
        {
            var json = JsonConvert.SerializeObject(ReservationsList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public static Reservation AddReservation(Reservation reservation)
        {
            if(ReservationsList == null || ReservationsList.Count == 0)
            {
                reservation.Id = 1;
            }
            else
            {
                reservation.Id = ReservationsList.Max(r => r.Id) + 1;
            }
            
            ReservationsList.Add(reservation);
            SaveReservations();
            return reservation;
        }

        public static void RemoveReservation(Reservation reservation)
        {
            ReservationsList.Remove(reservation);
            SaveReservations();
        }
    }
}