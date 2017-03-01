namespace BalkanAir.Web.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class SelectSeat : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        private string SelectSeatForRoute
        {
            get
            {
                if (((bool)this.Session[NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT]) &&
                !((bool)this.Session[NativeConstants.RETURN_ROUTE_SELECT_SEAT]))
                {
                    return NativeConstants.ONE_WAY_ROUTE_BOOKING;
                }
                else
                {
                    return NativeConstants.RETURN_ROUTE_BOOKING;
                }
            }
        }

        private Booking Booking
        {
            get
            {
                return (Booking)this.Session[this.SelectSeatForRoute];
            }
        }

        public IEnumerable<Seat> SeatRepeater_GetData()
        {
            return this.GenerateSeatMap();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.Booking == null || (this.Booking != null && this.Booking.LegInstanceId == 0 || 
                    this.Booking.TravelClassId == 0))
                {
                    this.Response.Redirect(Pages.HOME);
                }
                else
                {
                    LegInstance selectedLegInstance = this.LegInstancesServices.GetLegInstance(this.Booking.LegInstanceId);

                    this.FromAirportLabel.Text = selectedLegInstance.FlightLeg.Route.Origin.Name;
                    this.ToAirportLabel.Text = selectedLegInstance.FlightLeg.Route.Destination.Name;
                    this.SelectedTravelClassLabel.Text = selectedLegInstance.Aircraft.TravelClasses
                        .FirstOrDefault(t => t.Id == this.Booking.TravelClassId)
                        .Type
                        .ToString();

                    if (this.Booking.Row != 0 && !string.IsNullOrEmpty(this.Booking.SeatNumber))
                    {
                        this.SelectedRowAndSeatLabel.Text = "Seat: " + this.Booking.Row + this.Booking.SeatNumber;
                        this.SelectedRowHiddenField.Value = this.Booking.Row.ToString();
                        this.SelectedSeatHiddenField.Value = this.Booking.SeatNumber;
                    }
                    else
                    {
                        this.SelectedRowAndSeatLabel.Text = "Seat: No seat selected!";
                    }
                }
            }
        }

        protected void ContinueBookingBtn_Click(object sender, EventArgs e)
        {
            this.Booking.Row = int.Parse(this.SelectedRowHiddenField.Value);
            this.Booking.SeatNumber = this.SelectedSeatHiddenField.Value;

            this.Session.Add(this.SelectSeatForRoute, this.Booking);
            this.Response.Redirect(Pages.EXTRAS);
        }

        protected string GetTravelClassType(int travelClassId)
        {
            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            if (travelClass == null)
            {
                return string.Empty;
            }

            return travelClass.Type.ToString();
        }

        private IEnumerable<Seat> GenerateSeatMap()
        {
            var seatMap = new List<Seat>();

            TravelClass firstClass = this.GetTravelClassFromFlight(this.Booking.LegInstanceId, TravelClassType.First);
            TravelClass businessClass = this.GetTravelClassFromFlight(this.Booking.LegInstanceId, TravelClassType.Business);
            TravelClass economyClass = this.GetTravelClassFromFlight(this.Booking.LegInstanceId, TravelClassType.Economy);

            int travelClassId = 0;
            int numberOfRows = 30;
            int rowsForFirstClass = 2;
            int rowsForBusinessClass = 4;
            
            // 2 rows for First Class and Business Class and 26 rows for Economy Class
            for (int row = 1; row <= numberOfRows; row++)
            {
                // First and second row for first class.
                if (row <= rowsForFirstClass && firstClass != null)
                {
                    travelClassId = firstClass.Id;
                }
                // Third and fourth row for business class
                else if (row > rowsForFirstClass && row <= rowsForBusinessClass && businessClass != null)
                {
                    travelClassId = businessClass.Id;
                }
                else if (row > rowsForBusinessClass && economyClass != null)
                {
                    travelClassId = economyClass.Id;
                }

                seatMap.Add(new Seat() { Number = "A", Row = row, TravelClassId = travelClassId });
                seatMap.Add(new Seat() { Number = "B", Row = row, TravelClassId = travelClassId });
                seatMap.Add(new Seat() { Number = "C", Row = row, TravelClassId = travelClassId });
                seatMap.Add(new Seat() { Number = "D", Row = row, TravelClassId = travelClassId });
                seatMap.Add(new Seat() { Number = "E", Row = row, TravelClassId = travelClassId });
                seatMap.Add(new Seat() { Number = "F", Row = row, TravelClassId = travelClassId });
            }

            List<Seat> reservedSeats = this.GetReservedSeats(this.Booking.LegInstanceId);

            for (int i = 0; i < seatMap.Count; i++)
            {
                for (int j = 0; j < reservedSeats.Count; j++)
                {
                    if (seatMap[i].TravelClassId == reservedSeats[j].TravelClassId && seatMap[i].Row == reservedSeats[j].Row && 
                        seatMap[i].Number == reservedSeats[j].Number)
                    {
                        seatMap[i].IsReserved = true;
                        break;
                    }
                }
            }

            return seatMap;
        }

        private TravelClass GetTravelClassFromFlight(int legInstanceId, TravelClassType type)
        {
            return this.LegInstancesServices.GetLegInstance(legInstanceId)
                .Aircraft
                .TravelClasses
                .Where(t => t.Type == type)
                .FirstOrDefault();
        }

        private List<Seat> GetReservedSeats(int legInstanceId)
        {
            return this.SeatsServices.GetAll()
                .Where(s => !s.IsDeleted && s.LegInstanceId == legInstanceId && s.IsReserved)
                .ToList();
        }
    }
}