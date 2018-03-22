$(document).ready(function () {
    //$().UItoTop({ easingType: 'easeOutQuart' });
    $("#usernammelogin").focus();
    $("#userpassword").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("#btnLogin").trigger("click");
        }
    });
    
    $("#btnfPassword").click(function () {
        var userid = $("#usernammeflogin").val();
        if (userid == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "लॉग-इन नाव रिक्त असू नये.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#usernammelogin").focus();
            return false;
        }

        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var model = { UserName: userid, Password: userpassword };
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
                        Message: "तुमचा पासवर्ड तुमच्या मेल वरती पाठवला आहे, तुमचा नवीन पासवर्ड वापरून लॉगिन करा ",
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
    });

    $("#btnLogin").click(function () {
        
        var userid = $("#usernammelogin").val();
        if (userid=="") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "लॉग-इन नाव रिक्त असू नये.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#usernammelogin").focus();
            return false;
        }
        var userpassword = $("#userpassword").val();
        if (userpassword == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "पासवर्ड रिक्त असू नये.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#userpassword").focus();
            return false;
        }
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var model = { UserName: userid, Password: userpassword };
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            data: model,
            url: GetVirtualDirectory() + "/Account/Login",
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    var data = $("#requeststatus").val();
                    window.location = GetVirtualDirectory() + "/UserProfile/Index";
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
    });



    $("#ReligionId").change(function () {
        var ReligionId = $(this).val();
        if (ReligionId > 0) {
            var spinner = new Spinner().spin();
            document.getElementById("contentdiv").appendChild(spinner.el);
            $.ajax({
                cache: false,
                type: 'POST',
                url: GetVirtualDirectory() + "/Cast/GetCasts",
                data: { ReligionId: ReligionId },
                success: function (data) {
                    var items1 = "";
                    $.each(data, function (i, item) {
                        items1 += "<option value=\"" + item.CastId + "\">" + item.CastName + "</option>";
                    });
                    $("#cmbCast").append(items1);
                    document.getElementById("contentdiv").removeChild(spinner.el);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('Error during process: \n' + xhr.responseText);
                    document.getElementById("contentdiv").removeChild(spinner.el);
                }
            });
        }
    });

    $("#btnSearchUser").click(function () {
        var isSearchPage = document.getElementById("pagename");
        if (isSearchPage!==undefined) {
            isSearchPage = false;
        }
        else {
            isSearchPage = true;
        }
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var ReligionId = $("#ReligionId").val();
        var CastId = $("#cmbCast").val();
        var OrasId = $("#OrasId").val();
        var QualificationId = $("#QualificationId").val();
        var Gender = $("input:radio[name='rdogender']:checked").val();
        $.ajax({
            cache: false,
            type: 'POST',
            url: GetVirtualDirectory() + "/Home/SearchUser",
            data: { ReligionId: ReligionId, CastId: CastId, OrasId: OrasId, QualificationId: QualificationId, Gender: Gender, IsSearchPage: isSearchPage },
            success: function (data) {
                window.location = GetVirtualDirectory() + "/Home/Search";
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
            }
        });
    });

    $("#username").change(function () {
        var spinner = new Spinner().spin();
        document.getElementById("formcontainer").appendChild(spinner.el);
        var username = $("#username").val();
        if (username == "") {
            return false;
        }
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: GetVirtualDirectory() + "/Account/ValidateUserName",
            data: { username: username },
            dataType: "json",
            success: function (response) {
                if (response == true) {
                    $("#username").val("");
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: "लॉग-इन नाव आधीपासून दुसर्या उमेदवार वापरले.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("formcontainer").removeChild(spinner.el);
            }
        });
    });

    $("#btnRegister").click(function () {
        
        var FirstName = $("#FirstName").val();
        var MiddleName = $("#MiddleName").val();
        var LastName = $("#LastName").val();
        var emailid = $("#emailid").val();
        var username = $("#username").val();
        var password = $("#password").val();
        var confirmpassword = $("#confirmpassword").val();
        var contactno = $("#contactno").val();
        if ($("#FirstName").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "नाव भरा",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#FirstName").focus();
            return false;
        }
        if ($("#MiddleName").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "वडिलांचे नाव भरा",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#MiddleName").focus();
            return false;
        }
        if ($("#LastName").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "आडनाव भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#LastName").focus();
            return false;
        }
        if ($("#emailid").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "ई-मेल भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#emailid").focus();
            return false;
        }
        if ($("#username").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "लॉग-इन नाव भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#username").focus();
            return false;
        }
        if ($("#password").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "पासवर्ड भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#password").focus();
            return false;
        }
        if ($("#confirmpassword").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "पासवर्ड पुन्हा  प्रविष्ट करा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#confirmpassword").focus();
            return false;
        }
        if ($("#contactno").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "मोबाईल नंबर भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#contactno").focus();
            return false;
        }
        if (password != $("#confirmpassword").val()) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "पासवर्ड आणि कन्फर्म पासवर्ड समान असणे आवश्यक आहे.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#confirmpassword").focus();
            return false;
        }
        if (contactno.length>10 || contactno.length<10) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "मोबाईल नंबर 10 अंकी असणे आवश्यक आहे.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#contactno").focus();
            return false;
        }
        var spinner = new Spinner().spin();
        document.getElementById("formcontainer").appendChild(spinner.el);
        var model = { FirstName: FirstName, MiddleName: MiddleName, LastName: LastName, Email: emailid, UserName: username, Password: password, MobileNo: contactno };
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetVirtualDirectory() + "/Account/RegisterUser",
            data: model,
            dataType: "json",
            success: function (response) {
                if (response.Status == true) {
                    window.location = GetVirtualDirectory() + "/UserProfile/Index";
                }
                document.getElementById("formcontainer").removeChild(spinner.el);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("formcontainer").removeChild(spinner.el);
            }
        });
    });

    $("#btnVerify").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("formcontainer").appendChild(spinner.el);
        var OTP = $("#OTP").val();
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetVirtualDirectory() + '/Account/VerifyOTP',
            data: {OTP:OTP},
            dataType: "json",
            success: function (response) {
                if (response.Status == true) {
                    window.location = GetVirtualDirectory() + "/UserProfile/Index";
                }
                document.getElementById("formcontainer").removeChild(spinner.el);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("formcontainer").removeChild(spinner.el);
            }
        });
    });

    $("#liLogout").click(function () {
        window.location = GetVirtualDirectory() + '/Account/LogOff';
    });

    $("#btnLoadMore").click(function () {
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/Home/LoadMore',
            dataType: "json",
            success: function (response) {
                if (response.Status==true) {
                    bindUserInfo(response.DataResponse);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: response.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    

    $("#btnSearchVendors").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        if ($("#txtSearchVendor").val() == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "माहिती भरा",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            $("#txtSearchVendor").focus();
            return false;
        }
        $.ajax({
            cache: false,
            type: 'POST',
            url: GetVirtualDirectory() + "/Vendor/VendorsFilter?prefix=" + $("#txtSearchVendor").val(),
            success: function (data) {
                if (data.length==0) {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: "आणखी माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                else {
                    BindVendorInfo(data);
                    document.getElementById("contentdiv").removeChild(spinner.el);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#txtSearchVendor").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("#btnSearchVendors").trigger("click");
        }
    });

});

function BindVendorInfo(data)
{
    $("#datatbody").html();
    var html = "";
    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        html += '<tr><td><input id="' + item.VendorId + '" value="' + item + '" type="hidden"><div class="col-md-3">';

        //html += '<div class="region-grid" id="' + data[i].VendorId + '"><div class="state-name">';
        if (item.LogoImage != null) {
            html += '<img src="~/' + item.LogoImage + '" class="profileImg" />';
        }
        if (item.ProfileImage != null && item.LogoImage == null) {
            html += '<img src="~/' + item.ProfileImage + '" class="profileImg" />';
        }
        if (item.ProfileImage == null && item.LogoImage == null) {
            html += '<img src="../Content/FrontUI/images/Shake_hand.jpg" class="profileImg" />';
        }
        html += '</div></td><td><div class="col-md-6 text-left">';
        html += item.VendorName + ',' + item.OwnerName + '<br />';
        html += item.Address + ',' + item.City + ',<br /></div></td><td><div class="col-md-3">';
        if (item.IsActive) {
            html += '<div class="btn btn-primary" data-toggle="modal" data-target="#myModal" onclick="DisplayVendorProfile(' + item.VendorId +')">प्रोफाइल पहा</div>';
        }
        html += '</div></td></tr>';
    }
    $("#datatbody").html(html);
}

function BindVendorTypes()
{
    var lst = $("#hdnVendorType").val();
    lst = JSON.parse(lst);
    $("#flexiselDemo1").html("");
    var html = "";
    var url = GetVirtualDirectory();
    $.each(lst, function (i, item) {
        html += "<li><a href='/Vendor/Vendors'><img src='" + item.TypeImagesPath + "' alt='' /></a><div class='slide-title'><h4>आपला व्यवसाय वाढवण्यासाठी येथे नोंदणी करा.</h4> </div><div class='date-city'><div class='buy-tickets'><a href='/Vendor/RegisterVendor'>नोंदणी करा</a></div></div></li>";
    });
    $("#flexiselDemo1").html(html);
}

function bindUserInfo(users)
{
    $("#Container").html("");
    var html = "";
    for (var i = 0; i < users.length; i++) {
        var gender = users[i].Gender;
        var ReligionName = users[i].ReligionName;
        var Cast = users[i].CastName;
        var Qualification = users[i].Qualification;
        var Height = users[i].Height;
        var FirstName = users[i].FirstName;
        var LastName = users[i].LastName;
        var Age = users[i].Age;

        var MarritalStatus = users[i].MarritalStatus;
        var status = "";
        switch ( parseInt(MarritalStatus)) {
            case 1:
                status = "Unmarried";
                break
            case 2:
                status = "Widow";
                break
            case 3:
                status = "Divorcee";
                break
            case 4:
                status = "Separated";
                break;

        }
        if (gender != "" && ReligionName != "" && Cast != "" && Qualification != "" && Height != "") {
            html += '<div class="row" style="margin-top:10px;margin-bottom:10px;"><div class="col-md-2" id="' + users[i].UserId + '" data="' + gender + '">';
            //html += "<div class='region-grid' id='" + Cast + "' data='" + gender + "'><div class='state-name'>";
            if (gender=="M") {
                html += "<img src='/Content/Images/user_male.png' /></div>"
            }
            else {
                html += "<img src='/Content/Images/user_female.png' /></div>"
            }
            html+='<div class="col-md-6 text-left">';
            //html += "<div class='btn profileDiv'>";
            if ($("#chkIsLogin").val()==true) {
                html += FirstName + " " + LastName;
            }
            html += ReligionName + "," + Cast + "," + Qualification + ",<span>" + status + "</span> वय: " + Age + ", उंची :" + Height;
            html += "</div>"
            html += '<div class="col-md-4"><div class="btn btn-primary" onclick="RequestToUser(' + users[i].UserId + ');" data="' + users[i].UserId + '">प्रोफाइल पहा</div></div></div>';
            //html += "</div><div class='btn btn-primary requestButton' onclick='RequestToUser(" + users[i].UserId + ");' data='" + users[i].UserId + "'>View Profile</div></div>"
        }
    }

    $("#Container").html(html);
}

var Lansettings = [];

function SetLanguage(lantype)
{
    if (Lansettings!==undefined && Lansettings.length==0) {
        GetLanguageConfig(lantype);
    }
    for (var i = 0; i < Lansettings.length; i++) {
        switch (Lansettings[i].MenuType) {
            case "mnulogin":
                if (lantype == Lansettings[i].Lan) {
                    $("#btnUserLogin").html(Lansettings[i].Alias);
                }
                if (lantype == "mar") {
                    $("#btnUserLogin").addClass("marathifont");
                }
                else {
                    $("#btnUserLogin").removeClass("marathifont");
                }
                break;
            case "mnuhome":
                if (lantype == Lansettings[i].Lan) {
                    $("#lihome>a").html(Lansettings[i].Alias);
                }
                if (lantype == "mar") {
                    $("#lihome").addClass("marathifont");
                }
                else {
                    $("#lihome").removeClass("marathifont");
                }
                break;
            case "mnusearch":
                if (lantype == Lansettings[i].Lan) {
                    $("#liSearch>a").html(Lansettings[i].Alias);
                    $("#lisearch1>a").html(Lansettings[i].Alias);
                }
                if (lantype == "mar") {
                    $("#liSearch>a").addClass("marathifont");
                    $("#lisearch1>a").addClass("marathifont");
                }
                else {
                    $("#liSearch>a").removeClass("marathifont");
                    $("#lisearch1>a").removeClass("marathifont"); //lireg
                }
                break;
            case "mnuregister":
                if (lantype == Lansettings[i].Lan) {
                    $("#lireg>a").html(Lansettings[i].Alias);
                }
                if (lantype == "mar") {
                    $("#lireg>a").addClass("marathifont");
                }
                else {
                    $("#lireg>a").removeClass("marathifont");
                }
                break;
        }
    }
    
}

function GetLanguageConfig(lantype)
{
    $.ajax({
        cache: false,
        type: "Get",
        async: false,
        url: GetVirtualDirectory() + '/Settings/GetMaratiTagPrefix',
        dataType: "json",
        success: function (response) {
            Lansettings = response.menuprefix;
            if (lantype!==undefined) {
                SetLanguage(lantype);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}



function SendProfileRequest(userid)
{
    window.location = GetVirtualDirectory() + "/UserProfile/Index";
}



