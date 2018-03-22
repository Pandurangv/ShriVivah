function validateForm(frm)
{
    var valid=true;
    $("#" +frm).each(function () {
        var inputcontrolarr = $(this).find(':input');
        for (var i = 0; i < inputcontrolarr.length; i++) {
            var inputcontrol = inputcontrolarr[i];
            if ($(inputcontrol).attr("type") == "text") {
                valid = validateallAttribute(inputcontrol, "text");
            }
            if ($(inputcontrol).attr("type") == "select") {
                valid = validateallAttribute(inputcontrol, "select");
            }
            if ($(inputcontrol).attr("type") == "textarea") {
                valid = validateallAttribute(inputcontrol, "textarea");
            }
            if ($(inputcontrol).attr("type") == "email") {
                valid = validateallAttribute(inputcontrol, "email");
            }
            if ($(inputcontrol).attr("type") == "date") {
                valid = validateallAttribute(inputcontrol, "date");
            }
            if ($(inputcontrol).attr("type") == "radio") {
                valid = validateallAttribute(inputcontrol, "radio");
            }
            if ($(inputcontrol).attr("type") == "time") {
                valid = validateallAttribute(inputcontrol, "time");
            }
            if ($(inputcontrol).attr("type") == "number") {
                valid = validateallAttribute(inputcontrol, "number");
            }
            if ($(inputcontrol).attr("type") == "file") {
                valid = validateallAttribute(inputcontrol, "file");
            }
            if ($(inputcontrol).attr("type") == "password") {
                valid = validateallAttribute(inputcontrol, "password");
            }
            if ($(inputcontrol).attr("type") == "tel") {
                valid = validateallAttribute(inputcontrol, "tel");
            }
        }
        
    });
    if (valid==false) {
        return valid;
    }
}

function validateallAttribute(inputcontrol,typeofcontrol)
{
    var valid = true;
    $(inputcontrol).each(function () {
        $.each(this.attributes, function () {
            if (this.specified) {
                //console.log(this.name, this.value);
                switch (this.name.toLowerCase()) {
                    case "maxlength":
                        if ($(inputcontrol).val().length>this.value) {
                            alert("Content length should be less than" + this.value);
                            $(inputcontrol).val("");
                            valid = false;
                        }
                        break;
                    case "max":
                        if (parseInt($(inputcontrol).val()) > this.value) {
                            alert("Maximum value should be less than" + this.value);
                            $(inputcontrol).val("");
                            valid = false;
                        }
                        break;
                    case "min":
                        if (parseInt($(inputcontrol).val()) < this.value) {
                            alert("Minimum value should be less than" + this.value);
                            $(inputcontrol).val("");
                            valid = false;
                        }
                        break;
                    case "required":
                        switch (typeofcontrol) {
                            case "radio":
                                var namerdo = $(inputcontrol).attr("name");
                                var val = $("input:radio[name='" + namerdo + "']:checked").val();
                                if (val=="") {
                                    alert("Please select option.");
                                    valid = false;
                                }
                                break;
                            case "select":
                                var val = $(inputcontrol).val();
                                if (val==0) {
                                    alert("Please select option.");
                                    valid = false;
                                }
                                break;
                            default:
                                var val = $(inputcontrol).val();
                                if (val == "") {
                                    alert("Please fill input value.");
                                    valid = false;
                                }
                                break;
                        }
                        break;
                    case "maxdate":
                        var inputdate = new Date($(inputcontrol).val());
                        var maxdate = new Date(this.value);
                        if (inputdate>maxdate) {
                            alert("Minimum value should be less than" + this.value);
                            $(inputcontrol).val("");
                            valid = false;
                        }
                        break;
                    case "mindate":
                        var inputdate = new Date($(inputcontrol).val());
                        var mindate = new Date(this.value);
                        if (inputdate > mindate) {
                            alert("Minimum value should be less than" + this.value);
                            $(inputcontrol).val("");
                            valid = false;
                        }
                        break;

                }
            }
            if (valid==false) {
                return valid;
            }
        });
    });
}

