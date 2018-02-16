<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="IncomeReceipt.aspx.cs" Inherits="IncomeReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                  //startDate: '-9d',
                  endDate: '0d',
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

                    <h2 class="panel-title">New Income Receipt</h2>
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
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDate" runat="server" ID="rfvtxtDate" ValidationGroup="rct"
                                ErrorMessage="Enter Date" Text="SelectEnter Date" class="validationred" Display="Dynamic" ForeColor="Red" />
                        </div>

                        <div class="col-sm-2">
                            <label class="control-label">
                                Prepared By</label>
                        </div>

                        <div class="col-sm-2">
                       <asp:TextBox ID="txtPreparedBy" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        </div>

                        <div class="col-sm-2">

                            <label class="control-label">
                                Source Reference </label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtSourceRef" runat="server"  CssClass="form-control"></asp:TextBox>

				             <asp:RequiredFieldValidator ControlToValidate="txtSourceRef" runat="server" ID="rfvtxtSourceRef" ValidationGroup="rct"
                                ErrorMessage="Enter Source Reference" Text="Enter Source Reference" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                 
                        </div>

                        <div class="col-sm-2">
                            <label class="control-label">
                               Category </label>
                        </div>
                        <div class="col-sm-2">
                               <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                               
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlCategory" runat="server" ID="rfvddlCategory" ValidationGroup="rct"
                                ErrorMessage="Select Receipt Type" Text="Select Receipt Type" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                         
                        </div>
                    </div>

                    <div class="form-group">


                        <div class="col-sm-2">
                            <label class="control-label">
                                From Account</label>
                        </div>
                        <div class="col-sm-2">

                            <asp:DropDownList ID="ddlFmAccCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFmAccCode_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlFmAccCode" runat="server" ID="rfvddlFmAccCode" ValidationGroup="rct"
                                ErrorMessage="Select Account No" Text="Select Account No" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                 
                        </div>
                        <div class="col-sm-2">
                            <label class="control-label">
                                To Account</label>
                        </div>
                        <div class="col-sm-2">
                        
                            	  <asp:DropDownList ID="ddlToAccCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlToAccCode_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ControlToValidate="ddlToAccCode" runat="server" ID="rfvddlToAccCode" ValidationGroup="rct"
                                ErrorMessage="Select Account No" Text="Select Account No" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
			  
                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Amount </label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtpytmAmount" runat="server"  CssClass="form-control"></asp:TextBox>

				             <asp:RequiredFieldValidator ControlToValidate="txtpytmAmount" runat="server" ID="rfvtxtpytmAmount" ValidationGroup="rct"
                                ErrorMessage="Enter Amount" Text="Enter Amount" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                 
                        </div>



                    </div>

             

                <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="btnSave" class="btn btn-success" ValidationGroup="rct"
                        Text="Submit" OnClick="btnSave_Click"/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;
                  <%--  <asp:Button runat="server" ID="btnClear"
                            class="btn btn-primary" ValidationGroup="" Text="Reset" OnClick="btnClear_Click"  />--%>

                </div>
            </div>

                     </div>

            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

