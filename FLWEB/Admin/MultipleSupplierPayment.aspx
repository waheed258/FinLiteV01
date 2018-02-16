<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MultipleSupplierPayment.aspx.cs" Inherits="Admin_MultipleSupplierPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
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
        .decimalRight {
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


       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/multiple-select/1.2.0/multiple-select.js"></script>

<script type="text/javascript">
         $(document).ready(function () {
             DatePickerSet();
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {

                 DatePickerSet();

             });

         });
       
    //ddlAccType

    //  $(function () {
         function Confirm()
         {
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
         }
         </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
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
                                Prepared By</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtPreparedBy" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        </div>
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


                    </div>
                    <div class="form-group">


                        <div class="col-sm-2">

                            <label class="control-label">
                               Account Type<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList  ID="ddlAccType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlAccType" runat="server" ID="rfvddlAccType" ValidationGroup="rct"
                                ErrorMessage="Account Type" Text="Account Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Account No</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlAccountNo" runat="server"  multiple="multiple" CssClass="multiselect" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>

                               <asp:Label ID="lblMainAcc" runat="server" CssClass="form-control" Visible="false"></asp:Label>


                            <%--<asp:RequiredFieldValidator ControlToValidate="ddlAccountNo" runat="server" ID="rfvddlAccountNo" ValidationGroup="rct"
                                ErrorMessage="Select Account No" Text="Select Account No" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />--%>
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
                        <div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Previous Open</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lblPrvClientOpenAmount" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>

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

                         <div class="col-sm-2">
                            <label class="control-label" style="font-weight: bold;">
                                Allocate</label>
                        </div>
                        <div class="col-sm-2">
                             <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                      
                        </div>

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


                            <asp:GridView ID="gvData" runat="server" AllowPaging="true" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="TicketId, invid"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="100" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfInvId" runat="server" Value='<%#Eval("invid")%>' />
                                            <asp:HiddenField ID="hfTicketId" runat="server" Value='<%#Eval("TicketId")%>' />
                                            <asp:HiddenField ID="hfAllocatedAmount" runat="server" Value="0" />
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreatedOn" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                  <%--  <asp:BoundField DataField="invid" HeaderText="invid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>--%>


                                    <asp:BoundField DataField="supplamount" HeaderText="Invoice Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="SupplPaiedAmount" HeaderText="Invoice Paid Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="SupplOpeningAmount" HeaderText="Open Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:TemplateField HeaderText="This Entry" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtThisEntry" runat="server" CssClass="form-control decimalRight" OnTextChanged="txtThisEntry_TextChanged" AutoPostBack="true" ValidationGroup="gvvalida"></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="txtThisEntry" runat="server" ID="rextxtThisEntry" ValidationGroup="gvvalida"
                                                ErrorMessage="Enter  number only." Text="Enter  number only."
                                                ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="SupplOpeningAmount" HeaderText="Balance">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

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
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnPrint" class="btn btn-primary green" ValidationGroup="rct"
                                Text="Print" OnClick="btnPrint_Click" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btncancel" class="btn btn-danger" ValidationGroup=""
                                Text="Cancel"  OnClick="btncancel_Click"/>
                        </div>
                        
                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


