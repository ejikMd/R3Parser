R3App.controller("MapController", function ($scope, mainListingService) {

    $scope.listOfRealEstates = [];

    function getAllRecords() {

        document.getElementById("loading").style.display = 'block';

        mainListingService.getAllRecords()
            .success(function (listOfRealEstates) {
                document.getElementById("loading").style.display = 'none';
                $scope.listOfRealEstates = listOfRealEstates;
                populateMarkers();
            })
            .error(function (error) {
                document.getElementById("loading").style.display = 'none';
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }

    function populateMarkers() {

        for (var i = 0; i < $scope.listOfRealEstates.length; i++) {
            var item = $scope.listOfRealEstates[i];

            var message = "<a href='" + item.RelativeDetailsURL + "' target='_blank'>" + item.Price + "</a>";
            if (item.PriceChange !== 0)
                message = message + "</br>" + item.PriceChange;

            var marker = L.marker([item.Latitude, item.Longitude]).bindPopup(message);

            marker.on('mouseover', function (evt) {
                evt.target.openPopup();
            });

            marker.addTo(map);
        }
    }

    var map = L.map('map').setView([45.4739896, -73.7513217], 13);


    L.tileLayer('http://{s}.mqcdn.com/tiles/1.0.0/map/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="http://osm.org/copyright" title="OpenStreetMap" target="_blank">OpenStreetMap</a> contributors | Tiles Courtesy of <a href="http://www.mapquest.com/" title="MapQuest" target="_blank">MapQuest</a> <img src="http://developer.mapquest.com/content/osm/mq_logo.png" width="16" height="16">',
        subdomains: ['otile1', 'otile2', 'otile3', 'otile4']
    }).addTo(map);

    getAllRecords();

});


R3App.factory('mainListingService', ['$http', function ($http) {

    var mainListingService = {};

    mainListingService.getAllRecords = function () {
        return $http.get('GetAllRecords');
    };

    return mainListingService;
}]);
