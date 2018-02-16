<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CommissionReport.aspx.cs" Inherits="CommissionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });
</script>

    <style type="text/css">
    .Grid td
    {
        background-color: #A1DCF2;
        color: black;
        font-size: 10pt;
        line-height: 200%;
         padding: 0px 0px 0px 5px;
    }
    .Grid th
    {
        background-color: #3AC0F2;
        color: White;
        font-size: 10pt;
        line-height: 200%;
        padding: 0px 0px 0px 5px;
    }
    .ChildGrid td
    {
        background-color: #eee !important;
        color: black;
        font-size: 10pt;
        line-height: 200%;
    }
    .ChildGrid th
    {
        background-color: #6C6C6C !important;
        color: White;
        font-size: 10pt;
        line-height: 200%;
        padding: 0px 0px 0px 5px;
    }
    .Nested_ChildGrid td
    {
        background-color: #fff !important;
        color: black;
        font-size: 10pt;
        line-height: 200%;
    }
    .Nested_ChildGrid th
    {
        background-color: #2B579A !important;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }
    .floatRight { float: right; }

    .leftColumn {
            width: 19%;
            vertical-align: top;
            float: left;
            /*border: solid 1px Black;*/
        }
        .rightColumn {
            width: 102%;
            vertical-align: top;
            float: right;
            display: inline-block;
            padding: 0px 0px 0px 5px;
           /*border: solid 1px Black;*/
        }

     

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="PDF"/>--%>

     <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>--%>
      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
          <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Commission Report</h2>
        </header>
         <div class="panel-body">
             <div  style="padding-left:20px;">
    <asp:GridView ID="gvCustomers" runat="server"  AutoGenerateColumns="false" CssClass="Grid"
    DataKeyNames="InvId" Width="85%" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid" ImageUrl="~/images/plus.png"
                    CommandArgument="Show"  Width="20px" Height="20px"/>
                
                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative" CssClass="floatRight">
                     <div class="leftColumn">
                         </div>
                    <div class="rightColumn">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" PageSize="5"
                        AllowPaging="true" OnPageIndexChanging="OnOrdersGrid_PageIndexChanging" CssClass="ChildGrid">
                        <Columns>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgProductsShow" runat="server" OnClick="Show_Hide_ProductsGrid" ImageUrl="~/images/plus.png"
                                        CommandArgument="Show" />
                                    <asp:Panel ID="pnlProducts" runat="server" Visible="false" Style="position: relative">
                                        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" PageSize="2"
                                            AllowPaging="true" OnPageIndexChanging="OnProductsGrid_PageIndexChanging" CssClass="Nested_ChildGrid">
                                            <Columns>
                                                   <asp:BoundField ItemStyle-Width="150px" DataField="TicketType" HeaderText="Ticket Type" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="CommiAmount" HeaderText="Commi Amount" />
                                                   <asp:BoundField ItemStyle-Width="150px" DataField="InvDocumentNo" HeaderText="Document No" />
                                                   
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField ItemStyle-Width="150px" DataField="TicketType" HeaderText="Ticket Type" />
                             <asp:BoundField ItemStyle-Width="150px" DataField="CommiAmount" HeaderText="Commi Amount" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField ItemStyle-Width="150px" DataField="InvDocumentNo" HeaderText="Document No" />
                             <asp:BoundField ItemStyle-Width="150px" DataField="ChartedAccName" HeaderText="Account Name" />
                        </Columns>
                    </asp:GridView>

                        </div>
                </asp:Panel>

            </ItemTemplate>
        </asp:TemplateField>


        <asp:BoundField ItemStyle-Width="150px" DataField="InvDocumentNo" HeaderText="Document No" />
        <asp:BoundField ItemStyle-Width="150px" DataField="TotalCommInclu" HeaderText="Commi Inclu" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField ItemStyle-Width="150px" DataField="TotalCommExclu" HeaderText="Commi Exclu" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField ItemStyle-Width="150px" DataField="TotalCommVatAmount" HeaderText="Commi VAT" ItemStyle-HorizontalAlign="Right"/>
             <%--<asp:BoundField ItemStyle-Width="150px" DataField="ChartedAccName" HeaderText="Account Name" />--%>
    </Columns>
</asp:GridView>

              </div>

         </div>
    </section>
            <%--</ContentTemplate>
          </asp:UpdatePanel>--%>
</asp:Content>

