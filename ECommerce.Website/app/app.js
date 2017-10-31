(function () {
    "use strict";

    var app = angular.module("productManagement",
    ["common.services", "ui.router", "ngCookies","ngTable"]);

    //state -- > view --> url/templateurl/controller
    //call --> viewnae@statename

    app.config(["$stateProvider",
            "$urlRouterProvider","$httpProvider",
            function ($stateProvider, $urlRouterProvider, $httpProvider) {

                // $httpProvider.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
               // $httpProvider.defaults.headers.common['Access-Control-Allow-Methods'] = "GET, POST, PUT, DELETE, OPTIONS";
               // $httpProvider.defaults.headers.common['Access-Control-Allow-Headers'] = "Authorization";

                $urlRouterProvider.otherwise("/");

                $stateProvider
                     .state("home", {
                         // welcome
                         url: "/",
                         templateUrl: "app/views/welcomeView.html"
                     })
                    .state("login", {
                        // login
                        url: "/login",
                        templateUrl: "app/views/loginView.html",
                        controller: "MainCtrl as vm",
                    })
                    //.state("logout", {
                    //    // logut
                    //    url: "/logout"
                    //})
                     .state("register", {
                         // login
                         url: "/register",
                         templateUrl: "app/views/registerView.html",
                         controller: "MainCtrl as vm",
                     })
                    .state("productList", {
                        // Product List
                        url: "/products",
                        templateUrl: "app/views/productListView.html",
                        controller: "ProductListCtrl as vm"
                    })
                .state("productEdit", {
                    // Product edit
                    url: "/products/edit/:productId",
                    templateUrl: "app/views/productEditView.html",
                    controller: "ProductEditCtrl as vm",
                    resolve: {
                        productResource: "productResource",
                        product: ["productResource", "$stateParams",function (productResource, $stateParams) {
                            var productId = $stateParams.productId;
                            return productResource.get({ id: productId }, function (data) {
                                return data;
                            });
                        }]
                    }

                })
                .state("productDetail", {
                    // Product Details
                    url: "/products/:productId",
                    templateUrl: "app/views/productDetailView.html",
                    controller: "ProductDetailCtrl as vm",
                    resolve: {
                        productResource: "productResource",
                        product: ["productResource", "$stateParams", function (productResource, $stateParams) {
                            var productId = $stateParams.productId;
                            return productResource.get({ id: productId }, function (data) {
                                return data;
                            });
                        }]
                    }
                });

            }]
    );

    app.run(['$rootScope', 'currentUser', 'persistenceSerivce', '$stateParams', '$state', '$templateCache',
        function ($rootScope, currentUser, persistenceSerivce, $stateParams, $state, $templateCache) {

        $rootScope.logout = function() {
            persistenceSerivce.clearCookieData();
            currentUser.setProfile(currentUser.getProfile().username, '');
            $state.go('login');
        };

        $rootScope.isLoggedIn = function () {
           return currentUser.getProfile().isLoggedIn;
        };

        //$rootScope.$on('$viewContentLoaded', function () {
        //    $templateCache.removeAll();
        //});

        $rootScope.$on('$locationChangeStart', function (event) {

            if ($state.current.name == "register") return;
            var token = persistenceSerivce.getCookieData();
            if (token == null || token == '' || token == undefined) {
                currentUser.token = '';
                $state.go('login');
            } else {
                currentUser.setProfile(currentUser.getProfile().username, token);
            }

            if (!currentUser.getProfile().isLoggedIn) {
                event.preventDefault();
                $state.go("login");
            }
           
         //   console.log('user token: ' + currentUser.getProfile().token);
          //  console.log('cookie token: ' + persistenceSerivce.getCookieData());

        });
    }]);

}());