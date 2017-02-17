<%@ Page Title="Manage Flights" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageFlights.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageFlights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageFlightsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Flight"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageFlightsGridView_GetData"
        UpdateMethod="ManageFlightsGridView_UpdateItem"
        DeleteMethod="ManageFlightsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="Flight Number" />
            <asp:TemplateField HeaderText="Flight Legs">
                <ItemTemplate>
                    <%#: Item.FlightLegs.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No flights found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RequiredFieldValidator ErrorMessage="Fligth number is required!" ControlToValidate="AddFlightNumberTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

    <asp:Panel ID="CreateFlightPanel" runat="server" ClientIDMode="Static">
        <h3>Create new flight</h3>

        <asp:Label runat="server" AssociatedControlID="AddFlightNumberTextBox" Text="Flight number (unique):" />
        <asp:TextBox ID="AddFlightNumberTextBox" ClientIDMode="Static" Style="text-transform: uppercase;" runat="server" ReadOnly="true" />
        <asp:Button ID="GenerateFlightNumberBtn" ClientIDMode="Static" Text="Generate number"
            UseSubmitBehavior="false" CssClass="btn btn-success btn-sm" runat="server" OnClick="GenerateFlightNumberBtn_Click" />

        <asp:Button ID="CreateFlightBtn" ValidationGroup="CreateNewFlight" runat="server" Text="Create"
            CssClass="btn btn-info" UseSubmitBehavior="true" OnClick="CreateFlightBtn_Click" />
        <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
            UseSubmitBehavior="false" />
    </asp:Panel>
</asp:Content>
