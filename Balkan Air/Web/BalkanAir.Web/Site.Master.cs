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

    using Auth;
    using BalkanAir.Common;
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
        public IFaresServices FaresServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

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

        private Data.Models.Booking OneWayRouteBooking
        {
            get
            {
                return (Data.Models.Booking)this.Session[Common.Constants.ONE_WAY_ROUTE_BOOKING];
            }
        }

        private Data.Models.Booking ReturnRouteBooking
        {
            get
            {
                return (Data.Models.Booking)this.Session[Common.Constants.RETURN_ROUTE_BOOKING];
            }
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

            if (this.User != null && this.User.DoesAdminForcedLogoff)
            {
                this.UsersServices.SetLogoffForUser(this.User.Id, false);
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                this.Response.Redirect(Pages.HOME);
            }

            if (!IsPostBack)
            {
                this.SiteMapPathBreadcrump.Visible = (SiteMap.CurrentNode != SiteMap.RootNode);

                if (this.User != null && this.Manager.IsInRole(this.User.Id, UserRolesConstants.ADMINISTRATOR_ROLE))
                {
                    this.AdministrationMenu.Visible = true;
                }

                if (this.User != null && !this.User.EmailConfirmed)
                {
                    this.EmailNotConfirmedPanel.Visible = true;
                }

                string curretnUrl = HttpContext.Current.Request.Url.AbsolutePath;

                // Show itinerary info during flight booking.
                if ((curretnUrl == base.Page.ResolveUrl(Pages.SELECT_FLIGHT) ||
                    curretnUrl == base.Page.ResolveUrl(Pages.EXTRAS) ||
                    curretnUrl == base.Page.ResolveUrl(Pages.SELECT_SEAT) ||
                    curretnUrl == base.Page.ResolveUrl(Pages.PAYMENT)) &&
                    (this.Session[Common.Constants.DEPARTURE_AIRPORT_ID] != null &&
                    this.Session[Common.Constants.DESTINATION_AIRPORT_ID] != null))
                {
                    this.ManageItineraryInfo();
                }
                else
                {
                    this.Session.Clear();
                }
            }
        }

        protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            this.UsersServices.SetLastLogout(this.User.Email, DateTime.Now);
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

        protected string GetProfileIconSrc()
        {
            if (this.User != null && this.User.UserSettings.ProfilePicture != null)
            {
                return "data:image/jpeg;base64," + Convert.ToBase64String(this.User.UserSettings.ProfilePicture);
            }

            return string.Empty;
        }

        protected string GetUserInfo()
        {
            if (!string.IsNullOrEmpty(this.User.UserSettings.FirstName))
            {
                return this.User.UserSettings.FirstName;
            }

            return this.User.Email;
        }

        protected void MarkAllNotificationsAsReadBtn_Click(object sender, EventArgs e)
        {
            this.UserNotificationsServices.SetAllNotificationsAsRead(this.User.Id);
        }

        private void ManageItineraryInfo()
        {
            this.SetAirprotsInfoToItinerary();

            decimal totalCost = 0;

            if (this.OneWayRouteBooking != null)
            {
                this.SetItineraryForBooking(this.OneWayRouteBooking, ref totalCost, this.OutboundFlightSeatLiteral,
                this.OutboundFlightNumberLiteral, this.OutboundFlightDateTimeLiteral, this.OutboundFlightPriceLiteral,
                this.OutboundFlightTravelClassPrice, this.OutboundFlightTravelClassLiteral);

                if (this.OneWayRouteBooking.Baggage.Count > 0)
                {
                    this.SetCabinBagToItinerary(this.OneWayRouteBooking, ref totalCost, this.OutboundFlightCabinBagLiteral,
                    this.OutboundFlightCabinBagPriceLiteral);

                    this.SetCheckedInBagsToItinerary(this.OneWayRouteBooking, ref totalCost, this.OutboundFlightCheckedIdBagsLiteral,
                        this.OutboundFlightCheckedIdBagsPricesLiteral);

                    this.SetBagEquipmentsToItinerary(this.OneWayRouteBooking, ref totalCost, BaggageType.BabyEquipment,
                        this.OutboundFlightBabyEquipmentLiteral, this.OutboundFlightBabyEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(this.OneWayRouteBooking, ref totalCost, BaggageType.SportsEquipment,
                        this.OutboundFlightSportsEquipmentLiteral, this.OutboundFlightSportsEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(this.OneWayRouteBooking, ref totalCost, BaggageType.MusicEquipment,
                        this.OutboundFlightMusicEquipmentLiteral, this.OutboundFlightMusicEquipmentPriceLiteral);
                }
            }

            if (this.ReturnRouteBooking != null)
            {
                this.SetItineraryForBooking(this.ReturnRouteBooking, ref totalCost, this.ReturnFlightSeatLiteral,
                this.ReturnFlightNumberLiteral, this.ReturnFlightDateTimeLiteral, this.ReturnFlightPriceLiteral,
                this.ReturnFlightTravelClassPrice, this.ReturnFlightTravelClassLiteral);

                if (this.ReturnRouteBooking.Baggage.Count > 0)
                {
                    this.SetCabinBagToItinerary(this.ReturnRouteBooking, ref totalCost, this.ReturnFlightCabinBagLiteral,
                    this.ReturnFlightCabinBagPriceLiteral);

                    this.SetCheckedInBagsToItinerary(this.ReturnRouteBooking, ref totalCost, this.ReturnFlightCheckedIdBagsLiteral,
                        this.ReturnFlightCheckedIdBagsPricesLiteral);

                    this.SetBagEquipmentsToItinerary(this.ReturnRouteBooking, ref totalCost, BaggageType.BabyEquipment,
                        this.ReturnFlightBabyEquipmentLiteral, this.ReturnFlightBabyEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(this.ReturnRouteBooking, ref totalCost, BaggageType.SportsEquipment,
                        this.ReturnFlightSportsEquipmentLiteral, this.ReturnFlightSportsEquipmentPriceLiteral);

                    this.SetBagEquipmentsToItinerary(this.ReturnRouteBooking, ref totalCost, BaggageType.MusicEquipment,
                        this.ReturnFlightMusicEquipmentLiteral, this.ReturnFlightMusicEquipmentPriceLiteral);
                }
            }
            else
            {
                this.ReturnFlightInfo.Visible = false;
                this.ReturnFlightSeatInfo.Visible = false;
                this.ReturnFlightBagsInfo.Visible = false;
            }

            if (this.User != null && !string.IsNullOrEmpty(this.User.UserSettings.FirstName) &&
                !string.IsNullOrEmpty(this.User.UserSettings.LastName))
            {
                this.PassengerNameLiteral.Text = this.User.UserSettings.FirstName + " " + this.User.UserSettings.LastName;
            }

            this.TotalCostLiteral.Text = "&#8364; " + string.Format("{0:0.00}", totalCost);
            this.ItineraryInfoPanel.Visible = true;
        }

        private void SetItineraryForBooking(Data.Models.Booking booking, ref decimal totalCost, Literal seatLiteral,
            Literal flightNumberLiteral, Literal dateTimeLiteral, Literal flightPriceLiteral, Literal travelClassPriceLiteral,
            Literal travelClassLiteral)
        {
            this.SetSelectedFlightToItinerary(booking, ref totalCost, flightNumberLiteral, dateTimeLiteral,
                    flightPriceLiteral, travelClassPriceLiteral, travelClassLiteral);

            if (booking.Row > 0 && !string.IsNullOrEmpty(booking.SeatNumber))
            {
                seatLiteral.Text = booking.Row.ToString() + booking.SeatNumber;
            }
        }

        private void SetAirprotsInfoToItinerary()
        {
            int departureAirportId = int.Parse(this.Session[Common.Constants.DEPARTURE_AIRPORT_ID].ToString());
            int destinationAirportId = int.Parse(this.Session[Common.Constants.DESTINATION_AIRPORT_ID].ToString());

            var departureAirport = this.AirportsServices.GetAirport(departureAirportId);
            var destinationAirport = this.AirportsServices.GetAirport(destinationAirportId);

            this.OutboundFlightAirports.Text = departureAirport.Name + " (" + departureAirport.Abbreviation + ") to " +
                destinationAirport.Name + " (" + destinationAirport.Abbreviation + ")";

            if (this.ReturnRouteBooking != null)
            {
                this.ReturnFlightAirports.Text = destinationAirport.Name + " (" + destinationAirport.Abbreviation + ") to" +
                    departureAirport.Name + " (" + departureAirport.Abbreviation + ")";
            }
        }

        private void SetSelectedFlightToItinerary(Data.Models.Booking booking, ref decimal totalCost, Literal flightNumberLiteral,
            Literal dateTimeLiteral, Literal flightPriceLiteral, Literal travelClassPriceLiteral, Literal travelClassLiteral)
        {
            LegInstance selectedLegInstance = this.LegInstancesServices.GetLegInstance(booking.LegInstanceId);

            flightNumberLiteral.Text = "Flight № " + selectedLegInstance.FlightLeg.Flight.Number;
            dateTimeLiteral.Text = selectedLegInstance.DepartureDateTime.ToString("MMMM dd, yyyy hh:mm", CultureInfo.InvariantCulture);

            flightPriceLiteral.Text = "&#8364; " + string.Format("{0:0.00}", selectedLegInstance.Price);

            decimal travelClassPrice = this.TravelClassesServices.GetTravelClass(booking.TravelClassId).Price;
            travelClassPriceLiteral.Text = "&#8364; " + string.Format("{0:0.00}", travelClassPrice);
            totalCost += selectedLegInstance.Price + travelClassPrice;

            travelClassLiteral.Text = this.TravelClassesServices.GetTravelClass(booking.TravelClassId).Type.ToString() + " class";
        }

        private void SetCabinBagToItinerary(Data.Models.Booking booking, ref decimal totalCost, Literal cabinBagLiteral,
            Literal cabinBabPriceLiteral)
        {
            var cabinBag = booking.Baggage
                       .FirstOrDefault(b => b.Type == BaggageType.Cabin);

            if (cabinBag != null)
            {
                cabinBagLiteral.Text = "One cabin bag " + cabinBag.Size;
                cabinBabPriceLiteral.Text = "&#8364; " + string.Format("{0:0.00}", cabinBag.Price);
                totalCost += cabinBag.Price;
            }
        }

        private void SetCheckedInBagsToItinerary(Data.Models.Booking booking, ref decimal totalCost,
            Literal checkedInBagsLiteral, Literal checkedInBagsPricesLiteral)
        {
            var checkedInBags = booking.Baggage
                .Where(b => b.Type == BaggageType.CheckedIn)
                .ToList();

            if (checkedInBags.Count > 0)
            {
                checkedInBagsLiteral.Text = "Checked in bags " + checkedInBags.Count + " x " +
                    checkedInBags[0].MaxKilograms + " kg. ";

                decimal checkedInBagsPriceSum = checkedInBags.Sum(b => b.Price);
                checkedInBagsPricesLiteral.Text = "&#8364; " + string.Format("{0:0.00}", checkedInBagsPriceSum);
                totalCost += checkedInBagsPriceSum;
            }
        }

        // Set baby, sports and music equipments to the itenerary info panel. 
        private void SetBagEquipmentsToItinerary(Data.Models.Booking booking, ref decimal totalCost, BaggageType baggageType,
            Literal literalInfo, Literal literalPrice)
        {
            var equipment = booking.Baggage
                .FirstOrDefault(b => b.Type == baggageType);

            if (equipment != null)
            {
                literalInfo.Text = string.Format("{0} equipment included!", this.GetBagEquiptmentAsString(baggageType));
                literalPrice.Text = "&#8364; " + string.Format("{0:0.00}", equipment.Price);
                totalCost += equipment.Price;
            }
        }

        private string GetBagEquiptmentAsString(BaggageType type)
        {
            string typeAsString = type.ToString();
            int index = typeAsString.ToLower().IndexOf("equipment");

            return typeAsString.Substring(0, index);
        }
    }
}
