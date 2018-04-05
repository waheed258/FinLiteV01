<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ClientLevelReport.aspx.cs" Inherits="Admin_ClientLevelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />
    <style>
        .imgright {
            Width: 50px;
            Height: 30px;
            margin-left: 95%;
             margin-top:-50px;
        }
    </style>
    <%--<script type="text/javascript">
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
          //  $('#ContentPlaceHolder1_txtFromDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
            $("#ContentPlaceHolder1_txtFromDate").datepicker({
               // format: 'yyyy-mm-dd',
                endDate: '0d',
                autoclose: true
            }).attr('readonly', 'false');;

          //  $('#ContentPlaceHolder1_txtToDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
            $("#ContentPlaceHolder1_txtToDate").datepicker({
               // format: 'yyyy-mm-dd',
                endDate: '0d',
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

    <%--<asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="PDF" OnClientClick = "SetTarget();"/>--%>

    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Client level Report</h2>
        </header>
        <div class="panel-body">

            <div class="row">
                <div class="col-sm-3">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd" BackColor="White"></asp:TextBox>

                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd" BackColor="White"></asp:TextBox>

                </div>
                <div class="col-sm-3">
                    <asp:Button ID="btnReportSubmit" runat="server" CssClass="btn btn-primary" Text="submit" OnClick="btnReportSubmit_Click" />

                </div>

            </div>
                <asp:ImageButton ID="btnPdf" runat="server" OnClick="btnPdf_Click" ToolTip="Pdf" OnClientClick="SetTarget();" ImageUrl="~/images/PdfIcon.png" CssClass="imgright" />
            <br />
            <asp:GridView ID="gvClientLevelReport" runat="server" AllowPaging="true" Width="100%" PageSize="10" OnPageIndexChanging="gvClientLevelReport_PageIndexChanging"
                AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                ShowHeaderWhenEmpty="true">
                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                <Columns>

                   <%-- <asp:TemplateField HeaderText="Trans Date">
                        <ItemTemplate>
                            <%#Eval("TransDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Client Name">
                        <ItemTemplate>
                            <%#Eval("ClientName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ClientType AccCode">
                        <ItemTemplate>
                            <%#Eval("ClientTypeAccCode")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Total" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("InvoiceTotal")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice OpenAmount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("InvoiceOpenAmount")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Invoice Paid Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("InvoicePaiedAmount")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Invoice OpenAmount">
                                <ItemTemplate>
                                    <%#Eval("InvoiceOpenAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                </Columns>
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
            </asp:GridView>

        </div>
    </section>

</asp:Content>

