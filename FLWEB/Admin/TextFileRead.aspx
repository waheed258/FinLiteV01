<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="TextFileRead.aspx.cs" Inherits="Admin_TextFileRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="panel">
        <header class="panel-heading">
            <div class="panel-actions">
                <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
            </div>
            <h2 class="panel-title">Text File Reading</h2>
        </header>

        <div class="panel-body">
            <div class="col-sm-12">
                <h3>Single  Files Upload </h3>
                <asp:FileUpload runat="server" ID="textFileUpload" /><br />

                <asp:Button runat="server" ID="BtnUploadTxtFile" Text="Upload" class="btn btn-success" OnClick="BtnUploadTxtFile_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                <br />
                <br />
                <asp:Label runat="server" ID="LblFileStatus" Text="Upload status: " ForeColor="Blue" />
            </div>

            <div>
                <br />
                <br />
                <br />
            </div>
            <div>
                <div>
                    <h3>Multiple Files Upload </h3>
                    <asp:FileUpload ID="mirfileUpload" runat="server" AllowMultiple="true" />
                </div>
                <div>
                    <br />
                </div>
                <div>
                    <asp:Button ID="fileUpload" class="btn btn-success" Text="Upload All files" runat="server" OnClick="fileUpload_Click" />
                </div>
            </div>
            <asp:Label runat="server" ID="Result" Text="Upload status: " ForeColor="Blue" />
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <asp:Label ID="Uploadfile" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="readfile" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Unreadcount" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="PreviousUplodfiles" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <asp:Label ID="remaining" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-sm-3">
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="Unread" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="filenames" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                </div>
            </div>

            <div>
            </div>
        </div>


    </section>

</asp:Content>

