R3App.controller("SoldRealEstatesController", function ($scope, soldListingService) {

    $scope.listOfSoldRealEstates = [];

    function getAllRecords() {

        document.getElementById("loading").style.display = 'block';

        soldListingService.getAllSoldRecords()
            .success(function (listOfSoldRealEstates) {
                document.getElementById("loading").style.display = 'none';
                $scope.listOfSoldRealEstates = listOfSoldRealEstates;
                //console.log($scope.listOfSoldRealEstates);
            })
            .error(function (error) {
                document.getElementById("loading").style.display = 'none';
                $scope.status = 'Unable to load customer data: ' + error.message;
                //console.log($scope.status);
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
