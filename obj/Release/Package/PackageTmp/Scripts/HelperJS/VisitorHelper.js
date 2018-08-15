$(document).ready(function () {
    $("#btnFirst,#btnNext,#btnPrev,#btnLast").click(function () {
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        var btnId = $(this).attr("id");
        var urluse = "";
        if (btnId == "btnFirst") {
            urluse = "../UserProfile/VisitorFirst";
        }
        if (btnId == "btnNext") {
            urluse = "../UserProfile/VisitorNext";
        }
        if (btnId == "btnPrev") {
            urluse = "../UserProfile/VisitorPrev";
        }
        if (btnId == "btnLast") {
            urluse = "../UserProfile/VisitorLast";
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
                    bindVisitorData(students.VisitorDetails);
                }
                else {
                    var objShowCustomAlert = new ShowCustomAlert({
                        Title: "",
                        Message: students.ErrorMessage,
                        Type: "alert",
                    });
                    objShowCustomAlert.ShowCustomAlertBox();
                }
                //var spinner = new Spinner().spin();
                document.getElementById("contentdiv").removeChild(spinner.el);
            },
            error: function (data) {
                //var spinner = new Spinner().spin();
                document.getElementById("contentdiv").removeChild(spinner.el);
            }
        });
    });
})

function bindVisitorData(SubCategory)
{
    //var table = document.getElementById("SubCategoryInfo");
    
    var html="";
    $.each(SubCategory, function (i, item) {
        html += '<tr class="active"><td>' + item.VisitedUserId + '</td><td>' + item.UserName + '</td><td>' + item.Address + '</td><td>' + item.ReligionName + '</td><td><input type="button" class="btn-success btn" value="View Profile" onclick="ViewProfile(' + item.VisitedUserId + ');" /></td><td><input type="button" class="btn-success btn" value="Approve Request" onclick="ApproveRequestSPMO(' + item.VisitorId + ');" /></td></tr>';
    })
    //html += "</tbody>";
    $("#visitordata").html(html);
}