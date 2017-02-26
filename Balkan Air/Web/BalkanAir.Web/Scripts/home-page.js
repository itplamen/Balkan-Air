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

    $(document).mouseup(function (e) {
        var $departureAirportsDiv = $("#DepartureAirportsDiv"),
            $destinationAirportsDiv = $('#DestinationAirportsDiv');

        // If the target of the click isn't the container nor a descendant of the container.
        if ($departureAirportsDiv.is(':visible') && !$departureAirportsDiv.is(e.target) ||
            $departureAirportsDiv.has(e.target).length !== 0) {

            $departureAirportsDiv.hide();
            $('#DepartureFancyTextBox').css('border', '3px solid #E0E0E0');
        }

        // If the target of the click isn't the container nor a descendant of the container.
        if ($destinationAirportsDiv.is(':visible') && !$destinationAirportsDiv.is(e.target) ||
            $destinationAirportsDiv.has(e.target).length !== 0) {

            $destinationAirportsDiv.hide();
            $('#DestinationFancyTextBox').css('border', '3px solid #E0E0E0');
        }
    });
});

function showDepartureAirports() {
    $(this).css('border', '3px solid #C5027C');
    $('#DepartureAirportsDiv').show();
}

function showDestinationAirports() {
    $(this).css('border', '3px solid #C5027C');
    $('#DestinationAirportsDiv').show();
}