angular.module('developer-shop').controller('developer-controller', function ($scope, $resource) {

    $scope.developers = [];
    var resDevelopers = $resource('/api/developer/:id');

    resDevelopers.query(
		function (developers) {
		    $scope.developers = developers;
		},
		function (error) {
		    $dialogs.error('This is my error message');
		    console.log("Couldn't load developers");
		    console.log(error);
		}
	);
});