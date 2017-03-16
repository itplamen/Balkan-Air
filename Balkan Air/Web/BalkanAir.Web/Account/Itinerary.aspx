<%@ Page Title="Itinerary" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Itinerary.aspx.cs" Inherits="BalkanAir.Web.Account.Itinerary" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Data.Models" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:FormView ID="ViewItineraryFormView" runat="server"
        ItemType="BalkanAir.Data.Models.Booking"
        SelectMethod="ViewItineraryFormView_GetItem">
        <ItemTemplate>
            <div id="TravelPlanDiv">
                <div id="TravelPlanHeaderDiv">
                    <h3>Travel plan</h3>
                    <span class="statusSpan">STATUS</span>
                    <span class="statusTextSpan"><%#: Item.Status.ToString() %></span>
                    <span class="confirmationCodeSpan">CONFIRMATION CODE</span>
                    <span class="confirmationCodeTextSpan"><%#: Item.ConfirmationCode %></span>
                </div>
                <div id="TravelPlanFlightInfoDiv">
                    <span class="flightNumberSpan"><%#: Item.LegInstance.FlightLeg.Flight.Number %></span>
                    <span class="fromAirportSpan"><%#: Item.LegInstance.FlightLeg.Route.Origin.Name %></span>
                    <span class="fromAirportAbbreviationSpan"><%#: Item.LegInstance.FlightLeg.Route.Origin.Abbreviation %></span>
                    <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
                    <span class="toAirportSpan"><%#: Item.LegInstance.FlightLeg.Route.Destination.Name %></span>
                    <span class="toAirportAbbreviationSpan"><%#: Item.LegInstance.FlightLeg.Route.Destination.Abbreviation %></span>
                    <span class="flightDateSpan"><%#: Item.LegInstance.DepartureDateTime.ToString("ddd dd, MMM", CultureInfo.InvariantCulture) %></span>
                    <span class="flightTimeSpan"><%#: Item.LegInstance.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                    <input type="image" class="datetimeBetween" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
                    <span class="flightDateSpan"><%#: Item.LegInstance.ArrivalDateTime.ToString("ddd dd, MMM", CultureInfo.InvariantCulture) %></span>
                    <span class="flightTimeSpan"><%#: Item.LegInstance.ArrivalDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                </div>
                <div id="TravelPlanPassengerDiv">
                    <span><%#: Item.User.UserSettings.FirstName + " " + Item.User.UserSettings.LastName %></span>
                </div>
                <div id="TravelPlanBagsAndSeatDiv">
                    <input id="SeatIcon" type="image" alt="Seat Icon" src="../Content/Images/Itinerary/seat-icon.png" />
                    <input type="image" alt="Bag Icon" src="../Content/Images/Itinerary/bag-icon.png" />
                    <p>
                        <%#: this.ShowCabinBags(Item.Id) %>
                        <span id="SeatNumberSpan"><%#: this.ShowTravelClass(Item.Id) %> <%#: Item.Row + Item.SeatNumber %></span>
                    </p>
                    <p><%#: this.ShowCheckedInBags(Item.Id) %></p>
                    <p><%#: this.ShowEquipmentBags(Item.Id, BaggageType.BabyEquipment) %></p>
                    <p><%#: this.ShowEquipmentBags(Item.Id, BaggageType.SportsEquipment) %></p>
                    <p><%#: this.ShowEquipmentBags(Item.Id, BaggageType.MusicEquipment) %></p>
                </div>
                <div id="TravelPlanTotalCostDiv">
                    <h2>Total: &#8364; <%#: Item.TotalPrice %></h2>
                </div>
            </div>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
