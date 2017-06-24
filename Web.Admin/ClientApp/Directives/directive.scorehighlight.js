app.directive('poahighlight', [function ($location) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, model) {
            var $el = angular.element(element);
            var $a = $el.children('a');
            
            var href = $a.attr('href');
            var poa = attrs.poahighlight;
            
            $el.toggleClass('negative', (poa < 0));
        }
    };
}]);

app.directive('winhighlight', [function ($location) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, model) {
            var $el = angular.element(element);
            var $a = $el.children('a');
            
            var href = $a.attr('href');
            var wlt = attrs.winhighlight;

            $el.toggleClass('win', (wlt == "W"));
            $el.toggleClass('loss', (wlt == "L"));
            $el.toggleClass('tie', (wlt == "T"));
        }
    };
}]);