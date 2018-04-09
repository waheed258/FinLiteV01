<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MainAccounts.aspx.cs" Inherits="MainAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .style1 {
            color: #FF0000;
        }
    </style>
    <%--<script src="../Admin/js/jquery-1.2.6.min.js"></script>--%>
    <script>
        //function checkIDAvailability() {
        //    debugger;
        //    var result = $('#txtMainAccCode').val();
        //    $.ajax({
        //        type: "get",
        //        url: "MainAccounts.aspx/checkAccCode",
        //        data: "{MainAccCode: '" + result + "' }",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: onSuccess,
        //        failure: function (AjaxResponse) {
        //            document.getElementById("lblStatus").innerHTML = "Dfgdfg";
        //        }
        //    });
        //    function onSuccess(AjaxResponse) {
        //        document.getElementById("lblStatus").innerHTML = AjaxResponse.d;
        //    }
        //}

        $(document).ready(function () {
            DrpSearch();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                DrpSearch();
            });

        });

        function DrpSearch() {
            $('#<%= ddlDepartment.ClientID %>').select2();
            $('#<%= ddlCurrency.ClientID %>').select2();
            $('#<%= ddlMainAcType.ClientID %>').select2()
            $('#<%= DropDownCategory.ClientID %>').select2()
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">Main Accounts</h2>

        </header>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Code<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtMainAccCode" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtMainAccCode_TextChanged" />
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvmainaccCode" runat="server" ControlToValidate="txtMainAccCode" ValidationGroup="mainAccount" ErrorMessage="Please Enter Account Code " Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblaccnoerr" runat="server" ></asp:Label> 
                                 </div>

                            <%--<asp:UpdatePanel ID="upMessage" runat="Server" UpdateMode="Conditional">
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="txtMainAccCode" EventName="TextChanged" />
       </Triggers>
   <ContentTemplate>
       <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
    </ContentTemplate>
         </asp:UpdatePanel>--%>



                            <div class="col-sm-2">
                                <label class="control-label">
                                    Account Name<span class="style1">*</span>
                                </label>
                            </div>

                            <div class="col-sm-3">

                                <asp:TextBox ID="txtMainAccName" runat="server" class="form-control" OnTextChanged="txtMainAccName_TextChanged" AutoPostBack="true" />
                                <asp:RequiredFieldValidator ID="rfvmainAccName" runat="server" ControlToValidate="txtMainAccName" ValidationGroup="mainAccount" ErrorMessage="Please Enter Account Name" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Department<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">

                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" InitialValue="0" ControlToValidate="ddlDepartment" ValidationGroup="mainAccount" ErrorMessage="Please Select Department" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </div>
                            <%--     <div class="col-sm-1">
                        </div>--%>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Currency<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-1">
                                <asp:DropDownList ID="ddlDefaultCurrency" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-2">

                                <asp:DropDownList ID="ddlCurrency" runat="server" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCurrency" runat="server" InitialValue="0" ControlToValidate="ddlCurrency" ValidationGroup="mainAccount" ErrorMessage="Please Select Currency" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Account Type<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">

                                <asp:DropDownList ID="ddlMainAcType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMainAcType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlMainAcType" runat="server" InitialValue="0" ControlToValidate="ddlMainAcType" ValidationGroup="mainAccount" ErrorMessage="Please Select Currency" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>

                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Category<span class="style1">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">

                                <asp:DropDownList ID="DropDownCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownCategory_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" InitialValue="0" ControlToValidate="DropDownCategory" ValidationGroup="mainAccount" ErrorMessage="Please Select Category" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>

             

            <br />
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">

                    <asp:Button runat="server" ID="btnMainAccount" class="btn btn-success" ValidationGroup="mainAccount"
                        Text="Submit"
                            UseSubmitBehavior="false" 
                             OnClientClick="this.disabled='true';this.value='Please Wait...' "
                         OnClick="btnMainAccount_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnMainAccountCancel"
                        class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnMainAccountCancel_Click" />


                </div>
            </div>

   </ContentTemplate>
            </asp:UpdatePanel>
        </div>


    </section>
</asp:Content>

