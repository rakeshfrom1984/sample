app.controller('LoginController', function ($scope, $location, SinglePageCRUDService, ShareData) {
    angular.element(document).ready(function () {
        $scope.phone = "9464518844";
        $scope.password = "test123";
    });
    $scope.Login = function () {
        var Login = {
            PhoneNumber: $scope.phone,
            Email: $scope.email,
            Password: $scope.password,
        };

        var promisePost = SinglePageCRUDService.Login(Login);


        promisePost.then(function (pl) {
            debugger;
            $scope.userId= pl.data.UserId;
            $scope.userName = pl.data.UserName;
            $scope.phone = pl.data.Phone;
            $scope.loginMethod = pl.data.LoginWith;
            //setting global variable
            ShareData.UserId = $scope.userId;
            ShareData.UserName = $scope.userName;
            ShareData.Phone = $scope.phone;
            ShareData.LoginMethod = $scope.loginMethod;
            $location.path("/Slots");
        },
              function (errorPl) {
                  $scope.error = 'failure loading Employee', errorPl;
              });

    };
});