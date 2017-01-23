$(function () {
    'use strict';

    var $fancyTextBox = $('.fancyTextBox'),
    $textBox = $fancyTextBox.children('input[type="text"]');


    $fancyTextBox.click(function () {
        $(this).css('border', '3px solid #C5027C');
        $(this).children('input[type="text"]').focus();
    });

    $textBox.blur(function () {
        $(this).parent().css('border', '3px solid #E0E0E0');
        $(this).children('input[type="text"]').blur();
    });

    $('#DatepickerTextBox').datepicker({ dateFormat: "yy/mm/dd" });
});


