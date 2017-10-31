(function() {
    var app = angular.module("productManagement");
    app.factory('persistenceSerivce', ['$cookies', cookieService]);

    function cookieService($cookies) {
        var userName = "";
        return {
            setCookieData: function(username) {
                userName = username;
                $cookies.put("token", username);
            },
            getCookieData: function() {
                userName = $cookies.get("token");
                return userName;
            },
            clearCookieData: function() {
                userName = "";
                $cookies.remove("token");
            }
        };

    }

}());