<%@ Page Title="Manage flight statuses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageFlightStatuses.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageFlightStatuses" %>

<asp:Content ID="BoydContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageFlightStatusesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.FlightStatus"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        PageSize="50"
        SelectMethod="ManageFlightStatusesGridView_GetData"
        UpdateMethod="ManageFlightStatusesGridView_UpdateItem"
        DeleteMethod="ManageFlightStatusesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />
            <asp:BoundField DataField="Flights.Count" HeaderText="Flights" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No flight statuses found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid name length!" Type="String" ForeColor="Red" runat="server"
        ValidationExpression="^[\s\S]{2,15}$" ControlToValidate="FlightStatusNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new flight status</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="FlightStatusNameTextBox" required MaxLength="15" runat="server" />

        <asp:Button ID="CreateFlightStatustBtn" runat="server" Text="Create" CssClass="btn btn-info" 
            OnClick="CreateFlightStatustBtn_Click" />
    </asp:Panel>
</asp:Content>
