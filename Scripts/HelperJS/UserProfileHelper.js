var step = 0;
$(document).ready(function () {
    var startdt = new Date();
    var mindate = startdt.getDate().toString() + "/" + (startdt.getMonth() + 1) + "/" + (startdt.getFullYear() - 60).toString();
    var maxdate = startdt.getDate().toString() + "/" + (startdt.getMonth() + 1) + "/" + (startdt.getFullYear() - 22).toString();
    Step0();
    $("#rdoUnmarried").attr("checked", true);
    $("#ReligionId").val(1);
    //$("#ReligionId").trigger("change");
    LoadCasts(1);
    $("#CasteId").val(1);
    $("#CountryId").val(1);
    //$("#divUserProfile").hide();
    $("#myModal").hide();
    $("#myModal1").hide();
    LoadCountry();
    LoadAllImage();

    $("#btnCloseModel").click(function () {
        $("#myModal").hide();
        $("#myModal1").hide();
    });
    $("#btnChangePassword").click(function () {
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").appendChild(spinner.el);
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
                        Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                //document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });
    $("#btnShowPopup,#btnShowPopup_0,#btnShowPopup_1,#btnShowPopup_2,#btnShowPopup_3,#btnShowPopup_4").click(function () {
        $("#myModal").show();
        $("#Img1").attr("src", $(this).attr("src"));
        $("#myModal1").show();
    });
    $("#divUserExpectation").hide();

    $("#txtSearch").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("#btnSearch").trigger("click");
        }
    });

    $("#btnReset").click(function () {
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").appendChild(spinner.el);
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
                            Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                //var spinner = new Spinner().spin();
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                //document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });

    $("#btnASearch").click(function () {
        var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").appendChild(spinner.el);
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
                            Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //document.getElementById("contentdiv").removeChild(spinner.el);
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
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                //document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
        $("#btnCloseModel").trigger("click");
    })

    $("#btnSearch").click(function () {
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").appendChild(spinner.el);
        var prefix = $("#txtSearch").val();
        if (prefix == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "Please fill search text!",
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
                            Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही.",
                            Type: "alert",
                        });
                        objShowCustomAlert.ShowCustomAlertBox();
                    }
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही",
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Information not available" : "माहिती उपलब्ध नाही",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                //document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    })

    //var maxdate = dtcurrent.setDate(dtcurrent.getFullYear() - 22);
    //$("#DOB").attr("min", mindate);
    //$("#DOB").attr("max", maxdate);
    $("#ReligionId").change(function () {
        
        var ReligionId = $(this).val();
        if (ReligionId > 0) {
            LoadCasts(1);
        }
    });


    //$("#noofchilds").attr("disabled", "disabled");
    //$("#rdochildLWM").attr("disabled", "disabled");
    //$("#rdochildNLWM").attr("disabled", "disabled");
    //$("#txtCompanyName").attr("disabled", "disabled");
    $("#disabledesc").attr("disabled","disabled");
    $("#rdoJob").click(function () {
        //$("#txtCompanyName").attr("disabled", "disabled");
    });
    $("#rdoBusiness").click(function () {
        $("#txtCompanyName").removeAttr("disabled", "disabled");
    });
    $("#rdoWidow,#rdoDivorcee,#rdoSeparated").click(function () {
        //$("#noofchilds").removeAttr("disabled");
        //$("#rdochildLWM").removeAttr("disabled");
        //$("#rdochildNLWM").removeAttr("disabled");
        $("#divorceeInfo").show();
    });
    $("#rdoUnmarried").click(function () {
        $("#divorceeInfo").hide();
        //$("#noofchilds").val("");
        //$("#noofchilds").attr("disabled", "disabled");
        //$("#rdochildLWM").attr("disabled", "disabled");
        //$("#rdochildNLWM").attr("disabled", "disabled");
    });

    var relatives = [];

    $("#btnAddFamilyDetails,#btnAddRelativeDetails").click(function () {
        var btnid = $(this).attr("id");
        var relativedetails="";
        if (btnid == "btnAddFamilyDetails") {
            relativedetails = { RelativeName: $("#txtUncleName").val(), RelativeAddress: $("#txtUncleAddress").val(), Relation:$("#hdnbranding").val()=="SINDHI"? "Uncle": "काका", MobileNo: $("#txtUContactNo").val() };
            var htmlrel = "<tr><td>" + $("#txtUncleName").val() + "</td><td>" + $("#txtUncleAddress").val() + "</td><td>" + $("#txtUContactNo").val() + "</td></tr>";
            htmlrel = $("#tblUncle").html() + htmlrel;
            $("#tblUncle").html(htmlrel)
            $("#txtUncleName").val("");
            $("#txtUncleAddress").val("");
            $("#txtUContactNo").val("");
        }
        else {
            var cmbRelations = $("#cmbRelations").val();
            relativedetails = { RelativeName: $("#txtRelativeName").val(), RelativeAddress: $("#txtRelativeAddress").val(), Relation: cmbRelations, MobileNo: $("#txtRContactNo").val() };
            var htmlrel = "<tr><td>" + $("#txtRelativeName").val() + "</td><td>" + $("#txtRelativeAddress").val() + "</td><td>" + $("#txtRContactNo").val() + "</td></tr>";
            htmlrel = $("#tblRelative").html() + htmlrel;
            $("#tblRelative").html(htmlrel)
            $("#txtRelativeName").val("");
            $("#txtRelativeAddress").val("");
            $("#txtRContactNo").val("");  
        }
        relatives.push(relativedetails);
    });

    $("#rdoIsHandicapped").click(function () {
        if ($("#rdoIsHandicapped").prop("checked") == true) {
            $("#disabledesc").removeAttr("disabled");
        }
        else {
            $("#disabledesc").attr("disabled","disabled");
        }
    });

    $("#rdoBusiness").change(function () {
        if ($("#rdoBusiness").prop("checked") == true) {
            $("#officediv").show();
        }
        else {
            $("#officediv").hide();
        }
    });
    $("#rdoJob").change(function () {
        if ($("#rdoJob").prop("checked") == true) {
            $("#officediv").hide();
        }
    });
    $("#rdoNotWorking").change(function () {
        if ($("#rdoNotWorking").prop("checked") == true) {
            $("#officediv").hide();
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
        $.ajax({
            cache: false,
            type: 'GET',
            url: "../UserProfile/UserNext",
            success: function (data) {
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
            }
        });
    });

    $("#btnGoBack").click(function () {
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoBack',
            dataType: "json",
            success: function (response) {
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
            }
        });
    });

    $("#btnGoLast").click(function () {
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoLast',
            dataType: "json",
            success: function (response) {
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
            }
        });
    });

    $("#btnGoFirst").click(function () {
        $.ajax({
            cache: false,
            type: "Get",
            async: false,
            url: GetVirtualDirectory() + '/UserProfile/GoFirst',
            dataType: "json",
            success: function (response) {
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
            }
        });
    });

                    
    $("#CountryId").click(function () {
        var cid = $(this).val();
        if (cid > 0) {
            LoadStates(cid);
        }
    });

    
    $("#BirthCountry").click(function () {
        var cid = $(this).val();
        if (cid > 0) {
            if (cid > 0) {
                LoadBirthStates(cid);
            }
        }
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
            if ($("#hdnbranding").val()!="SINDHI") {
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
            var IsIntercast = false;// document.getElementById('rdoIsInterCast').checked;
            var DOB = $("#DOB").val(); var ReligionId = $("#ReligionId").val();
            var CasteId = $("#CasteId").val();
            var SubCaste = $("#Subcaste").val();
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
            var IsManglik = $("input:radio[name='Manglik']:checked").val();
            var Jobdata = occuption;
            var txtCompanyName = $("#txtCompanyName").val();
            var cmbIncome = $("#cmbIncome").val();
            var txtFatherName = $("#txtFatherName").val(); var txtMotherName = $("#txtMotherName").val(); var txtNoofBrothers = $("#txtNoofBrothers").val();
            var txtNoofSisters = $("#txtNoofSisters").val();
            var Address = $("#txtAddress").val(); var weight = $("#weight").val();
            var txtBirthName = $("#txtBirthName").val();
            var txtFatherMobileNo = $("#txtFatherMobileNo").val();

            var foccuption = $("input:radio[name='foccuption']:checked").val();

            var IsJob = foccuption == "Job" ? true : false;
            var JobBusinessInfo = $("#txtFCompanyName").val();
            var txtFathersIncome = $("#cmbFIncome").val();
            var txtCompanyLocation = $("#txtCompanyLocation").val();
            var IdentificationMark = $("#IdentificationMark").val(); var PANNO = $("#PANNO").val(); var MobileNo = $("#MobileNo").val();
            var txtHobbies = $("#txtHobbies").val();
            var txtActivities = $("#txtActivities").val();

            var City = $("#City").val();
            var State = $("#StateId").find(":selected").text();
            var BirthCountry = $("#BirthCountry").find(":selected").text();
            var BirthState = $("#BirthState").find(":selected").text();
            var Country = $("#CountryId").find(":selected").text();
            var txtUserExpectation = $("#txtUserExpectation").val();
            var family = {
                FathersName: txtFatherName, MothersName: txtMotherName,
                NoofBrothers: txtNoofBrothers, NoOfSisters: txtNoofSisters,
                NoOfMBro: $("#NoOfMBro").val(), NoOfMSis: $("#NoOfMSis").val(),
                MobileNo: txtFatherMobileNo, IsJob: IsJob, FathersIncome: txtFathersIncome,
                GrandFatherName: $("#txtGFatherName").val()
            };
            var JobDetails = { Jobdata: Jobdata, CompanyName: txtCompanyName, JobLocation: txtCompanyLocation, Income: cmbIncome, IsManglik: IsManglik };
            var model =
                {
                    UserId:$("#ActiveUserId").val(),
                    BloodGroupId: BloodGroupId, ReligionId: ReligionId, CasteId: CasteId,
                    OrasId: OrasId, Gender: Gender, DateOfBirth: DOB,
                    Address: Address,
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
                    Subcaste: SubCaste,
                    State: State,
                    BirthCountry: BirthCountry,

                    BirthState: BirthState,
                    Taluka: $("#Taluka").val(),
                    District: $("#District").val(),
                    Achievements: txtActivities,
                    Pincode: $("#Pincode").val(),
                    IsOwnHouse: $("input:radio[name='IsOwnHouse']:checked").val(),
                    IsSpec: $("input:radio[name='IsSpec']:checked").val(),
                    IsOwnShop: $("input:radio[name='IsOwnShop']:checked").val(),
                    ReferenceName: $("#ReferenceName").val(),
                    ReferenceContact: $("#ReferenceContact").val(),

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

            //var spinner = new Spinner().spin();
            //document.getElementById("contentdiv").appendChild(spinner.el);
            model = JSON.stringify(model); family = JSON.stringify(family);
            var cmbRelations = $("#cmbRelations").val();

            var txtRelativeName = $("#txtRelativeName").val();
            var txtRelativeAddress = $("#txtRelativeAddress").val();
            if ((txtRelativeName != "" || txtRelativeAddress != "") && cmbRelations != "") {
                relatives.push({ RelativeName: $("#txtRelativeName").val(), RelativeAddress: $("#txtRelativeAddress").val(), Relation: cmbRelations, MobileNo: $("#txtRContactNo").val() });
                var htmlrel = "<tr><td>" + txtUncleName + "</td><td>" + txtUncleAddress + "</td><td>" + MobileNo + "</td></tr>";
                htmlrel = $("#tblRelative").html() + htmlrel;
                $("#tblRelative").html(htmlrel)
            }
            var txtUncleName = $("#txtUncleName").val();
            var txtUncleAddress = $("#txtUncleAddress").val();
            if (txtUncleName != "" || txtUncleAddress != "") {
                relatives.push({ RelativeName: txtUncleName, RelativeAddress: txtUncleAddress, Relation: $("#hdnbranding").val() == "SINDHI" ? "Uncle" : "काका", MobileNo: $("#txtUContactNo").val() });
                var htmluncle = "<tr><td>" + txtUncleName + "</td><td>" + txtUncleAddress + "</td><td>" + MobileNo + "</td></tr>";
                htmluncle=$("#tblUncle").html() + htmluncle;
                $("#tblUncle").html(htmluncle)
            }
            relatives = JSON.stringify(relatives);
            JobDetails = JSON.stringify(JobDetails);
            ShowLoader();
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
                            Message: $("#hdnbranding").val()=="SINDHI"?"Your information saved successfully.": "तुमची माहिती सेव केली आहे.",
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
                    HideLoader();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //document.getElementById("contentdiv").removeChild(spinner.el);
                }
            });
        }
    });

    
});

