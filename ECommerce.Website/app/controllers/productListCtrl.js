(function () {
    "use strict";
    var app = angular
        .module("productManagement");

    app.controller("ProductListCtrl",
   ["$scope",
    "currentUser", "NgTableParams",
                 "productResource", productList]);

    function productList($scope, currentUser, NgTableParams, productResource) {
        var vm = this;
        vm.searchCriteria = "GDN";
        productResource.query(
            {
              //  $skip: 1,
              //  $top: 10,
             //   $orderby: "Price desc"
                // $filter:"ProductCode eq 'GDN-0023'"
            },
        function (data) {
            vm.products = data;
            vm.tableParams = new NgTableParams({}, { dataset: data });
        });
    }

}());
