<%@ Page Title="Book your flights online with" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BalkanAir.Web._Default" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Web.Common" %>

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
                <div class="fancyTextBox" id="DepartureFancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="From:" AssociatedControlID="DepartureAirportTextBox" />
                    <asp:TextBox ID="DepartureAirportTextBox" runat="server" placeholder="Departure Airport" AutoPostBack="true"
                        ClientIDMode="Static" ReadOnly="true" />
                </div>

                <div class="fancyTextBox" id="DestinationFancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="To:" AssociatedControlID="DestinationAirportTextBox" />
                    <asp:TextBox ID="DestinationAirportTextBox" runat="server" placeholder="Destination Airport"
                        AutoPostBack="true" ClientIDMode="Static" ReadOnly="true" />
                </div>

                <div class="buttonBox">
                    <asp:Button ID="SearchBtn" runat="server" ClientIDMode="Static" Text="Search" OnClick="OnFlightSearchButtonClicked" />
                </div>

                <asp:HiddenField ID="DepartureAirportIdHiddenField" runat="server" />
                <asp:HiddenField ID="DestinationAirportIdHiddenField" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
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
        <asp:UpdatePanel ID="adddd" runat="server" UpdateMode="Always">
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
                            <p class="price">&#8364; <%#: Item.Price %></p>
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
                                    Convert.ToBase64String(Item.HeaderImage) %>' visible="<%# Item.HeaderImage != null ? true : false %>" />
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
</asp:Content>
