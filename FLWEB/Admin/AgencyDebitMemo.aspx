<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AgencyDebitMemo.aspx.cs" Inherits="Admin_AgencyDebitMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="panel">
            <header class="panel-heading">

                <h2 class="panel-title">Agency Debit Memo</h2>
            </header>
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                                ADM No
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtADMNO" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                                Ticket No
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtADMTicketNO" runat="server" CssClass="form-control" MaxLength="50" />
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
                          
                             <asp:DropDownList ID="DDLADMSupplier" runat="server" CssClass="form-control" AutoPostBack="true">
                              
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
                          
                            <asp:DropDownList ID="DDLADMType" runat="server"  OnSelectedIndexChanged="DDLType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                              
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
                                Passenger Name
                            </label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtADMPassengerName" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
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
                          
                            <asp:TextBox ID="txtADMExclFire" Style="text-align: right"  OnTextChanged="txtADMExclFire_TextChanged" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="50" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                               Commission
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtADMCommission"  Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" OnTextChanged="txtADMCommission_TextChanged" AutoPostBack="true" />
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
                           <asp:DropDownList ID="DDLADMFarevat" runat="server"  CssClass="form-control" AutoPostBack="true" OnTextChanged="DDLADMFarevat_TextChanged" >
                              
                            </asp:DropDownList>
                           <%-- <asp:TextBox ID="txtFareVat" runat="server" CssClass="form-control" MaxLength="50" />--%>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                     <div class="col-sm-2">
                            <label>
                               Agent VAT
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtADMVat" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" OnTextChanged="txtADMVat_TextChanged" AutoPostBack="true" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <label class="control-label">
                              Airport Taxes
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtADMDepTaxes" OnTextChanged="txtADMDepTaxes_TextChanged" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" AutoPostBack="true" />
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label class="control-label">
                               Client Total
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtADMClientTotal" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" />
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
                          
                             <asp:DropDownList ID="DDLADMCreditCard" runat="server" CssClass="form-control" AutoPostBack="true">
                              
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtCrediCard" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" />--%>
                        </div>
                   <%--     <div class="col-sm-1">
                        </div>--%>
                        <div class="col-sm-2">
                            <label>
                                BSP
                            </label>
                        </div>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtADMBSP" Style="text-align: right" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                   
                    </div>
                </div>


                

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-3">

                            <asp:Button ID="ADMSubmit" runat="server" class="btn btn-success" OnClick="ADMSubmit_Click"  Text="Submit" />
                        </div>
                        <div class="col-sm-3">

                            <asp:Button ID="Cancel" runat="server" class="btn btn-danger" OnClick="Cancel_Click"  Text="Cancel" />
                        </div>
                        <div class="col-sm-3">

                            <asp:Button ID="Reset" runat="server" class="btn btn-info" OnClick="Reset_Click" Text="Reset" />
                        </div>

                    </div>
                </div>
                </div>
            </section>
</asp:Content>

