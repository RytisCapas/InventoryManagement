/*global jQuery: true, alert: true, ondpgAdmin */
/*jslint browser: true, white: true, plusplus: true */
Item = (function ($) {
    'use strict';

    var item = {};

    $(function () {
        $(function () {
            $('.delete-item').on('click', function () {
                $.post(this.href, function (resp) {
                    if (resp.Success) {
                        location.reload();
                    } else {
                        if (typeof (resp.Content) != "undefined" && resp.Content.length > 0) {
                            $('.table').prepend(resp.Content);
                            Shared.close();
                        }
                    }
                });
                return false;
            });
        });
    });

    return item;
}(jQuery));
