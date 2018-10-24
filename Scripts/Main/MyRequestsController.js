VarmalaVivahApp.factory('MyRequestsService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.myRequestDetails = function (model) {
        return $http.post(urlBase + "/MyRequests/SearchRequests", model);
    };

    ReportService.ApproveRequest = function (RequestId, ApprovedImage, ApprovedImageWithContact) {
        return $http.post(urlBase + "/UserProfile/ApproveRequestSPMO", { VisitorId: RequestId, ApprovedImage: ApprovedImage, ApprovedImageWithContact: ApprovedImageWithContact });
    };

    return ReportService;
}]);


VarmalaVivahApp.controller("MyRequestsController", ['$scope', '$http', '$filter', 'MyRequestsService', function ($scope, $http, $filter, MyRequestsService) {

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


    $scope.ApproveRequestAll = function (user)
    {
        ShowLoader();
        MyRequestsService.ApproveRequest(user.RequestId, 1, 1)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Request has been approved.",
                   Type: "alert",
                   OnOKClick: function () {
                       $scope.SearchData();
                   },
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.RedirectUser = function (user) {
        window.location = GetVirtualDirectory() + "/UserProfile/ShowProfile?ProfileId=" + user.UserId;
    }

    $scope.ApproveRequest = function (user)
    {
        ShowLoader();
        MyRequestsService.ApproveRequest(user.RequestId,0,1)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Request has been approved.",
                   Type: "alert",
                   OnOKClick: function () {
                       $scope.SearchData();
                   },
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }


    $scope.TotalRecords = 0;

    $scope.SearchData = function () {

        ShowLoader();
        var model = { PageNo: $("#ddlPageNo").val(), PageSize: $("#ddlPageSize").val() };
        MyRequestsService.myRequestDetails(model)
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.DataResponse;
               if ($scope.UserList.length > 0) {
                   $scope.TotalRecords = response.DataResponse[0].TotalRecords;
                   $scope.SetPageNo();
               }

               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }


    $scope.GetUserRequests = function () {

        ShowLoader();
        var model = { PageNo: 1, PageSize: 50 };
        MyRequestsService.myRequestDetails(model)
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.DataResponse;
               if ($scope.UserList.length>0) {
                   $scope.TotalRecords = response.DataResponse[0].TotalRecords;
                   $scope.SetPageNo();
               }

               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.UserGender="";
    $scope.init = function () {
        $scope.IsAdmin = ($scope.IsAdmin === 'true');
        $scope.UserGender=$("#hdnGender").val();
        $scope.GetUserRequests();
    }

    $scope.init();

}]);