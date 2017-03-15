$(function () {
    'use strict';

    var $fancyTextBox = $('.fancyTextBox');

    $fancyTextBox.click(function () {
        $(this).css('border', '3px solid #C5027C');
        $(this).children().not('label').focus();
    });

    $fancyTextBox.children().not('label').blur(function () {
        $(this).parent().css('border', '3px solid #E0E0E0');
        $(this).children().not('label').blur();
    });
});