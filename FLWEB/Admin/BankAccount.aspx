<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="BankAccount.aspx.cs" Inherits="Admin_BankAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="labelError" runat="server"></asp:Label>
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
            $('#<%= ddlAccountType.ClientID %>').select2();
            $('#<%= ddlGraphic.ClientID %>').select2();
            $('#<%= ddlOwnerBranch.ClientID %>').select2();
            $('#<%= ddlStatementFormat.ClientID %>').select2();
        };
    </script>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Bank Account</h2>
        </header>
        <asp:HiddenField ID="hf_BankAcId" runat="server" Value="0" />
        <div class="panel-body">
            <div class="form-group">
                <div class="col-sm-12">
                  
                    <div class="col-sm-2">
                        <label class="control-label">Account Code</label>
                    </div>
                    <div class="col-sm-1">
                         <asp:TextBox ID="txtAccountCode" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="col-sm-3"></div>
                      <div class="col-sm-1">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                    
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="3" OnTextChanged="txtKey_TextChanged"  AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Key" Text="Enter Key" Display="Dynamic" ForeColor="Red" />
                         <asp:Label ID="lblKeyerr" runat="server" ></asp:Label>
                    </div>
                  
                    <div class="col-sm-2">
                        <asp:CheckBox ID="chkDeactivate" runat="server" />
                        <label class="control-label">Deactivate?</label>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Bank Name(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" OnTextChanged="txtBankName_TextChanged" AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtBankName" runat="server" ID="rfvtxtBankName" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Bank Name" Text="Enter Bank Name" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Account Type(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="-Please Select-" Value="0" />
                            <asp:ListItem Text="Savings" Value="Savings" />
                            <asp:ListItem Text="Current" Value="Current" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlAccountType" runat="server" ID="rfvddlAccountType" ValidationGroup="BankAcc"
                            ErrorMessage="Select Account Type" Text="Select  Account Type" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Account Number(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" OnTextChanged="txtAccountNumber_TextChanged" AutoPostBack="true"/>
                       <asp:Label ID="lblaccNumber" runat="server" ></asp:Label>
                        
                          <asp:RequiredFieldValidator ControlToValidate="txtAccountNumber" runat="server" ID="rfvtxtAccountNumber" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Account Number" Text="Enter Account Number" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtAccountNumber" runat="server" ID="revtxtAccountNumber" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Valid Account Number" Text="Enter Valid Account Number" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]*$" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Branch Code</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control" />
                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtBranchCode" runat="server" ID="rfvtxtBranchCode" ValidationGroup="BankAcc"
                        ErrorMessage="Enter Branch Code" Text="Enter Branch Code" Display="Dynamic" ForeColor="Red" />--%>
                        <asp:RegularExpressionValidator ControlToValidate="txtBranchCode" runat="server" ID="revtxtBranchCode" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Valid Branch Code" Text="Enter Valid Branch Code" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]*$" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Branch Name(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" OnTextChanged="txtBranchName_TextChanged" AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtBranchName" runat="server" ID="rfvtxtBranchName" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Branch Name" Text="Enter Branch Name" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Account Holder(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtAccountHolder" runat="server" CssClass="form-control"  OnTextChanged="txtAccountHolder_TextChanged" AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtAccountHolder" runat="server" ID="rfvtxtAccountHolder" ValidationGroup="BankAcc"
                            ErrorMessage="Enter Account Holder" Text="Enter Account Holder" Display="Dynamic" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label>Graphic</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlGraphic" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlGraphic_SelectedIndexChanged" AutoPostBack="true"/>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label>Owner Branch</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlOwnerBranch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOwnerBranch_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="-Please Select-" Value="-1" />
                            <asp:ListItem Text="Serendipity Tours Cc" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">QuickGI Code</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtQuickGICode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">QuickGI Deposits Batch</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtQuickGIDepositsBatch" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">QuickGI Payments Batch</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtQuickGIPaymentsBatch" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Internet Banking Web Link</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtInternetBankingWebLink" runat="server" CssClass="form-control multipleLine" TextMode="MultiLine" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label>Statement Format</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlStatementFormat" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-Please Select-" Value="-1" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4">
                    <asp:Button runat="server" ID="btnSubmit" class="btn btn-success" ValidationGroup="BankAcc"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                              OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="btnSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button runat="server" ID="btnReset"
                                class="btn btn-primary green" Text="Reset" OnClick="btnReset_Click" />

                </div>
            </div>
        </div>
    </section>
</asp:Content>

