<%@ Page Title="Seats Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeatsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.SeatsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="SeatsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Seat"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="SeatsGridView_GetData"
        UpdateMethod="SeatsGridView_UpdateItem"
        DeleteMethod="SeatsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="Number" />
            <asp:BoundField DataField="Row" SortExpression="Row" HeaderText="Row" />
            <asp:BoundField DataField="IsReserved" SortExpression="IsReserved" HeaderText="Is Reserved" />
            <asp:BoundField DataField="TravelClassId" SortExpression="TravelClassId" HeaderText="Travel Class Id" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No seats found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [2 - 50]!" Type="String" 
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,50}$" ControlToValidate="AirportNameTextBox" />

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Abbreviation length must be in the range [1 - 3]!" 
        Type="String" ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{1,3}$" ControlToValidate="AbbreviationTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new seat</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="AirportNameTextBox" required MaxLength="50" runat="server" />

        <asp:Label Text="Abbreviation:" runat="server" />
        <asp:TextBox ID="AbbreviationTextBox" required MaxLength="3" Style="text-transform: uppercase;" runat="server" />

         

        <%--<asp:Button ID="CreateAirportBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateAirportBtn_Click" />--%>
    </asp:Panel>
</asp:Content>
