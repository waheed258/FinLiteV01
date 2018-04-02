<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="GeneralReceiptList.aspx.cs" Inherits="Admin_GeneralRececiptList" %>

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

                        <h2 class="panel-title">General Receipt Register</h2>
                    </header>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="mb-md">
                                    <asp:Button ID="btnGenReciptAdd" runat="server" Class="btn btn-primary" Text="Add" OnClick="btnGenReciptAdd_Click" />
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
                                                    <asp:ImageButton ID="imgsearch" runat="server" ImageUrl="~/images/icon-search.png" Height="25" Width="25" OnClick="imgsearch_Click" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- <asp:HiddenField ID="hf_branchid" runat="server" Value="0" />--%>

                        <asp:GridView ID="gvGenReceiptList" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                            AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                            ShowHeaderWhenEmpty="true" OnSorting="gvGenReceiptList_Sorting">
                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <%#Eval("Date")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Number">
                                    <ItemTemplate>
                                        <%#Eval("AccountNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <%#Eval("BankName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Type">
                                    <ItemTemplate>
                                        <%#Eval("ReceiptType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source Ref">
                                    <ItemTemplate>
                                        <%#Eval("SourceRef")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <%#Eval("IncAmount")%>
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

