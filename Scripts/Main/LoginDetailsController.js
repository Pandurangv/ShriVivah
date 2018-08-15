VarmalaVivahApp.factory('LoginService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.GetLoginDetails = function (Id) {
        if (Id === undefined) {
            Id = 0
        }
        return $http.get(urlBase + "/LoginDetails/GetLoginDetails");
    };

    
    return ReportService;
}]);

VarmalaVivahApp.controller("LoginDetailsController", ['$scope', '$http', '$filter', '$rootScope', 'LoginService', function ($scope, $http, $filter, $rootScope, LoginService) {
    $scope.MainLoginDetailsList = [];

    $scope.LoginDetailsList = [];
    $scope.SearchLoginDetailsList = [];

    $scope.Details = true;
    $scope.ErrorModel = { IsSelectLoginDetails: false, IsLoginDetailssAmount: false };
    $scope.ErrorMessage = ""
    $scope.Add = false;
    $scope.Edit = false;
    $scope.LoginDetailsId = 0;
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.LoginDetailsModel = { LoginDetailsId: 0, LoginDetailssAmount: 0, LoginDetailsName: "" };

    $scope.Prefix = "";

    //$scope.AddNewUI = function (isedit) {
    //    $scope.LoginDetailsModel = { LoginDetailsId: 0, LoginDetailssAmount: 0, LoginDetailsName: "" };
    //    $scope.Details = false;
    //    $scope.Add = true;
    //    $scope.Edit = false;
    //}


    function GetLoginDetailss() {
        LoginService.GetLoginDetails()
           .success(function (qualifications) {
               $scope.MainLoginDetailsList = qualifications.LoginDetails;
               $scope.LoginDetailsList = qualifications.LoginDetails;
               $scope.First();
           }).error(function (error) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Record not saved.",
                   Type: "alert",
               });
               objShowCustomAlert.ShowCustomAlertBox();
           });
    }

    $scope.FilterList = function () {
        //$scope.LoginDetailsList = $filter('filter')(JSON.parse($("#LoginDetailsList").val()), { LoginDetails: $scope.Prefix })
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.LoginDetailsList = $scope.MainLoginDetailsList.filter(function (actype) {
            return (reg.test(actype.UserName.toLowerCase()));
        });
        $scope.First();
    }

    //$scope.Reset = function () {
    //    $scope.LoginDetailsList = $scope.MainLoginDetailsList;
    //    $scope.SearchLoginDetailsList = $scope.LoginDetailsList;
    //    $scope.First();
    //}

    //$scope.CancelClick = function () {
    //    $scope.LoginDetailsModel = { LoginDetailsId: 0, LoginDetailssAmount: 0, LoginDetailsName: "" };
    //    $scope.Details = true;
    //    $scope.Add = false;
    //    $scope.Edit = false;
    //}

    //$scope.EditClick = function (LoginDetailsModel) {
    //    $scope.LoginDetailsModel = LoginDetailsModel;
    //    $scope.Details = false;
    //    $scope.Add = false;
    //    $scope.Edit = true;
    //}

    //$scope.Save = function (isEdit) {
    //    if ($scope.LoginDetailsModel.LoginDetailsName == "") {
    //        $scope.ErrorModel.IsSelectLoginDetails = true;
    //        $scope.ErrorMessage = "LoginDetails should be filled.";
    //        return false;
    //    }
    //    else {
    //        $scope.ErrorModel.IsSelectLoginDetails = false;
    //    }
    //    if ($scope.LoginDetailsModel.LoginDetailssAmount == 0) {
    //        $scope.ErrorModel.IsLoginDetailssAmount = true;
    //        $scope.ErrorMessage = "LoginDetails amount should be filled.";
    //        return false;
    //    }
    //    else {
    //        $scope.ErrorModel.IsLoginDetailssAmount = false;
    //    }
    //    var url = GetVirtualDirectory() + '/LoginDetails/Save';
    //    if (isEdit == false) {
    //        url = GetVirtualDirectory() + '/LoginDetails/Update';
    //    }
    //    var model = $scope.LoginDetailsModel;

    //    LoginDetailsService.Save(model, isEdit)
    //       .success(function (qualifications) {
    //           HideLoader();
    //           var objShowCustomAlert = new ShowCustomAlert({
    //               Title: "",
    //               Message: "Record saved successfully.",
    //               Type: "alert",
    //           });
    //           objShowCustomAlert.ShowCustomAlertBox();
    //           $scope.CancelClick();

    //       }).error(function (error) {
    //           HideLoader();
    //           var objShowCustomAlert = new ShowCustomAlert({
    //               Title: "",
    //               Message: "Record not saved.",
    //               Type: "alert",
    //           });
    //           objShowCustomAlert.ShowCustomAlertBox();
    //       });
    //}

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchLoginDetailsList = $filter('limitTo')($scope.LoginDetailsList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchLoginDetailsList = $filter('limitTo')($scope.LoginDetailsList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.LoginDetailsList.length) {
            $scope.SearchLoginDetailsList = $filter('limitTo')($scope.LoginDetailsList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.LoginDetailsList.length;
        var rem = parseInt($scope.LoginDetailsList.length) % parseInt($scope.Paging);
        var position = $scope.LoginDetailsList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.LoginDetailsList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchLoginDetailsList = $filter('limitTo')($scope.LoginDetailsList, $scope.Paging, position);
    }

    $scope.init = function () {
        GetLoginDetailss();
    }

    $scope.init();

}]);