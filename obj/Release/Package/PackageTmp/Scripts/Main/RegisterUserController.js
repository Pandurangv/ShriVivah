

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
        IsTerms:false,
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
        reg = new RegExp(/^[A-Za-z0-9]+$/);
        if (!reg.test($("#UserName").val())) {
            $scope.ErrorModel.IsUserName = true;
            $scope.ErrorMessage = "User Name should be alphanumeric.";
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
        var spinner = new Spinner().spin();
        document.getElementById("contentdivbody").appendChild(spinner.el);
        $scope.RegisterModel = {
            FirstName: $("#FirstName").val(),
            MiddleName: $("#MiddleName").val(),
            LastName: $("#LastName").val(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            MobileNo: $("#ContactNo").val(),
        };
        var url = GetVirtualDirectory();
        var req = {
            method: 'POST',
            url: url + '/account/RegisterUser',
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
            document.getElementById("contentdivbody").removeChild(spinner.el);
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

    $scope.init = function () {
        
    }
    $scope.init();

}]);

