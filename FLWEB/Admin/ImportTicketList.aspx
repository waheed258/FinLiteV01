<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ImportTicketList.aspx.cs" Inherits="Admin_ImportTicketList" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="css/pagging.css" rel="stylesheet" />
    <style>
        .invbtn {
            color: #ffffff;
            background-color: #0088cc;
            border-color: #0088cc;
            line-height: 1.42857143;
            border-radius: 4px;
            margin-bottom: 2px;
        }

                .hiddencol {
            display: none;
        }

    </style>
     <link href="css/pagging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Imported Ticket History</h2>
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
                            <asp:ImageButton ID="imgsearch" runat="server" ImageUrl="../images/icon-search.png" Height="25" Width="25" OnClick="imgsearch_Click" />
                            </span>
                        </div>
                    </div>
                </div>

               &nbsp;
     <asp:GridView ID="gvImportTickets" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-striped table-bordered"
         OnSorting="gvImportTickets_Sorting" OnPageIndexChanging="gvImportTickets_PageIndexChanging"
                ShowHeaderWhenEmpty="true">
                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                <Columns>
                    
                    <asp:TemplateField HeaderText="No" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" >
                        <ItemTemplate>
                            <%#Eval("ID")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pnr">
                        <ItemTemplate>
                            <%#Eval("PNR")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <%#Eval("Date")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="AirLine">
                        <ItemTemplate>
                            <%#Eval("Air Line")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Consultant">
                        <ItemTemplate>
                            <%#Eval("Consultant")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agency">
                        <ItemTemplate>
                            <%#Eval("Client")%>
                        </ItemTemplate>
                    </asp:TemplateField>
             <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Fare")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="VAT" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("VAT")%>
                        </ItemTemplate>
                    </asp:TemplateField>
           <%--  <asp:TemplateField HeaderText="Com%" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("T50RTE")%>
                        </ItemTemplate>
                    </asp:TemplateField>
             <asp:TemplateField HeaderText="Comm" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("T50COM")%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                     <asp:TemplateField HeaderText="Airport Taxes" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Airport Taxes")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BSP Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("BSP Amount")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Reference">
                        <ItemTemplate>
                            <%#Eval("Reference")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                     <asp:Button ID="btnAutoInvoice" Text="Invoice" CssClass="invbtn" runat="server" OnClick="btnAutoInvoice_Click" title="Invoice" />
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
            </asp:GridView>

         </div>

         </div>
</asp:Content>

