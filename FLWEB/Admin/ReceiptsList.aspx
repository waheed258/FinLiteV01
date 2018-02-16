<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ReceiptsList.aspx.cs" Inherits="Admin_ReceiptsList" %>

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

            <h2 class="panel-title">Receipt Register</h2>
        </header>
        <div class="panel-body">
               <div class="row">
                         <div class="col-sm-6">
                           <div class="mb-md">
                              <asp:Button  ID="btnReciptAdd" runat="server" Class="btn btn-primary" Text="Add" OnClick="btnReciptAdd_Click" />
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

           <asp:HiddenField ID="hf_branchid" runat="server" Value="0" />
                   
            <asp:GridView ID="gvReceiptList" runat="server" AllowPaging="true" Width="100%" PageSize="10" EmptyDataText="No Data Found" OnPageIndexChanging="gvReceiptList_PageIndexChanging" OnSorting="gvReceiptList_Sorting"
                        AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                       ShowHeaderWhenEmpty="true">
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                        <Columns>
                           <asp:TemplateField HeaderText="Reciept Type">
                                <ItemTemplate>
                                    <%#Eval("RecieptType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Type">
                                <ItemTemplate>
                                    <%#Eval("ClientType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Name">
                                <ItemTemplate>
                                    <%#Eval("clientName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("ReceiptAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Allocated Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("AllocatedAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                       <%--     <asp:TemplateField HeaderText="Receipt Balance Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("ReceiptBalanceAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                             <asp:TemplateField HeaderText="Invoice Due Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("InvoiceBalanceAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Invoice Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("InvoiceTotalAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Invoice No">
                                <ItemTemplate>
                                    <%#Eval("InvDocumentNo")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>
        </div>
    </section>
</asp:Content>

