app.controller('ReportDelayController', function ($scope, $log, $location, ShareData, SinglePageCRUDService) {

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
        debugger;
        $scope.UserId = ShareData.UserId;
        $scope.phone = ShareData.Phone;
        $scope.method = ShareData.LoginMethod;
        $scope.data = ShareData.data;
        $scope.slotBookerId = ShareData.SlotBookerId;
    });

    $scope.mytime = new Date();

    $scope.hstep = 1;
    $scope.mstep = 15;

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };

    $scope.ismeridian = true;
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

    $scope.update = function () {
        var d = new Date();
        d.setHours(14);
        d.setMinutes(0);
        $scope.mytime = d;
    };

    $scope.changed = function () {
        $log.log('Time changed to: ' + $scope.mytime);
    };

    $scope.clear = function () {
        $scope.mytime = null;
    };

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
        StatusDetail.ReasonForDelay = $scope.reason;
        StatusDetail.DelayTimeETA = $scope.mytime;
        var promisePost = SinglePageCRUDService.SaveMessage(StatusDetail);
        promisePost.then(function (pl) {
            //debugger;
            //$scope.List = pl.data;
            //$scope.message = '';
            //GetList();
        },
        function (errorPl) {
            $scope.error = 'failure loading Employee', errorPl;
        });
    };
    function GetList() {
        debugger;
        $scope.UserId = ShareData.UserId;
        $scope.phone = ShareData.Phone;
        $scope.method = ShareData.LoginMethod;
        $scope.data = ShareData.data;
        $scope.slotBookerId = ShareData.SlotBookerId;

        //var promisePost = SinglePageCRUDService.GetSlotMessages(ShareData.UserId, ShareData.data.SlotBookerId, ShareData.data.Status);
        //promisePost.then(function (pl) {
        //    debugger;
        //    $scope.List = pl.data;
        //},
        //function (errorPl) {
        //    $scope.error = 'failure loading Employee', errorPl;
        //});
    }
});