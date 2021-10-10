$(document).ready(function () {
    debugger
    var height = $(window).height();
    var width = $(window).width();
    if (height == 640 && width == 360) {
        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '80%';
            all[i].style.margin = '29% 12% 0 12%';
        }
        //$(".").width("80%");
    }
    else if (width == 768) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '35%';
            all[i].style.margin = '20% 12% 0 33%';
        }

    }
    else if (width == 375) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '80%';
            all[i].style.margin = '28% 12% 0 11%';
        }

    }
    else if (width == 1024) {

        var all = document.getElementsByClassName('modal-content11');
        for (var i = 0; i < all.length; i++) {
            all[i].style.width = '26%';
            all[i].style.margin = '15% 12% 0 37%';
        }

    }
})
function mobPopup() {


    var modal = document.getElementById("mobmyModal1");

    // Get the button that opens the modal
    var btn = document.getElementById("txtname");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close1")[0];

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
            alert("HOME COUNTRY  CANNOT BE UNITED STATES OF AMERICA");
        }
    }
}


function mobGetAllList() {
    debugger
    usersList = [];
    var isocode = $("#mobhdnisocode").val();
    var Touristisocode = $("#mobhdnTouristIsocode").val();
    var citizenship = $("input[name='mobcitizenship']:checked").val();
    if (isocode == "") {
        alert("Home Country Required");
    }
    else if (Touristisocode == "") {
        alert("Tourist Country Required");
    }
    else if (isocode == Touristisocode) {

        alert("HOME COUNTRY COUNTRY CANNOT BE UNITED STATES OF AMERICA");

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
        $("#IDEditYourPlanMob").hide();
        $("#loader").show();
        $("#filterdiv").hide();
        $("#tagdiv").hide();
        var hdnisocode = $("#mobhdnisocode").val();
        var hdnTouristIsocode = $("#mobhdnTouristIsocode").val();
        var txtCoverageStartDate = $("#mobtxtCoverageStartDate").val();
        var txtCoverageEndDate = $("#mobtxtCoverageEndDate").val();
        var txttravellerageinyear = $("#mobtxttravellerageinyear").val();
        var txtspouseage = $("#mobtxtspouseage").val();
        var txtchild1age = $("#mobtxtchild1age").val();
        var txtchild2age = $("#mobtxtchild2age").val();
        var txtchild3age = $("#mobtxtchild1age").val();
        var txtchild4age = $("#mobtxtchild2age").val();
        var CountryName = $("#mobCountryName").val();
        var txttouristountry = $("#mobtxttouristountry").val();
        var formdata = new FormData();
        formdata.append('hdnisocode', hdnisocode); formdata.append('hdnTouristIsocode', hdnTouristIsocode); formdata.append('txtCoverageStartDate', txtCoverageStartDate); formdata.append('txtCoverageEndDate', txtCoverageEndDate);
        formdata.append('txttravellerageinyear', txttravellerageinyear); formdata.append('txtspouseage', txtspouseage); formdata.append('txtchild1age', txtchild1age);
        formdata.append('txtchild2age', txtchild2age); formdata.append('txtchild4age', txtchild4age); formdata.append('txtchild3age', txtchild3age); formdata.append('CountryName', CountryName);
        formdata.append('Citizenship', citizenship);
        $.ajax({
            url: "/BusinessTripQuotation/GetAllQuationList/",
            type: "POST",
            contentType: false,
            processData: false,
            //dataType: "json",
            data: formdata,


            success: function (Res) {
                debugger




                var age = $("#mobhdntravelersage").val();
                $("#loader").hide();
                $("#filterdiv").show();
                $("#tagdiv").show();
                $("#tabContainerId").html("");
                $("#CompareDiv").html("");
                $("#mobbtngetquotes").hide()
                $("#mobbtneditt").show()
                $("#mobtxtCoverageStartDate").removeClass("enabled").addClass("disabled2");

                $("#mobtxtCoverageEndDate").removeClass("enabled").addClass("disabled2");
                $("#mobCountryName").removeClass("enabled").addClass("disabled2");

                $("#mobtxttouristountry").removeClass("enabled").addClass("disabled2");
                $("#mobtxttravelerdetails").removeClass("enabled").addClass("disabled2");
                var hdnisocode = $("#hdnisocode").val();
                var hdnTouristIsocode = $("#hdnTouristIsocode").val();
                $("#mobhdnISOCODE").val(hdnisocode);
                $("#mobhdnTouristcode").val(hdnTouristIsocode);
                $("#mobhdnLeavingHome").val(txtCoverageStartDate);
                $("#mobhdnTillDate").val(txtCoverageEndDate);
                $("#mobhdntravelersage").val(txttravellerageinyear);




                Result2 = Res.Result2;
                DistinctQuot = Res.DistinctQuot;
                DistinctQuotNo = Res.DistinctQuotNo;
                DistinctQuotYes = Res.DistinctQuotYes;
                AtlasGroup = Res.AtlasGroup;
                INFGroupNo = Res.INFGroupNo;
                INFGroupYes = Res.INFGroupYes;

                ComprehensivePlan = Res.ComprehensivePlan;
                LimitedPlan = Res.LimitedPlan;
                DistinctQuot2 = Res.DistinctQuot2;
                DistinctQuotNo2 = Res.DistinctQuotNo2;
                DistinctQuotYes2 = Res.DistinctQuotYes2;
                Result5 = Res.Result5;
                taborderbyasc = Res.taborderbyasc;
                taborderbydes = Res.taborderbydes;
                PreExisting = Res.PreExisting;
                PreExistingNo = Res.PreExistingNo;
                Comprehensivebyasc = Res.Comprehensivebyasc;
                Comprehensivebydes = Res.Comprehensivebydes;
                ComprehensivebyascPreexistingYES = Res.ComprehensivebyascPreexistingYES;
                ComprehensivebydesPreexistingYES = Res.ComprehensivebydesPreexistingYES;
                ComprehensivebyascPreexistingNO = Res.ComprehensivebyascPreexistingNO;
                ComprehensivebydesPreexistingNO = Res.ComprehensivebydesPreexistingNO;
                LimitedPreExistingYEs = Res.LimitedPreExistingYEs;
                LimitedPreExistingNo = Res.LimitedPreExistingNo;
                LimitedByDes = Res.LimitedByDes;
                LimitedByAsc = Res.LimitedByAsc;
                ComprehensivePreExistingYes = Res.ComprehensivePreExistingYes;
                ComprehensivePreExistingNo = Res.ComprehensivePreExistingNo;
                PreexistingByLimitedByAsc = Res.PreexistingByLimitedByAsc;
                PreexistingByLimitedbydes = Res.PreexistingByLimitedbydes;
                PreExistingNoByDes = Res.PreExistingNoByDes;
                PreExistingNoByAcs = Res.PreExistingNoByAcs;
                PreExistingYesByDes = Res.PreExistingYesByDes;
                PreExistingYesByAcs = Res.PreExistingYesByAcs;
                PreexistingNoByLimitedByAsc = Res.PreexistingNoByLimitedByAsc;
                PreexistingNoByLimitedbydes = Res.PreexistingNoByLimitedbydes;
                var INsuranceCompany = Res.INsuranceComp;
                var PLANTYPE = Res.PLANTYPE;
                BindTab(Result2, DistinctQuot);
                Bindbyorder();
                $(INsuranceCompany).each(function (index, value) {

                    $("#ddlINSURER").append($("<option></option>").val(value.INsuranceComp).text(value.INsuranceComp));
                });
                var CountryList = '<option value=All>ALL</option>';
                $.each(PLANTYPE, function (i, item) {
                    debugger

                    //$("#ddlPLANTYPE").append($("<option></option>").val(value.Plan_Type)).text(value.Plan_Type);
                    CountryList = CountryList + '<option value=' + item.Plan_Type + '>' + item.Plan_Type + '</option>';





                });
                $('#ddlPLANTYPE').html(CountryList);
            },
            error: function (Res) {
                debugger
                $("#loader").hide();
                $("#filterdiv").hide();
                $("#tagdiv").hide();
            }
        });
    }
}

function mobShowGetQuotesbtn() {
    debugger
    $("#mobbtngetquotes").show()
    $("#mobbtneditt").hide()
    $("#mobtxtCoverageStartDate").removeClass("disabled2").addClass("enabled");

    $("#mobtxtCoverageEndDate").removeClass("disabled2").addClass("enabled");
    $("#mobCountryName").removeClass("disabled2").addClass("enabled");

    $("#mobtxttouristountry").removeClass("disabled2").addClass("enabled");
    $("#mobtxttravelerdetails").removeClass("disabled2").addClass("enabled");
}


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