<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Branch.aspx.cs" Inherits="Admin_Branch" %>

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
            <h2 class="panel-title">New Branch</h2>
        </header>
        <asp:HiddenField ID="hf_branchid" runat="server" Value="0" />
        <div class="panel-body">
            <div class="col-sm-12">
            </div>
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
            
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Branch Code(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control" MaxLength="3" />
                        <asp:RequiredFieldValidator ControlToValidate="txtBranchCode" runat="server" ID="rfvtxtBranchCode" ValidationGroup="branch"
                            ErrorMessage="Enter Branch Code" Text="Enter Branch Code" class="validationred" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Branch Name(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtBranchName" runat="server" ID="rfvtxtBranchName" ValidationGroup="branch"
                            ErrorMessage="Enter Branch Name" Text="Enter Branch Name" class="validationred" Display="Dynamic" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                   <div class="col-sm-2">
                       <asp:CheckBox ID="chkDeActivate" runat="server"/>
                       <label class="control-label">Deactivate?</label>
                   </div>
                </div>
            </div>
            <div class="col-sm-12">
                <h5><b>Contact Details</b></h5>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Contact Name</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Telephone Number(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTelephoneNumber" runat="server" CssClass="form-control" MaxLength="30" />
                        <asp:RequiredFieldValidator ControlToValidate="txtTelephoneNumber" runat="server" ID="rfvtxtTelephoneNumber" ValidationGroup="branch"
                            ErrorMessage="Enter Telephone Number" Text="Enter Telephone Number" class="validationred" Display="Dynamic" ForeColor="Red" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                         <label class="control-label">Fax Number(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="form-control" MaxLength="30" />
                         <asp:RequiredFieldValidator ControlToValidate="txtFaxNumber" runat="server" ID="rfvtxtFaxNumber" ValidationGroup="branch"
                            ErrorMessage="Enter Fax Number" Text="Enter Fax Number" class="validationred" Display="Dynamic" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                         <label class="control-label">Cell Number</label>
                    </div>
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtCellNumber" runat="server" CssClass="form-control" MaxLength="30" />
                        
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Email Address(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" MaxLength="75" />
                         <asp:RequiredFieldValidator ControlToValidate="txtEmailAddress" runat="server" ID="rfvtxtEmailAddress" ValidationGroup="branch"
                            ErrorMessage="Enter Email Address" Text="Enter Email Address" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtEmailAddress" runat="server" ID="revtxtEmailAddress"
                            Display="Dynamic" ErrorMessage="Enter Valid Email Id." Text="Enter Valid Email Id." ValidationGroup="branch" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Web Address</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtWebAddress" runat="server" CssClass="form-control" MaxLength="75" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Province(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ControlToValidate="ddlProvince" runat="server" ID="rfvddlProvince"
                            Display="Dynamic" ErrorMessage="Select Province." Text="Select Province." ValidationGroup="branch" InitialValue="-1" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">City(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlCity" runat="server" ID="rfvddlCity"
                            Display="Dynamic" ErrorMessage="Select City." Text="Select City." ValidationGroup="branch" InitialValue="-1" ForeColor="Red" />

                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Postal Address</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPostalAddress" runat="server" CssClass="form-control" MaxLength="200" TextMode="MultiLine"/>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Physical Address</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control" MaxLength="200" TextMode="MultiLine"/>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">DOCEX</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDOCEX" runat="server" CssClass="form-control" MaxLength="50"/>
                    </div>
                 </div>
            </div>
            <div class="col-sm-12">
                <h5><b>Registration Numbers</b></h5>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Co Reg No</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCoRegNo" runat="server" CssClass="form-control" MaxLength="50"/>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">IATA Reg No</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtIATARegNo" runat="server" CssClass="form-control" MaxLength="50"/>
                        <asp:RegularExpressionValidator ControlToValidate="txtIATARegNo" runat="server" ID="revtxtIATARegNo"
                            Display="Dynamic" ErrorMessage="Enter Valid No." Text="Enter Valid No." ValidationGroup="branch" ValidationExpression="^[0-9]*$" ForeColor="Red" />

                    </div>
                 </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Vat Reg No</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="form-control" MaxLength="50"/>
                        <asp:RegularExpressionValidator ControlToValidate="txtVatRegNo" runat="server" ID="revtxtVatRegNo"
                            Display="Dynamic" ErrorMessage="Enter Valid No." Text="Enter Valid No." ValidationGroup="branch" ValidationExpression="^[0-9]*$" ForeColor="Red" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-3">
                        <asp:CheckBox ID="chkASATAMember" runat="server"/>
                        <label class="control-label">Member of ASATA?</label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-5">
                        <asp:CheckBox ID="chkPrintDoc" runat="server"/>
                        <label class="control-label">Always use these details when printing documents?</label>
                    </div>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="branch"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button runat="server" ID="cmdReset"
                                class="btn btn-primary green" Text="Reset" OnClick="cmdReset_Click" />

                </div>
            </div>
        </div>
    </section>
</asp:Content>


