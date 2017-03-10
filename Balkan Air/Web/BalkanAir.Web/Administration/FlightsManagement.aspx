<%@ Page Title="Flights Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.FlightsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New flight with ID <asp:Literal ID="AddedFlightIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="FlightsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Flight"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="FlightsGridView_GetData"
        UpdateMethod="FlightsGridView_UpdateItem"
        DeleteMethod="FlightsGridView_DeleteItem"
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

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Create new flight</h3>

        <asp:Label runat="server" AssociatedControlID="AddFlightNumberTextBox" Text="Flight number (unique):" />
        <asp:TextBox ID="AddFlightNumberTextBox" ClientIDMode="Static" Style="text-transform: uppercase;" runat="server" ReadOnly="true" />
        <asp:Button ID="GenerateFlightNumberBtn" ClientIDMode="Static" Text="Generate number"
            UseSubmitBehavior="false" CssClass="btn btn-success btn-sm" runat="server" OnClick="GenerateFlightNumberBtn_Click" />

        <p>
            <asp:Button ID="CreateFlightBtn" ValidationGroup="CreateNewFlight" runat="server" Text="Create"
                CssClass="btn btn-info" UseSubmitBehavior="true" OnClick="CreateFlightBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
