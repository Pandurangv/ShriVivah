VarmalaVivahApp.factory('UserService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getUserDetails = function () {
        return $http.get(urlBase + "/userprofile/GetUserData");
    };

    ReportService.getQualifications = function () {
        return $http.get(urlBase + "/Qualification/Education");
    };

    ReportService.MarriageDone = function (UserId) {
        return $http.post(urlBase + "/userprofile/MarriageDone", { UserId: UserId });
    };

    ReportService.SendSMS = function (UserId) {
        return $http.post(urlBase + "/PendingUsers/SendSMS", { UserId: UserId });
    };

    return ReportService;
}]);

VarmalaVivahApp.controller("UserController", ['$scope', '$http', '$filter','UserService', function ($scope, $http, $filter,UserService) {

    $scope.IsAdmin = $("#IsAdmin").val();
    $scope.UserGender = $("#UserGender").val();
    $scope.MainUserList = [];

    $scope.UserList = [];

    $scope.SearchUserList = [];

    $scope.RootUrl = GetVirtualDirectory() + "/";
    $scope.Field = "Name";
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

    $scope.CasteList = [];

    $scope.SelectedCaste = {};

    $scope.MinAge = 18;
    $scope.MaxAge = 45;

    $scope.ViewProfile = function (RequestFrom) {
        window.location =$scope.RootUrl + "/UserProfile/ShowProfile?ProfileId=" + RequestFrom;
    }

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchUserList = $filter('limitTo')($scope.UserList, $scope.Paging, 0);
        if ($scope.UserList.length==0) {
            $scope.SearchUserList = $scope.UserList;
        }
        try {
            setTimeout($scope.$apply(), 3000);
        } catch (e) {

        }
    }

    $scope.FilterPageSize = function (PageSize) {
        $scope.Paging = PageSize;
        $scope.First();
    }

    $scope.HideShowInput = function ()
    {
        $scope.Field = $("#Fields").val();
        switch ($scope.Field) {
            case "City":
            case "JobBusinessLocation":
            case "Name":
                $("#txtPrefix").show();
                $("#ddlQ").hide();
                $("#MarritalStatus").hide();
                $("#AgeBetween").hide();
                $("#OrasId").hide();
                $("#BloodGroupId").hide();
                break;
            case "Qualification":
                $("#txtPrefix").hide();
                $("#ddlQ").show();
                $("#MarritalStatus").hide();
                $("#AgeBetween").hide();
                $("#OrasId").hide();
                $("#BloodGroupId").hide();
                break;
            case "MarritalStatus":
                $("#txtPrefix").hide();
                $("#ddlQ").hide();
                $("#MarritalStatus").show();
                $("#AgeBetween").hide();
                $("#OrasId").hide();
                $("#BloodGroupId").hide();
                break;
            case "AgeBetween":
                $("#txtPrefix").hide();
                $("#ddlQ").hide();
                $("#MarritalStatus").hide();
                $("#AgeBetween").show();
                $("#OrasId").hide();
                $("#BloodGroupId").hide();
                break;
            case "BloodGroup":
                $("#txtPrefix").hide();
                $("#ddlQ").hide();
                $("#MarritalStatus").hide();
                $("#AgeBetween").hide();
                $("#OrasId").hide();
                $("#BloodGroupId").show();
                break;
            case "Moonsign":
                $("#txtPrefix").hide();
                $("#ddlQ").hide();
                $("#MarritalStatus").hide();
                $("#AgeBetween").hide();
                $("#OrasId").show();
                $("#BloodGroupId").hide();
                break;
            default:

        }
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
    
    $scope.PrefixCaste = "";

    $scope.MarritalStatus="0";

    $scope.FliterCaste = function () {
        if ($("#ddlCast").val() > 0) {
            var caste = $scope.CasteList.filter(function (actype) { actype.Value === $("#ddlCast").val() });
            if (caste!==undefined) {
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

    

    $scope.FilterListQ = function (Qualification) {
        var regqualification = new RegExp(Qualification.DegreeName.toLowerCase());
        $scope.UserList = $scope.MainUserList.filter(function (actype) {
            return regqualification.test(actype.Qualification==null?"":actype.Qualification.toLowerCase());
        });
        $scope.First();
    }

    $scope.FilterListM = function () {
        var marritalStatus = $("#MarritalStatus").val();
        $scope.UserList = $scope.MainUserList.filter(function (actype) {
            return actype.MarritalStatus==marritalStatus;
        });
        $scope.First();
    }

    $scope.FilterAge = function () {
        if ($scope.MaxAge=="") {
            $scope.MaxAge = 45;
        }
        if ($scope.MinAge == "") {
            $scope.MinAge = 18;
        }
        $scope.UserList = $scope.MainUserList.filter(function (actype) {
            return actype.Age>= $scope.MinAge && actype.Age<=$scope.MaxAge;
        });
        $scope.First();
    }

    $scope.FilterList = function () {
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.UserList = $scope.MainUserList.filter(function (actype) {
            if ($scope.Field == "Name") {
                return ((reg.test(actype.FirstName.toLowerCase())
                || reg.test(actype.MName.toLowerCase())
                || reg.test(actype.LName.toLowerCase()))
                );
            }
            else if ($scope.Field == "City") {
                return reg.test(actype.City==null?"":actype.City.toLowerCase());
            }
            else if ($scope.Field == "JobBusinessLocation") {
                return reg.test(actype.JobLocation == null ? "" : actype.JobLocation.toLowerCase());
            }
        });
        $scope.First();
    }

    $scope.MinAge = "";
    $scope.MaxAge = "";
    $scope.Qualifications = [];
    $scope.SelectedQualification = {};
    $scope.GetUserData = function ()
    {
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
                   $scope.MainUserList = [];
                   angular.forEach(studs, function (value, key) {
                       if (value.Img1 == null) {
                           if (value.Gender == 'M') {
                               value.Img1 = $scope.RootUrl + "Content/Images/user_male.png";
                           }
                           else {
                               value.Img1 = $scope.RootUrl + "Content/Images/user_female.png";
                           }
                       }
                       else {
                           $scope.IsAdmin = ($scope.IsAdmin === 'true');
                           if ($scope.UserGender.toUpperCase() == 'F' || $scope.IsAdmin) {
                               value.Img1 = $scope.RootUrl + value.Img1;
                           }
                           else {
                               value.Img1 = $scope.RootUrl + "Content/Images/user_female.png";
                           }
                       }
                       $scope.MainUserList.push(value);
                   });
                   $scope.UserList = $scope.MainUserList;
                   $scope.First();
                   //$scope.UserList = studs;
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

    $scope.MarriageDone = function (UserId) {
        UserService.MarriageDone(UserId)
           .success(function (result) {
               HideLoader();
               if (result == true) {
                   var objShowCustomAlert = new ShowCustomAlert({
                       Title: "",
                       Message: "Information updated successfully",
                       Type: "alert",
                       OnOKClick: function () {
                           window.location = $scope.RootUrl + "/UserProfile/Index";
                       },
                   });
                   objShowCustomAlert.ShowCustomAlertBox();
               }
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
           });
    }

    $scope.SendSMS = function (UserId) {
        ShowLoader();
        UserService.SendSMS(UserId)
           .success(function (result) {
               HideLoader();
               if (result == true) {
                   var objShowCustomAlert = new ShowCustomAlert({
                       Title: "",
                       Message: "Message send successfully.",
                       Type: "alert",
                   });
                   objShowCustomAlert.ShowCustomAlertBox();
               }
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
           });
    }

    $scope.SendMessage = function (UserId) {
        window.location = GetVirtualDirectory() + '/Message/Index?UserId=' + UserId;
    }

    

    $scope.init = function () {
        $scope.IsAdmin = ($scope.IsAdmin === 'true');
        if ($scope.IsAdmin) {
            $("#btnReg").show();
        }
        else {
            $("#btnReg").hide();
        }
        $scope.HideShowInput();
        
        $scope.Paging = 10;
         $("#myModal").hide();
         $("#btnCloseModel").click(function () {
             $("#myModal").hide();
         });
         $("#PageSizeDown,#PageSizeUp").change(function () {
             $scope.FilterPageSize($(this).val());
             if ($(this).attr("id") == "PageSizeUp") {
                 $("#PageSizeDown").val($(this).val());
             }
             else {
                 $("#PageSizeUp").val($(this).val());
             }
         });
         $("#Fields").change(function () {
             $scope.HideShowInput();
         });
         $("#MarritalStatus").change(function () {
             $scope.FilterListM();
         })
         $("#MarritalStatus").val(0);
         $scope.CasteList = JSON.parse($("#castelist").val());
         var html = "";
         angular.forEach($scope.CasteList, function (value, key) {
             html += "<option value='"+ value.Value +"'>"+ value.Text +"</option>";
         });
         $("#ddlCast").html(html);
         $("#ddlCast").val(0);
         $scope.GetUserData();
    }
    $scope.init();

}]);