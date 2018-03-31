<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Admin_Index" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- start: page -->


    <asp:Label ID="lblMsg" runat="server"></asp:Label>

    <div class="row">

        <div class="col-md-6">
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Daily Report - Ticket Amounts & Tickets Count </h2>
                    <p class="panel-subtitle">

                        <asp:Label ID="lblDailyDate" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body">
                    <label id="lbldailychartamt" runat="server"
                        style="-moz-transform: rotate(-90deg); -o-transform: rotate(-90deg); -webkit-transform: rotate(-90deg);">
                    </label>
                    <asp:Chart ID="DailyChartAmt" runat="server" BackGradientStyle="LeftRight"
                        BorderlineWidth="0" Height="340px" Palette="None"
                        Width="180px" BorderlineColor="192, 64, 0">
                        <Series>
                            <asp:Series Name="Series3" YValuesPerPoint="12" IsValueShownAsLabel="true">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea3">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>


                    <label id="lbldailychartcnt" runat="server" style="-moz-transform: rotate(-90deg); -o-transform: rotate(-90deg); -webkit-transform: rotate(-90deg);"></label>
                    <asp:Chart ID="DailyChartCnt" runat="server" BackGradientStyle="LeftRight"
                        BorderlineWidth="0" Height="340px" Palette="None"
                        Width="180px" BorderlineColor="192, 64, 0">
                        <Series>
                            <asp:Series Name="Series4" YValuesPerPoint="12" IsValueShownAsLabel="true">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea4">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>


                </div>
            </section>
        </div>

        <div class="col-md-6">
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Monthly Report - Ticket Amounts & Tickets Count</h2>
                    <p class="panel-subtitle">

                        <asp:Label ID="lblMontlyDate" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body">
                    <label id="lblmontlychartamt" runat="server" style="-moz-transform: rotate(-90deg); -o-transform: rotate(-90deg); -webkit-transform: rotate(-90deg);"></label>
                    <asp:Chart ID="MontlyChartAmt" runat="server" BackGradientStyle="LeftRight"
                        BorderlineWidth="0" Height="340px" Palette="None"
                        Width="180px" BorderlineColor="192, 64, 0">
                        <Series>
                            <asp:Series Name="Series1" YValuesPerPoint="12" IsValueShownAsLabel="true">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>


                    <label id="lblmontlychartcnt" runat="server" style="-moz-transform: rotate(-90deg); -o-transform: rotate(-90deg); -webkit-transform: rotate(-90deg);"></label>
                    <asp:Chart ID="MontlyChartCnt" runat="server" BackGradientStyle="LeftRight"
                        BorderlineWidth="0" Height="340px" Palette="None"
                        Width="180px" BorderlineColor="192, 64, 0">
                        <Series>
                            <asp:Series Name="Series2" YValuesPerPoint="12"  IsValueShownAsLabel="true">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>


                </div>
            </section>
        </div>

    </div>

    <div class="row">
        <asp:HiddenField ID="hfDayReport" runat="server" />
        <asp:HiddenField ID="hfDayReportValues" runat="server" />
        <div class="col-md-6">
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Daily Report</h2>
                    <p class="panel-subtitle">

                        <asp:Label ID="lblDayTitle" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body">

                    <!-- Flot: Pie -->
                    <div class="ct-chart ct-perfect-fourth ct-golden-section" id="flotPie" style="height: 290px;"></div>


                </div>
            </section>
        </div>

        <div class="col-md-6">
            <asp:HiddenField ID="hfMonthReport" runat="server" />
            <asp:HiddenField ID="hfMonthReportValues" runat="server" />
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Monthly Report</h2>
                    <p class="panel-subtitle">
                        <asp:Label ID="lblMonthTitle" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body" style="height: 303px;">
                    <div id="ChartistExtremeResponsiveConfiguration" class="ct-chart ct-perfect-fourth ct-golden-section"></div>

                    <!-- See: assets/javascripts/ui-elements/examples.charts.js for the example code. -->
                </div>
            </section>
        </div>

    </div>

    <div class="row" style="display:none">

        <asp:HiddenField ID="hfYearMonth" runat="server" />
        <asp:HiddenField ID="hfYearValue" runat="server" />


        <div class="col-md-6">

            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Yearly Report (<asp:Label ID="lblYearTitel" runat="server"></asp:Label>)</h2>

                </header>
                <div class="panel-body">

                    <!-- Flot: Bars -->
                    <div class="chart chart-md" id="flotBars"></div>


                </div>
            </section>
        </div>


        <%--<div class="col-md-6">


            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Target Report</h2>
                </header>

                <div class="panel-body" runat="server" id="divTarget">
                </div>
                <!-- See: assets/javascripts/ui-elements/examples.charts.js for the example code. -->

            </section>



        </div>--%>
    </div>

    

    <!-- end: page -->
</asp:Content>

