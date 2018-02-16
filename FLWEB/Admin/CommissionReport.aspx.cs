using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
public partial class CommissionReport : System.Web.UI.Page
{
    BAReport _objBAreport = new BAReport();
    BOUtiltiy _BOUtilities = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }

       
    }

    private  DataSet GetData()
    {

       
            DataSet ds = _objBAreport.GetInvoiceData();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomers.DataSource = ds;
                gvCustomers.DataBind();
            }

            return ds;
       
              
    }
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlOrders").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                string InvId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
                GridView gvOrders = row.FindControl("gvOrders") as GridView;
                BindOrders(InvId, gvOrders);
            }
            else
            {
                row.FindControl("pnlOrders").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }
        catch (Exception ex)
        {
            
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindOrders(string InvId, GridView gvOrders)
    {
        try
        {
            gvOrders.ToolTip = InvId;

            DataSet ds = _objBAreport.GetCommissionData(Convert.ToInt32(InvId));
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOrders.DataSource = ds;
                gvOrders.DataBind();
            }

        }
        catch (Exception ex)
        {
            
            ExceptionLogging.SendExcepToDB(ex);
        }
        
    }

    protected void OnOrdersGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView gvOrders = (sender as GridView);
            gvOrders.PageIndex = e.NewPageIndex;
            BindOrders(gvOrders.ToolTip, gvOrders);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void Show_Hide_ProductsGrid(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlProducts").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                int orderId = Convert.ToInt32((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
                GridView gvProducts = row.FindControl("gvProducts") as GridView;
                BindProducts(orderId, gvProducts);
            }
            else
            {
                row.FindControl("pnlProducts").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }
        catch (Exception ex)
        {
             ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindProducts(int orderId, GridView gvProducts)
    {
       // gvProducts.ToolTip = orderId.ToString();
       // gvProducts.DataSource = GetData(string.Format("SELECT CommiAmount, TicketType,InvDocumentNo FROM Commission WHERE CommiId  = '{0}')", orderId));
       // gvProducts.DataBind();
    }

    protected void OnProductsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView gvProducts = (sender as GridView);
            gvProducts.PageIndex = e.NewPageIndex;
            BindProducts(int.Parse(gvProducts.ToolTip), gvProducts);
        }
        catch (Exception ex)
        {
              ExceptionLogging.SendExcepToDB(ex);
        }
    }


    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
    //       server control at run time. */
    //}

    //protected void btnPdf_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        using (StringWriter sw = new StringWriter())
    //        {
    //            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
    //            {
    //                int companyId = 0;

    //                if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
    //                {
    //                    companyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
    //                }
    //                DataSet objds = _objBAreport.GetCompanyDetails(companyId);


    //                if (objds.Tables.Count > 0)
    //                {

    //                    if (objds.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dtlRow in objds.Tables[0].Rows)
    //                        {

    //                            hw.Write("<table><tr>");
    //                            hw.Write("<td><img src='" + "http://flv.swdtcpl.com/Logos/" + dtlRow["comapnylogo"].ToString() + "' width=150px; height=30px; style='text-align:left;'/></td><td style='text-align:right;'>" + dtlRow["CompanyName"].ToString() + "</td></tr></table>");


    //                        }
    //                    }
    //                }



    //                //To Export all pages
    //                gvCustomers.AllowPaging = false;

    //                gvCustomers.RenderControl(hw);
    //                GetData();
    //                StringReader sr = new StringReader(sw.ToString());
    //                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
    //                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //                pdfDoc.Open();

    //                htmlparser.Parse(sr);
    //                pdfDoc.Close();

    //                Response.ContentType = "application/pdf";
    //                Response.AddHeader("content-disposition", "inline;filename=Report.pdf");


    //                Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //                Response.Write(pdfDoc);
    //                Response.End();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _BOUtilities.ShowMessage("danger", "Error", ex.Message);
    //    }
    //}
}