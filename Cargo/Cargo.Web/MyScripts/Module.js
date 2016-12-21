var app = angular.module("CargoClix", ["ngRoute",  'ui.bootstrap']);

    //The Factory used to define the value to
    //Communicate and pass data across controllers

    app.factory("ShareData", function () {
        return { UserId: '', UserName: '', Phone: '', LoginMethod: '', SlotBooking:'', data: {}}
    });

    //Defining Routing
    app.config(['$routeProvider','$locationProvider', function ($routeProvider,$locationProvider) {
        $routeProvider.when('/Login',
                            {
                                templateUrl: 'Home/login',
                                controller: 'LoginController'
                            });
        $routeProvider.when('/',
                            {
                                templateUrl: 'Home/Home',
                                controller: 'HomeController'
                            });
        $routeProvider.when("/Slots",
                            {
                                templateUrl: 'Home/Slots',
                                controller: 'SlotsController'
                            });
        $routeProvider.when("/SlotDetail",
                            {
                                templateUrl: 'Home/SlotDetail',
                                controller: 'SlotDetailController'
                            });
        $routeProvider.when("/SlotDetailMessage",
                            {
                                templateUrl: 'Home/SlotDetailMessage',
                                controller: 'SlotDetailMessageController'
                            });
        $routeProvider.when("/ReportDelay",
                            {
                                templateUrl: 'Home/ReportDelay',
                                controller: 'ReportDelayController'
                            });
        //$routeProvider.when('/deleteemployee',
        //                    {
        //                        templateUrl: 'EmployeeInfo/DeleteEmployee',
        //                        controller: 'DeleteEmployeeController'
        //                    });
        $routeProvider.otherwise(
                            {
                                redirectTo: '/'
                            });
       // $locationProvider.html5Mode(true);
        $locationProvider.html5Mode(true).hashPrefix('!')
    }]);