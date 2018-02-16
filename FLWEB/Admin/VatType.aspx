<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="VatType.aspx.cs" Inherits="Admin_VatType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>

    <%-- <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.js"></script>


    <script src="js/wickedpicker.js"></script>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            DatePickerSet();
        });
        function DatePickerSet() {
            $("#ContentPlaceHolder1_txtEffectiveDate").datepicker({
                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                autoclose: true,
            }).attr('readonly', 'true');;
        }
        </script>--%>
     <script type="text/javascript">
         $(document).ready(function () {
             DatePickerSet();
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {

                 DatePickerSet();

             });

         });
         function DatePickerSet() {
            // $('#ContentPlaceHolder1_txtEffectiveDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
             $("#ContentPlaceHolder1_txtEffectiveDate").datepicker({
                 format: 'yyyy-mm-dd',
                 autoclose: true
             }).attr('readonly', 'false');;
             $('#<%= ddlApplicableTo.ClientID %>').select2();

         }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="labelError" runat="server"></asp:Label>
   
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New VAT Type</h2>
        </header>
        <asp:HiddenField ID="hf_VatId" runat="server" Value="0" />
        <div class="panel-body">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="2" />
                        <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey" ValidationGroup="Vat"
                            ErrorMessage="Enter Key" Text="Enter Key" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Description(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="40" />
                        <asp:RequiredFieldValidator ControlToValidate="txtDescription" runat="server" ID="rfvtxtDescription" ValidationGroup="Vat"
                            ErrorMessage="Enter Description" Text="Enter Description" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtDescription" runat="server" ForeColor="Red"
                            ID="revtxtDescription" ValidationGroup="Vat" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">VAT Rate(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtVatRate" runat="server" CssClass="form-control" MaxLength="10" />
                        <asp:RequiredFieldValidator ControlToValidate="txtVatRate" runat="server" ID="rfvtxtVatRate" ValidationGroup="Vat"
                            ErrorMessage="Enter Vat Rate" Text="Enter Vat Rate" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Applicable To(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlApplicableTo" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-Select-" Value="-1" />
                            <asp:ListItem Text="Purchases" Value="Purchases" />
                            <asp:ListItem Text="Sales" Value="Sales" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlApplicableTo" runat="server" ID="rfvddlApplicableTo" ValidationGroup="Vat"
                            ErrorMessage="Select Applicable To" Text="Select  Applicable To" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Effective Date</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control" MaxLength="20" />
                       <%-- <asp:RequiredFieldValidator ControlToValidate="txtEffectiveDate" runat="server" ID="rfvtxtEffectiveDate" ValidationGroup="Vat"
                            ErrorMessage="Enter Effective Date" Text="Enter Effective Date" Display="Dynamic" ForeColor="Red" />--%>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">GI Code</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGICode" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="btnSubmit" class="btn btn-success" ValidationGroup="Vat"
                        Text="Submit" OnClick="btnSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button runat="server" ID="btnReset"
                                class="btn btn-primary green" Text="Reset" OnClick="btnReset_Click" />

                </div>
            </div>
        </div>

    </section>

</asp:Content>

