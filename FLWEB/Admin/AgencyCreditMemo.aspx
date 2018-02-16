<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AgencyCreditMemo.aspx.cs" Inherits="Admin_AgencyCreditMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="panel">
            <header class="panel-heading">

                <h2 class="panel-title">Agency Credit Memo</h2>
            </header>
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                                ACM No
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMNO" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                                Ticket No
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtACMTicketNO" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                               Supplier
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                             <asp:DropDownList ID="DDLACMSupplier" runat="server" CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                          <div class="col-sm-2">
                            <label class="control-label">
                               Type
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:DropDownList ID="DDLACMType" OnSelectedIndexChanged="DDLACMType_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <%--<div class="col-sm-2">
                            <label class="control-label">
                               Type
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:DropDownList ID="DDLType" runat="server" CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>
                        </div>--%>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                                Pax Name
                            </label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtACMPassengerName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                           <%--<asp:DropDownList ID="DDLPassengerName" runat="server" CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                                Exclusive Fire
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMExclFire" runat="server" OnTextChanged="txtACMExclFire_TextChanged" CssClass="form-control" MaxLength="50" AutoPostBack="true"/>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                               Commission
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtACMCommission" runat="server" OnTextChanged="txtACMCommission_TextChanged" CssClass="form-control" MaxLength="50" AutoPostBack="true" />
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                               Fare Vat
                            </label>
                        </div>
                        <div class="col-sm-3">
                          <asp:DropDownList ID="DDLACMFarevat" runat="server"  CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>
                           <%-- <asp:TextBox ID="txtFareVat" runat="server" CssClass="form-control" MaxLength="50" />--%>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                     <div class="col-sm-2">
                            <label>
                                VAT
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtACMagentVat" runat="server" OnTextChanged="txtACMagentVat_TextChanged" CssClass="form-control" MaxLength="50" AutoPostBack="true" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                              Departure Taxes
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMDepTaxes" OnTextChanged="txtACMDepTaxes_TextChanged" runat="server" CssClass="form-control" MaxLength="50" AutoPostBack="true" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                                BSP
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtACMBSP" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                      <div class="col-sm-2">
                            <label class="control-label">
                              CreditCard
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMCrediCard" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                            <label class="control-label">
                               Client Total
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMClientTotal" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-3">

                            <asp:Button ID="ACMSubmit" runat="server" OnClick="ACMSubmit_Click" class="btn btn-success"  Text="Submit" />
                        </div>
                        <div class="col-sm-3">

                            <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" class="btn btn-danger"   Text="Cancel" />
                        </div>
                        <div class="col-sm-3">

                            <asp:Button ID="Reset" runat="server" OnClick="Reset_Click" class="btn btn-info"  Text="Reset" />
                        </div>

                    </div>
                </div>
                </div>
            </section>
</asp:Content>

