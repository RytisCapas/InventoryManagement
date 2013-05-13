/*global jQuery: true, alert: true, ondpgAdmin */
/*jslint browser: true, white: true, plusplus: true */
Shared = (function ($) {
    'use strict';

    var shared = {};

    $(function () {
        shared.close();
    });

    shared.close = function() {
        $('.close').on('click', function () {
            $('.msg').hide();
        });
    };

    return shared;
}(jQuery));