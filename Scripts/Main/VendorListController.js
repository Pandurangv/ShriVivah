VarmalaVivahApp.controller("VendorListController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    $scope.VendorList = [];
    $scope.SelectedVendor = {};
    $scope.VendorType = "all";
    $scope.VendorTypeID = 0;
    $scope.PageNo = 0;
    $scope.BindVendorList = function (IsLoadMore, Nav) {
        var spinner = new Spinner().spin();
        document.getElementById("contentdivbody").appendChild(spinner.el);
        var url = GetVirtualDirectory();
        var vendorTypeId = getParameterByName("VendorTypeId");
        if (vendorTypeId == null) {
            vendorTypeId = 0;
        }
        var vendorType = getParameterByName("VendorType");
        if (vendorType == null) vendorType = "";

        var searchCity = $scope.City == undefined || $scope.City == '---Select---' ? "" : $scope.City;
        var searchText = $scope.prefix == undefined ? "" : $scope.prefix;
        $scope.VendorType = vendorType;
        var IsNext = IsLoadMore;

        if (Nav == "Prev") {
            $scope.PageNo = $scope.PageNo - 1;
            if ($scope.PageNo > 0) {
                $scope.PageNo--;
            }
            $scope.IsPrev = true;
        }
        var queryStr = "?VendorTypeID=" + vendorTypeId + "&VendorType=" + vendorType + "&SearchCity=" + searchCity + "&SearchText=" + searchText + "&PageNo=" + $scope.PageNo;

        var finalurl = $('#branding').val() == 'SINDHI' ? (url + '/vendor/GetSelectedVendorTypesVendors' + queryStr) : (url + '/vendor/GetAllVendorList');

        var req = {
            method: 'GET',
            url: finalurl,
            headers: {
                'Content-Type': "application/json"
            },
        }

        $http(req).then(function (response) {
            if (response.data.Status == true) {
                $scope.VendorList = response.data.DataResponse;
                if (IsLoadMore === undefined) {
                    $scope.CityList = JSON.parse(response.data.CityList);
                    if ($scope.CityList !== undefined) {
                        $scope.CityList.unshift("---Select---");
                        $scope.City = $scope.CityList[0];
                    }
                }
                $scope.PageNo = $scope.PageNo + 1;
            }
            else {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            document.getElementById("contentdivbody").removeChild(spinner.el);
            try {
                var vendortype = getParameterByName("VendorType");
                if (vendortype) {

                }
                var newurl = removeURLParameter(document.location.href, "VendorType");
                var newurl2 = removeURLParameter(document.location.href, "VendorTypeId");
                history.pushState({}, '', newurl);
                history.pushState({}, '', newurl2);
            } catch (e) {
                console.log("not able to remove.");
            }
        },
        function (response) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: response.data.ErrorMessage,
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            document.getElementById("contentdivbody").removeChild(spinner.el);
        });

    }

                        
    $scope.BindVendorListByCity = function () {
        $scope.PageNo = 0;
        $scope.BindVendorList(true);
    }
    
    $scope.BindVendorListByPrefix = function () {
        $scope.PageNo = 0;
        $scope.BindVendorList(true);
    }

    $scope.ShowModel = function (item)
    {
        $scope.SelectedVendor = item;
    }

    $scope.init = function () {
        $scope.BindVendorList();
    }
    $scope.init();
}]);