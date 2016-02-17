(function () {
    "use strict";

    var reservationController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);
        var year = url.slice(lastSlash - 4, lastSlash);

        $scope.model = {
            participants: [],
            rooms: [],
            year: year
        };

        if (year && province) {
            dataService.LoadParticipants(year, province).
                success(function (participants) {
                    $scope.model.participants = participants;
                });
            dataService.LoadRooms(year, province).
                success(function (data) {
                    var sparseRooms = [];

                    for (var i = 1; i <= 25; i++) {
                        var room = data.HotelRooms.filter(function (obj) { return obj.RoomNumber == i; })[0];
                        sparseRooms[i] = room || { RoomNumber: i };
                    }

                    $scope.model.contingentId = data.Id;
                    $scope.model.rooms = sparseRooms;
                    $scope.model.instructions = data.Instructions;
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

    app.controller("ReservationController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reservationController]);
}());