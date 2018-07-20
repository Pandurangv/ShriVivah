var step = 0;
$(document).ready(function () {
    var startdt = new Date();
    var mindate = startdt.getDate().toString() + "/" + (startdt.getMonth() + 1) + "/" + (startdt.getFullYear() - 60).toString();
    var maxdate = startdt.getDate().toString() + "/" + (startdt.getMonth() + 1) + "/" + (startdt.getFullYear() - 22).toString();
    Step0();
    //$("#divUserProfile").hide();
    $("#myModal").hide();
    $("#btnCloseModel").click(function () {
        $("#myModal").hide();
    });
    $("#btnChangePassword").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: "../UserProfile/UpdatePassword",
            dataType: "json",
            data: { Old: $("#txtOldPassword").val(), NewPassword: $("#txtNewPassword").val() },
            success: function (students) {
                if (students.Status == true) {
                    window.location = GetVirtualDirectory() + "/UserProfile/Index";
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                //var spinner = new Spinner().spin();
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });
    $("#profileImage,#btnShowPopup").click(function () {
        $("#myModal").show();
    });
    $("#divUserExpectation").hide();

    $("#txtSearch").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("#btnSearch").trigger("click");
        }
    });

    $("#btnReset").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: "../UserProfile/Reset",
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    if (students.DataResponse.length > 0) {
                        bindUserSearchResult(students);
                    }
                    else {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                //var spinner = new Spinner().spin();
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnASearch").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var CastId = $("#CasteId").val() == null ? 0 : $("#CasteId").val();
        var EHeightId = $("#EHeightId").val() == null ? 0 : $("#EHeightId").val();
        var HeightId = $("#HeightId").val() == null ? 0 : $("#HeightId").val();
        var QualificationId = $("#QualificationId").val();
        var Income = $("#cmbIncome").val() == null ? 0 : $("#cmbIncome").val();
        var url = "../UserProfile/GetUserDetailsByParam?MinAge=" + $("#ageFrom").val() + "&MaxAge=" + $("#ageTo").val() + "&CastId=" + CastId + "&MaxHeightId=" + EHeightId + "&MinHeightId=" + HeightId + "&QualificationId=" + QualificationId + "&JobLocation=" + $("#City").val() + "&Income=" + Income;
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: url,
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    if (students.DataResponse.length > 0) {
                        bindUserSearchResult(students);
                    }
                    else {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
                $("#ageFrom").val("");
                $("#ageTo").val("");
                $("#EHeightId").val("0");
                $("#QualificationId").val("0");
                $("#HeightId").val("0");
                $("#City").val("");
                $("#CasteId").val("");
            },
            error: function (data) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
        $("#btnCloseModel").trigger("click");
    })

    $("#btnSearch").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var prefix = $("#txtSearch").val();
        if (prefix == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "शोध माहिती भरा!",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            return false;
        }
        var url = GetVirtualDirectory();
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            data: { prefix: prefix },
            url: url + "/UserProfile/GetUserDetails",
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    if (students.DataResponse.length>0) {
                        bindUserSearchResult(students);
                    }
                    else {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Information not available" : "माहिती उपलब्ध नाही",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //var spinner = new Spinner().spin();
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                //var spinner = new Spinner().spin();
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    })

    //var maxdate = dtcurrent.setDate(dtcurrent.getFullYear() - 22);
    $("#DOB").attr("min", mindate);
    $("#DOB").attr("max", maxdate);
    $("#ReligionId").change(function () {
        $('#CasteId').empty();
        var ReligionId = $(this).val();
        if (ReligionId > 0) {
            var spinner = new Spinner().spin();
            document.getElementById("contentdiv").appendChild(spinner.el);
            $.ajax({
                cache: false,
                type: 'POST',
                url: "../Cast/GetCasts",
                data: { ReligionId: ReligionId },
                success: function (data) {
                    var items1 = "";
                    if ($("#hdnbranding").val()=="SPMO") {
                        items1 += "<option value=\"0\">---Select Cast---</option>";
                    }
                    else {
                        items1 += "<option value=\"0\">---जात निवडा---</option>";
                    }
                    $.each(data, function (i, item) {
                        items1 += "<option value=\"" + item.CastId + "\">" + item.CastName + "</option>";
                    });
                    $("#CasteId").append(items1);
                    document.getElementById("contentdiv").removeChild(spinner.el);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('Error during process: \n' + xhr.responseText);
                    document.getElementById("contentdiv").removeChild(spinner.el);
                }
            });
        }
    });


    $("#noofchilds").attr("disabled", "disabled");
    $("#rdochildLWM").attr("disabled", "disabled");
    $("#rdochildNLWM").attr("disabled", "disabled");
    //$("#txtCompanyName").attr("disabled", "disabled");
    $("#disabledesc").attr("disabled","disabled");
    $("#rdoJob").click(function () {
        //$("#txtCompanyName").attr("disabled", "disabled");
    });
    $("#rdoBusiness").click(function () {
        $("#txtCompanyName").removeAttr("disabled", "disabled");
    });
    $("#rdoWidow,#rdoDivorcee,#rdoSeparated").click(function () {
        $("#noofchilds").removeAttr("disabled");
        $("#rdochildLWM").removeAttr("disabled");
        $("#rdochildNLWM").removeAttr("disabled");
    });
    $("#rdoUnmarried").click(function () {
        $("#noofchilds").val("");
        $("#noofchilds").attr("disabled", "disabled");
        $("#rdochildLWM").attr("disabled", "disabled");
        $("#rdochildNLWM").attr("disabled", "disabled");
    });

    var relatives = [];

    $("#btnAddFamilyDetails,#btnAddRelativeDetails").click(function () {
        var btnid = $(this).attr("id");
        var relativedetails="";
        if (btnid == "btnAddFamilyDetails") {
            relativedetails = { RelativeName: $("#txtUncleName").val(), RelativeAddress: $("#txtUncleAddress").val(), Relation:$("#hdnbranding").val()=="SPMO"? "Uncle": "काका", MobileNo: $("#txtUContactNo").val() };
            $("#txtUncleName").val("");
            $("#txtUncleAddress").val("");
            $("#txtUContactNo").val("");
        }
        else {
            var cmbRelations = $("#cmbRelations").val();
            relativedetails = { RelativeName: $("#txtRelativeName").val(), RelativeAddress: $("#txtRelativeAddress").val(), Relation: cmbRelations, MobileNo: $("#txtRContactNo").val() };
            $("#txtRelativeName").val("");
            $("#txtRelativeAddress").val("");
            $("#txtRContactNo").val("");
        }
        relatives.push(relativedetails);
    });

    $("#rdoIsHandicapped").click(function () {
        if ($("#rdoIsHandicapped").checked==true) {
            $("#disabledesc").removeAttr("disabled");
        }
        else {
            $("#disabledesc").attr("disabled","disabled");
        }
    });

    $("#btnCancel").click(function () {
        if (step==1) {
            Step0();
            step = 0;
        }
        if (Step==2) {
            Step1();
            step = 1;
        }
        if (Step == 3) {
            Step2();
            step = 2;
        }
        if (Step == 4) {
            Step3();
            step = 3;
        }
        if (Step==5) {
            Step4();
            step = 4;
        }
    });

    $("#btnLoadMore").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: 'GET',
            url: "../UserProfile/UserNext",
            success: function (data) {
                document.getElementById("contentdiv").removeChild(spinner.el);
                if (data.DataResponse!=null && data.DataResponse.length > 0) {
                    bindUserSearchResult(data);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: data.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnGoBack").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoBack',
            dataType: "json",
            success: function (response) {
                document.getElementById("contentdiv").removeChild(spinner.el);
                if (response.DataResponse != null && response.DataResponse.length > 0) {
                    bindUserSearchResult(response);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: response.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnGoLast").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoLast',
            dataType: "json",
            success: function (response) {
                document.getElementById("contentdiv").removeChild(spinner.el);
                if (response.DataResponse != null && response.DataResponse.length > 0) {
                    bindUserSearchResult(response);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: response.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnGoFirst").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoFirst',
            dataType: "json",
            success: function (response) {
                document.getElementById("contentdiv").removeChild(spinner.el);
                if (response.DataResponse != null && response.DataResponse.length > 0) {
                    bindUserSearchResult(response);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: response.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnSave").click(function () {
        var valid = false;
        if (step == 0) {
            valid = ValidateStep1();
            if (valid==true) {
                Step1();
                return;
            }
            else {
                return;
            }
        }
        if (step == 1) {
            valid = ValidateStep2();
            if (valid==true) {
                Step2();
                return;
            }
            else {
                return;
            }
        }
        if (step == 2) {
            valid = ValidateStep3();
            if (valid == true) {
                Step3();
                return;
            }
            else {
                return;
            }
        }
        if (step == 3) {
            Step4();
            return;
        }
        if (step == 4) {
            var Gender="";
            if ($("#hdnbranding").val()!="SPMO") {
                Gender = document.getElementById("rdoMale").checked == true ? "M" : "F";
            }
            
            var MarritalStatus = "1";
            if (document.getElementById("rdoSeparated").checked == true) {
                MarritalStatus = "2";
            }
            if (document.getElementById("rdoWidow").checked == true) {
                MarritalStatus = "3";
            }
            if (document.getElementById("rdoDivorcee").checked == true) {
                MarritalStatus = "4";
            }
            var ChildCount = 0;
            var ChildLivingStatus = false;
            if (MarritalStatus == "Unmarried") {
                ChildCount = 0;
            }
            else {
                if ($("#noofchilds").val() > 4) {
                    ChildCount = 5;
                }
                ChildLivingStatus = $("input:radio[name='childrenstatus']:checked").val();
            }
            //validateForm("frmUserProfile");
            var IsIntercast = document.getElementById('rdoIsInterCast').checked;
            var DOB = $("#DOB").val(); var ReligionId = $("#ReligionId").val();
            var CasteId = $("#CasteId").val();
            var Subcaste = $("#Subcaste").val();
            var PlaceOfBirth = $("#PlaceOfBirth").val();
            var TimeofBirth = $("#TimeofBirth").val();
            var OrasId = $("#OrasId").val(); 
            var gotradesc = $("#gotradesc").val(); 
            var HeightId = $("#HeightId").val();
            var BloodGroupId = $("#BloodGroupId").val(); 
            var rdoIsHandicapped = $("#rdoIsHandicapped").checked; 
            var disabledesc = $("#disabledesc").val();
            var cmbColor = $("#cmbColor").val(); 
            var bodytype = $("input:radio[name='bodytype']:checked").val();
            var QualificationId = $("#QualificationId").val();
            var occuption = $("input:radio[name='occuption']:checked").val();
            var IsJobOrBusiness = occuption == "Job" ? true : (occuption!==undefined?false:occuption);
            var txtCompanyName = $("#txtCompanyName").val();
            var cmbIncome = $("#cmbIncome").val();
            var txtFatherName = $("#txtFatherName").val(); var txtMotherName = $("#txtMotherName").val(); var txtNoofBrothers = $("#txtNoofBrothers").val();
            var txtNoofSisters = $("#txtNoofSisters").val(); var Address = $("#txtAddress").val(); var weight = $("#weight").val();
            var txtBirthName = $("#txtBirthName").val();
            var txtFatherMobileNo = $("#txtFatherMobileNo").val();

            var foccuption = $("input:radio[name='foccuption']:checked").val();

            var IsJob = foccuption == "Job" ? true : false;
            var JobBusinessInfo = $("#txtFCompanyName").val();
            var txtFathersIncome = $("#cmbFIncome").val();
            var txtCompanyLocation = $("#txtCompanyLocation").val();
            var IdentificationMark = $("#IdentificationMark").val(); var PANNO = $("#PANNO").val(); var MobileNo = $("#MobileNo").val();
            var txtHobbies = $("#txtHobbies").val();
            var City = $("#City").val();
            var State = $("#StateId").find(":selected").text();
            var Country = $("#CountryId").find(":selected").text();
            var txtUserExpectation = $("#txtUserExpectation").val();
            var family = { FathersName: txtFatherName, MothersName: txtMotherName, NoofBrothers: txtNoofBrothers,NoOfSisters:txtNoofSisters, MobileNo: txtFatherMobileNo, IsJob: IsJob, FathersIncome: txtFathersIncome };
            var JobDetails = { IsJobOrBusiness: IsJobOrBusiness, CompanyName: txtCompanyName, JobLocation: txtCompanyLocation, Income: cmbIncome };
            var model =
                {
                    BloodGroupId: BloodGroupId, ReligionId: ReligionId, CasteId: CasteId,
                    OrasId: OrasId, Gender: Gender, DateOfBirth: DOB, Address: Address,
                    HeightId: HeightId, Weight: weight, IdentificationMark: IdentificationMark,
                    PANNO: PANNO, MobileNo: MobileNo, IsHandiCapped: rdoIsHandicapped,
                    HandicapedType: disabledesc, Color: cmbColor, MarritalStatus: MarritalStatus,
                    NoOfChildrens: ChildCount, ChildLivingStatus: ChildLivingStatus,
                    PlaceofBirth: PlaceOfBirth, strTimeofBirth: TimeofBirth, BodyType: bodytype,
                    Qualification: QualificationId, Income: cmbIncome, BirthName: txtBirthName,
                    GotraName: gotradesc, Hobbies: txtHobbies, UserExpectation: txtUserExpectation,
                    IsIntercast: IsIntercast,
                    City: City,
                    Country: Country,
                    State: State,
                    Taluka: $("#Taluka").val(),
                    District: $("#District").val(),
                };

            var colorlist = [];
            $('#EtxtColor :selected').each(function (i, selected) {
                colorlist[i] = $(selected).val();
            });

            var HeightIdlist = [];
            $('#EHeightId :selected').each(function (i, selected) {
                HeightIdlist[i] = $(selected).val();
            });

            var OrasIdlist = [];
            $('#EOrasId :selected').each(function (i, selected) {
                OrasIdlist[i] = $(selected).val();
            });

            var QualificationIdlist = [];
            $('#EQualificationId :selected').each(function (i, selected) {
                QualificationIdlist[i] = $(selected).val();
            });

            var spinner = new Spinner().spin();
            document.getElementById("contentdiv").appendChild(spinner.el);
            model = JSON.stringify(model); family = JSON.stringify(family);
            var cmbRelations = $("#cmbRelations").val();

            var txtRelativeName = $("#txtRelativeName").val();
            var txtRelativeAddress = $("#txtRelativeAddress").val();
            if ((txtRelativeName != "" || txtRelativeAddress != "") && cmbRelations != "") {
                relatives.push({ RelativeName: $("#txtRelativeName").val(), RelativeAddress: $("#txtRelativeAddress").val(), Relation: cmbRelations, MobileNo: $("#txtRContactNo").val() });
            }
            var txtUncleName = $("#txtUncleName").val();
            var txtUncleAddress = $("#txtUncleAddress").val();
            if (txtUncleName != "" || txtUncleAddress != "") {
                relatives.push({ RelativeName: txtUncleName, RelativeAddress: txtUncleAddress, Relation: $("#hdnbranding").val() == "SPMO" ? "Uncle" : "काका", MobileNo: $("#txtUContactNo").val() });
            }
            relatives = JSON.stringify(relatives);
            JobDetails = JSON.stringify(JobDetails);
            $.ajax({
                cache: false,
                type: 'POST',
                url: "../UserProfile/UpdateUserProfile",
                data: {
                    "model": model, "family": family,
                    "relatives": relatives,
                    "JobDetails": JobDetails,
                    "colorlist": colorlist,
                    "HeightIdlist": HeightIdlist,
                    "OrasIdlist": OrasIdlist,
                    "QualificationIdlist": QualificationIdlist
                },
                success: function (data) {
                    if (data.Status == true) {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: $("#hdnbranding").val()=="SPMO"?"Your information saved successfully.": "तुमची माहिती सेव केली आहे.",
                            Type: "alert",
                            OnOKClick: function () {
                                window.location = GetVirtualDirectory() + '/UserProfile/UploadPhotos';
                            }
                        });
                        objShowCustomAlert.ShowCustomAlertBox();

                    }
                    else {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: data.ErrorMessage,
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                    document.getElementById("contentdiv").removeChild(spinner.el);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    document.getElementById("contentdiv").removeChild(spinner.el);
                }
            });
        }
    });

    
});

function Step0()
{
    $("#divPersonal").show();
    $("#divSocio").hide();
    $("#divPhysical").hide();
    $("#divProfessional").hide();
    $("#divFamilyDetails").hide();
    $("#divUserExpection").hide();
    $("#divRelativeDetails").hide();
}


function ApproveUser(UserId, IsActive)
{
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/ApproveUser",
        data: { UserId: UserId,IsActive:IsActive },
        success: function (data) {
            if (data == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Your account active successfully." : "तुमचे खाते सक्रिय केले आहे.",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location = "../UserProfile/Index";
                    },
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function UpdateUserData() {

    var family = {MobileNo: $("#txtFatherMobileNo").val() };
    var JobDetails = { JobLocation: $("#txtCompanyLocation").val(), Income: $("#cmbIncome").val() };
    var model =
        {
            Address: $("#txtAddress").val(),
            City: $("#City").val(),
            MobileNo: $("#MobileNo").val(),
            PlaceOfBirth:$("#PlaceOfBirth").val(),
            BirthName: $("#txtBirthName").val(),
            Email: $("#txtEmail").val(),
            Weight: $("#weight").val(),
            Taluka: $("#Taluka").val(),
            District: $("#District").val(),
        };

    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    model = JSON.stringify(model);
    family = JSON.stringify(family);
    JobDetails = JSON.stringify(JobDetails);
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/UpdateUserData",
        data: {
            "model": model, "family": family,
            "JobDetails": JobDetails,
        },
        success: function (data) {
            if (data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Your information saved successfully." : "तुमची माहिती सेव केली आहे",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location = GetVirtualDirectory() + '/UserProfile/Index';
                    }
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            else {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: data.ErrorMessage,
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            document.getElementById("contentdiv").removeChild(spinner.el);
        }
    });
}

function bindData() {
    var userdata = JSON.parse($("#UserDetails").val());
    $("#txtAddress").val(userdata.Address);
    $("#City").val(userdata.City);
    $("#Taluka").val(userdata.Taluka);
    $("#District").val(userdata.District);
    $("#txtBirthName").val(userdata.BirthName);
    $("#PlaceOfBirth").val(userdata.PlaceofBirth);
    $("#weight").val(userdata.Weight);
    $("#txtEmail").val(userdata.MailId);
    $("#MobileNo").val(userdata.MobileNo);
    $("#txtCompanyLocation").val(userdata.JobLocation);
    $("#txtCompanyName").val(userdata.CompanyName);
    $("#cmbIncome").val(userdata.Income);
}

function EditUser(UserId)
{
    window.location = "../UserProfile/EditUser?UserId=" + UserId;
}

function bindUserSearchResult(students)
{
    $("#maindiv").html("");
    var html="";
    for (var i = 0; i < students.DataResponse.length; i++) {
        var item = students.DataResponse[i];
        if ($("#hdnbranding").val() == "SPMO") { 
            html += "<div class='media' style='margin-top:0px;!important;overflow: inherit;'><div class='media-left'><a href='#'>";
            if (item.Img1 != null) {
                html += "<img class='media-object' src='../../" + item.Img1 + "' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
            }
            else {
                if (item.Gender == 'M') {
                    html += "<img class='media-object' src='../../Content/Images/User_male.png')' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
                }
                else {
                    html += "<img class='media-object' src='../../Content/Images/User_female.png')' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
                }
            }
            html += "</a><span class='glyphicon glyphicon-refresh' onclick='Rotate(" + item.UserId + ")'></span></div><div class='media-body'><input type='hidden' id='" + item.UserId + "' value=" + item.UserId + "' /><span class='mediaprofile spanStyle' id='" + item.UserId + "'>";
            if (item.Gender == 'M') {
                html += "Mr. " + item.FirstName + " " + item.LName + "</span>";
            }
            else if (item.Gender == 'F') {
                html += "Miss. " + item.FirstName + " " + item.LName + "</span>";
            }
            else {
                html += item.FirstName + " " + item.LName + "</span>";
            }
            if ($("#IsAdmin").val() == "true") {
                if (item.IsActive == "true") {
                    html += "<span>Active</span>";
                }
                else {
                    html += "<span>New Candidate</span>";
                }
            }
            html += "<p class='mediaprofile'>Education- " + item.Qualification + ", &nbsp;&nbsp; Height - " + item.Height + ", &nbsp;&nbsp; Religion - " + item.ReligionName + ",&nbsp;&nbsp; Cast - " + item.CastName;
            html += ", &nbsp;&nbsp;Age-" + item.Age + ", &nbsp;&nbsp; Moonsign- " + item.OrasName + "&nbsp;&nbsp;" + item.Address + "";
            html += "</p>";
            if ($("#IsAdmin").val() == "true") {
                html += "<input type='button' value='Send Password' onclick='SendSMS(" + item.UserId + ")' class='btn btn-default' />";
                html += "<input type='button' value='Married' class='btn btn-default' onclick='MarriageDone(" + item.UserId + ")' />";
            }
            html += "<input type='button' value='View Information.' onclick='ViewProfile(" + item.UserId + ")' class='btn btn-default' /> &nbsp;&nbsp;";
            html += "<input type='button' value='Send Message' class='btn btn-default' onclick='SendMessage(" + item.UserId + ")' />";
            html += "</div><div class='clearfix'> </div></div>";
        }
        else {
            html += "<div class='media' style='margin-top:0px;!important;overflow: inherit;'><div class='media-left'><a href='#'>";
            if (item.Img1 != null) {
                html += "<img class='media-object' src='../../" + item.Img1 + "' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
            }
            else {
                if (item.Gender == 'M') {
                    html += "<img class='media-object' src='../../Content/Images/User_male.png')' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
                }
                else {
                    html += "<img class='media-object' src='../../Content/Images/User_female.png')' data-holder-rendered='true' style='width: 80px; height: 80px;'>";
                }
            }
            html += "</a><span class='glyphicon glyphicon-refresh' onclick='Rotate(" + item.UserId + ")'></span></div><div class='media-body'><input type='hidden' id='" + item.UserId + "' value=" + item.UserId + "' /><span class='mediaprofile spanStyle' id='" + item.UserId + "'>";
            if (item.Gender == 'M') {
                html += "चि. " + item.FirstName + " " + item.LName + "</span>";
            }
            else if (item.Gender == 'F') {
                html += "कु. " + item.FirstName + " " + item.LName + "</span>";
            }
            else {
                html += item.FirstName + " " + item.LName + "</span>";
            }
            if ($("#IsAdmin").val() == "true") {
                if (item.IsActive == "true") {
                    html += "<span>सक्रिय</span>";
                }
                else {
                    html += "<span>नवीन उमेदवार</span>";
                }
            }
            html += "<p class='mediaprofile'>शिक्षण- " + item.Qualification + ", &nbsp;&nbsp; उंची - " + item.Height + ", &nbsp;&nbsp; धर्म - " + item.ReligionName + ",&nbsp;&nbsp; जात - " + item.CastName;
            html += ", &nbsp;&nbsp;वय-" + item.Age + ", &nbsp;&nbsp; राशी- " + item.OrasName + "&nbsp;&nbsp;" + item.Address + "";
            html += "</p>";
            if ($("#IsAdmin").val() == "true") {
                html += "<input type='button' value='पासवर्ड पाठवा' onclick='SendSMS(" + item.UserId + ")' class='btn btn-default' />";
                html += "<input type='button' value='विवाह झाला' class='btn btn-default' onclick='MarriageDone(" + item.UserId + ")' />";
            }
            html += "<input type='button' value='संपूर्ण माहिती पहा.' onclick='ViewProfile(" + item.UserId + ")' class='btn btn-default' /> &nbsp;&nbsp;";
            html += "<input type='button' value='संदेश पाठवा' class='btn btn-default' onclick='SendMessage(" + item.UserId + ")' />";
            html += "</div><div class='clearfix'> </div></div>";
        }
    }
    $("#maindiv").html(html);
}

function SendSMS(UserId) {
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var url = GetVirtualDirectory() + '/PendingUsers/SendSMS';
    url = url + "?UserId=" + UserId;//$("#smsapi").val() + "&mobiles=" + MobileNo + "&message=Please complete your profile to find your match&sender= VARMALAVIVAH&route=4&country=91";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: url,
        dataType: "json",
        success: function (students) {
            console.log(students);
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "SMS Send successfully",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (data) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "Not able to send sms." + data.responseText,
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            document.getElementById("contentdiv").removeChild(spinner.el);
        }
    });
}

function RemoveUser(UserId)
{
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/RemoveUser",
        data: { UserId: UserId },
        success: function (data) {
            document.getElementById("contentdiv").removeChild(spinner.el);
            if (data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "User removed successfully.",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location= GetVirtualDirectory() + "/UserProfile/Index";
                    },
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            else {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Not able to delete user profile.",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location = GetVirtualDirectory() + "/UserProfile/Index";
                    },
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function Step1() {
    if (step == 0) {
        $("#divPersonal").hide();
        $("#divSocio").show();
        $("#divPhysical").show();
        $("#divProfessional").hide();
        $("#divFamilyDetails").hide();
        $("#divRelativeDetails").hide();
        //$("#divUserProfile").hide();
        $("#divUserExpection").hide();
        $("#OrasId").focus();
        step = 1;
    }
}

function Step3()
{
    $("#divPersonal").hide();
    $("#divSocio").hide();
    $("#divPhysical").hide();
    $("#divProfessional").hide();
    $("#divFamilyDetails").hide();
    $("#divRelativeDetails").show();
    //$("#divUserProfile").show();
    $("#divUserExpection").hide();
    $("#cmbRelations").focus();
    step = 3;
}

function Step4()
{
    $("#divPersonal").hide();
    $("#divSocio").hide();
    $("#divPhysical").hide();
    $("#divProfessional").hide();
    $("#divFamilyDetails").hide();
    $("#divRelativeDetails").hide();
    //$("#divUserProfile").hide();
    $("#divUserExpection").show();
    $("#EtxtColor").focus();
    step = 4;
}

function Step2() {
    $("#divPersonal").hide();
    $("#divSocio").hide();
    $("#divPhysical").hide();
    $("#divProfessional").show();
    $("#divFamilyDetails").show();
    $("#divRelativeDetails").hide();
    //$("#divUserProfile").hide();
    $("#divUserExpection").hide();
    $("#QualificationId").focus();
    step = 2;
}

function SendMessage(UserId)
{
    window.location = GetVirtualDirectory() + '/Message/Index?UserId=' + UserId;
}

function ValidateStep1()
{
    var valid = true;
    if ($("#hdnbranding").val()!="SPMO") {
        var gender = $("input:radio[name='gender']:checked").val();
        var valid = true;
        if (gender === undefined) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select gender" : "कृपया लिंग निवडा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var marritalstatus = $("input:radio[name='marritalstatus']:checked").val();
        if (marritalstatus === undefined) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select marrital status" : "कृपया वैवाहिक स्थिती निवडा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var DOB = $("#DOB").val();
        if (DOB == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please fill date of birth." : "जन्मतारीख भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var ReligionId = $("#ReligionId").val();
        if (ReligionId == 0) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select religion" : "तुमचा धर्म निवडा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var CasteId = $("#CasteId").val();
        if (CasteId == 0) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select caste" : "जात निवडा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var TimeofBirth = $("#TimeofBirth").val();
        if (TimeofBirth == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please fill time of birth." : "जन्म वेळ भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var txtAddress = $("#txtAddress").val();
        if (txtAddress == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please fill address" : "पत्ता भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    return valid;
}

function ValidateStep2()
{
    var valid = true;
    
    if (valid == true) {
        var HeightId = $("#HeightId").val();
        if (HeightId == 0) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select your height" : "उंची निवडा",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }

    if (valid == true) {
        var txtComplexion = $("#txtComplexion").val();
        if (txtComplexion == 0) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please select your complexion" : "वर्ण भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    return valid;
}

function ValidateStep3()
{
    var valid = true;
    var QualificationId = $("#QualificationId").val();
    if (QualificationId == "") {
        var objShowCustomAlert = new ShowCustomAlert({
            Title: "",
            Message: $("#hdnbranding").val() == "SPMO" ? "Please fill your qualification." : "शिक्षण भरा.",
            Type: "alert",
        });
        objShowCustomAlert.ShowCustomAlertBox();
        valid = false;
    }
    
    
    if (valid == true) {
        var txtFatherName = $("#txtFatherName").val();
        if (txtFatherName == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please fill your fathers name" : "वडिलांचे नाव भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var txtMotherName = $("#txtMotherName").val();
        if (txtMotherName == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Please fill your mothers name" : "आईचे नाव भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    
    return valid;
}



function ApproveRequest(RequestFrom,RequestTo,RequestId,button)
{
    //var RequestId=RequestFrom
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/ApproveRequest",
        data: { RequestId: RequestId },
        success: function (data) {
            if (data.Status==true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Request has been confirmed, Your profile shared with requested user.",
                    Type: "alert",
                    OnOKClick: function () {
                        $(button).val("View Profile");
                        $(button).attr("onclick", "ViewProfile(" + RequestFrom + "," + RequestTo + ")");
                    },
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function ViewProfile(RequestFrom) {
    window.location = "../UserProfile/ShowProfile?ProfileId=" + RequestFrom;
}

function UploadSPMO(UserId) {
    if (UserId === undefined) {
        UserId = 0;
    }

    
 
    var data = new FormData();
    var files = null;
    if (UserId > 0) {
        files = $("#fileprofileImage")[0].files;
    }
    else {
        files = $("#profileImage")[0].files;
    }
    if (files.length > 5) {
        var objShowCustomAlert = new ShowCustomAlert({
            Title: "",
            Message: "Only upto 5 images are allowed.",
            Type: "alert"

        });
        objShowCustomAlert.ShowCustomAlertBox();
    }
    else {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        $.ajax({
            url: "../UserProfile/UploadSPMO?UserId=" + UserId,
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                if (response == true) {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Image uploaded successfully." : "फोटो दाखल केला.",
                        Type: "alert",
                        OnOKClick: function () {
                            window.location.reload();
                        }
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //var spinner = new Spinner().spin();
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (er) {
                alert(er.responseText);
            }
        });
    }
}

function Upload(UserId) {
    if (UserId===undefined) {
        UserId = 0;
    }
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var data = new FormData();
    var files = null;
    if (UserId>0) {
        files = $("#fileprofileImage").get(0).files;
    }
    else {
        files = $("#profileImage").get(0).files;
    }
    if (files.length > 0) {
        data.append("profileImage", files[0]);
    }
    $.ajax({
        url: "../UserProfile/Upload?UserId=" + UserId,
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            if (response == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SPMO" ? "Image uploaded successfully." : "फोटो दाखल केला.",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location.reload();
                    }
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            //var spinner = new Spinner().spin();
            document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (er) {
            alert(er.responseText);
        }
    });
}

function UploadA()
{
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var data = new FormData();
    var files = $("#profileMainImage").get(0).files;
    if (files.length > 0) {
        data.append("profileMainImage", files[0]);
    }
    $.ajax({
        url: "../UserProfile/UploadA",
        type: "POST",
    processData: false,
    contentType: false,
    data: data,
    success: function (response) {
        if (response==true) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Image uploaded successfully." : "फोटो दाखल केला.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
        }
        //var spinner = new Spinner().spin();
        document.getElementById("contentdiv").removeChild(spinner.el);
    },
    error: function (er) {
        alert(er.responseText);
        //var spinner = new Spinner().spin();
        document.getElementById("contentdiv").removeChild(spinner.el);
    }
});
}

function UploadB() {
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var data = new FormData();
    var files = $("#KundaliImage").get(0).files;
    if (files.length > 0) {
        data.append("KundaliImage", files[0]);
    }
    $.ajax({
        url: "../UserProfile/UploadB",
        type: "POST",
    processData: false,
    contentType: false,
    data: data,
    success: function (response) {
        if (response == true) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SPMO" ? "Image uploaded successfully." : "फोटो दाखल केला.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
        }
        //var spinner = new Spinner().spin();
        document.getElementById("contentdiv").removeChild(spinner.el);
    },
    error: function (er) {
        alert(er.responseText);
        //var spinner = new Spinner().spin();
        document.getElementById("contentdiv").removeChild(spinner.el);

    }
});
}