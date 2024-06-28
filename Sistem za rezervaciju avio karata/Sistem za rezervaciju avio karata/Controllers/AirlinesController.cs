using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata.Controllers
{
    public class AirlinesController : ApiController
    {
        public List<Airline> Get()
        {
            return Airlines.AirlinesList;
        }
        [HttpPost]
        [Route("api/airlines/add")]
        public IHttpActionResult Add(Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }
            return Ok(Airlines.AddAirline(airline));
        }

        [HttpPost]
        [Route("api/airlines/remove")]
        public IHttpActionResult Remove(Airline airline)
        {
            if (airline == null || airline.Id <= 0)
            {
                return BadRequest("Invalid airline.");
            }

            var existingAirline = Airlines.AirlinesList.FirstOrDefault(a => a.Id == airline.Id);
            if (existingAirline == null)
            {
                return BadRequest("Airline not found.");
            }

            Airlines.RemoveAirline(existingAirline);
            return Ok("Airline removed successfully.");
        }

        [HttpPost]
        [Route("api/airlines/update")]
        public IHttpActionResult UpdateAirline(Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }

            var existingAirline = Airlines.AirlinesList.FirstOrDefault(a => a.Id == airline.Id);
            if (existingAirline == null)
            {
                return BadRequest("Airline not found.");
            }

            Airlines.UpdateAirline(airline);
            return Ok("Airline updated successfully.");
        }
    }
}
