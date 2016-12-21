app.controller('SlotDetailMessageController', function ($scope, $location, ShareData, SinglePageCRUDService) {

    var StatusDetail = {
        UserId: '',
        SlotId: '',
        Message: '',
        DelayTimeETA: '',
        ReasonForDelay: '',
        OnTheWay: '',
        Status: ''
    }
    // On view load the function get the list of Slot bookings
    angular.element(document).ready(function () {
        GetList();
    });

    $scope.SaveMessage = function () {
        debugger;
        StatusDetail.Message = $scope.message;
        StatusDetail.UserId = ShareData.UserId;
        StatusDetail.SlotId = ShareData.SlotBookerId;
        StatusDetail.Status = ShareData.data.Status;
        var promisePost = SinglePageCRUDService.SaveMessage(StatusDetail);
        promisePost.then(function (pl) {
            debugger;
            $scope.List = pl.data;
            $scope.message = '';
            GetList();
        },
        function (errorPl) {
            $scope.error = 'failure loading Employee', errorPl;
        });
    };
    function GetList() {
        debugger;
        $scope.UserId = ShareData.UserId;
        $scope.data = ShareData.data;

        $scope.phone = ShareData.Phone;
        $scope.method = ShareData.LoginMethod;

        var promisePost = SinglePageCRUDService.GetSlotMessages(ShareData.UserId, ShareData.data.SlotBookerId, ShareData.data.Status);
        promisePost.then(function (pl) {
            debugger;
            $scope.List = pl.data;
        },
        function (errorPl) {
            $scope.error = 'failure loading Employee', errorPl;
        });
    }
});