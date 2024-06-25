using Sistem_za_rezervaciju_avio_karata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sistem_za_rezervaciju_avio_karata.Controllers
{
    public class ReviewsController : ApiController
    {
        [HttpPost]
        [Route("api/reviews/add")]
        public IHttpActionResult AddReview(Review review)
        {
            review.User.Reservations.Clear();
            Reviews.AddReview(review);

            return Ok();
        }
    }
}
