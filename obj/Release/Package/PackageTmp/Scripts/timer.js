(function () {
    // Defining a connection to the server hub.
    var myHub = $.connection.myHub;
    // Setting logging to true so that we can see whats happening in the browser console log. [OPTIONAL]
    $.connection.hub.logging = true;
    // Start the hub
    $.connection.hub.start();

    //setInterval(function () {
    //    myHub.server.readMessage($("#ActiveUserId").val()).done(function (response) {
    //        bindUserMessage(response, $("#ActiveUserId").val());
    //    }).fail(function (response) {
    //        console.log(response);
    //    })        
    //}, 3000);

    //Button click jquery handler
    //$("#btnSendMessage").click(function () {
    //    // Call SignalR hub method
    //    myHub.server.sendMessage($("#MessageText").val(), $("#ActiveUserId").val(), $("#SelectedUserId").val()).done(function (response) {
    //        window.location = GetVirtualDirectory() + "/UserProfile/Index";
    //    }).fail(function (response) {
    //        console.log(response);
    //    })
    //});
}());