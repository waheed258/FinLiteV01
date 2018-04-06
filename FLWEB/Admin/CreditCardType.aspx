<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CreditCardType.aspx.cs" Inherits="Admin_CreditCardType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_CreditCardId" runat="server" Value="0" />
     
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Card Type</h2>
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
                                <asp:TextBox ID="txtCreditKey" runat="server" Cssclass="form-control" MaxLength="3" />
                                 <asp:RequiredFieldValidator ControlToValidate="txtCreditKey" runat="server" ID="rfvtxtCreditKey" ValidationGroup="credit"
                                    ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                            </div>
                            <div class="col-sm-1"></div>
                             <div class="col-sm-2">
                                <label class="control-label">Description(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCreDescription" runat="server" Cssclass="form-control" MaxLength="50" />
                                 <asp:RequiredFieldValidator ControlToValidate="txtCreDescription" runat="server" ID="rfvtxtCreDescription" ValidationGroup="credit"
                                    ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtCreDescription" runat="server" ForeColor="Red"
                            ID="revtxtCreDescription" ValidationGroup="credit" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                            </div>

                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Number Prefix(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtNumberPrefix" runat="server" Cssclass="form-control" MaxLength="3" />
                                 <asp:RequiredFieldValidator ControlToValidate="txtNumberPrefix" runat="server" ID="rfvtxtNumberPrefix" ValidationGroup="credit"
                                    ErrorMessage="Enter Number Prefix" Text="Enter Number Prefix" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtNumberPrefix" runat="server" ForeColor="Red"
                                        ID="revtxtNumberPrefix" ValidationGroup="credit" ErrorMessage="Enter Only Numbers."
                                        Text="Enter Only Numbers." ValidationExpression="^(0|[1-9]\d*)$"
                                        Display="Dynamic"></asp:RegularExpressionValidator>

                            </div>
                            </div>
                        </div>

                    
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="credit"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>


        </div>
    </section>
</asp:Content>

