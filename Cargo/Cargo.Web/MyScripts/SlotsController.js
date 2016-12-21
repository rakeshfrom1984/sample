app.controller('SlotsController', function ($scope ,$location,ShareData, SinglePageCRUDService) {
    // On view load the function get the list of Slot bookings
    angular.element(document).ready(function () {
        debugger;
        var z = ShareData.UserId;
        $scope.phone = ShareData.Phone;
        $scope.method = ShareData.LoginMethod;
        var promisePost = SinglePageCRUDService.GetSlots(ShareData.UserId);


        promisePost.then(function (pl) {
            debugger;
            $scope.List=pl.data;
        },
              function (errorPl) {
                  $scope.error = 'failure loading Employee', errorPl;
              });

    });
    $scope.ShowDetail = function (obj) {
        debugger;
        ShareData.SlotBookerId = obj.BookingId;
        ShareData.data = { SlotBookerId: obj.BookingId, SlotBookerName: obj.BookerName, ReachTime: obj.ReachTime };
        $location.path("/SlotDetail");
    };
});