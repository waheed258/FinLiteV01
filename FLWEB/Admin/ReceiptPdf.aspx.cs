using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReceiptPdf : System.Web.UI.Page
{
    BOUtiltiy _BOUtility = new BOUtiltiy();
    BALTransactions _objBALTransactions = new BALTransactions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Receipt_Pdf();
        }
    }

    private void Receipt_Pdf()
    {
        if (HttpContext.Current.Session["UserCompanyId"] != null)
        {
            int invid = 0; var qs = "0";

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string getId = Convert.ToString(Request.QueryString["id"]);
                qs = _BOUtility.Decrypts(HttpUtility.UrlDecode(getId), true);
                invid = Convert.ToInt32(qs);
            }

            DataSet objDs = _objBALTransactions.Get_PrintReceipt(Convert.ToInt32(invid));
            if (objDs.Tables[0].Rows.Count >= 1)
            {
                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/ReceiptPdf.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();
                int header = 0;
                StringBuilder sbMainrow = new StringBuilder();

                foreach (DataRow dtlRow in objDs.Tables[0].Rows)
                {
                    if (header == 0)
                    {
                      //  sbMainrow.Append("<table>");
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Client Name</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Client Account Code</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Invoice Total</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Open Amount</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>paid Amount</td>");

                        sbMainrow.Append("</tr>");
                        header = 1;
                    }
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["ClientName"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["ClientTypeAccCode"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'/>" + dtlRow["InvoiceTotal"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["InvoiceOpenAmount"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["InvoicePaiedAmount"] + "</td>");
                    sbMainrow.Append("</tr>");
                   // sbMainrow.Append("</table>");
                }
                readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());
                string StrContent = readFile;

                GenerateHTML_TO_PDF(StrContent, true, "", false);


                //GenerateHTML_TO_PDF(StrContent, true, "", false);


            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    private void GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string FileName, bool SaveFileDir)
    {
        try
        {
            string pdf_page_size = "A4";
            SelectPdf.PdfPageSize pageSize = (SelectPdf.PdfPageSize)Enum.Parse(typeof(SelectPdf.PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            SelectPdf.PdfPageOrientation pdfOrientation =
                (SelectPdf.PdfPageOrientation)Enum.Parse(typeof(SelectPdf.PdfPageOrientation),
                pdf_orientation, true);


            int webPageWidth = 1024;


            int webPageHeight = 0;




            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

            // save pdf document      

            if (!SaveFileDir)
                doc.Save(Response, ResponseShow, FileName);
            else
                doc.Save(FileName);

            doc.Close();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

}