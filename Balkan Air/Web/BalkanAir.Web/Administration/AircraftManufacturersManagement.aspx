<%@ Page Title="Aircraft Manufacturers Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AircraftManufacturersManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.AircraftManufacturersManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="AircraftsManufacturersGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.AircraftManufacturer"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="AircraftsManufacturersGridView_GetData"
        UpdateMethod="AircraftsManufacturersGridView_UpdateItem"
        DeleteMethod="AircraftsManufacturersGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:TemplateField HeaderText="Number of aircrafts">
                <ItemTemplate>
                    <%#: Item.Aircrafts.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No aircraft manufacturers found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [2 - 20]!" Type="String" 
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,20}$" ControlToValidate="AircraftManufacturerNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new aircraft manufacturer</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="AircraftManufacturerNameTextBox" required runat="server" MaxLength="20" />

        <asp:Button ID="CreateAircraftManufacturerBtn" runat="server" Text="Create" CssClass="btn btn-info"
            OnClick="CreateAircraftManufacturerBtn_Click" />
    </asp:Panel>
</asp:Content>
