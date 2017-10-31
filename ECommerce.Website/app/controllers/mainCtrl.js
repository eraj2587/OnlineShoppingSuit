(function () {
    "use strict";
    angular.module("productManagement")
        .controller("MainCtrl", 
        ["$scope","userAccount","currentUser","persistenceSerivce","$stateParams", "$state", mainCtrl]);

    function mainCtrl($scope, userAccount, currentUser,persistenceSerivce, $stateParams, $state) {
        var vm = this;
        vm.isLoggedIn = function() {
            return currentUser.getProfile.isLoggedIn;
        };
        vm.message = '';
        vm.userData = {
            userName: '',
            email: '',
            password: '',
            confirmPassword: ''
        };

        vm.registerUser = function () {

            if (vm.password != vm.confirmPassword) {
                vm.message = "Password does not match";
                return;
            }
            userAccount.registration.registerUser(vm.userData,
                function (data) {
                    vm.userData.confirmPassword = '';
                    vm.message = "...Registration successful";
                    $state.go("login");
                },
                function (response) {
                    vm.message = response.statusText + "\r\n";

                    if (response.data.modelState) {
                        for (var key in response.data.modelState) {
                            vm.message += response.data.modelState[key] + "\r\n";
                        }
                    }

                    if (response.data.exceptionMessage)
                        vm.message += response.data.exceptionMessage;
                }
                );

        };
        vm.login = function () {
           
            vm.userData.grant_type = "password";
            //vm.userData.userName = vm.userData.user;
            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.password = '';
                    vm.message = "";
                    currentUser.setProfile(vm.userData.userName, data.access_token);
                    persistenceSerivce.setCookieData(data.access_token);
                    $state.go("productList");
                },
                function (response) {
                    vm.password = '';
                    if (response.statusText)
                        vm.message = response.statusText + "\r\n";

                    if (response.data.error) {
                        if (~response.data.error.indexOf("invalid_grant"))
                            vm.message = "Invalid username or password" + "\r\n";
                    }

                    if (response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                    }
                }
                );

        };

    }


}()
);