(function() {

  angular
    .module('meanApp')
    .service('profileService', profileService);

  profileService.$inject = ['$http', 'authentication'];
  function profileService($http, authentication) {

      var getProfile = function () {
      return $http.get('/api/profile', {
        headers: {
          Authorization: 'Bearer '+ authentication.getToken()
        }
      });
    };

    return {
      getProfile : getProfile
    };
  }

})();