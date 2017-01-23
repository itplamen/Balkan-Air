'use strict';

function pageLoad(sender, args) {
    $('#DepartureFancyTextBox').on('click', function () {
        $(this).css('border', '3px solid #C5027C');
        $('#DepartureAirportsDiv').show();
    });

    $('#DestinationFancyTextBox').on('click', function () {
        $(this).css('border', '3px solid #C5027C');
        $('#DestinationAirportsDiv').show();
    });
}

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