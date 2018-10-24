VarmalaVivahApp.factory('UserSearchService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var ReportService = {};
    ReportService.getUserDetails = function (model) {
        return $http.post(urlBase + "/userprofile/SearchUsers",model);
    };

    ReportService.expressInterest = function (UserId) {
        return $http.post(urlBase + "/userprofile/ExpressInterest", { UserId: UserId });
    };

    ReportService.getQualifications = function () {
        return $http.get(urlBase + "/Qualification/Education");
    };

    ReportService.MarriageDone = function (UserId) {
        return $http.post(urlBase + "/userprofile/MarriageDone", { UserId: UserId });
    };

    ReportService.Shortlist = function (UserId) {
        return $http.post(urlBase + "/Userprofile/Shortlist", { UserId: UserId });
    };

    ReportService.SendSMS = function (UserId) {
        return $http.post(urlBase + "/PendingUsers/SendSMS", { UserId: UserId });
    };

    ReportService.VerifyAndLogin = function (UserId) {
        return $http.post(urlBase + "/userprofile/VerifyAndLogin", { UserId: UserId });
    };
    ReportService.DeactivateUser = function (UserId) {
        return $http.post(urlBase + "/Agent/ActivateDeactivate", { UserId: UserId, IsActive: false });
    };
    

    return ReportService;
}]);

