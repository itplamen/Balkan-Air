$(function () {
    'use strict';

    // Click events can't fire, because of the UpdatePanel. The solution is to use Sys.WebForms.PageRequestManage.
    var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();

    // Re-bind jQuery events.
    pageRequestManager.add_endRequest(function () {
        $('#DepartureFancyTextBox').click(showDepartureAirports);
        $('#DestinationFancyTextBox').click(showDestinationAirports);
    });
});

$(document).ready(function () {
    var BORDER_COLOR_INITIAL = 'rgb(224, 224, 224)',
        BORDER_COLOR_ON_CLICK = 'rgb(197, 2, 124)',
        $departureDateTextBox = $('#DepartureDateTextBox'),
        $arrivalDateTextBox = $('#ArrivalDateTextBox');

    $('.homeSlider').slick({
        arrows: false,
        dots: true,
        fade: true,
        infinite: true,
        focusOnSelect: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        speed: 2000,
    });

    // Bind jQuery events initially.
    $('#DepartureFancyTextBox').click(showDepartureAirports);
    $('#DestinationFancyTextBox').click(showDestinationAirports);

    $departureDateTextBox
        .add($arrivalDateTextBox)
        .click(function () {
            $(this).parent().css('border-color', BORDER_COLOR_ON_CLICK);
        });

    $(document).mouseup(function (e) {
        var $departureAirportsDiv = $("#DepartureAirportsDiv"),
            $destinationAirportsDiv = $('#DestinationAirportsDiv');

        // If the target of the click isn't the container nor a descendant of the container.
        if ($departureAirportsDiv.is(':visible') && !$departureAirportsDiv.is(e.target) ||
            $departureAirportsDiv.has(e.target).length !== 0) {

            $departureAirportsDiv.hide();
            $('#DepartureFancyTextBox').css('border-color', BORDER_COLOR_INITIAL);
        }

        // If the target of the click isn't the container nor a descendant of the container.
        if ($destinationAirportsDiv.is(':visible') && !$destinationAirportsDiv.is(e.target) ||
            $destinationAirportsDiv.has(e.target).length !== 0) {

            $destinationAirportsDiv.hide();
            $('#DestinationFancyTextBox').css('border-color', BORDER_COLOR_INITIAL);
        }

        if ($departureDateTextBox.parent().css('border-color') == BORDER_COLOR_ON_CLICK) {
            $departureDateTextBox.parent().css('border-color', BORDER_COLOR_INITIAL);
        }

        if ($arrivalDateTextBox.parent().css('border-color') == BORDER_COLOR_ON_CLICK) {
            $arrivalDateTextBox.parent().css('border-color', BORDER_COLOR_INITIAL);
        }
    });

    $('#SearchBtn').click(areAllFieldsFilled);

    
});

function showDepartureAirports() {
    $(this).css('border-color', 'rgb(197, 2, 124)');
    $('#DepartureAirportsDiv').show();
}

function showDestinationAirports() {
    $(this).css('border-color', 'rgb(197, 2, 124)');
    $('#DestinationAirportsDiv').show();
}

function areAllFieldsFilled() {
    var $departureAirportTextBox = $('#DepartureAirportTextBox'),
        $destinationAirportTextBox = $('#DestinationAirportTextBox'),
        $departureDateTextBox = $('#DepartureDateTextBox'),
        $arrivalDateTextBox = $('#ArrivalDateTextBox');

    if ($departureAirportTextBox.val() !== '' && $destinationAirportTextBox.val() !== '' &&
        $departureDateTextBox.val() !== '' ) {

        $departureAirportTextBox.parent().css('border-color', '#E0E0E0');
        $destinationAirportTextBox.parent().css('border-color', '#E0E0E0');
        $departureDateTextBox.parent().css('border-color', '#E0E0E0');

        if ($arrivalDateTextBox.is('input:text') && $arrivalDateTextBox.val() === '') {
            $arrivalDateTextBox.parent().css('border-color', 'red');
            return false;
        }
        else {
            $arrivalDateTextBox.parent().css('border-color', '#E0E0E0');
        }
        
        return true;
    }
    else if ($departureAirportTextBox.val() === '') {
        $departureAirportTextBox.parent().css('border-color', 'red');
    }
    else if ($destinationAirportTextBox.val() === '') {
        $destinationAirportTextBox.parent().css('border-color', 'red');
    }
    else if ($departureDateTextBox.val() === '') {
        $departureDateTextBox.parent().css('border-color', 'red');
    }
    else {
        throw new Error('Invalid field ID!')
    }

    return false;
}