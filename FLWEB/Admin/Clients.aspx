<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Clients.aspx.cs" Inherits="Admin_Clients" %>

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
            $('#<%= ddlClientType.ClientID %>').select2();
            $('#<%= ddlGroup.ClientID %>').select2();
            $('#<%= ddlConsultant.ClientID %>').select2();
            $('#<%= ddlDivision.ClientID %>').select2();
            $('#<%= ddlDepartment.ClientID %>').select2();
            $('#<%= ddlYearEnd.ClientID %>').select2();
            $('#<%= ddlAlign.ClientID %>').select2();
            $('#<%= ddlDuplicateOrderNo.ClientID %>').select2();
            $('#<%= ddlNoServiceFees.ClientID %>').select2();
            $('#<%= ddlOpenItemLoad.ClientID %>').select2();
            $('#<%= ddlFwdBreDwn.ClientID %>').select2();
            $('#<%= ddlLineItemBreakDwn.ClientID %>').select2();
            $('#<%= ddlDbOrCd.ClientID %>').select2();
            $('#<%= ddlSuppressNilVal.ClientID %>').select2();
            $('#<%= ddlTotalCharge.ClientID %>').select2();
            $('#<%= ddlCreditCard.ClientID %>').select2();
            $('#<%= ddlTransactionRef.ClientID %>').select2();
            $('#<%= ddlAllocPaidItems.ClientID %>').select2();
            $('#<%= ddlLimitAction.ClientID %>').select2();
            $('#<%= ddlCreditTerms.ClientID %>').select2();
            $('#<%= ddlTermsAction.ClientID %>').select2();
            $('#<%= ddlCalAir.ClientID %>').select2();
            $('#<%= ddlCalLand.ClientID %>').select2();
            $('#<%= ddlDirectSettleTrans.ClientID %>').select2();
            $('#<%= ddlCCForCashTrans.ClientID %>').select2();
            $('#<%= ddlCardImportedTickets.ClientID %>').select2();
            $('#<%= ddlNoteType.ClientID %>').select2();

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_ClientId" runat="server" Value="0" />
    <asp:HiddenField ID="hfImageLogo" runat="server" />
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Client</h2>
        </header>
        <div class="panel-body">
            <div class="form-group">

                <div class="col-sm-1">
                    <label class="control-label">
                        ClientType(<span class="style1">*</span>)
                    </label>
                </div>
                <div class="col-sm-1">
                    <asp:DropDownList ID="ddlClientType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlClientType_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlClientType" runat="server" ID="rfvddlClientType"
                        Display="Dynamic" Text="Select Client Type." ErrorMessage="Select Client Type." ValidationGroup="client" ForeColor="Red" InitialValue="0" />
                </div>
                <div class="col-sm-2">
                    <label class="control-label">
                        Account Number(<span class="style1">*</span>)
                    </label>
                </div>
                <div class="col-sm-1">

                    <asp:TextBox ID="txtAccCode" runat="server" CssClass="form-control" Enabled="false" />
                </div>
                <div class="col-sm-2">

                    <asp:TextBox ID="txtAccountNo" OnTextChanged="txtAccountNo_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" MaxLength="5" />
                    <asp:RequiredFieldValidator ControlToValidate="txtAccountNo" runat="server" ID="rfvtxtAccountNo"
                        Display="Dynamic" Text="Enter Account Number." ErrorMessage="Enter Account Number." ValidationGroup="client" ForeColor="Red" />
                 <asp:Label ID="lblaccnoerr" runat="server" ></asp:Label> 
                </div>

                <div class="col-sm-1">
                    <label class="control-label">
                        Name(<span class="style1">*</span>)
                    </label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator ControlToValidate="txtClientName" runat="server" ID="rfvtxtClientName"
                        Display="Dynamic" Text="Enter Client Name." ErrorMessage="Enter Client Name." ValidationGroup="client" ForeColor="Red" />
                </div>
                <div class="col-sm-1">
                    <label class="control-label">Active?</label>
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="chkIsActive" runat="server" />
                </div>

            </div>
            <h5><b>GENERAL</b></h5>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Group
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Vat Reg No
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="form-control" MaxLength="30" />

                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">No Vat No</label>
                        <asp:CheckBox ID="chkNoVatNo" runat="server" />
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Consultant
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlConsultant" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Division
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>


                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Department
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>

                </div>
            </div>
            <h5><b>Contact Details</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Telephone(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="form-control" MaxLength="15" />
                        <asp:RequiredFieldValidator ControlToValidate="txtTelephoneNo" runat="server" ID="rfvtxtTelephoneNo"
                            Display="Dynamic" Text="Enter Telephone No." ErrorMessage="Enter Telephone No." ValidationGroup="client" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtTelephoneNo" runat="server" ForeColor="Red"
                            ID="revtxtTelephoneNo" ValidationGroup="client" ErrorMessage="Enter Valid Telephone No."
                            Text="Enter Valid Telephone No." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Fax
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFaxNo" runat="server" CssClass="form-control" MaxLength="20" />
                        <asp:RegularExpressionValidator ControlToValidate="txtFaxNo" runat="server" ForeColor="Red"
                            ID="revtxtFaxNo" ValidationGroup="client" ErrorMessage="Enter Valid FaxNo."
                            Text="Enter Valid FaxNo." ValidationExpression="^[0-9]{10,20}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Contact
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" MaxLength="15" />
                        <asp:RegularExpressionValidator ControlToValidate="txtContactNo" runat="server" ForeColor="Red"
                            ID="revtxtContactNo" ValidationGroup="client" ErrorMessage="Enter Valid ContactNo."
                            Text="Enter Valid ContactNo." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Email(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="75" />
                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ID="rfvtxtEmail"
                            Display="Dynamic" Text="Enter Email Id." ErrorMessage="Enter Email Id." ValidationGroup="client" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server"
                            ID="revtxtEmail" ValidationGroup="client" ErrorMessage="Enter Valid Email Id."
                            Text="Enter Valid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Postal Address
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPostalAddress" runat="server" CssClass="form-control multipleLine" MaxLength="200" TextMode="MultiLine" />

                        <asp:RegularExpressionValidator ID="revtxtPostalAddress" runat="server" ErrorMessage="Postal Address accept 200 character."
                            ControlToValidate="txtPostalAddress" ValidationExpression="^[\s\S]{0,200}$"
                            ValidationGroup="client" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Physical Address
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control multipleLine" MaxLength="200" TextMode="MultiLine" />

                        <asp:RegularExpressionValidator ID="revtxtPhysicalAddress" runat="server" ErrorMessage="Physical Address accept 200 character."
                            ControlToValidate="txtPostalAddress" ValidationExpression="^[\s\S]{0,200}$"
                            ValidationGroup="client" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <h5><b>Other</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Year End
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlYearEnd" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Please Select--" Value="0"> </asp:ListItem>
                            <asp:ListItem Text="2017" Value="2017"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Logo
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="fuLogo" runat="server" />

                        <a id="logoview" href="#" runat="server">
                            <asp:Label ID="lblLogo" runat="server" /></a>
                        <%--<asp:TextBox ID="txtLogo" runat="server" CssClass="form-control" MaxLength="50" />--%>
                        <%--   <asp:RequiredFieldValidator ControlToValidate="txtLogo" runat="server"
                        ID="rfvtxtLogo" ValidationGroup="client" ErrorMessage="Enter Logo."
                        Text="Enter Logo." ForeColor="Red" Display="Dynamic" />
                        --%>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            HTML Width
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtHtmlWidth" runat="server" CssClass="form-control" MaxLength="50" />

                    </div>
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Height
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtHtmlHeight" runat="server" CssClass="form-control" MaxLength="50" />

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Align
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlAlign" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Please Select--" Value="0"> </asp:ListItem>
                            <asp:ListItem Text="Left" Value="Left"></asp:ListItem>
                            <asp:ListItem Text="Right" Value="Right"></asp:ListItem>
                            <asp:ListItem Text="Center" Value="Center"></asp:ListItem>

                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>

                </div>
            </div>
            <h4><b>BILLING</b></h4>
            <h5><b>Billing Options</b></h5>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-4">
                        <asp:CheckBox ID="chkAutoPrintmanual" runat="server" />
                        <label class="control-label">Disable auto printing of manual invoices ?</label>
                    </div>

                    <div class="col-sm-2"></div>
                    <div class="col-sm-5">
                        <asp:CheckBox ID="chkAutoPrintTicket" runat="server" />
                        <label class="control-label">Disable auto printing of imported ticket invoices ?</label>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Default Order No
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtDefaultOrderNo" runat="server" CssClass="form-control" MaxLength="50" />

                    </div>
                    <div class="col-sm-3">
                        <asp:CheckBox ID="chkForceOrderNo" runat="server" />
                        <label class="control-label">Force Order Numbers?</label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-4">
                        <asp:CheckBox ID="chkForceExternalVoucher" runat="server" />
                        <label class="control-label">Force External Voucher Verification?</label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">
                        <label class="control-label">
                            Action In The Event Of A Duplicate Order Number Being Captured
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDuplicateOrderNo" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">
                        <label class="control-label">
                            Action If No Service Fees On Invoices
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlNoServiceFees" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">
                        <label class="control-label">
                            Allocation Open Item Loading
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlOpenItemLoad" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>

                </div>
            </div>

            <h5><b>Statements</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Show Brought Forward Breakdown?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlFwdBreDwn" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Show Line Item Breakdown Allocation Open Item Loading?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlLineItemBreakDwn" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Use Debit/Credit Columns ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDbOrCd" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Suppress Nil Value Entries ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlSuppressNilVal" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Show 'Total Charge' Column ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlTotalCharge" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Show Credit Card Column ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCreditCard" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Sort By Transaction Reference ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlTransactionRef" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Hide Allocated(paid) items ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlAllocPaidItems" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Custom Detail Column ?
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCustmDetail" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-3">
                        <asp:CheckBox ID="chkExcludeFmPrint" runat="server" />
                        <label class="control-label">Exclude From Print run?</label>
                    </div>


                </div>
            </div>
            <h5><b>Credit Control</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Credit Limit
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="form-control" MaxLength="50" placeholder="0" />
                    </div>

                    <div class="col-sm-2">
                        <label class="control-label">
                            Action If Limit Exceeded?
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlLimitAction" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Max Invoice Value
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtMaxInvoiceVal" runat="server" CssClass="form-control" MaxLength="50" placeholder="0" />
                    </div>


                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Credit Terms
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlCreditTerms" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Action If Terms Exceeded?
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlTermsAction" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <h5><b>Published Fares</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-1">
                        <asp:CheckBox ID="chkIsForce" runat="server" />

                    </div>
                    <div class="col-sm-1">
                        <label class="control-label">Force ?</label>
                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Calculations-Air
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlCalAir" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Land
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlCalLand" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <h4><b>CONTACTS</b></h4>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactKey" runat="server" class="form-control" MaxLength="3" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <asp:CheckBox ID="chkDeActivate" runat="server" />
                        <label class="control-label">DeActivate?</label>
                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Contact Name</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="50" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Position</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPosition" runat="server" class="form-control" MaxLength="50" />
                    </div>


                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Telephone</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTelephone" runat="server" class="form-control" MaxLength="15" />
                        <asp:RegularExpressionValidator ControlToValidate="txtTelephone" runat="server" ForeColor="Red"
                            ID="revtxtTelephone" ValidationGroup="client" ErrorMessage="Enter Valid Telephone No."
                            Text="Enter Valid Telephone No." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Fax No</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtConFaxNo" runat="server" class="form-control" MaxLength="20" />
                        <asp:RegularExpressionValidator ControlToValidate="txtConFaxNo" runat="server" ForeColor="Red"
                            ID="revtxtConFaxNo" ValidationGroup="client" ErrorMessage="Enter Valid Fax No."
                            Text="Enter Valid Fax No." ValidationExpression="^[0-9]{10,20}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12 marginbtm">
                    <div class="col-sm-2">
                        <label class="control-label">Cell No</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtConCellNo" runat="server" class="form-control" MaxLength="15" />
                        <asp:RegularExpressionValidator ControlToValidate="txtConCellNo" runat="server" ForeColor="Red"
                            ID="revtxtConCellNo" ValidationGroup="client" ErrorMessage="Enter Valid Cell No."
                            Text="Enter Valid Cell No." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Email Address</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmailAddress" runat="server" class="form-control" MaxLength="75" />
                        <asp:RegularExpressionValidator ControlToValidate="txtEmailAddress" runat="server" ForeColor="Red"
                            ID="revtxtEmailAddress" ValidationGroup="client" ErrorMessage="Enter Valid Email."
                            Text="Enter Valid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12 marginbtm">
                    <div class="col-sm-2">
                        <label class="control-label">AutoMail</label>
                    </div>
                    <div class="col-sm-1">
                        <asp:CheckBox ID="chkAutomail" runat="server" />
                    </div>

                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>


                    <asp:Repeater ID="rptContactDetails" runat="server" OnItemCommand="rptContactDetails_ItemCommand">
                        <ItemTemplate>
                            <div class="col-sm-12 marginbtm" style="border: 1px solid #08376a; background: #D4E4EF; margin-top: 10px;">
                                <br />

                                <asp:HiddenField runat="server" ID="hfContactId" Value='<%# Eval("ContactId") %>' />

                                <div class="col-sm-12 marginbtm">
                                    <asp:ImageButton ID="imgContactDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ContactId") %>' ImageUrl="~/images/close.png"
                                        OnClientClick="javascript:return confirm('Are You Sure To Delete Contact Details?')" />
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">Key</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtContactKey" runat="server" class="form-control" MaxLength="3" Text='<%#DataBinder.Eval(Container.DataItem,"ContactKey")%>' />
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="chkDeActivate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConDeactivate")%>' />
                                            <label class="control-label">DeActivate?</label>
                                        </div>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <label class="control-label">Contact Name</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="50" Text='<%#DataBinder.Eval(Container.DataItem,"ContactName")%>' />
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Position</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtPosition" runat="server" class="form-control" MaxLength="50" Text='<%#DataBinder.Eval(Container.DataItem,"Position")%>' />
                                        </div>



                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">

                                        <div class="col-sm-2">
                                            <label class="control-label">Telephone</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTelephone" runat="server" class="form-control" MaxLength="15" Text='<%#DataBinder.Eval(Container.DataItem,"Telephone")%>' />
                                            <asp:RegularExpressionValidator ControlToValidate="txtTelephone" runat="server" ForeColor="Red"
                                                ID="revtxtTelephone" ValidationGroup="client" ErrorMessage="Enter Valid Telephone No."
                                                Text="Enter Valid Telephone No." ValidationExpression="^[0-9]{10,15}$"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Fax No</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtConFaxNo" runat="server" class="form-control" MaxLength="20" Text='<%#DataBinder.Eval(Container.DataItem,"ConFax")%>' />
                                            <asp:RegularExpressionValidator ControlToValidate="txtConFaxNo" runat="server" ForeColor="Red"
                                                ID="revtxtConFaxNo" ValidationGroup="client" ErrorMessage="Enter Valid Fax No."
                                                Text="Enter Valid Fax No." ValidationExpression="^[0-9]{10,20}$"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 marginbtm">
                                        <div class="col-sm-2">
                                            <label class="control-label">Email Address</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtEmailAddress" runat="server" class="form-control" MaxLength="75" Text='<%#DataBinder.Eval(Container.DataItem,"EmailAddress")%>' />
                                            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddress" runat="server" ForeColor="Red"
                                                ID="revtxtEmailAddress" ValidationGroup="client" ErrorMessage="Enter Valid Email."
                                                Text="Enter Valid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-1"></div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Cell No</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtConCellNo" runat="server" class="form-control" MaxLength="15" Text='<%#DataBinder.Eval(Container.DataItem,"ConCellNo")%>' />
                                            <asp:RegularExpressionValidator ControlToValidate="txtConCellNo" runat="server" ForeColor="Red"
                                                ID="revtxtConCellNo" ValidationGroup="client" ErrorMessage="Enter Valid Cell No."
                                                Text="Enter Valid Cell No." ValidationExpression="^[0-9]{10,15}$"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">


                                        <div class="col-sm-2">
                                            <label class="control-label">AutoMail</label>
                                        </div>
                                        <div class="col-sm-1">

                                            <asp:CheckBox ID="chkAutomail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AutoMail")%>' />
                                        </div>

                                    </div>
                                </div>



                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="form-group">
                        &nbsp;
                    </div>
                    <div class="form-group">

                        <div class="col-sm-12">
                            <asp:ImageButton ID="ImgbtnContactDetails" runat="server" ToolTip="Add Contact Details" ImageUrl="~/images/add.png" OnClick="ImgbtnContactDetails_Click" />
                            <label><b>Add Contact </b></label>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>



            <h4><b>CREDIT CARDS</b></h4>
            <h5><b>Client Credit Cards</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">

                        <label class="control-label">Credit Card Capture On Non Ticket Direct Settlement Transactions</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDirectSettleTrans" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">

                        <label class="control-label">Default Credit Card For Cash Transactions</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCCForCashTrans" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">
                        <asp:CheckBox ID="chkTicketOrLandCreditCard" runat="server" />
                        <label class="control-label">Always Use The Source Ticket/Land Credit Card If Available For Cash Transactions?</label>
                    </div>

                    <div class="col-sm-4"></div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-8">

                        <label class="control-label">Automatically Add Credit Cards From Imported Tickets?</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCardImportedTickets" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>



                </div>
            </div>
            <h4><b>NOTES</b></h4>
            <h5><b>Client Notepad</b></h5>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">

                        <label class="control-label">Note Type(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlNoteType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlNoteType" runat="server"
                            ID="rfvddlNoteType" ValidationGroup="client" ErrorMessage="Select Note Type."
                            Text="Select Note Type." ForeColor="Red" Display="Dynamic" InitialValue="0s" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Notes(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control multipleLine" MaxLength="200" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ControlToValidate="txtNotes" runat="server"
                            ID="rfvtxtNotes" ValidationGroup="client" ErrorMessage="Enter Notes."
                            Text="Enter Notes." ForeColor="Red" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revtxtNotes" runat="server" ErrorMessage="Notes accept 200 character."
                            ControlToValidate="txtNotes" ValidationExpression="^[\s\S]{0,200}$"
                            ValidationGroup="client" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="client"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>

        </div>
    </section>
</asp:Content>

