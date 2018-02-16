<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="RouteMiles.aspx.cs" Inherits="Admin_RouteMiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="labelError" runat="server"></asp:Label>
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Routing Miles And Distance</h2>
        </header>
        <asp:HiddenField ID="hf_RMId" runat="server" Value="0" />
        <div class="panel-body">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="10" />
                        <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey" ValidationGroup="RouteMiles"
                            ErrorMessage="Enter Key" Text="Enter Key" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Deactivate?</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:CheckBox ID="chkDeactivate" runat="server" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Routing(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRouting" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtRouting" runat="server" ID="rfvtxtRouting" ValidationGroup="RouteMiles"
                            ErrorMessage="Enter Routing" Text="Enter Routing" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Miles(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtMiles" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtMiles" runat="server" ID="rfvtxtMiles" ValidationGroup="RouteMiles"
                            ErrorMessage="Enter Miles" Text="Enter Miles" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtMiles" runat="server" ID="revtxtMiles" ValidationGroup="RouteMiles"
                            ErrorMessage="Enter Number Only" Text="Enter Number Only" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]*$" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                   <div class="col-sm-2">
                        <label class="control-label">Duration[hh:mm]</label>
                   </div>
                   <div class="col-sm-3">
                         <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control"/>
                        <%--<asp:RequiredFieldValidator ControlToValidate="txtRouting" runat="server" ID="rfvtxtDuration" ValidationGroup="RouteMiles"
                            ErrorMessage="Enter Duration" Text="Enter Duration" Display="Dynamic" ForeColor="Red" />--%>
                   </div>
                </div>
            </div>
            <div class="form-group">
            <div class="col-sm-5">
            </div>
            <div class="col-sm-3">
                <asp:Button runat="server" ID="btnSubmit" class="btn btn-success" ValidationGroup="RouteMiles"
                    Text="Submit" OnClick="btnSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button runat="server" ID="btnReset"
                            class="btn btn-primary green" Text="Reset" OnClick="btnReset_Click"/>

            </div>
        </div>
        </div>
    </section>
</asp:Content>

