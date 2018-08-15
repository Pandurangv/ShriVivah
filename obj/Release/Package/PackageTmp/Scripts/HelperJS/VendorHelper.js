var url=GetVirtualDirectory();
$(document).ready(function () {
    $("#btnFirst,#btnNext,#btnPrev,#btnLast").click(function () {
        var spinner = new Spinner().spin();
        
        document.getElementById("contentdiv").appendChild(spinner.el);
        var btnId = $(this).attr("id");
        var urluse = "";
        if (btnId == "btnFirst") {
            urluse =  url +  "/Vendor/VendorFirst";
        }
        if (btnId == "btnNext") {
            urluse = url +  "/Vendor/VendorNext";
        }
        if (btnId == "btnPrev") {
            urluse = url + '/Vendor/VendorPrev';
        }
        if (btnId == "btnLast") {
            urluse = url+ '/Vendor/VendorLast';
        }
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: urluse,
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    $('#SubCategoryInfo tr').not(function () { if ($(this).has('th').length) { return true } }).remove();
                    if ($("#hdnbranding").val() == "SPMO") {
                        bindUserData(students.VendorListSPMO);
                    }
                    else {
                        bindUserData(students.VendorList);
                    }
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
            }
        });
    });
    $("#btnSearch").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);

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
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            data: { prefix: prefix },
            url: url + "/Vendor/SearchVendor",
            dataType: "json",
            success: function (students) {
                if (students.Status == true) {
                    $('#SubCategoryInfo tr').not(function () { if ($(this).has('th').length) { return true } }).remove();
                    if ($("#hdnbranding").val() == "SPMO") {
                        bindUserData(students.VendorListSPMO);
                    }
                    else {
                        bindUserData(students.VendorList);
                    }
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
            }
        });
    });

    $("#btnReset").click(function () {
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: url + '/Vendor/Reset',
            dataType: "json",
            success: function (students) {
                $("#txtSearch").val("");
                $('#SubCategoryInfo tr').not(function () { if ($(this).has('th').length) { return true } }).remove();
                bindUserData(students);
            }
        });
    });

    $("#btnSave").click(function () {
        SaveVendor();
    });
    $("#btnUpdate").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var model =
            {
                VendorTypeId: $("#VendorTypeId").val(),
                Address: $("#Address").val(),
                BusinessDescription: $("#BusinessDescription").val(),
                City: $("#City").val(),
                ContactNo: $("#ContactNo").val(),
                EmailId: $("#EmailId").val(),
                ExpiryDate: $("#ExpiryDate").val(),
                LogoImage: $("#logoImagehidden").val(),
                OwnerName: $("#OwnerName").val(),
                Pincode: $("#Pincode").val(),
                ProductImage: $("#productImagehidden").val(),
                ProfileImage: $("#ProfileImagehidden").val(),
                RegistrationDate: $("#RegistrationDate").val(),
                VendorName: $("#VendorName").val(),
                VendorId: $("#VendorId").val(),
                Taluka: $("#Taluka").val(),
                District: $("#District").val(),
                State: $("#State").val(),
                Country: $("#Country").val(),
                AgentId: $("#AgentId").val(),
            }
        $.ajax({
            cache: false,
            type: 'POST',
            url: url + '/Vendor/Update',
            data: model,
            success: function (data) {
                if (data.Status == true) {
                    $('#SubCategoryInfo tr').not(function () { if ($(this).has('th').length) { return true } }).remove();
                    if ($("#hdnbranding").val() == "SPMO") {
                        bindUserData(data.VendorListSPMO);
                    }
                    else {
                        bindUserData(data.VendorList);
                    }

                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: data.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                    $("#btnCancel").trigger("click");
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
                alert('Error during process: \n' + xhr.responseText);
            }
        });
    });

    $("#btnCancel").click(function () {
        $("#createDiv").hide();
        $("#divMain").show();
        clearValues();
        return false;
    });
    $("#btnAdd").click(function () {
        $("#createDiv").show();
        $("#divMain").hide();
        clearValues();
        $("#btnSave").show();
        $("#btnUpdate").hide();
        return false;
    });
});

