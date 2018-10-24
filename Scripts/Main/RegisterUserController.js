

VarmalaVivahApp.controller("RegisterUserController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    
    $scope.ErrorMessage = "";
    $scope.ErrorModel =
    {
        IsFirstName: true,
        IsMiddleName: true,
        IsLastName: true,
        //IsEmail: false,
        IsUserName: true,
        IsPassword: true,
        IsConfirmPassword: true,
        IsContactNo: true,
        IsTerms: true,
        IsGender:true,
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

    //$scope.ValidateEmail = function ()
    //{
    //    if ($("#Email").val() != "") {
    //        $scope.ErrorModel.IsEmail = false;
    //    }
    //}

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
            $scope.ErrorModel.IsContactNo = false;
            $scope.ErrorMessage = "Please fill contact number.";
        }
        if ($("#ContactNo").val().length<10 || $("#ContactNo").val().length>10) {
            $scope.ErrorModel.IsContactNo = true;
            $scope.ErrorMessage = "Invalid contact number.";
            return false;
        }
    }


    $scope.ValidateGender = function () {
        if ($("#ddlGender").val() != "") {
            $scope.ErrorModel.IsGender = false;
        }
    }


    $scope.IsRegistrationSuccess = false;
    $scope.OTP = "";

    $scope.SaveUserFromAdmin = function (model) {
        var reg = new RegExp("[a-zA-Z]");
        if ($("#AgentId").val() == "0") {
            $scope.ErrorModel.IsPanchayatName = true;
            $scope.ErrorMessage = "Please select Panchayat.";
            return false;
        }
        if ($("#FirstName").val() == "") {
            $("#spanFName").show();
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

        //if ($("#Email").val() == "") {
        //    $scope.ErrorModel.IsEmail = true;
        //    $scope.ErrorMessage = "Email should be filled.";
        //    return false;
        //}
        //reg = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        //if (reg.test($("#Email").val()) == false) {
        //    $scope.ErrorModel.IsEmail = true;
        //    $scope.ErrorMessage = "Please fill valid mail id.";
        //    return false;
        //}
        if ($("#UserName").val() == "") {
            $scope.ErrorModel.IsUserName = true;
            $scope.ErrorMessage = "User Name should be filled.";
            return false;
        }
        reg = new RegExp(/^(?![0-9]*$)(?![a-zA-Z]*$)(?![a-zA-Z0-9]*$)[a-zA-Z0-9@#$%&!]+$/g);
        if ($("#branding").val() != "SINDHI") {
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
        else {
            $scope.ErrorModel.IsGender = false;
        }
        if ($("#ContactNo").val() == "") {
            $scope.ErrorModel.IsContactNo = true;
            $scope.ErrorMessage = "Contact No should be filled!";
            return false;
        }
        if ($("#ContactNo").val().length > 10) {
            $scope.ErrorModel.IsContactNo = true;
            $scope.ErrorMessage = "Invalid contact number.";
            return false;
        }
        if ($("#Password").val() == "") {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password should be filled.";
            return false;
        }
        //if (!reg.test($("#Password").val())) {
        //    $scope.ErrorModel.IsPassword = true;
        //    $scope.ErrorMessage = "Password should be numeric with minimum eight digit length";
        //    return false;
        //}

        if ($("#ConfirmPassword").val() == "") {
            $scope.ErrorModel.IsConfirmPassword = true;
            $scope.ErrorMessage = "Confirm Password should be filled.";
            return false;
        }
        
        if ($("#Password").val().toLowerCase() != $("#ConfirmPassword").val().toLowerCase()) {
            $scope.ErrorModel.IsConfirmPassword = true;
            $scope.ErrorMessage = "Password and Confirm Password should be same!";
            return false;
        }
        

        $("#spanvalidate").hide();
        //ShowLoader();
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $scope.RegisterModel = {
            FirstName: $("#FirstName").val().toUpperCase(),
            MiddleName: $("#MiddleName").val().toUpperCase(),
            LastName: $("#LastName").val().toUpperCase(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            MobileNo: $("#ContactNo").val().toUpperCase(),
            Gender: $("#ddlGender").val().toUpperCase(),
            AgentId:$("#AgentId").val()
        };
        var url = GetVirtualDirectory();
        
        url = url + '/UserProfile/RegisterUser';
        var req = {
            method: 'POST',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            data: $scope.RegisterModel,
        }

        $http(req).then(function (response) {
            if (response.data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                    OnOKClick: function () {
                        window.location = GetVirtualDirectory() + "/UserProfile/Index";
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
            document.getElementById("contentdiv").removeChild(spinner.el);
        },
        function (response) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: response.data.ErrorMessage,
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            //var spinner = new Spinner().spin();
            document.getElementById("contentdiv").removeChild(spinner.el);
        });
    }

    $scope.IsDisplayResend = false;

    $scope.Save = function (model)
    {
        var reg = new RegExp("[a-zA-Z]");
        if ($("#AgentId").val() == "0") {
            $scope.ErrorModel.IsPanchayatName = true;
            $scope.ErrorMessage = "Please select Panchayat.";
            return false;
        }
        if ($("#FirstName").val()=="") {
            $("#spanFName").show();
            $("#spanFName").html("First name should be filled.");
            return false;
        }
        else {
            $("#spanFName").hide();
        }
        if ($("#MiddleName").val() == "") {
            $("#spanMName").show();
            $("#spanMName").html("Middle name should be filled.");
            return false;
        }
        else {
            $("#spanMName").show();
        }
        
        if ($("#LastName").val() == "") {
            $("#spanLName").show();
            $("#spanLName").html("Last name should be filled.");
            return false;
        }
        else {
            $("#spanLName").hide();
        }
        
        //if ($("#Email").val() == "") {
        //    $scope.ErrorModel.IsEmail = true;
        //    $scope.ErrorMessage = "Email should be filled.";
        //    return false;
        //}
        if ($("#Email").val() != "") {
            reg = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
            if (reg.test($("#Email").val()) == false) {
                $("#spanEmail").show();
                $("#spanEmail").html("Please fill valid email id.");
                return false;
            }
            else {
                $("#spanEmail").hide();
            }
        }
        
        //reg = new RegExp(/^(?![0-9]*$)(?![a-zA-Z]*$)(?![a-zA-Z0-9]*$)[a-zA-Z0-9@#$%&!]+$/g);
        reg = new RegExp('^[0-9]+$');
        if ($("#branding").val()!="SINDHI") {
            if (!reg.test($("#UserName").val())) {
                $scope.ErrorModel.IsUserName = true;
                $scope.ErrorMessage = "User Name should be alphanumeric.";
                return false;
            }
        }
        
        if ($("#ddlGender").val() == "") {
            $("#spanGender").show();
            $("#spanGender").html("Please select gender.");
            return false;
        }
        else {
            $("#spanGender").hide();
        }
        if ($("#ContactNo").val() == "") {
            $("#spanContactNo").show();
            $("#spanContactNo").html("Please fill contact no.");
            return false;
        }
        else {
            $("#spanContactNo").hide();
        }
        if ($("#ContactNo").val().length != 10) {
            $("#spanContactNo").show();
            $("#spanContactNo").html("contact no should be 10 digit.");
            return false;
        }
        else {
            $("#spanContactNo").hide();
        }
        if ($("#Password").val() == "") {
            $("#spanPassword").show();
            $("#spanPassword").html("Please fill password.");
            return false;
        }
        else {
            $("#spanPassword").hide();
        }
        if ($("#Password").val().length<8) {
            $("#spanPassword").show();
            $("#spanPassword").html("Password length should be greater than 8");
            return false;
        }
        else {
            $("#spanPassword").hide();
        }
        //if (!reg.test($("#Password").val())) {
        //    $scope.ErrorModel.IsPassword = true;
        //    $scope.ErrorMessage = "Password format should be numeric with minimum length 8 digit.";
        //    return false;
        //}
        
        if ($("#ConfirmPassword").val() == "") {
            $("#spanConfirmPassword").show();
            $("#spanConfirmPassword").html("Please fill confirm password");
            return false;
        }
        else {
            $("#spanConfirmPassword").hide();
        }
        
        if ($("#Password").val().toLowerCase() != $("#ConfirmPassword").val().toLowerCase()) {
            $("#spanConfirmPassword").show();
            $("#spanConfirmPassword").html("Password and confirm password should be same.");
            return false;
        }
        else {
            $("#spanConfirmPassword").hide();
        }
        if ($("#termsandconditions").is(':checked') == false) {
            $("#spanTerms").show();
            return false;
        }
        else {
            $("#spanTerms").hide();
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
            AgentId:$("#AgentId").val(),
            Gender: $("#ddlGender").val().toUpperCase()
        };
        if ($scope.IsRegistrationSuccess==false) {
            var url = GetVirtualDirectory();
            if ($("#branding").val() == "SINDHI") {
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
                if ($("#branding").val() == "SINDHI") {
                    $scope.IsRegistrationSuccess = true;
                    $scope.OTP = response.data.ModelObject.OTP;
                    console.log($scope.OTP);
                    $scope.SaveWithOTP();
                    $scope.IsDisplayResend = true
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

    $scope.ResendOTP = function () {
        $scope.IsDisplayResend = false;
        var reg = new RegExp("[a-zA-Z]");
        if ($("#AgentId").val() == "0") {
            $scope.ErrorModel.IsPanchayatName = true;
            $scope.ErrorMessage = "Please select Panchayat.";
            return false;
        }
        if ($("#FirstName").val() == "") {
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

        //if ($("#Email").val() == "") {
        //    $scope.ErrorModel.IsEmail = true;
        //    $scope.ErrorMessage = "Email should be filled.";
        //    return false;
        //}
        //reg = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        //if (reg.test($("#Email").val()) == false) {
        //    $scope.ErrorModel.IsEmail = true;
        //    $scope.ErrorMessage = "Please fill valid mail id.";
        //    return false;
        //}
        if ($("#UserName").val() == "") {
            $scope.ErrorModel.IsUserName = true;
            $scope.ErrorMessage = "User Name should be filled.";
            return false;
        }
        //reg = new RegExp(/^(?![0-9]*$)(?![a-zA-Z]*$)(?![a-zA-Z0-9]*$)[a-zA-Z0-9@#$%&!]+$/g);
        reg = new RegExp('^[0-9]+$');
        if ($("#branding").val() != "SINDHI") {
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
        else {
            $scope.ErrorModel.IsGender = false;
        }
        if ($("#Password").val() == "") {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password should be filled.";
            return false;
        }
        if ($("#Password").val().length <= 8) {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password length should be greater than.";
            return false;
        }
        if (!reg.test($("#Password").val())) {
            $scope.ErrorModel.IsPassword = true;
            $scope.ErrorMessage = "Password format should be numeric with minimum length 8 digit.";
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
        if ($("#ContactNo").val().length > 10) {
            $scope.ErrorModel.IsContactNo = true;
            $scope.ErrorMessage = "Invalid contact number.";
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
            AgentId: $("#AgentId").val(),
            Gender: $("#ddlGender").val().toUpperCase()
        };

        var url = GetVirtualDirectory();
        url = url + '/account/RegisterUserForOTP';
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
            if ($("#branding").val() == "SINDHI") {
                $scope.IsRegistrationSuccess = true;
                $scope.OTP = response.data.ModelObject.OTP;
                console.log($scope.OTP);
                $scope.SaveWithOTP();
                $scope.IsDisplayResend = true;
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

    $scope.SaveWithOTP = function () {
        if ($("#OTP").val() == "") {
            $scope.ErrorModel.IsOTP = true;
            $scope.ErrorMessage = "OTP should be filled!";
            HideLoader();
            return false;
        }
        if ($("#OTP").val().toLowerCase() != $scope.OTP.toLowerCase()) {
            $scope.ErrorModel.IsOTP = true;
            $scope.ErrorMessage = "Invalid OTP!";
            HideLoader();
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
            Gender: $("#ddlGender").val(),
            AgentId: $("#AgentId").val(),
            BehalfOf:$("#BehalfOf").val()
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

