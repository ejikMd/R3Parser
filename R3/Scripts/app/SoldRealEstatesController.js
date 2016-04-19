R3App.controller("SoldRealEstatesController", function ($scope, soldListingService) {

    $scope.listOfSoldRealEstates = [];

    function getAllRecords() {

        soldListingService.getAllSoldRecords()
            .success(function (listOfSoldRealEstates) {
                $scope.listOfSoldRealEstates = listOfSoldRealEstates;
                console.log($scope.listOfSoldRealEstates);
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    getAllRecords();

});


R3App.factory('soldListingService', ['$http', function ($http) {

    var soldListingService = {};

    soldListingService.getAllSoldRecords = function () {
        return $http.get('SoldInfo/GetAllSoldRecords');
    };

    return soldListingService;
}]);
