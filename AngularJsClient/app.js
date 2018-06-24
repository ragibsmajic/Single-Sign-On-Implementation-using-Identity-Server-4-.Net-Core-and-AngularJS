(function(){

    var mainAppModule = angular.module('angularjsclient', ['ngRoute', 'ngResource','ngStorage', 'afOAuth2']);

    mainAppModule.config(["$routeProvider", "$locationProvider", function($routeProvider, $locationProvider){

        $routeProvider
            .when("/", {
                templateUrl: 'Templates/home.html',
                controller: 'HomeController'
            })
            .when("/about", {
                templateUrl: 'Templates/about.html',
                controller: 'AboutController'
            })
            .when("/protected", {
                templateUrl: 'Templates/protected.html',
                controller: 'ProtectedController',
                requireToken: true
            })
            .when("/contact", {
                templateUrl: 'Templates/contact.html',
                controller: 'ContactController'
            })

    }]);


}());