function LoadStates(cid)
{
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: GetVirtualDirectory() + '/State/GetStatesByCountry',
        data: { CountryId: cid },
        success: function (data) {
            $('#StateId').children('option:not(:first)').remove();
            //For spoke ddl
            var items1 = "";

            $.each(data, function (i, item) {
                items1 += "<option value=\"" + item.StateId + "\">" + item.StateName + "</option>";
            });
            $("#StateId").append(items1);
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function LoadBirthStates(cid) {
    var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: GetVirtualDirectory() + '/State/GetStatesByCountry',
        data: { CountryId: cid },
        success: function (data) {
            $('#BirthState').children('option:not(:first)').remove();
            //For spoke ddl
            var items1 = "";

            $.each(data, function (i, item) {
                items1 += "<option value=\"" + item.StateId + "\">" + item.StateName + "</option>";
            });
            $("#BirthState").append(items1);
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function LoadCountry()
{
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: GetVirtualDirectory() + "/Country/GetCountries",
        success: function (data) {
            $('#CountryId').html("");
            $('#BirthCountry').html("");
            //For spoke ddl
            var items1 = "<option value='0'>---Select Country---</option>";

            $.each(data, function (i, item) {
                items1 += "<option value=\"" + item.CountryId + "\">" + item.CountryName + "</option>";
            });
            $("#CountryId").append(items1);
            $("#BirthCountry").append(items1);
            //document.getElementById("contentdiv").removeChild(spinner.el);
            $("#CountryId").val(1);
            $("#BirthCountry").val(1);
            LoadStates(1);
            LoadBirthStates(1);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}
var ImageList = [];

function LoadAllImage()
{
    var basepath = GetVirtualDirectory();
    basepath += "/";
    var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'Get',
        url: "../UserProfile/GetAllImages",
        success: function (data) {
            ImageList = data;
            $("#btnShowPopup_0").attr("src", basepath + data[0].ImagePath);
            var html = "";
            for (var i = 1; i < data.length; i++) {
                html += '<div class="col-md-3"><div class="coffee">';
                html += '<div class="coffee-top">';
                html += '   <a href="#">';
                html += '        <img class="img-responsive profilepicture showprofile"  alt="" id="btnShowPopup_' + i + '" data="0" src="' + basepath + "/"+ data[i].ImagePath + '" onclick="ShowImage(this)">';
                html += '    </a>';
                html += ' </div>';
                html += '</div></div>';
            }
            $("#profileImageList").html(html);
            html = "";
            var htmlli = "";
            var htmlimg = "";
            html += '<div id="myCarousel" class="carousel slide" data-ride="carousel">';
            htmlimg += '<div class="carousel-inner">';

            for (var i = 0; i < 1; i++) {
                htmlimg += '<div class="item active" data-index="0" id="sliderImg">';
                htmlimg += '<img src="' + basepath + "/" + data[i].ImagePath + '">';
                htmlimg += '</div>';
            }
            htmlimg += '</div>';
            html += htmlli + htmlimg;
            html += '<a class="left carousel-control" href="#myCarousel" data-slide="prev" onclick="ShowPrevImg()">';
            html += '<span class="glyphicon glyphicon-chevron-left"></span>';
            html += '<span class="sr-only">Previous</span>';
            html += '</a>';
            html += '<a class="right carousel-control" href="#myCarousel" data-slide="next" onclick="ShowNextImg()">';
            html += '<span class="glyphicon glyphicon-chevron-right"></span>';
            html += '<span class="sr-only">Next</span>';
            html += '</a>';
            html += '</div>';
            $("#modalbody").html(html);
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Error during process: \n' + xhr.responseText);
            //document.getElementById("contentdiv").removeChild(spinner.el);
        }
    });
}

function PrevImage() {
    var basepath = GetVirtualDirectory();
    var index = $("#img01").attr("data-index");
    if (parseInt(index) == 0) {
        return false;
    }
    index--;
    $("#img01").attr("data-index", index);
    $("#img01").attr("src", ImageList[index].ImagePath);
}

function NextImage() {
    var basepath = GetVirtualDirectory();
    var index = $("#img01").attr("data-index");
    if (parseInt(index) == (ImageList.length - 1)) {
        return false;
    }
    index++;
    $("#img01").attr("data-index", index);
    $("#img01").attr("src", ImageList[index].ImagePath);
}

function ShowPrevImg()
{
    var basepath = GetVirtualDirectory();
    var index = $("#sliderImg").attr("data-index");
    if (parseInt(index)==0) {
        return false;
    }
    index--;
    $("#sliderImg").attr("data-index",index);
    $("#sliderImg>img").attr("src", basepath + "/" + ImageList[index].ImagePath);
}

function ShowNextImg() {
    var basepath = GetVirtualDirectory();
    var index = $("#sliderImg").attr("data-index");
    if (parseInt(index) == (ImageList.length -1)) {
        return false;
    }
    index++;
    $("#sliderImg").attr("data-index", index);
    $("#sliderImg>img").attr("src", basepath + "/" + ImageList[index].ImagePath);
}

function Rotate(id) {
    var currunt = parseInt($("#Img1").attr("data"));
    if (currunt == 360) {
        currunt = 0;
    }
    currunt = currunt + 90;
    $("#Img1").css({
        "-webkit-transform": "rotate(" + currunt + "deg)",
        "-moz-transform": "rotate(" + currunt + "deg)",
        "transform": "rotate(" + currunt + "deg)" /* For modern browsers(CSS3)  */
    });
    $("#profileImage").css({
        "-webkit-transform": "rotate(" + currunt + "deg)",
        "-moz-transform": "rotate(" + currunt + "deg)",
        "transform": "rotate(" + currunt + "deg)" /* For modern browsers(CSS3)  */
    });
    $("#Img1").attr("data", currunt);
    $("#profileImage").attr("data", currunt);
}

function ShowImage(element)
{
    $("#Img1").attr("src", $(element).attr("src"));
    $("#myModal1").show();
}

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

function LoadCasts(ReligionId)
{
    $('#CasteId').empty();
    var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../Cast/GetCasts",
        data: { ReligionId: ReligionId },
        success: function (data) {
            var items1 = "";
            if ($("#hdnbranding").val() == "SINDHI") {
                items1 += "<option value=\"0\">---Select Cast---</option>";
            }
            else {
                items1 += "<option value=\"0\">---जात निवडा---</option>";
            }
            $.each(data, function (i, item) {
                items1 += "<option value=\"" + item.CastId + "\">" + item.CastName + "</option>";
            });
            $("#CasteId").append(items1);
            //document.getElementById("contentdiv").removeChild(spinner.el);
            $("#CasteId").val(1);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Error during process: \n' + xhr.responseText);
            //document.getElementById("contentdiv").removeChild(spinner.el);
        }
    });
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
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Your account active successfully." : "तुमचे खाते सक्रिय केले आहे.",
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

    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
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
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Your information saved successfully." : "तुमची माहिती सेव केली आहे",
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
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
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
        if ($("#hdnbranding").val() == "SINDHI") { 
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
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
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
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (data) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "Not able to send sms." + data.responseText,
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            //document.getElementById("contentdiv").removeChild(spinner.el);
        }
    });
}

