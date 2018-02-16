<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MultipleFileUpload.aspx.cs" Inherits="Admin_MultipleFileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
                .floatedTable {
            float:left;
            border:initial;
        }
        .inlineTable {
            display: inline-block;
            padding-top:20px;
             border:initial;
        }
       
    .style1
    {
        width: 92px;
    }
    .btn
    {
        margin-left: 0px;
    }
    .style2
    {
        width: 68px;
    }
    .Removebtn
    {
        width: 66px;
        height: 27px;
    }
    .Addbtn
    {
        width: 56px;
        height: 26px;
        margin-top: 0px;
    }

       .progress
        {
            background: #FFFFFF;
            font-family: Verdana,Arial, Helvetica;
            color: Black;
            font-size: 11.5px;  
        }

</style>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-progressbar/0.9.0/bootstrap-progressbar.js"></script>

    <script  type="text/javascript">
        

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 
    <Triggers>
                <asp:AsyncPostBackTrigger ControlID="button2" EventName="Click" />
           <%--<asp:AsyncPostBackTrigger ControlID="button1" EventName="Click" />--%>
    </Triggers>
    
            <table border="1" class="inlineTable" style="width:30%;height:30%;border-color:#3785e4;">  
                <tr>  
                    <th><span style="color:#3785e4">Multiple File Upload</span></th>  
                </tr>  
              
                <tr>  
                    <td>  
                        <asp:FileUpload ID="fileuplaod1" runat="server" AllowMultiple="true" Font-Bold="true" />  
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                        <asp:Button ID="button1" runat="server" Text="upload" OnClick="button1_Click" Width="82px" />  
                    </td>  
                </tr>  
                <tr>  
                    <td></td>  
                </tr>  
                <tr style="height:30%;">  
                    <td>  
                        <asp:Label ID="label1" runat="server" ForeColor="Red" Font-Size="Small" Font-Bold="true"></asp:Label><br />  
                    </td>  
                </tr>  
            </table>  

        <table border="1" class="inlineTable" style="width:50%;height:30%;border-color:#3785e4;">
            
              <tr>
                  <td style="padding-right:50px;"><span style="color:#3785e4">Uploading Data</span></td>
              </tr>
                <%--<tr style="padding-top:0px;">  
                    <td>  
                        <asp:Label ID="labbel2" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label><br />  
                    </td>  
                </tr>--%>  
                <tr>  
                    <td>  
                        <asp:Label ID="label3" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="Small"></asp:Label>  
                    </td>  
                </tr>  
        </table>
      
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>


    <table>
        <tr>
            <td>
                   <asp:Button ID="button2" runat="server" Text="Import Data" OnClick="button2_Click" Width="82px" /> 
            </td>
        </tr>
        

        
   
    </table>

                    <div id="progressbar"></div>

        
  
      </ContentTemplate>
    </asp:UpdatePanel>
    
<asp:UpdateProgress ID="updProgress" runat="server">
                                    <ProgressTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align="center" class="progress">
                                                        <img alt="" style="vertical-align:           middle;" src="/images/progress.gif" /> 
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="progress">
                                                        Loading...
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
</asp:Content>

