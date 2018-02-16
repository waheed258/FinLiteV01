<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CarbonCoefficients.aspx.cs" Inherits="Admin_CarbonCoefficients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
        .style1{
            color:#FF0000;
        }
    </style>

     <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.js"></script>


    <script src="js/wickedpicker.js"></script>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {


            DatePickerSet();
            DatePickerSet1();
        });


        function DatePickerSet() {
            $("#ContentPlaceHolder1_txtStartDate").datepicker({

                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                autoclose: true,

            }).attr('readonly', 'true');;
        }
            function DatePickerSet1() {
                $("#ContentPlaceHolder1_txtEndDate").datepicker({

                    numberOfMonths: 1,
                    dateFormat: 'yy-mm-dd',
                    autoclose: true,

                }).attr('readonly', 'true');;

            }
        
        </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:HiddenField ID="hf_CarbonId" runat="server" Value="0" />
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">New Carbon Coefficient</h2>
        </header>
        <div class="panel-body">
             <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-2">
                        <label class="control-label">
                            Key(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" MaxLength="30" />
                        <asp:RequiredFieldValidator ControlToValidate="txtKey" runat="server" ID="rfvtxtKey"
                            Display="Dynamic" Text="Enter Key." ErrorMessage="Enter Key." ValidationGroup="carbon" ForeColor="Red" />

                    </div>


                    <div class="col-sm-1"></div>
                        <div class="col-sm-2">
                        <label class="control-label">
                            Description(<span class="style1">*</span>)
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtDescription" runat="server" ID="rfvtxtDescription"
                            Display="Dynamic" Text="Enter Description." ErrorMessage="Enter Description." ValidationGroup="carbon" ForeColor="Red" />
                         <%-- <asp:RegularExpressionValidator ControlToValidate="txtDescription" runat="server"
                            ID="revtxtDescription" ValidationGroup="carbon" ErrorMessage="Enter Only Characters.."
                            Text="Enter Only Characters.." ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                    </div>
                  
                </div>
            </div>
             <div class="form-group">
                <div class="col-sm-12">
                      <div class="col-sm-2">
                        <label class="control-label">Start Date</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" MaxLength="20" />
                       <%-- <asp:RequiredFieldValidator ControlToValidate="txtEffectiveDate" runat="server" ID="rfvtxtEffectiveDate" ValidationGroup="Vat"
                            ErrorMessage="Enter Effective Date" Text="Enter Effective Date" Display="Dynamic" ForeColor="Red" />--%>
                    </div>
                    <div class="col-sm-1">
                        </div>
                          <div class="col-sm-2">
                        <label class="control-label">End Date</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" MaxLength="20" />
                       <%-- <asp:RequiredFieldValidator ControlToValidate="txtEffectiveDate" runat="server" ID="rfvtxtEffectiveDate" ValidationGroup="Vat"
                            ErrorMessage="Enter Effective Date" Text="Enter Effective Date" Display="Dynamic" ForeColor="Red" />--%>
                    </div>
                 </div>
                 </div>
            <div class="form-group">
                <div class="col-sm-12">
                     <div class="col-sm-2">
                        <label class="control-label">Coefficient(<span class="style1">*</span>)</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCoefficient" runat="server" CssClass="form-control" MaxLength="20" placeholder="0.00" />
                        <asp:RequiredFieldValidator ControlToValidate="txtCoefficient" runat="server" ID="rfvtxtCoefficient" ValidationGroup="carbon"
                            ErrorMessage="Enter Coefficient." Text="Enter Coefficient." Display="Dynamic" ForeColor="Red" />
                    </div>
                </div>
            </div>
            
            <div class="form-group">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-success" ValidationGroup="carbon"
                        Text="Submit" OnClick="cmdSubmit_Click"/>&nbsp;<asp:Button runat="server" ID="btnCancel"
                            class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp; <asp:Button ID="btnreset" runat="server" CssClass="btn btn-primary blue" Text="Reset" OnClick="btnreset_Click"/>

                </div>
            </div>
        </div>
    </section>
</asp:Content>

