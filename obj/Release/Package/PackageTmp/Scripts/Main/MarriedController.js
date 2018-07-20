VarmalaVivahApp.factory('MarriedUserService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getMarriedUsers = function () {
        return $http.get(urlBase + "/userprofile/GetMarriedUserData");
    };

    //ReportService.getQualifications = function () {
    //    return $http.get(urlBase + "/Qualification/Educations");
    //};

    //ReportService.MarriageDone = function (UserId) {
    //    return $http.post(urlBase + "/userprofile/MarriageDone", { UserId: UserId });
    //};

    //ReportService.SendSMS = function (UserId) {
    //    return $http.post(urlBase + "/PendingUsers/SendSMS", { UserId: UserId });
    //};

    return ReportService;
}]);

VarmalaVivahApp.controller("MarriedController", ['$scope', '$http', '$filter', 'MarriedUserService', function ($scope, $http, $filter, MarriedUserService) {

    $scope.MainUserList = [];

    $scope.UserList = [];

    $scope.SearchUserList = [];

    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

    $scope.Prefix = "";

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
            findAndCallErrorBox("", "Already on first page", "alert", null, null);
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.UserList.length) {
            $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            findAndCallErrorBox("", "Already on last page", "alert", null, null);
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.UserList.length;
        var rem = parseInt($scope.UserList.length) % parseInt($scope.Paging);
        var position = $scope.UserList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.UserList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, position);
    }

    $scope.GetMarriedUserData = function () {
        ShowLoader();
        MarriedUserService.getMarriedUsers()
           .success(function (qualifications) {
               HideLoader();
               $scope.MainUserList = qualifications;
               $scope.UserList = $scope.MainUserList;
               $scope.First();
               //$scope.UserList = studs;
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
           });

    }

    $scope.init = function () {
        
        $scope.GetMarriedUserData();
    }
    $scope.init();

}]);