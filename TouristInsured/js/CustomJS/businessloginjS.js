$(document).ready(function () {
    debugger
    var height = $(window).height();
    var width = $(window).width();
    if (height == 640 && width == 360) {
        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '80%';
            all[i].style.margin = '203% 12% 0 12%';
        }
        //$(".").width("80%");
    }
    else if (width == 768) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '35%';
            all[i].style.margin = '107% 12% 0 33%';
        }

    }
    else if (width == 375) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '80%';
            all[i].style.margin = '204% 12% 0 11%';
        }

    }
    else if (width == 1024) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '27%';
            all[i].style.margin = '109% 16% 0 37%';
        }

    }
})
function mobUpdateEndDate() {
    debugger
    $('#mobtxtCoverageEndDate').val("");
    $('#mobtxtCoverageEndDate').datepicker('destroy');
    var d = new Date($("#mobtxtCoverageStartDate").val());
    var date = d.getDate() + 1;
    var month = d.getMonth(); // Since getMonth() returns month from 0-11 not 1-12.
    var year = d.getFullYear();
    $('#mobtxtCoverageEndDate').datepicker({

        dateFormat: "MM-dd-yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "+0:+2",
        minDate: new Date(year, month, date)
    }).val();
}

function mobPopup() {


    var modal = document.getElementById("mobmyModal1");

    // Get the button that opens the modal
    var btn = document.getElementById("mobtxtname");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close11")[0];

    // When the user clicks the button, open the modal
    modal.style.display = "block";
    var date1 = new Date($("#mobtxtCoverageStartDate").val());
    var date2 = new Date($("#mobtxtCoverageEndDate").val());

    // To calculate the time difference of two dates
    var Difference_In_Time = date2.getTime() - date1.getTime();

    // To calculate the no. of days between two dates
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    var total = Difference_In_Days + 1;
    $("#lblnoofdays").text(total);
    //btn.onclick = function()  {
    //

    //}

    // When the user clicks on <span> (x), close the modal
    //span.onclick = function () {
    //    modal.style.display = "none";
    //}

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}

