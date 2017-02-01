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
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="FlightStatusLabel" Text="Flight Status" runat="server" />
                </HeaderTemplate>
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
            <asp:BoundField DataField="Aircraft.Id" HeaderText="Aircraft" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="FromAirportLabel" Text="From" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="FromAirportLiberal" Text="<%#: Item.DepartureAirport.Name %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="FromAirportsDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.Airport"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.DepartureAirport.Id %>"
                        SelectMethod="AirportsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="ToAirportLabel" Text="To" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Literal ID="ToAirportLiberal" Text="<%#: Item.ArrivalAirport.Name %>" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ToAirportsDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.Airport"
                        DataValueField="Id"
                        DataTextField="Name"
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

    <asp:UpdatePanel ID="CreateFlightPanel" runat="server" ClientIDMode="Static" UpdateMode="Always">
        <ContentTemplate>
            <asp:CustomValidator ErrorMessage="Flight number must contains exactly 6 characters!" ControlToValidate="AddFlightNumberTextBox"
                ForeColor="Red" Display="Dynamic" ClientValidationFunction="isFlightNumberLengthValid" runat="server" />

            <asp:RequiredFieldValidator ErrorMessage="Fligth number is required!" ControlToValidate="AddFlightNumberTextBox"
                ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" />

            <asp:RequiredFieldValidator ErrorMessage="Departure date is required!" ControlToValidate="DepartureDatepickerTextBox"
                ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" />

            <asp:RequiredFieldValidator ErrorMessage="Departure time is required!" ControlToValidate="DepartureTimeTextBox"
                ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" />

            <asp:RequiredFieldValidator ErrorMessage="Arrival date is required!" ControlToValidate="ArrivalDatepickerTextBox"
                ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" />

            <asp:RequiredFieldValidator ErrorMessage="Arrival time is required!" ControlToValidate="ArrivalTimeTextBox"
                ForeColor="Red" Display="Dynamic" runat="server" CssClass="validatorSpan" />

            <h3>Create new flight</h3>

            <asp:Label runat="server" AssociatedControlID="AddFlightNumberTextBox" Text="Flight number (unique):" />
            <asp:TextBox ID="AddFlightNumberTextBox" ClientIDMode="Static" Style="text-transform: uppercase;" runat="server" ReadOnly="true" />
            <asp:Button ID="GenerateFlightNumberBtn" ClientIDMode="Static" Text="Generate number"
                UseSubmitBehavior="false" CssClass="btn btn-success btn-sm" runat="server" OnClick="GenerateFlightNumberBtn_Click" />

            <asp:Label runat="server" Text="Departure Date:" AssociatedControlID="DepartureDatepickerTextBox" />
            <asp:TextBox ID="DepartureDatepickerTextBox" ClientIDMode="Static" runat="server" ReadOnly="true" />
            <span id="DepartureCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

            <ajaxToolkit:CalendarExtender runat="server"
                TargetControlID="DepartureDatepickerTextBox"
                CssClass="CalendarExtender"
                Format="MMMM d, yyyy"
                PopupButtonID="DepartureCalendarIconSpan"
                StartDate="8/3/2010"
                EndDate="10/7/2010" />

            <asp:Label runat="server" Text="Departure Time:" AssociatedControlID="DepartureTimeTextBox" />
            <asp:TextBox runat="server" ID="DepartureTimeTextBox" ClientIDMode="Static" TextMode="Time" />
            <span class="glyphicon glyphicon-time"></span>

            <asp:Label runat="server" Text="Arrival Date:" AssociatedControlID="ArrivalDatepickerTextBox" />
            <asp:TextBox runat="server" ID="ArrivalDatepickerTextBox" ClientIDMode="Static" ReadOnly="true" />
            <span id="ArrivalCalendarIconSpan" class="glyphicon glyphicon-calendar"></span>

            <ajaxToolkit:CalendarExtender runat="server"
                TargetControlID="ArrivalDatepickerTextBox"
                CssClass="CalendarExtender"
                Format="MMMM d, yyyy"
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

            <asp:Label Text="From Airport:" runat="server" AssociatedControlID="DepartureAirportsDropDownList" />
            <asp:DropDownList ID="DepartureAirportsDropDownList" runat="server"
                DataValueField="Id"
                DataTextField="AirportInfo"
                AutoPostBack="true"
                SelectMethod="DepartureAirportsDropDownList_GetData"
                OnSelectedIndexChanged="DepartureAirportsDropDownList_SelectedIndexChanged" />
            
            <asp:Label Text="To Airport:" runat="server" AssociatedControlID="ArrivalAirportsDropDownList" />
            <asp:DropDownList ID="ArrivalAirportsDropDownList" runat="server"
                DataValueField="Id"
                DataTextField="AirportInfo"
                AutoPostBack="true" />

            <br />
            <asp:Button ID="CreateFlightBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateFlightBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
