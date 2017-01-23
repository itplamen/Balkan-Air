$(function () {
    'use strict';

    var MIN_CHECKED_IN_BAGGAGES = 1,
        MAX_CHECKED_IN_BAGGAGES = 3,
        $price = $('#Price'),
        $priceSpan = $('#PriceSpan'),
        $weightSpan = $('#WeightSpan'),
        $numericBag = $('#NumericBag'),
        $numberOfBaggages = $('#NumberOfBaggages'),
        $noneCheckedInBag = $('#NoneCheckedInBag'),
        $23KgCheckedInBag = $('#23KgCheckedInBag'),
        $32KgCheckedInBag = $('#32KgCheckedInBag'),
        $removeBagBtn = $('#RemoveBagBtn'),
        $addBagBtn = $('#AddBagBtn'),
        $checkedBagRadioBtn;

    $numericBag.hide();
    $price.hide();

    $('#CheckedInBaggageDiv').on('click', '.checkedInBag', function () {
        $('#SelectedCheckedInBagPrice').val($(this).attr('data-price'));
        $('#SelectedCheckedInBagWeight').val($(this).val());
    });

    $('#CabinBaggageDiv').on('click', '.cabinBag', function () {
        $('#SelectedCabinBagPrice').val($(this).attr('data-price'));
        $('#SelectedCabinBagSize').val($(this).val());
    });

    $noneCheckedInBag.click(function () {
        $numericBag.hide();
        $price.hide();
        $weightSpan.html(0);
        $numberOfBaggages.html(0);
        $('#NumberOfCheckedInBags').val(0);
    });

    $23KgCheckedInBag
        .add($32KgCheckedInBag)
        .click(function () {
            $numericBag.show();
            $price.show();
            $checkedBagRadioBtn = $('#CheckedInBaggageDiv input[type="radio"]:checked');
            $priceSpan.html(parseInt($checkedBagRadioBtn.attr('data-price')));
            $weightSpan.html(parseInt($checkedBagRadioBtn.val()));
            $numberOfBaggages.html(1);
            $('#NumberOfCheckedInBags').val($numberOfBaggages.html());
        });

    $removeBagBtn.click(function () {
        var currentWeight,
            currentPrice;

        if ($numberOfBaggages.html() > MIN_CHECKED_IN_BAGGAGES) {
            currentPrice = parseInt($priceSpan.html());
            $priceSpan.html(currentPrice - parseInt($checkedBagRadioBtn.attr('data-price')));
            currentWeight = parseInt($weightSpan.html());
            $weightSpan.html(currentWeight - parseInt($checkedBagRadioBtn.val()));
            $numberOfBaggages.html(parseInt($numberOfBaggages.html()) - 1);
            $('#NumberOfCheckedInBags').val($numberOfBaggages.html());
        }
    });

    $addBagBtn.click(function () {
        var currentWeight,
            currentPrice;

        if ($numberOfBaggages.html() < MAX_CHECKED_IN_BAGGAGES) {
            currentPrice = parseInt($priceSpan.html());
            $priceSpan.html(currentPrice + parseInt($checkedBagRadioBtn.attr('data-price')));
            currentWeight = parseInt($weightSpan.html());
            $weightSpan.html(currentWeight + parseInt($checkedBagRadioBtn.val()));
            $numberOfBaggages.html(parseInt($numberOfBaggages.html()) + 1);
            $('#NumberOfCheckedInBags').val($numberOfBaggages.html());
        }
    });
});