var bindUserData = function (SubCategory) {
    var table = document.getElementById("SubCategoryInfo");
    var tbody = document.createElement("tbody");
    $.each(SubCategory, function (i, SubCategorydata) {
        var row = document.createElement("tr");

        var cell = document.createElement("td");
        $(cell).css("display", "none");
        cell.innerHTML = SubCategorydata.VendorId;
        row.appendChild(cell);

        cell = document.createElement("td");
        cell.innerHTML = SubCategorydata.VendorName;
        row.appendChild(cell);

        cell = document.createElement("td");
        cell.innerHTML = SubCategorydata.Address;
        row.appendChild(cell);

        cell = document.createElement("td");
        cell.innerHTML = SubCategorydata.City;
        row.appendChild(cell);

        cell = document.createElement("td");
        cell.innerHTML = SubCategorydata.ContactNo;
        row.appendChild(cell);

        cell = document.createElement("td");
        if (SubCategorydata.RegistrationDate!=null) {
            var date = new Date(parseInt(SubCategorydata.RegistrationDate.substr(6)));
            cell.innerHTML = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
        }
        row.appendChild(cell);
            
        cell = document.createElement("td");
        if (SubCategorydata.ExpiryDate!=null) {
            date = new Date(parseInt(SubCategorydata.ExpiryDate.substr(6))); //SubCategorydata.;
            cell.innerHTML = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
        }
        row.appendChild(cell);
        if ($("#hdnbranding").val() != "SPMO") {
            cell = document.createElement("td");
            var btn = document.createElement("input");
            btn.id = SubCategorydata.VendorId;
            $(btn).attr("type", "button");
            $(btn).addClass("btn-success btn");
            $(btn).val("बदल करा");
            $(btn).attr("onclick", "EditVendor(" + SubCategorydata.VendorId + ");")
            cell.appendChild(btn);
            row.appendChild(cell);
        }

        cell = document.createElement("td");
        btn = document.createElement("input");
        btn.id = SubCategorydata.VendorId;
        $(btn).attr("type", "button");
        $(btn).addClass("btn-success btn");
        if ($("#hdnbranding").val() == "SPMO") {
            if (SubCategorydata.IsApproved == true || SubCategorydata.IsApproved == 'true') {
                $(btn).val("Deactivate");
                $(btn).attr("onclick", "ActiveDeactiveVendor(" + SubCategorydata.VendorId + ",false,this);")
            }
            else {
                $(btn).val("Activate");
                $(btn).attr("onclick", "ActiveDeactiveVendor(" + SubCategorydata.VendorId + ",true,this);")
            }
        }
        else {
            if (SubCategorydata.IsActive == true || SubCategorydata.IsActive == 'true') {
                $(btn).val("रद्द करा");
                $(btn).attr("onclick", "ActiveDeactiveVendor(" + SubCategorydata.VendorId + ",false,this);")
            }
            else {
                $(btn).val("सक्रिय करा");
                $(btn).attr("onclick", "ActiveDeactiveVendor(" + SubCategorydata.VendorId + ",true,this);")
            }
        }
        
        cell.appendChild(btn);

        row.appendChild(cell);
        tbody.appendChild(row);
        table.appendChild(tbody);
    });
};

function SaveVendor()
{
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var model =
        {
            VendorTypeId: $("#VendorTypeId").val(),
            Address: $("#Address").val(),
            BusinessDescription: $("#BusinessDescription").val(),
            City: $("#City").val(),
            ContactNo: $("#ContactNo").val(),
            EmailId: $("#EmailId").val(),
            ExpiryDate: $("#ExpiryDate").val(),
            LogoImage: $("#logoImagehidden").val(),
            OwnerName: $("#OwnerName").val(),
            Pincode: $("#Pincode").val(),
            ProductImage: $("#productImagehidden").val(),
            ProfileImage: $("#ProfileImagehidden").val(),
            RegistrationDate: $("#RegistrationDate").val(),
            VendorName: $("#VendorName").val(),
            Taluka: $("#Taluka").val(),
            District: $("#District").val(),
            State: $("#State").val(),
            Country: $("#Country").val(),
            AgentId: $("#AgentId").val(),
        }
    $.ajax({
        cache: false,
        type: 'POST',
        url: url + '/Vendor/AddVendor',
        data: model,
        success: function (data) {
            if (data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Vendor inserted successfully.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                var table = document.getElementById("SubCategoryInfo")
                if (table!=null) {
                    $('#SubCategoryInfo tr').not(function () { if ($(this).has('th').length) { return true } }).remove();
                    bindUserData(data.VendorList);
                    $("#btnCancel").trigger("click");
                }
                else {
                    window.location = url;
                }
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
            alert('Error during process: \n' + xhr.responseText);
        }
    });
}

function ActiveDeactiveVendor(VendorId, IsActive,btn) {
    //var IsActive = true;
    //if ($(btn).val() == "रद्द करा") {
    //    IsActive = false;
    //}
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    if (VendorId > 0) {
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            data: { VendorId: VendorId, IsActive: IsActive },
            url: url + '/Vendor/ActiveDeactiveVendor',
            dataType: "json",
            success: function (students) {
                if (students==true) {
                    if ($("#hdnbranding").val() == "SPMO") {
                        if (IsActive == false) {
                            $(btn).attr("onclick", "ActiveDeactiveVendor(" + VendorId + ",this)");
                            $(btn).val("Activate");
                        }
                        else {
                            $(btn).attr("onclick", "ActiveDeactiveVendor(" + VendorId + ",this)");
                            $(btn).val("Deactivate");
                        }
                    }
                    else {
                        if (IsActive == false) {
                            $(btn).attr("onclick", "ActiveDeactiveVendor(" + VendorId + ",this)");
                            $(btn).val("सक्रिय करा");
                        }
                        else {
                            $(btn).attr("onclick", "ActiveDeactiveVendor(" + VendorId + ",this)");
                            $(btn).val("रद्द करा");
                        }
                    }
                }
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (er) {
                document.getElementById("contentdiv").removeChild(spinner.el);
                alert(er);
            }
        });
    }
}

