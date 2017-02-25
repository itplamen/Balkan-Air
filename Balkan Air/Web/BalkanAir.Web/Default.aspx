<%@ Page Title="Book your flights online with" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BalkanAir.Web._Default" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Web.Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="homeSlider">
        <img src="Content/Images/Destinations/Paris.jpg" alt="Paris Destination Image" />
        <img src="Content/Images/Destinations/Rome.jpg" alt="Rome Destination Image" />
        <img src="Content/Images/Destinations/Budapest.jpg" alt="Budapest Destination Image" />
        <img src="Content/Images/Destinations/London.jpg" alt="London Destination Image" />
        <img src="Content/Images/Destinations/Porto.jpg" alt="Porto Destination Image" />
    </div>

    <div id="SearchFlightsDiv">
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:RadioButton ID="ReturnRouteRadioButton" ClientIDMode="Static" Text="Return" runat="server"
                    GroupName="RouteOption" AutoPostBack="true" OnCheckedChanged="ReturnRouteRadioButton_CheckedChanged" />
                <asp:RadioButton ID="OneWayRadioButton" ClientIDMode="Static" Text="One way" runat="server"
                    GroupName="RouteOption" AutoPostBack="true" OnCheckedChanged="OnewayRadioButton_CheckedChanged" />

                <div class="fancyTextBox" id="DepartureFancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="From:" AssociatedControlID="DepartureAirportTextBox" />
                    <asp:TextBox ID="DepartureAirportTextBox" runat="server" placeholder="Departure Airport" AutoPostBack="true"
                        ClientIDMode="Static" />
                </div>

                <div class="fancyTextBox" id="DestinationFancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="To:" AssociatedControlID="DestinationAirportTextBox" />
                    <asp:TextBox ID="DestinationAirportTextBox" runat="server" placeholder="Arrival Airport"
                        AutoPostBack="true" ClientIDMode="Static" />
                </div>

                <asp:HiddenField ID="DepartureAirportIdHiddenField" runat="server" />
                <asp:HiddenField ID="DestinationAirportIdHiddenField" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:CustomValidator ID="InvalidDatesCustomValidator" Display="Dynamic" ForeColor="Red" ClientIDMode="Static" runat="server" />

        <div class="fancyTextBox" id="DepartureDateFancyTextBox">
            <asp:Label CssClass="label" runat="server" Text="Departure:" AssociatedControlID="DepartureDateTextBox" />
            <asp:TextBox ID="DepartureDateTextBox" runat="server" placeholder="Date" ClientIDMode="Static" />
        </div>

        <ajaxToolkit:CalendarExtender ID="DepartureCalendar" runat="server"
            TargetControlID="DepartureDateTextBox"
            CssClass="CalendarExtender"
            Format="d MMM yyyy"
            PopupButtonID="DepartureDateTextBox" />

        <asp:Panel runat="server" CssClass="fancyTextBox" ID="ArrivalDateFancyTextBox" ClientIDMode="Static">
            <asp:Label CssClass="label" runat="server" Text="Arrival:" AssociatedControlID="ArrivalDateTextBox" />
            <asp:TextBox ID="ArrivalDateTextBox" runat="server" placeholder="Date" ClientIDMode="Static" />
        </asp:Panel>

        <ajaxToolkit:CalendarExtender ID="ArrivalCalendarExtender" runat="server"
            TargetControlID="ArrivalDateTextBox"
            CssClass="CalendarExtender"
            Format="d MMM yyyy"
            PopupButtonID="ArrivalDateTextBox" />

        <div class="buttonBox">
            <asp:Button ID="SearchBtn" runat="server" ClientIDMode="Static" Text="Search" OnClick="OnFlightSearchButtonClicked" />
        </div>
    </div>

    <div id="DepartureAirportsDiv">
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Repeater ID="DepartureAirportsRepeater" runat="server"
                    ItemType="BalkanAir.Data.Models.Airport"
                    SelectMethod="DepartureAirportsRepeater_GetData">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%#: Item.Name + ", (" + Item.Abbreviation + ")" %>'
                            CssClass="airport" CommandArgument="<%# Item.Id %>" OnClick="OnSelectDepartureAirportButtonClicked" />
                        <span><%#: Item.Country.Name %></span>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="DestinationAirportsDiv">
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Repeater ID="DestinationAirportsRepeater" runat="server" ItemType="BalkanAir.Data.Models.Airport">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%#: Item.Name + ", (" + Item.Abbreviation + ")" %>'
                            CssClass="airport" CommandArgument="<%# Item.Id %>" OnClick="OnSelectDestinationAirportButtonClicked" />

                        <span><%#: Item.Country.Name %></span>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Literal ID="NoFlightsLiteral" Text="There is no result for this search!" Visible="false" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="TopCheapestFlightsDiv">
        <h2>Are you still looking for cheap flights?</h2>

        <asp:Repeater ID="TopCheapestFlightsRepeater" runat="server"
            ItemType="BalkanAir.Data.Models.LegInstance"
            SelectMethod="TopCheapestFlightsRepeater_GetData">
            <ItemTemplate>
                <asp:LinkButton runat="server" CommandArgument="<%#: Item.Id %>" CssClass="searchFlightElement"
                    OnClick="OnCheapFlightLinkButtonClicked">
                    <div class="cheapFlightDiv">
                        <div class="fromAirportInfo">
                            <span class="abbreviation"><%#: Item.FlightLeg.Route.Origin.Abbreviation %></span>
                            <span><%#: Item.FlightLeg.Route.Origin.Name %></span>
                        </div>
                        <img src="Content/Images/airplane_fly_out_image.png" alt="Fly out image" />
                        <div class="toAirportInfo">
                            <span class="abbreviation"><%#: Item.FlightLeg.Route.Destination.Abbreviation %></span>
                            <span><%#: Item.FlightLeg.Route.Destination.Name %></span>
                        </div>
                        <div class="priceAndDateInfo">
                            <p class="price">&#8364; <%#: Item.Price + Item.Aircraft.GetCheapestTravelClassPrice %></p>
                            <p class="date"><%#: Item.DepartureDateTime.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %></p>
                        </div>
                    </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
        <img src="Content/Images/airplane-background.jpg" id="BackgroundImg" alt="Airplane background image" />
    </div>

    <div id="LatestNewsDiv">
        <h2>Whats new?</h2>

        <div class="row">
            <asp:Repeater ID="LatestNewsRepeater" runat="server"
                ItemType="BalkanAir.Data.Models.News"
                SelectMethod="LatestNewsRepeater_GetData">
                <ItemTemplate>
                    <a href='<%= Page.ResolveUrl(Pages.VIEW_NEWS) + "?id=" %><%# Item.Id %>'>
                        <div class="col-md-4 latestArticle">
                            <p class="title">
                                <%#: !string.IsNullOrEmpty(Item.Title) && Item.Title.Length > 40 ? 
                                        Item.Title.Substring(0, 40) + "..." : Item.Title %>
                            </p>

                            <p>
                                <img runat="server" src='<%# Item.HeaderImage == null ? null : "data:image/jpeg;base64," + 
                                    Convert.ToBase64String(Item.HeaderImage) %>'
                                    visible="<%# Item.HeaderImage != null ? true : false %>" />
                            </p>

                            <p class="content">
                                <%# !string.IsNullOrEmpty(Item.Content) && Item.Content.Length > 140 ? 
                                        Item.Content.Substring(0, 140) + "..." : Item.Content %>
                            </p>

                            <p class="date">
                                <%#: Item.DateCreated.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                            </p>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script>
        $('#SearchBtn').click(areAllFieldsFilled);

        function areAllFieldsFilled() {
            var $departureAirportTextBox = $('#DepartureAirportTextBox'),
                $destinationAirportTextBox = $('#DestinationAirportTextBox'),
                $departureDateTextBox = $('#DepartureDateTextBox'),
                $arrivalDateTextBox = $('#ArrivalDateTextBox');

            if ($departureAirportTextBox.val() !== '' && $destinationAirportTextBox.val() !== '' &&
                $departureDateTextBox.val() !== '' && $arrivalDateTextBox.val() !== '') {

                $departureAirportTextBox.parent().css('border-color', '#E0E0E0');
                $destinationAirportTextBox.parent().css('border-color', '#E0E0E0');
                $departureDateTextBox.parent().css('border-color', '#E0E0E0');
                $arrivalDateTextBox.parent().css('border-color', '#E0E0E0');

                return true;
            }
            else if ($departureAirportTextBox.val() === '') {
                $departureAirportTextBox.parent().css('border-color', 'red');
            }
            else if ($destinationAirportTextBox.val() === '') {
                $destinationAirportTextBox.parent().css('border-color', 'red');
            }
            else if ($departureDateTextBox.val() === '') {
                $departureDateTextBox.parent().css('border-color', 'red');
            }
            else if ($arrivalDateTextBox.val() === '') {
                $arrivalDateTextBox.parent().css('border-color', 'red');
            }
            else {
                throw new Error('Invalid field ID!')
            }

            return false;
        }
    </script>
</asp:Content>
