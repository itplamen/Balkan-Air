<%@ Page Title="Extras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Extras.aspx.cs" Inherits="BalkanAir.Web.Booking.Extras" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>
    
    <div id="ExtrasDiv" class="row">
        <div id="CheckedInBaggageDiv" class="col-md-6">
            <h4>CHECKED-IN BAGGAGE</h4>
            <small>PLEASE SELECT CHECKED-IN BAGGAGE</small> <br />

            <div>
                <input type="radio" id="NoneCheckedInBag" class="checkedInBag" value="0" required data-price="0" name="checked-in-bag" />
                <label for="NoneCheckedInBag">None<span>Travel light</span></label>
            </div>

            <div>
                <input type="radio" id="23KgCheckedInBag" class="checkedInBag" value="23" required data-price="26" name="checked-in-bag"" />
                <label for="23KgCheckedInBag">23 kg<span>&#8364; 26.00</span></label>
            </div>

            <div>
                <input type="radio" id="32KgCheckedInBag" class="checkedInBag" value="32" required data-price="36" name="checked-in-bag" />
                <label for="32KgCheckedInBag">32 kg<span>&#8364; 36.00</span></label>
            </div>

            <div id="TotalWeightDiv">
                <img src="../Content/Images/total-weight-icon.png" alt="Total Weight Icon" />
                <p id="Price">Price: &#8364; <span id="PriceSpan">0</span></p>
                <p>Total weight: <span id="WeightSpan">0</span>kg</p>
                <span id="NumericBag">
                    <input type="button" id="RemoveBagBtn" value="-" />
                    <span id="NumberOfBaggages">0</span>
                    <input type="button" id="AddBagBtn" value="+" />
                </span>
            </div>
        </div>

        <div id="CabinBaggageDiv" class="col-md-6">
            <h4>CABIN BAGGAGE</h4>
            <small>PLEASE SELECT CABIN BAGGAGE</small> <br />

            <div>
                <input type="radio" id="SmallCabinBag" class="cabinBag" required value="42 × 32 × 25 CM" data-price="0" name="cabin-bag" />
                <label for="SmallCabinBag">Small<span>Free</span></label> <br />
                <small>42 × 32 × 25 CM</small>
            </div>

            <div>
                <input type="radio" id="LargeCabinBag" class="cabinBag" required value="56 × 45 × 25 CM" data-price="14" name="cabin-bag" />
                <label for="LargeCabinBag">Large<span>&#8364; 14.00</span></label> <br />
                <small>56 × 45 × 25 CM</small>
            </div>
        </div>

        <div id="OtherExtrasDiv" class="col-md-12">
            <h4>OTHER EXTRAS</h4>

            <div id="BabyEquipmentDiv">
                <asp:CheckBox ID="BabyEquipmentCheckBox" Text="Baby equipment &#8364; 10.00" runat="server" />
            </div>

            <div id="SportsEquipmentDiv">
                <asp:CheckBox ID="SportsEquipmentCheckBox" Text="Sports equipment &#8364; 30.00" runat="server" />
            </div>
            
            <div id="MusicEquipmentDiv">
                <asp:CheckBox ID="MusicEquipmentCheckBox" Text="Music equipment &#8364; 50.00" runat="server" />
            </div>

            <div id="SelectSeatDiv">
                <asp:Button ID="SelectSeatBtn" ClientIDMode="Static" UseSubmitBehavior="false" OnClick="SelectSeatBtn_Click" runat="server" />
                <asp:Image ImageUrl="~/Content/Images/selected-seat-icon.png" ID="SelectedSeatImage" runat="server" />
                <asp:Label ID="SelectedSeatLabel" runat="server" />
            </div>
        </div> 
    </div>

    <div id="ContinueBookingDiv">
        <asp:Button ID="ContinueBookingBtn" Text="Continue" runat="server" ClientIDMode="Static"
            OnClick="OnContinueBookingBtnClicked" />
    </div>

    <asp:HiddenField ID="SelectedCheckedInBagPrice" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SelectedCheckedInBagWeight" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="NumberOfCheckedInBags" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SelectedCabinBagPrice" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SelectedCabinBagSize" ClientIDMode="Static" runat="server" />
</asp:Content>
