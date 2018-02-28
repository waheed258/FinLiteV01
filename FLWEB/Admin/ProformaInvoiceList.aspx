<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ProformaInvoiceList.aspx.cs" Inherits="Admin_ProformaInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />
    <style>
        .modalBackground {
            /*filter: alpha(opacity=90);
            opacity: 0.8;*/
            border: 1px solid;
            background: #eee;
            padding: 2px;
            color: black;
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
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
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

                    <h2 class="panel-title">Proforma Invoices</h2>
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
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"> </asp:TextBox>
                                    <span class="input-group-btn">&nbsp; &nbsp;
                                <asp:ImageButton ID="PFimgsearch" runat="server" ImageUrl="../images/icon-search.png" Height="25" Width="25" OnClick="PFimgsearch_Click" />
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
                </div>

                <div>
                    <asp:GridView runat="server" ID="gvPFInvoiceList" PageSize="10" 
                        AllowPaging="true" DataKeyNames="PFInvId,clientemail" AutoGenerateColumns="false" Width="100%" Height="50%"
                        CssClass="table table-striped table-bordered" OnSorting="gvPFInvoiceList_Sorting" OnPageIndexChanging="gvPFInvoiceList_PageIndexChanging">
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
                            <asp:BoundField DataField="PFInvId" HeaderText="PF InvId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="InvDate" HeaderText="PF Invoice Date" />
                            <asp:BoundField DataField="clientemail" HeaderText="Client Email" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="clientname" HeaderText="Client Name" />
                            <asp:BoundField DataField="consultantName" HeaderText="consultant Name" />
                            <asp:BoundField DataField="PFInvOrder" HeaderText="PF Invoice Order" />
                            <asp:BoundField DataField="PFInvoiceTotal" HeaderText="PF Invoice Total"  ItemStyle-HorizontalAlign="Right"/>

                            <asp:TemplateField HeaderText="Sending Options">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSendMail" ImageUrl="~/images/icon-email.png" runat="server" Width="30" Height="20" OnClick="imgSendMail_Click" />
                                    <asp:ImageButton ID="imgPFPdf" ImageUrl="~/images/PdfIcon.png" runat="server" Width="30" Height="20"  OnClick="imgPFPdf_Click"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <EmptyDataTemplate>
                          <h4><asp:Label ID = "lblEmptyMessage" Text="" runat="server" /></h4>  
                            </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:Label ID="lblresult" runat="server" />
                    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="SendMailPopupExtender" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                        CancelControlID="btnCancel" BackgroundCssClass="popupextndrBg">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup" runat="server" BorderColor="#003300" BorderWidth="1" CssClass="modalBackground" Height="50%" Width="30%" Style="display: none">


                        <br />
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        <%--Invoice No--%>
                                    </label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="lblPFID" runat="server" CssClass="form-control" Style="display: none"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        From
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtPFFrom" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Subject 
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtPFSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        To
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtPFTo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvmail" runat="server" ValidationGroup="PFemailGrp" ForeColor="Red" ControlToValidate="txtPFTo" ErrorMessage="Please Enter Email ID" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="PFrevmail" runat="server" ValidationGroup="PFemailGrp" ControlToValidate="txtPFTo" ForeColor="Red" ErrorMessage="Please Enter Email ID" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Body
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtPFBody" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Attatch file
                                    </label>
                                </div>
                                <div class="col-sm-10">
                                    <asp:FileUpload ID="Profuattachment" runat="server" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <asp:Button ID="PFSendMail" CommandName="Send" ValidationGroup="emailGrp" CssClass="btn btn-info" runat="server" Text="Send" OnClick="PFSendMail_Click" />
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
                    </asp:Panel>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

