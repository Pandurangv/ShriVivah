google.load("elements", "1", { packages: "transliteration" });

function onLoad() {
    var options = {
        //Source Language
        sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
        // Destination language to Transliterate
        destinationLanguage: [google.elements.transliteration.LanguageCode.MARATHI],
        shortcutKey: 'ctrl+g',
        transliterationEnabled: true
    };
    var control = new google.elements.transliteration.TransliterationControl(options);
    if ($("#pagename").val() == "registeruser") {
        control.makeTransliteratable(['FirstName']);
        control.makeTransliteratable(['MiddleName']);
        control.makeTransliteratable(['LastName']);
    }
    if ($("#pagename").val() == "registervendor") {
        /*control.makeTransliteratable(['VendorName']);
        control.makeTransliteratable(['BusinessDescription']);
        control.makeTransliteratable(['OwnerName']);
        control.makeTransliteratable(['Address']);
        control.makeTransliteratable(['City']);
        control.makeTransliteratable(['Taluka']);
        control.makeTransliteratable(['District']);*/
    }
    if ($("#pagename").val() == "userinfo") {
        control.makeTransliteratable(['PlaceOfBirth']);
        control.makeTransliteratable(['txtBirthName']);
        control.makeTransliteratable(['txtAddress']);
        control.makeTransliteratable(['gotradesc']);
        control.makeTransliteratable(['txtCompanyName']);
        control.makeTransliteratable(['txtCompanyLocation']);
        control.makeTransliteratable(['txtHobbies']);
        control.makeTransliteratable(['txtUserExpectation']);
        control.makeTransliteratable(['txtFatherName']);
        control.makeTransliteratable(['txtMotherName']);
        control.makeTransliteratable(['txtFCompanyName']);
        control.makeTransliteratable(['txtUncleName']);
        control.makeTransliteratable(['txtUncleAddress']);
        control.makeTransliteratable(['City']);
        control.makeTransliteratable(['Taluka']);
        control.makeTransliteratable(['District']);
        control.makeTransliteratable(['txtRelativeName']);
        control.makeTransliteratable(['txtRelativeAddress']);
        control.makeTransliteratable(['Subcaste']);
        //control.makeTransliteratable(['txtRelativeName']);
        control.makeTransliteratable(['txtNoofBrothers']);
        control.makeTransliteratable(['txtNoofSisters']);
        control.makeTransliteratable(["Subcaste"]);
    }
    if ($("#pagename").val() == "edituser") {
        control.makeTransliteratable(['FirstName']);
        control.makeTransliteratable(['MiddleName']);
        control.makeTransliteratable(['LastName']);
        control.makeTransliteratable(['PlaceOfBirth']);
        control.makeTransliteratable(['txtBirthName']);
        control.makeTransliteratable(['txtAddress']);
        control.makeTransliteratable(['gotradesc']);
        control.makeTransliteratable(['txtCompanyName']);
        control.makeTransliteratable(['JobLocation']);
        
        control.makeTransliteratable(['txtFatherName']);
        control.makeTransliteratable(['txtMotherName']);
        control.makeTransliteratable(['txtFCompanyName']);
        control.makeTransliteratable(['City']);
        control.makeTransliteratable(["Subcaste"]);
        control.makeTransliteratable(['Taluka']);
        control.makeTransliteratable(['District']);
        control.makeTransliteratable(['txtNoofBrothers']);
        control.makeTransliteratable(['txtNoofSisters']);
        control.makeTransliteratable(['txtUncleName']);
        control.makeTransliteratable(['txtUncleAddress']);
        control.makeTransliteratable(['txtRelativeName']);
        control.makeTransliteratable(['txtRelativeAddress']);
    }
    if ($("#pagename").val() == "editprofile") {
        control.makeTransliteratable(['txtAddress']);
        control.makeTransliteratable(['City']);
        control.makeTransliteratable(['PlaceOfBirth']);
        control.makeTransliteratable(['txtBirthName']);
        control.makeTransliteratable(['txtCompanyName']);
        control.makeTransliteratable(['txtCompanyLocation']);
        control.makeTransliteratable(['Taluka']);
        control.makeTransliteratable(['District']);
    }
    if ($("#pagename").val() == "userprofiles") {
        control.makeTransliteratable(['txtSearch']);
    }
    if ($("#pagename").val() == "oras") {
        control.makeTransliteratable(['txtSearch']);
        control.makeTransliteratable(['OrasName']);
    }
    if ($("#pagename").val() == "caste") {
        control.makeTransliteratable(['txtSearch']);
        control.makeTransliteratable(['CastName']);
    }
    if ($("#pagename").val() == "religion") {
        control.makeTransliteratable(['txtSearch']);
        control.makeTransliteratable(['ReligionName']);
    }
    if ($("#pagename").val() == "msg") {
        control.makeTransliteratable(['MessageText']);
    }
    if ($("#pagename").val() == "country") {
        control.makeTransliteratable(['txtSearch']);
        control.makeTransliteratable(['CountryName']);
    }
    if ($("#pagename").val() == "agent") {
        control.makeTransliteratable(['FirstName']);
        control.makeTransliteratable(['LastName']);
        control.makeTransliteratable(['City']);
        control.makeTransliteratable(['Taluka']);
        control.makeTransliteratable(['District']);
        control.makeTransliteratable(['State']);
        control.makeTransliteratable(['Address']);
    }
    if ($("#pagename").val() == "vendor") {
        //control.makeTransliteratable(['VendorName']);
        //control.makeTransliteratable(['City']);
        //control.makeTransliteratable(['Taluka']);
        //control.makeTransliteratable(['District']);
        //control.makeTransliteratable(['State']);
        //control.makeTransliteratable(['Country']);
        //control.makeTransliteratable(['BusinessDescription']);
        //control.makeTransliteratable(['OwnerName']);
        //control.makeTransliteratable(['Address']);
    }
    if ($("#pagename").val() == "vedndorsearch")
    {
        //control.makeTransliteratable(['txtSearchVendor']);
    }
}
google.setOnLoadCallback(onLoad);