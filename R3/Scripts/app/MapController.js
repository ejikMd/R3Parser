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

            var markerIcon = L.spriteIcon();
            if (item.IsNew === true || item.PriceChange !== 0)
                markerIcon = L.spriteIcon('red');
            if (item.Status === "Maybe")
                markerIcon = L.spriteIcon('yellow');
            if (item.Status === "Yes")
                markerIcon = L.spriteIcon('green');


            var marker = L.marker([item.Latitude, item.Longitude], { icon: markerIcon }).bindPopup(message);

            marker.on('mouseover', function (evt) {
                evt.target.openPopup();
            });

            marker.addTo(map);
        }
    }

    var map = L.map('map').setView([45.4656229, -73.837839], 14);

    L.tileLayer('http://a.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="http://osm.org/copyright" title="OpenStreetMap" target="_blank">OpenStreetMap</a>',
        subdomains: ['otile1', 'otile2', 'otile3', 'otile4']
    }).addTo(map);

    L.polygon([[45.49620752476158, -73.86676112076171], [45.480402341184416, -73.87696755522353], [45.46475324875623, -73.89928353422744], [45.44006700490998, -73.88572228544814],
        [45.425250071921845, -73.85344994658095], [45.470652263077326, -73.7792922317372], [45.51501288513557, -73.76920615049247], [45.54447547471287, -73.79821692319754],
        [45.53052776415771, -73.8342658123577], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171],
        [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171],
        [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171], [45.49620752476158, -73.86676112076171]]
            , { fill: false }).addTo(map);


    getAllRecords();

});


R3App.factory('mainListingService', ['$http', function ($http) {

    var mainListingService = {};

    mainListingService.getAllRecords = function () {
        return $http.get('GetAllRecords');
    };

    return mainListingService;
}]);
