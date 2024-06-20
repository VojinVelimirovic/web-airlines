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
    }
}