﻿

VarmalaVivahApp.controller("RegisterUserController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    
    $scope.ErrorMessage = "";
    $scope.ErrorModel =
    {
        IsFirstName: false,
        IsMiddleName: false,
        IsLastName: false,
        IsEmail: false,
        IsUserName: false,
        IsPassword: false,
        IsConfirmPassword: false,
        IsContactNo: false,
        IsTerms: false,
        IsGender:false,
    };

    $scope.RegisterModel = { FirstName: "", MiddleName: "", LastName: "", Email: "", UserName: "", Password: "", ConfirmPassword: "", ContactNo: "" };

    $scope.ValidateFName = function ()
    {
        if ($("#FirstName").val() != "") {
            $scope.ErrorModel.IsFirstName = false;
        }
    }

    $scope.ValidateMName = function () {
        if ($("#MiddleName").val() != "") {
            $scope.ErrorModel.IsMiddleName = false;
        }
    }

    $scope.ValidateLName = function () {
        if ($("#LastName").val() != "") {
            $scope.ErrorModel.IsLastName = false;
        }
    }

    $scope.ValidateEmail = function ()
    {
        if ($("#Email").val() != "") {
            $scope.ErrorModel.IsEmail = false;
        }
    }

    $scope.ValidateUserName = function ()
    {
        if ($("#UserName").val() != "") {
            $scope.ErrorModel.IsUserName = false;
        }
    }

    $scope.ValidatePassword = function ()
    {
        if ($("#Password").val() != "") {
            $scope.ErrorModel.IsPassword = false;
        }
    }

    $scope.ValidateConfirmPassword = function ()
    {
        if ($("#ConfirmPassword").val() != "") {
            $scope.ErrorModel.IsConfirmPassword = false;
        }
        if ($("#Password").val().toLowerCase() == $("#ConfirmPassword").val().toLowerCase()) {
            $scope.ErrorModel.IsConfirmPassword = false;
        }
    }

    $scope.ValidateContactNo = function ()
    {
        if ($("#ContactNo").val() != "") {
            $scope.ErrorModel.IsFirstName = false;
        }
    }

    $scope.IsRegistrationSuccess = false;
    $scope.OTP = "";

    $scope.Save = function (model)
    {
        var reg = new RegExp("[a-zA-Z]");
        if ($("#FirstName").val()=="") {
            $scope.ErrorModel.IsFirstName = true;
            $scope.ErrorMessage = "First name should be filled.";
            return false;
        }
        
        if ($("#MiddleName").val() == "") {
            $scope.ErrorModel.IsMiddleName = true;
            $scope.ErrorMessage = "Middle name should be filled.";
            return false;
        }
        
        if ($("#LastName").val() == "") {
            $scope.ErrorModel.IsLastName = true;
            $scope.ErrorMessage = "Last name should be filled.";
            return false;
        }
        
        if ($("#Email").val() == "") {
            $scope.ErrorModel.IsEmail = true;
            $scope.ErrorMessage = "Email should be filled.";
            return false;
        }
        reg = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        if (reg.test($("#Email").val()) == false) {
            $scope.ErrorModel.IsEmail = true;
            $scope.ErrorMessage = "Please fill valid mail id.";
            return false;
        }
        if ($("#UserName").val() == "") {
            $scope.ErrorModel.IsUserName = true;
            $scope.ErrorMessage = "User Name should be filled.";
            return false;
        }
        reg = new RegExp(/^(?![0-9]*$)(?![a-zA-Z]*$)[a-zA-Z0-9]+$/g);
        if ($("#branding").val()!="SPMO") {
            if (!reg.test($("#UserName").val())) {
                $scope.ErrorModel.IsUserName = true;
                $scope.ErrorMessage = "User Name should be alphanumeric.";
                return false;
            }
        }
        
        if ($("#ddlGender").val() == "") {
            $scope.ErrorModel.IsGender = true;
            $scope.ErrorMessage = "Please select gender.";
            return false;
        }
        if ($("#Password").val() == "") {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password should be filled.";
            return false;
        }
        if (!reg.test($("#Password").val())) {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password should be alphanumeric.";
            return false;
        }
        
        if ($("#ConfirmPassword").val() == "") {
            $scope.ErrorModel.IsConfirmPassword = true;
            $scope.ErrorMessage = "Confirm Password should be filled.";
            return false;
        }
        if ($("#ContactNo").val() == "") {
            $scope.ErrorModel.IsContactNo = true;
            $scope.ErrorMessage = "Contact No should be filled!";
            return false;
        }
        if ($("#Password").val().toLowerCase() != $("#ConfirmPassword").val().toLowerCase()) {
            $scope.ErrorModel.IsConfirmPassword = true;
            $scope.ErrorMessage = "Password and Confirm Password should be same!";
            return false;
        }
        if ($("#termsandconditions").is(':checked') == false) {
            $scope.ErrorModel.IsTerms = true;
            $scope.ErrorMessage = "Please select terms and conditions.";
            return false;
        }

        $("#spanvalidate").hide();
        ShowLoader();
        $scope.RegisterModel = {
            FirstName: $("#FirstName").val().toUpperCase(),
            MiddleName: $("#MiddleName").val().toUpperCase(),
            LastName: $("#LastName").val().toUpperCase(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            MobileNo: $("#ContactNo").val().toUpperCase(),
            Gender: $("#ddlGender").val().toUpperCase()
        };
        if ($scope.IsRegistrationSuccess==false) {
            var url = GetVirtualDirectory();
            if ($("#branding").val() == "SPMO") {
                url = url + '/account/RegisterUserForOTP';
            }
            else {
                url = url + '/account/RegisterUser';
            }
            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': "application/json"
                },
                data: $scope.RegisterModel,
            }

            $http(req).then(function (response) {
                HideLoader();
                if ($("#branding").val() == "SPMO") {
                    $scope.IsRegistrationSuccess = true;
                    $scope.OTP = response.data.ModelObject.OTP;
                    $scope.SaveWithOTP();
                }
                else {
                    if (response.data.Status == true) {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: response.data.ErrorMessage,
                            Type: "alert",
                            OnOKClick: function () {
                                window.location = url + "/UserProfile/Index";
                            },
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                    else {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: response.data.ErrorMessage,
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
            },
            function (response) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                HideLoader();
            });
        }
        else {
            $scope.SaveWithOTP();
        }
    }

    $scope.SaveWithOTP = function () {
        if ($("#OTP").val() == "") {
            $scope.ErrorModel.IsOTP = true;
            $scope.ErrorMessage = "OTP should be filled!";
            return false;
        }
        if ($("#OTP").val().toLowerCase() != $scope.OTP.toLowerCase()) {
            $scope.ErrorModel.IsOTP = true;
            $scope.ErrorMessage = "Invalid OTP!";
            return false;
        }
        ShowLoader();
        var url = GetVirtualDirectory();
        $scope.RegisterModel = {
            FirstName: $("#FirstName").val(),
            MiddleName: $("#MiddleName").val(),
            LastName: $("#LastName").val(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            MobileNo: $("#ContactNo").val(),
            Gender: $("#ddlGender").val()
        };
        var newurl = url + '/account/RegisterUser';
        var req = {
            method: 'POST',
            url: newurl,
            headers: {
                'Content-Type': "application/json"
            },
            data: $scope.RegisterModel,
        }

        $http(req).then(function (response) {
            HideLoader();
            if (response.data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                    OnOKClick: function () {
                        window.location = url + "/UserProfile/Index";
                    },
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            else {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
        },
        function (response) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: response.data.ErrorMessage,
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            HideLoader();
        });
    }

    $scope.init = function () {
        
    }
    $scope.init();

}]);

