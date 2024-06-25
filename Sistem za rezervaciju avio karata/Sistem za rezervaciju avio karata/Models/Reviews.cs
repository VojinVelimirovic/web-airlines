using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Reviews
    {
        private static readonly string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/reviews.json");
        public static List<Review> ReviewsList { get; set; }
        public static List<Review> LoadReviews()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<Review>();
            }

            var json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<Review>>(json) ?? new List<Review>();
        }

        public static void SaveReviews()
        {
            var json = JsonConvert.SerializeObject(ReviewsList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public static Review AddReview(Review review)
        {
            if (ReviewsList == null || ReviewsList.Count == 0)
            {
                review.Id = 1;
            }
            else
            {
                review.Id = ReviewsList.Max(r => r.Id) + 1;
            }

            ReviewsList.Add(review);
            foreach(Review r in ReviewsList)
            {
                r.Airline.Reviews.Clear();
            }
            var airline = Airlines.AirlinesList.FirstOrDefault(f => f.Id == review.Airline.Id);
            if (airline != null)
            {
                airline.Reviews.Add(review);
            }
            Airlines.SaveAirlines();
            SaveReviews();
            return review;
        }

        public static void RemoveReview(Review review)
        {
            ReviewsList.Remove(review);

            var airline = Airlines.AirlinesList.FirstOrDefault(f => f.Id == review.Airline.Id);
            if (airline != null)
            {
                var reviewToRemove = airline.Reviews.FirstOrDefault(r => r.Id == review.Id);
                if (reviewToRemove != null)
                {
                    airline.Reviews.Remove(reviewToRemove);
                }
            }

            SaveReviews();
            Airlines.SaveAirlines();
        }
    }
}