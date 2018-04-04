<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="LatestPaymentTransaction.aspx.cs" Inherits="Admin_LatestPaymentTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .pnlBackGround {
            position: fixed;
            top: 10%;
            left: 377px;
            width: 300px;
            height: 125px;
            text-align: center;
            background-color: White;
            border: solid 3px black;
        }


        .modalBackground {
            background-color: silver;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: White;
            border: 1px solid #4C3C1B;
            padding: 5px;
            left: 424px !important;
            width: 865px;
            top: 204.5px !important;
        }

        .DialogHeader {
            background-color: #7098ad;
            color: White;
            height: 30px;
            font-size: medium;
            font-style: normal;
            font-weight: bold;
            vertical-align: middle;
            display: table-cell;
        }

        .DialogHeaderFrame {
            width: 100%;
            cursor: default;
            background-color: white;
            display: table;
        }

        .Grid td {
            background-color: #bbe3f9;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            padding: 0px 0px 0px 5px;
        }

        .Grid th {
            background-color: #7098ad;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding: 0px 0px 0px 5px;
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 131%;
            padding: 0px 53px 3px 12px;
        }

        .Nested_ChildGrid td {
            background-color: #fff !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Nested_ChildGrid th {
            background-color: #2B579A !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .floatRight {
            float: right;
        }

        .leftColumn {
            width: 59%;
            vertical-align: top;
            float: left;
            /*border: solid 1px Black;*/
        }

        .rightColumn {
            width: 102%;
            vertical-align: top;
            float: right;
            display: inline-block;
            padding: 0px 0px 0px 5px;
            /*border: solid 1px Black;*/
        }


        #loadingDiv {
            position: absolute;
            top: 0px;
            right: 0px;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.87);
            background-image: url('ajax-loader.gif');
            background-repeat: no-repeat;
            background-position: center;
            z-index: 10000000;
            opacity: 1.4;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }

        .centerHeaderText {
            text-align: right;
        }

        .hiddencol {
            display: none;
        }

        .loading {
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -60px 0 0 -60px;
            background: #fff;
            width: 150px;
            height: 150px;
            border-radius: 100%;
            border: 10px solid #19bee1;
        }

        .style1 {
            color: #FF0000;
        }

        .loading:after {
            content: '';
            background: trasparent;
            width: 140%;
            height: 140%;
            position: absolute;
            border-radius: 100%;
            top: -20%;
            left: -20%;
            opacity: 0.7;
            box-shadow: rgba(255, 255, 255, 0.6) -4px -5px 3px -3px;
            animation: rotate 2s infinite linear;
        }

        @keyframes rotate {
            0% {
                transform: rotateZ(0deg);
            }

            100% {
                transform: rotateZ(360deg);
            }
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

        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });


        //ddlAccType

        //  $(function () {
        function Confirm() {
            debugger;
            $('#<%=ddlAccountNo.ClientID %>').multiselect({
                //includeSelectAllOption: true,
                //maxHeight: 200,
                //onDropdownHidden: function (event) {
                //    var selected = [];
                //    $('[id*=ddlAccountNo] option:selected').each(function () {
                //        selected.push($(this).val());
                //    });

                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: 200

                // var data = '{stateIds: "' + selected.join(',') + '"}';
                //  PopulateDropDownList(selected, $(targetDropdown), pageUrl, 'PopulateCities', data);
                //  }
            })

        };

        function DatePickerSet() {


            $('#ContentPlaceHolder1_txtDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtDate").datepicker({
                format: 'yyyy-mm-dd',
                startDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;
            $('#<%= ddlDivision.ClientID %>').select2();
        $('#<%= ddlPaymentType.ClientID %>').select2();
            $('#<%= ddlAccType.ClientID %>').select2();
            $('#<%= ddlAccountNo.ClientID %>').select2();
            $('#<%= ddlAutoDepositeAccount.ClientID %>').select2();

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                    </div>

                    <h2 class="panel-title">New Payment</h2>
                </header>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <asp:Label ID="lblMsg" class="message" ForeColor="Red" runat="server" Text=""
                            EnableViewState="false"></asp:Label>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-2">
                            <label class="control-label">
                                Date</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"  BackColor="White" ></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDate" runat="server" ID="rfvtxtDate" ValidationGroup="rct"
                                ErrorMessage="Enter Date" Text="SelectEnter Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                        </div>

                        <div class="col-sm-2">
                            <label class="control-label">
                                SourceRef<span class="style1">*</span></label>
                        </div>

                        <div class="col-sm-2">
                            <asp:TextBox ID="txtSourceRef" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtSourceRef" runat="server" ID="rfvtxtSourceRef" ValidationGroup="rct"
                                ErrorMessage="Enter SourceRef" Text="Enter SourceRef" class="validationred" Display="Dynamic" ForeColor="Red" />
                        </div>
                         <div class="col-sm-2">
                            <label class="control-label">
                                Prepared By</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtPreparedBy" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        </div>

                        
                    </div>

                    <div class="form-group">


                       
                        <div class="col-sm-2">
                            <label class="control-label">
                                Division<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlDivision" runat="server" ID="rfvddlDivision" ValidationGroup="rct"
                                ErrorMessage="Select Devission" Text="Select Devission" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Payment Type<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlPaymentType" runat="server" ID="rfvddlPaymentType" ValidationGroup="rct"
                                ErrorMessage="Select Receipt Type" Text="Select Receipt Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>
                          <div class="col-sm-2">

                            <label class="control-label">
                                From Account No<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlAutoDepositeAccount" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlAutoDepositeAccount" runat="server" ID="rfvddlAutoDepositeAccount" ValidationGroup="rct"
                                ErrorMessage="Select Auto Deposite" Text="Select Auto Deposite" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>

                    </div>
                    <div class="form-group">


                        <div class="col-sm-2">

                            <label class="control-label">
                                Account Type<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlAccType" runat="server" ID="rfvddlAccType" ValidationGroup="rct"
                                ErrorMessage="Account Type" Text="Account Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Account No</label>
                        </div>
                        <div class="col-sm-2">
                            <%-- <asp:DropDownList ID="ddlAccountNo" runat="server"  multiple="multiple" CssClass="multiselect" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlAccountNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblMainAcc" runat="server" CssClass="form-control" Visible="false"></asp:Label>


                            <%--<asp:RequiredFieldValidator ControlToValidate="ddlAccountNo" runat="server" ID="rfvddlAccountNo" ValidationGroup="rct"
                                ErrorMessage="Select Account No" Text="Select Account No" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />--%>
                        </div>

                      <div class="col-sm-2">
                            <label class="control-label">
                                Amount<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" ValidationGroup="gvvalida"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtAmount" runat="server" ID="rfvtxtAmount" ValidationGroup="rct"
                                ErrorMessage="Enter Amount" Text="Enter Amount" class="validationred" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ControlToValidate="txtAmount" runat="server" ID="rextxtAmount" ValidationGroup="gvvalida"
                                ErrorMessage="Enter  number only." Text="Enter  number only."
                                ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>

                    </div>
                    <div class="form-group">


                        <div class="col-sm-2">
                            <label class="control-label">
                                Ageing</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtAgeing" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-sm-2">

                            <label class="control-label">
                                Payee Details</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtPayeeDetails" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-sm-2">
                            <label class="control-label">
                                details</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" Text="Payment- thank you" ReadOnly="true"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-group">

                        <div class="col-sm-2">
                            <label class="control-label">
                                Payment Reference</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtPaymentRefNo" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>

                    <div class="form-group">


                        <div class="col-sm-2">
                            <asp:Label ID="lblAllocated" class="control-label" runat="server" Text="Allocated(0)" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lblAllocatedAmount" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>

                        </div>

                        <div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Open</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lblReceiptOpenAmount" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>

                        </div>


                    </div>

                    <div class="form-group">
                        <div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Total Available</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lblTotalAvailable" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>

                        </div>

                        <%--<div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Allocate</label>
                        </div>--%>

                      <%--  <div class="col-sm-2">
                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"  Visible="false"/>

                        </div>--%>

                    </div>

                    <div class="form-group">


                        <div class="col-sm-8">
                            <asp:Label ID="lblOpenItemsFromclient" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-sm-4">

                            <asp:Label ID="lblOpenItemAmounFromclient" class="control-label" runat="server" ForeColor="Blue"></asp:Label>
                        </div>



                    </div>



                    <div class="form-group">
                        <div style="height: 290px; overflow: auto">


                            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                DataKeyNames="SupplierId" Width="100%">
                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="213px">
                                        <ItemTemplate>

                             
                                            <asp:LinkButton ID="lnkInvoiceClosed" runat="server" CommandArgument='<%# Eval("InvoiceId")%>'
                                                OnClick="ViewInvoiceClosed" Text="Closed History" />
                                            |
                                                       <asp:LinkButton ID="lnkinvoiceOpen" runat="server" CommandArgument='<%# Eval("InvoiceId")%>'
                                                           OnClick="ViewInvocieOpen" Text="Open History" />


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:HiddenField ID="hfInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                            <asp:HiddenField ID="hfTicketId" runat="server" Value='<%#Eval("TicketId")%>' />
                                            <asp:HiddenField ID="hfAllocatedAmount" runat="server" Value="0" />

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField ItemStyle-Width="150px" DataField="SupplierName" HeaderText="Supp.Name" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="supplamount" HeaderText="Supp.Amount" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField ItemStyle-Width="190px" DataField="SupplOpeningAmount" HeaderText="Supp.OutStanding Amount" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="SupplPaiedAmount" HeaderText="Supp.PaidAmount" ItemStyle-HorizontalAlign="Right" />


                                    <asp:TemplateField HeaderText="ThisEntry" HeaderStyle-CssClass="panel-heading" ItemStyle-HorizontalAlign="left" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtThisEntry" runat="server" CssClass="form-control"  AutoPostBack="true"
                                                ValidationGroup="gvvalida" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="txtThisEntry" runat="server" ID="rextxtThisEntry" ValidationGroup="gvvalida"
                                                ErrorMessage="Enter  number only." Text="Enter  number only."
                                                ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField ItemStyle-Width="150px" DataField="SupplOpeningAmount" HeaderText="Balance" ItemStyle-HorizontalAlign="Right" />



                                </Columns>

                            </asp:GridView>




                        </div>
                    </div>

                    <asp:Label runat="server" ID="lblSuppOpenAmt" Visible="false"></asp:Label>





                    <cc1:ModalPopupExtender ID="InvoiceclosePopExtender" runat="server" TargetControlID="btnInvcloseshow" CancelControlID="BtnCloseInvocie"
                        Drag="false" PopupControlID="PanelInvclose" Enabled="True" BackgroundCssClass="modalBackground" />
                    <asp:Button ID="btnInvcloseshow" Style="display: none" runat="server" Width="120" Text="Filter Charges" ToolTip="show Chargefilter-Dialog" />

                    <asp:Panel ID="PanelInvclose" CssClass="modalPopup" runat="server">
                        <asp:Panel ID="DialogHeaderFrame" CssClass="DialogHeaderFrame" runat="server">
                            <asp:Panel ID="DialogHeader" runat="server" CssClass="DialogHeader">
                                &nbsp;<asp:Label ID="lblclosedInvPopupHeader" runat="server" Text="Inv.Closed History" />
                            </asp:Panel>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" >
                            <ContentTemplate>
                                <asp:Panel runat="server" Height="250px" ScrollBars="Vertical">
                                    <asp:GridView ID="gvClosedInvoice" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="200px" DataField="InvDocumentNo" HeaderText="Invoice No." />

                                            <asp:BoundField ItemStyle-Width="200px" DataField="supplierName" HeaderText="Supp.Name" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="supplamount" HeaderText="Supp.Amount" ItemStyle-HorizontalAlign="Right" />
                                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="SupplOpeningAmount" HeaderText="Supp.Outstanding Amount" ItemStyle-HorizontalAlign="Right" />--%>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="SupplPaiedAmount" HeaderText="Supp.PaidAmount " ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Status" HeaderText="Invoice Status" />

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <div style="padding-left: 784px; padding-top: 4px;">
                                    <asp:Button ClientIDMode="Static" ID="BtnCloseInvocie" Text="Close" ToolTip="close filter-dialog"                               
                                        class="btn btn-danger" CausesValidation="false" Width="60px" runat="server" /><br />
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>


                    <cc1:ModalPopupExtender ID="InvocieOpenPopExtender" runat="server" TargetControlID="btnInvoiceopenshow" CancelControlID="btnOpenInvocie"
                        Drag="false" PopupControlID="PanelInvopen" Enabled="True" BackgroundCssClass="modalBackground" />
                    <asp:Button ID="btnInvoiceopenshow" Style="display: none" runat="server" Width="120" Text="Filter Charges" ToolTip="show Chargefilter-Dialog" />

                    <asp:Panel ID="PanelInvopen" CssClass="modalPopup" runat="server">
                        <asp:Panel ID="Panel2" CssClass="DialogHeaderFrame" runat="server">
                            <asp:Panel ID="Panel3" runat="server" CssClass="DialogHeader">
                                &nbsp;<asp:Label ID="lblInvocieOpen" runat="server" Text="Inv.Open History" />
                            </asp:Panel>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Panel runat="server" Height="250px" ScrollBars="Vertical">

                                <asp:GridView ID="gvInvoiceOpen" runat="server" AutoGenerateColumns="false" Height="10%">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="200px" DataField="InvDocumentNo" HeaderText="Invoice No." />

                                        <asp:BoundField ItemStyle-Width="200px" DataField="supplierName" HeaderText="Supp.Name" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="supplamount" HeaderText="Supp.Amount" ItemStyle-HorizontalAlign="Right" />
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="SupplOpeningAmount" HeaderText="Supp.Outstanding Amount" ItemStyle-HorizontalAlign="Right" />--%>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SupplPaiedAmount" HeaderText="Supp.PaidAmount " ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Status" HeaderText="Invoice Status" />

                                    </Columns>
                                </asp:GridView>

                                    </asp:Panel>

                                <div style="padding-left: 784px; padding-top: 4px;">
                                    <asp:Button ClientIDMode="Static" ID="btnOpenInvocie" Text="Close" ToolTip="close filter-dialog" CausesValidation="false" 
                                        Width="60px" runat="server"  class="btn btn-danger" /><br />
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>



                    <div class="form-group">

                        <div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Message</label>
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control multipleLine" MaxLength="200"></asp:TextBox>
                        </div>

                    </div>

                    <div class="form-group">

                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary green" ValidationGroup="rct"
                                Text="Save" OnClick="btnSave_Click" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnClear" class="btn btn-primary green" ValidationGroup="rct"
                                Text="Clear" OnClick="btnClear_Click" />
                        </div>
                       <%-- <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnPrint" class="btn btn-primary green" ValidationGroup="rct"
                                Text="Print" OnClick="btnPrint_Click" />
                        </div>--%>
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btncancel" class="btn btn-danger"  
                                Text="Cancel" OnClick="btncancel_Click" />
                        </div>

                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
