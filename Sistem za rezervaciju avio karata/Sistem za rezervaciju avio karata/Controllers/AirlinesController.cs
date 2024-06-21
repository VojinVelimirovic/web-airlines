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
        public IHttpActionResult Post(Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }
            return Ok(Airlines.AddAirline(airline));
        }
    }
}
