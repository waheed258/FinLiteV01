<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Airports.aspx.cs" Inherits="Admin_Airports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
             $('#<%= dropCountry.ClientID %>').select2();
             $('#<%= dropState.ClientID %>').select2();
             $('#<%= dropCity.ClientID %>').select2();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_AirportId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Airport</h2>
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
                                <asp:TextBox ID="txtAirKey" runat="server" CssClass="form-control" MaxLength="3" />
                                <asp:RequiredFieldValidator ControlToValidate="txtAirKey" runat="server" ID="rfvtxtAirKey" ValidationGroup="airport"
                                    ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-3">
                                <asp:CheckBox ID="chkDeactivate" runat="server" />
                                <label>Deactivate</label>
                            </div>
                           </div>
                        </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Name(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtAirportName" runat="server" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ControlToValidate="txtAirportName" runat="server" ID="rfvtxtAirportName" ValidationGroup="airport"
                                    ErrorMessage="Enter Name" Text="Enter Name" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtAirportName" runat="server" ForeColor="Red"
                            ID="revtxtAirportName" ValidationGroup="airport" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                            </div>
                           
                           <div class="col-sm-1"></div>
                     
                            <div class="col-sm-2">
                                <label class="control-label">Country (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" 
                                    OnSelectedIndexChanged="dropCountry_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCountry" runat="server" ID="rfvdropCountry" ValidationGroup="airport"
                                    ErrorMessage="Select Country" Text="Select Country" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                               
                            </div>
                            </div>
                            </div>

                       <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Province (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropState" runat="server" CssClass="form-control" AppendDataBoundItems="true" 
                                    OnSelectedIndexChanged="dropState_SelectedIndexChanged" AutoPostBack="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropState" runat="server" ID="rfvdropState" ValidationGroup="airport"
                                    ErrorMessage="Select State" Text="Select State" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                               
                                </div>
                             <div class="col-sm-1"></div>
                         <div class="col-sm-2">
                              <label class="control-label">City (<span class="style1">*</span>)</label>

                         </div>
                           <div class="col-sm-3">
                                <asp:DropDownList ID="dropCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCity" runat="server" ID="rfvdropCity" ValidationGroup="airport"
                                    ErrorMessage="Select City" Text="Select City" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                               
                                </div>
                          
                            </div>
                        </div>
                    
                    <div class="form-group">
                        <div class="col-sm-12">
                         
                           <div class="col-sm-6">
                                <asp:CheckBox ID="chkCountryDetails" runat="server" />
                               <label>Exclude Country Details from Extended Routes?</label>
                               
                            </div>
                            </div>
                        </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group"></div>
            <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" CssClass="btn btn-success" ValidationGroup="airport"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                                                OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="cmdSubmit_Click"/>&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click"/>

                </div>
            </div>


        </div>
    </section>
</asp:Content>

