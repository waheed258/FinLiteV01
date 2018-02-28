<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="BankAccountList.aspx.cs" Inherits="Admin_BankAccountList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Bank Accounts</h2>
        </header>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <div class="mb-md">
                        <asp:Button ID="btnAdd" runat="server" Class="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
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
            <asp:HiddenField ID="hf_BankAcId" runat="server" Value="0" />

            <asp:GridView ID="gvBankAccountList" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                OnRowCommand="gvBankAccountList_RowCommand" OnPageIndexChanging="gvBankAccountList_PageIndexChanging" OnSorting="gvBankAccountList_Sorting" ShowHeaderWhenEmpty="true">
                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                <Columns>
                    <asp:TemplateField HeaderText="Key">
                        <ItemTemplate>
                            <%#Eval("BankAcKey")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account Name">
                        <ItemTemplate>
                            <%#Eval("BankName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A/C Type">
                        <ItemTemplate>
                            <%#Eval("BankAcType")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A/C Number">
                        <ItemTemplate>
                            <%#Eval("BankAcNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate>
                            <%#Eval("BranchName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="	Account Holder">
                        <ItemTemplate>
                            <%#Eval("AccountHolder")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgEdit" ToolTip="Edit" runat="server" ImageUrl="~/images/icon-edit.png" Height="20" Width="20"
                                            CommandName="Edit Bank Details" CommandArgument='<%#Eval("BankAcId") %>' />
                                        <asp:ImageButton ID="imgDelete" ToolTip="Delete" runat="server" ImageUrl="~/images/icon-delete.png" Height="20" Width="20"
                                            CommandName="Delete Bank Details" CommandArgument='<%#Eval("BankAcId") %>' OnClientClick="javascript:return confirm('Are You Sure You Want To Delete Bank Account Details')" />
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
</asp:Content>

