<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Generalreceipts.aspx.cs" Inherits="Admin_Generalreceipts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
          .style1 {
            color: #FF0000;
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
             $('#ContentPlaceHolder1_txtGRDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
             $("#ContentPlaceHolder1_txtGRDate").datepicker({
                format: 'yyyy-mm-dd',
                startDate: '-9d',
                endDate: '0d',
                autoclose: true
             }).attr('readonly', 'false');;
             $('#<%= ddlGRCategory.ClientID %>').select2();
             $('#<%= ddlGRFmAccCode.ClientID %>').select2();
             $('#<%= ddlGRToAccCode.ClientID %>').select2();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="GeneralReceipts" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                    </div>

                    <h2 class="panel-title">General Receipts</h2>
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
                            <asp:TextBox ID="txtGRDate" runat="server" CssClass="form-control" BackColor="White" ></asp:TextBox>
                        </div>

                        <div class="col-sm-2">
                            <label class="control-label">
                                Prepared By</label>
                        </div>

                        <div class="col-sm-2">
                       <asp:TextBox ID="txtGRPreparedBy" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-sm-2">
                            <label class="control-label">
                               Category <span class="style1">*</span></label></label>
                        </div>
                        <div class="col-sm-2">
                               <asp:DropDownList ID="ddlGRCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGRCategory_SelectedIndexChanged"  AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCategory" ErrorMessage="Select Category" Display="Dynamic" ControlToValidate="ddlGRCategory" runat="server" ValidationGroup="GeneralReceipts" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">


                        <div class="col-sm-2">
                            <label class="control-label">
                                From Account<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">

                            <asp:DropDownList ID="ddlGRFmAccCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGRFmAccCode_SelectedIndexChanged">
                            </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvaccountfrom" ErrorMessage="Select from Account" Display="Dynamic" ControlToValidate="ddlGRFmAccCode" runat="server" ValidationGroup="GeneralReceipts" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">
                            <label class="control-label">
                                To Account<span class="style1">*</span></label></label>
                        </div>
                        <div class="col-sm-2">
                        
                            	  <asp:DropDownList ID="ddlGRToAccCode"  runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGRToAccCode_SelectedIndexChanged">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvtoAccount" ErrorMessage="Select to Account" Display="Dynamic" ControlToValidate="ddlGRToAccCode" runat="server" ValidationGroup="GeneralReceipts" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                           
                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Amount <span class="style1">*</span></label></label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtGRPaymentAmount" runat="server"  CssClass="form-control" OnTextChanged="txtGRPaymentAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                         <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtGRPaymentAmount" Display="Dynamic" runat="server" ValidationGroup="GeneralReceipts" ErrorMessage="Please Enter Amount" ForeColor="Red"></asp:RequiredFieldValidator>

                    </div>

                <div class="form-group">

                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary green" ValidationGroup="GeneralReceipts"
                                Text="Save" 
                                    UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                                 OnClick="btnSave_Click" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnClear" class="btn btn-primary green" 
                                Text="Clear"  OnClick="btnClear_Click" />
                        </div>
                     <div class="col-sm-2">
                            <asp:Button runat="server" ID="btnCancel" class="btn btn-danger" 
                                Text="Cancel"  OnClick="btnCancel_Click" />
                        </div>
                      
                    </div>
                     </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

