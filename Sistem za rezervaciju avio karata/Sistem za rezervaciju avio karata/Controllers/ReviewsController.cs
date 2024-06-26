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
        public List<Review> Get()
        {
            return Reviews.ReviewsList;
        }
        [HttpPost]
        [Route("api/reviews/add")]
        public IHttpActionResult AddReview(Review review)
        {
            review.User.Reservations.Clear();
            Reviews.AddReview(review);

            return Ok();
        }

        [HttpPost]
        [Route("api/reviews/update")]
        public IHttpActionResult UpdateReview(Review review)
        {
            var airline = Airlines.AirlinesList.FirstOrDefault(f => f.Id == review.Airline.Id);
            if (airline != null)
            {
                var reviewFromAirline = airline.Reviews.FirstOrDefault(f => f.Id == review.Id);
                if (reviewFromAirline != null)
                {
                    reviewFromAirline.Status = review.Status;
                }
            }

            var reviewToUpdate = Reviews.ReviewsList.FirstOrDefault(f => f.Id == review.Id);
            if (reviewToUpdate != null)
            {
                reviewToUpdate.Status = review.Status;
            }

            Airlines.SaveAirlines();
            Reviews.SaveReviews();
            return Ok();
        }
    }
}
