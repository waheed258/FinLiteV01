using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.Data;
using BusinessManager;
using System.Drawing;

public partial class Admin_Index : System.Web.UI.Page
{
    BAReport objBAReport = new BAReport();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserLoginId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                DailyReport();
                MonthlyReport();
                Montlychart();
                DailyChart();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
            //Chandra Commented user is Null 
          //  ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void DailyReport()
    {
        try
        {
            lblDayTitle.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            string FromDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            string ToDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            DataSet objDayWiseReport = objBAReport.GetDashBoard(FromDate, ToDate);
            string strDayReport = string.Empty;
            string strDayReportValues = string.Empty;

            foreach (DataRow dr in objDayWiseReport.Tables[0].Rows)
            {
                strDayReport = strDayReport + "," + dr["operation"].ToString() + "-" + dr["PaymentAmount"].ToString();
                strDayReportValues = strDayReportValues + "," + dr["PaymentAmount"].ToString();

            }
            hfDayReport.Value = strDayReport.TrimStart(',');
            hfDayReportValues.Value = strDayReportValues.TrimStart(',');
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void MonthlyReport()
    {

        try
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            lblMonthTitle.Text = DateTime.Now.ToString("MMMM, yyyy");
            string MonthlyFromDate = _objBOUtiltiy.ConvertDateFormat(startDate.ToString());
            string MonthlyToDate = _objBOUtiltiy.ConvertDateFormat(endDate.ToString());
            string strMonthReport = string.Empty;
            string strMonthReportValues = string.Empty;
            DataSet objMonthWiseReport = objBAReport.GetDashBoard(MonthlyFromDate, MonthlyToDate);
          //  Chart1.DataSource = objMonthWiseReport;

            foreach (DataRow dr in objMonthWiseReport.Tables[0].Rows)
            {
                strMonthReport = strMonthReport + "," + dr["operation"].ToString() + "-" + dr["PaymentAmount"].ToString();
                strMonthReportValues = strMonthReportValues + "," + dr["PaymentAmount"].ToString();
            }
            hfMonthReport.Value = strMonthReport.TrimStart(',');
            hfMonthReportValues.Value = strMonthReportValues.TrimStart(',');
            
        }
        catch (Exception ex)
        {
             ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void Montlychart()
    {
        try
        {

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            lblMontlyDate.Text = DateTime.Now.ToString("MMMM, yyyy");
            string MonthlyFromDate = _objBOUtiltiy.ConvertDateFormat(startDate.ToString());
            string MonthlyToDate = _objBOUtiltiy.ConvertDateFormat(endDate.ToString());
            string strMonthReport = string.Empty;
            string strMonthReportValues = string.Empty;
            DataSet ds = objBAReport.GetTicketsAmount(MonthlyFromDate, MonthlyToDate);

            DataTable ChartData = ds.Tables[0];

            //storing total rows count to loop on each Record  
            string[] XPointMember = new string[ChartData.Rows.Count];
            decimal[] YPointMember = new decimal[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                if (ChartData.Rows[count]["Name"].ToString() != "")
                {
                    //storing Values for X axis  
                    XPointMember[count] = ChartData.Rows[count]["Name"].ToString();
                }
                decimal value = Convert.ToDecimal(string.IsNullOrEmpty(ChartData.Rows[count]["Amount"].ToString()) ? 0 : Convert.ToDecimal(ChartData.Rows[count]["Amount"].ToString()));
                if (value != 0)
                {
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToDecimal(ChartData.Rows[count]["Amount"].ToString());

                }


            }
            //binding chart control  
            MontlyChartAmt.Series[0].Points.DataBindXY(XPointMember, YPointMember);


            //Setting width of line  
            MontlyChartAmt.Series[0].BorderWidth = 10;
            //setting Chart type   
            MontlyChartAmt.Series[0].ChartType = SeriesChartType.Column;
            //Chart1.Series[0].ChartType = SeriesChartType.StackedColumn;  
            MontlyChartAmt.Series[0].Points[0].Color = Color.Coral;
            MontlyChartAmt.Series[0].Points[1].Color = Color.LightSeaGreen;
            MontlyChartAmt.Series[0].Points[2].Color = Color.SlateBlue;

            //Hide or show chart back GridLines  
            MontlyChartAmt.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            MontlyChartAmt.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            lblmontlychartamt.InnerText = "Amounts";
            //Enabled 3D  
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  


            DataTable TicketCountChartData = ds.Tables[1];

            //storing total rows count to loop on each Record  
            string[] XcountPointMember = new string[TicketCountChartData.Rows.Count];
            int[] YcountPointMember = new int[TicketCountChartData.Rows.Count];

            for (int count = 0; count < TicketCountChartData.Rows.Count; count++)
            {
                if (TicketCountChartData.Rows[count]["Name"].ToString() != "")
                {
                    //storing Values for X axis  
                    XcountPointMember[count] = TicketCountChartData.Rows[count]["Name"].ToString();
                }
                int value = Convert.ToInt32(string.IsNullOrEmpty(TicketCountChartData.Rows[count]["TktCount"].ToString()) ? 0 : Convert.ToInt32(TicketCountChartData.Rows[count]["TktCount"].ToString()));
                if (value != 0)
                {
                    //storing values for Y Axis  
                    YcountPointMember[count] = Convert.ToInt32(TicketCountChartData.Rows[count]["TktCount"].ToString());

                }


            }
            //binding chart control  
            MontlyChartCnt.Series[0].Points.DataBindXY(XcountPointMember, YcountPointMember);


            //Setting width of line  
            MontlyChartCnt.Series[0].BorderWidth = 10;
            //setting Chart type   
            MontlyChartCnt.Series[0].ChartType = SeriesChartType.Column;
            //Chart1.Series[0].ChartType = SeriesChartType.StackedColumn;  
            MontlyChartCnt.Series[0].Points[0].Color = Color.Coral;
            MontlyChartCnt.Series[0].Points[1].Color = Color.LightSeaGreen;
            //Hide or show chart back GridLines  
            MontlyChartCnt.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
            MontlyChartCnt.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
            lblmontlychartcnt.InnerText = "Count";
            //Enabled 3D  
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  

        }
        catch (Exception ex)
        {
             ExceptionLogging.SendExcepToDB(ex);
         
        }
    }  


    private void DailyChart()
    {
        try
        {
            lblDailyDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            string FromDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            string ToDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            DataSet ds = objBAReport.GetTicketsAmount(FromDate, ToDate);
            string strDayReport = string.Empty;
            string strDayReportValues = string.Empty;

            DataTable ChartData = ds.Tables[0];

            //storing total rows count to loop on each Record  
            string[] XPointMember = new string[ChartData.Rows.Count];
            decimal[] YPointMember = new decimal[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                if (ChartData.Rows[count]["Name"].ToString() != "")
                {
                    //storing Values for X axis  
                    XPointMember[count] = ChartData.Rows[count]["Name"].ToString();
                }
                decimal value = Convert.ToDecimal(string.IsNullOrEmpty(ChartData.Rows[count]["Amount"].ToString()) ? 0 : Convert.ToDecimal(ChartData.Rows[count]["Amount"].ToString()));
                if (value != 0)
                {
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToDecimal(ChartData.Rows[count]["Amount"].ToString());

                }


            }
            //binding chart control  
            DailyChartAmt.Series[0].Points.DataBindXY(XPointMember, YPointMember);


            //Setting width of line  
            DailyChartAmt.Series[0].BorderWidth = 10;
            //setting Chart type   
            DailyChartAmt.Series[0].ChartType = SeriesChartType.Column;
            //Chart1.Series[0].ChartType = SeriesChartType.StackedColumn;  

            DailyChartAmt.Series[0].Points[0].Color = Color.Coral;
            DailyChartAmt.Series[0].Points[1].Color = Color.LightSeaGreen;
            DailyChartAmt.Series[0].Points[2].Color = Color.SlateBlue;

            //Hide or show chart back GridLines  
            DailyChartAmt.ChartAreas["ChartArea3"].AxisX.MajorGrid.Enabled = false;
            DailyChartAmt.ChartAreas["ChartArea3"].AxisY.MajorGrid.Enabled = false;

            lbldailychartamt.InnerText = "Amounts";
            //Enabled 3D  
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  


            DataTable TicketCountChartData = ds.Tables[1];

            //storing total rows count to loop on each Record  
            string[] XcountPointMember = new string[TicketCountChartData.Rows.Count];
            int[] YcountPointMember = new int[TicketCountChartData.Rows.Count];

            for (int count = 0; count < TicketCountChartData.Rows.Count; count++)
            {
                if (TicketCountChartData.Rows[count]["Name"].ToString() != "")
                {
                    //storing Values for X axis  
                    XcountPointMember[count] = TicketCountChartData.Rows[count]["Name"].ToString();
                }
                int value = Convert.ToInt32(string.IsNullOrEmpty(TicketCountChartData.Rows[count]["TktCount"].ToString()) ? 0 : Convert.ToInt32(TicketCountChartData.Rows[count]["TktCount"].ToString()));
                if (value != 0)
                {
                    //storing values for Y Axis  
                    YcountPointMember[count] = Convert.ToInt32(TicketCountChartData.Rows[count]["TktCount"].ToString());

                }


            }
            //binding chart control  
            DailyChartCnt.Series[0].Points.DataBindXY(XcountPointMember, YcountPointMember);


            //Setting width of line  
            DailyChartCnt.Series[0].BorderWidth = 10;
            //setting Chart type   
            DailyChartCnt.Series[0].ChartType = SeriesChartType.Column;
            //Chart1.Series[0].ChartType = SeriesChartType.StackedColumn;  
            DailyChartCnt.Series[0].Points[0].Color = Color.Coral;
            DailyChartCnt.Series[0].Points[1].Color = Color.LightSeaGreen;
            //Hide or show chart back GridLines  
            DailyChartCnt.ChartAreas["ChartArea4"].AxisX.MajorGrid.Enabled = false;
            DailyChartCnt.ChartAreas["ChartArea4"].AxisY.MajorGrid.Enabled = false;
            lbldailychartcnt.InnerText = "Count";
            //Enabled 3D  
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
          
        }
    }
}