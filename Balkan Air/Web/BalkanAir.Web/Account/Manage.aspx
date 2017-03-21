<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="BalkanAir.Web.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="WelcomeText"><asp:Literal ID="WelcomeTextLiteral" runat="server" /></h2>

    <asp:Panel ID="UpcomingTripsPanel" ClientIDMode="Static" runat="server">
        <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
        <h3>Upcoming trips</h3>

        <asp:Repeater ID="UpcomingTripsRepeater" runat="server"
            ItemType="BalkanAir.Data.Models.Booking"
            SelectMethod="UpcomingTripsRepeater_GetData">
            <ItemTemplate>
                <a href='<%= Page.ResolveUrl(Pages.ITINERARY) %>?number=<%#: Item.ConfirmationCode %>&passenger=<%#: Item.User.UserSettings.LastName %>'>

                    <div class="divBox">
                        <span class="flightNumberSpan"><%#: Item.ConfirmationCode %></span>
                        <span class="fromAirportSpan"><%#: Item.LegInstance.FlightLeg.Route.Origin.Name %></span>
                        <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
                        <span class="toAirportSpan"><%#: Item.LegInstance.FlightLeg.Route.Destination.Name %></span>
                        <span class="flightDateSpan"><%#: Item.LegInstance.DepartureDateTime.ToString("dddd dd, MMMM", CultureInfo.InvariantCulture) %></span>
                        <span class="flightTimeSpan"><%#: Item.LegInstance.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                        <span class="bookingStatus"><%#: Item.Status.ToString() %></span>
                    </div>

                </a>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-success" role="alert">
                <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
                <span class="sr-only">Succes:</span>
                <%: this.SuccessMessage %>
            </div>
        </asp:PlaceHolder>
    </div>

    <a href="<%= Page.ResolveUrl(Pages.PROFILE) %>">
        <div id="YourProfileDivBox" class="divBox">
            <h5>YOUR PROFILE</h5>
            <img src="../Content/Images/Account/user-avatar-image.png" alt="Custon account icon" />
            <p>Personal information</p>
        </div>
    </a>

    <a href="<%= Page.ResolveUrl(Pages.SETTINGS) %>">
        <div id="UserSettings" class="divBox">
            <h5>SETTINGS</h5>
            <img src="../Content/Images/Account/user-settings-icon.png" alt="User settings icon" />
            <p>Manage account settings</p>
        </div>
    </a>

    <a href="<%= Page.ResolveUrl(Pages.CREDIT_CARDS) %>">
        <div id="CreditCardsDivBox" class="divBox">
            <h5>CREDIT CARDS</h5>
            <img src="../Content/Images/Account/credit-card-icon.png" alt="Credit cards icon" />
            <p>Check your saved credit cards</p>
        </div>
    </a>

    <a href="<%= Page.ResolveUrl(Pages.PREVIOUS_TRIPS) %>">
        <div id="PreviousTripsDivBox" class="divBox">
            <h5>PREVIOUS TRIPS</h5>
            <img src="../Content/Images/Account/previous-trips-icon.png" alt="Previous trips icon" />
            <p>Check your previous trips</p>
        </div>
    </a>

    <a href="<%= Page.ResolveUrl(Pages.NOTIFICATIONS) %>">
        <div id="NotificationsDivBox" class="divBox">
            <h5>NOTIFICATIONS</h5>
            <img src="../Content/Images/Account/notifications-icon.png" alt="Notifications icon" />
            <p>Check your notifications</p>
        </div>
    </a>
</asp:Content>
