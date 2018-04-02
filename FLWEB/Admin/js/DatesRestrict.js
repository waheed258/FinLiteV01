

<%--  function showProgress() {
    var updateProgress = $get("<%= UpdateProgress.ClientID %>");
    updateProgress.style.display = "block";
}--%>


  $(document).ready(function () {
      DatePickerSet();
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.add_endRequest(function () {
          DatePickerSet();
      });

  });
 


function DatePickerSet() {
    $('#ContentPlaceHolder1_txtInvDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtInvDate").datepicker({
        format: 'yyyy-mm-dd',
        startDate: '-9d',
        endDate: '0d',
        autoclose: true
    }).attr('readonly', 'false');;
    //  $('#ContentPlaceHolder1_txtAirTravelDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
    <%--    $("#ContentPlaceHolder1_txtAirTravelDate").datepicker({

        format: 'yyyy-mm-dd',

        autoclose: true

    }).attr('readonly', 'false');;


    //  $('#ContentPlaceHolder1_txtAirReturnDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker({

        format: 'yyyy-mm-dd',

        autoclose: true

    }).attr('readonly', 'true');;--%>

       




            
    $("#ContentPlaceHolder1_txtAirTravelDate").datepicker({
        onSelect: function (selected) {
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate() );
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;
            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtDate1").datepicker("option", "maxDate", dtFormatted)
            $("#ContentPlaceHolder1_txtDate2").datepicker("option", "maxDate", dtFormatted)
            $("#ContentPlaceHolder1_txtDate3").datepicker("option", "maxDate", dtFormatted)
            $("#ContentPlaceHolder1_txtDate4").datepicker("option", "maxDate", dtFormatted)
        }
    });

    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker({
        onSelect: function (selected) {
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate() );
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;
            $("#ContentPlaceHolder1_txtAirTravelDate").datepicker("option", "maxDate", dtFormatted)
                  

        }
    });

    //--------------------------------------------------------------------------------------------
    $("#ContentPlaceHolder1_txtDate1").datepicker({
        onSelect: function (selected) {
                   
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate());
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;

            $("#ContentPlaceHolder1_txtDate2").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
                   
            $("#ContentPlaceHolder1_txtDate2").val('');
            $("#ContentPlaceHolder1_txtDate3").val('');
            $("#ContentPlaceHolder1_txtDate4").val('');
            $("#ContentPlaceHolder1_txtAirReturnDate").val('');
            $("#ContentPlaceHolder1_txtAirTravelDate").val($("#ContentPlaceHolder1_txtDate1").val());


        }


    });
         



    $("#ContentPlaceHolder1_txtDate2").datepicker({
        onSelect: function (selected) {
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate());
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;
            $("#ContentPlaceHolder1_txtDate3").datepicker("option", "minDate", dtFormatted)
            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtDate3").val('');
            $("#ContentPlaceHolder1_txtDate4").val('');
            $("#ContentPlaceHolder1_txtAirReturnDate").val('');
        }
    });
    $("#ContentPlaceHolder1_txtDate3").datepicker({
        onSelect: function (selected) {
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate() );
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;
            $("#ContentPlaceHolder1_txtDate4").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtDate4").val('');
            $("#ContentPlaceHolder1_txtAirReturnDate").val('');
        }
    });

    $("#ContentPlaceHolder1_txtDate4").datepicker({
        onSelect: function (selected) {
            var dtMax = new Date(selected);
            dtMax.setDate(dtMax.getDate() );
            var dd = dtMax.getDate();
            var mm = dtMax.getMonth() + 1;
            var y = dtMax.getFullYear();
            var dtFormatted = mm + '/' + dd + '/' + y;
            $("#ContentPlaceHolder1_txtDate1").datepicker("option", "minDate", dtFormatted)
            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
            $("#ContentPlaceHolder1_txtAirReturnDate").val('');
        }

    });

    //   $('#ContentPlaceHolder1_txtDate1').val('<%=System.DateTime.Now.ToShortDateString()%>');
    <%--   $("#ContentPlaceHolder1_txtDate1").datepicker({

        numberOfMonths: 1,
        dateFormat: 'yyyy-mm-dd',
        autoclose: true,

    }).attr('readonly', 'true');;
    //    $('#ContentPlaceHolder1_txtDate2').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtDate2").datepicker({

        numberOfMonths: 1,
        dateFormat: 'yyyy-mm-dd',
        autoclose: true,

    }).attr('readonly', 'true');;--%>
    //     $('#ContentPlaceHolder1_txtDate3').val('<%=System.DateTime.Now.ToShortDateString()%>');
    //$("#ContentPlaceHolder1_txtDate3").datepicker({

    //    numberOfMonths: 1,
    //    dateFormat: 'yyyy-mm-dd',
    //    autoclose: true,

    //}).attr('readonly', 'true');;
    // $('#ContentPlaceHolder1_txtDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
    //$("#ContentPlaceHolder1_txtDate4").datepicker({

    //    numberOfMonths: 1,
    //    dateFormat: 'yyyy-mm-dd',
    //    autoclose: true,

    //}).attr('readonly', 'true');;
    $('#ContentPlaceHolder1_txtlandTravelFrom').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtlandTravelFrom").datepicker({

        numberOfMonths: 1,
        dateFormat: 'yyyy-mm-dd',
        autoclose: true,

    }).attr('readonly', 'true');;

    // $('#ContentPlaceHolder1_txtlandTravelto').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtlandTravelto").datepicker({

        numberOfMonths: 1,
        dateFormat: 'yyyy-mm-dd',
        autoclose: true,

    }).attr('readonly', 'true');;
    // $('#ContentPlaceHolder1_txtSerTravelDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
    $("#ContentPlaceHolder1_txtSerTravelDate").datepicker({

        numberOfMonths: 1,
        dateFormat: 'yyyy-mm-dd',
        autoclose: true,

    }).attr('readonly', 'true');;
    //Invoice
    $('#<%= drpInvClientType.ClientID %>').select2();
    $('#<%= drpInvClientName.ClientID %>').select2();
    $('#<%= ddlInvCosultant.ClientID %>').select2();
    $('#<%= drpInvBookingSrc.ClientID %>').select2();
    $('#<%= drpInvBookDest.ClientID %>').select2();
    $('#<%= ddlInvPdfPrintStyle.ClientID %>').select2();
    //Air Ticket
    <%-- $('#<%= drpTicketType.ClientID %>').select2();
    $('#<%= ddlAirLine.ClientID %>').select2();
    $('#<%= ddlAirService.ClientID %>').select2();
    $('#<%= ddlType.ClientID %>').select2();
    $('#<%= ddlAirPayment.ClientID %>').select2();
    //Land Suppliers
    $('#<%= DDlandSupplier.ClientID %>').select2();
    $('#<%= DDlandService.ClientID %>').select2();
    $('#<%= DDlandType.ClientID %>').select2();
    $('#<%= DDlandPayment.ClientID %>').select2();
    $('#<%= DDlandCreditCard.ClientID %>').select2();
    //Service Fee
    $('#<%= ddlServiceType.ClientID %>').select2();
    $('#<%= ddlSoureceref.ClientID %>').select2();
    $('#<%= ddlPassengerName.ClientID %>').select2();
    $('#<%= ddlPaymentMethod.ClientID %>').select2();
    $('#<%= ddlCreditCardType.ClientID %>').select2();
    $('#<%= ddlCollectVia.ClientID %>').select2();
    //GenneralCharge
    $('#<%= ddlGenchrgType.ClientID %>').select2();
    $('#<%= ddlPassengerNames.ClientID %>').select2();
    $('#<%= ddlCrdCardType.ClientID %>').select2();--%>
    }
