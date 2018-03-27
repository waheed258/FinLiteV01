<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AirSuppliers.aspx.cs" Inherits="Admin_AirSuppliers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            $('#<%= dropStatus.ClientID %>').select2();
            $('#<%= dropServiceType.ClientID %>').select2();
            $('#<%= dropCountry.ClientID %>').select2();
            $('#<%= dropGroup.ClientID %>').select2();
            $('#<%= dropState.ClientID %>').select2();
            $('#<%= dropDivision.ClientID %>').select2();
            $('#<%= dropCity.ClientID %>').select2();
            $('#<%= dropConsultant.ClientID %>').select2();
            $('#<%= dropBank.ClientID %>').select2();
            $('#<%= dropAccountType.ClientID %>').select2();
            $('#<%= dropCommiMethod.ClientID %>').select2();
            $('#<%= dropPaymentMethod.ClientID %>').select2();
            $('#<%= dropTreatInvType.ClientID %>').select2();
            $('#<%= dropAllocItemType.ClientID %>').select2();
            $('#<%= dropNoteType.ClientID %>').select2();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_SupplierId" runat="server" Value="0" />
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
           

             <asp:Label CssClass="panel-title" runat="server" Text="New Air Supplier"></asp:Label>
        </header>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <%--<label class="control-label" runat="server"></label>--%>
                                <asp:Label CssClass="control-label" runat="server" Text="Account Code"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                                <asp:TextBox ID="txtAccountCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                <%--<asp:DropDownList ID="drpMainAccounts" runat="server"></asp:DropDownList>--%>
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label" runat="server">Supplier Name (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ControlToValidate="txtSupplierName" runat="server" ID="rfvtxtSupplierName" ValidationGroup="airsupplier"
                                    ErrorMessage="Enter Supplier Name" Text="Enter Supplier Name" class="validationred" Display="Dynamic" ForeColor="Red" />
                                <%--<asp:RegularExpressionValidator ControlToValidate="txtSupplierName" runat="server" ForeColor="Red"
                                    ID="revtxtSupplierName" ValidationGroup="airsupplier" ErrorMessage="Enter Only Characters."
                                    Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">Status (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="dropStatus" runat="server" CssClass="form-control select populate" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropStatus" runat="server" ID="rfvdropStatus" ValidationGroup="airsupplier"
                                    ErrorMessage="Select Status" Text="Select Status" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h4><b>GENERAL </b></h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Service Type (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropServiceType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropServiceType" runat="server" ID="rfvdropServiceType" ValidationGroup="airsupplier"
                                    ErrorMessage="Select Service Type" Text="Select Service Type" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Country (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="dropCountry_SelectedIndexChanged" AutoPostBack="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCountry" runat="server" ID="rfvdropCountry" ValidationGroup="airsupplier"
                                    ErrorMessage="Select Country" Text="Select Country" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Group</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropGroup" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="--Please Select--" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">State (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropState" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="dropState_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropState" runat="server" ID="rfvdropState" ValidationGroup="airsupplier"
                                    ErrorMessage="Select State" Text="Select State" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Division</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropDivision" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">City (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCity" runat="server" ID="rfvdropCity" ValidationGroup="airsupplier"
                                    ErrorMessage="Select City" Text="Select City" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Consultant</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropConsultant" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5><b>Contact Details </b></h5>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Telephone (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="form-control" MaxLength="15" />
                                <asp:RequiredFieldValidator ControlToValidate="txtTelephoneNo" runat="server" ID="rfvtxtTelephoneNo" ValidationGroup="airsupplier"
                                    ErrorMessage="Enter Telephone No" Text="Enter Telephone No" class="validationred" Display="Dynamic" ForeColor="Red" />
                                <%--<asp:RegularExpressionValidator ControlToValidate="txtTelephoneNo" runat="server" ForeColor="Red"
                                    ID="revtxtTelephoneNo" ValidationGroup="airsupplier" ErrorMessage="Enter Valid Telephone No."
                                    Text="Enter Valid Telephone No." ValidationExpression="^[0-9]{10,15}$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Fax</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtFax" runat="server" CssClass="form-control" MaxLength="15" />
                                <%--<asp:RegularExpressionValidator ControlToValidate="txtFax" runat="server" ForeColor="Red"
                                    ID="revtxtFax" ValidationGroup="airsupplier" ErrorMessage="Enter Valid Fax No."
                                    Text="Enter Valid Fax No." ValidationExpression="^[0-9]{10,15}$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Cell</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCellNo" runat="server" CssClass="form-control" MaxLength="15" />
                                <asp:RegularExpressionValidator ControlToValidate="txtCellNo" runat="server" ForeColor="Red"
                                    ID="revtxtCellNo" ValidationGroup="airsupplier" ErrorMessage="Enter Valid Cell No."
                                    Text="Enter Valid Cell No." ValidationExpression="^[0-9]{10,15}$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Contact</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="15" />
                                <asp:RegularExpressionValidator ControlToValidate="txtContact" runat="server" ForeColor="Red"
                                    ID="revtxtContact" ValidationGroup="airsupplier" ErrorMessage="Enter Valid Contact No."
                                    Text="Enter Valid Contact No." ValidationExpression="^[0-9]{10,15}$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Email (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="75" />
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ID="rfvtxtEmail" ValidationGroup="airsupplier"
                                    ErrorMessage="Enter Email" Text="Enter Email" class="validationred" Display="Dynamic" ForeColor="Red" />
                                <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server" ForeColor="Red"
                                    ID="revtxtEmail" ValidationGroup="airsupplier" ErrorMessage="Enter Valid Email."
                                    Text="Enter Valid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Web</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtWeb" runat="server" CssClass="form-control" MaxLength="100" />

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Physical Address</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control multipleLine" MaxLength="200" TextMode="MultiLine" />
                                <asp:RegularExpressionValidator ID="revtxtPhysicalAddress" ControlToValidate="txtPhysicalAddress"
                                    runat="server" ErrorMessage="Maximum 200 characters." Text="Maximum 200 characters."
                                    Display="Dynamic" ValidationGroup="airsupplier" class="validationred" ValidationExpression="^[\s\S]{0,200}$" />
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Postal Address</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPostalAddress" runat="server" CssClass="form-control multipleLine" MaxLength="200" TextMode="MultiLine" />
                                <asp:RegularExpressionValidator ID="revtxtPostalAddress" ControlToValidate="txtPostalAddress"
                                    runat="server" ErrorMessage="Maximum 200 characters." Text="Maximum 200 characters."
                                    Display="Dynamic" ValidationGroup="airsupplier" class="validationred" ValidationExpression="^[\s\S]{0,200}$" />
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <h4><b>EXTERNAL</b></h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5><b>External References</b></h5>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">No VAT No</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:CheckBox ID="chkVATNO" runat="server" OnCheckedChanged="chkVATNO_CheckedChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">VAT Reg No</label>
                            </div>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txtVATRegNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Ext Account</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtExtAcc" runat="server" CssClass="form-control" MaxLength="50" />

                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Alpha Code</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtAlphaCode" runat="server" CssClass="form-control" MaxLength="50" />

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5><b>Banking Details</b></h5>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Bank</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropBank" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-1"></div>

                            <div class="col-sm-2">
                                <label class="control-label">Branch Code</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Branch Name</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                            <div class="col-sm-1"></div>

                            <div class="col-sm-2">
                                <label class="control-label">Account No</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" MaxLength="50" />

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Account Type</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropAccountType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-1"></div>

                            <div class="col-sm-2">
                                <label class="control-label">Account Holder</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtAccHolder" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h4><b>BILLING</b></h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5><b>Invoice Billing Options</b></h5>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">QuickGI Account (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:TextBox ID="txtGiAccount" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtGiAccountSUb" runat="server"  OnTextChanged="txtGiAccountSUb_TextChanged" AutoPostBack="true" CssClass="form-control" MaxLength="3" />
                                <asp:RequiredFieldValidator ControlToValidate="txtGiAccountSUb" runat="server" ID="rfvtxtGiAccount1" ValidationGroup="airsupplier"
                                    ErrorMessage="Enter QuickGI Account" Text="Enter QuickGI Account" class="validationred" Display="Dynamic" ForeColor="Red" />
                                <%--<asp:RegularExpressionValidator ControlToValidate="txtGiAccountSUb" runat="server" ForeColor="Red"
                                    ID="revtxtGiAccount1" ValidationGroup="airsupplier" ErrorMessage="Enter Only Numbers."
                                    Text="Enter Only Numbers." ValidationExpression="^(0|[0-9]\d*)$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                 <asp:Label ID="lblaccnoerr" runat="server" ></asp:Label> 
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Ledger Account</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtLedgerAccount" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Commission Method</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCommiMethod" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Commission%</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtCommPerc" runat="server" CssClass="form-control" MaxLength="20" OnTextChanged="txtCommPerc_TextChanged" placeholder="0.00" Style="text-align: right;" AutoPostBack="true" />
                            </div>

                            <div class="col-sm-2">
                                <asp:CheckBox ID="ChkZeroComm" runat="server" OnCheckedChanged="ChkZeroComm_CheckedChanged" AutoPostBack="true" />
                                <label class="control-label">Zero Commission %</label>
                            </div>

                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Payment Method (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropPaymentMethod" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropPaymentMethod" runat="server" ID="rfvdropPaymentMethod" ValidationGroup="airsupplier"
                                    ErrorMessage="Select Payment Method" Text="Select Payment Method" class="validationred" Display="Dynamic" InitialValue="0" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Invoices</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkClientInvoice" runat="server" OnCheckedChanged="chkClientInvoice_CheckedChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-sm-8">
                                <label>Client Tax Invoice (Agency To Issue Tax Invoices To Clients On Behalf Of This Supplier)</label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2"></div>
                            <div class="col-sm-4">
                                <label>Treatment Of Supplier VAT On Our Invoices</label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="dropTreatInvType" runat="server" CssClass="form-control" AppendDataBoundItems="true" style="width:231px;">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Remit Advices</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkPrinTaxInvoice" runat="server" />
                            </div>
                            <div class="col-sm-7">
                                <label>Principal Tax Invoice (Issue Combined Remittance Advice And Tax Invoice To Principal)?</label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Other</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkAlphaTicket" runat="server" />
                            </div>
                            <div class="col-sm-6">
                                <label>Allow Alphanumeric Ticket Numbers?</label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Allocations</label>
                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">Open Item Loading</label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="dropAllocItemType" runat="server" CssClass="form-control" AppendDataBoundItems="true" style="width:231px;">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Flight Classes</label>
                            </div>
                            <div class="col-sm-1">
                                <label class="control-label">Economy</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkEconomy" runat="server" />
                            </div>

                            <div class="col-sm-1">
                                <label class="control-label">Business</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkBusiness" runat="server" />
                            </div>

                            <div class="col-sm-1">
                                <label class="control-label">First Class</label>
                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox ID="ChkFirstClass" runat="server" />
                            </div>

                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <h4><b>NOTES</b></h4>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">

                            <div class="col-sm-2">
                                <label class="control-label">Notepad</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropNoteType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">Notes</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control multipleLine" MaxLength="400" TextMode="MultiLine" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="airsupplier"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnReset"
                        class="btn btn-primary" ValidationGroup="" Text="Reset" OnClick="btnReset_Click" />

                </div>
            </div>


        </div>
    </section>

</asp:Content>

