<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="InvoiceLevelReport.aspx.cs" Inherits="Admin_InvoiceLevelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            DatePickerSet();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                DatePickerSet();

            });

        });

        function DatePickerSet() {
            $('#ContentPlaceHolder1_txtFromDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtFromDate").datepicker({

                format: 'yyyy-mm-dd',
                autoclose: true

            }).attr('readonly', 'false');;
            $('#ContentPlaceHolder1_txtToDate').val('<%=System.DateTime.Now.ToShortDateString()%>');
            $("#ContentPlaceHolder1_txtToDate").datepicker({

                format: 'yyyy-mm-dd',
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

            <h2 class="panel-title">Invoice level Report</h2>
        </header>
         <div class="panel-body">

             <div class="row">
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ></asp:TextBox>

                  </div>
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>

                  </div>
                  <div class="col-sm-3">
                    <asp:Button ID="btnInvoiceSubmit" CssClass="btn btn-primary" runat="server" Text="submit" OnClick="btnInvoiceSubmit_Click" />

                  </div>

             </div>
              <br />

              <asp:GridView ID="gvInvoiceLevelReport" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                        AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                         ShowHeaderWhenEmpty="true">
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                        <Columns>
                           <asp:TemplateField HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("PaidAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Recieved Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("RecievedAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="FromDate">
                                <ItemTemplate>
                                    <%#Eval("FromDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ToDate">
                                <ItemTemplate>
                                    <%#Eval("ToDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                        </Columns>
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>
           
         </div>
         
    </section>

</asp:Content>

