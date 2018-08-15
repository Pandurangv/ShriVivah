VarmalaVivahApp.factory('BarrierService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.GetDetails = function (Id) {
        if (Id === undefined) {
            Id = 0
        }
        return $http.get(urlBase + "/Barrier/GetBarriers");
    };

    ReportService.Save = function (model, IsEdit) {
        var url = urlBase + '/Barrier/Save';
        if (IsEdit == false) {
            url = urlBase + '/Barrier/Update';
        }
        var req = {
            method: 'POST',
            dataType: 'JSON',
            url: url,
            headers: {
                'Content-Type': 'application/json;'
            },
            data: model,
        }
        return $http(req);
    };
    return ReportService;
}]);

VarmalaVivahApp.controller("BarrierController", ['$scope', '$http', '$filter', '$rootScope', 'BarrierService', function ($scope, $http, $filter, $rootScope, BarrierService) {
    $scope.MainBarrierList = [];

    $scope.BarrierList = [];
    $scope.SearchBarrierList = [];

    $scope.Details = true;
    $scope.ErrorModel = { IsSelectPackage: false, IsPrice: false,IsGender:false };
    $scope.ErrorMessage = ""
    $scope.Add = false;
    $scope.Edit = false;
    $scope.PackageId = 0;
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.BarrierModel = {
        Gender : "M",
        PackageName : "",
        Price : 0,
        Validity: 1,
        PackageId:0
    };

    $scope.Prefix = "";

    $scope.AddNewUI = function (isedit) {
        $scope.BarrierModel = {
            Gender: "M",
            PackageName: "",
            Price: 0,
            Validity: 1, PackageId: 0
        };
        $scope.Details = false;
        $scope.Add = true;
        $scope.Edit = false;
    }


    function GetBarriers() {
        BarrierService.GetDetails()
           .success(function (qualifications) {
               $scope.MainBarrierList = qualifications.PackageList;
               $scope.BarrierList = qualifications.PackageList;
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
        //$scope.BarrierList = $filter('filter')(JSON.parse($("#BarrierList").val()), { Barrier: $scope.Prefix })
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.BarrierList = $scope.MainBarrierList.filter(function (actype) {
            return (reg.test(actype.Barrier.toLowerCase()));
        });
        $scope.First();
    }

    $scope.Reset = function () {
        $scope.BarrierList = $scope.MainBarrierList;
        $scope.SearchBarrierList = $scope.BarrierList;
        $scope.First();
    }

    $scope.CancelClick = function () {
        $scope.BarrierModel = {
            Gender: "M",
            PackageName: "",
            Price: 0,
            Validity: 1, PackageId: 0
        };
        $scope.Details = true;
        $scope.Add = false;
        $scope.Edit = false;
    }

    $scope.EditClick = function (BarrierModel) {
        $scope.BarrierModel = BarrierModel;
        $("#ddlGender").val(BarrierModel.Gender);
        $scope.Details = false;
        $scope.Add = false;
        $scope.Edit = true;
    }

    $scope.Save = function (isEdit) {
        if ($scope.BarrierModel.PackageName == "") {
            $scope.ErrorModel.IsSelectPackage = true;
            $scope.ErrorMessage = "Barrier should be filled.";
            return false;
        }
        else {
            $scope.ErrorModel.IsSelectPackage = false;
        }
        if ($scope.BarrierModel.Price == 0) {
            $scope.ErrorModel.IsPrice = true;
            $scope.ErrorMessage = "Package amount should be filled.";
            return false;
        }
        else {
            $scope.ErrorModel.IsPrice = false;
        }
        if ($("#ddlGender").val() == "") {
            $scope.ErrorModel.IsGender = true;
            $scope.ErrorMessage = "Gender should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsGender = false;
        }
        var url = GetVirtualDirectory() + '/Barrier/Save';
        if (isEdit == false) {
            url = GetVirtualDirectory() + '/Barrier/Update';
        }
        var model = $scope.BarrierModel;

        BarrierService.Save(model, isEdit)
           .success(function (qualifications) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Record saved successfully.",
                   Type: "alert",
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $scope.CancelClick();
               GetBarriers();
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

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchBarrierList = $filter('limitTo')($scope.BarrierList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchBarrierList = $filter('limitTo')($scope.BarrierList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.BarrierList.length) {
            $scope.SearchBarrierList = $filter('limitTo')($scope.BarrierList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.BarrierList.length;
        var rem = parseInt($scope.BarrierList.length) % parseInt($scope.Paging);
        var position = $scope.BarrierList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.BarrierList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchBarrierList = $filter('limitTo')($scope.BarrierList, $scope.Paging, position);
    }

    $scope.init = function () {
        GetBarriers();
    }

    $scope.init();

}]);