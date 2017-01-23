<%@ Page Title="Manage Bookings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageBookings.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageBookings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageBookingsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Booking"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        PageSize="50"
        SelectMethod="ManageBookingsGridView_GetData"
        UpdateMethod="ManageBookingsGridView_UpdateItem"
        DeleteMethod="ManageBookingsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="DateOfBooking" SortExpression="DateOfBooking" HeaderText="Booking Date" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="RowLabel" Text="Row" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="RowLiteral" Text="<%#: Item.Row %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="RowTextBox" Text="<%#: BindItem.Row  %>" runat="server" TextMode="Number" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="SeatLabel" Text="Seat" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="SeatLiteral" Text="<%#: Item.Row %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="SeatTextBox" Text="<%#: BindItem.SeatNumber %>" runat="server" Style="text-transform: uppercase;" MaxLength="1" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalPrice" SortExpression="TotalPrice" HeaderText="Price (&#8364;)" />
            <asp:BoundField DataField="TravelClassId" SortExpression="TravelClassId" HeaderText="Travel Class Id" />
            <asp:BoundField DataField="UserId" SortExpression="UserId" HeaderText="User Id" />
            <asp:BoundField DataField="FlightId" SortExpression="FlightId" HeaderText="Flight Id" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No bookings found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
