<%@ Page Title="Extras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Extras.aspx.cs" Inherits="BalkanAir.Web.Booking.Extras" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <h3 class="routeInfo">
        <%: this.RouteInfo.Origin.Name %>, <small>(<%: this.RouteInfo.Origin.Abbreviation %>)</small> to 
        <%: this.RouteInfo.Destination.Name %>, <small>(<%: this.RouteInfo.Destination.Abbreviation %>)</small>
    </h3>

    <div class="extrasDiv row">
        <div id="OneWayRouteCheckedInBaggageDiv" class="col-md-6">
            <h4>CHECKED-IN BAGGAGE</h4>
            <small>PLEASE SELECT CHECKED-IN BAGGAGE</small>
            <br />

            <div>
                <input type="radio" id="OneWayRouteNoneCheckedInBag" class="checkedInBag" value="0"
                    required data-price="0" name="one-way-route-checked-in-bag" />
                <label for="OneWayRouteNoneCheckedInBag">None<span>Travel light</span></label>
            </div>

            <div>
                <input type="radio" id="OneWayRoute23KgCheckedInBag" class="checkedInBag" value="23"
                    required data-price="26" name="one-way-route-checked-in-bag" />
                <label for="OneWayRoute23KgCheckedInBag">23 kg<span>&#8364; 26.00</span></label>
            </div>

            <div>
                <input type="radio" id="OneWayRoute32KgCheckedInBag" class="checkedInBag" value="32"
                    required data-price="36" name="one-way-route-checked-in-bag" />
                <label for="OneWayRoute32KgCheckedInBag">32 kg<span>&#8364; 36.00</span></label>
            </div>

            <div class="totalWeightDiv">
                <img src="../Content/Images/total-weight-icon.png" alt="Total Weight Icon" />
                <p id="OneWayRoutePrice">Price: &#8364; <span id="OneWayRoutePriceSpan">0</span></p>
                <p>Total weight: <span id="OneWayRouteWeightSpan">0</span>kg</p>
                <span id="OneWayRouteNumericBag">
                    <input type="button" id="OneWayRouteRemoveBagBtn" value="-" />
                    <span id="OneWayRouteNumberOfBaggages">0</span>
                    <input type="button" id="OneWayRouteAddBagBtn" value="+" />
                </span>
            </div>
        </div>

        <div id="OneWayRouteCabinBaggageDiv" class="col-md-6">
            <h4>CABIN BAGGAGE</h4>
            <small>PLEASE SELECT CABIN BAGGAGE</small>
            <br />

            <div>
                <input id="OneWayRouteSmallCabinBag" type="radio" class="cabinBag" required
                    value="42 × 32 × 25 CM" data-price="0" name="one-way-route-cabin-bag" />
                <label for="OneWayRouteSmallCabinBag">Small<span>Free</span></label>
                <br />
                <small>42 × 32 × 25 CM</small>
            </div>

            <div>
                <input id="OneWayRouteLargeCabinBag" type="radio" class="cabinBag" required
                    value="56 × 45 × 25 CM" data-price="14" name="one-way-route-cabin-bag" />
                <label for="OneWayRouteLargeCabinBag">Large<span>&#8364; 14.00</span></label>
                <br />
                <small>56 × 45 × 25 CM</small>
            </div>
        </div>

        <div class="otherExtrasDiv col-md-12">
            <h4>OTHER EXTRAS</h4>

            <div>
                <asp:CheckBox ID="OneWayRouteBabyEquipmentCheckBox" Text="Baby equipment &#8364; 10.00" runat="server" />
            </div>

            <div>
                <asp:CheckBox ID="OneWayRouteSportsEquipmentCheckBox" Text="Sports equipment &#8364; 30.00" runat="server" />
            </div>

            <div>
                <asp:CheckBox ID="OneWayRouteMusicEquipmentCheckBox" Text="Music equipment &#8364; 50.00" runat="server" />
            </div>

            <div class="selectSeatDiv">
                <asp:Button ID="OneWayRouteSelectSeatBtn" ClientIDMode="Static" UseSubmitBehavior="false"
                    ValidateRequestMode="Disabled" OnClick="OneWayRouteSelectSeatBtn_Click" runat="server" />
                <asp:Image ImageUrl="~/Content/Images/selected-seat-icon.png" ID="OneWayRouteSelectedSeatImage" runat="server" />
                <asp:Label ID="OneWayRouteSelectedSeatLabel" runat="server" />
            </div>
        </div>
    </div>

    <asp:Panel ID="ReturnRouteExtrasPanel" runat="server" ClientIDMode="Static" CssClass="extrasDiv row" Visible="false">
        <h3 class="routeInfo">
            <%: this.RouteInfo.Destination.Name %>, <small>(<%: this.RouteInfo.Destination.Abbreviation %>)</small> to 
            <%: this.RouteInfo.Origin.Name %>, <small>(<%: this.RouteInfo.Origin.Abbreviation %>)</small>
        </h3>

        <div id="ReturnRouteCheckedInBaggageDiv" class="col-md-6">
            <h4>CHECKED-IN BAGGAGE</h4>
            <small>PLEASE SELECT CHECKED-IN BAGGAGE</small>
            <br />

            <div>
                <input type="radio" id="ReturnRouteNoneCheckedInBag" class="checkedInBag" value="0"
                    required data-price="0" name="return-route-checked-in-bag" />
                <label for="ReturnRouteNoneCheckedInBag">None<span>Travel light</span></label>
            </div>

            <div>
                <input type="radio" id="ReturnRoute23KgCheckedInBag" class="checkedInBag" value="23"
                    required data-price="26" name="return-route-checked-in-bag" />
                <label for="ReturnRoute23KgCheckedInBag">23 kg<span>&#8364; 26.00</span></label>
            </div>

            <div>
                <input type="radio" id="ReturnRoute32KgCheckedInBag" class="checkedInBag" value="32"
                    required data-price="36" name="return-route-checked-in-bag" />
                <label for="ReturnRoute32KgCheckedInBag">32 kg<span>&#8364; 36.00</span></label>
            </div>

            <div class="totalWeightDiv">
                <img src="../Content/Images/total-weight-icon.png" alt="Total Weight Icon" />
                <p id="ReturnRoutePrice">Price: &#8364; <span id="ReturnRoutePriceSpan">0</span></p>
                <p>Total weight: <span id="ReturnRouteWeightSpan">0</span>kg</p>
                <span id="ReturnRouteNumericBag">
                    <input type="button" id="ReturnRouteRemoveBagBtn" value="-" />
                    <span id="ReturnRouteNumberOfBaggages">0</span>
                    <input type="button" id="ReturnRouteAddBagBtn" value="+" />
                </span>
            </div>
        </div>

        <div id="ReturnRouteCabinBaggageDiv" class="col-md-6">
            <h4>CABIN BAGGAGE</h4>
            <small>PLEASE SELECT CABIN BAGGAGE</small>
            <br />

            <div>
                <input id="ReturnRouteSmallCabinBag" type="radio" class="cabinBag" required
                    value="42 × 32 × 25 CM" data-price="0" name="return-route-cabin-bag" />
                <label for="ReturnRouteSmallCabinBag">Small<span>Free</span></label>
                <br />
                <small>42 × 32 × 25 CM</small>
            </div>

            <div>
                <input id="ReturnRouteLargeCabinBag" type="radio" class="cabinBag" required
                    value="56 × 45 × 25 CM" data-price="14" name="return-route-cabin-bag" />
                <label for="ReturnRouteLargeCabinBag">Large<span>&#8364; 14.00</span></label>
                <br />
                <small>56 × 45 × 25 CM</small>
            </div>
        </div>

        <div class="otherExtrasDiv col-md-12">
            <h4>OTHER EXTRAS</h4>

            <div>
                <asp:CheckBox ID="ReturnRouteBabyEquipmentCheckBox" Text="Baby equipment &#8364; 10.00" runat="server" />
            </div>

            <div>
                <asp:CheckBox ID="ReturnRouteSportsEquipmentCheckBox" Text="Sports equipment &#8364; 30.00" runat="server" />
            </div>

            <div>
                <asp:CheckBox ID="ReturnRouteMusicEquipmentCheckBox" Text="Music equipment &#8364; 50.00" runat="server" />
            </div>

            <div class="selectSeatDiv">
                <asp:Button ID="ReturnRouteSelectSeatBtn" ClientIDMode="Static" UseSubmitBehavior="false"
                    ValidateRequestMode="Disabled" OnClick="ReturnRouteSelectSeatBtn_Click" runat="server" />
                <asp:Image ImageUrl="~/Content/Images/selected-seat-icon.png" ID="ReturnRouteSelectedSeatImage" runat="server" />
                <asp:Label ID="ReturnRouteSelectedSeatLabel" runat="server" />
            </div>
        </div>
    </asp:Panel>

    <div id="ContinueBookingDiv">
        <asp:Button ID="ContinueBookingBtn" Text="Continue" runat="server" ClientIDMode="Static"
            OnClick="OnContinueBookingBtnClicked" />
    </div>

    <asp:HiddenField ID="OneWayRouteSelectedCheckedInBagPriceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="OneWayRouteSelectedCheckedInBagWeightHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="OneWayRouteNumberOfCheckedInBagsHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="OneWayRouteSelectedCabinBagPriceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="OneWayRouteSelectedCabinBagSizeHiddenField" ClientIDMode="Static" runat="server" />

    <asp:HiddenField ID="ReturnRouteSelectedCheckedInBagPriceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="ReturnRouteSelectedCheckedInBagWeightHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="ReturnRouteNumberOfCheckedInBagsHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="ReturnRouteSelectedCabinBagPriceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="ReturnRouteSelectedCabinBagSizeHiddenField" ClientIDMode="Static" runat="server" />
</asp:Content>
