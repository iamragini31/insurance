var App = angular.module("IndexApp", []).controller("IndexCtrl", function ($scope, $http, $window, $filter) {
    $scope.Array = {};
    $scope.GetQuotes = function () {
        debugger
        $scope.Array.HomeCountry = $scope.txtHomeCountry;
        $scope.Array.TouristCountry = $scope.txttocountry;
        $scope.Array.CoverageStartDate = $scope.txtCoverageStartDate;
        $scope.Array.CoverageEndDate = $scope.txtCoverageEndDate;
        $scope.Array.Name = $scope.txtname;
        $http({
            method: "POST",
            url: "/Quotation/GETQuotation",
            //   data: { Array },
            data: JSON.stringify($scope.Array),
        }).then(function (response) {
            debugger
            //$scope.StudentRegistrationNo = null;
            if (response.data > 0) {
                //var url = '@Url.Action("Quotation","Quotation")';
                //window.location.href = url;
                window.location.href = '/Quotation/Quotation';
            }
        }, function (error) {
        });
    }
})