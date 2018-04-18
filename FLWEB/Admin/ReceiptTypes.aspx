<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ReceiptTypes.aspx.cs" Inherits="Admin_ReceiptTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
     <script>

        <%-- $(document).ready(function () {
             DrpSearch();
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
                 DrpSearch();
             });

         });

         function DrpSearch() {
             $('#<%= dropDepMethod.ClientID %>').select2();
            $('#<%= dropBankAccount.ClientID %>').select2();
            $('#<%= dropCreditType.ClientID %>').select2();
        };--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_ReceiptTypeId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Receipt Type</h2>
        </header>
        <div class="panel-body">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Key(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtReceiptKey" runat="server" Cssclass="form-control" MaxLength="3" OnTextChanged="txtReceiptKey_TextChanged" AutoPostBack="true" />
                                <asp:RequiredFieldValidator ControlToValidate="txtReceiptKey" runat="server" ID="rfvtxtReceiptKey" ValidationGroup="receipt"
                                    ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:Label ID="lblKeyerr" runat="server" ></asp:Label> 

                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-3">
                                <asp:CheckBox ID="chkDeactivate" runat="server" />
                                <label class="control-label">Deactivate?</label>
                            </div>

                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Description(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDescription" runat="server" Cssclass="form-control" MaxLength="50" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"/>
                                <asp:RequiredFieldValidator ControlToValidate="txtDescription" runat="server" ID="rfvtxtDescription" ValidationGroup="receipt"
                                    ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtDescription" runat="server" ForeColor="Red"
                            ID="revtxtDescription" ValidationGroup="receipt" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Dep list Method</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropDepMethod" runat="server" Cssclass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="dropDepMethod_SelectedIndexChanged" AutoPostBack="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                               
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                           <div class="col-sm-2">
                                <label class="control-label">Bank Account(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropBankAccount" runat="server" Cssclass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dropBankAccount_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropBankAccount" runat="server" ID="rfvdropBankAccount" ValidationGroup="receipt"
                                    ErrorMessage="Select Bank Account" Text="Select Bank Account" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                            <div class="col-sm-1"></div>
                             <div class="col-sm-2">
                                <label class="control-label">C/Card Type(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCreditType" runat="server" Cssclass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="dropCreditType_SelectedIndexChanged" AutoPostBack="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCreditType" runat="server" ID="rfvdropCreditType" ValidationGroup="receipt"
                                    ErrorMessage="Select C/Card Type" Text="Select C/Card Type" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-6">                            
                                <asp:CheckBox ID="ChkDefaultReciepts" runat="server" />
                                <label class="control-label">Set as the default for New Reciepts</label>  
                                </div>                            
                        </div>
                    </div>

            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="receipt"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </section>
</asp:Content>

