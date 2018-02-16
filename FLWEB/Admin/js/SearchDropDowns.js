
//AirSuppliers
$(document).ready(function () {
    $('#<%= dropStatus.ClientID %>').select2();
});
$(document).ready(function () {
    $('#<%= dropServiceType.ClientID %>').select2();
});
$(document).ready(function () {
    $('#<%= dropCountry.ClientID %>').select2();
});
$(document).ready(function () {
    $('#<%= dropGroup.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropState.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropDivision.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropCity.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropConsultant.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropBank.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropAccountType.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropCommiMethod.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropPaymentMethod.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropTreatInvType.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropAllocItemType.ClientID %>').select2();
});

$(document).ready(function () {
    $('#<%= dropNoteType.ClientID %>').select2();
});


$(document).ready(function () {
    //Invoice 
    $('#<%= drpInvClientType.ClientID %>').select2();
    $('#<%= drpInvClientName.ClientID %>').select2();
    $('#<%= ddlInvCosultant.ClientID %>').select2();
    $('#<%= drpInvBookingSrc.ClientID %>').select2();
    $('#<%= drpInvBookDest.ClientID %>').select2();

    //Air Ticket
    $('#<%= drpTicketType.ClientID %>').select2();
    $('#<%= ddlAirLine.ClientID %>').select2();
    $('#<%= drpAirPassenger.ClientID %>').select2();
    $('#<%= ddlAirService.ClientID %>').select2();
    $('#<%= ddlType.ClientID %>').select2();
    $('#<%= ddlAirPayment.ClientID %>').select2();

    ///Land Ticket
    $('#<%= DDlandSupplier.ClientID %>').select2();
    $('#<%= DDlandService.ClientID %>').select2();
    $('#<%= DDlandType.ClientID %>').select2();
    $('#<%= DDlandPayment.ClientID %>').select2();
    $('#<%= DDlandCreditCard.ClientID %>').select2();

    // Service Fee
    $('#<%= ddlServiceType.ClientID %>').select2();
    $('#<%= ddlSoureceref.ClientID %>').select2();
    $('#<%= ddlPassengerName.ClientID %>').select2();
    $('#<%= ddlPaymentMethod.ClientID %>').select2();
    $('#<%= ddlCreditCardType.ClientID %>').select2();
    $('#<%= ddlCollectVia.ClientID %>').select2();

    //General Charge
    $('#<%= ddlGenchrgType.ClientID %>').select2();
    $('#<%= ddlPassengerNames.ClientID %>').select2();
    $('#<%= ddlCrdCardType.ClientID %>').select2();
});




//Main Accounts
$(document).ready(function () {
    DrpSearch();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        DrpSearch();
    });

});

function DrpSearch() {
    $('#<%= ddlDepartment.ClientID %>').select2();
    $('#<%= ddlCurrency.ClientID %>').select2();
    $('#<%= ddlMainAcType.ClientID %>').select2()
    $('#<%= DropDownCategory.ClientID %>').select2()
};
