<%@ Page Title="Previous Trips" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreviousTrips.aspx.cs" Inherits="BalkanAir.Web.Account.PreviousTrips" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="PreviousTripsListView" runat="server"
        DataKeyNames="Id"
        ItemType="BalkanAir.Data.Models.Booking"
        SelectMethod="PreviousTripsListView_GetData">
        <LayoutTemplate>
            <div runat="server" id="itemPlaceholder"></div>
            <asp:DataPager runat="server" PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowPreviousPageButton="true" ShowNextPageButton="false"
                        ButtonCssClass="btn btn-success" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="false" ShowNextPageButton="true"
                        ButtonCssClass="btn btn-success" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="previousTripDiv">
                <p>
                    From: 
                    <span>
                        <%#: Item.LegInstance.FlightLeg.Route.Origin.Name %> 
                        (<%#: Item.LegInstance.FlightLeg.Route.Origin.Abbreviation %>), 
                        <%#: Item.LegInstance.FlightLeg.Route.Origin.Country.Name %>
                    </span>
                </p>
                <p>
                    To: 
                    <span>
                        <%#: Item.LegInstance.FlightLeg.Route.Destination.Name %> 
                        (<%#: Item.LegInstance.FlightLeg.Route.Destination.Abbreviation %>), 
                        <%#: Item.LegInstance.FlightLeg.Route.Destination.Country.Name %>
                    </span>
                </p>
                <p>Date of trip: <span><%#: Item.LegInstance.DepartureDateTime %></span></p>
                <p>Date of booking: <span><%#: Item.DateOfBooking %></span></p>
                <p>Seat: <span><%#: Item.Row %><%#: Item.SeatNumber %></span></p>
                <p>Total price: <%#: Item.TotalPrice %></p>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:Panel ID="NoPreviousTripsPanel" runat="server" CssClass="warningPanel">
                <h5>YOU DO NOT HAVE ANY PREVIOUS TRIPS!</h5>
            </asp:Panel>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
