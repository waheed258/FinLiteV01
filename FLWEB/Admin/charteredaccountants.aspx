<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="charteredaccountants.aspx.cs" Inherits="Finance_charteredaccountants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
          .style1 {
            color: #FF0000;
        }
         </style>
      <script>

          $(document).ready(function () {
              DrpSearch();
              var prm = Sys.WebForms.PageRequestManager.getInstance();
              prm.add_endRequest(function () {
                  DrpSearch();
              });

          });

          function DrpSearch() {
              $('#<%= ddlMasterAccount.ClientID %>').select2();
              $('#<%= ddlCategory.ClientID %>').select2();
              $('#<%= ddlCurrency.ClientID %>').select2();
          };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title"> Chartered Accounts</h2>
        </header>
        <div class="panel-body">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="col-sm-12">
                <div class="col-sm-2">
                    <label class="control-label">
                        Name<span class="style1">*</span></label>
                </div>
                <div class="col-sm-2">
                      <asp:TextBox ID="txtchartacName" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="rfvcName" runat="server" ControlToValidate="txtchartacName" ForeColor="Red" Display="Dynamic" ErrorMessage=" Enter Name" ValidationGroup="accounts"></asp:RequiredFieldValidator>
                  
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-2">
                    <label class="control-label">
                        Master Account<span class="style1">*</span>
                    </label>
                </div>
         
                <div class="col-sm-2">
                  
                    <asp:DropDownList ID="ddlMasterAccount" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMasterAccount_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Text="--Select Master Account--" Value="-1" Selected="True"></asp:ListItem>--%>
                     </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvmasteracc" runat="server" ControlToValidate="ddlMasterAccount" ForeColor="Red" Display="Dynamic" ErrorMessage=" Select master Account" ValidationGroup="accounts" InitialValue="0"></asp:RequiredFieldValidator>

                </div>
                           
                    </div>         
            </div>
            <div class="form-group">
                 <div class="col-sm-12">
                <div class="col-sm-2">
                    <label class="control-label">
                       Type<span class="style1">*</span></label>
                </div>
                <div class="col-sm-2">
                                <asp:TextBox ID="txtchartAcType" runat="server" class="form-control" />
                         
                   <asp:RequiredFieldValidator ID="rfvactype" runat="server" ControlToValidate="txtchartAcType" ForeColor="Red" Display="Dynamic" ErrorMessage=" Enter Type" ValidationGroup="accounts"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-1">

                </div>
                <div class="col-sm-2">
                    <label class="control-label">
                        Account Code<span class="style1">*</span> 
                    </label>
                </div>
              
                    <div class="col-sm-1">
               
          <asp:TextBox ID="txtCharAcCommCode" runat="server" class="form-control"  ReadOnly="true"/>
                         </div>
                       <div class="col-sm-2">
                     <asp:TextBox ID="txtCharAcountCode" runat="server" class="form-control" OnTextChanged="txtCharAcountCode_TextChanged" AutoPostBack="true" />
                   <asp:RequiredFieldValidator ID="rfvAcCode" runat="server" ControlToValidate="txtCharAcountCode" ForeColor="Red" Display="Dynamic" ErrorMessage="Enter Account Code" ValidationGroup="accounts"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblaccnoerr" runat="server" ></asp:Label>
                </div>

            </div>
                </div>
            
            <div class="form-group">
                    <div class="col-sm-12">
                      <div class="col-sm-2">
                            <label class="control-label">
                             Category<span class="style1">*</span>
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                             <asp:DropDownList ID="ddlCategory"  runat="server" CssClass="form-control">
                                 <%--<asp:ListItem Text="--Select Category--" Value="-1" Selected="True"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvcategory" runat="server" ControlToValidate="ddlCategory" ErrorMessage=" Select Category" Display="Dynamic" ForeColor="Red" ValidationGroup="accounts" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                       
                     <div class="col-sm-2">
                            <label class="control-label">
                             Currency<span class="style1">*</span>
                            </label>
                        </div>
                    <div class="col-sm-1">
               <asp:DropDownList ID="ddlDefaultCurrency" runat="server" CssClass="form-control">
                             
                            </asp:DropDownList>
         
                         </div>
                       <div class="col-sm-2">
                   
                             <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                              
                            </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ControlToValidate="ddlCurrency" ErrorMessage="Select Currency" ForeColor="Red" Display="Dynamic" InitialValue="0" ValidationGroup="accounts"></asp:RequiredFieldValidator>
                </div>
                    </div>
                </div>

              </ContentTemplate>
                </asp:UpdatePanel>

            <br />
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
          
                          <asp:Button runat="server" ID="btnChartedAccount" class="btn btn-success" 
                        Text="Submit"
                                  UseSubmitBehavior="false" 
                                  OnClientClick="this.disabled='true';this.value='Please Wait...' "
                               OnClick="btnChartedAccount_Click"  ValidationGroup="accounts"/>&nbsp;
                    <asp:Button runat="server" ID="btnChartedAccountCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnChartedAccountCancel_Click" />

                                  
                </div>
            </div>

           
        </div>

         
    </section>
</asp:Content>