function mobGetTravelerdetails() {
    var modal = document.getElementById("mobmyModal1");
    var count = 0;
    var nooftraveler = "";
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if (age >= 80) {

        if (age == spouseage) {

        }
        else if (spouseage == "") {

        }
        else if (spouseage < age) {
            alert("PLEASE PURCHASE SEPARATE PLANS FOR EACH INDIVIDUAL FOR AGE 70 YEARS AND ABOVE");
        }


    }
    else if (age >= 70 && age <= 79) {

        if (age == spouseage) {

        }
        else if (spouseage == "") {

        }
        else if (spouseage < age) {
            alert("PLEASE PURCHASE SEPARATE PLANS FOR EACH INDIVIDUAL FOR AGE 70 YEARS AND ABOVE");
        }
    }
    else if ($("#mobtxttravellerageinyear").val() == "") {
        alert("Primary Age Required");
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() == "" && $("#mobtxtchild1age").val() == "" && $("#mobtxtchild2age").val() == "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() != "" && $("#mobtxtchild1age").val() == "" && $("#mobtxtchild2age").val() == "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtspouseage").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() != "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() == "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtspouseage").val() + ',' + $("#mobtxtchild1age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() != "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtspouseage").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() != "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() != "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtspouseage").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val() + ',' + $("#mobtxtChild3age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() != "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() != "" && $("#mobtxtchild4age").val() != "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtspouseage").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val() + ',' + $("#mobtxtChild3age").val() + ',' + $("#mobtxtchild4age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }

    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() == "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() == "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtchild1age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() == "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() == "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() == "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() != "" && $("#mobtxtchild4age").val() == "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val() + ',' + $("#mobtxtChild3age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }
    else if ($("#mobtxttravellerageinyear").val() != "" && $("#mobtxtspouseage").val() == "" && $("#mobtxtchild1age").val() != "" && $("#mobtxtchild2age").val() != "" && $("#mobtxtChild3age").val() != "" && $("#mobtxtchild4age").val() != "") {
        count = 1
        nooftraveler = $("#mobtxttravellerageinyear").val() + ',' + $("#mobtxtchild1age").val() + ',' + $("#mobtxtchild2age").val() + ',' + $("#mobtxtChild3age").val() + ',' + $("#mobtxtchild4age").val();
        $("#mobtxttravelerdetails").val(nooftraveler);
        modal.style.display = "none";
    }

    //var Travelerdetail = document.getElementById("txtTravelerdetail").value;
    //if (Travelerdetail == 1) {
    //    document.getElementById("mobtxttravelerdetails").value = Travelerdetail + " Traveler";
    //}
    //else {
    //    document.getElementById("mobtxttravelerdetails").value = Travelerdetail + " Travelers";
    //}




}

function mobcheckchild3details() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if ($("#mobtxttravellerageinyear").val() == "" || $("#mobtxttravellerageinyear").val() == "") {
        alert("Please Enter Primary Traveler Age");
    }
    //else if ($("#mobtxtspouseage").val() == "" || $("#mobtxtspouseage").val() == null) {
    //    alert("Please Enter Spouse Age");
    //}
    else if ($("#mobtxtchild1age").val() == "" || $("#mobtxtchild1age").val() == null) {
        alert("Please Enter Child1 Age");
    }
    else if ($("#mobtxtchild2age").val() == "" || $("#mobtxtchild2age").val() == null) {
        alert("Please Enter Child2 Age");
    }
    else if ($("#mobtxtChild3age").val() == "" || $("#mobtxtChild3age").val() == null) {
        alert("Please Enter Child3 Age");
    }
    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");

    }
}
function mobcheckchild1details() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if ($("#mobtxttravellerageinyear").val() == "" || $("#mobtxttravellerageinyear").val() == "") {
        alert("Please Enter Primary Traveler Age");
    }
    //else if ($("#mobtxtspouseage").val() == "" || $("#mobtxtspouseage").val() == null) {
    //    alert("Please Enter Spouse Age");
    //}
    else if ($("#mobtxtchild1age").val() == "" || $("#mobtxtchild1age").val() == null) {
        alert("Please Enter Child1 Age");
    }
    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");

    }

}
function mobcheckchild2details() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if ($("#mobtxttravellerageinyear").val() == "" || $("#mobtxttravellerageinyear").val() == "") {
        alert("Please Enter Primary Traveler Age");
    }
    //else if ($("#mobtxtspouseage").val() == "" || $("#mobtxtspouseage").val() == null) {
    //    alert("Please Enter Spouse Age");
    //}
    else if ($("#mobtxtchild1age").val() == "" || $("#mobtxtchild1age").val() == null) {
        alert("Please Enter Child1 Age");
    }
    else if ($("#mobtxtchild2age").val() == "" || $("#mobtxtchild2age").val() == null) {
        alert("Please Enter Child2 Age");
    }
    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");

    }
}
function mobcheckspousedetails() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if ($("#mobtxttravellerageinyear").val() == "" || $("#mobtxttravellerageinyear").val() == "") {
        alert("Please Enter Primary Traveler Age");
    }

    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");

    }
}

function mobchecktravelerdetails() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if ($("#mobtxttravellerageinyear").val() == "" || $("#mobtxttravellerageinyear").val() == "") {
        alert("Please Enter Primary Traveler Age");
    }
    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");

    }
}

function mobCheckagelimit() {
    var age = $("#mobtxttravellerageinyear").val();
    var spouseage = $("#mobtxtspouseage").val();
    if (age >= 80) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
        if (age == spouseage) {

        }
        else if (spouseage == "") {

        }
        else if (spouseage < age) {
            alert("PLEASE PURCHASE SEPARATE PLANS FOR EACH INDIVIDUAL FOR AGE 70 YEARS AND ABOVE");
        }


    }
    else if (age >= 70 && age <= 79) {
        $("#mobtxtchild1age").addClass("disabled2");
        $("#mobtxtchild2age").addClass("disabled2");
        $("#mobtxtChild3age").addClass("disabled2");
        $("#mobtxtchild4age").addClass("disabled2");
        if (age == spouseage) {

        }
        else if (spouseage == "") {

        }
        else if (spouseage < age) {
            alert("PLEASE PURCHASE SEPARATE PLANS FOR EACH INDIVIDUAL FOR AGE 70 YEARS AND ABOVE");
        }
    }
}

