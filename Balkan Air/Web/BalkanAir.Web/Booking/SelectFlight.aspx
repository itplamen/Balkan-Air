<%@ Page Title="Select Flight" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="SelectFlight.aspx.cs" Inherits="BalkanAir.Web.Booking.SelectFlight" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="FlightDetailsPanel" runat="server" ClientIDMode="Static">
            <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
            <h3>Choose flight out</h3>

            <div class="center slider">
                <asp:Repeater ID="AvailableDatesRepeater" runat="server"
                    ItemType="BalkanAir.Data.Models.LegInstance"
                    SelectMethod="AvailableDatesRepeater_GetData">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="datesLinkButton" runat="server" CommandArgument="<%#: Item.Id %>"
                            OnClick="OnFlightDateLinkButtonClicked">
                            <div class="flightDatesDiv">
                                <span class="date">
                                    <%#: Item.DepartureDateTime.ToString("ddd dd, MMM", CultureInfo.InvariantCulture) %>
                                </span>
                                <span class="price">
                                    &#8364; <%#: Item.Price %>
                                </span>
                            </div>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div id="SelectedFlightDetailsDiv">
                <asp:FormView ID="FlightDetailsFormView" runat="server" ItemType="BalkanAir.Data.Models.LegInstance">
                    <ItemTemplate>
                        <div id="FlightDetailsDiv">
                            <h4>Flight details</h4>
                            <small>
                                <strong>
                                    <%#: Item.DepartureDateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                                </strong>
                            </small>
                            <hr />
                            <div id="FlightDepartureDetailsDiv">
                                <span id="FlightNumberSpan"><%#: Item.FlightLeg.Flight.Number %></span>
                                <span id="FromAirportSpan"><%#: Item.FlightLeg.Route.Origin.Name %></span>
                                <span id="DepartureSpan"><%#: Item.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                            </div>
                            <div id="FlightMiddleImage">
                                <span>
                                    <input type="image" alt="Airplane image" src="../Content/Images/airplane_between_airports.png"
                                        class="airplaneBetweenAirports" />
                                </span>
                            </div>
                            <div id="FlightDestinationDetailsDeiv">
                                <span id="FlightDurationSpan"><%#: Item.Duration.Hours %> hr <%#: Item.Duration.Minutes %> min</span>
                                <span id="ToAirportSpan"><%#: Item.FlightLeg.Route.Destination.Name %></span>
                                <span id="ArrivalSpan"><%#: Item.ArrivalDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:FormView>

                <div id="FlightTravelClassesDiv">
                    <asp:Repeater ID="FlightTravelClassesRepeater" runat="server" ClientIDMode="Static"
                        ItemType="BalkanAir.Data.Models.TravelClass">
                        <ItemTemplate>
                            <div class="travelClass <%#: Item.Type %>ClassDiv" title="">
                                <span id="<%#: Item.Type %>ClassSpan" class="travelClassTypeSpan"><%#: Item.Type %> Class
                                <span class="travelClassTypeDetailsSpan">
                                    <img src="../Content/Images/online-check-in-icon.png" class="availableIcon" alt="Online ckeck-in Icon" />
                                    <img src="../Content/Images/meal-icon.png" class="availableIcon" alt="Meal Icon" />
                                    <img src="../Content/Images/reserved-seat-icon.png" class="availableIcon" alt="Reserved Seat Icon" />
                                    <img src="../Content/Images/priority-boarding-icon.png" class="priorityBoardingIcons" alt="Priority Boarding Icon" />
                                    <img src="../Content/Images/extra-baggage-icon.png" class="extraBaggageIcons" alt="Extra Baggage Icon" />
                                    <img src="../Content/Images/earn-miles-icon.png" class="earnMilesIcons" alt="Earn Miles Icon" />
                                </span>
                                </span>
                                <span class="travelClassPriceSpan">
                                    <label>
                                        <input type="radio" required name="price" value="<%# Item.Id %>" 
                                            class="<%#: Item.NumberOfAvailableSeats == 0 ? "noMoreSeats" : "" %>" /> 
                                        &#8364; <%# Item.Price %>
                                    </label>

                                    <span class="travelClassSeats">
                                        <%#: Item.NumberOfAvailableSeats %> seats remaining at this price
                                    </span>
                                </span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:HiddenField ID="SelectedFlightIdHiddenField" runat="server" />
                    <asp:HiddenField ID="SelectedTravelClassIdHiddenField" ClientIDMode="Static" runat="server" />
                </div>
            </div>

            <div id="ContinueBookingDiv">
                <asp:Button ID="ContinueBookingBtn" Text="Continue" runat="server" ClientIDMode="Static"
                    OnClick="OnContinueBookingBtnClicked" Visible="false" />
            </div>

            <asp:Panel ID="SignInRequiredPanel" Visible="false" CssClass="warningPanel" runat="server" ClientIDMode="Static">
                <h5>YOU NEED TO SIGN IN TO CONTINUE.</h5>

                <asp:Button ID="CreateNewAccountToContinueBtn" ClientIDMode="Static" UseSubmitBehavior="false"
                    Text="CREATE A NEW ACCOUNT TO CONTINUE" runat="server" PostBackUrl="~/Account/Register.aspx" />

                <asp:Button ID="SignInToContinueBtn" ClientIDMode="Static" Text="SIGN IN" runat="server" UseSubmitBehavior="false"
                    PostBackUrl="~/Account/Login.aspx" />
            </asp:Panel>
    </asp:Panel>

    <script>
        $('.center').slick({
            infinite: false,
            centerMode: false,
            focusOnSelect: false,
            variableWidth: true,
            slidesToShow: 5,
            slidesToScroll: 1,
        });
    </script>
</asp:Content>
