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
            var flight = Flights.FlightsList.FirstOrDefault(f => f.Id == reservation.Flight.Id);

            if (flight == null)
            {
                return BadRequest("Flight not found.");
            }

            if (reservation.NumberOfPassengers > flight.AvailableSeats)
            {
                return BadRequest("Not enough available seats.");
            }
            reservation.User.Reservations.Clear();
            Reservations.AddReservation(reservation);
            var user = Users.FindByUsername(reservation.User.Username);
            flight.AvailableSeats -= reservation.NumberOfPassengers;
            flight.BookedSeats += reservation.NumberOfPassengers;
            Flights.SaveFlights();
            if (user != null)
            {
                if (user.Reservations == null)
                {
                    user.Reservations = new List<Reservation>();
                }
                user.Reservations.Add(reservation);
                Users.SaveUsers();
            }
            foreach (var res in Reservations.ReservationsList)
            {
                if (res.Flight.Id == flight.Id)
                {
                    res.Flight.AvailableSeats = flight.AvailableSeats;
                    res.Flight.BookedSeats = flight.BookedSeats;
                }
            }
            Reservations.SaveReservations();

            return Ok();
        }

        [Route("api/reservations/cancel")]
        public IHttpActionResult CancelReservations([FromBody] List<Reservation> reservationsToCancel)
        {
            if (reservationsToCancel == null || reservationsToCancel.Count == 0)
            {
                return BadRequest("No reservations to cancel.");
            }

            try
            {
                foreach (var reservationtoCancel in reservationsToCancel)
                {
                    var reservation = Reservations.ReservationsList.FirstOrDefault(r =>
                        r.Id == reservationtoCancel.Id &&
                        r.Status != ReservationStatus.Cancelled &&
                        r.Status != ReservationStatus.Completed
                    );

                    if (reservation != null)
                    {
                        reservation.Status = ReservationStatus.Cancelled;

                        var flight = Flights.FlightsList.FirstOrDefault(f =>
                            f.Id == reservation.Flight.Id
                        );

                        if (flight != null)
                        {
                            flight.AvailableSeats += reservation.NumberOfPassengers;
                            flight.BookedSeats -= reservation.NumberOfPassengers;
                            reservation.Flight = flight;
                            foreach (var res in Reservations.ReservationsList)
                            {
                                if (res.Flight.Id == flight.Id)
                                {
                                    res.Flight.AvailableSeats = flight.AvailableSeats;
                                    res.Flight.BookedSeats = flight.BookedSeats;
                                }
                            }
                        }
                        else
                        {
                            return BadRequest("Flight associated with reservation not found.");
                        }

                        var user = Users.FindByUsername(reservation.User.Username);
                        if (user != null)
                        {
                            var userReservation = user.Reservations.FirstOrDefault(r =>
                                r.Id == reservationtoCancel.Id &&
                                r.Status != ReservationStatus.Cancelled &&
                                r.Status != ReservationStatus.Completed
                            );

                            if (userReservation != null)
                            {
                                userReservation.Status = ReservationStatus.Cancelled;
                                userReservation.User.Reservations.Clear();
                            }
                        }
                        else
                        {
                            return BadRequest("User associated with reservation not found.");
                        }
                    }
                    else
                    {
                        return BadRequest($"Reservation with id {reservationtoCancel.Id} not found or already cancelled/completed.");
                    }
                }
                Users.SaveUsers();
                Flights.SaveFlights();
                Reservations.SaveReservations();

                return Ok("Reservations successfully cancelled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while cancelling reservations: {ex.Message}");
                return InternalServerError();
            }
        }
    }
}
