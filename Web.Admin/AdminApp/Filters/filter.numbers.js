angular.module('numberFilters', []).
    filter('rounddown', function () {
        return function (input) {
            return Math.floor(input);
        };
    }).
    filter('hour', function () {
        return function (input) {
            if (input < 12)
                return input + ' am';
            else if (input == 12)
                return input + ' pm';
            else
                return (input - 12) + ' pm';
        };
    });