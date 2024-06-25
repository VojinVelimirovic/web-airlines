using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeData();
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
        }
        private void InitializeData()
        {
            Users.UsersList = Users.LoadUsers();
            Flights.FlightsList = Flights.LoadFlights();
            Reservations.ReservationsList = Reservations.LoadReservations();
            Airlines.AirlinesList = Airlines.LoadAirlines();
            Reviews.ReviewsList = Reviews.LoadReviews();
            UpdateStatuses();
        }
        private void UpdateStatuses()
        {
            DateTime now = DateTime.Now;
            bool usersUpdated = false;
            bool flightsUpdated = false;
            bool reservationsUpdated = false;
            foreach (var flight in Flights.FlightsList)
            {
                if (flight.ArrivalDateTime <= now && flight.Status != FlightStatus.Completed)
                {
                    flight.Status = FlightStatus.Completed;
                    flightsUpdated = true;
                }
            }
            foreach (var reservation in Reservations.ReservationsList)
            {
                if (reservation.Flight.ArrivalDateTime <= now && reservation.Status != ReservationStatus.Completed)
                {
                    reservation.Status = ReservationStatus.Completed;
                    reservationsUpdated = true;
                }
            }
            foreach (var user in Users.UsersList)
            {
                foreach (var reservation in user.Reservations)
                {
                    if (reservation.Flight.ArrivalDateTime <= now && reservation.Status != ReservationStatus.Completed)
                    {
                        reservation.Status = ReservationStatus.Completed;
                        usersUpdated = true;
                    }
                }
            }
            if (flightsUpdated)
            {
                Flights.SaveFlights();
            }
            if (reservationsUpdated)
            {
                Reservations.SaveReservations();
            }
            if (usersUpdated)
            {
                Users.SaveUsers();
            }
        }

    }
}
