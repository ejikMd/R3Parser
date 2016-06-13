R3App.controller("AccessLogsController", function ($scope, accessLogsService) {
    //$scope.message = "";
    //$scope.left = function () { return 100 - $scope.message.length; };
    //$scope.clear = function () { $scope.message = ""; };
    //$scope.save = function () { alert("Note Saved"); };

    $scope.remove = function (index) {
        $scope.accessLogs.splice(index, 1);
    }



    function getAccessLogs() {

        document.getElementById("loading").style.display = 'block';

        accessLogsService.getAccessLogs()
            .success(function (accessLogs) {
                document.getElementById("loading").style.display = 'none';
                $scope.accessLogs = accessLogs;
                console.log($scope.accessLogs);
            })
            .error(function (error) {
                document.getElementById("loading").style.display = 'none';
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    getAccessLogs();
});


R3App.factory('accessLogsService', ['$http', function ($http) {

    var accessLogsService = {};
    accessLogsService.getAccessLogs = function () {
        return $http.get('/AccessLogs/GetAllAccessLogs');
    };
    return accessLogsService;

}]);

