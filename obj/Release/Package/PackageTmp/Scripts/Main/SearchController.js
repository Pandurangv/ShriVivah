﻿VarmalaVivahApp.factory('UserService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getUserDetails = function () {
        return $http.get(urlBase + "/userprofile/GetUsers");
    };

    ReportService.getQualifications = function () {
        return $http.get(urlBase + "/Qualification/Education");
    };

    return ReportService;
}]);

VarmalaVivahApp.controller("SearchController", ['$scope', '$http', '$filter', 'UserService', function ($scope, $http, $filter, UserService) {

    $scope.IsAdmin = $("#IsAdmin").val();

    $scope.IsLogin = false;

    $scope.MainUserList = [];

    $scope.UserList = [];

    $scope.SearchUserList = [];

    $scope.RootUrl = GetVirtualDirectory() + "/";

    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

    $scope.CasteList = [];

    $scope.SelectedCaste = {};

    $scope.ViewProfile = function (RequestFrom) {
        window.location = $scope.RootUrl + "/UserProfile/ShowProfile?ProfileId=" + RequestFrom;
    }

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
    $scope.Prefix = "";

    $scope.PrefixQualification = "";

    $scope.FliterQualification = function (quali) {
        if (quali.QualificationId > 0) {
            $scope.PrefixQualification = quali.DegreeName;
        }
        else {
            $scope.PrefixQualification = "";
        }
        $scope.FilterList();
    }

    $scope.PrefixCaste = "";

    $scope.MarritalStatus = "0";

    $scope.FliterCaste = function () {
        if ($("#ddlCast").val() > 0) {
            var caste = $scope.CasteList.filter(function (actype) { actype.Value === $("#ddlCast").val() });
            if (caste !== undefined) {
                $scope.PrefixCaste = caste.Text;
            }
            else {
                $scope.PrefixCaste = "";
            }
        }
        else {
            $scope.PrefixCaste = "";
        }
        $scope.FilterList();
    }

    $scope.FilterMarriage = function () {
        if ($("#MarritalStatus").val() > 0) {
            $scope.MarritalStatus = $("#MarritalStatus").val();
        }
        else {
            $scope.MarritalStatus = "0";
        }
        $scope.FilterList();
    }

    $scope.FilterList = function () {
        var reg = new RegExp($scope.Prefix.toLowerCase());
        var regqualification = new RegExp($scope.PrefixQualification.toLowerCase());
        var regcaste = new RegExp($scope.PrefixCaste.toLowerCase());

        if ($scope.MinAge == "") {
            $scope.MinAge = 18;
        }
        if ($scope.MaxAge == "") {
            $scope.MaxAge = 45;
        }
        $scope.UserList = $scope.MainUserList.filter(function (actype) {
            if ($scope.Prefix != "" && $scope.PrefixQualification != "" && $scope.PrefixCaste != "" && $scope.MarritalStatus != "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
                || reg.test(actype.MName.toLowerCase())
                || reg.test(actype.LName.toLowerCase()))
                && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
                && regqualification.test(actype.Qualification.toLowerCase())
                && actype.MarritalStatus == $scope.MarritalStatus
                && regcaste.test(actype.CastName.toLowerCase()));
            }
            else if ($scope.Prefix == "" && $scope.PrefixQualification != "" && $scope.PrefixCaste != "" && $scope.MarritalStatus != "0") {
                return ((actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
               && regqualification.test(actype.Qualification.toLowerCase())
                    && actype.MarritalStatus == $scope.MarritalStatus
               && regcaste.test(actype.CastName.toLowerCase()));
            }
            else if ($scope.Prefix != "" && $scope.PrefixQualification == "" && $scope.PrefixCaste != "" && $scope.MarritalStatus != "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
               || reg.test(actype.MName.toLowerCase())
               || reg.test(actype.LName.toLowerCase()))
               && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
               && actype.MarritalStatus == $scope.MarritalStatus
               && regcaste.test(actype.CastName.toLowerCase()));
            }
            else if ($scope.Prefix != "" && $scope.PrefixQualification != "" && $scope.PrefixCaste != "" && $scope.MarritalStatus != "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
               || reg.test(actype.MName.toLowerCase())
               || reg.test(actype.LName.toLowerCase())
               || reg.test(actype.CastName.toLowerCase())
                    )
               && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
               && actype.MarritalStatus == $scope.MarritalStatus
               && regqualification.test(actype.Qualification.toLowerCase()));
            }
            else if ($scope.Prefix != "" && $scope.PrefixQualification == "" && $scope.PrefixCaste == "" && $scope.MarritalStatus != "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
               || reg.test(actype.MName.toLowerCase())
               || reg.test(actype.LName.toLowerCase())
               || reg.test(actype.CastName.toLowerCase())
               || reg.test(actype.Qualification.toLowerCase())
                    )
               && actype.MarritalStatus == $scope.MarritalStatus
               && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge));
            }
            else if ($scope.Prefix == "" && $scope.PrefixQualification != "" && $scope.PrefixCaste == "" && $scope.MarritalStatus != "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
                || reg.test(actype.MName.toLowerCase())
                || reg.test(actype.LName.toLowerCase())
                || reg.test(actype.CastName.toLowerCase())
                || reg.test(actype.Qualification.toLowerCase()))
                && regqualification.test(actype.Qualification.toLowerCase())
                && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge));
            }
            else if ($scope.Prefix == "" && $scope.PrefixQualification == "" && $scope.PrefixCaste != "" && $scope.MarritalStatus != "0") {
                return ((actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
                && actype.MarritalStatus == $scope.MarritalStatus
               && regcaste.test(actype.CastName.toLowerCase()));
            }
            else if ($scope.Prefix != "" && $scope.PrefixQualification == "" && $scope.PrefixCaste == "" && $scope.MarritalStatus == "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
                || reg.test(actype.MName.toLowerCase())
                || reg.test(actype.LName.toLowerCase())
                || reg.test(actype.CastName.toLowerCase())
                || reg.test(actype.Qualification.toLowerCase()))
                && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge));
            }
            else if ($scope.Prefix != "" && $scope.PrefixQualification == "" && $scope.PrefixCaste != "" && $scope.MarritalStatus == "0") {
                return ((reg.test(actype.FirstName.toLowerCase())
                || reg.test(actype.MName.toLowerCase())
                || reg.test(actype.LName.toLowerCase())
                || reg.test(actype.CastName.toLowerCase())
                || reg.test(actype.Qualification.toLowerCase()))
                && regcaste.test(actype.CastName.toLowerCase())
                && (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge));
            }
            else if ($scope.Prefix == "" && $scope.PrefixQualification == "" && $scope.PrefixCaste == "" && $scope.MarritalStatus != "0") {
                return ((actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge)
                && actype.MarritalStatus == $scope.MarritalStatus);
            }
            else {
                return (actype.Age >= $scope.MinAge && actype.Age <= $scope.MaxAge);
            }
        });
        $scope.First();
    }

    $scope.MinAge = "";
    $scope.MaxAge = "";
    $scope.Qualifications = [];
    $scope.SelectedQualification = {};
    $scope.GetUserData = function () {
        ShowLoader();
        UserService.getQualifications()
           .success(function (qualifications) {
               var edu = { DegreeName: "---Select---", QualificationId: 0 };
               qualifications.splice(0, 0, edu);
               $scope.Qualifications = qualifications;
               $scope.SelectedQualification = edu;
               UserService.getUserDetails()
               .success(function (studs) {
                   HideLoader();
                   $scope.MainUserList=studs;
                   //angular.forEach(studs, function (value, key) {
                   //    if (value.Img1 == null) {
                   //        if (value.Gender == 'M') {
                   //            value.Img1 = $scope.RootUrl + "Content/Images/user_male.png";
                   //        }
                   //        else {
                   //            value.Img1 = $scope.RootUrl + "Content/Images/user_female.png";
                   //        }
                   //    }
                   //    else {
                   //        value.Img1 = $scope.RootUrl + value.Img1;
                   //    }
                   //    $scope.MainUserList.push(value);
                   //});
                   $scope.UserList = $scope.MainUserList;
                   $scope.First();
               }).error(function (error) {
                   HideLoader();
                   findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
               });
               //$scope.UserList = studs;
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
           });

    }




    $scope.init = function () {
        $("#myModal").hide();
        $("#btnCloseModel").click(function () {
            $("#myModal").hide();
        });

        $scope.IsLogin= $("#chkIsLogin").val();

        $("#MarritalStatus").val(0);
        $scope.CasteList = JSON.parse($("#castelist").val());
        var html = "";
        angular.forEach($scope.CasteList, function (value, key) {
            html += "<option value='" + value.Value + "'>" + value.Text + "</option>";
        });
        $("#ddlCast").html(html);
        $("#ddlCast").val(0);
        $scope.GetUserData();
    }
    $scope.init();

}]);