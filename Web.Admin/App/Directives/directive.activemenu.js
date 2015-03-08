app.directive('activeMenu', ['$location', function ($location) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, model) {
            var $el = angular.element(element);
            var $a = $el.children('a');
            var className = attrs[0] || 'active';

            var href = $a.attr('href');
            var url = $location.absUrl();

            var isActive = !!(url.indexOf(href, url.length - href.length) !== -1);
                $el.toggleClass(className, isActive);
        }
    };
}]);