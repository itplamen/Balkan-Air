<%@ Page Title="Leg Instance Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LegInstancesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.LegInstancesManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="LegInstancesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.LegInstance"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="LegInstancesGridView_GetData"
        UpdateMethod="LegInstancesGridView_UpdateItem"
        DeleteMethod="LegInstancesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="DepartureDateTime" SortExpression="DepartureDateTime" HeaderText="Departure" />
            <asp:BoundField DataField="ArrivalDateTime" SortExpression="ArrivalDateTime" HeaderText="Arrival" />
            <asp:BoundField DataField="Price" SortExpression="Price" HeaderText="Price (&#8364;)" />
            <asp:TemplateField HeaderText="Flight Leg" SortExpression="FlightLegId">
                <ItemTemplate>
                    <%#: "Id:" + Item.FlightLegId + " " + Item.FlightLeg.Flight.Number + ", " + 
                            this.GetAirport(Item.FlightLeg.DepartureAirportId) + " (" + Item.FlightLeg.ScheduledDepartureDateTime + 
                            ")" + " -> " + this.GetAirport(Item.FlightLeg.ArrivalAirportId) + " (" + 
                            Item.FlightLeg.ScheduledArrivalDateTime + ")" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditFlightLegDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="FlightLegInfo"
                        SelectedValue="<%#: BindItem.FlightLegId %>"
                        SelectMethod="FlightLegDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Flight Status" SortExpression="FlightStatusId">
                <ItemTemplate>
                    <%#: "Id:" + Item.FlightStatusId + ", " + Item.FlightStatus.Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditFlightStatusDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.FlightStatus"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.FlightStatusId %>"
                        SelectMethod="FlightStatusDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aircraft" SortExpression="AircraftId">
                <ItemTemplate>
                    <%#: "Id:" + Item.AircraftId + ", " + Item.Aircraft.AircraftManufacturer.Name + " " + Item.Aircraft.Model %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditAircraftDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AircraftInfo"
                        SelectedValue="<%#: BindItem.AircraftId %>"
                        SelectMethod="AircraftDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Seats">
                <ItemTemplate>
                    <%#: Item.Seats.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bookings">
                <ItemTemplate>
                    <%#: Item.Bookings.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Duration">
                <ItemTemplate>
                    <%#: Item.Duration %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No leg instances found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RequiredFieldValidator ErrorMessage="Departure date is required!" ControlToValidate="DepartureDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Departure time is required!" ControlToValidate="DepartureTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival date is required!" ControlToValidate="ArrivalDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival time is required!" ControlToValidate="ArrivalTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:CustomValidator ID="AreDateTimesValidCustomValidator" CssClass="validatorSpan" Display="Dynamic" ForeColor="Red"
        ErrorMessage="" runat="server" />

    <asp:CustomValidator ID="AreDateTimesAfterDateTimeNowCustomValidator" CssClass="validatorSpan" Display="Dynamic"
        ForeColor="Red" ErrorMessage="" runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new leg instance</h3>

        <asp:Label runat="server" Text="Departure Date:" AssociatedControlID="DepartureDateTextBox" />
        <asp:TextBox ID="DepartureDateTextBox" ClientIDMode="Static" runat="server" />
        <span id="DepartureCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="DepartureDateTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="DepartureCalendarIconSpan" />

        <asp:Label runat="server" Text="Departure Time:" AssociatedControlID="DepartureTimeTextBox" />
        <asp:TextBox runat="server" ID="DepartureTimeTextBox" ClientIDMode="Static" TextMode="Time" />

        <asp:Label runat="server" Text="Arrival Date:" AssociatedControlID="ArrivalDateTextBox" />
        <asp:TextBox runat="server" ID="ArrivalDateTextBox" ClientIDMode="Static" />
        <span id="ArrivalCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="ArrivalDateTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="ArrivalCalendarIconSpan" />

        <asp:Label runat="server" Text="Arrival Time:" AssociatedControlID="ArrivalTimeTextBox" />
        <asp:TextBox runat="server" ID="ArrivalTimeTextBox" ClientIDMode="Static" TextMode="Time" />
        <span class="glyphicon glyphicon-time"></span>

        <asp:Label Text="Price (&#8364;):" runat="server" AssociatedControlID="AddFareDropDownList" />
        <asp:DropDownList ID="AddFareDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="FareInfo"
            SelectMethod="FaresDropDownList_GetData" />

        <asp:Label Text="Flight Leg:" runat="server" AssociatedControlID="AddFlightLegDropDownList" />
        <asp:DropDownList ID="AddFlightLegDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="FlightLegInfo"
            SelectMethod="FlightLegDropDownList_GetData" />

        <asp:Label Text="Flight Status:" runat="server" AssociatedControlID="AddFlightStatusDropDownList" />
        <asp:DropDownList ID="AddFlightStatusDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.FlightStatus"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="FlightStatusDropDownList_GetData" />

        <asp:Label Text="Aircraft:" runat="server" AssociatedControlID="AddAircraftDropDownList" />
        <asp:DropDownList ID="AddAircraftDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AircraftInfo"
            SelectMethod="AircraftDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateLegInstancetBtn" runat="server" Text="Create" CssClass="btn btn-info"
                ValidationGroup="CreateLegInstance" OnClick="CreateLegInstancetBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
