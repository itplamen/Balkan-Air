$(function () {
    'use strict';

    var $seatsDiv = $('#SeatsDiv'),
        $selectedRowHiddenField = $('#SelectedRowHiddenField'),
        $selectedSeatHiddenField = $('#SelectedSeatHiddenField'),
        $rowsDiv = $('<div id="RowsDiv" />'),
        $row = $('<p class="rowSeat" />'),
        documentFragment = document.createDocumentFragment(),
        numberOfAirplaneRows = 30;

    for (var i = 1; i <= numberOfAirplaneRows; i++) {
        $row.html(i);
        documentFragment.appendChild($row[0].cloneNode(true));
    }

    $(document).ready(function () {
        $('#SeatsDiv .reservedSeatLabel, #SeatsDiv .unavailableSeatLabel').prev().attr('disabled', true);

        $seatsDiv.append($('<hr class="upperHr" />'));
        $seatsDiv.append($('<hr class="firstClassHr" />'));
        $seatsDiv.append($('<hr class="businessClassHr" />'));
        $seatsDiv.append($('<hr class="economyClassHr" />'));
        $seatsDiv.append($('<span class="infoSpan firstClassInfoSpan">First class<span>First row seats<br/>Extra legroom<br/>Window seats</span></span>'));
        $seatsDiv.append($('<span class="infoSpan businessClassInfoSpan">Business class<span>Extra legroom<br/>Window seats</span></span>'));
        $seatsDiv.append($('<span class="infoSpan economyClassInfoSpan">Economy class<span>Window seats</span></span>'));
    });

    $rowsDiv.append(documentFragment);
    $seatsDiv.append($rowsDiv);

    if ($selectedRowHiddenField.val() !== '' || $selectedSeatHiddenField.val() !== '') {
        var radioBtnId = $selectedRowHiddenField.val() + $selectedSeatHiddenField.val();
        $('#' + radioBtnId).prop('checked', true);
    }

    $('#SeatsDiv input[type="radio"]').click(function (event) {
        $('#SelectedRowAndSeatLabel').text('Seat: ' + $(event.target).val() + $(event.target).attr('data-value'));
        $selectedRowHiddenField.val($(event.target).val());
        $selectedSeatHiddenField.val($(event.target).attr('data-value'));
    });
});