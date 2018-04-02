<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MainAccountList.aspx.cs" Inherits="Admin_MainAccountList" %>

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

                        <h2 class="panel-title">Main Accounts</h2>
                    </header>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="mb-md">

                                    <asp:Button ID="btnMainAcAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnMainAcAdd_Click" />
                                </div>
                            </div>
                        </div>

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

                            &nbsp;

                <asp:HiddenField ID="hf_AirportId" runat="server" Value="0" />
                            <asp:GridView ID="gvMainAccList" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMainAccList_PageIndexChanging" OnSorting="gvMainAccList_Sorting"
                                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                Width="100%">

                                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                                <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <%#Eval("MainAccCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Main Account Name">
                                        <ItemTemplate>
                                            <%#Eval("MainAcName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Account Type">
                                        <ItemTemplate>
                                            <%#Eval("AcType")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Base Currency">
                                        <ItemTemplate>
                                            <%#Eval("BaseCurrency")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                </Columns>
                                <EmptyDataTemplate>
                                    <h4>
                                        <asp:Label ID="lblEmptyMessage" Text="" runat="server" /></h4>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
</asp:Content>

