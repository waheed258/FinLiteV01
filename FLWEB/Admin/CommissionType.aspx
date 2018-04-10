<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CommissionType.aspx.cs" Inherits="CommissionType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="labelError" runat="server"></asp:Label>
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
             $('#<%= ddlCategory.ClientID %>').select2();
             $('#<%= ddlLandSubCategory.ClientID %>').select2();
             $('#<%= ddlDefaultType.ClientID %>').select2();
             $('#<%= ddlDefaultVAT.ClientID %>').select2();
             $('#<%= ddlZeroUnitsType.ClientID %>').select2();
             $('#<%= ddlIncomeCharges.ClientID %>').select2();
             $('#<%= ddlOutputVAT.ClientID %>').select2();

         };
    </script>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Commission Type</h2>
        </header>
        <asp:HiddenField ID="hf_ComId" runat="server" Value="0" />
        <div class="panel-body">
            <div class="col-sm-12"></div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Key(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="3" OnTextChanged="txtKey_TextChanged" AutoPostBack="true"/>
                         <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey" ValidationGroup="CommType"
                            ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />
                        <asp:Label ID="lblKeyerr" runat="server" ></asp:Label> 
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Deactivate?</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:CheckBox ID="chkDeactivate" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                   <div class="col-sm-2">
                        <label class="control-label">Description(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtDescription" runat="server" ID="rfvtxtDescription"
                            Display="Dynamic" Text="Enter Description." ErrorMessage="Enter Description." ValidationGroup="CommType" ForeColor="Red" />
                         <%-- <asp:RegularExpressionValidator ControlToValidate="txtDescription" runat="server"
                            ID="revtxtDescription" ValidationGroup="CommType" ErrorMessage="Enter Only Characters.."
                            Text="Enter Only Characters.." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Category(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"> 
                                    <asp:ListItem Text="--Select Category--" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlCategory" runat="server" ID="rfvddlCategory" ValidationGroup="CommType"
                        ErrorMessage="Select Category" Text="Select  Category" Display="Dynamic" ForeColor="Red" InitialValue="0"/>
                </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Land Sub Category</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlLandSubCategory" runat="server" CssClass="form-control">
                           <asp:ListItem Text="-Select-" Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Default Type(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDefaultType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="--Select Default Type--" Value="0" Selected="True"></asp:ListItem>

                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlDefaultType" runat="server" ID="rfvddlDefaultType" ValidationGroup="CommType"
                        ErrorMessage="Select Default Type" Text="Select Default Type" Display="Dynamic" ForeColor="Red" InitialValue="0"/>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">Default Comm%</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDefaultComm" runat="server" CssClass="form-control" placeholder="0.00"/>
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2">
                        <label class="control-label">Default Rate</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDefaultRate" runat="server" CssClass="form-control"/>
                    </div>
                </div>
            </div>
            <div class="form-group">
               <div class="col-sm-12">
                  <div class="col-sm-2">
                        <label class="control-label">Unit Descriptions</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtUnitDescription" runat="server" CssClass="form-control"/>
                    </div>
                   <div class="col-sm-1"></div>
                   <div class="col-sm-2">
                        <label class="control-label">Default VAT</label>

                   </div>
                   <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDefaultVAT" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                     <%--  <asp:RequiredFieldValidator ControlToValidate="ddlDefaultVAT" runat="server" ID="rfvddlDefaultVAT" ValidationGroup="CommType"
                        ErrorMessage="Select Default VAT" Text="Select  Default VAT" Display="Dynamic" ForeColor="Red" InitialValue="-1"/>--%>
                   </div>
               </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                  <div class="col-sm-2">
                      <label class="control-label">Other</label>
                  </div>
                  <div class="col-sm-3">
                      <asp:CheckBox ID="ChkNonTravelFee" runat="server"/>
                      <label class="control-label">Non Travel Fee</label>
                  </div>
                  <div class="col-sm-1"></div>
                  <div class="col-sm-2">
                     <label class="control-label">Zero Units Type?(<span class="style1">*</span>)</label>
                  </div>
                  <div class="col-sm-3" >
                     <asp:DropDownList ID="ddlZeroUnitsType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-" Value="-1" />
                        <asp:ListItem Text="Yes" Value="1"/>
                        <asp:ListItem Text="No" Value="0"/>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlZeroUnitsType" runat="server" ID="rfvddlZeroUnitsType" ValidationGroup="CommType"
                        ErrorMessage="Select Zero Units Type" Text="Select Zero Units Type" Display="Dynamic" ForeColor="Red" InitialValue="-1"/>
                </div>
                </div>
           </div>
           <div class="col-sm-12"><h5><b>General Ledger Codes</b></h5></div>
           <div class="form-group">
              <div class="col-sm-12">
                <div class="col-sm-2">
                    <label class="control-label">Income/Charges</label>
                </div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlIncomeCharges" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-" Value="0" />
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ControlToValidate="ddlIncomeCharges" runat="server" ID="rfvddlIncomeCharges" ValidationGroup="CommType"
                        ErrorMessage="Select Income/Charges" Text="Select Income/Charges" Display="Dynamic" ForeColor="Red" InitialValue="-1"/>--%>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-2">
                    <label class="control-label">Output VAT</label>
                </div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlOutputVAT" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-" Value="0" />
                    </asp:DropDownList>
                   <%-- <asp:RequiredFieldValidator ControlToValidate="ddlOutputVAT" runat="server" ID="rfvddlOutputVAT" ValidationGroup="CommType"
                        ErrorMessage="Select Output VAT" Text="Select Output VAT" Display="Dynamic" ForeColor="Red" InitialValue="-1"/>--%>
                </div>
              </div>
           </div>
           <div class="form-group">
            <div class="col-sm-5">
            </div>
            <div class="col-sm-3">
                <asp:Button runat="server" ID="btnSubmit" class="btn btn-success" ValidationGroup="CommType"
                    Text="Submit" 
                        UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                    OnClick="btnSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;<asp:Button runat="server" ID="btnReset"
                            class="btn btn-primary green" Text="Reset" OnClick="btnReset_Click" />

            </div>
        </div>
         
       </div>
    </section>
</asp:Content>

