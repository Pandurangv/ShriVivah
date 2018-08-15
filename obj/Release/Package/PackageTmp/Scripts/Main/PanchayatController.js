VarmalaVivahApp.factory('PanchayatService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getUserDetails = function () {
        return $http.get(urlBase + "/agent/getpanchayatlist");
    };

    return ReportService;
}]);

VarmalaVivahApp.controller("PanchayatController", ['$scope', '$http', '$filter', 'PanchayatService', function ($scope, $http, $filter, PanchayatService) {

    $scope.MainUserList = [];

    $scope.UserList = [];

    $scope.SearchUserList = [];

    $scope.RootUrl = GetVirtualDirectory() + "/";

    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

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
    $scope.Prefix = "";
    $scope.FilterList = function () {
        if ($scope.Prefix=="") {
            $scope.UserList = $scope.MainUserList;
        }
        else {
            var reg = new RegExp($scope.Prefix.toLowerCase());
            $scope.UserList = $scope.MainUserList.filter(function (actype) {
                return ((reg.test(actype.Name.toLowerCase())
                    || reg.test(actype.City.toLowerCase())
                    || reg.test(actype.MailId.toLowerCase())
                        )
                    );
            });
        }
        $scope.First();
    }
    $scope.GetUserData = function () {
        ShowLoader();
        PanchayatService.getUserDetails()
        .success(function (studs) {
            HideLoader();
            $scope.MainUserList = [];
            $scope.MainUserList=studs.UserList;
            $scope.UserList = $scope.MainUserList;
            $scope.First();
            //$scope.UserList = studs;
        }).error(function (error) {
            HideLoader();
            findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
        });
    }

    $scope.init = function () {
        $scope.Paging = 10;
        
        $scope.GetUserData();
    }
    $scope.init();
    
}])