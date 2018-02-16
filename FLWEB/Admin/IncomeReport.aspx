<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="IncomeReport.aspx.cs" Inherits="Admin_IncomeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            DatePickerSet();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                DatePickerSet();

            });

        });
        function SetTarget() {

            document.forms[0].target = "_blank";

        }

        function DatePickerSet() {
            $('#ContentPlaceHolder1_txtFromDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtFromDate").datepicker({
                format: 'yyyy-mm-dd',
                endDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;
            $('#ContentPlaceHolder1_txtToDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtToDate").datepicker({
                format: 'yyyy-mm-dd',
                endDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;
        }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
          <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Income Statement</h2>
        </header>
         <div class="panel-body">

             <%--<div class="row">
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd"></asp:TextBox>

                  </div>
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd"></asp:TextBox>

                  </div>
                  <div class="col-sm-3">
                    <asp:Button ID="btnReportSubmit" runat="server" CssClass="btn btn-primary" Text="submit" OnClick="btnReportSubmit_Click" />

                  </div>

             </div>--%>
                 <asp:ImageButton ID="imgPdf" ImageUrl="~/images/PdfIcon.png" runat="server" Width="30" Height="20" OnClick="imgPdf_Click"
                      title="Pdf" OnClientClick = "SetTarget();" />
                                
             <br />
             <asp:Literal id="ltrlctrl1" runat="server" />

             <%-- <asp:GridView ID="gvIncomeReport" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                        AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                         ShowHeaderWhenEmpty="true">
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                        <Columns>
                           <asp:TemplateField HeaderText="Income">
                                <ItemTemplate>
                                    <%#Eval("Name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="MainAc Name">
                                <ItemTemplate>
                                    <%#Eval("MainAcName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("Amount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                              

                           
                        </Columns>
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>--%>

         </div>
    </section>

</asp:Content>


