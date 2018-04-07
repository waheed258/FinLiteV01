<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta name="description" content="" />
<link rel="shortcut icon" href="images/favicon.png" />

<head runat="server">
    <title>Dinoosys</title>
    <script src="js/jquery.min.js" type="text/jscript"></script>

    <!--Core CSS -->

    <link href="style/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <%-- <link href="admin/css/style.css" rel="stylesheet" />--%>
    <link href="style/style.css" rel="stylesheet" />
    <%--  <link href="admin/css/style-responsive.css" rel="stylesheet" />--%>
    <link href="style/style-responsive.css" rel="stylesheet" />

    <script type="text/javascript">
        function UpdateValidators() {
            for (var i = 0; i < Page_Validators.length; i++) {
                var val = Page_Validators[i];
                var ctrl = document.getElementById(val.controltovalidate);
                if (ctrl != null && ctrl.style != null) {
                    if (!val.isvalid)
                        ctrl.style.border = '1px solid #f7f7f7';
                    else
                        ctrl.style.border = null;
                }
            }
        }


        function showMacAddress() {

            var locator = new ActiveXObject("WbemScripting.SWbemLocator");
            alert('hi');
            var service = locator.ConnectServer(".");
            var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            var e = new Enumerator(properties);
            document.write("<table border=1>");
            dispHeading();
            for (; !e.atEnd() ; e.moveNext()) {
                var p = e.item();
                document.write("<tr>");
                document.write("`d>" + p.Caption + "</td>");
                document.write("<td>" + p.IPFilterSecurityEnabled + "</td>");
                document.write("<td>" + p.IPPortSecurityEnabled + "</td>");
                document.write("<td>" + p.IPXAddress + "</td>");
                document.write("<td>" + p.IPXEnabled + "</td>");
                document.write("<td>" + p.IPXNetworkNumber + "</td>");
                document.write("<td>" + p.MACAddress + "</td>");
                document.write("<td>" + p.WINSPrimaryServer + "</td>");
                document.write("<td>" + p.WINSSecondaryServer + "</td>");
                document.write("</tr>");
            }
            document.write("</table>");
        }

        function dispHeading() {
            document.write("<thead>");
            document.write("<td>Caption</td>");
            document.write("<td>IPFilterSecurityEnabled</td>");
            document.write("<td>IPPortSecurityEnabled</td>");
            document.write("<td>IPXAddress</td>");
            document.write("<td>IPXEnabled</td>");
            document.write("<td>IPXNetworkNumber</td>");
            document.write("<td>MACAddress</td>");
            document.write("<td>WINSPrimaryServer</td>");
            document.write("<td>WINSSecondaryServer</td>");
            document.write("</thead>");
        }

    </script>

    <style type="text/css">
        .hideit {
            background-color: #ccc;
            color: #ccc;
            border-color: #ccc;
            border: none;
        }
    </style>
</head>
<body>
    <form runat="server" id="LoginForm" class="login-body cmxform form-horizontal " novalidate="novalidate" style="background-image: url(../images/FLlgonback.jpg)">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <div class="container">
            <div class="container">
                <div class="header-login">
                    <div>
                        <img src="Logos/logo-dinoosys.png" style="width: 170px; height: 50px;" />

                        <%--<img src="http://salesdemo.dinoosystech.com/Logos/Serendipitylogo.png" style="width: 170px; height: 50px;" />--%>
                    </div>
                </div>
            </div>
            <div>
                <div class="container">


                    <div class="form-signin">
                        <h2 class="form-signin-heading"></h2>
                        <div class="login-wrap">
                            <div class="user-login-info">
                                <asp:TextBox runat="server" ID="txtLoginId" class=" form-control" placeholder="Enter User ID"
                                    autofocus="" ValidationGroup="login"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLoginId" runat="server" ID="rfvtxtLoginId"
                                    ValidationGroup="submit" ErrorMessage="Enter your UserId." Text="Enter your UserId."
                                     ForeColor="White"  />
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" class="form-control"
                                    placeholder="Enter Password" ValidationGroup="submit" CausesValidation="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" ID="rfvtxtPassword"
                                    ValidationGroup="akki" ErrorMessage="Enter your Password." Text="Enter your Password."
                                    ForeColor="White"  />
                                <div>

                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                         OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" style="display:none">
                                    </asp:DropDownList>
                                </div>
                                <div class="clear">
                                </div>
                            </div>

                            <asp:Button runat="server" ID="cmdSubmit" class="btn btn-lg btn-login btn-block"
                                ValidationGroup="submit" Text="Sign in" OnClick="cmdSubmit_Click" />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:LinkButton data-toggle="modal" ID="cmdPopup" runat="server" class="forgotpassword"
                                Text="Forgot Password?" Visible="false"></asp:LinkButton>
                            <%-- Visible for to cmdPopup --%>
                            <div class="registration" style="display: none;">
                                Don't have an account yet?<a class="" href="registration.html">Create an account</a>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="PanelForgotPassword" runat="server" Style="display: none">
            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="cmdClose" runat="server" class="close">&times;</asp:LinkButton>
                            <h4 class="modal-title">Forgot Password ?</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                Enter your registered e-mail address to receive a new password.
                            </p>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" autocomplete="off"
                                class="form-control placeholder-no-fix" ValidationGroup="prog"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ID="rfvtxtEmail"
                                ValidationGroup="prog" ErrorMessage="Enter registered EmailId." Text="Enter registered EmailId."
                                ForeColor="White"  />
                            <p>
                            </p>
                            <asp:TextBox ID="txtLoginId2" runat="server" placeholder="Login Id" autocomplete="off"
                                class="form-control placeholder-no-fix" ValidationGroup="prog"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtLoginId2" runat="server" ID="rfvtxtLoginId2"
                                ValidationGroup="prog" ErrorMessage="Enter your login Id." Text="Enter your login Id."
                                ForeColor="White" />
                        </div>
                        <div>
                            <asp:Label ID="labelError2" class="linheight" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="cmdSend" runat="server" class="btn btn-success" Text="Submit" ValidationGroup="prog"
                                OnClick="cmdSend_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ForgotPasswordModalPopupExtender" runat="server" TargetControlID="cmdPopup"
            PopupControlID="PanelForgotPassword" CancelControlID="cmdClose">
        </cc1:ModalPopupExtender>

        <script src="js/jquery.js" type="text/jscript"></script>
        <script src="js/bootstrap.min.js" type="text/jscript"></script>

        <script src="js/jquery.validate.min.js" type="text/jscript"></script>

        <!--common script init for all pages-->

        <script src="js/scripts.js" type="text/jscript"></script>

        <!--this page script-->
    </form>
</body>
</html>
