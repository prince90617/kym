(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserService', UserService);

    UserService.$inject = ['$http', '$timeout'];
    function UserService($http, $timeout) {
 var baseUrl = 'http://localhost:9553/'; 
        var service = {};
        service.Authenticate = Authenticate;
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByUsername = GetByUsername;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;


        return service;
       
        function Authenticate(username, password) {
            $http.defaults.headers.common = {};
            $http.defaults.headers.post = {};
            $http.defaults.headers.put = {};
            $http.defaults.headers.patch = {};
            var user = { grant_type: 'password', userName: username, password: password, client_id: 1 }
            var req = {
                method: 'POST',
                url: baseUrl+'api/token',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: $.param(user)
            }

            return $http(req).then(handleSuccess, handleError);


        }
        function GetAll() {

            //return $http.get('/api/users').then(handleSuccess, handleError('Error getting all users'));
        }

        function GetById(id) {
            // return $http.get('/api/users/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }

        function GetByUsername(username) {
            // return $http.get('/api/users/' + username).then(handleSuccess, handleError('Error getting user by username'));
        }

        function Create(user) {
            return $http.post('/api/users', user).then(handleSuccess, handleError('Error creating user'));
        }

        function Update(user) {
            //return $http.put('/api/users/' + user.id, user).then(handleSuccess, handleError('Error updating user'));
        }

        function Delete(id) {
            // return $http.delete('/api/users/' + id).then(handleSuccess, handleError('Error deleting user'));
        }

        // private functions

        function handleSuccess(res) {
            return res.data;
        }
        
        function handleError(error) {
             return errorMessage(error.data['error_description']);
        }
        function errorMessage(errMsg) {
            return { success: false, message: errMsg };
        };
        function failureError(msg) {
            return function () {
                return { success: false, message: msg };
            };
        }


    }

})();
