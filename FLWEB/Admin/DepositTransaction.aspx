<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="DepositTransaction.aspx.cs" Inherits="Admin_DepositTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         .Error {
            color: #FF0000;
        }


          .hiddencol {
            display: none;
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
             $('#ContentPlaceHolder1_txtDpstDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
             $("#ContentPlaceHolder1_txtDpstDate").datepicker({
                 format: 'yyyy-mm-dd',
                 startDate: '0d',
                 autoclose: true
             }).attr('readonly', 'false');;
             $('#<%= ddldpstReceiptType.ClientID %>').select2();
             $('#<%= ddlDepstClientPredfix.ClientID %>').select2();
             $('#<%= ddlDepositAcoount.ClientID %>').select2();
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

                    <h2 class="panel-title">New Deposit </h2>
                </header>
                    <div class="panel-body">
                         <div class="col-sm-12">
                        <asp:Label ID="lblMsg" class="message" ForeColor="Red" runat="server" Text=""
                            EnableViewState="false"></asp:Label>
                    </div>
                           <div class="form-group">
                        <div class="col-sm-3">
                            <label class="control-label">
                                Date<span class="Error">*</span></label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtDpstDate" runat="server" CssClass="form-control" BackColor="White" ></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDpstDate" runat="server" ID="rfvtxtDpstDate" ValidationGroup="deposit"
                                ErrorMessage="Enter Date" Text="Enter Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                        </div>

                        <div class="col-sm-3">
                            <label class="control-label">
                                Receipt Type<span class="Error">*</span></label>
                        </div>

                        <div class="col-sm-3">
                             <asp:DropDownList ID="ddldpstReceiptType" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldpstReceiptType_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddldpstReceiptType" runat="server" ID="rfvtxtDpstReciptType" ValidationGroup="deposit"
                                ErrorMessage="Select Receipt Type" Text="Select Receipt Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                        </div>

                      
                    </div>
                          <div class="form-group">
                        <div class="col-sm-3">
                            <label class="control-label">
                                Source Ref No<span class="Error">*</span></label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtDpstSourceRef" runat="server" CssClass="form-control" OnTextChanged="txtDpstSourceRef_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDpstSourceRef" runat="server" ID="rfvtxtDpstSourceRef" ValidationGroup="deposit"
                                ErrorMessage="Enter Source Ref No" Text= "Enter Source Ref No" class="validationred" Display="Dynamic" ForeColor="Red" />
                        </div>

                        <div class="col-sm-3">
                            <label class="control-label">
                                Client Prefix</label>
                        </div>

                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlDepstClientPredfix" runat="server"  CssClass="form-control" OnSelectedIndexChanged="ddlDepstClientPredfix_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                      
                    </div>
                         <div class="form-group">
                        <div class="col-sm-3">
                            <label class="control-label">
                                Consultant</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtDepstConsultant" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            
                        </div>
                             <div class="col-sm-3">
                            <label class="control-label">
                                Deposit Account<span class="Error">*</span></label>
                        </div>

                        <div class="col-sm-3">
                             <asp:DropDownList ID="ddlDepositAcoount" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositAcoount_SelectedIndexChanged"></asp:DropDownList>
                         
                                      <asp:RequiredFieldValidator ControlToValidate="ddlDepositAcoount" runat="server" ID="rfvddlDepositAcoount" ValidationGroup="deposit"
                                ErrorMessage="Select Deposit Account" Text="Select Deposit Account" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0"/>
                  
                            
                               </div>

                             </div>
                         <div class="form-group">
                        <div class="col-sm-3">
                            <label class="control-label">
                                Comments</label>
                        </div>

                        <div class="col-sm-3">
                            <asp:TextBox ID="txtDpstComments" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                      
                    </div>

                        <div class="form-group">
                            <div class="col-sm-6">
                                <asp:label ID="lblCurDepoist"  runat="server" ForeColor="Blue">On this Deposit <span id="spnCurDpstCount" runat="server"> 0  </span> Recipts,R  <span id="spnThisDepositAmnt" runat="server"> 0.00</span></asp:label>
                            </div>
                            <div class="col-sm-6">
                                 <asp:label ID="lblUnbankRecipts" runat="server"  ForeColor="Blue">Unbanked <span id="unBankCount" runat="server">0</span> Recipts,R <span id="unBankAmount" runat="server">0.00</span></asp:label>
                            </div>

                         </div>

                          <div class="form-group">
                              <div class="col-sm-6">
                                   <asp:GridView ID="gvSeocondRecipts" runat="server" AllowPaging="true" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="ReceivedTransactionId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="30" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>

                                    <asp:TemplateField>
                                <ItemTemplate>
                                <asp:CheckBox ID="chkRightSelect" runat="server" AutoPostBack="true"  OnCheckedChanged="chkRightSelect_CheckedChanged"/>
                                </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="ReceivedTransactionId" HeaderText="SN">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                               
                                   <%-- <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                            <%--<asp:HiddenField ID="hfRecieveTransId" runat="server" Value='<%#Eval("ReceivedTransactionId")%>' />--%>
                                           
                                           
                               
                                    <%--<%#Eval("ReceivedTransactionId")%>
                                
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="TransactionDate" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RecieptType" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:BoundField DataField="ClientType" HeaderText="Client Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ClientAcNo" HeaderText="Client Ac No">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AllocatedAmount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="invoiceId" HeaderText="InvoiceId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                              </div>
                        <div style="overflow: auto" class="col-sm-6">
                             <asp:GridView ID="gvReciptData" runat="server" AllowPaging="true" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="ReceivedTransactionId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="40" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>
                                     <asp:TemplateField>
                                <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                                   </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                          
                                           
                                           <%#Eval("ReceivedTransactionId")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="ReceivedTransactionId" HeaderText="SN">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TransactionDate" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RecieptType" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>


                                    <asp:BoundField DataField="ClientType" HeaderText="Client Type">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ClientAcNo" HeaderText="Client Ac No">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AllocatedAmount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                        <asp:BoundField DataField="invoiceId" HeaderText="InvoiceId" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                        <ItemStyle HorizontalAlign="Center"  />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                            </div>
                               <div style="height: 290px; overflow: auto" class="col-sm-6">
                            </div>
                              </div>

                         <div class="form-group">
                             <div class="col-sm-3">

                             </div>
                        <div class="col-sm-3">
                            <asp:Button runat="server" ID="btnDepositSave" class="btn btn-primary green" ValidationGroup="deposit"
                                Text="Save" 
                                    UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                                 OnClick="btnDepositSave_Click" />
                        </div>
                             <asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;

                        <div class="col-sm-3">
                            <asp:Button runat="server" ID="btnDpstClear" OnClick="btnDpstClear_Click" class="btn btn-primary green"  
                                Text="Clear"  />
                        </div>
                       
                    </div>

                    </div>
                </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

