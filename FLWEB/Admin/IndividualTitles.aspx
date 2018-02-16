<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="IndividualTitles.aspx.cs" Inherits="Admin_IndividualTitles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_TitleId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Individual Titles</h2>
        </header>
        <div class="panel-body">


            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTitleKey" runat="server" CssClass="form-control" MaxLength="5" />
                       
                        <asp:RequiredFieldValidator ControlToValidate="txtTitleKey" runat="server" ID="rfvtxtTitleKey" ValidationGroup="title"
                            ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                    </div>
                    <div class="col-sm-1"></div>
                   
                    <div class="col-sm-2">
                        <label class="control-label">Description(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTitleDescription" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtTitleDescription" runat="server" ID="rfvtxtTitleDescription" ValidationGroup="title"
                            ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtTitleDescription" runat="server" ForeColor="Red"
                            ID="revtxtTitleDescription" ValidationGroup="title" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                    </div>
                    </div>
                </div>

            <div class="form-group">
                <div class="col-sm-12">
             <div class="col-sm-2">
                        <label class="control-label">Gender(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="dropGender" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                            </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="dropGender" runat="server" ID="rfvdropGender" ValidationGroup="title"
                            ErrorMessage="Select Gender" Text="Select Gender" class="validationred" Display="Dynamic" InitialValue="-1" ForeColor="Red" />
                    </div>
                    </div>
                </div>

            <div class="form-group"></div>

            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="title"
                        Text="Submit" OnClick="cmdSubmit_Click"/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click"/>

                </div>
            </div>


        </div>
    </section>
</asp:Content>

