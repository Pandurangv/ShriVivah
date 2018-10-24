function DisableControls()
{
    $("#noofchilds").attr("disabled", "disabled");
    $("#rdochildLWM").attr("disabled", "disabled");
    $("#rdochildNLWM").attr("disabled", "disabled");
    //$("#txtCompanyName").attr("disabled", "disabled");
    $("#disabledesc").attr("disabled", "disabled");
}
$(document).ready(function () {
    LoadCountry();
    $("#ReligionId").change(function () {
        var ReligionId = $(this).val();
        BindCasts(ReligionId,0);
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

    var relatives = [];

    $("#btnAddFamilyDetails,#btnAddRelativeDetails").click(function () {
        var btnid = $(this).attr("id");
        var relativedetails="";
        if (btnid == "btnAddFamilyDetails") {
            relativedetails = { RelativeName: $("#txtUncleName").val(), RelativeAddress: $("#txtUncleAddress").val(), Relation: $("#hdnbranding").val() == "SINDHI" ? "Uncle" : "काका", MobileNo: $("#txtUContactNo").val() };
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

    $("#btnSave").click(function () {
        if (ValidateInput()) {
            //validateForm("frmUserProfile");
            //var IsIntercast = document.getElementById('rdoIsInterCast').checked;
            var DOB = $("#DOB").val();
            var Gender = $("#ddlGender").val();
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
            if (document.getElementById("rdoAnnulled").checked == true) {
                MarritalStatus = "5";
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
            var ReligionId = $("#ReligionId").val();
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
            var IsJobOrBusiness = false;
            if (occuption == "Job") {
                IsJobOrBusiness = true;
            }
            var txtCompanyName = $("#txtCompanyName").val();
            var cmbIncome = $("#cmbIncome").val();
            var txtFatherName = $("#txtFatherName").val();
            var txtMotherName = $("#txtMotherName").val();
            var txtNoofBrothers = $("#txtNoofBrothers").val();
            var txtNoofSisters = $("#txtNoofSisters").val();
            var weight = $("#weight").val();
            var txtBirthName = $("#txtBirthName").val();
            var txtFatherMobileNo = $("#txtFatherMobileNo").val();

            var foccuption = $("input:radio[name='foccuption']:checked").val();

            var IsJob = foccuption == "Job" ? true : false;
            var JobBusinessInfo = $("#txtFCompanyName").val();
            var txtFathersIncome = $("#cmbFIncome").val();
            var City = $("#City").val();
            var txtCompanyLocation = $("#txtCompanyLocation").val();
            var IdentificationMark = $("#IdentificationMark").val();
            var Address = $("#txtAddress").val();
            var State = $("#StateId").find(":selected").text();
            var BirthCountry = $("#BirthCountry").find(":selected").text();
            var BirthState = $("#BirthState").find(":selected").text();
            var Country = $("#CountryId").find(":selected").text();
            var txtHobbies = $("#txtHobbies").val();
            var txtActivities = $("#txtActivities").val();
            var MobileNo = $("#MobileNo").val();
            var txtUserExpectation = $("#txtUserExpectation").val();
            var family =
                {
                    FathersName: txtFatherName,
                    MothersName: txtMotherName,
                    NoofBrothers: txtNoofBrothers,
                    IsJob: IsJob,
                    FathersIncome: txtFathersIncome,
                    JobBusinessInfo: $("#txtFCompanyName").val(),
                    MobileNo: $("#txtFatherMobileNo").val(),
                    NoOfSisters: $("#txtNoofSisters").val(),
                    NoOfMBro: $("#txtNoofMBrothers").val(),
                    NoOfMSis: $("#txtNoofMSisters").val(),
                };
            var IsManglik = $("input:radio[name='Manglik']:checked").val();

            var JobDetails =
                {
                    IsJobOrBusiness: IsJobOrBusiness,
                    CompanyName: txtCompanyName,
                    JobLocation: $("#txtCompanyLocation").val(),
                    Income: cmbIncome,
                    IsManglik: IsManglik
                };
            var model =
                {
                    UserId: $("#UserId").val(),
                    Email: $("#Email").val(),
                    BloodGroupId: BloodGroupId, ReligionId: ReligionId, CasteId: CasteId,
                    OrasId: OrasId, Gender: Gender, DateOfBirth: DOB,
                    Address: Address,
                    HeightId: HeightId, Weight: weight, IdentificationMark: IdentificationMark,
                    //PANNO: PANNO,
                    MobileNo: MobileNo, IsHandiCapped: rdoIsHandicapped,
                    HandicapedType: disabledesc, Color: cmbColor, MarritalStatus: MarritalStatus,
                    NoOfChildrens: ChildCount,
                    ChildLivingStatus: ChildLivingStatus,
                    PlaceofBirth: PlaceOfBirth,
                    strTimeofBirth: TimeofBirth, BodyType: bodytype,
                    Qualification: QualificationId, Income: cmbIncome, BirthName: txtBirthName,
                    GotraName: gotradesc, Hobbies: txtHobbies,
                    //UserExpectation: txtUserExpectation,
                    //IsIntercast: IsIntercast,
                    City: City,
                    Country: Country,
                    Subcaste: Subcaste,
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

            var cmbRelations = $("#cmbRelations").val();

            var txtRelativeName = $("#txtRelativeName").val();
            var txtRelativeAddress = $("#txtRelativeAddress").val();
            if ((txtRelativeName != "" || txtRelativeAddress != "") && cmbRelations != "") {
                relatives.push({ RelativeName: $("#txtRelativeName").val(), RelativeAddress: $("#txtRelativeAddress").val(), Relation: cmbRelations, MobileNo: $("#txtRContactNo").val() });
            }
            var txtUncleName = $("#txtUncleName").val();
            var txtUncleAddress = $("#txtUncleAddress").val();
            if (txtUncleName != "" || txtUncleAddress != "") {
                relatives.push({ RelativeName: txtUncleName, RelativeAddress: txtUncleAddress, Relation: $("#hdnbranding").val() == "SINDHI" ? "Uncle." : "काका", MobileNo: $("#txtUContactNo").val() });
            }
            relatives = JSON.stringify(relatives);

            var spinner = new Spinner().spin();
            document.getElementById("contentdiv").appendChild(spinner.el);
            model = JSON.stringify(model);
            family = JSON.stringify(family);
            JobDetails = JSON.stringify(JobDetails);
            ShowLoader();
            $.ajax({
                cache: false,
                type: 'post',
                url: "../userprofile/updateuserbyadmin",
                data: {
                    "model": model,
                    "family": family,
                    "JobDetails": JobDetails,
                    "relatives": relatives,
                },
                success: function (data) {
                    if (data.Status == true) {
                        var objshowcustomalert = new ShowCustomAlert({
                            title: "",
                            message: $("#hdnbranding").val() == "sindhi" ? "information completed successfully." : "माहिती पूर्ण झाली.",
                            type: "alert",
                            onokclick: function () {
                                window.location = getvirtualdirectory() + '/userprofile/index';
                            }
                        });
                        objshowcustomalert.ShowCustomAlertBox();

                    }
                    else {
                        var objshowcustomalert = new ShowCustomAlert({
                            title: "",
                            message: data.errormessage,
                            type: "alert",
                        });
                        objshowcustomalert.ShowCustomAlertBox();
                    }
                    HideLoader();
                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    document.getelementbyid("contentdiv").removechild(spinner.el);
                }
            });
        }
        
    });
})

function BindCasts(ReligionId, CastId)
{
    $('#CasteId').empty();
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
                $("#CasteId").val(CastId);
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error during process: \n' + xhr.responseText);
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    }
}

function bindData() {
    var userdata = JSON.parse($("#UserDetails").val());

    //console.log(userdata);
    $("#MobileNo").val(userdata.MobileNo);
    $("#UserId").val(userdata.UserId);
    $("#ddlGender").val(userdata.Gender);
    switch (userdata.MarritalStatus) {
        case 1:
            $("input[name=marritalstatus][value=1]").attr('checked', 'checked');
            break;
        case 2:
            $("input[name=marritalstatus][value=2]").attr('checked', 'checked');
            break
        case 3:
            $("input[name=marritalstatus][value=3]").attr('checked', 'checked');
            break
        case 4:
            $("input[name=marritalstatus][value=4]").attr('checked', 'checked');
            break
        case 5:
            $("input[name=marritalstatus][value=5]").attr('checked', 'checked');
            break
    }
    var now = new Date(userdata.DateOfBirth);

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $("#DOB").val(today);
    $("#FirstName").val(userdata.FirstName);
    $("#MiddleName").val(userdata.MName);
    $("#LastName").val(userdata.LName);
    $("#Email").val(userdata.MailId);
    
    $("#Taluka").val(userdata.Taluka);
    $("#District").val(userdata.District);
    
    if (userdata.Jobdata == 0) {
        $("input[name=occuption][value=0]").attr('checked', 'checked');
    }
    else if (userdata.Jobdata == 1) {
        $("input[name=occuption][value=1]").attr('checked', 'checked');
    }
    else {
        $("input[name=occuption][value=2]").attr('checked', 'checked');
    }

    if (userdata.IsOwnHouse == 1) {
        $("input[name=IsOwnHouse][value=1]").attr('checked', 'checked');
    }
    else if (userdata.IsOwnHouse == 0) {
        $("input[name=IsOwnHouse][value=0]").attr('checked', 'checked');
    }

    if (userdata.IsSpec == true) {
        $("input[name=IsSpec][value=true]").attr('checked', 'checked');
    }
    else if (userdata.IsSpec == false) {
        $("input[name=IsSpec][value=false]").attr('checked', 'checked');
    }

    if (userdata.IsManglik == 0) {
        $("input[name=Manglik][value=0]").attr('checked', 'checked');
    }
    else if (userdata.IsSpec == 1) {
        $("input[name=Manglik][value=1]").attr('checked', 'checked');
    }

    $("#txtFatherMobileNo").val(userdata.Expr2);
    $("#txtNoofMBrothers").val(userdata.NoOfMBro);
    $("#txtNoofMSisters").val(userdata.NoOfMSis);
    $("#txtGFatherName").val(userdata.GrandFatherName);
    $("#txtActivities").val(userdata.Achievements);

    $("#txtCompanyName").val(userdata.CompanyName);
    $("#JobLocation").val(userdata.JobLocation);
    $("#cmbIncome").val(userdata.Expr1);
    $("#Subcaste").val(userdata.SubCaste);
    $("#CountryId option:contains(" + userdata.Country + ")").attr('selected', 'selected');
    $("#StateId option:contains(" + userdata.State + ")").attr('selected', 'selected');
    $("input[name=bodytype][value='" + userdata.BodyType + "']").attr('checked', 'checked');
    
    $("#ReligionId").val(userdata.ReligionId);
    $("#txtAddress").val(userdata.Address);
    $("#City").val(userdata.City);
    BindCasts(userdata.ReligionId, userdata.CasteId)
    
    $("#ReferenceName").val(userdata.ReferenceName);
    $("#ReferenceContact").val(userdata.ReferenceContact);


    
    $("#PlaceOfBirth").val(userdata.PlaceofBirth);
    $("#BirthDistrict").val(userdata.BirthDistrict);
    $("#Pincode").val(userdata.Pincode);


    $("#TimeofBirth").val(userdata.TimeofBirth);
    $("#txtBirthName").val(userdata.BirthName);
    $("#Taluka").val(userdata.Taluka);
    $("#District").val(userdata.District);
    $("#OrasId").val(userdata.OrasId);
    $("#gotradesc").val(userdata.Gotra);
    $("#HeightId").val(userdata.HeightId);
    $("#weight").val(userdata.Weight);
    $("#BloodGroupId").val(userdata.BloodGroupId);
    $("#cmbColor").val(userdata.Color);
    $("#QualificationId").val(userdata.Qualification);
    //
    $("#FMobileNo").val(userdata.Expr2);
    if (userdata.IsJob==true) {
        $("input[name=foccuption][value=Job]").attr('checked', 'checked');
    }
    else {
        $("input[name=foccuption][value=Business]").attr('checked', 'checked');
    }
    
    $("#txtFatherName").val(userdata.FathersName);
    $("#txtMotherName").val(userdata.MothersName);
    $("#txtNoofBrothers").val(userdata.BotherInfo);
    $("#txtNoofSisters").val(userdata.SisterInfo);
    $("#cmbFIncome").val(userdata.FathersIncome);
    $("#txtFCompanyName").val(userdata.JobBusinessInfo);
    $("#FJobLocation").val(userdata.JobBusinessInfo1);
    $("#cmbFIncome").val(userdata.FathersIncome);
    LoadRelativeDetails(userdata.UserId);
}


function LoadRelativeDetails(UserId)
{
    ShowLoader();
    $.ajax({
        cache: false,
        type: 'POST',
        url: GetVirtualDirectory() + '/UserProfile/GetRelativeDetails',
        data: { UserId: UserId },
        success: function (data) {
            var items1 = "";

            $.each(data, function (i, item) {
                items1 += "<tr><td>" + item.RelativeName + "</td><td>" + item.RelativeAddress + "</td><td>" + item.MobileNo + "</td></tr>";
            });
            $("#tblUncle").append(items1);
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function ValidateInput()
{
    var valid = true;
    if ($("#hdnbranding").val() != "SINDHI") {
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
    if (valid == true) {
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
            if (txtFatherMobileNo.length != 10) {
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
            if (txtNoofBrothers.length > 1) {
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
            if (parseInt(txtNoofBrothers) < parseInt(txtNoofMBrothers)) {
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
        var txtNoofMSisters = $("#txtNoofMSisters").val();
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

function LoadStates(cid) {
    ShowLoader();
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
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function LoadBirthStates(cid) {
    ShowLoader();
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
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function LoadCountry() {
    ShowLoader();
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
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //document.getElementById("contentdiv").removeChild(spinner.el);
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}
