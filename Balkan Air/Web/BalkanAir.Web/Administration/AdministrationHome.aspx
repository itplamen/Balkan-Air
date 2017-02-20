<%@ Page Title="Administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrationHome.aspx.cs" Inherits="BalkanAir.Web.Administration.AdministrationHome" %>

<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div>
        <div class="row">
            <div class="col-md-4">
                <h3>Administrators</h3>

                <asp:Repeater ID="AdministratorsRepeater" runat="server"
                    ItemType="BalkanAir.Data.Models.User"
                    SelectMethod="AdministratorsRepeater_GetData">
                    <ItemTemplate>
                        <p>
                            <%#: Item.UserSettings.FirstName + " " + Item.UserSettings.LastName %>
                            <br />
                            <small><%#: Item.Email %></small>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="col-md-4">
                <h3>Main categories</h3>
                <table class="table table-striped">
                    <tbody>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.AIRCRAFT_MANUFACTURERS_MANAGEMENT) %>">Aircraft Manufacturers Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.AIRCRAFTS_MANAGEMENT) %>">Aircrafts Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.AIRPORTS_MANAGEMENT) %>">Airports Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.BAGGAGE_MANAGEMENT) %>">Baggage Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.BOOKINGS_MANAGEMENT) %>">Bookings Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.CATEGORIES_MANAGEMENT) %>">Categories Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.COMMENTS_MANAGEMENT) %>">Comments Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.COUNTRIES_MANAGEMENT) %>">Countries Management</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
             
            <div id="SecondColumn" class="col-md-4">
                <table class="table table-striped">
                    <tbody>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.FARES_MANAGEMENT) %>">Fares Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.FLIGHT_LEGS_MANAGEMENT) %>">Flight Legs Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.FLIGHTS_MANAGEMENT) %>">Flights Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.FLIGHT_STATUSES_MANAGEMENT) %>">Flight Statuses Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.LEG_INSTANCES_MANAGEMENT) %>">Leg Instances Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.NEWS_MANAGEMENT) %>">News Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.NOTIFICATIONS_MANAGEMENT) %>">Notifications Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.ROUTES_MANAGEMENT) %>">Routes Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.SEATS_MANAGEMENT) %>">Seats Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.TRAVEL_CLASSES_MANAGEMENT) %>">Travel Classes Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.USER_NOTIFICATIONS_MANAGEMENT) %>">User Notifications Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.USER_ROLES_MANAGEMENT) %>">User Roles Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.USERS_MANAGEMENT) %>">Users Management</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.SEND_MAIL) %>">Send Mail</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
