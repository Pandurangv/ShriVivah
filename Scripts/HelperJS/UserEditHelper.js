function DisableControls()
{
    $("#noofchilds").attr("disabled", "disabled");
    $("#rdochildLWM").attr("disabled", "disabled");
    $("#rdochildNLWM").attr("disabled", "disabled");
    //$("#txtCompanyName").attr("disabled", "disabled");
    $("#disabledesc").attr("disabled", "disabled");
}
$(document).ready(function () {
    $("#ReligionId").change(function () {
        var ReligionId = $(this).val();
        BindCasts(ReligionId,0);
    });
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
            relativedetails = { RelativeName: $("#txtUncleName").val(), RelativeAddress: $("#txtUncleAddress").val(), Relation: $("#hdnbranding").val() == "SPMO" ? "Uncle" : "काका", MobileNo: $("#txtUContactNo").val() };
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
        var Gender = document.getElementById("rdoMale").checked == true ? "M" : "F";
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
        var DOB = $("#DOB").val();
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
        if (occuption == "Job")
        {
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
        var Country = $("#CountryId").find(":selected").text();
        var MobileNo = $("#MobileNo").val();
        var txtUserExpectation = $("#txtUserExpectation").val();
        var family =
            {
                FathersName: txtFatherName,
                MothersName: txtMotherName,
                NoofBrothers: txtNoofBrothers,
                MobileNo: txtFatherMobileNo,
                IsJob: IsJob,
                FathersIncome: txtFathersIncome,
                JobBusinessInfo: $("#txtFCompanyName").val(),
                MobileNo: $("#FMobileNo").val(),
                NoOfSisters: $("#txtNoofSisters").val(),
            };
        var JobDetails =
            {
                IsJobOrBusiness: IsJobOrBusiness,
                CompanyName: txtCompanyName,
                JobLocation: $("#JobLocation").val(),
                Income: cmbIncome
            };
        var model =
            {
                FirstName: $("#FirstName").val(),
                MiddleName: $("#MiddleName").val(),
                LastName: $("#LastName").val(),
                Email: $("#Email").val(),
                MiddleName: $("#MiddleName").val(),
                Taluka:$("#Taluka").val(),
                District:$("#District").val(),
                UserId:$("#userid").val(),
                BloodGroupId: BloodGroupId,
                ReligionId: ReligionId,
                CasteId: CasteId,
                OrasId: OrasId,
                Gender: Gender,
                DateOfBirth: DOB,
                Address: Address,
                HeightId: HeightId,
                Weight: weight,
                MobileNo: MobileNo,
                IsHandiCapped: rdoIsHandicapped,
                HandicapedType: disabledesc,
                Color: cmbColor,
                MarritalStatus: MarritalStatus,
                NoOfChildrens: ChildCount,
                ChildLivingStatus: ChildLivingStatus,
                PlaceofBirth: PlaceOfBirth,
                strTimeofBirth: TimeofBirth,
                BodyType: bodytype,
                Qualification: QualificationId,
                Income: cmbIncome, BirthName: txtBirthName,
                GotraName: gotradesc, 
                IsIntercast: IsIntercast,
                City: City,
                Country: Country,
                State: State,
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
            relatives.push({ RelativeName: txtUncleName, RelativeAddress: txtUncleAddress, Relation:$("#hdnbranding").val() == "SPMO" ? "Uncle.": "काका", MobileNo: $("#txtUContactNo").val() });
        }
        relatives = JSON.stringify(relatives);

        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        model = JSON.stringify(model);
        family = JSON.stringify(family);
        JobDetails = JSON.stringify(JobDetails);
        
        $.ajax({
            cache: false,
            type: 'POST',
            url: "../UserProfile/UpdateUserByAdmin",
            data: {
                "model": model,
                "family": family,
                "JobDetails": JobDetails,
                "relatives": relatives,
            },
            success: function (data) {
                if (data.Status == true) {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: $("#hdnbranding").val() == "SPMO" ? "Information completed successfully." : "माहिती पूर्ण झाली.",
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
                if ($("#hdnbranding").val() == "SPMO") {
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
    $("#userid").val(userdata.UserId);
    if (userdata.Gender=="M") {
        $("input[name=gender][value=Male]").attr('checked', 'checked');
    }
    else {
        $("input[name=gender][value=Female]").attr('checked', 'checked');
    }
    switch (userdata.MarritalStatus) {
        case 1:
            $("input[name=marritalstatus][value=Unmarried]").attr('checked', 'checked');
            break;
        case 2:
            $("input[name=marritalstatus][value=Widow]").attr('checked', 'checked');
            break
        case 3:
            $("input[name=marritalstatus][value=Divorcee]").attr('checked', 'checked');
            break
        case 4:
            $("input[name=marritalstatus][value=Separated]").attr('checked', 'checked');
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
    
        if (userdata.IsJobOrBusiness == true) {
            $("input[name=occuption][value=Job]").attr('checked', 'checked');
        }
        else {
            $("input[name=occuption][value=Business]").attr('checked', 'checked');
        }
        $("#txtCompanyName").val(userdata.CompanyName);
        $("#JobLocation").val(userdata.JobLocation);
        $("#cmbIncome").val(userdata.Expr1);
    
    $("#CountryId option:contains(" + userdata.Country + ")").attr('selected', 'selected');
    $("#StateId option:contains(" + userdata.State + ")").attr('selected', 'selected');
    $("input[name=bodytype][value='" + userdata.BodyType + "']").attr('checked', 'checked');
    
    $("#ReligionId").val(userdata.ReligionId);
    $("#txtAddress").val(userdata.Address);
    $("#City").val(userdata.City);
    BindCasts(userdata.ReligionId, userdata.CasteId)
    if (userdata.IsIntercast==true) {
        $("#rdoIsInterCast").attr("checked", true);
    }
    
    $("#PlaceOfBirth").val(userdata.PlaceofBirth);
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
}
