(function () {
    "use strict";

    var common = angular.module("common.services", ["ngResource"]);
    common.constant("appSettings", { serverPath: "http://localhost:52965/" });

    common.factory("currentUser", currentUser);

    function currentUser() {
        var profile = {
            isLoggedIn: false,
            username: "",
            token: ""
        };

        var setProfile = function (username, token) {
            profile.username = username;
            profile.token = token;
            profile.isLoggedIn = true;
        };

        var getProfile = function () {
            return profile;
        };

        return {
            setProfile: setProfile,
            getProfile: getProfile
        };
    }

}());