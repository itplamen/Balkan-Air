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
});

$(document).ready(function () {
    $('.travelClassPriceSpan input[type="radio"].noMoreSeats').attr('disabled', true);

    $('.travelClassPriceSpan input[type="radio"]').click(function (event) {
        $('#SelectedTravelClassIdHiddenField').val($(event.target).val());
        $('.travelClassPriceSpan').css('background-color', 'initial');
        $(event.target).closest('.travelClassPriceSpan').css('background-color', 'pink');
    });

    var dddd = $('.flightDatesDiv');

    $('.flightDatesDiv').click(function () {
        var currentFlightDate = $("div.flightDatesDiv.slick-current")[0];
        
        $('#CurrentFlightInfoIdHiddenField').val($(currentFlightDate).attr('data-value'));
    });
});


