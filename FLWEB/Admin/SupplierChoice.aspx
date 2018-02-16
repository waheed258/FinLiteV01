﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SupplierChoice.aspx.cs" Inherits="Admin_SupplierChoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_SupChoiceId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Supplier Choice Reason</h2>
        </header>
        <div class="panel-body">


            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtChoiceKey" runat="server" CssClass="form-control" MaxLength="3" />
                       
                        <asp:RequiredFieldValidator ControlToValidate="txtChoiceKey" runat="server" ID="rfvtxtChoiceKey" ValidationGroup="choice"
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
                        <asp:TextBox ID="txtChoiceDescription" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtChoiceDescription" runat="server" ID="rfvtxtChoiceDescription" ValidationGroup="choice"
                            ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtChoiceDescription" runat="server" ForeColor="Red"
                            ID="revtxtChoiceDescription" ValidationGroup="choice" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                    </div>
                    </div>
                </div>

            <div class="form-group"></div>

            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="choice"
                        Text="Submit" OnClick="cmdSubmit_Click"/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>


        </div>
    </section>
</asp:Content>

