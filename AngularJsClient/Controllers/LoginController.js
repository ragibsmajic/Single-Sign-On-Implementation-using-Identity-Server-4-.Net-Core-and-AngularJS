(function(){
    
        var mainAppModule = angular.module('PhotostationMainModule');
    
        var LoginController = function($scope, AuthService, $location){
        
        if(AuthService.isAuthenticated()){
                $location.path("/");
        }

                $scope.username = null;
                $scope.password = null;

            $scope.login = function(){

                AuthService.login($scope.username, $scope.password);

                if(AuthService.isAuthenticated()){
                        
                        $scope.loginError = "";

                        $scope.session.isAuthenticated = AuthService.isAuthenticated();

                        $location.path('/');

                }
                else{
                        $scope.loginError = "Bad credentials";
                }

            }

            
            
        };
    
        mainAppModule.controller('LoginController', ['$scope', 'AuthService', '$location', LoginController]);
    
}());