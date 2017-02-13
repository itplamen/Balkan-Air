<%@ Page Title="Administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrationHome.aspx.cs" Inherits="BalkanAir.Web.Administration.AdministrationHome" %>

<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div>
        <div class="row">
            <div class="col-md-3">
                <h3>Administrators</h3>

                <asp:Repeater ID="AdministratorsRepeater" runat="server"
                    ItemType="BalkanAir.Data.Models.User"
                    SelectMethod="AdministratorsRepeater_GetData">
                    <ItemTemplate>
                        <p>
                            <%#: Item.UserSettings.FirstName + " " + Item.UserSettings.LastName %> <br />
                            <small><%#: Item.Email %></small>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="col-md-9">
                <h3>Main categories</h3>
                <table class="table table-striped">
                    <tbody>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_AIRCRAFT_MANUFACTURERS) %>">Manage Aircraft Manufacturers</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_AIRCRAFTS) %>">Manage Aircrafts</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_AIRPORTS) %>">Manage Airports</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_BOOKINGS) %>">Manage Bookings</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_COUNTRIES) %>">Manage Countries</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_FLIGHTS) %>">Manage Flights</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_FLIGHT_STATUSES) %>">Manage Flight Statuses</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_NEWS) %>">Manage News</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_NOTIFICATIONS) %>">Manage Notifications</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_TRAVEL_CLASSES) %>">Manage Travel Classes</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_USER_ROLES) %>">Manage User Roles</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="<%= Page.ResolveUrl(Pages.MANAGE_USERS) %>">Manage Users</a>
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
