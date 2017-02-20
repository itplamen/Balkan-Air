﻿<%@ Page Title="Routes Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoutesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.RoutesManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="RoutesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Route"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="RoutesGridView_GetData"
        UpdateMethod="RoutesGridView_UpdateItem"
        DeleteMethod="RoutesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Origin" SortExpression="Origin">
                <ItemTemplate>
                    <%#: "Id:" + Item.OriginId + ", " + Item.Origin.Name + " (" + Item.Origin.Abbreviation + ")" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditOriginDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: BindItem.OriginId %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Destination" SortExpression="DestinationId">
                <ItemTemplate>
                    <%#: "Id:" + Item.DestinationId + ", " + Item.Destination.Name + " (" + Item.Destination.Abbreviation + ")" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditDestinationDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: BindItem.DestinationId %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Distance (Km)" DataField="DistanceInKm" SortExpression="DistanceInKm" />
            <asp:TemplateField HeaderText="Fares">
                <ItemTemplate>
                    <%#: Item.Fares.Count %>
                </ItemTemplate>
            </asp:TemplateField>
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
            <h4>No routes found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:CustomValidator ID="InvalidOriginAndDestinationCustomValidator" ErrorMessage="Origin and destination cannot be same!" 
        Display="Dynamic" ForeColor="Red" runat="server" />

    <asp:CustomValidator ID="InvalidDistanceCustonValidator" ErrorMessage="Invalid distance!" Display="Dynamic" ForeColor="Red" 
        runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new route</h3>

        <asp:Label Text="Origin:" runat="server" AssociatedControlID="AddOriginDropDownList" />
        <asp:DropDownList ID="AddOriginDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label Text="Destination:" runat="server" AssociatedControlID="AddDestinationDropDownList" />
        <asp:DropDownList ID="AddDestinationDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label Text="Distance (Km):" runat="server" AssociatedControlID="DistanceTextBox" />
        <asp:TextBox ID="DistanceTextBox" runat="server" />

        <p>
            <asp:Button ID="CreateRoutetBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateRoutetBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
