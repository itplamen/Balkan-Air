$(function () {
    'use strict';

    var MIN_CHECKED_IN_BAGGAGES = 1,
        MAX_CHECKED_IN_BAGGAGES = 3,
        DATA_PRICE_ATTR = 'data-price',
        DECIMAL_PLACES = 2,
        $oneWayRoute23KgCheckedInBag = $('#OneWayRoute23KgCheckedInBag'),
        $oneWayRoute32KgCheckedInBag = $('#OneWayRoute32KgCheckedInBag'),
        $oneWayRouteAddRemoveBagSpan = $('#OneWayRouteAddRemoveBagSpan'),
        $oneWayRouteWeightSpan = $('#OneWayRouteWeightSpan'),
        $oneWayRoutePrice = $('#OneWayRoutePrice'),
        $oneWayRoutePriceSpan = $('#OneWayRoutePriceSpan'),
        $oneWayRouteNumberOfBaggage = $('#OneWayRouteNumberOfBaggage'),
        $oneWayRouteBagsHidden = $('#OneWayRouteNumberOfCheckedInBagsHiddenField'),
        $oneWayRouteSelectedCheckedInBag,
        $returnRoute23KgCheckedInBag = $('#ReturnRoute23KgCheckedInBag'),
        $returnRoute32KgCheckedInBag = $('#ReturnRoute32KgCheckedInBag'),
        $returnRouteAddRemoveBagSpan = $('#ReturnRouteAddRemoveBagSpan'),
        $returnRouteWeightSpan = $('#ReturnRouteWeightSpan'),
        $returnRoutePrice = $('#ReturnRoutePrice'),
        $returnRoutePriceSpan = $('#ReturnRoutePriceSpan'),
        $returnRouteNumberOfBaggage = $('#ReturnRouteNumberOfBaggage'),
        $returnRouteBagsHidden = $('#ReturnRouteNumberOfCheckedInBagsHiddenField'),
        $returnRouteSelectedCheckedInBag;

    setCheckedInBagsInfo();
    manageContinueBookingDivBox();

    $('#OneWayRouteNoneCheckedInBag').change(function () {
        setCheckedInBagsInfo();
        $oneWayRouteSelectedCheckedInBag = null;
        clearCheckedInBagsInfo($oneWayRoutePriceSpan, $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);
    });

    $('#ReturnRouteNoneCheckedInBag').change(function () {
        setCheckedInBagsInfo();
        $returnRouteSelectedCheckedInBag = null;
        clearCheckedInBagsInfo($returnRoutePriceSpan, $returnRouteWeightSpan, $returnRouteNumberOfBaggage, $returnRouteBagsHidden);
    });
    
    $oneWayRoute23KgCheckedInBag
        .add($oneWayRoute32KgCheckedInBag)
        .add($returnRoute23KgCheckedInBag)
        .add($returnRoute32KgCheckedInBag)
        .change(function () {
            setCheckedInBagsInfo();
        });

    $('#OneWayRouteExtrasPanel input[type="radio"]')
        .add('#ReturnRouteExtrasPanel input[type="radio"]')
        .change(manageContinueBookingDivBox);

    $('#OneWayRouteAddBagBtn')
        .click(function () {
            addCheckedInBag($oneWayRouteSelectedCheckedInBag, $oneWayRoutePriceSpan,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);
        });

    $('#OneWayRouteRemoveBagBtn')
        .click(function () {
            removeCheckedInBag($oneWayRouteSelectedCheckedInBag, $oneWayRoutePriceSpan,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);
        });

    $('#ReturnRouteAddBagBtn')
       .click(function () {
           addCheckedInBag($returnRouteSelectedCheckedInBag, $returnRoutePriceSpan,
                $returnRouteWeightSpan, $returnRouteNumberOfBaggage, $returnRouteBagsHidden);
       });

    $('#ReturnRouteRemoveBagBtn')
        .click(function () {
            removeCheckedInBag($returnRouteSelectedCheckedInBag, $returnRoutePriceSpan,
                $returnRouteWeightSpan, $returnRouteNumberOfBaggage, $returnRouteBagsHidden);
        });
    
    function setCheckedInBagsInfo() {
        if ($oneWayRoute23KgCheckedInBag.is(':checked') || $oneWayRoute32KgCheckedInBag.is(':checked')) {
            if ($oneWayRoute23KgCheckedInBag.is(':checked')) {
                $oneWayRouteSelectedCheckedInBag = $oneWayRoute23KgCheckedInBag;
            }
            else if ($oneWayRoute32KgCheckedInBag.is(':checked')) {
                $oneWayRouteSelectedCheckedInBag = $oneWayRoute32KgCheckedInBag;
            }
            else {
                throw new Error('Invalid checked-in bag!');
            }

            initializeCheckedInBagsInfo($oneWayRouteSelectedCheckedInBag, $oneWayRoutePriceSpan,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);

            showCheckedInBagsInfo($oneWayRouteAddRemoveBagSpan, $oneWayRoutePrice);
        }
        else {
            hideCheckedInBagsInfo($oneWayRouteAddRemoveBagSpan, $oneWayRoutePrice);
        }

        if ($('#ReturnRouteExtrasPanel').is(':visible') &&
            ($returnRoute23KgCheckedInBag.is(':checked') || $returnRoute32KgCheckedInBag.is(':checked'))) {
            if ($returnRoute23KgCheckedInBag.is(':checked')) {
                $returnRouteSelectedCheckedInBag = $returnRoute23KgCheckedInBag;
            }
            else if ($returnRoute32KgCheckedInBag.is(':checked')) {
                $returnRouteSelectedCheckedInBag = $returnRoute32KgCheckedInBag;
            }
            else {
                throw new Error('Invalid checked-in bag!');
            }

            initializeCheckedInBagsInfo($returnRouteSelectedCheckedInBag, $returnRoutePriceSpan,
                $returnRouteWeightSpan, $returnRouteNumberOfBaggage, $returnRouteBagsHidden);

            showCheckedInBagsInfo($returnRouteAddRemoveBagSpan, $returnRoutePrice);
        }
        else {
            hideCheckedInBagsInfo($returnRouteAddRemoveBagSpan, $returnRoutePrice);
        }
    }

    function showCheckedInBagsInfo($addRemoveBagsButtons, $priceField) {
        $addRemoveBagsButtons.show();
        $priceField.show();
    }

    function hideCheckedInBagsInfo($addRemoveBagsButtons, $priceField) {
        $addRemoveBagsButtons.hide();
        $priceField.hide();
    }

    function clearCheckedInBagsInfo($priceSpan, $weightSpan, $numberOfBags, $numberOfBagsHiddenField) {
        $priceSpan.html(0);
        $weightSpan.html(0);
        $numberOfBags.html(0);
        $numberOfBagsHiddenField.val(0);
    }

    function initializeCheckedInBagsInfo($checkedInBag, $priceSpan, $weightSpan, $bagsNumber, $bagsNumberHidden) {
        var numberOfBags = parseInt($bagsNumberHidden.val(), 10),
            checkedInBagPrice = parseFloat(Math.round($checkedInBag.parent().attr(DATA_PRICE_ATTR) * 100) / 100),
            checkedInBagWeight = parseInt($checkedInBag.val(), 10);

        if (numberOfBags && numberOfBags > MIN_CHECKED_IN_BAGGAGES) {
            checkedInBagPrice *= numberOfBags
            checkedInBagWeight *= numberOfBags;

            $bagsNumber.html(numberOfBags);
            $bagsNumberHidden.val(numberOfBags);
        }
        else {
            numberOfBags = MIN_CHECKED_IN_BAGGAGES;
        }
        
        $priceSpan.html(checkedInBagPrice.toFixed(DECIMAL_PLACES));
        $weightSpan.html(checkedInBagWeight);
        $bagsNumber.html(numberOfBags);
        $bagsNumberHidden.val(numberOfBags);
    }

    function addCheckedInBag($checkedInBag, $priceSpan, $weightSpan, $bagsNumber, $bagsNumberHidden) {
        var numberOfBags,
            currentPrice,
            priceToAdd,
            currentWeight;

        if (!$checkedInBag) {
            throw new Error('There is no selected checked-in bag!');
        }

        numberOfBags = parseInt($bagsNumber.html(), 10);

        if (numberOfBags < MAX_CHECKED_IN_BAGGAGES) {
            currentPrice = parseFloat(Math.round(($priceSpan.html() * 100) / 100));
            priceToAdd = parseFloat(Math.round(($checkedInBag.parent().attr(DATA_PRICE_ATTR) * 100) / 100));
            $priceSpan.html((currentPrice + priceToAdd).toFixed(DECIMAL_PLACES));

            currentWeight = parseInt($weightSpan.html(), 10);
            $weightSpan.html(currentWeight + parseInt($checkedInBag.val(), 10));

            $bagsNumber.html(numberOfBags + 1);
            $bagsNumberHidden.val(numberOfBags + 1);
        }
    }

    function removeCheckedInBag($checkedInBag, $priceSpan, $weightSpan, $bagsNumber, $bagsNumberHidden) {
        var numberOfBags,
            currentPrice,
            priceToAdd,
            currentWeight;

        if (!$checkedInBag) {
            throw new Error('There is no selected checked-in bag!');
        }
            
        numberOfBags = parseInt($bagsNumber.html(), 10);

        if (numberOfBags > MIN_CHECKED_IN_BAGGAGES) {
            currentPrice = parseFloat(Math.round(($priceSpan.html() * 100) / 100));
            priceToAdd = parseFloat(Math.round(($checkedInBag.parent().attr(DATA_PRICE_ATTR) * 100) / 100));
            $priceSpan.html((currentPrice - priceToAdd).toFixed(DECIMAL_PLACES));

            currentWeight = parseInt($weightSpan.html(), 10);
            $weightSpan.html(currentWeight - parseInt($checkedInBag.val(), 10));

            $bagsNumber.html(numberOfBags - 1);
            $bagsNumberHidden.val(numberOfBags - 1);
        }
    }

    // Manage continue booking button and span helper.
    function manageContinueBookingDivBox() {
        var OUTBOUND_FLIGHT_CHECK = "OUTBOUND",
            RETURN_FLIGHT_CHECK = "RETURN",
            CONTINUE_BOOKING = 'EVERYTHING LOOKS FINE. PLEASE CONTINUE!',
            areExtrasForOutboundFlightValid = false,
            areExtrasForReturnFlightValid = false,
            $oneWayRouteCheckedInBag = $('#OneWayRouteCheckedInBaggagePanel input[type="radio"]:checked'),
            $oneWayRouteCabinBag = $('#OneWayRouteCabinBaggagePanel input[type="radio"]:checked'),
            $oneWayRouteSelectedSeat = $('#OneWayRouteSelectedSeat'),
            $returnRouteCheckedInBag,
            $returnRouteCabinBag,
            $returnRouteSelectedSeat;

        areExtrasForOutboundFlightValid = areBagsAndSeatSelected($oneWayRouteCheckedInBag,
            $oneWayRouteCabinBag, $oneWayRouteSelectedSeat, OUTBOUND_FLIGHT_CHECK);

        if (!areExtrasForOutboundFlightValid) {
            return;
        }

        if ($('#ReturnRouteExtrasPanel').is(':visible')) {
            $returnRouteCheckedInBag = $('#ReturnRouteCheckedInBaggagePanel input[type="radio"]:checked');
            $returnRouteCabinBag = $('#ReturnRouteCabinBaggagePanel input[type="radio"]:checked'),
            $returnRouteSelectedSeat = $('#ReturnRouteSelectedSeat'),

            areExtrasForReturnFlightValid = areBagsAndSeatSelected($returnRouteCheckedInBag,
                $returnRouteCabinBag, $returnRouteSelectedSeat, RETURN_FLIGHT_CHECK);

            if (!areExtrasForReturnFlightValid) {
                return;
            }
        }

        setContinueBookingDivBox(CONTINUE_BOOKING, false);
    }

    function areBagsAndSeatSelected($checkedInBag, $cabinBag, $selectedSeatSpan, typeOfFlight) {
        var CHECKED_IN_BAG_NOT_SELECTED = 'PLEASE SELECT A CHECKED-IN BAGGAGE FOR ' + typeOfFlight + ' FLIGHT!',
            CABIN_BAG_NOT_SELECTED = 'PLEASE SELECT A CABIN BAGGAGE FOR ' + typeOfFlight + ' FLIGHT!',
            SEAT_NOT_SELECTED = 'PLEASE SELECT A SEAT FOR ' + typeOfFlight + ' FLIGHT!';

        if (!$checkedInBag.prop('checked')) {
            setContinueBookingDivBox(CHECKED_IN_BAG_NOT_SELECTED, true);
        }
        else if (!$cabinBag.prop('checked')) {
            setContinueBookingDivBox(CABIN_BAG_NOT_SELECTED, true);
        }
        else if (!$selectedSeatSpan.is('span') || $selectedSeatSpan.html() === '') {
            // The check if "$selectedSeatSpan is span" is required in case if the span is hidden
            // from code behind (in this scenario, $selectedSeatSpan will select "document", not "span"). 
            setContinueBookingDivBox(SEAT_NOT_SELECTED, true);
        }
        else {
            return true;
        }

        return false;
    }

    function setContinueBookingDivBox(helperText, isButtonDisabled) {
        $('#BookingHelperSpan').html(helperText);
        $('#ContinueBookingBtn').prop('disabled', isButtonDisabled);
    }
});