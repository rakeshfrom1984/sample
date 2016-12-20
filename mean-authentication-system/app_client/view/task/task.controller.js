(function () {


    angular
      .module('meanApp')
      .controller('taskCtrl', taskCtrl);

    taskCtrl.$inject = ['$location', 'taskService', 'authentication'];
    function taskCtrl($location, taskService, authentication) {
        var vm = this;
        vm.isAdmin = false;
        vm.taskList = [];
        vm.task = {
            userid: "",
            title: "",
            description: "",
        };

        vm.onSubmit = function () {
            console.log('Submitting task');

            if (!vm.isEdit) {
                taskService
                  .saveTask(vm.task)
                  .error(function (err) {
                      alert(err);
                  })
                  .then(function (data) {
                      vm.taskList.push(data.data);
                  });
            }
            else {
                alert('update');
                taskService
                    .updateTask(vm.task)
                    .error(function (err) {
                        alert(err);
                    })
                    .then(function (data) {
                        vm.isEdit = false;

                        // splice
                        vm.index
                        //vm.taskList.push(data.data);
                        vm.task = {};
                    });
            }
        };

        vm.onEdit = function (item, index) {
            vm.isEdit = true;
            vm.index = index;
            vm.task = item;
        }
        angular.element(document).ready(function () {
            // calling the code for the check role

            var role = authentication.getRole();
            vm.isAdmin = role == "Admin" ? true : false;

            vm.isEdit = false;
            console.log('getting task list');
            taskService
              .gettaskList()
                 //.success(function (data) {
                 //    taskList.push(data);
                 //})
              .error(function (err) {
                  alert(err);
              })
              .then(function (data) {
                  debugger;
                  //$location.path('profile');
                  vm.taskList = data.data;
              });
        });

    }

})();