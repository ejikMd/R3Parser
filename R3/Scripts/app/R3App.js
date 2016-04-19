var R3App = angular.module("R3App", ['ngAnimate']);


R3App.filter("mydate", function () {
    var re = /\\\/Date\(([0-9]*)\)\\\//;
    return function (x) {
        return new Date(parseInt(x.substr(6)));
        var m = x.match(re);
        if (m) {
            return new Date(parseInt(m[1]));
        } else return null;
    };
});

