(function () {

  angular.module('meanApp', ['ngRoute']);

  function config ($routeProvider, $locationProvider) {
    $routeProvider
      .when('/', {
          templateUrl: '/view/home/home.view.html',
        controller: 'homeCtrl',
        controllerAs: 'vm'
      })
      .when('/register', {
          templateUrl: '/view/register/register.view.html',
        controller: 'registerCtrl',
        controllerAs: 'vm'
      })
      .when('/login', {
          templateUrl: '/view/login/login.view.html',
        controller: 'loginCtrl',
        controllerAs: 'vm'
      })
      .when('/profile', {
        templateUrl: '/view/profile/profile.view.html',
        controller: 'profileCtrl',
        controllerAs: 'vm'
      })
        .when('/logout', {
            templateUrl: '/view/login/login.view.html',
            controller: 'loginCtrl',
            controllerAs: 'vm'
        })
          .when('/task', {
              templateUrl: '/view/task/task.view.html',
              controller: 'taskCtrl',
              controllerAs: 'vm'
          })
      .otherwise({redirectTo: '/'});

    // use the HTML5 History API
    $locationProvider.html5Mode(true);
  }

  function run($rootScope, $location, authentication) {
    $rootScope.$on('$routeChangeStart', function(event, nextRoute, currentRoute) {
        if (($location.path() === '/profile' && !authentication.isLoggedIn()) || ($location.path() === '/task' && !authentication.isLoggedIn())) {
        $location.path('/');
      }
    });
  }
  angular
    .module('meanApp')
    .config(['$routeProvider', '$locationProvider', config])
    .run(['$rootScope', '$location', 'authentication', run]);

})();