<%@ Page Title="Manage Airports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAirports.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageAirports" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageAirportsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Airport"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        PageSize="50"
        SelectMethod="ManageAirportsGridView_GetData"
        UpdateMethod="ManageAirportsGridView_UpdateItem"
        DeleteMethod="ManageAirportsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:BoundField DataField="Abbreviation" SortExpression="Abbreviation" HeaderText="Abbreviation" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="CountryLabel" Text="Country" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="CountryLiteral" Text="<%#: Item.Country.Name %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="CountriesDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.Country"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.CountryId %>"
                        SelectMethod="CountryDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DeparturesFlights.Count" HeaderText="DeparturesFlights" />
            <asp:BoundField DataField="ArrivalsFlights.Count" HeaderText="ArrivalsFlights" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No airports found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid name length!" Type="String" ForeColor="Red" runat="server"
        ValidationExpression="^[\s\S]{2,50}$" ControlToValidate="AirportNameTextBox" />

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid abbreviation length!" Type="String" ForeColor="Red" 
        runat="server" ValidationExpression="^[\s\S]{1,3}$" ControlToValidate="AbbreviationTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new airport</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="AirportNameTextBox" required MaxLength="50" runat="server" />

        <asp:Label Text="Abbreviation:" runat="server" />
        <asp:TextBox ID="AbbreviationTextBox" required MaxLength="3" Style="text-transform: uppercase;" runat="server" />

        <asp:Label Text="Country: " runat="server" />
        <asp:DropDownList ID="CountryDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.Country"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="CountryDropDownList_GetData" />

        <asp:Button ID="CreateAirportBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateAirportBtn_Click" />
    </asp:Panel>
</asp:Content>
