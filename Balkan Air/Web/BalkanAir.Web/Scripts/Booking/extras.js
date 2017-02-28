$(function () {
    'use strict';

    var MIN_CHECKED_IN_BAGGAGES = 1,
        MAX_CHECKED_IN_BAGGAGES = 3,
        $oneWayRoutePrice = $('#OneWayRoutePrice'),
        $oneWayRoutePriceSpan = $('#OneWayRoutePriceSpan'),
        $oneWayRouteWeightSpan = $('#OneWayRouteWeightSpan'),
        $oneWayRouteNumericBag = $('#OneWayRouteNumericBag'),
        $oneWayRouteNumberOfBaggages = $('#OneWayRouteNumberOfBaggages'),
        $oneWayRouteNoneCheckedInBag = $('#OneWayRouteNoneCheckedInBag'),
        $oneWayRoute23KgCheckedInBag = $('#OneWayRoute23KgCheckedInBag'),
        $oneWayRoute32KgCheckedInBag = $('#OneWayRoute32KgCheckedInBag'),
        $oneWayRouteRemoveBagBtn = $('#OneWayRouteRemoveBagBtn'),
        $oneWayRouteAddBagBtn = $('#OneWayRouteAddBagBtn'),
        $oneWayRouteCheckedInBagPrice = $('#OneWayRouteSelectedCheckedInBagPriceHiddenField'),
        $oneWayRouteCheckedInBagWeight = $('#OneWayRouteSelectedCheckedInBagWeightHiddenField'),
        $oneWayRouteCabinBagPrice = $('#OneWayRouteSelectedCabinBagPriceHiddenField'),
        $oneWayRouteCabinBagSize = $('#OneWayRouteSelectedCabinBagSizeHiddenField'),
        $oneWayRouteNumberOfCheckedInBags = $('#OneWayRouteNumberOfCheckedInBagsHiddenField'),
        $oneWayRouteCheckedBagRadioBtn,
        $returnRouteExtrasPanel = $('#ReturnRouteExtrasPanel'),
        $returnRoutePrice = $('#ReturnRoutePrice'),
        $returnRoutePriceSpan = $('#ReturnRoutePriceSpan'),
        $returnRouteWeightSpan = $('#ReturnRouteWeightSpan'),
        $returnRouteNumericBag = $('#ReturnRouteNumericBag'),
        $returnRouteNumberOfBaggages = $('#ReturnRouteNumberOfBaggages'),
        $returnRouteNoneCheckedInBag = $('#ReturnRouteNoneCheckedInBag'),
        $returnRoute23KgCheckedInBag = $('#ReturnRoute23KgCheckedInBag'),
        $returnRoute32KgCheckedInBag = $('#ReturnRoute32KgCheckedInBag'),
        $returnRouteRemoveBagBtn = $('#ReturnRouteRemoveBagBtn'),
        $returnRouteAddBagBtn = $('#ReturnRouteAddBagBtn'),
        $returnRouteCheckedInBagPrice = $('#ReturnRouteSelectedCheckedInBagPriceHiddenField'),
        $returnRouteCheckedInBagWeight = $('#ReturnRouteSelectedCheckedInBagWeightHiddenField'),
        $returnRouteCabinBagPrice = $('#ReturnRouteSelectedCabinBagPriceHiddenField'),
        $returnRouteCabinBagSize = $('#ReturnRouteSelectedCabinBagSizeHiddenField'),
        $returnRouteNumberOfCheckedInBags = $('#ReturnRouteNumberOfCheckedInBagsHiddenField'),
        $returnRouteCheckedBagRadioBtn;

    hideNumericBagAndPrice();

    $('#OneWayRouteCheckedInBaggageDiv').on('click', '.checkedInBag', function () {
            setPriceAndWeightForCheckedInBag($oneWayRouteCheckedInBagPrice, $oneWayRouteCheckedInBagWeight);
        });

    $('#ReturnRouteCheckedInBaggageDiv').on('click', '.checkedInBag', function () {
            setPriceAndWeightForCheckedInBag($returnRouteCheckedInBagPrice, $returnRouteCheckedInBagWeight);
        });
        
    $('#OneWayRouteCabinBaggageDiv').on('click', '.cabinBag', function () {
            setPriceAndWeightForCabinBag($oneWayRouteCabinBagPrice, $oneWayRouteCabinBagSize)
        });

    $('#ReturnRouteCabinBaggageDiv').on('click', '.cabinBag', function () {
            setPriceAndWeightForCabinBag($returnRouteCabinBagPrice, $returnRouteCabinBagSize);
        });

    $oneWayRouteNoneCheckedInBag.click(function () {
        selectNoneCheckedInBag($oneWayRouteNumericBag, $oneWayRoutePrice, $oneWayRouteWeightSpan,
            $oneWayRouteNumberOfBaggages, $oneWayRouteNumberOfCheckedInBags)
    });

    $returnRouteNoneCheckedInBag.click(function () {
        selectNoneCheckedInBag($returnRouteNumericBag, $returnRoutePrice, $returnRouteWeightSpan,
            $returnRouteNumberOfBaggages, $returnRouteNumberOfCheckedInBags)
    });

    $oneWayRoute23KgCheckedInBag
        .add($oneWayRoute32KgCheckedInBag)
        .click(function () {
            $oneWayRouteCheckedBagRadioBtn = $('#OneWayRouteCheckedInBaggageDiv input[type="radio"]:checked');

            selectCheckedInBag($oneWayRouteNumericBag, $oneWayRoutePrice, $oneWayRoutePriceSpan, $oneWayRouteCheckedBagRadioBtn,
                $oneWayRouteWeightSpan, $oneWayRouteNumberOfBaggages, $oneWayRouteNumberOfCheckedInBags);
        });

    $returnRoute23KgCheckedInBag
        .add($returnRoute32KgCheckedInBag)
        .click(function () {
            $returnRouteCheckedBagRadioBtn = $('#ReturnRouteCheckedInBaggageDiv input[type="radio"]:checked');

            selectCheckedInBag($returnRouteNumericBag, $returnRoutePrice, $returnRoutePriceSpan, $returnRouteCheckedBagRadioBtn,
                $returnRouteWeightSpan, $returnRouteNumberOfBaggages, $returnRouteNumberOfCheckedInBags);
        });
    
    $oneWayRouteRemoveBagBtn.click(function () {
        removeBag($oneWayRouteNumberOfBaggages, $oneWayRoutePriceSpan, $oneWayRouteCheckedBagRadioBtn,
            $oneWayRouteWeightSpan, $oneWayRouteNumberOfCheckedInBags)
    });

    $returnRouteRemoveBagBtn.click(function () {
        removeBag($returnRouteNumberOfBaggages, $returnRoutePriceSpan, $returnRouteCheckedBagRadioBtn,
            $returnRouteWeightSpan, $returnRouteNumberOfCheckedInBags)
    });

    $oneWayRouteAddBagBtn.click(function () {
        addBag($oneWayRouteNumberOfBaggages, $oneWayRoutePriceSpan, $oneWayRouteCheckedBagRadioBtn,
            $oneWayRouteWeightSpan, $oneWayRouteNumberOfCheckedInBags)
    });

    $returnRouteAddBagBtn.click(function () {
        addBag($returnRouteNumberOfBaggages, $returnRoutePriceSpan, $returnRouteCheckedBagRadioBtn,
           $returnRouteWeightSpan, $returnRouteNumberOfCheckedInBags)
    });

    function hideNumericBagAndPrice() {
        $oneWayRouteNumericBag.hide();
        $oneWayRoutePrice.hide();

        if ($returnRouteExtrasPanel.is(':visible')) {
            $returnRouteNumericBag.hide();
            $returnRoutePrice.hide();
        }
    }

    function setPriceAndWeightForCheckedInBag($bagPriceHiddenField, $bagWeightHiddenField) {
        $bagPriceHiddenField.val($(this).attr('data-price'));
        $bagWeightHiddenField.val($(this).val());
    }

    function setPriceAndWeightForCabinBag($cabinBagPriceHiddenField, $cabinBagSizeHiddenField) {
        $cabinBagPriceHiddenField.val($(this).attr('data-price'));
        $cabinBagSizeHiddenField.val($(this).val());
    }
    
    function selectNoneCheckedInBag($numericBag, $price, $weight, $numberOfBaggages, $numberOfCheckedInBags) {
        $numericBag.hide();
        $price.hide();
        $weight.html(0);
        $numberOfBaggages.html(0);
        $numberOfCheckedInBags.val(0);
    }

    function selectCheckedInBag($numericBag, $price, $priceSpan, $checkedRadioBtn, $weightSpan,
            $numberOfBaggages, $numberOfCheckedInBags) {

        $numericBag.show();
        $price.show();
        $priceSpan.html(parseInt($checkedRadioBtn.attr('data-price')));
        $weightSpan.html(parseInt($checkedRadioBtn.val()));
        $numberOfBaggages.html(1);
        $numberOfCheckedInBags.val($numberOfBaggages.html());
    }

    function removeBag($numberOfBaggages, $priceSpan, $checkedRadioBtn, $weightSpan, $numberOfCheckedInBags) {
        var currentWeight,
            currentPrice;

        if ($checkedRadioBtn && $numberOfBaggages.html() > MIN_CHECKED_IN_BAGGAGES) {
            currentPrice = parseInt($priceSpan.html());
            $priceSpan.html(currentPrice - parseInt($checkedRadioBtn.attr('data-price')));
            currentWeight = parseInt($weightSpan.html());
            $weightSpan.html(currentWeight - parseInt($checkedRadioBtn.val()));
            $numberOfBaggages.html(parseInt($numberOfBaggages.html()) - 1);
            $numberOfCheckedInBags.val($numberOfBaggages.html());
        }
    }

    function addBag($numberOfBaggages, $priceSpan, $checkedRadioBtn, $weightSpan, $numberOfCheckedInBags) {
        var currentWeight,
            currentPrice;

        if ($checkedRadioBtn && $numberOfBaggages.html() < MAX_CHECKED_IN_BAGGAGES) {
            currentPrice = parseInt($priceSpan.html());
            $priceSpan.html(currentPrice + parseInt($checkedRadioBtn.attr('data-price')));
            currentWeight = parseInt($weightSpan.html());
            $weightSpan.html(currentWeight + parseInt($checkedRadioBtn.val()));
            $numberOfBaggages.html(parseInt($numberOfBaggages.html()) + 1);
            $numberOfCheckedInBags.val($numberOfBaggages.html());
        }
    }
});