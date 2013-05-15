/*global jQuery: true, alert: true, Shared */
/*jslint browser: true, white: true, plusplus: true */
Shared = (function ($) {
    'use strict';

    var shared = {};

    $(function () {
        shared.close();
        shared.initializeDatePicker();
    });

    shared.close = function() {
        $('.close').on('click', function () {
            $('.msg').hide();
        });
    };

    shared.initializeDatePicker = function() {
        $('.datepicker').datepicker($.datepicker.regional["lt"]);
    };

    return shared;
}(jQuery));