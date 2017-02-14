<%@ Page Title="Manage Aircrafts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAircrafts.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageAircrafts" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageAircraftsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Aircraft"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        PageSize="50"
        SelectMethod="ManageAircraftsGridView_GetData"
        UpdateMethod="ManageAircraftsGridView_UpdateItem"
        DeleteMethod="ManageAircraftsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:TemplateField HeaderText="Id" SortExpression="Id">
                <ItemTemplate>
                    <%#: Item.Id %>
                </ItemTemplate>
            </asp:TemplateField>
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
            <asp:TemplateField HeaderText="Number of flights">
                <ItemTemplate>
                    <%#: Item.Flights.Count %>
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
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{3,20}$" ControlToValidate="AircraftModelTextBox" />

    <asp:RangeValidator ErrorMessage="Number of seats must be in the range [2 - 180]!" ControlToValidate="TotalSeatsTextBox" 
        runat="server" ForeColor="Red" MinimumValue="2" MaximumValue="180" Type="Integer" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new aircraft</h3>

        <asp:Label Text="Model: " runat="server" />
        <asp:TextBox ID="AircraftModelTextBox" required runat="server" MaxLength="20" Style="text-transform: uppercase;" />

        <asp:Label Text="Total seats:" runat="server" />
        <asp:TextBox ID="TotalSeatsTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Manufacturer: " runat="server" />
        <asp:DropDownList ID="AircraftManufacturersDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.AircraftManufacturer"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="AircraftManufacturersDropDownList_GetData" />

        <asp:Button ID="CreateAircraftBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateAircraftBtn_Click" />
    </asp:Panel>
</asp:Content>