function EditVendor(VendorId)
{
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    if (VendorId > 0) {
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            data: { VendorId: VendorId },
            url:  url  + '/Vendor/Edit',
            dataType: "json",
            success: function (students) {
                $("#btnAdd").trigger("click");
                $("#VendorName").val(students.VendorName);
                $("#BusinessDescription").val(students.BusinessDescription);
                if (students.RegistrationDate!=null) {
                    var date = new Date(parseInt(students.RegistrationDate.substr(6)));
                    var day = ("0" + date.getDate()).slice(-2);
                    var month = ("0" + (date.getMonth() + 1)).slice(-2);

                    var today = date.getFullYear() + "-" + (month) + "-" + (day);

                    $("#RegistrationDate").val(today);
                }
                $("#ValidDays").val(students.ValidDays);
                if (students.ExpiryDate!=null) {
                    date = new Date(parseInt(students.ExpiryDate.substr(6)));
                    day = ("0" + date.getDate()).slice(-2);
                    month = ("0" + (date.getMonth() + 1)).slice(-2);

                    today = date.getFullYear() + "-" + (month) + "-" + (day);
                    $("#ExpiryDate").val(today);
                }
                $("#OwnerName").val(students.OwnerName);
                $("#Address").val(students.Address);
                $("#City").val(students.City);
                $("#ContactNo").val(students.ContactNo);
                $("#EmailId").val(students.EmailId);
                $("#Pincode").val(students.Pincode);
                $("#VendorTypeId").val(students.VendorTypeId);
                $("#VendorId").val(students.VendorId);
                $("#Taluka").val(students.Taluka);
                $("#District").val(students.District);
                $("#State").val(students.State);
                $("#Country").val(students.Country);
                if (students.Agent!=null) {
                    $("#AgentId").val(students.Agent.UserId);
                }
                    
                $("#btnSave").hide();
                $("#btnUpdate").show();
                //var spinner = new Spinner().spin();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    }
}



function clearValues() {
    $("#VendorName").val("");
    $("#BusinessDescription").val("");
    $("#RegistrationDate").val("");
    $("#ValidDays").val("");
    $("#ExpiryDate").val("");
    $("#OwnerName").val("");
    $("#Address").val("");
    $("#City").val("");
    $("#ContactNo").val("");
    $("#EmailId").val("");
    $("#Pincode").val("");
    $("#Taluka").val("");
    $("#District").val("");
    $("#State").val("");
    $("#Country").val("");
    $("#VendorTypeId").val(0);
    $("#VendorId").val(0);
}

function Upload(ImageType) {
    var spinner = new Spinner().spin();
    document.getElementById("contentdiv").appendChild(spinner.el);
    var data = new FormData();
    var files;
    var urlpoint;
    if (ImageType == "LP") {
        files = $("#logoImage").get(0).files;
        urlpoint = url + "/Vendor/UploadLogo";
    }
    if (ImageType == "DP") {
        files = $("#ProfileImage").get(0).files;
        urlpoint = url + "Vendor/UploadProfile";
    }
    if (ImageType == "PP") {
        files = $("#productImage").get(0).files;
        urlpoint = url + "/Vendor/UploadProduct";
    }

    if (files.length > 0 && ImageType == "LP") {
        data.append("logoImage", files[0]);
    }
    if (files.length > 0 && ImageType == "DP") {
        data.append("ProfileImage", files[0]);
    }
    if (files.length > 0 && ImageType == "PP") {
        data.append("productImage", files[0]);
    }

    $.ajax({
        url: urlpoint,
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            if (response.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Image uploaded successfully.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
                if (ImageType == "LP") {
                    $("#logoImagehidden").val(response.FilePath);
                }
                if (ImageType == "DP") {
                    $("#ProfileImagehidden").val(response.FilePath);
                }
                if (ImageType == "PP") {
                    $("#productImagehidden").val(response.FilePath);
                }
            }
            else {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Not able to upload image.",
                    Type: "alert",
                });
                objShowCustomAlert.ShowCustomAlertBox();
            }
            document.getElementById("contentdiv").removeChild(spinner.el);
        },
        error: function (er) {
            alert(er);
        }
    });
}