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
                <a href='<%= Page.ResolveUrl(Pages.ITINERARY) %>?id=<%# Item.Id %>&flight=<%#: Item.Flight.Number %>'>
                    <div class="divBox">
                        <span class="flightNumberSpan"><%#: Item.Flight.Number %></span>
                        <span class="fromAirportSpan"><%#: Item.Flight.DepartureAirport.Name %></span>
                        <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
                        <span class="toAirportSpan"><%#: Item.Flight.ArrivalAirport.Name %></span>
                        <span class="flightDateSpan"><%#: Item.Flight.Departure.ToString("dddd dd, MMMM", CultureInfo.InvariantCulture) %></span>
                        <span class="flightTimeSpan"><%#: Item.Flight.Departure.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                        <span class="bookingStatus">Confirmed</span>
                    </div>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
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

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your account settings</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Password:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                    <dt>External Logins:</dt>
                    <dd><%: LoginsCount %>
                        <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[Manage]" runat="server" />

                    </dd>
                    <%--
                        Phone Numbers can used as a second factor of verification in a two-factor authentication system.
                        See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                        for details on setting up this ASP.NET application to support two-factor authentication using SMS.
                        Uncomment the following blocks after you have set up two-factor authentication
                    --%>
                    <%--
                    <dt>Phone Number:</dt>
                    <% if (HasPhoneNumber)
                       { %>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Add]" />
                    </dd>
                    <% }
                       else
                       { %>
                    <dd>
                        <asp:Label Text="" ID="PhoneNumber" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Change]" /> &nbsp;|&nbsp;
                        <asp:LinkButton Text="[Remove]" OnClick="RemovePhone_Click" runat="server" />
                    </dd>
                    <% } %>
                    --%>

                    <dt>Two-Factor Authentication:</dt>
                    <dd>
                        <p>
                            There are no two-factor authentication providers configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                            for details on setting up this ASP.NET application to support two-factor authentication.
                        </p>
                        <% if (TwoFactorEnabled)
                            { %>
                        <%--
                        Enabled
                        <asp:LinkButton Text="[Disable]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
                        --%>
                        <% }
                            else
                            { %>
                        <%--
                        Disabled
                        <asp:LinkButton Text="[Enable]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
                        --%>
                        <% } %>
                    </dd>
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
