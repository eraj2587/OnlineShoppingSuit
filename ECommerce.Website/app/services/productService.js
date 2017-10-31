(function () {
    "use strict";

    angular.module("common.services")
        .factory("productResource",
        ["$resource", "appSettings", "currentUser", productResource]);

    function productResource($resource, appSettings, currentUser) {
        return $resource(appSettings.serverPath + "/api/product/:id", null, {
            'get': {
                method: 'GET',
                headers: {
                    'Authorization': function () {
                        return 'Bearer ' + currentUser.getProfile().token;
                    }
                }
            },
            'save': {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + currentUser.getProfile().token
                    // ,'X-XSRF-Token':
                    //angular.element('input[name="__RequestVerificationToken"]').attr('value')
                },
                isArray: false
            },
            'update': {
                method: 'PUT',

                headers: {
                    'Authorization': 'Bearer ' + currentUser.getProfile().token
                    //   ,'X-XSRF-Token':
                    //angular.element('input[name="__RequestVerificationToken"]').attr('value')
                }
            }
        });
    }
}());