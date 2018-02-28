<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="DepositTransactionList.aspx.cs" Inherits="Admin_DepositTransactionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Deposit Transaction</h2>
        </header>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <div class="mb-md">
                       
                        <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" />
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
                                <asp:ImageButton ID="imgsearch" runat="server" ImageUrl="../images/icon-search.png" Height="25" Width="25" OnClick="imgsearch_Click"/>
                            </span>
                        </div>
                    </div>
                </div>

               &nbsp;

                <asp:HiddenField ID="hf_SupplierId" runat="server" Value="0" />
                <asp:GridView ID="gvDepositTransactionList" runat="server" AllowPaging="true"  PageSize="10"
                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                    Width="100%" OnRowCommand="gvDepositTransactionList_RowCommand" OnPageIndexChanging="gvDepositTransactionList_PageIndexChanging" OnSorting="gvDepositTransactionList_Sorting">

                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                    <RowStyle CssClass="gradeA odd" />
                    <AlternatingRowStyle CssClass="gradeA even" />
                    <Columns>

                        <asp:TemplateField HeaderText="Deposit Date">
                            <ItemTemplate>
                                <%#Eval("DepositDate")%>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Deposit SourceRef">
                            <ItemTemplate>
                                <%#Eval("DepositSourceRef")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Receipt Type">
                            <ItemTemplate>
                                <%#Eval("RecDescription")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Client">
                            <ItemTemplate>
                                <%#Eval("Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <%--<asp:TemplateField HeaderText="Consultant">
                            <ItemTemplate>
                                <%#Eval("DepositConsultant")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Total DepositAmount" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("TotalDepositAmount")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                    </Columns>
                    <EmptyDataTemplate>
                          <h4><asp:Label ID = "lblEmptyMessage" Text="" runat="server" /></h4>  
                            </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>

