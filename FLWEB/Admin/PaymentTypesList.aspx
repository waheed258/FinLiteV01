<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="PaymentTypesList.aspx.cs" Inherits="Admin_PaymentTypesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelSearch" runat="server" DefaultButton="imgsearch">
        <asp:UpdatePanel ID="UpdateSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <section class="panel">
                    <header class="panel-heading">
                        <div class="panel-actions">
                            <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                        </div>

                        <h2 class="panel-title">Payment Types</h2>
                    </header>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="mb-md">

                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="paneldiv-border">
                                <div class="form-group">
                                    <div class="col-sm-12">&nbsp</div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DropPage" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropPage_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">
                                                Records per page</label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-1">
                                            <label class="control-label">Search</label>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"> </asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:ImageButton ID="imgsearch" runat="server" ImageUrl="../images/icon-search.png" Height="25" Width="25" OnClick="imgsearch_Click" />
                                                </span>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>

                        <asp:HiddenField ID="hf_PaymentId" runat="server" Value="0" />
                        <asp:GridView ID="gvPaymentTypesList" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                            AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                            OnRowCommand="gvPaymentTypesList_RowCommand" OnPageIndexChanging="gvPaymentTypesList_PageIndexChanging" OnSorting="gvPaymentTypesList_Sorting" ShowHeaderWhenEmpty="true">
                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                            <Columns>

                                <asp:TemplateField HeaderText="Key">
                                    <ItemTemplate>
                                        <%#Eval("PaymentCode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <%#Eval("PaymentName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Default Payment">
                                    <ItemTemplate>
                                        <%#Eval("Paymentdesc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <table>
                                            <tr>

                                                <td>
                                                    <asp:ImageButton ID="imgEdit" ToolTip="Edit" runat="server" ImageUrl="../images/icon-edit.png" Height="20" Width="20"
                                                        CommandName="Edit PaymentTypes Details" CommandArgument='<%#Eval("PaymentId") %>' />
                                                    <asp:ImageButton ID="imgDelete" ToolTip="Delete" runat="server" ImageUrl="../images/icon-delete.png" Height="20" Width="20"
                                                        CommandName="Delete PaymentTypes Details" CommandArgument='<%#Eval("PaymentId") %>' OnClientClick="javascript:return confirm('Are You Sure To Delete PaymentTypes Details')" />
                                                </td>
                                            </tr>
                                        </table>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <h4>
                                    <asp:Label ID="lblEmptyMessage" Text="" runat="server" /></h4>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

