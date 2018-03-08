<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ProformaInvoice.aspx.cs" Inherits="Admin_ProformaInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            /*filter: alpha(opacity=90);
            opacity: 0.8;*/
            border: 1px solid;
            background: #eee;
            padding: 2px;
            color: black;
        }

        .decimalRight {
            text-align: right;
        }

        .style1 {
            color: #FF0000;
        }

        .uppercase {
            text-transform: uppercase;
        }

        .btncancle {
            float: right !important;
        }

        #txtPFInvClntMesg {
            overflow-y: scroll;
            max-height: 40px;
        }

        #ContentPlaceHolder1_txtPFInvClntMesg {
            max-height: 40px !important;
        }

        .overlay {
            position: fixed;
            z-index: 999;
            top: 300px;
            left: 850px;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
          .overlaypop {
            position: fixed;
            z-index: 999;
            top: 300px;
            left: 750px;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
  <script type="text/javascript">


      $(document).ready(function () {
          DatePickerSet();
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              DatePickerSet();
          });

      });



      function DatePickerSet() {
          $('#ContentPlaceHolder1_txtPFInvDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtPFInvDate").datepicker({
                format: 'yyyy-mm-dd',
                startDate: '-9d',
                endDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;



            $("#ContentPlaceHolder1_txtPFAirTravelDate").datepicker({

                onSelect: function (selected) {

                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFDate1").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFDate2").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFDate3").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFDate4").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFAirTravelDate").val(dtFormatted);
                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFAirTravelDate").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").val(dtFormatted);

                }
            }).attr('readonly', 'true');;

            //--------------------------------------------------------------------------------------------
            $("#ContentPlaceHolder1_txtPFDate1").datepicker({
                onSelect: function (selected) {
                    debugger;
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var y = dtMax.getFullYear();
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var dtFormatted = mm + '/' + dd + '/' + y;

                    $("#ContentPlaceHolder1_txtPFDate2").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker("option", "minDate", dtFormatted);

                    $("#ContentPlaceHolder1_txtPFDate2").val('');
                    $("#ContentPlaceHolder1_txtPFDate3").val('');
                    $("#ContentPlaceHolder1_txtPFDate4").val('');
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtPFAirTravelDate").val($("#ContentPlaceHolder1_txtPFDate1").val());



                }


            }).attr('readonly', 'true');;




            $("#ContentPlaceHolder1_txtPFDate2").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFDate3").datepicker("option", "minDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFDate3").val('');
                    $("#ContentPlaceHolder1_txtPFDate4").val('');
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtPFDate2").val(dtFormatted);
                }
            }).attr('readonly', 'true');;
            $("#ContentPlaceHolder1_txtPFDate3").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFDate4").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFDate4").val('');
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtPFDate3").val(dtFormatted);
                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtPFDate4").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    //  $("#ContentPlaceHolder1_txtDate1").datepicker("option", "minDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtPFDate4").val(dtFormatted);
                }

            }).attr('readonly', 'true');;


           // $('#ContentPlaceHolder1_txtPFlandTravelFrom').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtPFlandTravelFrom").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFlandTravelto").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtPFlandTravelFrom").val(dtFormatted);

                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtPFlandTravelto").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtPFlandTravelFrom").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtPFlandTravelto").val(dtFormatted);


                }
            }).attr('readonly', 'true');;




            $("#ContentPlaceHolder1_txtPFSerTravelDate").datepicker({

                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                autoclose: true,

            }).attr('readonly', 'true');;
            //Invoice
            $('#<%= drpPFInvClientType.ClientID %>').select2();
            $('#<%= drpPFInvClientName.ClientID %>').select2();
            $('#<%= ddlPFInvCosultant.ClientID %>').select2();
            $('#<%= drpPFInvBookingSrc.ClientID %>').select2();
            $('#<%= drpPFInvBookDest.ClientID %>').select2();
            $('#<%= ddlPFInvPdfPrintStyle.ClientID %>').select2();

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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>


    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>

            <section class="panel">
                <header class="panel-heading">
                    <%-- <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>--%>
                    <h2 class="panel-title">Proforma Invoice</h2>
                </header>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Date<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtPFInvDate" runat="server" CssClass="form-control test" MaxLength="50" placeholder="YYYY/MM/DD" />

                                <asp:RequiredFieldValidator ControlToValidate="txtPFInvDate" runat="server" ID="rfvtxtPFInvDate" ValidationGroup="PFinvoice"
                                    ErrorMessage="Select Date" Text="Select Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Client Type<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpPFInvClientType" runat="server" CssClass="form-control" OnTextChanged="drpPFInvClientType_TextChanged" AutoPostBack="true">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="drpPFInvClientType" runat="server" ID="RequiredFieldValidator1" ValidationGroup="PFinvoice"
                                    ErrorMessage="Select Client Type" Text="Select Client Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Client <span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpPFInvClientName" runat="server" CssClass="form-control" OnTextChanged="drpPFInvClientName_TextChanged" AutoPostBack="true">
                                    <asp:ListItem Text="--Select Client Name--" Value="0" Selected="True"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="drpPFInvClientName" runat="server" ID="rfvdrpPFInvClientName" ValidationGroup="PFinvoice"
                                    ErrorMessage="Select Client Name" Text="Select Client Name" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Consultant<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlPFInvCosultant" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlPFInvCosultant" runat="server" ID="rfvdrpPFInvCosultant" ValidationGroup="PFinvoice"
                                    ErrorMessage="Select Consultant" Text="Select Consultant" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Order<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtPFInvOrder" runat="server" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ControlToValidate="txtPFInvOrder" runat="server" ID="rfvtxtPFInvOrder" ValidationGroup="PFinvoice"
                                    ErrorMessage="Enter Order Number" Text="Enter Order Number" class="validationred" Display="Dynamic" ForeColor="Red" />
                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Booking No
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtPFInvBookNo" runat="server" CssClass="form-control" MaxLength="50" />

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Booking Source
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpPFInvBookingSrc" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="--Select Booking Source--" Value="-1" Selected="True"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Booking Destination
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpPFInvBookDest" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="--Select Booking Destination--" Value="-1" Selected="True"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <%-- model PopUp code Starts --%>
                    <asp:UpdatePanel ID="updatepanelContacts" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-5">

                                        <button runat="server" id="btnPFOpenFP" class="btn btn-mini">
                                            <i class="fa fa-plane"></i>
                                        </button>
                                        <asp:Button ID="bntCancelFP" runat="server" Text="Cancel" Style="display: none;" />
                                        <%--<asp:Button ID="btnOpenFP" runat="server" Text="Open"  />--%>
                        &nbsp;&nbsp;&nbsp;
                         <button runat="server" id="btnPFLand" class="btn btn-mini">
                             <i class="fa  fa-university"></i>
                         </button>
                                        &nbsp;&nbsp;
                         <button runat="server" id="btnPFserFee" onserverclick="btnPFserFee_ServerClick" class="btn btn-mini" title="Service Fee">
                             <i class="fa fa-keyboard-o"></i>
                         </button>
                                        &nbsp;&nbsp;
                         <button runat="server" id="btnPFGencharge" onserverclick="btnPFGencharge_ServerClick" class="btn btn-mini" title="General Charge">
                             <i class="fa fa-cloud data-unicode"></i>
                         </button>
                                        &nbsp;&nbsp;
                                    </div>
                                </div>
                            </div>

                            <%-- AirTicket --%>

                            <cc1:ModalPopupExtender ID="AirPFPopupExtender" runat="server" TargetControlID="btnPFOpenFP"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnPFOpenFP" CancelControlID="bntCancelFP"
                                PopupControlID="pnlPFFlight">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlPFFlight" runat="server" CssClass="panelpopup" Width="1000px" Height="500px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="cmdClose" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>

                                <div id="popupdiv" title="Basic modal dialog" class="modalBackground">
                                    <header class="panel-heading">
                                        <div style="padding-top: 3px; padding-right: 3px;">
                                            <asp:ImageButton ID="ImageButton5" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                            <h4 class="panel-title">AirTicket</h4>
                                        </div>
                                    </header>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Ticket No<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirTicketNo" runat="server" CssClass="form-control" MaxLength="10" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFAirTicketNo" runat="server" ID="rfvtxtPFAirTicketNo" ValidationGroup="PFairticket"
                                                    ErrorMessage="Enter Ticket Number" Text="Enter Ticket Number" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Ticket Type
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="drpPFTicketType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select Ticket Type--" Value="-1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="E-Ticket" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="drpPFTicketType" runat="server" ID="rfvdrpPFTicketType" ValidationGroup="PFairticket"
                                                    ErrorMessage="Select Ticket Type" Text="Select Ticket Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    PNR<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFPnr" runat="server" CssClass="form-control uppercase" MaxLength="6" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFPnr" runat="server" ID="rfvtxtPFPnr" ValidationGroup="PFairticket"
                                                    ErrorMessage="Enter Pnr" Text="Enter PNR" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Air Line<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlPFAirLine" runat="server" CssClass="form-control" OnTextChanged="ddlPFAirLine_TextChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select Airline--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFAirLine" runat="server" ID="rfvddlPFAirLine" ValidationGroup="PFairticket"
                                                    ErrorMessage="Select Airline" Text="Select Airline" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="drpPFAirPassenger" runat="server" CssClass="form-control">
                                    
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="drpPFAirPassenger" runat="server" ID="rfvdrpPFAirPassenger" ValidationGroup="PFairticket"
                                                    ErrorMessage="Enter Passenger Name" Text="Enter Passenger Name" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Conjunction
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirConjunction" runat="server" CssClass="form-control" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Service<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlPFAirService" runat="server" CssClass="form-control">
                                                    <%--<asp:ListItem Text="--Select Service--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFAirService" runat="server" ID="rfvdrpPFAirService" ValidationGroup="PFairticket"
                                                    ErrorMessage="Select Service" Text="Select Service" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlPFType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPFType_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFType" runat="server" ID="rfvddlPFType" ValidationGroup="PFairticket"
                                                    ErrorMessage="Select Type" Text="Select Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Routing<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirRouting" runat="server" CssClass="form-control uppercase" OnTextChanged="txtPFAirRouting_TextChanged" AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFAirRouting" runat="server" ID="rfvtxtPFAirRouting" ValidationGroup="PFairticket"
                                                    ErrorMessage="Enter Routing" Text="Enter Routing" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>
                                        </div>
                                    </div>
                                     <asp:HiddenField ID="hf_PF_Rout_Id1" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_PF_Rout_Id2" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_PF_Rout_Id3" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_PF_Rout_Id4" runat="server" Value="0" />

                                    <div class="form-group"   id="PFdivRouteHead" runat="server">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Routes
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Flight No
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Class
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Date
                                                </label>

                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Miles
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirMiles" runat="server" CssClass="form-control" />

                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group" id="divPFRouting" runat="server">
                                        <div class="col-sm-12" id="divPFRoutingtxt" runat="server">
                                            <div class="col-sm-2" id="divPFlblRoute" runat="server">
                                                <%--<label class="control-label" id="lblRoutes1" runat="server"></label>--%>
                                                <asp:TextBox ID="lblPFRoutes1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divPFtxtFlightNo">
                                                <asp:TextBox ID="txtPFFlightNo1" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFFlightNo1" runat="server" ID="rfvtxtPFFlightNo1"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter TASF MPD" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />

                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtClass">

                                                <asp:TextBox ID="txtPFClass1" runat="server" CssClass="form-control uppercase" MaxLength="2" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFClass1" runat="server" ID="rfvtxtPFClass1"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-2" runat="server" id="divPFtxtDate">
                                                <asp:TextBox ID="txtPFDate1" runat="server" CssClass="form-control" OnTextChanged="txtPFDate_TextChanged" AutoPostBack="true" placeholder="YYYY-MM-DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFDate1" runat="server" ID="rfvtxtPFDate1"
                                                    ValidationGroup="PFairticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

                                            </div>


                                        </div>

                                        <div class="col-sm-12" id="div1" runat="server">
                                            <div class="col-sm-2" id="div3" runat="server">
                                                <%--<label class="control-label" id="lblRoutes2" runat="server"></label>--%>
                                                <asp:TextBox ID="lblPFRoutes2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFFlightNo1">
                                                <asp:TextBox ID="txtPFFlightNo2" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFFlightNo2" runat="server" ID="rfvtxtPFFlightNo2"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />

                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtPFClass1">

                                                <asp:TextBox ID="txtPFClass2" runat="server" CssClass="form-control uppercase" MaxLength="2" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFClass2" runat="server" ID="rfvtxtPFClass2"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFDate1">
                                                <asp:TextBox ID="txtPFDate2" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFDate2" runat="server" ID="rfvtxtPFDate2"
                                                    ValidationGroup="PFairticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

                                            </div>

                                        </div>

                                        <div class="col-sm-12" id="div2" runat="server">
                                            <div class="col-sm-2" id="div4" runat="server">
                                                <%--<label class="control-label" id="lblRoutes3" runat="server"></label>--%>
                                                <asp:TextBox ID="lblPFRoutes3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFFlightNo2">
                                                <asp:TextBox ID="txtPFFlightNo3" runat="server" CssClass="form-control" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFFlightNo3" runat="server" ID="rfvtxtPFFlightNo3"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />


                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtPFClass2">

                                                <asp:TextBox ID="txtPFClass3" runat="server" CssClass="form-control uppercase" MaxLength="2" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFClass3" runat="server" ID="rfvtxtPFClass3"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFDate2">
                                                <asp:TextBox ID="txtPFDate3" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFDate3" runat="server" ID="rfvtxtPFDate3"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Class" Text="Select Date" ForeColor="red" Display="Dynamic" />


                                            </div>
                                        </div>

                                        <div class="col-sm-12" id="div5" runat="server">
                                            <div class="col-sm-2" id="div6" runat="server">
                                                <%--<label class="control-label" id="lblRoutes4" runat="server"></label>--%>
                                                <asp:TextBox ID="lblPFRoutes4" runat="server" CssClass="form-control"> </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFFlightNo3">
                                                <asp:TextBox ID="txtPFFlightNo4" runat="server" CssClass="form-control" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFFlightNo4" runat="server" ID="rfvtxtPFFlightNo4"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />


                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtPFClass3">

                                                <asp:TextBox ID="txtPFClass4" runat="server" CssClass="form-control uppercase" MaxLength="2" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFClass4" runat="server" ID="rfvtxtPFClass4"
                                                    ValidationGroup="PFairticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtPFDate3">
                                                <asp:TextBox ID="txtPFDate4" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFDate4" runat="server" ID="rfvtxtPFDate4"
                                                    ValidationGroup="PFairticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

                                            </div>

                                        </div>

                                    </div>




                                    

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Travel Date<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirTravelDate" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFAirTravelDate" runat="server" ID="rfvtxtPFAirTravelDate" ValidationGroup="PFairticket"
                                                    ErrorMessage="Select Travel Date" Text="Select Travel Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Return Date
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFAirReturnDate" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Exclusive Fare<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirExcluisvefare" runat="server" CssClass="form-control decimalRight" AutoPostBack="true" OnTextChanged="txtPFExcluisvefare_TextChanged" placeholder="0.00" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFAirExcluisvefare" runat="server" ID="rfvtxtPFAirExcluisvefare" ValidationGroup="PFairticket"
                                                    ErrorMessage="Enter Exclusive Amount" Text="Enter Exclusive Amount" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision%
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirCommisionper" runat="server" CssClass="form-control decimalRight" placeholder="0.00" AutoPostBack="true" OnTextChanged="txtPFAirCommisionper_TextChanged" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT on Fare
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFVatPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:TextBox ID="txtPFAirVatOnFare" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision(Exclusive)
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirCommExclu" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Airport Taxes
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirportTax" runat="server" CssClass="form-control decimalRight" placeholder="0.00" OnTextChanged="txtPFAirportTax_TextChanged" AutoPostBack="true" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT%
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirCommVat" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />

                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT incl Taxes
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirVatinclTax" runat="server" CssClass="form-control decimalRight" placeholder="0.00" ReadOnly="true" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Agent VAT
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirAgentVat" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Client Total
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirClientTot" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision(inclusive)
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirCommInclu" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Payment
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlPFAirPayment" runat="server" CssClass="form-control decimalRight">
                                                    <asp:ListItem Text="--Select Payment--" Value="-1" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Due to BSP
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPFAirDueToBsp" runat="server" CssClass="form-control decimalRight" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnPFAirSubmit" OnClick="btnPFAirSubmit_Click" class="btn btn-primary" ValidationGroup="PFairticket"
                                                Text="Submit" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" ValidationGroup="" OnClick="btnCancel_Click" Text="Cancel" />

                                        </div>
                                    </div>
                                    <div class="overlaypop">
                                        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                            <ProgressTemplate>
                                                <img src="../images/loading.gif" alt="" height="40" width="40" />
                                                <br />
                                                <h4>Please wait....</h4>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </asp:Panel>


                            <%-- Land Arrangemnts --%>

                            <cc1:ModalPopupExtender ID="landPFPopExtender" runat="server" TargetControlID="btnPFLand"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnPFLand" CancelControlID="bntCancelFP"
                                PopupControlID="PFlandPanel">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="PFlandPanel" runat="server" CssClass="panelpopup modalBackground " Width="80%" Height="93%" Style="display: none;">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton3" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>

                                <div class="form-group">
                                    <header class="panel-heading">
                                        <div style="padding-top: 3px; padding-right: 3px;">
                                            <asp:ImageButton ID="ImageButton3" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                            <h4 class="panel-title">Land Arrangements</h4>
                                        </div>
                                    </header>
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Land Supplier<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="DDPFlandSupplier" runat="server" CssClass="form-control" OnTextChanged="DDPFlandSupplier_TextChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDPFlandSupplier" runat="server" ID="rfvDDPFlandSupplier" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Select LandSupplier" Text="Select LandSupplier" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                            <%-- <asp:TextBox ID="txtDivision" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Services<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="DDPFlandService" runat="server" CssClass="form-control" AppendDataBoundItems="true"  OnSelectedIndexChanged="DDPFlandService_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDPFlandService" runat="server" ID="rfvDDPFlandService" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Select Service" Text="Select Service" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Type<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <%--<div class="col-sm-3">--%>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="DDPFlandType" runat="server" OnSelectedIndexChanged="DDPFType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDPFlandType" runat="server" ID="rfvDDPFlandType" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Select Type" Text="Select Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Passenger Name<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-2">

                                            <asp:TextBox ID="txtPFlandPassName" runat="server" CssClass="form-control" placeholder="Passenge Name" MaxLength="50" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtPFlandPassName" runat="server" ID="rfvtxtPFlandPassName" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Select Passenger" Text="Enter Passenger" class="validationred" Display="Dynamic" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Travel Dates<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandTravelFrom" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtPFlandTravelFrom" runat="server" ID="rfvtxtPFlandTravelFrom" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Select Travel Date" Text="Select Travel Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                to
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandTravelto" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" />

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Booking Ref No<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandBookingRef" runat="server" CssClass="form-control" placeholder="Book ref NO" MaxLength="50" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtPFlandBookingRef" runat="server" ID="rfvtxtPFlandBookingRef" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Enter Booking Ref No" Text="Enter Booking Ref No" class="validationred" Display="Dynamic" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Voucher
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandVocher" runat="server" CssClass="form-control" placeholder="Vocher No" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Supplier Ref No
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandSupplierRef" runat="server" CssClass="form-control" placeholder="Supplier Ref No" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Supplier Inv.No
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandSuppInvNo" runat="server" CssClass="form-control" placeholder="Supplier Invoice No" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Units
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandUnits" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandUnits_TextChanged" AutoPostBack="true" />

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commissionable(Excl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandcmblExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandcmblExcl_TextChanged" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Total(Exclusive)<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandTotalExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandTotalExcl_TextChanged" AutoPostBack="true" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtPFlandTotalExcl" runat="server" ID="rfvtxtPFlandTotalExcl" ValidationGroup="PFlandsupplier"
                                                ErrorMessage="Enter TotalExclu" Text="Enter TotalExclu" class="validationred" Display="Dynamic" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Rate(Inclusive)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandRateIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandRateIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission%
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandCommPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandCommPer_TextChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                VAT
                                            </label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtPFLandExlVatPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" AutoPostBack="true">
                             <%--   <asp:ListItem Text="Select item" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Test1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Test2" Value="2"></asp:ListItem>--%>
                                            </asp:TextBox>


                                            <%--<asp:TextBox ID="txtVat" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                            <%-- <asp:RegularExpressionValidator ID="revVat" runat="server" ControlToValidate="txtVat" ValidationExpression="^\d+" ErrorMessage="Enter Only Numbers" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtPFlandExclVatAmount" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandCmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission(Excl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandCommExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Total(Inclusive)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandTotalIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandTotalIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Other Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandOtherCmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtPFlandOtherCmblIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                VAT%
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFLandVatPer" runat="server" CssClass="form-control decimalRight" AutoPostBack="true">
                                
                                            </asp:TextBox>
                                            <%-- <asp:TextBox ID="txtVatPer" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Payment
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DDPFlandPayment" OnSelectedIndexChanged="DDPFlandPayment_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <%--  <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Total Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandTotalcmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                VAT Amount
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandVatAmount" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Credit Card
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DDPFlandCreditCard" runat="server" CssClass="form-control ">
                                                <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <%-- <asp:TextBox ID="txtCreditcard" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Non Commissionable
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandNoncmbl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandCommIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Due from Client
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandDuefromclient" runat="server" CssClass="form-control  decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Less Commission
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandLessComm" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Due to Supplier
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPFlandDuetoSupplier" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                CO2 Emission(actual)
                                            </label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtPFlandCO2" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-1">
                                            <label class="control-label">
                                                Kgs
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <%-- <label class="control-label">
                           Less Commission
                        </label>--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <%-- <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <%--  <label class="control-label">
                          Due to Supplier
                        </label>--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <%-- <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" MaxLength="50" />--%>

                                            <%--hidden Fields Added--%>
                                            <input type="hidden" name="txtPFLandarrId" id="txtPFLandarrId" value="" />

                                            <%--END--%>
                                        </div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="PFLandArrSubmit" runat="server" class="btn btn-primary" OnClick="PFLandArrSubmit_Click" Text="Submit" ValidationGroup="PFlandsupplier" />&nbsp;&nbsp;
                                            <asp:Button ID="Cancle" runat="server" class="btn btn-danger" OnClick="Cancel_Click" Text="Cancel" />
                                        </div>
                                        <%-- <div class="col-sm-4">

                                            <asp:Button ID="Reset" runat="server" class="btn btn-info" OnClick="Reset_Click" Text="Reset" />
                                        </div>--%>
                                    </div>
                                    <div class="overlaypop">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                            <ProgressTemplate>
                                                <img src="../images/loading.gif" alt="" height="40" width="40" />
                                                <br />
                                                <h4>Please wait....</h4>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>


                            </asp:Panel>

                            <%-- Service Fee --%>

                            <cc1:ModalPopupExtender ID="PFSerPopupExtender" runat="server" TargetControlID="btnPFserFee"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnPFserFee" CancelControlID="bntCancelFP"
                                PopupControlID="pnlPFServiceFee">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="pnlPFServiceFee" runat="server" CssClass="panelpopup" Width="800px" Height="400px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>
                                <div id="PFSerpopupdiv" title="Basic modal dialog" class="modalBackground">

                                    <div class="form-group">
                                        <header class="panel-heading">
                                            <div style="padding-top: 3px; padding-right: 3px;">
                                                <asp:ImageButton ID="ImageButton2" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                                <h4 class="panel-title">Service Fee</h4>
                                            </div>
                                        </header>
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFServiceType" runat="server" Class="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPFServiceType_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFServiceType" runat="server" ID="rfvddlPFServiceType" ValidationGroup="PFservicefee"
                                                    ErrorMessage="Select Service Type" Text="Select Service Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Source Reference<span class="style1">*</span>
                                                </label>
                                            </div>

                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFSoureceref" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnTextChanged="ddlPFSoureceref_TextChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select TicketNo--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                           <asp:RequiredFieldValidator ControlToValidate="ddlPFSoureceref" runat="server" ID="rfvddlPFSoureceref" ValidationGroup="PFservicefee"
                                                    ErrorMessage="Select Service Ref" Text="Select Service Ref" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                            <div class="col-sm-1">
                                                <label class="control-label">
                                                    Merge? 
                                                </label>
                                            </div>


                                            <div class="col-sm-2">

                                                <asp:CheckBox ID="chkPFMerge" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Travel Date<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFSerTravelDate" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFSerTravelDate" runat="server" ID="rfvtxtPFSerTravelDate"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Enter Travel Date." Text="Enter Travel Date." ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger Name<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFPassengerName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select PaasengerName--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFPassengerName" runat="server" ID="rfvddlPFPassengerName"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Select Passenger Name" Text="Select Passenger Name" ForeColor="red" Display="Dynamic" InitialValue="0" />

                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Exclusive Amount<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFExclusAmount" runat="server" class="form-control" ReadOnly="true" placeholder="0.00" Style="text-align: right;"    />


                                                <asp:RequiredFieldValidator ControlToValidate="txtPFExclusAmount" runat="server" ID="rfvtxtPFExclusAmount" ValidationGroup="PFservicefee"
                                                    ErrorMessage="Enter Exclusive Amount" Text="Enter Exclusive Amount" class="validationred" Display="Dynamic" ForeColor="Red" />
                                                <%--<asp:RequiredFieldValidator ControlToValidate="txtTargetAmount" runat="server" ID="RequiredFieldValidator1" ValidationGroup="subbmit"
                        ErrorMessage="Enter Service Fee" Text="Enter Target Amount" class="validationred" Display="Dynamic" />
                    <asp:RegularExpressionValidator ControlToValidate="txtTargetAmount" runat="server" ID="RegularExpressionValidator1" ValidationGroup="subbmit"
                        ErrorMessage="Enter  number only." Text="Enter  number only."
                        ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                                --%>
                                            </div>


                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Details
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFserDetails" runat="server" class="form-control" MaxLength="20" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT(%)</label>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtPFSerVatPer" runat="server" class="form-control" Style="text-align: right; width: 60px;" ReadOnly="true" />
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtPFSerVatAmount" runat="server" class="form-control" ReadOnly="true" Style="text-align: right; width: 60px;" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Payment Method<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <%--<asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="--Select  --" Value="-1" ></asp:ListItem>
                     </asp:DropDownList>--%>

                                                <asp:DropDownList ID="ddlPFPaymentMethod" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPFPaymentMethod_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Value="-1" Text="-- Select Type --" />--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFPaymentMethod" runat="server" ID="rfvddlPFPaymentMethod"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Select Payment Type" InitialValue="0" Text="Select Payment Type" ForeColor="red" Display="Dynamic" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Client Total</label>
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:TextBox ID="txtPFSerClientTotal" runat="server" class="form-control" OnTextChanged="txtPFSerClientTotal_TextChanged" AutoPostBack="true" placeholder="0.00" Style="text-align: right;" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    CreditCard Type
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlPFCreditCardType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFCreditCardType" runat="server" ID="rfvddlPFCreditCardType"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Select CreditCard Type" InitialValue="0" Text="Select CreditCard Type" ForeColor="red" Display="Dynamic" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Collect Via</label>
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFCollectVia" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPFCollectVia_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFCollectVia" runat="server" ID="rfvddlPFCollectVia"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Select Collect Via" InitialValue="0" Text="Select Collect Via" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    TASF MPD
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFTASFMPD" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPFTASFMPD" runat="server" ID="rfvtxtPFTASFMPD"
                                                    ValidationGroup="PFservicefee" ErrorMessage="Enter TASF MPD" Text="Enter TASFMPD" ForeColor="red" Display="Dynamic" />

                                            </div>
                                        </div>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="PFSerSubmit" class="btn btn-primary" ValidationGroup="PFservicefee"
                                                Text="Submit" OnClick="PFServFee_click" />&nbsp;
                    <asp:Button runat="server" ID="SerCancel"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnSerCancel_Click" />


                                        </div>
                                    </div>
                                    <div class="overlaypop">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                            <ProgressTemplate>
                                                <img src="../images/loading.gif" alt="" height="40" width="40" />
                                                <br />
                                                <h4>Please wait....</h4>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- General Charge --%>

                            <cc1:ModalPopupExtender ID="GenPFPopupExtender" runat="server" TargetControlID="btnPFGencharge"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnPFGencharge" CancelControlID="bntCancelFP"
                                PopupControlID="pnlPFGeneralCharge">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="pnlPFGeneralCharge" runat="server" CssClass="panelpopup" Width="900px" Height="550px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>
                                <div id="popupPFServfeediv" title="Basic modal dialog" class="modalBackground">

                                    <div class="form-group">
                                        <header class="panel-heading">
                                            <div style="padding-top: 3px; padding-right: 3px;">
                                                <asp:ImageButton ID="ImageButton1" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                                <h4 class="panel-title">General Charge</h4>
                                            </div>
                                        </header>
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFGenchrgType" runat="server" Class="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPFGenchrgType_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFGenchrgType" runat="server" ID="rfvddlPFGenchrgType" ValidationGroup="PFgeneralcharge"
                                                    ErrorMessage="Select Service Type" Text="Select Service Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />


                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger Name<span class="style1">*</span>
                                                </label>
                                            </div>

                                            <div class="col-sm-2">

                                                <asp:DropDownList ID="ddlPFPassengerNames" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select Passenger Name--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlPFPassengerNames" runat="server" ID="rfvddlPFPassengerNames" ValidationGroup="PFgeneralcharge"
                                                ErrorMessage="Select Passenger Name" Text="Select Passenger Name" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Details</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFDetails" runat="server" class="form-control" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    CreditCard Type <span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlPFCrdCardType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select CreditCard--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPFCrdCardType" runat="server" ID="rfvddlPFCrdCardType" ValidationGroup="PFgeneralcharge"
                                                    ErrorMessage="Select CreditCard Type " Text="Select CreditCard Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Units<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFUnits" runat="server" class="form-control" placeholder="0" Style="text-align: right;" OnTextChanged="txtPFUnits_TextChanged" AutoPostBack="true" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFUnits" runat="server" ID="rfvtxtPFUnits" ValidationGroup="PFgeneralcharge"
                                                    ErrorMessage="Enter Units" Text="Enter Units" class="validationred" Display="Dynamic" ForeColor="Red" />


                                            </div>


                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Rate Net<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFRateNet" runat="server" class="form-control" placeholder="0" Style="text-align: right;" OnTextChanged="txtPFRateNet_TextChanged" AutoPostBack="true" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtPFRateNet" runat="server" ID="rfvtxtRateNet" ValidationGroup="PFgeneralcharge"
                                                    ErrorMessage="Enter Rate Net" Text="Enter Rate Net" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT(%)</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFgenvat" runat="server" class="form-control" ReadOnly="true" Style="text-align: right;" />

                                            </div>

                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT Amount
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFVatAmount" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Exclusive Amount</label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFExcluAmount" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Client Total
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPFClientTotal" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />
                                            </div>
                                        </div>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">

                                            <asp:Button runat="server" ID="PFGenSubmit" class="btn btn-primary" ValidationGroup="PFgeneralcharge"
                                                Text="Submit" OnClick="btnPFGencharge_click" />&nbsp;
                    <asp:Button runat="server" ID="GenCancel"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />


                                        </div>
                                    </div>
                                    <div class="overlaypop">
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                            <ProgressTemplate>
                                                <img src="../images/loading.gif" alt="" height="40" width="40" />
                                                <br />
                                                <h4>Please wait....</h4>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="PFInvGridView" runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <asp:HiddenField ID="hf_ProInvListId" runat="server" Value="0" />

                                        <asp:GridView ID="PFInvListGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
                                            AutoGenerateColumns="False" DataKeyNames="Type" CssClass="table table-striped table-bordered"
                                            ShowHeaderWhenEmpty="true" OnRowCommand="PFInvListGrid_RowCommand" OnPageIndexChanging="PFInvListGrid_PageIndexChanging">
                                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <%#Eval("Type")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <%#Eval("Details")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Total" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("ClientTotal")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                    <div class="col-sm-4">
                                        <asp:HiddenField ID="Hf_ProInvLineCount" runat="server" Value="0" />

                                        <asp:GridView ID="PFInvoiceLineItemCountGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
                                            AutoGenerateColumns="False" DataKeyNames="Type" CssClass="table table-striped table-bordered"
                                            ShowHeaderWhenEmpty="true">
                                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Source">
                                                    <ItemTemplate>
                                                        <%#Eval("Type")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Count">
                                                    <ItemTemplate>
                                                        <%#Eval("Count")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("TotalAmount")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                        &nbsp;&nbsp;
                                          <div class="btncancle">
                                              <asp:Label ID="LBLPFInvoiceTotalAmount" runat="server" Font-Bold="true" ForeColor="#0088cc">Invoice Total:</asp:Label>
                                              &nbsp;
                                
                                    <asp:Label ID="txtPFInvoiceTotalAmount" runat="server" Font-Bold="true"></asp:Label>

                                          </div>

                                        <asp:Label ID="lblPFcommissionInclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblPFcommissionExclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblPFcommissionVatamt" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblPFaircommiinclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblPFlandcommiInclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblPFservcommi" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblPFaircommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblPFlandcommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblPFsercommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </asp:Panel>


                            <%-- model PopUp code End --%>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Messages
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlPFInvMesg" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <%--<asp:ListItem Text="--Select Message Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-4">
                                        <asp:TextBox TextMode="MultiLine" CssClass="form-control" ID="txtPFInvClntMesg" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Print Style
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlPFInvPdfPrintStyle" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select Print Style--" Value="-1" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>



                                    </div>
                                </div>
                            </div>

                            <%-- A/C Analysis --%>

                            <%--  <cc1:ModalPopupExtender ID="PFACAnalysisPopupExtender" runat="server" TargetControlID="btnPFACAnalysis"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnPFACAnalysis" CancelControlID="bntCancelFP"
                                PopupControlID="pnlPFACAnalysis">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="pnlPFACAnalysis" runat="server" CssClass="panelpopup" Width="800px" Height="400px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <div style="float: right; padding-top: 3px; padding-right: 3px;">
                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                    </div>
                                </div>
                                <div id="popupPFACAnalysis" title="Basic modal dialog" class="modalBackground">


                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">TravelWork Cc</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Total fare
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">Passengers</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Less Charged to Credit Card
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">Passengers</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Credit/Decrese A/C Receivable
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">Passengers</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Credit/Increase Comm.Income
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">Passengers</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Vat Laibility
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>


                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">Passengers</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="control-label">
                                                    Less Charged to Credit Card
                                                </label>
                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>
                                    </div>


                                    <div class="col-sm-12">

                                        <asp:GridView ID="PFACAnalysisGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
                                            AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
                                            ShowHeaderWhenEmpty="true">
                                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Air Passenger">
                                                    <ItemTemplate>
                                                        <%#Eval("AirPassenger")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AirTicketNo">
                                                    <ItemTemplate>
                                                        <%#Eval("AirTicketNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AirTicket Type">
                                                    <ItemTemplate>
                                                        <%#Eval("AirTicketType")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

                                        </asp:GridView>
                                    </div>




                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">

                                            <asp:Button runat="server" ID="btnSubmitPFACAnalsys" class="btn btn-success"
                                                Text="Submit" OnClick="btnSubmitPFACAnalsys_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancleACAnalysis"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" />


                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>--%>


                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-5">
                                    <asp:Button runat="server" ID="btnPFInvSave" class="btn btn-primary green" ValidationGroup="PFinvoice"
                                        Text="Submit" OnClick="btnPFInvSave_Click" />&nbsp;
                            <%--  <asp:Button ID="btnPFACAnalysis" runat="server" CssClass=" btn btn-info" ValidationGroup="analysis" Text="A/C Analysis" />--%>
                                    <asp:Button runat="server" ID="btnInvCancel"
                                        class="btn btn-primary red" ValidationGroup="" OnClick="btnInvCancel_Click" Text="Cancel" />&nbsp;
                                  <%--   <asp:Button runat="server" ID="btnPFDraftPdf"
                                         class="btn btn-primary red" ValidationGroup="" Text="DraftPdf" OnClick="btnPFDraftPdf_Click" />--%>
                                </div>
                                <div class="col-sm-3">
                                </div>
                                  <div class="overlay">
                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                    <ProgressTemplate>
                                        <img src="../images/loading.gif" alt="" height="40" width="40" />
                                        <br />
                                        <h4>Please wait....</h4>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                      </div>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

