/*global jQuery: true, alert: true, ondpgAdmin */
/*jslint browser: true, white: true, plusplus: true */
User = (function($) {
    'use strict';

    var user = {};

    $(function() {
        $(function () {
            $('.delete-user').on('click', function () {
                $.post(this.href, function(resp) {
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

    return user;
}(jQuery));
