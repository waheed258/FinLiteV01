<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="GeneralReceiptsList.aspx.cs" Inherits="Admin_GeneralReceiptsList" %>

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

            <h2 class="panel-title">General Receipts List</h2>
        </header>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6">
                    <div class="mb-md">
                        
                        <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click"  />
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

                <asp:GridView ID="gvGeneralReceiptsList" runat="server" EmptyDataText="No Data Found" AllowPaging="true" Width="100%" PageSize="10" OnPageIndexChanging="gvGeneralReceiptsList_PageIndexChanging" OnSorting="gvGeneralReceiptsList_Sorting"
                        AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered">
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1"/>
                        <Columns>
                           <asp:TemplateField HeaderText="Receipt Date">
                                <ItemTemplate>
                                    <%#Eval("GRPaymentDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Main Account">
                                <ItemTemplate>
                                    <%#Eval("GRSupplierMainAccCode")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Supplier Account">
                                <ItemTemplate>
                                    <%#Eval("GRSupplierFromAccCode")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bank">
                                <ItemTemplate>
                                    <%#Eval("BankName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("GRPaymentAmount")%>
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

