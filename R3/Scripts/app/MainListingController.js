R3App.controller("MainListingController", function ($scope, $filter, mainListingService, communicationsService, applicationInfo) {

    $scope.listOfRealEstates = [];
    $scope.sortType = 'PriceCoefficient'; // set the default sort type
    $scope.sortReverse = true;  // set the default sort order

    $scope.sendSatus = function (ev, record, status) {

        communicationsService.setRecordStatus(record.MlsId, status);

        if (status === "No")
            $scope.listOfRealEstates.splice($scope.listOfRealEstates.indexOf(record), 1);

        if (status === "Yes")
            ev.target.classList.add("disabled");

        if (status === "Maybe")
            ev.target.classList.add("disabled");
    }
   
    function getAllRecords() {

        document.getElementById("loading").style.display = 'block';

        mainListingService.getAllRecords()
            .success(function (listOfRealEstates) {
                document.getElementById("loading").style.display = 'none';
                $scope.listOfRealEstates = listOfRealEstates;
                //console.log($scope.listOfRealEstates);
            })
            .error(function (error) {
                document.getElementById("loading").style.display = 'none';
                $scope.status = 'Unable to load customer data: ' + error.message;
                //console.log($scope.status);
            });
    }

    getAllRecords();
});


R3App.factory('mainListingService', ['$http', function ($http) {

    var mainListingService = {};

    mainListingService.getAllRecords = function () {
        return $http.get('GetAllRecords');
    };

    return mainListingService;
}]);


// I act a repository for the remote  collection.
R3App.service("communicationsService",
    function ($http, $q) {
        // Return public API.
        return ({
            setRecordStatus: setRecordStatus
        });
        // ---
        // PUBLIC METHODS.
        // ---
        function setRecordStatus(index, status) {
            var request = $http({
                method: "post",
                url: "SetStatus",
                data: {
                    index: index,
                    status: status
                }
            });
            return (request.then(handleSuccess, handleError));
        }
        // ---
        // PRIVATE METHODS.
        // ---
        // I transform the error response, unwrapping the application dta from
        // the API response payload.
        function handleError(response) {
            // The API response from the server should be returned in a
            // nomralized format. However, if the request was not handled by the
            // server (or what not handles properly - ex. server error), then we
            // may have to normalize it on our end, as best we can.
            if (
                !angular.isObject(response.data) ||
                !response.data.message
                ) {
                return ($q.reject("An unknown error occurred."));
            }
            // Otherwise, use expected error message.
            return ($q.reject(response.data.message));
        }
        // I transform the successful response, unwrapping the application data
        // from the API response payload.
        function handleSuccess(response) {
            return (response.data);
        }
    }
);

