﻿(function () {
    "use strict";

    var reservationController = function ($scope, $http, $q, $location, modalFactory, dataService, moment) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        $scope.model = {
            participants: [],
            rooms: [],
            year: year,
            hotels: []
        };

        if (year && province) {
            dataService.LoadParticipants(year, province).
                success(function (participants) {
                    $scope.model.participants = participants;
                });

            dataService.LoadHotels(year)
                .success(function (data) {
                    $scope.model.hotels = data;
                }).then(function () {
                    var hotel = $scope.model.hotels[0];
                    var defaultCheckin = moment(hotel.DefaultCheckin, 'YYYY-MM-DD').toDate();
                    var defaultCheckout = moment(hotel.DefaultCheckout, 'YYYY-MM-DD').toDate();

                    dataService.LoadRooms(year, province).
                        success(function (data) {
                            var sparseRooms = [];

                            for (var i = 1; i <= 25; i++) {
                                var room = data.HotelRooms.filter(function (obj) { return obj.RoomNumber == i; })[0];
                                if (room) {
                                    //UGH, such a hack
                                    var checkin = room.Checkin ? new Date(room.Checkin) : defaultCheckin;
                                    room.Checkin = new Date(checkin.getUTCFullYear(), checkin.getUTCMonth(), checkin.getUTCDate());

                                    var checkout = room.Checkout ? new Date(room.Checkout) : defaultCheckout;
                                    room.Checkout = new Date(checkout.getUTCFullYear(), checkout.getUTCMonth(), checkout.getUTCDate());
                                }

                                sparseRooms[i] = room || {
                                    RoomNumber: i,
                                    Checkin: defaultCheckin,
                                    Checkout: defaultCheckout
                                };
                            }

                            $scope.model.contingentId = data.Id;
                            $scope.model.rooms = sparseRooms;
                            $scope.model.instructions = data.Instructions;
                        });
                });
        }


        $scope.calcBin = calcBin;
        function calcBin(row, col) {
            return ((row - 1) * 5) + col;
        };

        $scope.getRoomByBin = getRoomByBin;
        function getRoomByBin(row, col) {
            var bin = calcBin(row, col);
            var foundRoom;
            $scope.model.rooms.some(function (room) {
                if (room.RoomNumber === bin) {
                    foundRoom = room;
                    return true; // break
                }
            });
            return foundRoom;
        }

        $scope.isThisRoom = function (row, col) {
            return function (participant) {
                return participant.RoomNumber == calcBin(row, col);
            }
        }

        $scope.unassignedRoom = function (participant) {
            return participant.RoomNumber == 0;
        }

        $scope.setRoomType = function (roomNumber) {
            var type = $scope.model.rooms[roomNumber].Type;
            dataService.ChangeRoomType($scope.model.contingentId, province, roomNumber, type);
        }

        //TODO: Set checkin/checkout on change

        $scope.setCheckin = function (roomNumber) {
            var checkin = $scope.model.rooms[roomNumber].Checkin;
            var checkout = $scope.model.rooms[roomNumber].Checkout;
            dataService.ChangeRoomCheckin($scope.model.contingentId, province, roomNumber, checkin, checkout);
        }
        
        $scope.addToRoom = function (id, roomNumber) {
            dataService.AssignParticipantToRoom(id, roomNumber).then(function (data) {
                var participant = $scope.model.participants.filter(function (obj) { return obj.Id == id; })[0];
                if (!participant)
                    return;

                participant.RoomNumber = roomNumber;
            });
        }

        $scope.removeFromRoom = function (id) {
            dataService.RemoveParticipantFromRoom(id).then(function (data) {
                var participant = $scope.model.participants.filter(function (obj) { return obj.Id == id; })[0];
                if (!participant)
                    return;

                participant.RoomNumber = 0;
            });
        }

        $scope.saveInstructions = function () {
            dataService.SaveInstructions($scope.model.contingentId, province, $scope.model.instructions);
        }
    };

    app.controller("ReservationController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", "moment", reservationController]);
}());