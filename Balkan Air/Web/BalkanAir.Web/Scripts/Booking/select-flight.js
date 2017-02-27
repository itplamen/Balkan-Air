$(function () {
    'use strict';

    var documentFragment = document.createDocumentFragment(),
        position = { my: 'center bottom+45', at: 'center top' },
        $onlineCkeckIn = $('<p><img src="../Content/Images/online-check-in-icon.png" />Online ckeck-in</p>'),
        $mealAndDrinks = $('<p><img src="../Content/Images/meal-icon.png" />Meal and drinks</p>'),
        $priorityBoarding = $('<p><img src="../Content/Images/priority-boarding-icon.png" /> Priority boarding</p>'),
        $reservedSeat = $('<p><img src="../Content/Images/reserved-seat-icon.png" />Reserved seat, seat selection for the relevant travel class</p>'),
        $extraBaggage = $('<p><img src="../Content/Images/extra-baggage-icon.png" />Extra baggage</p>'),
        $earnMiles = $('<p><img src="../Content/Images/earn-miles-icon.png" />Earn miles</p>'),
        $standartContent = $('<div class="tooltipDiv" />').append($onlineCkeckIn, $mealAndDrinks, $reservedSeat);

    $('.EconomyClassDiv').tooltip({
        position: position,
        content: documentFragment.appendChild($standartContent[0].cloneNode(true))
    });

    $('.BusinessClassDiv').tooltip({
        position: position,
        content: documentFragment.appendChild($standartContent.append($priorityBoarding, $extraBaggage)[0].cloneNode(true))
    });

    $('.FirstClassDiv').tooltip({
        position: position,
        content: documentFragment.appendChild($standartContent.append($priorityBoarding, $extraBaggage, $earnMiles)[0].cloneNode(true))
    });

    // Click events can't fire, because of the UpdatePanel. The solution is to use Sys.WebForms.PageRequestManage.
    var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();

    // Re-bind jQuery events.
    pageRequestManager.add_endRequest(function () {
        $('.oneWayRouteTravelClasses .travelClassPriceSpan input[type="radio"]').click(selectOneWayRouteTravelClass);
        $('.returnRouteTravelClasses .travelClassPriceSpan input[type="radio"]').click(selectReturnRouteTravelClass);
    });
});


$(document).ready(function () {
    var INVALID_DATA_VALUE = 0,
        $noOneWayRouteFlightsDiv = $('#NoOneWayRouteFlightsDiv'),
        $noReturnRouteFlightsDiv = $('#NoReturnRouteFlightsDiv'),
        $continueBookingBtn = $('#ContinueBookingBtn');

    if (!$('div.oneWayRouteFlights.slick-current').hasClass('noFlightDatesDiv')) {
        $noOneWayRouteFlightsDiv.hide();
    }

    if (!$('div.returnRouteFlights.slick-current').hasClass('noFlightDatesDiv')) {
        $noReturnRouteFlightsDiv.hide();
    }

    $('.travelClassPriceSpan input[type="radio"].noMoreSeats').attr('disabled', true);

    // Bind jQuery events initially
    $('.oneWayRouteTravelClasses .travelClassPriceSpan input[type="radio"]').click(selectOneWayRouteTravelClass);
    $('.returnRouteTravelClasses .travelClassPriceSpan input[type="radio"]').click(selectReturnRouteTravelClass);

    $('.oneWayRouteFlights')
        .add('#OneWayRouteDepartureDatesDiv .slick-arrow')
        .click(function () {
            var currentFlightDate = $('div.oneWayRouteFlights.slick-current')[0],
                showOneWayFlgihtInfoHiddenButton = $('#ShowOneWayFlgihtInfoHiddenButton')[0],
                flightDataValue = $(currentFlightDate).attr('data-value'),
                $oneWayRouteSelectedFlightDetailsDiv = $('#OneWayRouteSelectedFlightDetailsDiv');

            if (flightDataValue !== '' && parseInt(flightDataValue, 10) !== INVALID_DATA_VALUE) {
                if (!$oneWayRouteSelectedFlightDetailsDiv.is(':visible')) {
                    $noOneWayRouteFlightsDiv.hide();
                    $oneWayRouteSelectedFlightDetailsDiv.show();
                    $continueBookingBtn.prop('disabled', false);
                }

                $('#OneWayRouteCurrentFlightInfoIdHiddenField').val(flightDataValue);
                showOneWayFlgihtInfoHiddenButton.click();
            }
            else {
                $oneWayRouteSelectedFlightDetailsDiv.hide();
                $noOneWayRouteFlightsDiv.show();
                $continueBookingBtn.prop('disabled', true);
            }
        });
    
    $('.returnRouteFlights')
        .add('#ReturnRouteDepartureDatesDiv .slick-arrow')
        .click(function () {
            var currentFlightDate = $('div.returnRouteFlights.slick-current')[0],
                showReturnFlgihtInfoHiddenButton = $('#ShowReturnFlgihtInfoHiddenButton')[0],
                flightDataValue = $(currentFlightDate).attr('data-value'),
                $returnRouteSelectedFlightDetailsDiv = $('#ReturnRouteSelectedFlightDetailsDiv');

            if (flightDataValue !== '' && parseInt(flightDataValue, 10) !== INVALID_DATA_VALUE) {
                if (!$returnRouteSelectedFlightDetailsDiv.is(':visible')) {
                    $noReturnRouteFlightsDiv.hide();
                    $returnRouteSelectedFlightDetailsDiv.show();
                    $continueBookingBtn.prop('disabled', false);
                }

                $('#ReturnRouteCurrentFlightInfoIdHiddenField').val(flightDataValue);
                showReturnFlgihtInfoHiddenButton.click();
            }
            else {
                $returnRouteSelectedFlightDetailsDiv.hide();
                $noReturnRouteFlightsDiv.show();
                $continueBookingBtn.prop('disabled', true);
            }
        });
});

function selectOneWayRouteTravelClass(event) {
    $('#OneWayRouteSelectedTravelClassIdHiddenField').val($(event.target).val());
    $('.oneWayRouteTravelClasses .travelClassPriceSpan').css('background-color', 'initial');
    $(event.target).closest('.oneWayRouteTravelClasses .travelClassPriceSpan').css('background-color', 'pink');
}

function selectReturnRouteTravelClass(event) {
    $('#ReturnRouteSelectedTravelClassIdHiddenField').val($(event.target).val());
    $('.returnRouteTravelClasses .travelClassPriceSpan').css('background-color', 'initial');
    $(event.target).closest('.returnRouteTravelClasses .travelClassPriceSpan').css('background-color', 'pink');
}
 