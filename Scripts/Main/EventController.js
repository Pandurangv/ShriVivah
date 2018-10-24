VarmalaVivahApp.factory('EventService', ['$http', function ($http) {
    var urlBase = GetVirtualDirectory();
    var EventService = {};

    EventService.getEventDetails = function () {
        return $http.get(urlBase + "/EventManagement/GetEvents");
    };
    return EventService;
}]);
VarmalaVivahApp.controller("EventController", ['$scope', '$http', '$filter', 'EventService', function ($scope, $http, $filter, EventService) {

    $scope.ErrorMessage = "";
    $scope.ErrorModel =
    {
        IsEventName: false,
        IsEventLocation: false,
        IsEventDate: false,
        IsEventDistrict: false,
        IsEventState: false,
        IsOrganizedBy: false,
        IsOrganizerName: false,
        IsContactPerson: false,
        IsMobileNo: false,
        IsContactNoLength:false,
    };
    $scope.MainEventList = [];

    $scope.EventList = [];

    $scope.SearchEventList = [];
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.AgentList = [];

    $scope.Details = true;
    $scope.Add = false;
    $scope.Edit = false;

    $scope.FilterList = function () {
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.EventList = $scope.MainEventList.filter(function (actype) {
            return (reg.test(actype.EventName.toLowerCase()) || reg.test(actype.EventLocation.toLowerCase()) || reg.test(actype.ContactPerson.toLowerCase()));
        });
        $scope.First();
    }

    $scope.AddNewUI = function (isedit) {
        //$("#ddlPType").val(0);
        $scope.EventModel = { EventName: "", EventLocation: "", EventDate: "", EventDistrict: "", EventState: "", OrganizedBy: "", ContactPerson: "", MobileNo: "" };

        $("#EventDate").val("");
        $scope.Details = false;
        $scope.Add = true;
        $scope.Edit = false;
        $scope.Report = false;
    }

    $scope.Cancel = function (isedit) {
        //$("#ddlPType").val(0);
        $scope.EventModel = { EventName: "", EventLocation: "", EventDate: "", EventDistrict: "", EventState: "", OrganizedBy: "", ContactPerson: "", MobileNo: "" };

        $("#EventDate").val("");
        $scope.Details = true;
        $scope.Add = false;
        $scope.Edit = false;
        $scope.Report = false;
    }

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchEventList = $filter('limitTo')($scope.EventList, $scope.Paging, 0);
        if ($scope.EventList.length == 0) {
            $scope.SearchEventList = $scope.EventList;
        }
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchEventList = $filter('limitTo')($scope.EventList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
            findAndCallErrorBox("", "Already on first page", "alert", null, null);
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.EventList.length) {
            $scope.SearchEventList = $filter('limitTo')($scope.EventList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            findAndCallErrorBox("", "Already on last page", "alert", null, null);
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.EventList.length;
        var rem = parseInt($scope.EventList.length) % parseInt($scope.Paging);
        var position = $scope.EventList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.EventList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchEventList = $filter('limitTo')($scope.EventList, $scope.Paging, position);
    }


    $scope.GetEventData = function () {
        ShowLoader();
        EventService.getEventDetails()
           .success(function (qualifications) {
               HideLoader();
               //$scope.AgentList = qualifications.AgentList;
               $scope.MainEventList = qualifications.EventList;
               $scope.EventList = $scope.MainEventList;
               $scope.First();
           })
          .error(function (error)
          {
               HideLoader();
               findAndCallErrorBox("", response.data.Error.Message, "alert", null, null);
         });
    }

    $scope.ValidateEventName = function () {
        if ($("#EventName").val() != "") {
            $scope.ErrorModel.IsEventName = false;
        }
    }

    $scope.ValidateEventLocation = function () {
        if ($("#EventLocation").val() != "") {
            $scope.ErrorModel.IsEventLocation = false;
        }
    }

    $scope.ValidateEventDistrict = function () {
        if ($("#EventDistrict").val() != "") {
            $scope.ErrorModel.IsEventDistrict = false;
        }
    }

    $scope.ValidateEventState = function () {
        if ($("#StateId").val() != "") {
            $scope.ErrorModel.IsEventState = false;
        }
    }

    $scope.ValidateOrganizedBy = function () {
        if ($("#AgentId").val() != "") {
            $scope.ErrorModel.IsOrganizedBy = false;
        }
    }

    $scope.ValidateEventDate = function () {
        if ($("#EventDate").val() != "") {
            $scope.ErrorModel.IsEventDate = false;
        }
    }
    
    
    $scope.EventModel = { EventName: "", EventLocation: "", EventDate: "", EventDistrict: "", EventState: "", OrganizedBy: "", ContactPerson: "", MobileNo: "" };


    $scope.SaveEvent = function (model) {
        var reg = new RegExp("[a-zA-Z]");
        
        if ($("#EventName").val() == "") {
           $("#IsEventName").show()
            $scope.ErrorMessage = "Event name should be filled.";
            return false;
        }
        else {
            $("#IsEventName").hide();
        }

        if ($("#EventLocation").val() == "") {
            $("#IsEventLocation").show()
            $scope.ErrorMessage = "Event Location should be filled.";
            return false;
        }
        else {
            $("#IsEventLocation").hide();
        }

        if ($("#EventDistrict").val() == "") {
            $("#IsEventDistrict").show()
            $scope.ErrorMessage = "Event District should be filled.";
            return false;
        }
        else {
            $("#IsEventDistrict").hide();
        }

        if ($("#StateId").val() == "") {
            $("#IsEventState").show()
            $scope.ErrorMessage = "Last name should be filled.";
            return false;
        }
        else {
            $("#IsEventState").hide();
        }
        
        if ($("#EventDate").val() == "") {
            $("#IsEventDate").show()
            $scope.ErrorMessage = "Event Date should be filled.";
            return false;
        }
        else {
            $("#IsEventDate").hide();
        }

        if ($("#ContactNo").val()!="") {
            if ($("#ContactNo").val().length != 10) {
                $("#IsContactNoLength").show()
                $scope.ErrorMessage = "Contact No lenght should be 10 digit.";
                return false;
            }
            else {
                $("#IsContactNoLength").hide();
            }
        }
        
        $("#spanvalidate").hide();
        //ShowLoader();
        var spinner = new Spinner().spin();
        document.getElementById("contentdiv").appendChild(spinner.el);
        $scope.EventModel = {
            EventName: $("#EventName").val().toUpperCase(),
            EventDate: $("#EventDate").val().toUpperCase(),
            EventLocation: $("#EventLocation").val().toUpperCase(),
            OrganizedBy: 1,
            EventDistrict: $("#EventDistrict").val(),
            EventState: $("#StateId").val(),
            ContactPerson: $("#ContactPerson").val(),
            MobileNo: $("#ContactNo").val().toUpperCase(),
            EventImage: $("#EventImagehidden").val(),
        };
        var url = GetVirtualDirectory();

        url = url + '/EventManagement/Save';
        var req = {
            method: 'POST',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            data: $scope.EventModel,
        }

        $http(req).then(function (response) {
            if (response.data.Status == true) {
                var objShowCustomAlert = new ShowCustomAlert({
                    Title: "",
                    Message: "Event registerd successfully.",
                    Type: "alert",
                    OnOKClick: function () {
                        $scope.GetEventData();
                        $scope.Cancel();
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
    
    $scope.init = function () {
        $scope.GetEventData();
    }
    $scope.init();

}]);

