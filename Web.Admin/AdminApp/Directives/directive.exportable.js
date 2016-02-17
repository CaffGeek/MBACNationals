app.directive('exportable', function ($location) {
    function exportData(exportTable) {
        var reportHtml = exportTable[0].outerHTML;

        var blob = new Blob([reportHtml], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });

        var fileName = exportTable.attr('data-exportable') || 'Report';
        saveAs(blob, fileName + ".xls");
    }

    return {
        restrict: 'A',
        link: function (scope, element, attrs, model) {
            var $el = $(element[0]);
            var $exportButton = $('<button />')
                .append('Export')
                .click(function () { exportData($el); });
            $exportButton.insertBefore($el);
        }
    };
});