<%@ Page Title="Leg Instance Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LegInstancesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.LegInstancesManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New leg instance with ID <asp:Literal ID="AddedLegInstanceIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

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
                    <asp:DropDownList ID="EditFlightLegDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="FlightLegInfo"
                        SelectedValue="<%#: Item.FlightLegId %>"
                        SelectMethod="FlightLegDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Flight Status" SortExpression="FlightStatusId">
                <ItemTemplate>
                    <%#: "Id:" + Item.FlightStatusId + ", " + Item.FlightStatus.Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditFlightStatusDropDownList" runat="server" ClientIDMode="Static"
                        ItemType="BalkanAir.Data.Models.FlightStatus"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: Item.FlightStatusId %>"
                        SelectMethod="FlightStatusDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aircraft" SortExpression="AircraftId">
                <ItemTemplate>
                    <%#: "Id:" + Item.AircraftId + ", " + Item.Aircraft.AircraftManufacturer.Name + " " + Item.Aircraft.Model %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditAircraftDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="AircraftInfo"
                        SelectedValue="<%#: Item.AircraftId %>"
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
    
    <asp:HiddenField ID="FlightLegIdHiddenField" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="FlightStatusIdHiddenField" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="AircraftIdHiddenField" runat="server" ClientIDMode="Static" />

    <asp:RequiredFieldValidator ErrorMessage="Departure date is required!" ControlToValidate="LegInstanceDepartureDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Departure time is required!" ControlToValidate="DepartureTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival date is required!" ControlToValidate="LegInstanceArrivalDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival time is required!" ControlToValidate="ArrivalTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateLegInstance" />

    <asp:CustomValidator ID="AreDateTimesValidCustomValidator" CssClass="validatorSpan" Display="Dynamic" ForeColor="Red"
        ErrorMessage="" runat="server" />

    <asp:CustomValidator ID="AreDateTimesAfterDateTimeNowCustomValidator" CssClass="validatorSpan" Display="Dynamic"
        ForeColor="Red" ErrorMessage="" runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new leg instance</h3>

        <asp:Label runat="server" Text="Departure Date:" AssociatedControlID="LegInstanceDepartureDateTextBox" />
        <asp:TextBox ID="LegInstanceDepartureDateTextBox" ClientIDMode="Static" runat="server" />
        <span id="DepartureCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="LegInstanceDepartureDateTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="DepartureCalendarIconSpan" />

        <asp:Label runat="server" Text="Departure Time:" AssociatedControlID="DepartureTimeTextBox" />
        <asp:TextBox runat="server" ID="DepartureTimeTextBox" ClientIDMode="Static" TextMode="Time" />

        <asp:Label runat="server" Text="Arrival Date:" AssociatedControlID="LegInstanceArrivalDateTextBox" />
        <asp:TextBox runat="server" ID="LegInstanceArrivalDateTextBox" ClientIDMode="Static" />
        <span id="ArrivalCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="LegInstanceArrivalDateTextBox"
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
<asp:Content ID="ScriptContent" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        $(function () {
            var $editFlightLegDropDownList = $('#EditFlightLegDropDownList'),
                $editFlightStatusDropDownList = $('#EditFlightStatusDropDownList'),
                $editAircraftDropDownList = $('#EditAircraftDropDownList'),
                $flightLegIdHiddenField = $('#FlightLegIdHiddenField'),
                $flightStatusIdHiddenField = $('#FlightStatusIdHiddenField'),
                $aircraftIdHiddenField = $('#AircraftIdHiddenField');

            setAllHiddenFields();

            $editFlightLegDropDownList.change(setFlightLegHiddenField);
            $editFlightStatusDropDownList.change(setFlightStatusHiddenField);
            $editAircraftDropDownList.change(setAircraftHiddenField);

            function setAllHiddenFields() {
                setFlightLegHiddenField();
                setFlightStatusHiddenField();
                setAircraftHiddenField();
            }

            function setFlightLegHiddenField() {
                var flightLegId = $editFlightLegDropDownList.find(':selected').val();

                $flightLegIdHiddenField.val(flightLegId);
            }

            function setFlightStatusHiddenField() {
                var flightStatusId = $editFlightStatusDropDownList.find(':selected').val();

                $flightStatusIdHiddenField.val(flightStatusId);
            }

            function setAircraftHiddenField() {
                var aircraftId = $editAircraftDropDownList.find(':selected').val();

                $aircraftIdHiddenField.val(aircraftId);
            }
        });
    </script>
</asp:Content>