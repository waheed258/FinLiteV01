using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_InvoiceLevelReport : System.Web.UI.Page
{
    BAReport objBaReport = new BAReport();
    BOUtiltiy _BOUtilities = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           
        }
    }
    protected void btnInvoiceSubmit_Click(object sender, EventArgs e)
    {
        invoicelevelReportBind();
    }


    

    private void invoicelevelReportBind()
    {
        try{
            DateTime? FromDate = (txtFromDate.Text != null) ? Convert.ToDateTime( txtFromDate.Text): (DateTime?)null ;
            DateTime? ToDate = (txtToDate.Text != null) ? Convert.ToDateTime( txtToDate.Text): (DateTime?)null ;
            DataSet ds = objBaReport.getInvoiceLevelReport(FromDate, ToDate);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvInvoiceLevelReport.DataSource = ds;
                gvInvoiceLevelReport.DataBind();
            }

            else
            {
                gvInvoiceLevelReport.DataSource = null;
                gvInvoiceLevelReport.DataBind();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtilities.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

   
}