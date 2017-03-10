<%@ Page Title="Aircrafts Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AircraftsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.AircraftsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New aircraft with ID <asp:Literal ID="AddedAircraftIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="AircraftsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Aircraft"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="AircraftsGridView_GetData"
        UpdateMethod="AircraftsGridView_UpdateItem"
        DeleteMethod="AircraftsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Manufacturer">
                <ItemTemplate>
                    <%#: Item.AircraftManufacturer.Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ManufacturersDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.AircraftManufacturer"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.AircraftManufacturerId %>"
                        SelectMethod="AircraftManufacturersDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Model" SortExpression="Model" HeaderText="Model" />
            <asp:BoundField DataField="TotalSeats" SortExpression="TotalSeats" HeaderText="Total Seats" />
            <asp:TemplateField HeaderText="Travel Classes">
                <ItemTemplate>
                    <%#: Item.TravelClasses.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Leg Instances">
                <ItemTemplate>
                    <%#: Item.LegInstances.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No aircrafts found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Model length must be in the range [3 - 20]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{3,20}$" ValidationGroup="CreateNewAircraft"
        CssClass="validatorSpan" ControlToValidate="AircraftModelTextBox" />

    <asp:RangeValidator ErrorMessage="Number of seats must be in the range [2 - 180]!" ControlToValidate="TotalSeatsTextBox"
        runat="server" ForeColor="Red" MinimumValue="2" MaximumValue="180" Type="Integer" CssClass="validatorSpan" 
        ValidationGroup="CreateNewAircraft" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new aircraft</h3>

        <asp:Label Text="Model:" runat="server" AssociatedControlID="AircraftModelTextBox" />
        <asp:TextBox ID="AircraftModelTextBox" required runat="server" MaxLength="20" Style="text-transform: uppercase;" />

        <asp:Label Text="Total seats:" runat="server" AssociatedControlID="TotalSeatsTextBox" />
        <asp:TextBox ID="TotalSeatsTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Manufacturer:" runat="server" AssociatedControlID="AircraftManufacturersDropDownList" />
        <asp:DropDownList ID="AircraftManufacturersDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.AircraftManufacturer"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="AircraftManufacturersDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateAircraftBtn" runat="server" Text="Create" CssClass="btn btn-info" ValidationGroup="CreateNewAircraft"
                OnClick="CreateAircraftBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
