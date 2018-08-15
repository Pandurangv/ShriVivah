// JavaScript source code

VarmalaVivahApp.factory('MainService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var MainService = {};

    MainService.getEventDetails = function () {
        return $http.get(urlBase + "/EventManagement/GetFutureEvents");
    };
    return MainService;
}]);
VarmalaVivahApp.controller("VendorTypesController", ['$scope', '$http', '$filter', 'MainService', function ($scope, $http, $filter, MainService) {

    $scope.VendorTypes = [];
    $scope.Branding = "";
    $scope.DialogTitle = "Log In";
    $scope.LoginModel = {
        UserName: '',
        Password: ''
    };

    $scope.ErrorModel = {
        IsName: false,
        IsEmail: false,
        IsUserMsg: false,
    };

    $scope.ErrorMessage = "";

    $scope.ValidateName = function ()
    {
        if ($("#Name").val() != "") {
            $scope.ErrorModel.IsName = false;
        }
    }

    $scope.ValidateEmail = function () {
        if ($("#useremail").val() != "") {
            $scope.ErrorModel.IsEmail = false;
        }
    }

    $scope.ValidateUserMessage = function () {
        if ($("#usermessage").val() != "") {
            $scope.ErrorModel.IsUserMsg = false;
        }
    }

    $scope.BindVendorTypes = function ()
    {
        var lst = $("#hdnVendorType").val();
        lst = JSON.parse(lst);
        var url = GetVirtualDirectory();
        angular.forEach(lst, function (key, value) {
            key.TypeImagesPath = url + "/" + key.TypeImagesPath;
            $scope.VendorTypes.push(key);
        })
    }

    function myFunction() {
        var x = document.getElementById("myInput");
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    }

    $scope.ValidateAndLogin = function ()
    {
        if ($scope.LoginModel.$invalid) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $scope.Branding == "SPMO" ? "Mobile no. and Password should be filled." : "लॉग-इन नाव किंवा पासवर्ड रिक्त असू नये.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
        }
        else {
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdivbody").appendChild(spinner.el);
            var url = GetVirtualDirectory();
            var req = {
                method: 'POST',
                url: url + '/account/login',
                headers: {
                    'Content-Type': "application/json"
                },
                data: $scope.LoginModel,
            }

            $http(req).then(function (response)
            {
                if (response.data.Status == true) {
                    if (response.data.DataResponse[0].UserType.toUpperCase() == "USER" || response.data.DataResponse[0].UserType.toUpperCase() == "ADMIN") {
                        window.location = url + "/UserProfile/Index";
                    }
                    else if (response.data.DataResponse[0].UserType.toUpperCase() == "AGENT") {
                        window.location = url + "/Vendor/Index";
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: response.data.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //document.getElementById("contentdivbody").removeChild(spinner.el);
            }, 
            function (response) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: response.data.ErrorMessage,
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                //document.getElementById("contentdivbody").removeChild(spinner.el);
            });
        }
    }

    $scope.lnkForgotPasswordClick = function ()
    {
        $scope.DialogTitle="Forgot Password";
        $("#divLogin").hide();
        $("#forGotPassword").show();
    }

    $scope.UpcomingEvents = "Upcoming Events : ";
    var objdatehelper = new datehelper({ format: "dd/MM/yyyy", cdate: new Date() });
    $scope.HideEvents=false;

    $scope.BindEvents = function ()
    {
        ShowLoader();
        MainService.getEventDetails()
           .success(function (qualifications) {
               HideLoader();
               if (qualifications.EventList.length>0) {
                   $scope.HideEvents = true;
               }
               angular.forEach(qualifications.EventList, function (value, key) {
                   $scope.UpcomingEvents += " ,Event Name : " + value.EventName + ", " + objdatehelper.getFormatteddate($filter('mydate')(value.EventDate), "dd-MM-yyyy") + ", " + value.EventLocation + ", " + value.EventState;
               });
           })
          .error(function (error) {
              HideLoader();
              findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
          });
    }

    $scope.lnkLoginClick = function ()
    {
        $scope.DialogTitle = "Log In";
        $("#divLogin").show();
        $("#forGotPassword").hide();
    }

    $scope.ForgotPassword = function ()
    {
        var userid = $("#UserLoginId").val();
        if (userid == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $scope.Branding == "SPMO" ? "Mobile No. should be filled" : "लॉग-इन नाव रिक्त असू नये.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#usernammelogin").focus();
            return false;
        }

        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var model = { UserName: userid, Password: "" };
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            data: model,
            url: GetVirtualDirectory() + "/Account/ForgotPassword",
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: students.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: students.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    }

    $scope.SaveContacts=function()
    {
        if ($("#Name").val() == "") {
            $scope.ErrorModel.IsName = true;
            $scope.ErrorMessage = "Name should be filled.";
            return false;
        }
        if ($("#useremail").val() == "") {
            $scope.ErrorModel.IsEmail = true;
            $scope.ErrorMessage = "Email should be filled.";
            return false;
        }
        var reg = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        if (reg.test($("#useremail").val()) == false) {
            $scope.ErrorModel.IsEmail = true;
            $scope.ErrorMessage = "Please fill valid mail id.";
            return false;
        }
        if ($("#usermessage").val() == "") {
            $scope.ErrorModel.IsUserMsg = true;
            $scope.ErrorMessage = "Please fill message.";
            return false;
        }
        $scope.ContactUsModel = {
            Name: $("#Name").val(),
            MailId: $("#useremail").val(),
            Description: $("#usermessage").val()
        }
        var url = GetVirtualDirectory();
        var req = {
            method: 'POST',
            url: url + '/home/SaveContactUs',
            headers: {
                'Content-Type': "application/json"
            },
            data: $scope.ContactUsModel,
        }

        $http(req).then(function (response)
        {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $scope.Branding == "SPMO" ? "Your message has been sent. Thank you!" : "तुमचा अभिप्राय आम्हाला भेटला वरमाला विवाह संस्थेकडून तुमचे आभार.",
                Type: "alert",
                OnOKClick: function () {
                    $("#Name").val('');
                    $("#useremail").val('');
                    $("#usermessage").val('');
                },
            });
            objShowCustomAlert.ShowCustomAlertBox();
            
        }, 
        function (response) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $scope.Branding == "SPMO" ? "Thanks for Sending your valuable Suggestion." : "तुमचा अभिप्राय आम्हाला भेटला वरमाला विवाह संस्थेकडून तुमचे आभार.",
                Type: "alert",
                OnOKClick: function () {
                    $("#Name").val('');
                    $("#useremail").val('');
                    $("#usermessage").val('');
                },
            });
            objShowCustomAlert.ShowCustomAlertBox();
        });
    }
    
    $scope.RedirectToVendorList = function (VType)
    {
        var url = GetVirtualDirectory();
        window.location = url + "/Vendor/Vendors?VendorType=" + VType;
    }

    $scope.RedirectToVendor = function ()
    {
        var url = GetVirtualDirectory();
        window.location = url+ "/Vendor/RegisterVendor";
    }
    $scope.init = function () {
        $scope.Branding = $("#branding").val();
        $scope.BindVendorTypes();
        $scope.BindEvents();
        $(document).ready(function () {
            $('#loginPage').on('shown.bs.modal', function () {
                if ($scope.Branding == "SPMO") {
                    $('body').css("padding-right", "0px");
                }
            })
        })
    }
    $scope.init();
}]);