namespace BalkanAir.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User User
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        protected int NumberOfUnreadNotifications
        {
            get { return this.User.NumberOfUnreadNotifications; }
        }

        public IEnumerable<UserNotification> LatestNotificationsRepeater_GetData()
        {
            return this.UserNotificationsServices.GetAll()
                .Where(un => un.UserId.Equals(this.User.Id))
                .OrderByDescending(un => un.DateReceived)
                .Take(5)
                .ToList();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;

            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };

                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }

                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SiteMapPathBreadcrump.Visible = (SiteMap.CurrentNode != SiteMap.RootNode);

                // TODO: Check for admin
                if (this.User != null)
                {
                    this.AdministrationMenu.Visible = true;

                    if (!this.User.EmailConfirmed)
                    {
                        this.EmailNotConfirmedPanel.Visible = true;
                    }
                }

                this.ManageItineraryInfo();
            }
        }

        protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void SendAnotherConfirmationEmailLinkButton_Click(object sender, EventArgs e)
        {
            string code = this.Manager.GenerateEmailConfirmationToken(this.User.Id);
            string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, this.User.Id, Request);

            string messageBody = "Hello, " + this.User.Email.Trim() + ",";
            messageBody += "<br /><br />Please click the following link to confirm your account!";
            messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to confirm your account.</a>";

            var mailSender = MailSender.Instance;
            mailSender.SendMail(this.User.Email, "Confirm your account!", messageBody);
        }

        protected string GetUserInfo()
        {
            if (!string.IsNullOrEmpty(this.User.UserSettings.FirstName) && !string.IsNullOrEmpty(this.User.UserSettings.LastName))
            {
                return this.User.UserSettings.FirstName + " " + this.User.UserSettings.LastName;
            }

            return this.User.Email;
        }

        protected void MarkAllNotificationsAsReadBtn_Click(object sender, EventArgs e)
        {
            this.UserNotificationsServices.SetAllNotificationsAsRead(this.User.Id);
        }

        private void ManageItineraryInfo()
        {
            if (this.Session[Parameters.DEPARTURE_AIRPORT_ID] == null || 
                this.Session[Parameters.DESTINATION_AIRPORT_ID] == null)
            {
                return;
            }

            this.SetAirprotsInfoToItinerary();

            decimal totalCost = 0;

            if (this.Session[Parameters.BOOKING] != null)
            {
                var booking = this.Session[Parameters.BOOKING] as Data.Models.Booking;
                this.SetSelectedFlightToItinerary(booking, ref totalCost);

                if (booking.Row > 0 && !string.IsNullOrEmpty(booking.SeatNumber))
                {
                    this.SeatLiteral.Text = booking.Row.ToString() + booking.SeatNumber;
                }

                if (booking.Baggages.Count > 0)
                {
                    this.SetCabinBagToItinerary(booking, ref totalCost);
                    this.SetCheckedInBagsToItinerary(booking, ref totalCost);

                    this.SetBagEquipmentsToItinerary(booking, ref totalCost, BaggageType.BabyEquipment,
                        this.BabyEquipmentLiteral, this.BabyEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(booking, ref totalCost, BaggageType.SportsEquipment,
                        this.SportsEquipmentLiteral, this.SportsEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(booking, ref totalCost, BaggageType.MusicEquipment,
                        this.MusicEquipmentLiteral, this.MusicEquipmentPriceLiteral);
                }
            }

            if (this.User != null && !string.IsNullOrEmpty(this.User.UserSettings.FirstName) &&
                !string.IsNullOrEmpty(this.User.UserSettings.LastName))
            {
                this.PassengerNameLiteral.Text = this.User.UserSettings.FirstName + " " + this.User.UserSettings.LastName;
            }

            this.TotalCostLiberal.Text = "&#8364; " + string.Format("{0:0.00}", totalCost);
            this.ItineraryInfoPanel.Visible = true;
        }

        private void SetAirprotsInfoToItinerary()
        {
            int departureAirportId = int.Parse(this.Session[Parameters.DEPARTURE_AIRPORT_ID].ToString());
            int destinationAirportId = int.Parse(this.Session[Parameters.DESTINATION_AIRPORT_ID].ToString());

            var departureAirport = this.AirportsServices.GetAirport(departureAirportId);
            var destinationAirport = this.AirportsServices.GetAirport(destinationAirportId);

            this.FromAirportToAirportLiteral.Text = departureAirport.Name + " (" + departureAirport.Abbreviation + ") to " +
                destinationAirport.Name + " (" + destinationAirport.Abbreviation + ")";
        }

        private void SetSelectedFlightToItinerary(Data.Models.Booking booking, ref decimal totalCost)
        {
            Flight selectedFlight = this.FlightsServices.GetFlight(booking.FlightId);

            this.FlightNuberLiteral.Text = selectedFlight.Number;
            this.DateTimeLiteral.Text = selectedFlight.Departure.ToString("MMMM dd, yyyy hh:mm", CultureInfo.InvariantCulture);

            decimal flightPrice = this.TravelClassesServices.GetTravelClass(booking.TravelClassId).Price;
            this.FlightPriceLiteral.Text = "&#8364; " + string.Format("{0:0.00}", flightPrice);
            totalCost += flightPrice;

            this.TravelClassLiteral.Text = this.TravelClassesServices.GetTravelClass(booking.TravelClassId).Type.ToString() + " class";
        }

        private void SetCabinBagToItinerary(Data.Models.Booking booking, ref decimal totalCost)
        {
            var cabinBag = booking.Baggages
                       .FirstOrDefault(b => b.Type == BaggageType.Cabin);

            if (cabinBag != null)
            {
                this.CabinBagLiteral.Text = "One cabin bag " + cabinBag.Size;
                this.CabinBagPriceLiteral.Text = "&#8364; " + string.Format("{0:0.00}", cabinBag.Price);
                totalCost += cabinBag.Price;
            }
        }

        private void SetCheckedInBagsToItinerary(Data.Models.Booking booking, ref decimal totalCost)
        {
            var checkedInBags = booking.Baggages
                        .Where(b => b.Type == BaggageType.CheckedIn)
                        .ToList();

            if (checkedInBags.Count > 0)
            {
                this.CheckedIdBagsLiteral.Text = "Checked in bags " + checkedInBags.Count + " x " + checkedInBags[0].MaxKilograms + " kg. ";
                decimal checkedInBagsPriceSum = checkedInBags.Sum(b => b.Price);
                this.CheckedIdBagsPricesLiteral.Text = "&#8364; " + string.Format("{0:0.00}", checkedInBagsPriceSum);
                totalCost += checkedInBagsPriceSum;
            }
        }

        // Set baby, sports and music equipments to the itenerary info panel. 
        private void SetBagEquipmentsToItinerary(Data.Models.Booking booking, ref decimal totalCost, BaggageType baggageType, 
            Literal literalInfo, Literal literalPrice)
        {
            var equipment = booking.Baggages
                        .FirstOrDefault(b => b.Type == baggageType);

            if (equipment != null)
            {
                literalInfo.Text = string.Format("{0} equipment included!", baggageType.ToString());
                literalPrice.Text = "&#8364; " + string.Format("{0:0.00}", equipment.Price);
                totalCost += equipment.Price;
            }
        }
    }
}
