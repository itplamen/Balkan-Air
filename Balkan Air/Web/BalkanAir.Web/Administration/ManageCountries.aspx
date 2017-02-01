<%@ Page Title="Manage Countries" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCountries.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageCountries" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageCountriesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Country"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        PageSize="50"
        SelectMethod="ManageCountriesGridView_GetData"
        UpdateMethod="ManageCountriesGridView_UpdateItem"
        DeleteMethod="ManageCountriesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:BoundField DataField="Abbreviation" SortExpression="Abbreviation" HeaderText="Abbreviation" />
            <asp:BoundField DataField="Airports.Count" HeaderText="Airports" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No countries found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid name length!" Type="String" ForeColor="Red" runat="server"
        ValidationExpression="^[\s\S]{2,50}$" ControlToValidate="CountryNameTextBox" />

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid name length!" Type="String" ForeColor="Red" runat="server"
        ValidationExpression="^[\s\S]{1,2}$" ControlToValidate="AbbreviationNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new country</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="CountryNameTextBox" required MaxLength="50" runat="server" />

        <asp:Label Text="Abbreviation: " runat="server" />
        <asp:TextBox ID="AbbreviationNameTextBox" required MaxLength="2" Style="text-transform: uppercase;" runat="server"  />

        <asp:Button ID="CreateCountrytBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateCountrytBtn_Click" />
    </asp:Panel>
</asp:Content>

