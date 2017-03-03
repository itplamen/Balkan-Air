$(function () {
    'use strict';

    var MIN_CHECKED_IN_BAGGAGES = 1,
        MAX_CHECKED_IN_BAGGAGES = 3,
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

    $('#OneWayRouteAddBagBtn')
        .click(function () {
            addCheckedInBag($oneWayRouteSelectedCheckedInBag, $oneWayRoutePriceSpan,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);
        });

    $('#ReturnRouteAddBagBtn')
       .click(function () {
           addCheckedInBag($returnRouteSelectedCheckedInBag, $returnRoutePriceSpan,
                $returnRouteWeightSpan, $returnRouteNumberOfBaggage, $returnRouteBagsHidden);
       });

    $('#OneWayRouteRemoveBagBtn')
        .click(function () {
            removeCheckedInBag($oneWayRouteSelectedCheckedInBag, $oneWayRoutePriceSpan,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggage, $oneWayRouteBagsHidden);
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
            checkedInBagPrice = parseFloat(Math.round($checkedInBag.attr('data-price') * 100) / 100),
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
        
        $priceSpan.html(checkedInBagPrice.toFixed(2));
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
            priceToAdd = parseFloat(Math.round(($checkedInBag.attr('data-price') * 100) / 100));
            $priceSpan.html((currentPrice + priceToAdd).toFixed(2));

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
            priceToAdd = parseFloat(Math.round(($checkedInBag.attr('data-price') * 100) / 100));
            $priceSpan.html((currentPrice - priceToAdd).toFixed(2));

            currentWeight = parseInt($weightSpan.html(), 10);
            $weightSpan.html(currentWeight - parseInt($checkedInBag.val(), 10));

            $bagsNumber.html(numberOfBags - 1);
            $bagsNumberHidden.val(numberOfBags - 1);
        }
    }
});