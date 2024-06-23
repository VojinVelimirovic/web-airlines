using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata.Controllers
{
    public class ReservationsController : ApiController
    {
        [HttpPost]
        [Route("api/reservations")]
        public IHttpActionResult CreateReservation([FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Invalid reservation data.");
            }
            var flight = Flights.FlightsList.FirstOrDefault(f =>
                f.Airline.Name == reservation.Flight.Airline.Name &&
                f.From == reservation.Flight.From &&
                f.Destination == reservation.Flight.Destination &&
                f.DepartureDateTime == reservation.Flight.DepartureDateTime
            );

            if (flight == null)
            {
                return BadRequest("Flight not found.");
            }

            if (reservation.NumberOfPassengers > flight.AvailableSeats)
            {
                return BadRequest("Not enough available seats.");
            }
            Reservations.ReservationsList.Add(reservation);
            var user = Users.FindByUsername(reservation.User.Username);
            if (user != null)
            {
                if (user.Reservations == null)
                {
                    user.Reservations = new List<Reservation>();
                }
                user.Reservations.Add(reservation);
                Users.SaveUsers();
            }
            flight.AvailableSeats -= reservation.NumberOfPassengers;
            flight.BookedSeats += reservation.NumberOfPassengers;
            Flights.SaveFlights();
            Reservations.SaveReservations();

            return Ok();
        }
    }
}
