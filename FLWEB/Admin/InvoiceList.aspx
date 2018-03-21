<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="InvoiceList.aspx.cs" Inherits="Admin_InvoiceList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />

    <style type="text/css">

        .modalBackground {
            /*filter: alpha(opacity=90);
            opacity: 0.8;*/
            border: 1px solid;
            background: #eee;
            padding: 2px;
            color: black;
        } 

        .modalBackground1 {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .panel-header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .panel-body {
                min-height: 50px;
                line-height: 30px;
                text-align: left;
                font-weight: normal;
            }

            .modalPopup .footer {
                padding: 6px;
            }

        .hiddencol {
            display: none;
        }

        .overlay {
            position: fixed;
            z-index: 999;
            top: 400px;
            left: 600px;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .wrapper {
            text-align: center;
            position: relative;
        }

            .wrapper .line {
                border-bottom: 2px solid #cbc2c2;
                position: relative;
                width: 70px;
                top: 30px;
                float: right;
            }

        .AmtRgt {
            float: right;
        }
        /*.btn
        {
            width:20%;
            height:20%;
            border-bottom-left-radius:5px;
            border-bottom-right-radius:5px;
            
        }*/
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }

        $(function () {
            $("#txtSearch").autocomplete({
                source: function (request, response) {
                    var param = { empName: $('#txtSearch').val() };
                    $.ajax({
                        url: "InvoiceList.aspx/SearchItemFromList",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
            <section class="panel">

                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                    </div>
                    <h2 class="panel-title">Invoices</h2>
                </header>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="mb-md">
                                <asp:Button ID="btnAdd" runat="server" Class="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
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
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control" OnTextChanged="txtSearch_TextChanged" > </asp:TextBox>
                                    <span class="input-group-btn">&nbsp; &nbsp;
                                <asp:ImageButton ID="imgsearch" runat="server" ImageUrl="../images/icon-search.png" Height="25" Width="25" OnClick="imgsearch_Click" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        &nbsp;

                 <asp:UpdatePanel ID="updatepanelContacts" runat="server">
                     <ContentTemplate>
                         <asp:HiddenField ID="hf_InvId" runat="server" Value="0" />


                     </ContentTemplate>
                 </asp:UpdatePanel>

                    </div>
               

               
                    <asp:GridView runat="server" EmptyDataText="No Data Found" ID="gvInvoiceList" OnPageIndexChanging="gvInvoiceList_PageIndexChanging"
                        AllowPaging="true"
                        PageSize="10" OnRowCommand="gvInvoiceList_RowCommand" DataKeyNames="InvId,clientemail"
                        AutoGenerateColumns="false" Width="100%" Height="50%"
                        CssClass="table table-striped table-bordered" OnSorting="gvInvoiceList_Sorting">
                        <RowStyle />
                        <FooterStyle Font-Bold="True" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle Font-Bold="True" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                        <%--<PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                    <RowStyle CssClass="gradeA odd" />
                    <AlternatingRowStyle CssClass="gradeA even" />--%>
                        <Columns>
                            <asp:BoundField DataField="InvId" HeaderText="InvId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="InvDate" HeaderText="Invoice Date" />
                            <asp:BoundField DataField="clientemail" HeaderText="Client Email" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="clientname" HeaderText="Client Name" />
                            <asp:BoundField DataField="consultantName" HeaderText="Consultant Name" />
                            <asp:BoundField DataField="InvOrder" HeaderText="Invoice Order" />
                            <asp:BoundField DataField="InvoiceTotal" HeaderText="Invoice Total" ItemStyle-HorizontalAlign="Right" />

                            <asp:TemplateField HeaderText="Sending Options">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSendMail" ImageUrl="~/images/icon-email.png" runat="server" Width="30" Height="20" OnClick="imgSendMail_Click" title="Mail" />
                                    <asp:ImageButton ID="imgPdf" ImageUrl="~/images/PdfIcon.png" runat="server" Width="30" Height="20" OnClick="imgPdf_Click" title="Pdf" />
                                    <asp:ImageButton ID="imgAccounting" ImageUrl="~/images/Money.png" runat="server" Width="30" Height="20" OnClick="imgAccounting_Click" title="Account Analysis" />

                                    <asp:ImageButton ID="imgEditInvoice" ToolTip="Edit" runat="server" ImageUrl="~/images/icon-edit.png" Height="20" Width="20"
                                        CommandName="Edit Invoice" CommandArgument='<%#Eval("InvId")%>' title="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                          <EmptyDataTemplate>
                          <h4><asp:Label ID = "lblEmptyMessage" Text="" runat="server" /></h4>  
                            </EmptyDataTemplate>
                    </asp:GridView>




                    <asp:Label ID="lblresult" runat="server" />
                    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                  <%--  <asp:ModalPopupExtender ID="SendMailPopupExtender" runat="server" TargetControlID="btnShowPopup"
                        PopupControlID="pnlpopup"
                        CancelControlID="btnCancel" BackgroundCssClass="popupextndrBg">
                    </asp:ModalPopupExtender>--%>

                      <asp:ModalPopupExtender ID="SendMailPopupExtender" runat="server" PopupControlID="pnlpopup"
                TargetControlID="btnShowPopup" BackgroundCssClass="modalBackground1" BehaviorID="SendMailPopupExtender"
                CancelControlID="btnCancel">
            </asp:ModalPopupExtender>

                    <asp:Panel ID="pnlpopup" runat="server" CssClass="modalPopup" Style="width: 600px; padding: 5px; min-height: 80px; display: none;">
                   <%-- <asp:Panel ID="pnlpopup" runat="server" BorderColor="#003300" BorderWidth="1" CssClass="modalBackground" Height="50%" Width="30%" Style="display: none">--%>
                        <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                            <tr style="background-color: #D55500">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">User Details</td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 45%">UserId:
                                </td>
                                <td>
                                    <asp:Label ID="lblID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="SendMail" CommandName="Send" runat="server" Text="Send" OnClick="btnSendmailSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                </td>
                            </tr>
                        </table>--%>
                        <br />
                         <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        <%--Invoice No--%>
                                    </label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="lblID" runat="server" CssClass="form-control" Style="display: none"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        From
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Subject 
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        To
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvmail" runat="server" ValidationGroup="emailGrp" ControlToValidate="txtTo" ErrorMessage="Please Enter Email ID" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revmail" runat="server" ForeColor="Red" ValidationGroup="emailGrp" ControlToValidate="txtTo" ErrorMessage="Please Enter Email ID" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Body
                                    </label>
                                </div>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtBody" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Attach file
                                    </label>
                                </div>

                                <div class="col-sm-10">
                                    <asp:FileUpload ID="fuattachment" runat="server" />
                                </div>

                            </div>
                            <br />
                            <br />
                            <div class="col-sm-12">
                                <div class="col-sm-3"></div>
                                <div class="col-sm-2">
                                    <asp:Button ID="SendMail" CommandName="Send" ValidationGroup="emailGrp" OnClientClick="showProgress()" CssClass="btn btn-info" runat="server" Text="Send" OnClick="btnSendmailSubmit_Click" />

                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-info" Text="Cancel" />
                                </div>
                            </div>

                        </div>

                        <div class="overlay">
                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                                <ProgressTemplate>
                                    <img src="../images/loading.gif" alt="" height="40" width="40" />
                                    <br />
                                    <h4>Please wait....</h4>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>



                             </div>

                    </asp:Panel>


                    <%-- Accounting Analaysis --%>

                    <asp:Button ID="btnShowAc" runat="server" Style="display: none" />
                   <%-- <asp:ModalPopupExtender ID="AccountAnalysisPopupExtender" runat="server" TargetControlID="btnShowAc"
                        PopupControlID="AcAnalysisPanel"
                        CancelControlID="btnCancel" BackgroundCssClass="popupextndrBg">
                    </asp:ModalPopupExtender>--%>

             <asp:ModalPopupExtender ID="AccountAnalysisPopupExtender" runat="server" PopupControlID="AcAnalysisPanel"
                TargetControlID="btnShowAc" BackgroundCssClass="modalBackground1" BehaviorID="AccountAnalysisPopupExtender"
                CancelControlID="btnCancel">
            </asp:ModalPopupExtender>

                    <%-- <asp:Panel ID="AcAnalysisPanel" runat="server" BorderColor="#003300" BorderWidth="1" CssClass="modalBackground" Height="70%" Width="50%" Style="display: none">--%>
                    <asp:Panel ID="AcAnalysisPanel" runat="server" CssClass="modalPopup" Style="width: 600px; padding: 5px; min-height: 80px; display: none;">
                        <%--<header class="panel-heading">
                            <div style="padding-top: 3px; padding-right: 3px;">
                                <asp:ImageButton ID="ImageButton5" CssClass="btncancle" runat="server" Height="20" Width="25" ImageUrl="~/images/close.png" OnClick="cmdClose_Click" />
                                <h4 class="panel-title">Accounting Analysis</h4>
                            </div>
                        </header>--%>

                        <div class="panel-header">
                            Accounting Analysis
                        </div>
                         <div class="panel-body">
                        <%--<div style="width: 100%; height: 100px;">
                            <div style="width: 40%; height: 100px">

                                <div class="col-sm-12">
                                    <div class="col-sm-12">
                                        <asp:Label ID="lblAcanlysisclientname" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <br />
                                <br />

                                <div style="width: 60%; height: 50px">


                                    <div style="position: absolute; right: -1280px;">

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                Total Fare
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Label ID="lblAcanAlysisTotalfare" runat="server" ReadOnly="true" />

                                            </div>
                                            <div class="col-sm-5"></div>
                                        </div>

                                        <div class="col-sm-12">


                                            <div class="col-sm-2">
                                                Less charged to credit card
                                            </div>

                                            <div class="col-sm-1">
                                                <div class="wrapper">
                                                    <div class="line"></div>
                                                    <asp:Label ID="lblAcAnalysisCC" runat="server" ReadOnly="true" />
                                                </div>

                                            </div>
                                        </div>


                                        <div class="col-sm-12">

                                            <div class="col-sm-2">
                                                <asp:Label Text="Credit/Decrease A/C Receivable" runat="server" ID="lblacReceivable"></asp:Label>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:Label ID="lblincrordecreAccRceivable" runat="server" ReadOnly="true" Style="text-align: right;" />
                                            </div>

                                        </div>


                                        <div class="col-sm-12" style="padding-top: 10px">
                                            <div class="col-sm-2">
                                                <p style="color: red">Credit/Increase Comm. Income</p>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Label ID="lblAcAnalcommincome" runat="server" ReadOnly="true" Style="text-align: right; color: red" />
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                Vat Liability
                                            </div>
                                            <div class="col-sm-1">
                                                <div class="wrapper">
                                                    <div class="line"></div>
                                                    <asp:Label ID="lblAcAnaliability" runat="server" ReadOnly="true" Style="text-align: right;" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="col-sm-2">
                                                Debit/Decrease A/C Payable
                                            </div>
                                            <div class="col-sm-1">

                                                <asp:Label ID="lblAcAnalPayable" runat="server" ReadOnly="true" Style="text-align: right;" />
                                            </div>

                                        </div>

                                    </div>



                                    <div style="border: 1px solid #ccc; padding-left: 5px; padding-right: 5px">

                                        <asp:GridView runat="server" ID="gvAccountanalysis" AutoGenerateColumns="false" Width="50%" Height="50%"
                                            CssClass="table table-striped table-bordered">
                                            <RowStyle />
                                            <FooterStyle Font-Bold="false" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                                            <Columns>

                                                <asp:BoundField DataField="AirPassenger" HeaderText="Passengers" />


                                            </Columns>
                                        </asp:GridView>

                                    </div>


                                </div>


                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                            <div style="width: 100%; height: 100%">
                                <asp:GridView ID="gvAccAnalysisDetails" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                                    AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                                    OnPageIndexChanging="gvAccAnalysisDetails_PageIndexChanging" ShowHeaderWhenEmpty="true">
                                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Principals">
                                            <ItemTemplate>
                                                <%#Eval("Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tot Charge">
                                            <ItemTemplate>
                                                <%#Eval("ClientTot")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comm">
                                            <ItemTemplate>
                                                <%#Eval("CommExclu")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CommVat">
                                            <ItemTemplate>
                                                <%#Eval("Commivatamt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                </asp:GridView>
                            </div>


                            <div style="float: right; padding-top: 25px; padding-left: 8px; margin-bottom: 10px;">

                                <asp:Button runat="server" ID="btnCancleACAnalysis"
                                    ValidationGroup="" Text="Close" OnClick="btnCancleACAnalysis_Click" />
                            </div>


                        </div>--%>
                        <div class="col-md-12" style="width:111% !important">
                            <div class="col-md-12" >
                                <asp:Label ID="lblAcanlysisclientname" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4" style="border-style: groove">
                                    <div style="height: 130px; overflow: auto;">
                                        <asp:GridView runat="server" ID="gvAccountanalysis" AutoGenerateColumns="false"
                                            CssClass="table table-striped table-bordered">
                                            <RowStyle />
                                            <FooterStyle Font-Bold="false" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                                            <Columns>
                                                <asp:BoundField DataField="AirPassenger" HeaderText="Passengers" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                               <%-- <div class="col-md-1">
                                </div>--%>

                                <div class="col-md-8" >
                                    <div class="col-sm-10">
                                        Total Fare
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblAcanAlysisTotalfare" runat="server" ReadOnly="true" CssClass="AmtRgt" />

                                    </div>


                                    <div class="col-sm-10">
                                        Less charged to credit card/Cash
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="wrapper">
                                            <div class="line"></div>
                                            <asp:Label ID="lblAcAnalysisCC" runat="server" ReadOnly="true" CssClass="AmtRgt" />
                                        </div>

                                    </div>
                                    <div class="col-sm-10">
                                        <asp:Label Text="Credit/Decrease A/C Receivable" runat="server" ID="lblacReceivable"></asp:Label>
                                    </div>

                                    <div class="col-sm-2">
                                        <asp:Label ID="lblincrordecreAccRceivable" runat="server" ReadOnly="true" CssClass="AmtRgt" />
                                    </div>

                                    <div class="col-sm-10">
                                        <p style="color: red">Credit/Increase Comm. Income</p>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblAcAnalcommincome" runat="server" ReadOnly="true" Style="text-align: right; color: red" CssClass="AmtRgt" />
                                    </div>
                                    <div class="col-sm-6">
                                        Vat Liability
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="wrapper">
                                            <div class="line"></div>
                                               <div class="col-sm-6">
                                            <asp:Label ID="lblAcAnaliability" runat="server" ReadOnly="true" CssClass="AmtRgt" />
                                                   </div>
                                          <%--  <div class="col-sm-1" ></div>--%>
                                            <div class="col-sm-5" <%--style="padding-right:10px;"--%> >
                                                <asp:Label ID="lblAccVatAmount" runat="server" ReadOnly="true"  />
                                                </div>
                                        </div>
                                    </div>

                                    
                                    <%--  <div class="col-sm-10">
                                        Vat Amount
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="wrapper">
                                            <div class="line"></div>
                                           

                                                <asp:Label ID="lblAccVatAmount" runat="server" ReadOnly="true" CssClass="AmtRgt" />

                                        </div>
                                    </div>--%>

                                    <div class="col-sm-10">
                                        Debit/Decrease A/C Payable
                                    </div>
                                    <div class="col-sm-2">

                                        <asp:Label ID="lblAcAnalPayable" runat="server" ReadOnly="true" CssClass="AmtRgt" />
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-12">
                            <br />
                        </div>
                        <div class="col-md-12">
                            <div style="height: 130px; overflow: auto;">
                                <asp:GridView ID="gvAccAnalysisDetails" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                                     ShowHeaderWhenEmpty="true">
                                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Principals">
                                            <ItemTemplate>
                                                <%#Eval("Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Amount" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("ClientTot")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comm" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("CommExclu")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CommVat" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%#Eval("Commivatamt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <br />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnCancleACAnalysis"
                                    ValidationGroup="" CssClass="btn btn-primary" Text="Close" OnClick="btnCancleACAnalysis_Click" />
                            </div>
                        </div>
                             </div>
                    </asp:Panel>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="SendMail" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

