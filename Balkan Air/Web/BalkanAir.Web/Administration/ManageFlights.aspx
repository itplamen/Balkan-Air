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
            <asp:BoundField DataField="Departure" SortExpression="Departure" HeaderText="Departure" />
            <asp:BoundField DataField="Arrival" SortExpression="Arrival" HeaderText="Arrival" />
            <asp:BoundField DataField="Duration" SortExpression="Duration" HeaderText="Duration" />
            <asp:TemplateField HeaderText="Flight Status">
                <ItemTemplate>
                    <asp:Literal ID="FlightStatusLiberal" Text="<%#: Item.FlightStatus.Name %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="FlightStatusesDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.FlightStatus"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.FlightStatusId %>"
                        SelectMethod="FlightStatusesDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Aircraft.Model" HeaderText="Aircraft" />
            <asp:TemplateField HeaderText="From">
                <ItemTemplate>
                    <asp:Literal ID="FromAirportLiberal" Text='<%#: Item.DepartureAirport.Name + " (" + Item.DepartureAirport.Abbreviation +  ")" %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="FromAirportsDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: BindItem.DepartureAirport.Id %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To">
                <ItemTemplate>
                    <asp:Literal ID="ToAirportLiberal" Text='<%#: Item.ArrivalAirport.Name +  " (" + Item.ArrivalAirport.Abbreviation +  ")" %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ToAirportsDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AirportInfo"
                        SelectedValue="<%#: BindItem.ArrivalAirport.Id %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No flights found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel ID="CreateFlightPanel" runat="server" ClientIDMode="Static">
        <asp:RequiredFieldValidator ErrorMessage="Fligth number is required!" ControlToValidate="AddFlightNumberTextBox"
            ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

        <asp:RequiredFieldValidator ErrorMessage="Departure date is required!" ControlToValidate="DepartureDatepickerTextBox"
            ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

        <asp:RequiredFieldValidator ErrorMessage="Departure time is required!" ControlToValidate="DepartureTimeTextBox"
            ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

        <asp:RequiredFieldValidator ErrorMessage="Arrival date is required!" ControlToValidate="ArrivalDatepickerTextBox"
            ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

        <asp:RequiredFieldValidator ErrorMessage="Arrival time is required!" ControlToValidate="ArrivalTimeTextBox"
            ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" ValidationGroup="CreateNewFlight" />

        <h3>Create new flight</h3>

        <asp:Label runat="server" AssociatedControlID="AddFlightNumberTextBox" Text="Flight number (unique):" />
        <asp:TextBox ID="AddFlightNumberTextBox" ClientIDMode="Static" Style="text-transform: uppercase;" runat="server" ReadOnly="true" />
        <asp:Button ID="GenerateFlightNumberBtn" ClientIDMode="Static" Text="Generate number"
            UseSubmitBehavior="false" CssClass="btn btn-success btn-sm" runat="server" OnClick="GenerateFlightNumberBtn_Click" />

        <asp:Label runat="server" Text="Departure Date:" AssociatedControlID="DepartureDatepickerTextBox" />
        <asp:TextBox ID="DepartureDatepickerTextBox" ClientIDMode="Static" runat="server" />
        <span id="DepartureCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="DepartureDatepickerTextBox"
            CssClass="CalendarExtender"
            Format="d/MM/yyyy"
            PopupButtonID="DepartureCalendarIconSpan" />

        <asp:Label runat="server" Text="Departure Time:" AssociatedControlID="DepartureTimeTextBox" />
        <asp:TextBox runat="server" ID="DepartureTimeTextBox" ClientIDMode="Static" TextMode="Time" />
        <span class="glyphicon glyphicon-time"></span>

        <asp:Label runat="server" Text="Arrival Date:" AssociatedControlID="ArrivalDatepickerTextBox" />
        <asp:TextBox runat="server" ID="ArrivalDatepickerTextBox" ClientIDMode="Static" />
        <span id="ArrivalCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

        <ajaxToolkit:CalendarExtender runat="server"
            TargetControlID="ArrivalDatepickerTextBox"
            CssClass="CalendarExtender"
            Format="d.MM.yyyy"
            PopupButtonID="ArrivalCalendarIconSpan" />

        <asp:Label runat="server" Text="Arrival Time:" AssociatedControlID="ArrivalTimeTextBox" />
        <asp:TextBox runat="server" ID="ArrivalTimeTextBox" ClientIDMode="Static" TextMode="Time" />
        <span class="glyphicon glyphicon-time"></span>

        <asp:Label Text="Flight status:" runat="server" AssociatedControlID="FlightStatusesDropDownList" />
        <asp:DropDownList ID="FlightStatusesDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.FlightStatus"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="FlightStatusesDropDownList_GetData" />

        <asp:Label Text="Aircraft:" runat="server" AssociatedControlID="AircraftsDropDownList" />
        <asp:DropDownList ID="AircraftsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AircraftInfo"
            SelectMethod="AircraftsDropDownList_GetData" />

        <asp:Label Text="From Airport:" runat="server" AssociatedControlID="DepartureAirportsDropDownList" />
        <asp:DropDownList ID="DepartureAirportsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            AutoPostBack="true"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label Text="To Airport:" runat="server" AssociatedControlID="ArrivalAirportsDropDownList" />
        <asp:DropDownList ID="ArrivalAirportsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AirportInfo"
            AutoPostBack="true"
            SelectMethod="AirportsDropDownList_GetData" />

        <asp:Label Text="Travel Classes:" runat="server" AssociatedControlID="TravelClassesListBox" />
        <asp:ListBox ID="TravelClassesListBox" runat="server" SelectionMode="Multiple" required
            DataValueField="Id"
            DataTextField="TravelClassInfo"
            SelectMethod="TravelClassesListBox_GetData" />

        <br />
        <br />

        <asp:Button ID="CreateFlightBtn" ValidationGroup="CreateNewFlight" runat="server" Text="Create"
            CssClass="btn btn-info" UseSubmitBehavior="true" OnClick="CreateFlightBtn_Click" />
        <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
            UseSubmitBehavior="false" />
    </asp:Panel>

    <script type="text/javascript">
        $(function () {
            $('[id*=TravelClassesListBox]').multiselect({
                enableFiltering: true,
                includeSelectAllOption: true,
                enableCollapsibleOptGroups: true,
                maxHeight: 300,
                buttonWidth: '350px'
            });

        });
    </script>
</asp:Content>
