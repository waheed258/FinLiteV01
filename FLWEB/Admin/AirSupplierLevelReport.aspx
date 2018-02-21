<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AirSupplierLevelReport.aspx.cs" Inherits="Admin_AirSupplierLevelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="css/pagging.css" rel="stylesheet" />
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
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="PDF" OnClientClick = "SetTarget();"/>
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
                     <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd" ></asp:TextBox>

                  </div>
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd"></asp:TextBox>

                  </div>
                  <div class="col-sm-3">
                    <asp:Button ID="btnSuppliSubmit" runat="server" CssClass="btn btn-primary" Text="submit" OnClick="btnSuppliSubmit_Click" />
                     
                  </div>

             </div>
             <br />

              <asp:GridView ID="gvAirSupplLevelReport" runat="server" AllowPaging="true" Width="100%" PageSize="10" OnPageIndexChanging="gvAirSupplLevelReport_PageIndexChanging"
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
                             <asp:TemplateField HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right">
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

