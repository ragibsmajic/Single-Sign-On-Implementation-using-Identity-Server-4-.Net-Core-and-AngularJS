(function(){
    
    var mainAppModule = angular.module('angularjsclient');

    var ProtectedController = function($scope, $location, AccessToken, $http){

            $scope.access_id_tokens = JSON.stringify(AccessToken.get(), undefined, 2);

            $scope.access_token_decoded = JSON.stringify(jwt_decode(AccessToken.get().access_token), undefined, 2);

            $scope.id_token_decoded = JSON.stringify(jwt_decode(AccessToken.get().id_token), undefined, 2);

            $scope.authenticatedUserName = jwt_decode(AccessToken.get().id_token).name;

            $http.get("http://localhost:5001/identity").then(function(response){

                    $scope.serverResponse = JSON.stringify(response.data, undefined, 2);
            });

            $http.get("http://localhost:5000/connect/userinfo").then(function(response){

                    $scope.user = JSON.stringify(response.data, undefined, 2);
            });

    };

    mainAppModule.controller('ProtectedController', ['$scope', '$location','AccessToken', '$http', ProtectedController]);

}());