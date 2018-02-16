<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ContactLog.aspx.cs" Inherits="Admin_ContactLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
        .style1 {
            color: #FF0000;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_ContactLogId" runat="server" Value="0" />
     
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Contact Log Category</h2>
        </header>
        <div class="panel-body">

      
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Key(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtLogKey" runat="server" Cssclass="form-control" MaxLength="3" />
                                 <asp:RequiredFieldValidator ControlToValidate="txtLogKey" runat="server" ID="rfvtxtLogKey" ValidationGroup="log"
                                    ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                            </div>
                            <div class="col-sm-1"></div>
                             <div class="col-sm-2">
                                <label class="control-label">Description(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtLogDescription" runat="server" Cssclass="form-control" MaxLength="80" />
                                 <asp:RequiredFieldValidator ControlToValidate="txtLogDescription" runat="server" ID="rfvtxtLogDescription" ValidationGroup="log"
                                    ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtLogDescription" runat="server" ForeColor="Red"
                            ID="revtxtLogDescription" ValidationGroup="log" ErrorMessage="Enter Only Characters."
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
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="log"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click"/>

                </div>
            </div>


        </div>
    </section>
</asp:Content>

