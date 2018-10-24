VarmalaVivahApp.factory('VisitorService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getVisitorsFirst = function (model) {
        return $http.post(urlBase + "/userprofile/VisitorFirst");
    };

    ReportService.getVisitorsPrev = function (model) {
        return $http.post(urlBase + "/userprofile/VisitorsPrev");
    };

    ReportService.getVisitorsNext = function (model) {
        return $http.post(urlBase + "/userprofile/VisitorsNext");
    };

    ReportService.getVisitorsLast = function (model) {
        return $http.post(urlBase + "/userprofile/VisitorsLast");
    };

    return ReportService;
}]);

VarmalaVivahApp.controller("VisitorController", ['$scope', '$http', '$filter', 'VisitorService', function ($scope, $http, $filter, VisitorService) {

    $scope.IsAdmin = $("#IsAdmin").val();
    $scope.UserGender = $("#UserGender").val();
    $scope.PageNo = 1;
    $scope.PageSize = 50;

    $scope.CityList = [];
    $scope.QualificationList = [];
    $scope.HeightList = [];

    $scope.UserList = [];

    

    $scope.SetPageNo = function () {
        var noofpages = $scope.TotalRecords / 50;
        var rem = $scope.TotalRecords % 50;
        if (rem > 0) {
            noofpages++;
        }
        html = "";
        for (var i = 1; i <= noofpages; i++) {
            html += '<option value="' + i + '">' + i + '</option>';
        }
        $("#ddlPageNo").html(html);
        $("#ddlPageNo").val(1);
    }

    $scope.RedirectUser = function (user) {
        window.location = GetVirtualDirectory() + "/UserProfile/ShowProfile?ProfileId=" + user.VisitedUserId;
    }

    $scope.TotalRecords = 0;

    $scope.getVisitorsLast = function () {
        ShowLoader();
        VisitorService.getVisitorsLast()
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.VisitorDetails;

               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.getVisitorsPrev = function () {
        ShowLoader();
        VisitorService.getVisitorsPrev()
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.VisitorDetails;

               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }


    $scope.getVisitorsNext = function () {
        ShowLoader();
        VisitorService.getVisitorsNext()
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.VisitorDetails;

               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.GetUserData = function () {
        ShowLoader();
        VisitorService.getVisitorsFirst()
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.VisitorDetails;
               
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.init = function () {
        $scope.IsAdmin = ($scope.IsAdmin === 'true');
        
        $scope.GetUserData();
    }

    $scope.init();

}]);