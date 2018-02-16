<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Errors.aspx.cs" Inherits="Admin_Errors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:GridView ID="GVErrors" runat="server" AllowPaging="true" Width="100%" PageSize="10"
                AutoGenerateColumns="False" DataKeyNames="" CssClass="table table-striped table-bordered"
                  ShowHeaderWhenEmpty="true">
                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />

                <Columns>
                     <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <%#Eval("Logid")%>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Message">
                        <ItemTemplate>
                            <%#Eval("ExceptionMsg")%>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Exception Type">   
                    <ItemTemplate>
                            <%#Eval("ExceptionType")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exception Source">
                        <ItemTemplate>
                            <%#Eval("ExceptionSource")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exception URL" >
                        <ItemTemplate>
                            <%#Eval("ExceptionURL")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Logdate">
                        <ItemTemplate>
                            <%#Eval("Logdate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Id">
                        <ItemTemplate>
                            <%#Eval("UserFirstName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</asp:Content>

