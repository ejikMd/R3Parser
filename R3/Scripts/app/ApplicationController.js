R3App.controller("ApplicationController", function ($scope, applicationInfo) {

    $scope.applicationStatus;

    function getApplicationStatus() {
        applicationInfo.getApplicationStatus()
            .success(function (applicationStatus) {

                applicationStatus.LastDateTaken = new Date(parseInt(applicationStatus.LastDateTaken.substr(6)));
                $scope.applicationStatus = applicationStatus;
                console.log($scope.applicationStatus);
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    getApplicationStatus();
});


R3App.service('applicationInfo', ['$http', function ($http) {

    var mainListingService = {};

    mainListingService.getApplicationStatus = function () {
        return $http.get('GetApplicationStatus');
    };

    return mainListingService;
}]);



