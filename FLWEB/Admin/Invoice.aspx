<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Admin_Invoice" %>

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

        .hiddencol {
            display: none;
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

        ContentPlaceHolder1_pnlFlight {
            left: -66px !important;
            top: -27px !important;
        }

        .test {
            color: white;
            border-radius: 4px;
        }

        #txtInvClntMesg {
            overflow-y: scroll;
            max-height: 40px;
        }

        #ContentPlaceHolder1_txtInvClntMesg {
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
             $('#ContentPlaceHolder1_txtInvDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtInvDate").datepicker({
                format: 'yyyy-mm-dd',
                startDate: '-9d',
                endDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;



            $("#ContentPlaceHolder1_txtAirTravelDate").datepicker({

                onSelect: function (selected) {

                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtDate1").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtDate2").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtDate3").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtDate4").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtAirTravelDate").val(dtFormatted);
                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtAirReturnDate").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtAirTravelDate").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtAirReturnDate").val(dtFormatted);

                }
            }).attr('readonly', 'true');;

            //--------------------------------------------------------------------------------------------
            $("#ContentPlaceHolder1_txtDate1").datepicker({
                onSelect: function (selected) {
                    debugger;
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var y = dtMax.getFullYear();
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var dtFormatted = mm + '/' + dd + '/' + y;

                    $("#ContentPlaceHolder1_txtDate2").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);

                    $("#ContentPlaceHolder1_txtDate2").val('');
                    $("#ContentPlaceHolder1_txtDate3").val('');
                    $("#ContentPlaceHolder1_txtDate4").val('');
                    $("#ContentPlaceHolder1_txtAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtAirTravelDate").val($("#ContentPlaceHolder1_txtDate1").val());



                }


            }).attr('readonly', 'true');;




            $("#ContentPlaceHolder1_txtDate2").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtDate3").datepicker("option", "minDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtDate3").val('');
                    $("#ContentPlaceHolder1_txtDate4").val('');
                    $("#ContentPlaceHolder1_txtAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtDate2").val(dtFormatted);
                }
            }).attr('readonly', 'true');;
            $("#ContentPlaceHolder1_txtDate3").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtDate4").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtDate4").val('');
                    $("#ContentPlaceHolder1_txtAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtDate3").val(dtFormatted);
                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtDate4").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    //  $("#ContentPlaceHolder1_txtDate1").datepicker("option", "minDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtAirReturnDate").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtAirReturnDate").val('');
                    $("#ContentPlaceHolder1_txtDate4").val(dtFormatted);
                }

            }).attr('readonly', 'true');;


           // $('#ContentPlaceHolder1_txtlandTravelFrom').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtlandTravelFrom").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtlandTravelto").datepicker("option", "minDate", dtFormatted);
                    $("#ContentPlaceHolder1_txtlandTravelFrom").val(dtFormatted);

                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtlandTravelto").datepicker({
                onSelect: function (selected) {
                    var dtMax = new Date(selected);
                    dtMax.setDate(dtMax.getDate());
                    var dd = ('0' + dtMax.getDate()).slice(-2);
                    var mm = ('0' + (dtMax.getMonth() + 1)).slice(-2);
                    var y = dtMax.getFullYear();
                    var dtFormatted = mm + '/' + dd + '/' + y;
                    $("#ContentPlaceHolder1_txtlandTravelFrom").datepicker("option", "maxDate", dtFormatted)
                    $("#ContentPlaceHolder1_txtlandTravelto").val(dtFormatted);


                }
            }).attr('readonly', 'true');;




            $("#ContentPlaceHolder1_txtSerTravelDate").datepicker({

                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
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
                    <h2 class="panel-title">New Invoice</h2>
                </header>
                <asp:HiddenField ID="hf_InvId" runat="server" Value="0" />
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Date<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtInvDate" runat="server" CssClass="form-control" MaxLength="50" BackColor="White" />
                                <asp:RequiredFieldValidator ControlToValidate="txtInvDate" runat="server" ID="rfvtxtInvDate" ValidationGroup="invoice"
                                    ErrorMessage="Select Date" Text="Select Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Client Type<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">

                                <asp:DropDownList ID="drpInvClientType" runat="server" CssClass="form-control" OnTextChanged="drpInvClientType_TextChanged" AutoPostBack="true">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="drpInvClientType" runat="server" ID="RequiredFieldValidator1" ValidationGroup="invoice"
                                    ErrorMessage="Select Client Type" Text="Select Client Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Client <span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpInvClientName" runat="server" CssClass="form-control" OnTextChanged="drpInvClientName_TextChanged" AutoPostBack="true">
                                    <asp:ListItem Text="--Select Client Name--" Value="0" Selected="True"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="drpInvClientName" runat="server" ID="rfvdrpInvClientName" ValidationGroup="invoice"
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
                                <asp:DropDownList ID="ddlInvCosultant" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlInvCosultant" runat="server" ID="rfvdrpInvCosultant" ValidationGroup="invoice"
                                    ErrorMessage="Select Consultant" Text="Select Consultant" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Order No<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtInvOrder" runat="server" CssClass="form-control" MaxLength="30" />
                                <asp:RequiredFieldValidator ControlToValidate="txtInvOrder" runat="server" ID="rfvtxtInvOrder" ValidationGroup="invoice"
                                    ErrorMessage="Enter Order Number" Text="Enter Order Number" class="validationred" Display="Dynamic" ForeColor="Red" />
                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Booking No
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtInvBookNo" runat="server" CssClass="form-control" MaxLength="50" />

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
                                <asp:DropDownList ID="drpInvBookingSrc" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="--Select Booking Source--" Value="-1" Selected="True"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Booking Destination
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="drpInvBookDest" runat="server" CssClass="form-control" AppendDataBoundItems="true">
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

                                        <button runat="server" id="btnOpenFP" class="btn btn-mini" title="AirTicket"> 
                                            <i class="fa fa-plane"></i>
                                        </button>
                                        <asp:Button ID="bntCancelFP" runat="server" Text="Cancel" Style="display: none;" />
                                        <%--<asp:Button ID="btnOpenFP" runat="server" Text="Open"  />--%>
                        &nbsp;&nbsp;&nbsp;
                         <button runat="server" id="btnLand" class="btn btn-mini" title="Land Arrangement">
                             <i class="fa  fa-university"></i>
                         </button>
                                        &nbsp;&nbsp;
                         <button runat="server" id="btnserFee" onserverclick="btnserFee_ServerClick" class="btn btn-mini" title="Service Fee">
                             <i class="fa fa-keyboard-o"></i>
                         </button>
                                        &nbsp;&nbsp;
                         <button runat="server" id="btnGencharge" onserverclick="btnGencharge_ServerClick" class="btn btn-mini" title="General Charge">
                             <i class="fa fa-line-chart"></i>
                         </button>
                                        &nbsp;&nbsp;
                                    </div>
                                </div>
                            </div>

                            <%-- AirTicket --%>

                            <cc1:ModalPopupExtender ID="VASPopupExtender" runat="server" TargetControlID="btnOpenFP"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnOpenFP" CancelControlID="bntCancelFP"
                                PopupControlID="pnlFlight">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlFlight" runat="server" CssClass="panelpopup " Width="1000px" Height="550px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="cmdClose" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>

                                <div id="popupdiv" class="modalBackground">
                                    <header class="panel-heading">
                                        <div style="padding-top: 3px; padding-right: 3px;">
                                            <asp:ImageButton ID="ImageButton5" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                            <h4 class="panel-title">AirTicket</h4>
                                        </div>
                                    </header>
                                    <asp:HiddenField ID="hf_Air_TicketNo" runat="server" Value="0" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Ticket No<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAirTicketNo" runat="server" CssClass="form-control" OnTextChanged="txtAirTicketNo_TextChanged" AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAirTicketNo" runat="server" ID="rfvtxtAirTicketNo" ValidationGroup="airticket"
                                                    ErrorMessage="Enter Ticket Number" Text="Enter Ticket Number" class="validationred" Display="Dynamic" ForeColor="Red" />

                                                <%-- <asp:RegularExpressionValidator ControlToValidate="txtAirTicketNo" runat="server" ForeColor="Red"
                            ID="revtxtAirTicketNo" ValidationGroup="airticket" ErrorMessage="Enter Valid Ticket No."
                            Text="Enter Valid Ticket No." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Ticket Type
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="drpTicketType" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="drpTicketType" runat="server" ID="rfvdrpTicketType" ValidationGroup="airticket"
                                                    ErrorMessage="Select Ticket Type" Text="Select Ticket Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    PNR<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtPnr" runat="server" CssClass="form-control uppercase" MaxLength="6" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPnr" runat="server" ID="rfvtxtPnr" ValidationGroup="airticket"
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
                                                <asp:DropDownList ID="ddlAirLine" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAirLine_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select Airline--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlAirLine" runat="server" ID="rfvddlAirLine" ValidationGroup="airticket"
                                                    ErrorMessage="Select Airline" Text="Select Airline" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />


                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger Name<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="drpAirPassenger" runat="server" CssClass="form-control">
                                    
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="drpAirPassenger" runat="server" ID="rfvdrpAirPassenger" ValidationGroup="airticket"
                                                    ErrorMessage="Enter Passenger Name" Text="Enter Passenger Name" class="validationred" Display="Dynamic" ForeColor="Red" />

                                                <asp:RegularExpressionValidator ControlToValidate="drpAirPassenger" runat="server" ForeColor="Red"
                                                    ID="revdrpAirPassenger" ValidationGroup="airticket" ErrorMessage="Enter Only Characters."
                                                    Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                                                    Display="Dynamic"></asp:RegularExpressionValidator>

                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Conjunction
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAirConjunction" runat="server" CssClass="form-control" />

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
                                                <asp:DropDownList ID="ddlAirService" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAirService_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select Service--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlAirService" runat="server" ID="rfvdrpAirService" ValidationGroup="airticket"
                                                    ErrorMessage="Select Service" Text="Select Service" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlType" runat="server" ID="rfvddlType" ValidationGroup="airticket"
                                                    ErrorMessage="Select Type" Text="Select Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Routing<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAirRouting" runat="server" CssClass="form-control uppercase" OnTextChanged="txtAirRouting_TextChanged" AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAirRouting" runat="server" ID="rfvtxtAirRouting" ValidationGroup="airticket"
                                                    ErrorMessage="Enter Routing" Text="Enter Routing" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:HiddenField ID="hf_Rout_Id1" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_Rout_Id2" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_Rout_Id3" runat="server" Value="0" />
                                    <asp:HiddenField ID="hf_Rout_Id4" runat="server" Value="0" />


                                    <div class="form-group" id="divRouteHead" runat="server">
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
                                                <asp:TextBox ID="txtAirMiles" runat="server" CssClass="form-control" />

                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group" id="divRouting" runat="server">
                                        <div class="col-sm-12" id="divRoutingtxt" runat="server">
                                            <div class="col-sm-2" id="divlblRoute" runat="server">
                                                <%--<label class="control-label" id="lblRoutes1" runat="server"></label>--%>
                                                <asp:TextBox ID="lblRoutes1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtFlightNo">
                                                <asp:TextBox ID="txtFlightNo1" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtFlightNo1" runat="server" ID="rfvtxtFlightNo1"
                                                    ValidationGroup="airticket" ErrorMessage="Enter TASF MPD" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />

                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtClass">

                                                <asp:TextBox ID="txtClass1" runat="server" CssClass="form-control uppercase" MaxLength="2"   />
                                                <asp:RequiredFieldValidator ControlToValidate="txtClass1" runat="server" ID="rfvtxtClass1"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtDate">
                                                <asp:TextBox ID="txtDate1" runat="server" CssClass="form-control" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" placeholder="YYYY-MM-DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtDate1" runat="server" ID="rfvtxtDate1"
                                                    ValidationGroup="airticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

                                            </div>


                                        </div>

                                        <div class="col-sm-12" id="div1" runat="server">
                                            <div class="col-sm-2" id="div3" runat="server">
                                                <%--<label class="control-label" id="lblRoutes2" runat="server"></label>--%>
                                                <asp:TextBox ID="lblRoutes2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtFlightNo1">
                                                <asp:TextBox ID="txtFlightNo2" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtFlightNo2" runat="server" ID="rfvtxtFlightNo2"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />

                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtClass1">

                                                <asp:TextBox ID="txtClass2" runat="server" CssClass="form-control uppercase" MaxLength="2" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtClass2" runat="server" ID="rfvtxtClass2"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtDate1">
                                                <asp:TextBox ID="txtDate2" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtDate2" runat="server" ID="rfvtxtDate2"
                                                    ValidationGroup="airticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

                                            </div>

                                        </div>

                                        <div class="col-sm-12" id="div2" runat="server">
                                            <div class="col-sm-2" id="div4" runat="server">
                                                <%--<label class="control-label" id="lblRoutes3" runat="server"></label>--%>
                                                <asp:TextBox ID="lblRoutes3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtFlightNo2">
                                                <asp:TextBox ID="txtFlightNo3" runat="server" CssClass="form-control" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtFlightNo3" runat="server" ID="rfvtxtFlightNo3"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />


                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtClass2">

                                                <asp:TextBox ID="txtClass3" runat="server" CssClass="form-control uppercase" MaxLength="2" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtClass3" runat="server" ID="rfvtxtClass3"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtDate2">
                                                <asp:TextBox ID="txtDate3" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtDate3" runat="server" ID="rfvtxtDate3"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Class" Text="Select Date" ForeColor="red" Display="Dynamic" />


                                            </div>
                                        </div>

                                        <div class="col-sm-12" id="div5" runat="server">
                                            <div class="col-sm-2" id="div6" runat="server">
                                                <%--<label class="control-label" id="lblRoutes4" runat="server"></label>--%>
                                                <asp:TextBox ID="lblRoutes4" runat="server" CssClass="form-control"> </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtFlightNo3">
                                                <asp:TextBox ID="txtFlightNo4" runat="server" CssClass="form-control" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtFlightNo4" runat="server" ID="rfvtxtFlightNo4"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Flight No" Text="Enter Flight No" ForeColor="red" Display="Dynamic" />


                                            </div>

                                            <div class="col-sm-2" runat="server" id="divtxtClass3">

                                                <asp:TextBox ID="txtClass4" runat="server" CssClass="form-control uppercase" MaxLength="2" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtClass4" runat="server" ID="rfvtxtClass4"
                                                    ValidationGroup="airticket" ErrorMessage="Enter Class" Text="Enter Class" ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-2" runat="server" id="divtxtDate3">
                                                <asp:TextBox ID="txtDate4" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtDate4" runat="server" ID="rfvtxtDate4"
                                                    ValidationGroup="airticket" ErrorMessage="Select Date" Text="Select Date" ForeColor="red" Display="Dynamic" />

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
                                                <asp:TextBox ID="txtAirTravelDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD"  BackColor="White"/>
                                                <asp:RequiredFieldValidator ControlToValidate="txtAirTravelDate" runat="server" ID="rfvtxtAirTravelDate" ValidationGroup="airticket"
                                                    ErrorMessage="Select Travel Date" Text="Select Travel Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Return Date
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtAirReturnDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD"   BackColor="White"/>
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
                                                <asp:TextBox ID="txtAirExcluisvefare" runat="server" CssClass="form-control decimalRight" AutoPostBack="true" OnTextChanged="txtExcluisvefare_TextChanged" placeholder="0.00" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAirExcluisvefare" runat="server" ID="rfvtxtAirExcluisvefare" ValidationGroup="airticket"
                                                    ErrorMessage="Enter Exclusive Amount" Text="Enter Exclusive Amount" class="validationred" Display="Dynamic" ForeColor="Red" />


                                                <asp:RegularExpressionValidator ID="revtxtAirExcluisvefare" ControlToValidate="txtAirExcluisvefare" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>

                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision%
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirCommisionper" runat="server" CssClass="form-control decimalRight" placeholder="0.00" AutoPostBack="true" OnTextChanged="txtAirCommisionper_TextChanged" />

                                                <asp:RegularExpressionValidator ID="revtxtAirCommisionper" ControlToValidate="txtAirCommisionper" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox ID="txtVatPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:TextBox ID="txtAirVatOnFare" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision(Exclusive)
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirCommExclu" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
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
                                                <asp:TextBox ID="txtAirportTax" runat="server" CssClass="form-control decimalRight" placeholder="0.00" OnTextChanged="txtAirportTax_TextChanged" AutoPostBack="true" />

                                                <asp:RegularExpressionValidator ID="revtxtAirportTax" ControlToValidate="txtAirportTax" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>


                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT%
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirCommVat" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />

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
                                                <asp:TextBox ID="txtAirVatinclTax" runat="server" CssClass="form-control decimalRight" placeholder="0.00" ReadOnly="true" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Agent VAT
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirAgentVat" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
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
                                                <asp:TextBox ID="txtAirClientTot" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Commision(Inclusive)
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirCommInclu" runat="server" CssClass="form-control decimalRight" placeholder="0.00" />
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
                                                <asp:DropDownList ID="ddlAirPayment" runat="server" CssClass="form-control decimalRight">
                                                    <asp:ListItem Text="--Select Payment--" Value="-1" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Due to BSP
                                                </label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAirDueToBsp" runat="server" CssClass="form-control decimalRight" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnAirSubmit"  OnClick="btnAirSubmit_Click1" class="btn btn-primary" ValidationGroup="airticket"
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

                            <cc1:ModalPopupExtender ID="landPopExtender" runat="server" TargetControlID="btnLand"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnLand" CancelControlID="bntCancelFP"
                                PopupControlID="landPanel">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="landPanel" runat="server" CssClass="panelpopup modalBackground " Width="80%" Height="95%" Style="display: none;">
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
                                    <asp:HiddenField ID="hf_Land_TicketNo" runat="server" Value="0" />
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Land Supplier<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="DDlandSupplier" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDlandSupplier_SelectedIndexChanged">
                                                <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDlandSupplier" runat="server" ID="rfvDDlandSupplier" ValidationGroup="landsupplier"
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
                                            <asp:DropDownList ID="DDlandService" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="DDlandService_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDlandService" runat="server" ID="rfvDDlandService" ValidationGroup="landsupplier"
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
                                            <asp:DropDownList ID="DDlandType" runat="server" OnSelectedIndexChanged="DDType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="DDlandType" runat="server" ID="rfvDDlandType" ValidationGroup="landsupplier"
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

                                            <asp:TextBox ID="txtlandPassName" runat="server" CssClass="form-control" placeholder="Passenge Name" MaxLength="50" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtlandPassName" runat="server" ID="rfvtxtlandPassName" ValidationGroup="landsupplier"
                                                ErrorMessage="Select Passenger" Text="Enter Passenger" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            <asp:RegularExpressionValidator ControlToValidate="txtlandPassName" runat="server" ForeColor="Red"
                                                ID="revtxtlandPassName" ValidationGroup="airticket" ErrorMessage="Enter Only Characters."
                                                Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                                                Display="Dynamic"></asp:RegularExpressionValidator>


                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Travel Dates<span class="style1">*</span>
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <%--<asp:TextBox ID="txtlandTravelFrom" runat="server" CssClass="form-control" placeholder="mm/dd/yy" MaxLength="50" />--%>
                                            <asp:TextBox ID="txtlandTravelFrom" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" BackColor="White"/>
                                            <asp:RequiredFieldValidator ControlToValidate="txtlandTravelFrom" runat="server" ID="rfvtxtlandTravelFrom" ValidationGroup="landsupplier"
                                                ErrorMessage="Select Travel Date" Text="Select Travel Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                to
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <%--<asp:TextBox ID="txtlandTravelto" runat="server" CssClass="form-control" placeholder="mm/dd/yy" MaxLength="50" />--%>
                                            <asp:TextBox ID="txtlandTravelto" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" BackColor="White" />
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
                                            <asp:TextBox ID="txtlandBookingRef" runat="server" CssClass="form-control" placeholder="Book ref NO" MaxLength="50" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtlandBookingRef" runat="server" ID="rfvtxtlandBookingRef" ValidationGroup="landsupplier"
                                                ErrorMessage="Enter Booking Ref No" Text="Enter Booking Ref No" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            <asp:RegularExpressionValidator ID="revtxtlandBookingRef" ControlToValidate="txtlandBookingRef" runat="server" ForeColor="Red"
                                                ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>


                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Voucher
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandVocher" runat="server" CssClass="form-control" placeholder="Vocher No" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Supplier Ref No
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandSupplierRef" runat="server" CssClass="form-control" placeholder="Supplier Ref No" MaxLength="50" />
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
                                            <asp:TextBox ID="txtlandSuppInvNo" runat="server" CssClass="form-control" placeholder="Supplier Invoice No" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Units
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandUnits" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandUnits_TextChanged" AutoPostBack="true" />

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commissionable(Excl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandcmblExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandcmblExcl_TextChanged" />
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
                                            <asp:TextBox ID="txtlandTotalExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandTotalExcl_TextChanged" AutoPostBack="true" />
                                            <asp:RequiredFieldValidator ControlToValidate="txtlandTotalExcl" runat="server" ID="rfvtxtlandTotalExcl" ValidationGroup="landsupplier"
                                                ErrorMessage="Enter TotalExclu" Text="Enter TotalExclu" class="validationred" Display="Dynamic" ForeColor="Red" />

                                            <asp:RegularExpressionValidator ID="revtxtlandTotalExcl" ControlToValidate="txtlandTotalExcl" runat="server" ForeColor="Red"
                                                ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>


                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Rate(Inclusive)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandRateIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandRateIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission%
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandCommPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandCommPer_TextChanged" AutoPostBack="true" />
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
                                            <asp:TextBox ID="txtLandExlVatPer" runat="server" CssClass="form-control decimalRight" placeholder="0.00" AutoPostBack="true">
                             <%--   <asp:ListItem Text="Select item" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Test1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Test2" Value="2"></asp:ListItem>--%>
                                            </asp:TextBox>


                                            <%--<asp:TextBox ID="txtVat" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                            <%-- <asp:RegularExpressionValidator ID="revVat" runat="server" ControlToValidate="txtVat" ValidationExpression="^\d+" ErrorMessage="Enter Only Numbers" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtlandExclVatAmount" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandCmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission(Excl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandCommExcl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
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
                                            <asp:TextBox ID="txtlandTotalIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandTotalIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Other Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandOtherCmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" OnTextChanged="txtlandOtherCmblIncl_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                VAT%
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtLandVatPer" runat="server" CssClass="form-control decimalRight" AutoPostBack="true">
                                
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
                                            <asp:DropDownList ID="DDlandPayment" OnSelectedIndexChanged="DDlandPayment_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <%--  <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" MaxLength="50" />--%>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Total Commissionable(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandTotalcmblIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                VAT Amount
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandVatAmount" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
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
                                            <asp:DropDownList ID="DDlandCreditCard" runat="server" CssClass="form-control ">
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
                                            <asp:TextBox ID="txtlandNoncmbl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Commission(Incl)
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandCommIncl" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
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
                                            <asp:TextBox ID="txtlandDuefromclient" runat="server" CssClass="form-control  decimalRight" placeholder="0.00" MaxLength="50" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Less Commission
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandLessComm" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Due to Supplier
                                            </label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtlandDuetoSupplier" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />

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
                                            <asp:TextBox ID="txtlandCO2" runat="server" CssClass="form-control decimalRight" placeholder="0.00" MaxLength="50" />
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
                                            <input type="hidden" name="txtLandarrId" id="txtLandarrId" value="" />

                                            <%--END--%>
                                        </div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="LandArrSubmit" runat="server" class="btn btn-primary" OnClick="LandArrSubmit_Click"  Text="Submit" ValidationGroup="landsupplier" />&nbsp;&nbsp;
                                    <asp:Button ID="Cancle" runat="server" class="btn btn-danger" OnClick="Cancel_Click" Text="Cancel" />
                                        </div>
                                        <%--<div class="col-sm-4">

                                    <asp:Button ID="Reset" runat="server" class="btn btn-info" OnClick="Reset_Click" Text="Reset" />
                                </div>--%>
                                    </div>
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


                            </asp:Panel>

                            <%-- Service Fee --%>

                            <cc1:ModalPopupExtender ID="SerPopupExtender" runat="server" TargetControlID="btnserFee"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnserFee" CancelControlID="bntCancelFP"
                                PopupControlID="pnlServiceFee">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="pnlServiceFee" runat="server" CssClass="panelpopup" Width="800px" Height="400px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>
                                <div id="Serpopupdiv" class="modalBackground">

                                    <div class="form-group">
                                        <header class="panel-heading">
                                            <div style="padding-top: 3px; padding-right: 3px;">
                                                <asp:ImageButton ID="ImageButton2" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                                <h4 class="panel-title">Service Fee</h4>
                                            </div>
                                        </header>
                                        <asp:HiddenField ID="hf_SF_TicketNo" runat="server" Value="0" />
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlservatType" runat="server" Class="form-control" AppendDataBoundItems="true" AutoPostBack="true" >
                                                 
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlServiceType" runat="server" Class="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceType_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlServiceType" runat="server" ID="rfvddlServiceType" ValidationGroup="servicefee"
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

                                                <asp:DropDownList ID="ddlSoureceref" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlSoureceref_TextChanged">
                                                    <%--<asp:ListItem Text="--Select TicketNo--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSoureceref" runat="server" ID="rfvddlSoureceref" ValidationGroup="servicefee"
                                                ErrorMessage="Select Ticket Number" Text="Select Ticket Number" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />


                                            <div class="col-sm-1">
                                                <label class="control-label">
                                                    Merge? 
                                                </label>
                                            </div>


                                            <div class="col-sm-2">

                                                <asp:CheckBox ID="chkMerge" runat="server" />

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
                                                <asp:TextBox ID="txtSerTravelDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" BackColor="White" />
                                                <%--<asp:TextBox ID="txtSerTravelDate" runat="server" CssClass="form-control" />--%>

                                                <asp:RequiredFieldValidator ControlToValidate="txtSerTravelDate" runat="server" ID="rfvtxtSerTravelDate"
                                                    ValidationGroup="servicefee" ErrorMessage="Enter Travel Date." Text="Enter Travel Date." ForeColor="red" Display="Dynamic" />


                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger Name<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:DropDownList ID="ddlPassengerName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select PaasengerName--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ControlToValidate="ddlPassengerName" runat="server" ID="rfvddlPassengerName"
                                            ValidationGroup="servicefee" ErrorMessage="Select Passenger Name" Text="Select Passenger Name" ForeColor="red" Display="Dynamic" InitialValue="0" />--%>
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
                                                <asp:TextBox ID="txtExclusAmount" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" ReadOnly="true" />


                                                <asp:RequiredFieldValidator ControlToValidate="txtExclusAmount" runat="server" ID="rfvtxtExclusAmount" ValidationGroup="servicefee"
                                                    ErrorMessage="Enter Exclusive Amount" Text="Enter Exclusive Amount" class="validationred" Display="Dynamic" ForeColor="Red" />

                                                <asp:RegularExpressionValidator ID="revtxtExclusAmount" ControlToValidate="txtExclusAmount" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>

                                            </div>


                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Details
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtserDetails" runat="server" class="form-control" MaxLength="20" />

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
                                                <asp:TextBox ID="txtSerVatPer" runat="server" class="form-control" Style="text-align: right; width: 60px;" ReadOnly="true" />
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtSerVatAmount" runat="server" class="form-control" ReadOnly="true" Style="text-align: right; width: 60px;" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Payment Method<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <%--<asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="--Select  --" Value="-1" ></asp:ListItem>
                     </asp:DropDownList>--%>

                                                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Value="-1" Text="-- Select Type --" />--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlPaymentMethod" runat="server" ID="rfvddlPaymentMethod"
                                                    ValidationGroup="servicefee" ErrorMessage="Select Payment Type" InitialValue="0" Text="Select Payment Type" ForeColor="red" Display="Dynamic" />

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

                                                <asp:TextBox ID="txtSerClientTotal" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" OnTextChanged="txtSerClientTotal_TextChanged" AutoPostBack="true" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    CreditCard Type
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCreditCardType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlCreditCardType" runat="server" ID="rfvddlCreditCardType"
                                                    ValidationGroup="servicefee" ErrorMessage="Select CreditCard Type" InitialValue="0" Text="Select CreditCard Type" ForeColor="red" Display="Dynamic" />

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

                                                <asp:DropDownList ID="ddlCollectVia" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectVia_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCollectVia" runat="server" ID="rfvddlCollectVia"
                                                    ValidationGroup="servicefee" ErrorMessage="Select Collect Via" InitialValue="0" Text="Select Collect Via" ForeColor="red" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    TASF MPD
                                                </label>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtTASFMPD" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtTASFMPD" runat="server" ID="rfvtxtTASFMPD"
                                                    ValidationGroup="servicefee" ErrorMessage="Enter TASF MPD" Text="Enter TASFMPD" ForeColor="red" Display="Dynamic" />

                                            </div>
                                        </div>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="SerSubmit" class="btn btn-primary"  ValidationGroup="servicefee"
                                                Text="Submit" OnClick="ServFee_click" />&nbsp;
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

                            <cc1:ModalPopupExtender ID="GenPopupExtender" runat="server" TargetControlID="btnGencharge"
                                BackgroundCssClass="popupextndrBg" BehaviorID="btnGencharge" CancelControlID="bntCancelFP"
                                PopupControlID="pnlGeneralCharge">
                            </cc1:ModalPopupExtender>

                            <asp:Panel ID="pnlGeneralCharge" runat="server" CssClass="panelpopup" Width="900px" Height="550px" Style="display: none;" BackgroundCssClass="modalBackground">
                                <div class="panelpopupheaderbox">
                                    <%--<div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>--%>
                                </div>
                                <div id="popupServfeediv" class="modalBackground">

                                    <div class="form-group">
                                        <header class="panel-heading">
                                            <div style="padding-top: 3px; padding-right: 3px;">
                                                <asp:ImageButton ID="ImageButton1" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                                <h4 class="panel-title">General Charge</h4>
                                            </div>
                                        </header>
                                        <asp:HiddenField ID="hf_GC_TicketNo" runat="server" Value="0" />
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Type<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:DropDownList ID="ddlGenchrgType" runat="server" Class="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlGenchrgType_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="--Select Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ControlToValidate="ddlGenchrgType" runat="server" ID="rfvddlGenchrgType" ValidationGroup="generalcharge"
                                                    ErrorMessage="Select Service Type" Text="Select Service Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />


                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Passenger Name<span class="style1">*</span>
                                                </label>
                                            </div>

                                            <div class="col-sm-3">

                                                <asp:DropDownList ID="ddlPassengerNames" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select Passenger Name--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlPassengerNames" runat="server" ID="rfvddlPassengerNames" ValidationGroup="generalcharge"
                                                ErrorMessage="Select Passenger Name" Text="Select Passenger Name" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Details</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDetails" runat="server" class="form-control" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    CreditCard Type <span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCrdCardType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <%--<asp:ListItem Text="--Select CreditCard--" Value="-1" Selected="True"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCrdCardType" runat="server" ID="rfvddlCrdCardType" ValidationGroup="generalcharge"
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
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtUnits" runat="server" class="form-control" placeholder="0" Style="text-align: right;" OnTextChanged="txtUnits_TextChanged" AutoPostBack="true" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtUnits" runat="server" ID="rfvtxtUnits" ValidationGroup="generalcharge"
                                                    ErrorMessage="Enter Units" Text="Enter Units" class="validationred" Display="Dynamic" ForeColor="Red" />

                                                <asp:RegularExpressionValidator ID="revtxtUnits" ControlToValidate="txtUnits" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>

                                            </div>


                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Rate Net<span class="style1">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtRateNet" runat="server" class="form-control" placeholder="0" Style="text-align: right;" OnTextChanged="txtRateNet_TextChanged" AutoPostBack="true" />

                                                <asp:RequiredFieldValidator ControlToValidate="txtRateNet" runat="server" ID="rfvtxtRateNet" ValidationGroup="generalcharge"
                                                    ErrorMessage="Enter Rate Net" Text="Enter Rate Net" class="validationred" Display="Dynamic" ForeColor="Red" />

                                                <asp:RegularExpressionValidator ID="revtxtRateNet" ControlToValidate="txtRateNet" runat="server" ForeColor="Red"
                                                    ErrorMessage="Enter only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT(%)</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtgenvat" runat="server" class="form-control" ReadOnly="true" Style="text-align: right;" />

                                            </div>

                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    VAT Amount
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtVatAmount" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Exclusive Amount</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtExcluAmount" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Client Total
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtClientTotal" runat="server" class="form-control" placeholder="0.00" Style="text-align: right;" />
                                            </div>
                                        </div>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-3">

                                            <asp:Button runat="server" ID="GenSubmit" class="btn btn-primary"  ValidationGroup="generalcharge"
                                                Text="Submit" OnClick="btnGencharge_click" />&nbsp;
                    <asp:Button runat="server" ID="GenCancel"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="GenCancel_Click" />


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

                            <asp:Panel ID="InvGridView" runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <asp:HiddenField ID="hf_InvListId" runat="server" Value="0" />
                                        <div style="height: 130px; width: 617px; overflow: auto;">
                                            <asp:GridView ID="InvListGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
                                                AutoGenerateColumns="False" HeaderStyle-BackColor="#0088cc" HeaderStyle-CssClass="test" DataKeyNames="Type,TicketId" CssClass="table table-striped table-bordered"
                                                ShowHeaderWhenEmpty="true" OnRowCommand="InvListGrid_RowCommand">
                                                <PagerStyle HorizontalAlign="Left" CssClass="pagination1" />
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
                                                    <asp:TemplateField HeaderText="Commission Total" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <%#Eval("Comminclu")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Commission Exclusi" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <%#Eval("CommiExclu")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Commission VatAmount" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <%#Eval("CommiVatamount")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Text="View" ID="lnkView" OnClick="lnkView_Click" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:HiddenField ID="Hf_InvLineCount" runat="server" Value="0" />

                                        <asp:GridView ID="InvoiceLineItemCountGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
                                            AutoGenerateColumns="False" HeaderStyle-BackColor="#0088cc" HeaderStyle-CssClass="test" DataKeyNames="Type" CssClass="table table-striped table-bordered"
                                            ShowHeaderWhenEmpty="true">
                                            <PagerStyle HorizontalAlign="Left" CssClass="pagination1" />
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
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                    <ItemTemplate>
                                                        <%#Eval("comminclu")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                    <ItemTemplate>
                                                        <%#Eval("commexclu")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                    <ItemTemplate>
                                                        <%#Eval("vatamount")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                        &nbsp;&nbsp;
                                <div class="btncancle">
                                    <asp:Label ID="LBLInvoiceTotalAmount" runat="server" Font-Bold="true" ForeColor="#0088cc">Invoice Total:</asp:Label>
                                    &nbsp;
                                    <asp:Label ID="txtInvoiceTotalAmount" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                        <asp:Label ID="lblcommissionInclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblcommissionExclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblcommissionVatamt" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblaircommiinclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lbllandcommiInclu" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblservcommi" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lblaircommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                        <asp:Label ID="lbllandcommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>
                                        <asp:Label ID="lblsercommType" runat="server" Font-Bold="true" class="hiddencol"></asp:Label>

                                    </div>

                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </asp:Panel>
                            <br>

                            <%-- model PopUp code End --%>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Messages
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlInvMesg" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <%--<asp:ListItem Text="--Select Message Type--" Value="-1" Selected="True"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-4">
                                        <asp:TextBox TextMode="MultiLine" CssClass="form-control" ID="txtInvClntMesg" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Print Style
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlInvPdfPrintStyle" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Text="--Select Print Style--" Value="-1" Selected="True"></asp:ListItem>--%>
                                        </asp:DropDownList>



                                    </div>
                                </div>
                            </div>

                            <%-- A/C Analysis --%>

                            <%--<cc1:ModalPopupExtender ID="ACAnalysisPopupExtender" runat="server" TargetControlID="btnACAnalysis"
                        BackgroundCssClass="popupextndrBg" BehaviorID="btnACAnalysis" CancelControlID="bntCancelFP"
                        PopupControlID="pnlACAnalysis">
                    </cc1:ModalPopupExtender>--%>

                            <%--<asp:Panel ID="pnlACAnalysis" runat="server" CssClass="panelpopup" Width="800px" Height="400px" Style="display: none;" BackgroundCssClass="modalBackground">
                        <div class="panelpopupheaderbox">
                            <div style="float: right; padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton4" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                            </div>
                        </div>
                        <div id="popupACAnalysis"  class="modalBackground">


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
                               
                                <asp:GridView ID="ACAnalysisGrid" runat="server" AllowPaging="true" Width="100%" PageSize="15"
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
                                <br />
                            </div>




                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-3">

                                    <asp:Button runat="server" ID="btnSubmitACAnalsys" class="btn btn-success"
                                        Text="Submit" OnClick="btnSubmitACAnalsys_Click" />&nbsp;
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
                                    <asp:Button runat="server" ID="btnInvSave" class="btn btn-primary green" ValidationGroup="invoice"
                                        Text="Submit" OnClick="btnInvSave_Click" />&nbsp;
                              <%--<asp:Button ID="btnACAnalysis" runat="server" CssClass=" btn btn-info" ValidationGroup="analysis" Text="A/C Analysis" />--%>
                                    <asp:Button runat="server" ID="btnInvCancel"
                                        class="btn btn-primary red" ValidationGroup="" Text="Cancel" OnClick="btnInvCancel_Click" />
                                    <asp:Button runat="server" ID="Button1"
                                        class="btn btn-primary red" ValidationGroup="" Text="DraftPdf" OnClick="btnDraftPdf_Click" />
                                </div>
                                <%-- <div class="col-sm-3">
                            <asp:Button runat="server" ID="btnDraftPdf"
                                class="btn btn-primary red" ValidationGroup="" Text="DraftPdf" OnClick="btnDraftPdf_Click" />
                        </div>--%>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

