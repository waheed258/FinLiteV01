<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SampleForm.aspx.cs" Inherits="Admin_SampleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">Sample Form</h2>
        </header>
        <div class="panel-body">

            <div class="form-group">
                <div class="col-sm-2">
                    <label class="control-label">
                        Month</label>
                </div>
                <div class="col-sm-2">

                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                        <asp:ListItem Text="January" Value="1"></asp:ListItem>
                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-2">
                    <label class="control-label">
                        Year
                    </label>
                </div>
                <div class="col-sm-2">
                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>

            </div>
            <div class="form-group">
                <div class="col-sm-2">
                    <label class="control-label">
                        Target Amount</label>
                </div>
                <div class="col-sm-2">

                    <asp:TextBox ID="txtTargetAmount" runat="server" class="form-control" MaxLength="10" />
                    <asp:RequiredFieldValidator ControlToValidate="txtTargetAmount" runat="server" ID="rfvPercentage" ValidationGroup="subbmit"
                        ErrorMessage="Enter Service Fee" Text="Enter Target Amount" class="validationred" Display="Dynamic" />
                    <asp:RegularExpressionValidator ControlToValidate="txtTargetAmount" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                        ErrorMessage="Enter  number only." Text="Enter  number only."
                        ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-2">
                    <label class="control-label">
                        Completed Amount
                    </label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtCompletedAmount" runat="server" class="form-control" ReadOnly="true" Text="0" />
                </div>

            </div>
            <div class="form-group">
                <div class="col-sm-2">
                    <label class="control-label">
                        Pending Amount</label>
                </div>
                <div class="col-sm-2">

                    <asp:TextBox ID="txtPendingAmount" runat="server" class="form-control" ReadOnly="true" Text="0" />

                </div>

            </div>
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel"
                            class="btn btn-primary red" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />

                </div>
            </div>

            
        </div>
    </section>
</asp:Content>

