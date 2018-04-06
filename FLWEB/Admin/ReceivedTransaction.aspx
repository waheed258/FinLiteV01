<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ReceivedTransaction.aspx.cs" Inherits="Admin_ReceivedTransaction" %>

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
            #ContentPlaceHolder1_txtMessage
            {
                 max-height: 40px !important;
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
         function DatePickerSet() {
             $('#ContentPlaceHolder1_txtDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
             $("#ContentPlaceHolder1_txtDate").datepicker({
                 format: 'yyyy-mm-dd',
                 startDate: '0d',
                 autoclose: true
             }).attr('readonly', 'false');;
             $('#<%= ddlDivision.ClientID %>').select2();
             $('#<%= ddlReceiptType.ClientID %>').select2();
             $('#<%= ddlClientType.ClientID %>').select2();
             $('#<%= ddlAccountNo.ClientID %>').select2();
             $('#<%= ddlAutoDepositeAccount.ClientID %>').select2();

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

                    <h2 class="panel-title">New Receipt</h2>
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
                                SourceRef<span class="style1">*</span>
                            </label>
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
                            <asp:RequiredFieldValidator ControlToValidate="ddlDivision" runat="server" ID="rfvddlDevission" ValidationGroup="rct"
                                ErrorMessage="Select Division" Text="Select Division" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Receipt Type<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control">
                               
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlReceiptType" runat="server" ID="rfvddlReceiptType" ValidationGroup="rct"
                                ErrorMessage="Select Receipt Type" Text="Select Receipt Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>
                           <div class="col-sm-2">

                            <label class="control-label">
                                Auto Deposite to</label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlAutoDepositeAccount" runat="server" CssClass="form-control">
                                
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ControlToValidate="ddlAutoDepositeAccount" runat="server" ID="rfvddlAutoDepositeAccount" ValidationGroup="rct"
                                ErrorMessage="Select Auto Deposite" Text="Select Auto Deposite" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />--%>

                        </div>


                    </div>
                    <div class="form-group">


                        <div class="col-sm-2">

                            <label class="control-label">
                                Client Type<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlClientType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClientType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlClientType" runat="server" ID="rfvddlClientType" ValidationGroup="rct"
                                ErrorMessage="Select Client Type" Text="Select Client Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Account No<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlAccountNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                           <asp:ListItem Value="0" Text="Select">Select Account</asp:ListItem>
                                 </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlAccountNo" runat="server" ID="rfvddlAccountNo" ValidationGroup="rct"
                                ErrorMessage="Select Account No" Text="Select Account No" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                        </div>
                        <div class="col-sm-2">
                            <label class="control-label">
                                Amount<span class="style1">*</span>

                            </label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control decimalRight" PlaceHolder="0.00" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" ValidationGroup="gvvalida"></asp:TextBox>
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
                            <asp:CheckBox ID="ChkAllocate" runat="server" AutoPostBack="true"  OnCheckedChanged="ChkAllocate_CheckedChanged"/>

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
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="InvId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="100" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>

                             <%-- <asp:TemplateField>
                              <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"  OnCheckedChanged="chkSelect_CheckedChanged"/>
                                </ItemTemplate>-
                                   </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfInvId" runat="server" Value='<%#Eval("InvId")%>' />
                                            <asp:HiddenField ID="hfAllocatedAmount" runat="server" Value="0" />
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreatedON" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InvOrder" HeaderText="Reference">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:BoundField DataField="InvoiceTotal" HeaderText="Invoice Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="InvoicePaiedAmount" HeaderText="Invoice Paid Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="InvoiceOpenAmount" HeaderText="Open Amount">
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:TemplateField  HeaderText="This Entry" HeaderStyle-CssClass="panel-heading" ItemStyle-Width="100px" ItemStyle-CssClass="gradeC" >
                                        <ItemTemplate>
                                            <asp:TextBox style="width:100px" ID="txtThisEntry" runat="server" CssClass="form-control decimalRight" OnTextChanged="txtThisEntry_TextChanged" Text="" AutoPostBack="true" ValidationGroup="gvvalida"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="rct" ID="rfvThisEntry" ControlToValidate="txtThisEntry" ErrorMessage="Please enter amount" ForeColor="Red" Display="Dynamic" CssClass="validationred"></asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ControlToValidate="txtThisEntry" runat="server" ID="rextxtThisEntry" ValidationGroup="rct"
                                                ErrorMessage="Enter  number only." Text="Enter  number only." ForeColor="Red"
                                                ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                       
                                            <%--<asp:CompareValidator ID="cmpTxtENtry" runat="server" ValueToCompare="0" ControlToValidate="txtThisEntry" ValidationGroup="rct" ForeColor="Red"
                                            ErrorMessage ="Must enter amount" Operator="GreaterThan" Type="Double"></asp:CompareValidator>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  

                                    <asp:BoundField DataField="InvoiceOpenAmount" HeaderText="Balance">
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
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="200"></asp:TextBox>
                        </div>

                    </div>

                    <div class="form-group">

                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary green" ValidationGroup="rct"
                                Text="Save" OnClick="btnSave_Click"  />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnClear" class="btn btn-primary green"  
                                Text="Clear" OnClick="btnClear_Click" />
                        </div>
                       <%-- <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnPrint" class="btn btn-primary green"  
                                Text="Print" OnClick="btnPrint_Click" />
                        </div>--%>
                        <div class="col-sm-2">
                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger"  OnClick="btnCancel_Click" />
                        </div>
                          
                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server"
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
        <ProgressTemplate>
            <div id="loadingDiv">
                <div style="margin: 14% 40%;">
                    <div class="ui yellow huge icon header" id="dimmmer">

                        <div class="loading" style="padding: 51px 20px; font-weight: bold; color: rgb(40, 56, 145)">
                            Please Wait..
                        </div>
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>

