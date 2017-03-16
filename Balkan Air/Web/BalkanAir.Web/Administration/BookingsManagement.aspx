<%@ Page Title="Bookings Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.BookingsManagement" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="BookingsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Booking"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="BookingsGridView_GetData"
        UpdateMethod="BookingsGridView_UpdateItem"
        DeleteMethod="BookingsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Confirmation Code" SortExpression="ConfirmationCode"> 
                <ItemTemplate>
                    <%#: Item.ConfirmationCode %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Of Booking" SortExpression="DateOfBooking">
                <ItemTemplate>
                    <%#: Item.DateOfBooking.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Row" SortExpression="Row" HeaderText="Row" />
            <asp:BoundField DataField="SeatNumber" SortExpression="SeatNumber" HeaderText="Seat" />
            <asp:BoundField DataField="TotalPrice" SortExpression="TotalPrice" HeaderText="Price (&#8364;)" />
            <asp:BoundField DataField="TravelClassId" SortExpression="TravelClassId" HeaderText="Travel Class Id" />
            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                <ItemTemplate>
                    <%#: Item.Status.ToString() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserId" SortExpression="UserId" HeaderText="User Id" />
            <asp:TemplateField HeaderText="Passenger">
                <ItemTemplate>
                    <%#: Item.User.UserSettings.FirstName + " " + Item.User.UserSettings.LastName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LegInstanceId" SortExpression="LegInstanceId" HeaderText="Leg Instance Id" />
            <asp:TemplateField HeaderText="Leg Instance">
                <ItemTemplate>
                    <%#: Item.LegInstance.FlightLeg.Flight.Number + ", " + Item.LegInstance.FlightLeg.Route.Origin.Name + " -> " + 
                            Item.LegInstance.FlightLeg.Route.Destination.Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Baggage">
                <ItemTemplate>
                    <%#: Item.Baggage.Count %>
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
