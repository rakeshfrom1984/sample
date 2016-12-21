app.controller('SlotDetailController', function ($scope, $location, ShareData, SinglePageCRUDService) {
    // On view load the function get the list of Slot bookings
    angular.element(document).ready(function () {
        debugger;
        $scope.UserId = ShareData.UserId;
        $scope.phone = ShareData.Phone;
        $scope.method = ShareData.LoginMethod;
        $scope.data = ShareData.data;
        $scope.slotBookerId = ShareData.SlotBookerId;
    });
    $scope.Status = '';
    var StatusDetail = {
        UserId: '',
        SlotId: '',
        Message: '',
        DelayTimeETA: '',
        ReasonForDelay: '',
        OnTheWay: '',
        Status: ''
    }
    $scope.SaveOnTheWay = function (obj) {
        debugger;
        StatusDetail.Status = $scope.Status;
        StatusDetail.UserId = $scope.UserId;
        StatusDetail.SlotId = $scope.slotBookerId;
        StatusDetail.OnTheWay = obj;
        var promisePost = SinglePageCRUDService.SaveMessage(StatusDetail);


        promisePost.then(function (pl) {
            debugger;
            //alert('Status Updated');
            //$location.path("/Slots");
            $('#modalWorkplace').modal('hide');
        },
              function (errorPl) {
                  $scope.error = 'failure loading Employee', errorPl;
              });

    }

    $scope.Message = function (obj) {
        debugger;
        if (obj == 1) {
            $('#modalWorkplace').modal('show');
            $scope.Status = 1;
        }
        else if (obj == 2) {
            $scope.Status = 2;
            ShareData.data.Status = 2;
            $location.path("/ReportDelay");
        }
        else if (obj == 3) {
            $scope.Status = 3;
            ShareData.data.Status = 3;
            $location.path("/SlotDetailMessage");
        }
    }

});