VarmalaVivahApp.controller("UserSearchController", ['$scope', '$http', '$filter', 'UserSearchService', function ($scope, $http, $filter, UserSearchService) {

    $scope.IsAdmin = $("#IsAdmin").val();
    $scope.UserGender = $("#UserGender").val();
    $scope.PageNo = 1;
    $scope.PageSize = 50;

    $scope.CityList = [];
    $scope.QualificationList =[];
    $scope.HeightList = [];

    $scope.UserList = [];

    $scope.ChangePagination = function () {
        ShowLoader();
        var Gender = $("input:radio[name='Gender']:checked").val();
        var model = { PageNo: parseInt($("#ddlPageNo").val()), PageSize: parseInt($("#ddlPageSize").val()), Gender: Gender };
        UserSearchService.getUserDetails(model)
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.DataResponse;
               if (response.DataResponse.length > 0) {
                   $scope.TotalRecords = response.DataResponse[0].TotalRecords;
               }
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.SearchData = function () {
        ShowLoader();
        var marritalstatus = undefined;
        
        if ($("#rdoUnmarried").prop("checked")) {
            marritalstatus = "1";
        }
        if ($("#rdoWidow").prop("checked")) {
            if (marritalstatus===undefined) {
                marritalstatus = "2";
            }
            else {
                marritalstatus = marritalstatus + ",2";
            }
        }
        if ($("#rdoDivorcee").prop("checked")) {
            if (marritalstatus === undefined) {
                marritalstatus = "3";
            }
            else {
                marritalstatus = marritalstatus + ",3";
            }
        }
        if ($("#rdoSeparated").prop("checked")) {
            if (marritalstatus === undefined) {
                marritalstatus = "4";
            }
            else {
                marritalstatus = marritalstatus + ",4";
            }
        }
        if ($("#rdoAnnulled").prop("checked")) {
            if (marritalstatus === undefined) {
                marritalstatus = "5";
            }
            else {
                marritalstatus = marritalstatus + ",5";
            }
        }

        var JobData = undefined;

        if ($("#rdojob").prop("checked")) {
            JobData = 0;
        }
        if ($("#rdobusiness").prop("checked")) {
            JobData = 1;
        }
        if ($("#rdonotworking").prop("checked")) {
            JobData = 2;
        }
        
        var Gender = $("input:radio[name='Gender']:checked").val();

        
        var model = { PageNo: 1, PageSize: $("#ddlPageSize").val(), Gender: Gender };
        if (marritalstatus!==undefined) {
            model.MarritalStatus = marritalstatus;
        }

        if (JobData !== undefined) {
            model.IsJob = JobData;
        }
        if ($("#ddlMinHeight").val() > 0) {
            model.MinHeightId = $("#ddlMinHeight").val();
        }
        if ($("#ddlMaxHeight").val() > 0) {
            model.MaxHeightId = $("#ddlMaxHeight").val();
        }
        if ($("#ddlCity").val() > 0) {
            model.City = $("#ddlCity").find(":selected").text();
        }
        if ($("#ddlQualification").val() > 0) {
            model.Qualification = $("#ddlQualification").find(":selected").text();
        }
        if ($("#ddlQualification").val() > 0) {
            model.Qualification = $("#ddlQualification").find(":selected").text();
        }
        if ($("#MinAge").val() != "") {
            model.MinAge = $("#MinAge").val();
        }
        if ($("#MaxAge").val() != "") {
            model.MaxAge = $("#MaxAge").val();
        }

        UserSearchService.getUserDetails(model)
           .success(function (response) {
               HideLoader();
               $scope.UserList = response.DataResponse;
               $("#rdojob").prop("checked", false);
               $("#rdobusiness").prop("checked", false);
               $("#rdonotworking").prop("checked", false);

               if (response.DataResponse.length > 0) {
                   $scope.TotalRecords = response.DataResponse[0].TotalRecords;
                   $scope.SetPageNo();
               }
               else {
                   var objShowCustomAlert = new ShowCustomAlert({
                       Title: "",
                       Message: "0 People Match your Search Criteria.",
                       Type: "alert"
                   });
                   objShowCustomAlert.ShowCustomAlertBox();
               }
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.SetPageNo = function ()
    {
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

    $scope.RedirectUser = function (user)
    {
        window.location = GetVirtualDirectory() + "/UserProfile/ShowProfile?ProfileId=" + user.UserId;
    }

    $scope.EditUser = function (user)
    {
        window.location = GetVirtualDirectory() + "/UserProfile/EditUser?UserId=" + user.UserId;
    }

    $scope.SendRequest = function (user) {
        UserSearchService.expressInterest(user.UserId)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Your request has been send.",
                   Type: "alert"
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.ShortlistUser = function (user) {
        UserSearchService.Shortlist(user.UserId)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Your request has been send.",
                   Type: "alert"
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.DeactivateUser = function (user) {
        UserSearchService.DeactivateUser(user.UserId)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Information updated successfully.",
                   Type: "alert",
                   OnOKClick: function () {
                       $scope.SearchData();
                   }
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.GotMarried = function (user) {
        UserSearchService.MarriageDone(user.UserId)
           .success(function (response) {
               HideLoader();
               var objShowCustomAlert = new ShowCustomAlert({
                   Title: "",
                   Message: "Information updated successfully.",
                   Type: "alert",
                   OnOKClick: function () {
                       $scope.SearchData();
                   }
               });
               objShowCustomAlert.ShowCustomAlertBox();
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.TotalRecords=0;

    $scope.GetUserData = function ()
    {
        ShowLoader();
        
        var model = { PageNo: $("#ddlPageNo").val(), PageSize: $("#ddlPageSize").val() };
        

        UserSearchService.getUserDetails(model)
           .success(function (response) {
               HideLoader();
               
               $scope.CityList = response.UserCityList;
               $scope.QualificationList = response.QualificationList;
               $scope.HeightList = response.HeightList;
               $scope.UserList = response.DataResponse;
               if (response.DataResponse.length > 0) {
                   $scope.TotalRecords = response.DataResponse[0].TotalRecords;
                   $scope.SetPageNo();
               }
               $scope.CityList.splice(0, 0, { Id: 0, City: "---Select City---" });
               $scope.QualificationList.splice(0, 0, { Id: 0, Qualification: "---Select Qualification---" });
               $scope.HeightList.splice(0, 0, { HeightId: 0, Height: "---Select Height---" });
               var html = "";
               angular.forEach($scope.CityList, function (value, key) {
                   html += '<option value="' + value.Id + '">' + value.City + '</option>';
               });
               $("#ddlCity").html(html);
               $("#ddlCity").val(0);
               html = "";
               angular.forEach($scope.QualificationList, function (value, key) {
                   html += '<option value="' + value.Id + '">' + value.Qualification + '</option>';
               });
               $("#ddlQualification").html(html);
               $("#ddlQualification").val(0);

               html = "";
               angular.forEach($scope.HeightList, function (value, key) {
                   html += '<option value="' + value.HeightId + '">' + value.Height + '</option>';
               });
               $("#ddlMinHeight").html(html);
               $("#ddlMinHeight").val(0);
               $(window).scrollTop(300);
           }).error(function (error) {
               HideLoader();
               findAndCallErrorBox("", response.Error.Message, "alert", null, null);
           });
    }

    $scope.init = function () {
        $scope.IsAdmin = ($scope.IsAdmin === 'true');
        $("#rdoWidow,#rdoDivorcee,#rdoSeparated,#rdoAnnulled").click(function () {
            if ($(this).prop("checked") == true) {
                if ($("#rdoWidow").prop("checked") == true || $("#rdoDivorcee").prop("checked") == true || $("#rdoSeparated").prop("checked") == true ||
                $("#rdoAnnulled").prop("checked") == true) {
                    $("#divorceeInfo").show();
                }
                else {
                    $("#divorceeInfo").hide();
                }
            }
            else {
                if ($("#rdoWidow").prop("checked") == true || $("#rdoDivorcee").prop("checked") == true || $("#rdoSeparated").prop("checked") == true ||
                $("#rdoAnnulled").prop("checked") == true) {
                    $("#divorceeInfo").show();
                }
                else {
                    $("#divorceeInfo").hide();
                }
            }
        });
        $("#rdoUnmarried").click(function () {
            if ($("#rdoWidow").prop("checked") == true || $("#rdoDivorcee").prop("checked") == true || $("#rdoSeparated").prop("checked") == true ||
                $("#rdoAnnulled").prop("checked") == true) {
                $("#divorceeInfo").show();
            }
            else if ($("#rdoWidow").prop("checked") == false || $("#rdoDivorcee").prop("checked") == false || $("#rdoSeparated").prop("checked") == false ||
                $("#rdoAnnulled").prop("checked") == false) {
                $("#divorceeInfo").hide();
            }
            else {
                $("#divorceeInfo").show();
            }
        });
        
        $("#rdochildYes").change(function () {
            if ($("#rdochildYes").prop("checked") == true) {
                $("#divchildstatus").show();
            }
            else {
                $("#divchildstatus").hide();
            }
        });
        $("#rdochildNo").change(function () {
            if ($("#rdochildNo").prop("checked") == true) {
                $("#divchildstatus").hide();
            }
        });

        $("#ddlPageNo").change(function () {
            $scope.ChangePagination();
        });
        $("#ddlPageSize").change(function () {
            $scope.ChangePagination();
        });

        $("#ddlMinHeight").click(function () {
            if ($(this).val() > 0) {
                html = "";
                var selectedid = parseInt($(this).val());
                angular.forEach($scope.HeightList, function (value, key) {
                    if (selectedid <= value.HeightId) {
                        html += '<option value="' + value.HeightId + '">' + value.Height + '</option>';
                    }
                });
                $("#ddlMaxHeight").html(html);
                //$("#ddlMaxHeight").val($(this).val());
            }
        });



        $scope.GetUserData();
        
    }

    $scope.init();

}]);