<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AirSupplierLevelReport.aspx.cs" Inherits="Admin_AirSupplierLevelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .imgright {
            Width: 50px;
            Height: 30px;
            margin-left: 1100px;
        }
    </style>
    <%-- <script type="text/javascript">
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
             debugger;
          //   $('#ContentPlaceHolder1_txtFromDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
             $("#ContentPlaceHolder1_txtFromDate").datepicker({
               //  format: 'yyyy-mm-dd',
                 endDate: '0d',
                 autoclose: true
             }).attr('readonly', 'false');;
            // $('#ContentPlaceHolder1_txtToDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
             $("#ContentPlaceHolder1_txtToDate").datepicker({
              //   format: 'yyyy-mm-dd',
                // endDate: '0d',

                 autoclose: true
             }).attr('readonly', 'false');;
         }
         </script>--%>
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("#ContentPlaceHolder1_txtFromDate").datepicker({
                maxDate: '0',
                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                onClose: function (selectedDate) {
                    $("#ContentPlaceHolder1_txtToDate").datepicker("option", "minDate", selectedDate);
                }
            }).attr('readonly', 'false');;

            $("#ContentPlaceHolder1_txtToDate").datepicker({
                maxDate: '0',
                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                onClose: function (selectedDate) {
                    $("#ContentPlaceHolder1_txtFromDate").datepicker("option", "maxDate", selectedDate);
                }
            }).attr('readonly', 'false');;
        });
        function SetTarget() {

            document.forms[0].target = "_blank";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="PDF" OnClientClick="SetTarget(); " />--%>
     
        <asp:ImageButton ID="btnPdf" runat="server" OnClientClick="SetTarget();" OnClick="btnPdf_Click" ImageUrl="~/images/PdfIcon.png"  CssClass="imgright"  />
    

    <%--<asp:ImageButton ID="imgPdf" ImageUrl="~/images/PdfIcon.png" runat="server" Width="30" Height="20" OnClick="imgPdf_Click" title="Pdf" />--%>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>

    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">AirSupplier level Report</h2>
        </header>
        <div class="panel-body">

            <div class="row">
                <div class="col-sm-3">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd"></asp:TextBox>

                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd"></asp:TextBox>

                </div>
                <div class="col-sm-3">
                    <asp:Button ID="btnSuppliSubmit" runat="server" CssClass="btn btn-primary" Text="submit" OnClick="btnSuppliSubmit_Click" />

                </div>

            </div>
            <br />

            <asp:GridView ID="gvAirSupplLevelReport" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                ShowHeaderWhenEmpty="true">
                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                <Columns>
                    <asp:TemplateField HeaderText="Trans Date">
                        <ItemTemplate>
                            <%#Eval("TransDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Supplier Name">
                        <ItemTemplate>
                            <%#Eval("SupplierName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("TicketAmount")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paied Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("SupPaiedAmoount")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Due Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("SupplierDueAmount")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
            </asp:GridView>

        </div>
    </section>
</asp:Content>

