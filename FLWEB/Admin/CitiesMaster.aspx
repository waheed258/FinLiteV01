<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CitiesMaster.aspx.cs" Inherits="Admin_CitiesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_CitiesId" runat="server" Value="0" />

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New City</h2>
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
                                <asp:TextBox ID="txtCityKey" runat="server" CssClass="form-control" MaxLength="5" />
                                <asp:RequiredFieldValidator ControlToValidate="txtCityKey" runat="server" ID="rfvtxtCityKey" ValidationGroup="city"
                                    ErrorMessage="Enter Key" Text="Enter Key" class="validationred" Display="Dynamic" ForeColor="Red" />

                            </div>
                            <div class="col-sm-1"></div>
                           
                            <div class="col-sm-2">
                                <label class="control-label">Description(<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="50" />
                                <asp:RequiredFieldValidator ControlToValidate="txtDescription" runat="server" ID="rfvtxtDescription" ValidationGroup="city"
                                    ErrorMessage="Enter Description" Text="Enter Description" class="validationred" Display="Dynamic" ForeColor="Red" />
                                 <asp:RegularExpressionValidator ControlToValidate="txtDescription" runat="server" ForeColor="Red"
                            ID="revtxtDescription" ValidationGroup="city" ErrorMessage="Enter Only Characters."
                            Text="Enter Only Characters." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic"></asp:RegularExpressionValidator>

                            </div>
                           
                            </div>
                        </div>
                      <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">Country (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dropCountry_SelectedIndexChanged">
                                    <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropCountry" runat="server" ID="rfvdropCountry" ValidationGroup="city"
                                    ErrorMessage="Select Country" Text="Select Country" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                               
                            </div>
                      <div class="col-sm-1"></div>
                            <div class="col-sm-2">
                                <label class="control-label">State (<span class="style1">*</span>)</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="dropState" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropState" runat="server" ID="rfvdropState" ValidationGroup="city"
                                    ErrorMessage="Select State" Text="Select State" class="validationred" Display="Dynamic" ForeColor="Red" InitialValue="-1" />
                               
                                </div>
                            </div>
                          </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                           <div class="col-sm-2">
                                <label class="control-label">TimeZone UTC</label>
                            </div>
                            <div class="col-sm-3">
                                  <asp:TextBox ID="txtTimeZone" runat="server" CssClass="form-control" MaxLength="50" />
                               
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
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="city"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click"/>

                </div>
            </div>


        </div>
    </section>
</asp:Content>

