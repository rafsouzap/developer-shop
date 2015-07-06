angular.module('developer-shop', ['ngRoute', 'ngResource', 'ui.bootstrap'])
	.config(function ($routeProvider) {
	    $routeProvider.when('/developers', {
	        templateUrl: 'Partials/developers.html',
	        controller: 'developer-controller'
	    });
	});