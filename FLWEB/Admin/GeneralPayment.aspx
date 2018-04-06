<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="GeneralPayment.aspx.cs" Inherits="Admin_GeneralPayment" %>

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
       <%--   var count=0;
          function DisableSubmitButton() {
              count++;
              var btn = document.getElementById('<%=btnSave.ClientID%>');
              alert(count);
              //make sure submit button was found (it may be in a panel that has Visible set to false). If found, disable it
              if (btn) {
                  btn.disabled = true;
              }

          }--%>

          function DatePickerSet() {
              $('#ContentPlaceHolder1_txtDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
              $("#ContentPlaceHolder1_txtDate").datepicker({
                  format: 'yyyy-mm-dd',
                  //startDate: '-9d',
                  endDate: '0d',
                  autoclose: true
              }).attr('readonly', 'false');;
              $('#<%= ddlCategory.ClientID %>').select2();
              $('#<%= ddlFmAccCode.ClientID %>').select2();
              $('#<%= ddlToAccCode.ClientID %>').select2();

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

                    <h2 class="panel-title">New General Payment</h2>
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
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" BackColor="White" ></asp:TextBox>
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
                               Category<span class="style1">*</span> </label>
                        </div>
                        <div class="col-sm-2">
                               <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                               
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlCategory" runat="server" ID="rfvddlCategory" ValidationGroup="rct"
                                ErrorMessage="Select Category" Text="Select Category" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />

                         
                        </div>
                    </div>

                    <div class="form-group">


                        <div class="col-sm-2">
                            <label class="control-label">
                                From Account<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">

                            <asp:DropDownList ID="ddlFmAccCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFmAccCode_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlFmAccCode" runat="server" ID="rfvddlFmAccCode" ValidationGroup="rct"
                                ErrorMessage="Select From Account" Text="Select From Account" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
                 
                        </div>
                        <div class="col-sm-2">
                            <label class="control-label">
                                To Account<span class="style1">*</span></label>
                        </div>
                        <div class="col-sm-2">
                        
                            	  <asp:DropDownList ID="ddlToAccCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlToAccCode_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ControlToValidate="ddlToAccCode" runat="server" ID="rfvddlToAccCode" ValidationGroup="rct"
                                ErrorMessage="Select To Account" Text="Select To Account" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="0" />
			  
                        </div>


                        <div class="col-sm-2">

                            <label class="control-label">
                                Amount<span class="style1">*</span> </label>
                        </div>
                        <div class="col-sm-2">

                            <asp:TextBox ID="txtpytmAmount" runat="server"  CssClass="form-control"></asp:TextBox>

				             <asp:RequiredFieldValidator ControlToValidate="txtpytmAmount" runat="server" ID="rfvtxtpytmAmount" ValidationGroup="rct"
                                ErrorMessage="Enter Amount" Text="Enter Amount" class="validationred" Display="Dynamic" ForeColor="Red"/>
                 
                        </div>


                    </div>

             

                <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="btnSave" class="btn btn-success" ValidationGroup="rct"
                        Text="Submit"
                        UseSubmitBehavior="false"
                         OnClick="btnSave_Click" OnClientClick="this.disabled='true';this.value='Please Wait...' "/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                  <%--  <asp:Button runat="server" ID="btnClear"
                            class="btn btn-primary" ValidationGroup="" Text="Reset" OnClick="btnClear_Click"  />--%>

                </div>
            </div>

                     </div>

            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

