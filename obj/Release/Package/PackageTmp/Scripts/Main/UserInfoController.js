VarmalaVivahApp.factory('UserInfoService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getUserDetails = function () {
        return $http.get(urlBase + "/userprofile/GetUserAdhaarData");
    };


    return ReportService;
}]);

VarmalaVivahApp.controller("UserInfoController", ['$scope', '$http', '$filter', 'UserInfoService', function ($scope, $http, $filter, UserInfoService) {

    $scope.MainUserList = [];

    $scope.UserList = [];

    $scope.SearchUserList = [];

    $scope.RootUrl = GetVirtualDirectory() + "/";
    $scope.Field = "Name";
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

    
    $scope.GetUserAdhaarPan=function()
    {
        ShowLoader();
        UserInfoService.getUserDetails()
            .success(function (studs) {
                   HideLoader();
                   $scope.MainUserList = [];// studs;
                   angular.forEach(studs, function (value, key) {
                       value.Img2 = $scope.RootUrl + value.Img2;
                       value.KImg = $scope.RootUrl + value.KImg;
                       $scope.MainUserList.push(value);
                   });
                   $scope.UserList = $scope.MainUserList;
                   $scope.First();
            }).error(function (error) {
              HideLoader();
              findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
        });
    }

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, 0);
        if ($scope.UserList.length == 0) {
            $scope.SearchUserList = $scope.UserList;
        }
        window.setTimeout(function () { $scope.$apply(); }, 3000);
    }

    $scope.FilterPageSize = function (PageSize) {
        $scope.Paging = parseInt(PageSize);
        $scope.First();
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
    $scope.Prefix = "";

    $scope.init = function () {
        $scope.GetUserAdhaarPan();
    }
    $scope.init();

}]);