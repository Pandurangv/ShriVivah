VarmalaVivahApp.controller("VendorListController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    $scope.VendorList = [];
    $scope.SelectedVendor = {};
    $scope.VendorType = "all";
    $scope.BindVendorList = function ()
    {
        var spinner = new Spinner().spin();
        document.getElementById("contentdivbody").appendChild(spinner.el);
        var url = GetVirtualDirectory();
        var req = {
            method: 'GET',
            url: url + '/vendor/GetAllVendorList',
            headers: {
                'Content-Type': "application/json"
            },
        }

        $http(req).then(function (response)
        {
            if (response.data.Status==true) {
                $scope.VendorList = response.data.DataResponse;
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
                history.pushState({}, '', newurl);
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
    
    $scope.ShowModel = function (item)
    {
        $scope.SelectedVendor = item;
    }

    $scope.init = function () {
        $scope.BindVendorList();
    }
    $scope.init();
}]);