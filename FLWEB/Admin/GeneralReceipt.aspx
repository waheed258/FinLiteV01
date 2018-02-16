<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="GeneralReceipt.aspx.cs" Inherits="Admin_GeneralReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">

            <h2 class="panel-title">General Receipt</h2>
        </header>
        <div class="panel-body">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-3">

                        <asp:TextBox ID="txtGRDate" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label>
                            Source Ref
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGRSourceRef" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Prepared By
                        </label>
                    </div>
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtGRPreparedBy" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                     <%--   <asp:DropDownList ID="DDLGRPreparedby" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Inc Amount
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGRIncAmount" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Division
                        </label>
                    </div>
                    <div class="col-sm-3">

                        <asp:DropDownList ID="DDLGRDivision" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label>
                            VAT
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DDLGRVat" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>


            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Receipt Type
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DDLGRReceiptType" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label>
                            VAT Amount
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGRVatAmount" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Auto Deposit to
                        </label>
                    </div>
                    <div class="col-sm-3">

                        <asp:DropDownList ID="DDLGRAutoDeposit" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label>
                            Excl Amount
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGRExclAmount" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Account No
                        </label>
                    </div>
                    <div class="col-sm-3">

                        <asp:DropDownList ID="DDLGRAccountNo" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDLGRAccountNo_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <%-- <div class="col-sm-2">
                            <label class="control-label">
                               Client Total
                            </label>
                        </div>
                        <div class="col-sm-3">
                          
                            <asp:TextBox ID="txtACMClientTotal" runat="server" CssClass="form-control" MaxLength="50" />
                        
                    </div>--%>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Pay Details
                        </label>
                    </div>
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtPayDetails" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                       <%-- <asp:DropDownList ID="DDLGRPayDetails" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                    <%--     <div class="col-sm-1">
                        </div>--%>
                    <div class="col-sm-2">
                        <label class="control-label">
                            Details
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtGRDetails" runat="server" CssClass="form-control" Text="Payment- thank you" ReadOnly="true"></asp:TextBox>
                       <%-- <asp:DropDownList ID="DDLGRDetails" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-10">
                        <asp:CheckBox ID="Chk" runat="server" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                        <asp:Label runat="server" Font-Bold="true">Capture reporting flags against this general ledge income statement entry</asp:Label>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Division
                        </label>
                    </div>
                    <div class="col-sm-8">

                        <asp:DropDownList ID="DDLGRDivision1" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Consultant
                        </label>
                    </div>
                    <div class="col-sm-8">

                        <asp:DropDownList ID="DDLGRConsultant" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Client
                        </label>
                    </div>
                    <div class="col-sm-8">

                        <asp:DropDownList ID="DDLGRClient" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
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
                    <div class="col-sm-8">

                        <asp:DropDownList ID="DDLGRSupplier" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Service Type
                        </label>
                    </div>
                    <div class="col-sm-8">

                        <asp:DropDownList ID="DDLGRServiceType" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <asp:DropDownList ID="DDLGRMessageType" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-8">

                        <asp:TextBox ID="txtGRMessage" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>
                </div>
            </div>





            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-3">

                        <asp:Button ID="GenReceptSubmit" runat="server" OnClick="GenReceptSubmit_Click" class="btn btn-success" Text="Submit" />
                    </div>
                    <div class="col-sm-3">

                        <asp:Button ID="Cancel" runat="server" class="btn btn-danger" Text="Cancel" />
                    </div>
                    <div class="col-sm-3">

                        <asp:Button ID="Reset" runat="server" class="btn btn-info" Text="Reset" />
                    </div>

                </div>
            </div>
        </div>
    </section>
</asp:Content>

