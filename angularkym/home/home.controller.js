(function () {
    'use strict';

    angular
        .module('loggedinapp')
        .controller('HomeController', HomeController);

    HomeController.$inject = [ '$rootScope', '$cookieStore'];
    function HomeController( $rootScope,$cookieStore) {
        var vm = this;

        vm.user = null;
       // vm.allUsers = [];
       // vm.deleteUser = deleteUser;
        
        initController();

        function initController() {
           
            loadCurrentUser();
           // loadAllUsers();
        }

        function loadCurrentUser() {
            $rootScope.globals = $cookieStore.get('globals') || {};
            vm.user = $rootScope.globals.currentUser;
          //  UserService.GetByUsername($rootScope.globals.currentUser.username)
          //      .then(function (user) {
          //          vm.user = user;
           //     });
        }

        function loadAllUsers() {
            UserService.GetAll()
                .then(function (users) {
                    vm.allUsers = users;
                });
        }

        function deleteUser(id) {
            UserService.Delete(id)
            .then(function () {
                loadAllUsers();
            });
        }
    }

})();