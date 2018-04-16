<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ClientsList.aspx.cs" Inherits="Admin_ClientsList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" />

    <style type="text/css">
        .WhiteBG {
            background-color: White;
        }

        .modalpopup {
            background-color: #ADADAD;
            filter: Alpha(Opacity=70);
            opacity: 0.70;
            -moz-opacity: 0.70;
        }

        .scrool {
            overflow: scroll;
            height: 500px;
        }
    </style>

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.js"></script>


    <script src="js/wickedpicker.js"></script>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {


            DatePickerSet();
        });


        function DatePickerSet() {
            $("#ContentPlaceHolder1_txtDateExpires").datepicker({

                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                autoclose: true,

            }).attr('readonly', 'true');;


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelSearch" runat="server" DefaultButton="imgsearch">
        <asp:UpdatePanel ID="UpdateSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <section class="panel">
                    <header class="panel-heading">
                        <div class="panel-actions">
                            <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                        </div>

                        <h2 class="panel-title">Clients</h2>
                    </header>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="mb-md">

                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="paneldiv-border">
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
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>

                        <asp:HiddenField ID="hf_ClientId" runat="server" Value="0" />
                        <asp:GridView ID="gvClientsList" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                            AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered" OnRowDataBound="gvClientsList_RowDataBound"
                            OnRowCommand="gvClientsList_RowCommand" OnPageIndexChanging="gvClientsList_PageIndexChanging" OnSorting="gvClientsList_Sorting" ShowHeaderWhenEmpty="true">
                            <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                            <Columns>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <%#Eval("ClientDesc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account No">
                                    <ItemTemplate>
                                        <%#Eval("ClientTypeAccCode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("ClientName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive">
                                    <ItemTemplate>
                                        <%#Eval("IsActive")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Telephone">
                                    <ItemTemplate>
                                        <%#Eval("ClientTelephone")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Is ItemActive">
                                <ItemTemplate>
                                    <%#Eval("ClientName")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <%#Eval("ClientEmail")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>

                                        <table>
                                            <tr>

                                                <td>
                                                    <asp:ImageButton ID="imgEdit" ToolTip="Edit" runat="server" ImageUrl="../images/icon-edit.png" Height="20" Width="20"
                                                        CommandName="Edit Client Details" CommandArgument='<%#Eval("ClientId") %>' />
                                                    <asp:ImageButton ID="imgDelete" ToolTip="Delete" runat="server" ImageUrl="../images/icon-delete.png" Height="20" Width="20"
                                                        CommandName="Delete Client Details" CommandArgument='<%#Eval("ClientId") %>' OnClientClick="javascript:return confirm('Are You Sure To Delete Client Details')" />
                                                    <asp:ImageButton ID="ImageCreditCard" ToolTip="Edit CreditCard" runat="server" ImageUrl="../images/CreditCard.png" Height="20" Width="20"
                                                        CommandName="Edit CreditCard Details" CommandArgument='<%#Eval("ClientId") %>' />
                                                </td>
                                            </tr>
                                        </table>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <h4>
                                    <asp:Label ID="lblEmptyMessage" Text="" runat="server" /></h4>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <asp:Panel ID="pnlCreditCard" runat="server" CssClass="WhiteBG" Style="width: 70%; padding: 5px; min-height: 96px; position: fixed; z-index: 100001; left: 16%; top: 5px; display: none; overflow: scroll; height: 400px;">
                            <%--     Your Modal Popup Message
            <br />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" />--%>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rpCreditCard" runat="server" OnItemCommand="rpCreditCard_ItemCommand" OnItemDataBound="rpCreditCard_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-sm-12 marginbtm" style="border: 1px solid #08376a; background: #D4E4EF; margin-top: 10px;">
                                                    <br />
                                                    <%-- <asp:HiddenField ID="hf_CreditCaredId" runat="server" Value='<%# Eval("CreditCardId")%>' />--%>
                                                    <div class="col-sm-12 marginbtm">
                                                        <asp:HiddenField ID="hf_CreditCardId" runat="server" Value='<%# Eval("CreditCardId") %>' />
                                                        <asp:ImageButton ID="imageCreditCard" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CreditCardId") %>' ImageUrl="../images/close.png"
                                                            OnClientClick="javascript:return confirm('Are You Sure To Delete CreditCard Details')" />
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-2">
                                                                <label class="control-label">Code</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCreditCardCode" runat="server" CssClass="form-control" MaxLength="3" Text='<%# DataBinder.Eval(Container.DataItem,"CreditCardCode") %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-2">
                                                                <label class="control-label">Type</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:HiddenField ID="Hf_ddlCrediCardId" runat="server" Value='<%# Eval("CreditCardType") %>' />
                                                                <asp:DropDownList ID="ddlCreditCardType" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>

                                                                <%--<asp:TextBox ID="txtCreditCardType" runat="server" CssClass="form-control" MaxLength="50" Text='<%#DataBinder.Eval(Container.DataItem,"CreditCardType") %>'></asp:TextBox>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-2">
                                                                <label class="control-label">Debit/Credit Card Number</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCreditCardAccNo" runat="server" CssClass="form-control" MinLength="16" MaxLength="16" Text='<%#DataBinder.Eval(Container.DataItem,"CreditCardAccNo") %>'></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtCreditCardAccNo" ValidationGroup="CC" ErrorMessage="Enter Number Only" SetFocusOnError="true" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="col-sm-1"></div>
                                                            <div class="col-sm-2">
                                                                <label class="control-label">Account Holder</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCreditCardAccHolder" runat="server" CssClass="form-control" MaxLength="50" Text='<%#DataBinder.Eval(Container.DataItem,"CreditCardAccHolder") %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <label class="col-sm-2">Expires</label>
                                                            <div class="col-sm-1">


                                                                <asp:TextBox ID="txtMonthExpire" runat="server" CssClass="form-control" MaxLength="2" placeholder="MM" Text='<%#DataBinder.Eval(Container.DataItem,"CreditCardExpireMonth") %>'></asp:TextBox>


                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:TextBox ID="txtYearExpire" runat="server" CssClass="form-control" MaxLength="2" placeholder="YY" Text='<%#DataBinder.Eval(Container.DataItem,"CreditCardExpireYear") %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1">
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <label class="col-sm-2"></label>
                                                            <div class="col-sm-3">
                                                                <asp:RegularExpressionValidator ID="rev" runat="server" Display="Dynamic" ControlToValidate="txtMonthExpire" ValidationGroup="CC" ForeColor="Red" ErrorMessage="Enter Number(01-12) Only" SetFocusOnError="true" ValidationExpression="(0[1-9]|1[012])"></asp:RegularExpressionValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="CC" ControlToValidate="txtYearExpire" ErrorMessage="Enter Number Only" SetFocusOnError="true" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <asp:ImageButton ID="imgbtnCreditCard" runat="server" ToolTip="Add CreditCard Details" ImageUrl="../images/add.png" OnClick="imgbtnCreditCard_Click" />
                                                <label>Add CreditCard Details</label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="CC"
                                            Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                        class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                   <%-- <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click" />--%>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:LinkButton ID="lnkFake" runat="server">
                        </asp:LinkButton>
                        <cc1:ModalPopupExtender ID="mpCreditCard" runat="server" PopupControlID="pnlCreditCard"
                            TargetControlID="lnkFake" BackgroundCssClass="modalpopup" BehaviorID="mpCreditCard"
                            CancelControlID="btnCancel">
                        </cc1:ModalPopupExtender>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

