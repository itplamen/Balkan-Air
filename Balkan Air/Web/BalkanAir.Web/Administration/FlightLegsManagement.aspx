<%@ Page Title="Flight Legs Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightLegsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.FlightLegsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New flight leg with ID <asp:Literal ID="AddedFlightLegIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="FlightLegsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.FlightLeg"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="FlightLegsGridView_GetData"
        UpdateMethod="FlightLegsGridView_UpdateItem"
        DeleteMethod="FlightLegsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Origin" SortExpression="DepartureAirportId">
                <ItemTemplate>
                    <%#: this.GetAirport(Item.DepartureAirportId) %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditDepartureAirportDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: Item.DepartureAirportId %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Scheduled Departure" SortExpression="ScheduledDepartureDateTime">
                <ItemTemplate>
                    <%#: Item.ScheduledDepartureDateTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Destination" SortExpression="ArrivalAirportId">
                <ItemTemplate>
                    <%#: this.GetAirport(Item.ArrivalAirportId) %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditArrivalAirportDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: Item.ArrivalAirportId %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Scheduled Arrival" SortExpression="ScheduledArrivalDateTime">
                <ItemTemplate>
                    <%#: Item.ScheduledArrivalDateTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Flight" SortExpression="FlightId">
                <ItemTemplate>
                    <%#: "Id:" + Item.FlightId + " " + Item.Flight.Number %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditFlightDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="FlightInfo"
                        SelectedValue="<%#: Item.FlightId %>"
                        SelectMethod="FlightsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Route" SortExpression="RouteId">
                <ItemTemplate>
                    <%#: "Id:" + Item.RouteId + " " + Item.Route.Origin.Name + " (" + Item.Route.Origin.Abbreviation + ") -> " + 
                            Item.Route.Destination.Name + " (" + Item.Route.Destination.Abbreviation + ")" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="RoutesDropDownList" runat="server" ClientIDMode="Static"
                        DataValueField="Id"
                        DataTextField="RouteInfo"
                        SelectedValue="<%#: Item.RouteId %>"
                        SelectMethod="RoutesDropDownList_GetData" />
                </EditItemTemplate>
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
            <h4>No airports found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:HiddenField ID="DepartureAirportIdHiddenField" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="ArrivalAirportIdHiddenField" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="FlightIdHiddenField" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="RouteIdHiddenField" runat="server" ClientIDMode="Static" />
 
    <asp:RequiredFieldValidator ErrorMessage="Departure date is required!" ControlToValidate="ScheduledDepartureDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlightLeg" />

    <asp:RequiredFieldValidator ErrorMessage="Departure time is required!" ControlToValidate="ScheduledDepartureTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlightLeg" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival date is required!" ControlToValidate="ScheduledArrivalDateTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlightLeg" />

    <asp:RequiredFieldValidator ErrorMessage="Arrival time is required!" ControlToValidate="ScheduledArrivalTimeTextBox"
        ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlightLeg" />

    <asp:CustomValidator ID="AreDateTimesValidCustomValidator" CssClass="validatorSpan" Display="Dynamic" ForeColor="Red" 
        ErrorMessage="" runat="server" />

    <asp:CustomValidator ID="AreDateTimesAfterDateTimeNowCustomValidator" CssClass="validatorSpan" Display="Dynamic" 
        ForeColor="Red" ErrorMessage="" runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new flight leg</h3>

        <asp:Label Text="Departure Airport:" runat="server" AssociatedControlID="AddDepartureAirportDropDownList" />
        <asp:DropDownList ID="AddDepartureAirportDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label runat="server" Text="Scheduled Departure Date:" AssociatedControlID="ScheduledDepartureDateTextBox" />
        <asp:TextBox ID="ScheduledDepartureDateTextBox" ClientIDMode="Static" runat="server" />
        <span id="ScheduledDepartureCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="ScheduledDepartureDateTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="ScheduledDepartureCalendarIconSpan" />

        <asp:Label runat="server" Text="Scheduled Departure Time:" AssociatedControlID="ScheduledDepartureTimeTextBox" />
        <asp:TextBox runat="server" ID="ScheduledDepartureTimeTextBox" ClientIDMode="Static" TextMode="Time" />
        <span class="glyphicon glyphicon-time"></span>

        <asp:Label Text="Arrival Airport:" runat="server" AssociatedControlID="AddArrivalAirportDropDownList" />
        <asp:DropDownList ID="AddArrivalAirportDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label runat="server" Text="Scheduled Arrival Date:" AssociatedControlID="ScheduledArrivalDateTextBox" />
        <asp:TextBox runat="server" ID="ScheduledArrivalDateTextBox" ClientIDMode="Static" />
        <span id="ArrivalCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="ScheduledArrivalDateTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="ArrivalCalendarIconSpan" />

        <asp:Label runat="server" Text="Scheduled Arrival Time:" AssociatedControlID="ScheduledArrivalTimeTextBox" />
        <asp:TextBox runat="server" ID="ScheduledArrivalTimeTextBox" ClientIDMode="Static" TextMode="Time" />
        <span class="glyphicon glyphicon-time"></span>

        <asp:Label Text="Flight:" runat="server" AssociatedControlID="AddFlightDropDownList" />
        <asp:DropDownList ID="AddFlightDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="FlightInfo"
            SelectMethod="FlightsDropDownList_GetData" />

        <asp:Label Text="Route:" runat="server" AssociatedControlID="AddRoutesDropDownList" />
        <asp:DropDownList ID="AddRoutesDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="RouteInfo"
            SelectMethod="RoutesDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateFlightLegBtn" ValidationGroup="CreateNewFlightLeg" runat="server" Text="Create"
                CssClass="btn btn-info" UseSubmitBehavior="true" OnClick="CreateFlightLegBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        $(function () {
            var $editDepartureAirportDropDownList = $('#EditDepartureAirportDropDownList'),
                $editArrivalAirportDropDownList = $('#EditArrivalAirportDropDownList'),
                $editFlightDropDownList = $('#EditFlightDropDownList'),
                $routesDropDownList = $('#RoutesDropDownList'),
                $departureAirportIdHiddenField = $('#DepartureAirportIdHiddenField'),
                $arrivalAirportIdHiddenField = $('#ArrivalAirportIdHiddenField'),
                $flightIdHiddenField = $('#FlightIdHiddenField'),
                $routeIdHiddenField = $('#RouteIdHiddenField');

            setAllHiddenFields();

            $editDepartureAirportDropDownList.change(setDepartureAirportHiddenField);
            $editArrivalAirportDropDownList.change(setArrivalAirportHiddenField);
            $editFlightDropDownList.change(setFlightHiddenField);
            $routesDropDownList.change(setRouteHiddenField);

            function setAllHiddenFields() {
                setDepartureAirportHiddenField();
                setArrivalAirportHiddenField();
                setFlightHiddenField();
                setRouteHiddenField();
            }

            function setDepartureAirportHiddenField() {
                var departureAirportId = $editDepartureAirportDropDownList.find(':selected').val();

                $departureAirportIdHiddenField.val(departureAirportId);
            }

            function setArrivalAirportHiddenField() {
                var arrivalAirportId = $editArrivalAirportDropDownList.find(':selected').val();

                $arrivalAirportIdHiddenField.val(arrivalAirportId);
            }

            function setFlightHiddenField() {
                var flightId = $editFlightDropDownList.find(':selected').val();

                $flightIdHiddenField.val(flightId);
            }

            function setRouteHiddenField() {
                var routeId = $routesDropDownList.find(':selected').val();

                $routeIdHiddenField.val(routeId);
            }
        });
    </script>
</asp:Content>
