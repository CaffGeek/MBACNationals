﻿(function () {
    "use strict";

    var historyController = function (dataService) {
        var vm = this;
        
        vm.History = [
            { year: 2019, province: 'QC', city: 'Gatineau' },
            { year: 2018, province: 'NO', city: 'Thunder Bay' },
            { year: 2017, province: 'SK', city: 'Regina' },
            { year: 2016, province: 'BC', city: 'Surrey' },
            { year: 2015, province: 'SO', city: 'Hamilton' },
            { year: 2014, province: 'MB', city: 'Winnipeg' },
            { year: 2013, province: 'AB', city: 'Calgary' },
            { year: 2012, province: 'NL', city: 'St John\'s' },
            { year: 2011, province: 'QC', city: 'Gatineau' },
            { year: 2010, province: 'NO', city: 'Sudbury' },
            { year: 2009, province: 'SK', city: 'Saskatoon' },
            { year: 2008, province: 'SO', city: 'Hamilton' },
            { year: 2007, isMissing: true },
            { year: 2006, province: 'MB', city: 'Winnipeg' },
            { year: 2005, province: 'AB', city: 'Red Deer' },
            { year: 2004, province: 'NL', city: 'St John\'s' },
            { year: 2003, province: 'QC', city: 'Gatineau' },
            { year: 2002, province: 'NO', city: 'Thunder Bay' },
            { year: 2001, province: 'SK', city: 'Saskatoon' },
            { year: 2000, province: 'SO', city: 'London' },
            { year: 1999, province: 'BC', city: 'Surrey' },
            { year: 1998, province: 'AB', city: 'Calgary' },
            { year: 1997, province: 'MB', city: 'Winnipeg' },
            { year: 1996, province: 'NL', city: 'St John\'s' },
            { year: 1995, province: 'SK', city: 'Saskatoon' },
            { year: 1994, province: 'SO', city: 'Oshawa' },
            { year: 1993, province: 'NO', city: 'Sudbury' },
            { year: 1992, province: 'BC', city: 'Victoria' },
            { year: 1991, province: 'MB', city: 'Winnipeg' },
            { year: 1990, province: 'AB', city: 'Edmonton' },
            { year: 1989, province: 'NL', city: 'St John\'s' },
            { year: 1988, province: 'SK', city: 'Saskatoon' },
            { year: 1987, province: 'BC', city: 'Nanaimo' },
            { year: 1986, isMissing: true },
            { year: 1985, province: 'MB', city: 'Winnipeg' },
            { year: 1984, province: 'AB', city: 'Calgary' },
        ];

        vm.selected = vm.History[0];

        vm.getPdfLink = function (year) {
            return '/Content/PDFs/' + year + '.pdf';
        };
    };

    app.controller("HistoryController", ["dataService", historyController]);
}());