function RemoveUser(UserId)
{
    var objShowCustomAlert = new ShowCustomAlert({
        Title: "",
        Message: "Are you sure you want to remove this user.",
        Type: "confirm",
        OnOKClick: function () {
            //var spinner = new Spinner().spin();
            //document.getElementById("contentdiv").appendChild(spinner.el);
            $.ajax({
                cache: false,
                type: 'POST',
                url: "../UserProfile/RemoveUser",
                data: { UserId: UserId },
                success: function (data) {
                    //document.getElementById("contentdiv").removeChild(spinner.el);
                    if (data.Status == true) {
                        var objShowCustomAlert = new ShowCustomAlert({
                            Title: "",
                            Message: "User removed successfully.",
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
        },
    });
    objShowCustomAlert.ShowCustomAlertBox();
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
    $("#divRelativeDetails").hide();
    //$("#divUserProfile").show();
    $("#divUserExpection").show();
    $("#cmbRelations").focus();
    step = 4;
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

function ExpressInterest(UserId)
{
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/ExpressInterest",
        data: { UserId: UserId },
        success: function (data) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "Your request has been send.",
                Type: "alert",
                OnOKClick: function () {
                    window.location = GetVirtualDirectory() + "/UserProfile/Index";
                },
            });
            objShowCustomAlert.ShowCustomAlertBox();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function SendMessage(UserId)
{
    window.location = GetVirtualDirectory() + '/Message/Index?UserId=' + UserId;
}

function ValidateStep1()
{
    var valid = true;
    if ($("#hdnbranding").val()!="SINDHI") {
        var gender = $("input:radio[name='gender']:checked").val();
        var valid = true;
        if (gender === undefined) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select gender" : "कृपया लिंग निवडा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select marrital status" : "कृपया वैवाहिक स्थिती निवडा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill date of birth." : "जन्मतारीख भरा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select religion" : "तुमचा धर्म निवडा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select caste" : "जात निवडा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    //if (valid == true) {
    //    var TimeofBirth = $("#TimeofBirth").val();
    //    if (TimeofBirth == "") {
    //        var objShowCustomAlert = new ShowCustomAlert({
    //            Title: "",
    //            Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill time of birth." : "जन्म वेळ भरा.",
    //            Type: "alert",
    //        });
    //        objShowCustomAlert.ShowCustomAlertBox();
    //        valid = false;
    //    }
    //}
    if (valid == true) {
        var txtAddress = $("#txtAddress").val();
        if (txtAddress == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill address" : "पत्ता भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var Pincode = $("#Pincode").val();
        if (Pincode == "") {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill Pincode" : "पत्ता भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
        
    }
    if (valid==true) {
        Pincode = $("#Pincode").val();
        if (Pincode != "") {
            if (Pincode.length != 6) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Pincode length should be 6" : "पत्ता भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }
    if (valid == true) {
        var ReferenceContact = $("#ReferenceContact").val();
        if (ReferenceContact != "") {
            if (ReferenceContact.length != 10) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Reference Mobile No Should be 10 digit." : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select your height" : "उंची निवडा",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }

    if (valid == true) {
        var weight = $("#weight").val();
        if (weight != "") {
            if (weight.length > 3) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill valid w" : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtComplexion = $("#txtComplexion").val();
        if (txtComplexion == 0) {
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please select your complexion" : "वर्ण भरा.",
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
            Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill your qualification." : "शिक्षण भरा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill your fathers name" : "वडिलांचे नाव भरा.",
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
                Message: $("#hdnbranding").val() == "SINDHI" ? "Please fill your mothers name" : "आईचे नाव भरा.",
                Type: "alert",
            });
            objShowCustomAlert.ShowCustomAlertBox();
            valid = false;
        }
    }
    if (valid == true) {
        var txtFatherMobileNo = $("#txtFatherMobileNo").val();
        if (txtFatherMobileNo != "") {
            if (txtFatherMobileNo.length!=10) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Father Mobile No Should be 10 digit." : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtNoofBrothers = $("#txtNoofBrothers").val();
        if (txtNoofBrothers != "") {
            if (txtNoofBrothers.length>1) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Please mention no of brothers less than 10." : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtNoofSisters = $("#txtNoofSisters").val();
        if (txtNoofSisters != "") {
            if (txtNoofSisters.length != 1) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Please mention no of sisters less than 10." : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtNoofBrothers = $("#txtNoofBrothers").val();
        var txtNoofMBrothers = $("#txtNoofMBrothers").val();
        if (txtNoofBrothers != "") {
            if (parseInt(txtNoofBrothers) <parseInt(txtNoofMBrothers)) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "No of married brothers should be less than or equal to no of brothers" : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtNoofSisters = $("#txtNoofSisters").val();
        var txtNoofMSisters=$("#txtNoofMSisters").val();
        if (txtNoofSisters != "") {
            if (parseInt(txtNoofSisters) < parseInt(txtNoofMSisters)) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "No of married sisters should be less than or equal to no of sisterss" : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
        }
    }

    if (valid == true) {
        var txtUContactNo = $("#txtUContactNo").val();
        if (txtUContactNo != "") {
            if (txtUContactNo.length != 10) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: $("#hdnbranding").val() == "SINDHI" ? "Uncle Mobile No Should be 10 digit." : "आईचे नाव भरा.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                valid = false;
            }
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
function ApproveRequestSPMO(UserId)
{
    $("#hdnVisitedUserId").val(UserId);
    $("#myModal").show();
    
}

function ApproveFinal()
{
    var VisitorId = $("#hdnVisitedUserId").val();
    var Approvaltype = $("#ddlApproveRequest").val();
    if (Approvaltype == 0) {
        var objShowCustomAlert = new ShowCustomAlert({
            Title: "",
            Message: "Please select approval type.",
            Type: "alert",
        });
        objShowCustomAlert.ShowCustomAlertBox();
        return false;
    }
    var ApprovedImage = 0;
    var ApprovedImageWithContact = 0;
    if (Approvaltype==1) {
        ApprovedImage = 1;
    }
    if(Approvaltype==2) {
        ApprovedImageWithContact = 1;
    }
    ShowLoader();
    $.ajax({
        cache: false,
        type: 'POST',
        url: "../UserProfile/ApproveRequestSPMO",
        data: { VisitorId: VisitorId, ApprovedImage: ApprovedImage, ApprovedImageWithContact: ApprovedImageWithContact },
        success: function (data) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            var objShowCustomAlert = new ShowCustomAlert({
                Title: "",
                Message: "Request has been approved.",
                Type: "alert",
                OnOKClick: function () {
                    $("#myModal").hide();
                },
            });
            objShowCustomAlert.ShowCustomAlertBox();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
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
        ShowLoader();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
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
                        Message: "Image uploaded successfully.",
                        Type: "alert",
                        OnOKClick: function () {
                            window.location = "../UserProfile/Index";
                        }
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: "Image uploaded successfully.",
                        Type: "alert",
                        OnOKClick: function () {
                            window.location = "../UserProfile/Index";
                        }
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                HideLoader();
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
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
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
                    Message: "Image uploaded successfully.",
                    Type: "alert",
                    OnOKClick: function () {
                        window.location.reload();
                    }
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            //var spinner = new Spinner().spin();
            //document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (er) {
            alert(er.responseText);
        }
    });
}

function UploadA()
{
    ShowLoader();
    var data = new FormData();
    var files = $("#AdhaarImage").get(0).files;
    if (files.length > 0) {
        data.append("AdhaarImage", files[0]);
    }
    $.ajax({
        url: "../UserProfile/UploadA",
        type: "POST",
    processData: false,
    contentType: false,
    data: data,
    success: function (response) {
        var objShowCustomAlert = new ShowCustomAlert({
            Title: "",
            Message: "Image uploaded successfully.",
            Type: "alert",
        });
        objShowCustomAlert.ShowCustomAlertBox();
        $("#AdhaarImagehidden").val(response)
        HideLoader();
    },
    error: function (er) {
        alert(er.responseText);
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").removeChild(spinner.el);
    }
});
}

function UploadB() {
    //var spinner = new Spinner().spin();
    //document.getElementById("contentdiv").appendChild(spinner.el);
    var data = new FormData();
    var files = $("#PANImage").get(0).files;
    if (files.length > 0) {
        data.append("PANImage", files[0]);
    }
    $.ajax({
        url: "../UserProfile/UploadB",
        type: "POST",
    processData: false,
    contentType: false,
    data: data,
    success: function (response) {
        var objShowCustomAlert = new ShowCustomAlert({
            Title: "",
            Message: "Image uploaded successfully.",
            Type: "alert",
        });
        objShowCustomAlert.ShowCustomAlertBox();
        $("#AdhaarImagehidden").val(response)
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").removeChild(spinner.el);
    },
    error: function (er) {
        alert(er.responseText);
        //var spinner = new Spinner().spin();
        //document.getElementById("contentdiv").removeChild(spinner.el);

    }
});
}