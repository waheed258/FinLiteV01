<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Consultant.aspx.cs" Inherits="Admin_Consultant" %>

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
             $('#<%= ddlGroup.ClientID %>').select2();
            $('#<%= ddlDivision.ClientID %>').select2();
            $('#<%= ddlClientType.ClientID %>').select2();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
   <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_ConsultantId" runat="server" Value="0" />
    <asp:HiddenField ID="hf_AccCode" runat="server" Value="0" />
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Consultant</h2>
        </header>
        <div class="panel-body">
            <div class="form-group">
                <div class="col-sm-12">
                     <div class="col-sm-2">
                        <label class="control-label">
                            Name(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="50" />
                         <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" ID="rfvtxtName"
                            Display="Dynamic" Text="Enter Name." ErrorMessage="Enter Name." ValidationGroup="consultant" ForeColor="Red" />
                    </div>
                   
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Key(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="3" OnTextChanged="txtKey_TextChanged" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey"
                            Display="Dynamic" Text="Enter Key." ErrorMessage="Enter Key." ValidationGroup="consultant" ForeColor="Red" />
                         <asp:Label ID="lblaccnoerr" runat="server" ></asp:Label> 
                    </div>
                </div>
            </div>
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
                            Email(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="75" />
                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ID="rfvtxtEmail"
                            Display="Dynamic" Text="Enter Email Id." ErrorMessage="Enter Email Id." ValidationGroup="consultant" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server"
                            ID="revtxtEmail" ValidationGroup="consultant" ErrorMessage="Enter Valid Email Id."
                            Text="Enter Valid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Telephone(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="form-control" MaxLength="15" />
                        <asp:RequiredFieldValidator ControlToValidate="txtTelephoneNo" runat="server" ID="rfvtxtTelephoneNo"
                            Display="Dynamic" Text="Enter Telephone No." ErrorMessage="Enter Telephone No." ValidationGroup="consultant" ForeColor="Red" />
                        <asp:RegularExpressionValidator ControlToValidate="txtTelephoneNo" runat="server" ForeColor="Red"
                            ID="revtxtTelephoneNo" ValidationGroup="consultant" ErrorMessage="Enter Valid Telephone No."
                            Text="Enter Valid Telephone No." ValidationExpression="^[0-9]{10,15}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Cell
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCellNo" runat="server" CssClass="form-control" MaxLength="15" />
                        <asp:RegularExpressionValidator ControlToValidate="txtCellNo" runat="server" ForeColor="Red"
                            ID="revtxtCellNo" ValidationGroup="consultant" ErrorMessage="Enter Valid CellNo."
                            Text="Enter Valid CellNo." ValidationExpression="^[0-9]{10,15}$"
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
                            ID="revtxtFaxNo" ValidationGroup="consultant" ErrorMessage="Enter Valid FaxNo."
                            Text="Enter Valid FaxNo." ValidationExpression="^[0-9]{10,20}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                <div class="col-sm-2">
                        <label class="control-label">
                            Client Type
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlClientType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                              <%--<asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>--%>
                     </asp:DropDownList>
                    </div>
                    <div class="col-sm-1"></div>
                     <div class="col-sm-2">
                         <asp:CheckBox ID="chkDeActivate" runat="server" />
                        <label class="control-label">DeActivate?</label>
                    </div>
                </div>
            </div>
           
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="consultant"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp; <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />

                </div>
            </div>
       
        </div>
   
    </section>
</asp:Content>

