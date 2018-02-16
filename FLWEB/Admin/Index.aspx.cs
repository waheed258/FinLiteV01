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

}