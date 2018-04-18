<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CashBookType.aspx.cs" Inherits="Admin_CashBookType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_CashBookId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Cash Book Type</h2>
        </header>
        <div class="panel-body">


            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCashKey" runat="server" CssClass="form-control" MaxLength="3" OnTextChanged="txtCashKey_TextChanged" AutoPostBack="true"/>
                       
                        <asp:RequiredFieldValidator ControlToValidate="txtCashKey" runat="server" ID="rfvtxtCashKey" ValidationGroup="cash"
                            ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-5">
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
                        <asp:TextBox ID="txtCashDescription" runat="server" CssClass="form-control" MaxLength="50" OnTextChanged="txtCashDescription_TextChanged" AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtCashDescription" runat="server" ID="rfvtxtCashDescription" ValidationGroup="cash"
                            ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtCashDescription" runat="server" ForeColor="Red"
                            ID="revtxtCashDescription" ValidationGroup="cash" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Default Action</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="dropDefaultAction" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="dropDefaultAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Default GI Code(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGICode" runat="server" CssClass="form-control" MaxLength="50" OnTextChanged="txtGICode_TextChanged" AutoPostBack="true"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtGICode" runat="server" ID="rfvtxtGICode" ValidationGroup="cash"
                            ErrorMessage="Enter Default GI Code" Text="Enter Default GI Code" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtGICode" runat="server" ForeColor="Red"
                            ID="revtxtGICode" ValidationGroup="cash" ErrorMessage="Enter Only Numbers."
                            Text="Enter Only Numbers." ValidationExpression="^(0|[1-9]\d*)$"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Format For Numeric References</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRefFormat" runat="server" CssClass="form-control" MaxLength="50" />

                    </div>
                </div>
            </div>

              <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Allowable VAT Codes</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtVatCodes" runat="server" CssClass="form-control" MaxLength="50" />

                    </div>
                    </div>
                  </div>
            <div class="form-group"></div>

            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="cash"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="cmdSubmit_Click"/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>


        </div>
    </section>
</asp:Content>