function mobCheckCountry() {
    var isocode = $("#mobhdnisocode").val();
    var Touristisocode = $("#mobhdnTouristIsocode").val();

    if (isocode != "" && Touristisocode != "") {
        if (Touristisocode == isocode) {
            alert("HOME COUNTRY CANNOT BE UNITED STATES OF AMERICA");
        }
    }
}

function mobCheckValidation() {
    var isocode = $("#mobhdnisocode").val();
    var Touristisocode = $("#mobhdnTouristIsocode").val();
    var citizenship = $("input[name='mobcitizenship']:checked").val();
    if (isocode == "") {
        alert("Please Enter Valid Home Country");
    }
    else if (Touristisocode == "") {
        alert("Please Enter Valid Tourist Country");
    }
    else if (Touristisocode == isocode) {
        alert("HOME COUNTRY CANNOT BE UNITED STATES OF AMERICA");
    }
    else if ($("#mobtxtCoverageStartDate").val() == "") {
        alert("Leaving Home Required");
    }
    else if ($("#mobtxtCoverageEndDate").val() == "") {
        alert("Returning Home Required");
    }
    else if ($("#mobtxttravelerdetails").val() == "") {
        alert("Tourists Ages  Required");
    }

    else {
        debugger
        //var formdata = new FormData();
        //formdata.append('ISOCODE', $("#mobhdnisocode").val()); formdata.append('TouristISOCODE', $("#mobhdnTouristIsocode").val());
        //formdata.append('mobtxtCoverageStartDate', $("#mobtxtCoverageStartDate").val()); formdata.append('txtCoverageEndDate', $("#txtCoverageEndDate").val());
        //formdata.append('txttravelerdetails', $("#txttravelerdetails").val()); formdata.append('mobtxttravellerageinyear', $("#mobtxttravellerageinyear").val());
        //formdata.append('mobtxtspouseage', $("#txtspouseage").val()); formdata.append('mobtxtchild1age', $("#txtchild1age").val()); formdata.append('mobtxtchild2age', $("#txtchild2age").val());
        //formdata.append('txtchild3age', $("#txtChild3age").val()); formdata.append('txtchild4age', $("#txtchild4age").val());
        CountryName = $("#mobCountryName").val();
        txttouristountry = "United States of America";
        ISOCODE = $("#mobhdnisocode").val();
        TouristISOCODE = $("#mobhdnTouristIsocode").val();
        txtCoverageStartDate = $("#mobtxtCoverageStartDate").val();
        txtCoverageEndDate = $("#mobtxtCoverageEndDate").val();
        txttravelerdetails = $("#mobtxttravelerdetails").val();
        txttravellerageinyear = $("#mobtxttravellerageinyear").val();
        txtspouseage = $("#mobtxtspouseage").val();
        txtchild1age = $("#mobtxtchild1age").val();
        txtchild2age = $("#mobtxtchild2age").val();
        txtchild3age = $("#mobtxtChild3age").val();
        txtchild4age = $("#mobtxtchild4age").val();
        var _array = [];
        _array.push({
            CountryName: CountryName,
            txttouristountry: txttouristountry,
            ISOCODE: ISOCODE,
            TouristISOCODE: TouristISOCODE,
            txtCoverageStartDate: txtCoverageStartDate,
            txtCoverageEndDate: txtCoverageEndDate,
            txttravelerdetails: txttravelerdetails,
            txttravellerageinyear: txttravellerageinyear,
            txtspouseage: txtspouseage,
            txtchild1age: txtchild1age,
            txtchild2age: txtchild2age,
            txtchild3age: txtchild3age,
            txtchild4age: txtchild4age,
            Citizenship: citizenship

        });
        window.location.href = '/BusinessTripQuotation/BusinessTripQuotation?Data=' + JSON.stringify(_array);
        //$.ajax({
        //    url: "/Login/LoginForm/",
        //    type: "POST",
        //    contentType: false,
        //    processData: false,
        //    //dataType: "json",
        //    data: formdata,


        //    success: function (data) {
        //        if (data == 1) {
        //            window.location.href = '/Quotation/Quotation?form=' + formdata ;
        //        }

        //    },
        //    error: function (response) {

        //    }
        //});
    }
}