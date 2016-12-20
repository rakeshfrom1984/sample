(function () {

    angular
      .module('meanApp')
      .service('authentication', authentication);

    authentication.$inject = ['$http', '$window'];
    function authentication($http, $window) {

        var saveToken = function (data) {
            $window.localStorage['mean-token'] = data.token;
            $window.localStorage['mean-role'] = data.role;
        };

        var getToken = function () {
            return $window.localStorage['mean-token'];
        };

        var getRole = function () {
            return $window.localStorage['mean-role'];
        };
        var isLoggedIn = function () {
            var token = getToken();
            var payload;

            if (token) {
                payload = token.split('.')[1];
                payload = $window.atob(payload);
                payload = JSON.parse(payload);

                return payload.exp > Date.now() / 1000;
            } else {
                return false;
            }
        };

        var currentUser = function () {
            if (isLoggedIn()) {
                var token = getToken();
                var payload = token.split('.')[1];
                payload = $window.atob(payload);
                payload = JSON.parse(payload);
                return {
                    email: payload.email,
                    name: payload.name
                };
            }
        };

        register = function (user) {
            return $http.post('/api/register', user).success(function (data) {
                saveToken(data);
            });
        };

        login = function (user) {
            return $http.post('/api/login', user).success(function (data) {
                saveToken(data);

            });
        };

        logout = function () {
            $window.localStorage.removeItem('mean-token');
        };

      

        return {
            currentUser: currentUser,
            saveToken: saveToken,
            getToken: getToken,
            isLoggedIn: isLoggedIn,
            register: register,
            login: login,
            logout: logout,
            getRole: getRole
        };
    }


})();