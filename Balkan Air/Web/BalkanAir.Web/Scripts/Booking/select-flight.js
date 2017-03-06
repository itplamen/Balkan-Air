$(function () {
    'use strict';

    var INVALID_DATA_VALUE = 0,
        pageRequestManager,
        documentFragment = document.createDocumentFragment(),
        position = { my: 'center bottom+45', at: 'center top' },
        $onlineCkeckIn = $('<p><img src="../Content/Images/online-check-in-icon.png" />Online ckeck-in</p>'),
        $mealAndDrinks = $('<p><img src="../Content/Images/meal-icon.png" />Meal and drinks</p>'),
        $priorityBoarding = $('<p><img src="../Content/Images/priority-boarding-icon.png" /> Priority boarding</p>'),
        $reservedSeat = $('<p><img src="../Content/Images/reserved-seat-icon.png" />Reserved seat, seat selection for the relevant travel class</p>'),
        $extraBaggage = $('<p><img src="../Content/Images/extra-baggage-icon.png" />Extra baggage</p>'),
        $earnMiles = $('<p><img src="../Content/Images/earn-miles-icon.png" />Earn miles</p>'),
        $standartContent = $('<div class="tooltipDiv" />').append($onlineCkeckIn, $mealAndDrinks, $reservedSeat),
        $noOneWayRouteFlightsDiv = $('#NoOneWayRouteFlightsDiv'),
        $noReturnRouteFlightsDiv = $('#NoReturnRouteFlightsDiv');

    manageContinueBookingDivBox();

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
    pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();

    // Re-bind jQuery events.
    pageRequestManager.add_endRequest(function () {
        $('.oneWayRouteTravelClasses input[type="radio"]').change(function () {
            selectTravelClass($(this), $('#OneWayRouteSelectedTravelClassIdHiddenField'), '.oneWayRouteTravelClasses');
        });

        $('.returnRouteTravelClasses input[type="radio"]').change(function () {
            selectTravelClass($(this), $('#ReturnRouteSelectedTravelClassIdHiddenField'), '.returnRouteTravelClasses');
        });
    });

    if (!$('div.oneWayRouteFlights.slick-current').hasClass('noFlightDatesDiv')) {
        $noOneWayRouteFlightsDiv.hide();
    }

    if (!$('div.returnRouteFlights.slick-current').hasClass('noFlightDatesDiv')) {
        $noReturnRouteFlightsDiv.hide();
    }

    $('.travelClassPriceSpan input[type="radio"].noMoreSeats').attr('disabled', true);

    // Bind jQuery events initially.
    $('.oneWayRouteTravelClasses input[type="radio"]').change(function () {
        selectTravelClass($(this), $('#OneWayRouteSelectedTravelClassIdHiddenField'), '.oneWayRouteTravelClasses');
    });

    $('.returnRouteTravelClasses input[type="radio"]').change(function () {
        selectTravelClass($(this), $('#ReturnRouteSelectedTravelClassIdHiddenField'), '.returnRouteTravelClasses');
    });

    $('.oneWayRouteFlights')
        .add('#OneWayRouteDepartureDatesDiv .slick-arrow')
        .click(function () {
            flightDetailsManagement($('#ShowOneWayFlgihtInfoHiddenButton'), $('div.oneWayRouteFlights.slick-current'),
                $('#OneWayRouteSelectedFlightDetailsDiv'), $noOneWayRouteFlightsDiv,
                $('#OneWayRouteCurrentFlightInfoIdHiddenField'), '.oneWayRouteTravelClasses');
        });

    $('.returnRouteFlights')
        .add('#ReturnRouteDepartureDatesDiv .slick-arrow')
        .click(function () {
            flightDetailsManagement($('#ShowReturnFlgihtInfoHiddenButton'), $('div.returnRouteFlights.slick-current'),
                $('#ReturnRouteSelectedFlightDetailsDiv'), $noReturnRouteFlightsDiv,
                $('#ReturnRouteCurrentFlightInfoIdHiddenField'), '.returnRouteTravelClasses');
        });

    function selectTravelClass($selectedTravelClass, $travelClassIdHidden, travelClasses) {
        $travelClassIdHidden.val($selectedTravelClass.val());
        $(travelClasses + ' .travelClassPriceSpan').css('background-color', 'initial');
        $selectedTravelClass.closest(travelClasses + ' .travelClassPriceSpan').css('background-color', 'pink');
        manageContinueBookingDivBox();
    }

    function flightDetailsManagement($hiddenBtn, $currentFlight, $flightDetailsDiv, $noFlightsDiv, $flightIdHidden, travelClasses) {
        var flightDataValue = $currentFlight.attr('data-value');

        if (flightDataValue !== '' && parseInt(flightDataValue, 10) !== INVALID_DATA_VALUE) {
            if ($noFlightsDiv.is(':visible')) {
                $noFlightsDiv.hide();
                $flightDetailsDiv.show();
            }

            $flightIdHidden.val(flightDataValue);
            $hiddenBtn[0].click();
        }
        else {
            $flightDetailsDiv.hide();
            $noFlightsDiv.show();

            $(travelClasses + ' input[type="radio"]:checked').attr('checked', false);
        }

        manageContinueBookingDivBox();
    }

    // Manage continue booking button and span helper.
    function manageContinueBookingDivBox() {
        var OUTBOUND_FLIGHT_NOT_SELECTED = 'CHOOSE AN OUTBOUND FLIGHT!',
            RETURN_FLIGHT_NOT_SELECTED = 'CHOOSE A RETURN FLIGHT!',
            CONTINUE_BOOKING = 'EVERYTHING LOOKS FINE. PLEASE CONTINUE!',
            $oneWayRouteSelectedTravelClass = $('.oneWayRouteTravelClasses input[type="radio"]:checked'),
            $returnRouteSelectedTravelClass;

        if (!$oneWayRouteSelectedTravelClass.prop('checked')) {
            setContinueBookingDivBox(OUTBOUND_FLIGHT_NOT_SELECTED, true);
        }
        else {
            if ($('#ReturnRouteFlightsPanel').is(':visible')) {
                $returnRouteSelectedTravelClass = $('.returnRouteTravelClasses input[type="radio"]:checked');

                if (!$returnRouteSelectedTravelClass.prop('checked')) {
                    setContinueBookingDivBox(RETURN_FLIGHT_NOT_SELECTED, true);
                    return;
                }                
            }

            setContinueBookingDivBox(CONTINUE_BOOKING, false);
        }
    }

    function setContinueBookingDivBox(helperText, isButtonDisabled) {
        $('#BookingHelperSpan').html(helperText);
        $('#ContinueBookingBtn').prop('disabled', isButtonDisabled);
    }
});
