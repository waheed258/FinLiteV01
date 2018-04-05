<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="LandSupplierReport.aspx.cs" Inherits="Admin_LandSupplierReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
           //   $('#ContentPlaceHolder1_txtLandFromDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
              $("#ContentPlaceHolder1_txtLandFromDate").datepicker({
                 // format: 'yyyy-mm-dd',
                  endDate: '0d',
                  autoclose: true
              }).attr('readonly', 'false');;
             // $('#ContentPlaceHolder1_txtLandToDate').val('<%=System.DateTime.Now.ToString("yyyy/MM/dd")%>');
              $("#ContentPlaceHolder1_txtLandToDate").datepicker({
                  //format: 'yyyy-mm-dd',
                  endDate:'0d',
                  autoclose: true
              }).attr('readonly', 'false');;
          }
         </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#ContentPlaceHolder1_txtLandFromDate").datepicker({
                maxDate: '0',
                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                onClose: function (selectedDate) {
                    $("#ContentPlaceHolder1_txtLandToDate").datepicker("option", "minDate", selectedDate);
                }
            }).attr('readonly', 'false');;

            $("#ContentPlaceHolder1_txtLandToDate").datepicker({
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="PDF" OnClientClick = "SetTarget();"/>--%>
  
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
          <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>

            <h2 class="panel-title">Land Supplier level Report</h2>
        </header>
         <div class="panel-body">
             
             <div class="row">
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtLandFromDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd" BackColor="White" ></asp:TextBox>

                  </div>
                 <div class="col-sm-3">
                     <asp:TextBox ID="txtLandToDate" runat="server" CssClass="form-control" Placeholder="yyyy-mm-dd" BackColor="White" ></asp:TextBox>

                  </div>
                  <div class="col-sm-3">
                    <asp:Button ID="btnLandSupSubmit" runat="server" Text="submit" CssClass="btn btn-primary"  OnClick="btnLandSupSubmit_Click" />

                  </div>

             </div>
               <asp:ImageButton ID="btnPdf" ToolTip="Pdf" runat="server" OnClick="btnPdf_Click" OnClientClick="SetTarget();"  ImageUrl="~/images/PdfIcon.png"  CssClass="imgright"  />
             <br />
              <asp:GridView ID="gvLandSupReport" runat="server" AllowPaging="true" Width="100%" PageSize="10" OnPageIndexChanging="gvLandSupReport_PageIndexChanging"
                        AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                         ShowHeaderWhenEmpty="true">
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                        <Columns>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <%#Eval("LSupplierName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ticket Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%#Eval("TicketAmount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Right">
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

