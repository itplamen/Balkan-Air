<%@ Page Title="Manage Bookings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageBookings.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageBookings" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageBookingsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Booking"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageBookingsGridView_GetData"
        UpdateMethod="ManageBookingsGridView_UpdateItem"
        DeleteMethod="ManageBookingsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Date of booking" SortExpression="DateOfBooking">
                <ItemTemplate>
                    <%#: Item.DateOfBooking.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Row" SortExpression="Row" HeaderText="Row" />
            <asp:BoundField DataField="SeatNumber" SortExpression="SeatNumber" HeaderText="Seat" />
            <asp:BoundField DataField="TotalPrice" SortExpression="TotalPrice" HeaderText="Price (&#8364;)" />
            <asp:BoundField DataField="TravelClassId" SortExpression="TravelClassId" HeaderText="Travel Class Id" />
            <asp:BoundField DataField="UserId" SortExpression="UserId" HeaderText="User Id" />
            <asp:TemplateField HeaderText="Passenger">
                <ItemTemplate>
                    <%#: Item.User.UserSettings.FirstName + " " + Item.User.UserSettings.LastName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FlightId" SortExpression="FlightId" HeaderText="Flight Id" />
            <asp:TemplateField HeaderText="Flight">
                <ItemTemplate>
                    <%#: Item.Flight.Number + ", " + Item.Flight.DepartureAirport.Name + " -> " + Item.Flight.ArrivalAirport.Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No bookings found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
