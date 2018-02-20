using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_AirSupplierLevelReport : System.Web.UI.Page
{

    BAReport objBaReport = new BAReport();
    BOUtiltiy _BOUtilities = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime ToDate = DateTime.Now;
            DateTime FromDate = ToDate.AddMonths(-1);

            string category = "Air Tickets";
            AirSuppllevelReportBind(FromDate, ToDate, category);
        }
    }
    protected void btnSuppliSubmit_Click(object sender, EventArgs e)
    {
        DateTime? FromDate = (txtFromDate.Text != "") ? Convert.ToDateTime(txtFromDate.Text) : (DateTime?)null;
        DateTime? ToDate = (txtToDate.Text != "") ? Convert.ToDateTime(txtToDate.Text) : (DateTime?)null;
        string CreatedBy = "Air Tickets";
        AirSuppllevelReportBind(FromDate, ToDate, CreatedBy);
    }

    private void AirSuppllevelReportBind(DateTime? FromDate, DateTime? ToDate, string CreatedBy)
    {
        try
        {
           
       
            DataSet ds = objBaReport.GetSupplierLevelData(FromDate, ToDate, CreatedBy);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvAirSupplLevelReport.DataSource = ds;
                gvAirSupplLevelReport.DataBind();
            }

            else
            {
                gvAirSupplLevelReport.DataSource = null;
                gvAirSupplLevelReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtilities.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    int companyId = 0;

                    if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
                    {
                        companyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
                    }
                    DataSet objds = objBaReport.GetCompanyDetails(companyId);


                    if (objds.Tables.Count > 0)
                    {

                        if (objds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dtlRow in objds.Tables[0].Rows)
                            {

                                hw.Write("<table><tr>");
                           
                                hw.Write("<td><img src='" + "http://flv.swdtcpl.com/Logos/" + dtlRow["comapnylogo"].ToString() + "' width=150px; height=30px; style='text-align:left;'/></td><td style='text-align:right;'>" + dtlRow["CompanyName"].ToString() + "</td></tr>");

                                hw.Write("<tr><td style='width:10px'></td><td style='font-size:20px;'><b>AirSupplier Report</b></td></tr></table><br/>");
                          
                            }
                        }
                    }



                    //To Export all pages
                    gvAirSupplLevelReport.AllowPaging = false;

                    gvAirSupplLevelReport.RenderControl(hw);
                    btnSuppliSubmit_Click(null, null);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();

                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "inline;filename=Report.pdf");


                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtilities.ShowMessage("danger", "Error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvAirSupplLevelReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DateTime ToDates = DateTime.Now;
        DateTime FromDate = ToDates.AddMonths(-1);
        string category = "Air Tickets";
        gvAirSupplLevelReport.PageIndex = e.NewPageIndex;
        AirSuppllevelReportBind(FromDate, ToDates, category);
    }
}