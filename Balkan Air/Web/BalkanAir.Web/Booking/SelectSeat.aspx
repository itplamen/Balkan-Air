<%@ Page Title="Select Seat" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectSeat.aspx.cs" Inherits="BalkanAir.Web.Booking.SelectSeat" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="SeatSelectionDiv">
        <input type="image" id="AirplaneInsideImg" src="../Content/Images/seat_selection_airplane_inside_image.png" />
        <div id="SeatsDiv">
            <asp:Repeater ID="SeatRepeater" runat="server"
                ItemType="BalkanAir.Data.Models.Seat"
                SelectMethod="SeatRepeater_GetData">
                <ItemTemplate>
                    <span>
                        <input type="radio" id="<%#: Item.Row + Item.Number %>" name="seat" data-value="<%#: Item.Number %>"
                            required value="<%# Item.Row %>" />
                        <label for="<%#: Item.Row + Item.Number %>" class="<%#: this.GetTravelClassType(Item.TravelClassId) %> 
                            <%#: this.GetTravelClassType(Item.TravelClassId) != this.SelectedTravelClassLabel.Text ? "unavailableSeatLabel" : 
                                Item.IsReserved ? "reservedSeatLabel" : "availableSeatLabel" %>">
                            <%# Item.Number %></label>
                    </span>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div id="BookingFlowDiv">
            <h4>Select your seat now!</h4>
            <p>You can select your favorite seat, depending of your selected travel class!</p>
            <p>Passengers that do not select a seat can not continue with the booking!</p>

            <div id="SelectedSeatInfoDiv">
                <asp:Label ID="FromAirportLabel" runat="server" />
                <input type="image" alt="Airplane image" src="../Content/Images/airplane_between_airports.png"
                    class="airplaneBetweenAirports" />
                <asp:Label ID="ToAirportLabel" runat="server" />
                <br />

                <asp:Literal Text="Class: " runat="server" />
                <asp:Label ID="SelectedTravelClassLabel" runat="server" />
                <br />

                <asp:Label ID="SelectedRowAndSeatLabel" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="SelectedRowHiddenField" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="SelectedSeatHiddenField" ClientIDMode="Static" runat="server" />
            </div>

            <div id="ContinueBookingDiv">
                <asp:Button ID="ContinueBookingBtn" Text="SAVE" runat="server" ClientIDMode="Static" OnClick="ContinueBookingBtn_Click" />
            </div>
        </div>

        <div id="SeatInfoDiv">
            <label class="availableSeatLabel"></label>
            <span>AVAILABLE</span>
            <br />

            <label class="selectedSeatLabel"></label>
            <span>SELECTED</span>
            <br />

            <label class="reservedSeatLabel"></label>
            <span>RESERVED</span>
            <br />

            <label class="unavailableSeatLabel"></label>
            <span>UNAVAILABLE FOR YOUR TRAVEL CLASS</span>
        </div>
    </div>
</asp:Content>
