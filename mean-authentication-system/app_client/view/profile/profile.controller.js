(function() {
  
  angular
    .module('meanApp')
    .controller('profileCtrl', profileCtrl);

  profileCtrl.$inject = ['$location', 'profileService'];
  function profileCtrl($location, profileService) {
    var vm = this;

    vm.user = {};

      profileService.getProfile()
      .success(function(data) {
        vm.user = data;
      })
      .error(function (e) {
        console.log(e);
      });
  }

})();