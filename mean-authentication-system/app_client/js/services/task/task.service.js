(function () {

    angular
      .module('meanApp')
      .service('taskService', taskService);

    taskService.$inject = ['$http', 'authentication'];
    function taskService($http, authentication) {

        // get task list
        var gettaskList = function () {
            return $http.get('/api/task', {
                headers: {
                    Authorization: 'Bearer ' + authentication.getToken()
                }
            });
        };
        // save task code
        var saveTask = function (obj) {
            return $http.post('/api/task', obj, {
                headers: {
                    Authorization: 'Bearer ' + authentication.getToken()
                }
            });
        };

        // update task code
        var updateTask = function (obj) {
            return $http.put('/api/task', obj, {
                headers: {
                    Authorization: 'Bearer ' + authentication.getToken()
                }
            });
        };

        return {
            gettaskList: gettaskList,
            saveTask: saveTask,
            updateTask: updateTask
        };
    }

})();