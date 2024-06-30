using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata.Controllers
{
    public class FlightsController : ApiController
    {
        public List<Flight> Get()
        {
            return Flights.FlightsList;
        }
        public IHttpActionResult Post(Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }
            return Ok(Flights.AddFlight(flight));
        }
        [HttpPost]
        [Route("api/flights/remove")]
        public IHttpActionResult Remove(Flight flight)
        {
            if (flight == null || flight.Id <= 0)
            {
                return BadRequest("Invalid flight.");
            }

            var existingflight = Flights.FlightsList.FirstOrDefault(a => a.Id == flight.Id);
            if (existingflight == null)
            {
                return BadRequest("Flight not found.");
            }

            Flights.RemoveFlight(existingflight);
            return Ok("Flight removed successfully.");
        }

        [HttpPost]
        [Route("api/flights/add")]
        public IHttpActionResult Add(Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }
            return Ok(Flights.AddFlight(flight));
        }

        [HttpPost]
        [Route("api/flights/update")]
        public IHttpActionResult Update(Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }
            var existingFlight = Flights.FlightsList.FirstOrDefault(a => a.Id == flight.Id);
            if (existingFlight == null)
            {
                return BadRequest("Flight not found.");
            }
            Flights.UpdateFlight(flight);
            return Ok("Flight updated successfully.");
        }
    }
}