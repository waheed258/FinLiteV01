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
        int companyId = 0; int invid = 0; var qs = "0"; 
        if (HttpContext.Current.Session["UserCompanyId"] != null)
        {
           

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string getId = Convert.ToString(Request.QueryString["id"]);
                qs = _BOUtility.Decrypts(HttpUtility.UrlDecode(getId), true);
                invid = Convert.ToInt32(qs);
            }

            if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
            {
                companyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
            }



            DataSet objDs = _objBALTransactions.Get_PrintReceipt(Convert.ToInt32(invid), companyId);
            if (objDs.Tables.Count > 0)
            {
                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/ReceiptPdf.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();
                int header = 0;
                StringBuilder sbMainrow = new StringBuilder();


                int ComapanyAddress = 0;
                int DocuHeader = 0;
                string currency = "";
                string companyname = "";
                string address = "";
                string country = ""; string state = ""; string city = "";
                int PrintStyleId = 0;

                        #region Company Deatils
                        if (objDs.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dtlRow in objDs.Tables[0].Rows)
                            {
                                if (ComapanyAddress == 0)
                                {

                                    readFile = readFile.Replace("{CompanyName}", dtlRow["CompanyName"].ToString());
                                    readFile = readFile.Replace("{address}", dtlRow["CompanyAddress"].ToString());
                                    readFile = readFile.Replace("{Country}", dtlRow["CountryName"].ToString() + " .");
                                    readFile = readFile.Replace("{State}", dtlRow["StateName"].ToString() + " ,");
                                    readFile = readFile.Replace("{City}", dtlRow["CityName"].ToString() + " ,");

                                    readFile = readFile.Replace("{currency}", dtlRow["currency"].ToString() + " ");
                                    string strImagPath = Server.MapPath("../images/" + dtlRow["comapnylogo"].ToString());
                                    readFile = readFile.Replace("{Image}", "<img style='height:40px;width:250px;'  src='" + "http://flv.swdtcpl.com/Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                                    //readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");


                                    companyname = dtlRow["CompanyName"].ToString();
                                    address = dtlRow["CompanyAddress"].ToString();
                                    country = dtlRow["CountryName"].ToString();
                                    state = dtlRow["StateName"].ToString();
                                    city = dtlRow["CityName"].ToString();
                                    currency = dtlRow["currency"].ToString();
                                    //string strUrl = _objBOUtiltiy.LogoUrl();
                                    //readFile = readFile.Replace("{Image}", "<img   src='" + strUrl + "Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                                    //readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + strUrl + "Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");

                                }
                                ComapanyAddress = 1;
                            }
                        }
                        if (objDs.Tables[0].Rows.Count == 0)
                        {
                            readFile = readFile.Replace("{CompanyName}", " ");
                            readFile = readFile.Replace("{address}", " ");
                            readFile = readFile.Replace("{Country}", " ");
                            readFile = readFile.Replace("{State}", " ");
                            readFile = readFile.Replace("{City}", " ");
                            readFile = readFile.Replace("{Image}", " ");
                            readFile = readFile.Replace("{Image3}", " ");
                        }

                        if (objDs.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dtlRow in objDs.Tables[1].Rows)
                            {
                                if (DocuHeader == 0)
                                {
                                    PrintStyleId = Convert.ToInt32(dtlRow["PrintStyleId"].ToString());
                                    readFile = readFile.Replace("{Invoice_No}", dtlRow["PaymentSourceRef"].ToString());
                                    readFile = readFile.Replace("{Date}", dtlRow["InvDate"].ToString());
                                    readFile = readFile.Replace("{Consultant}", dtlRow["ConsultantName"].ToString());
                                    if (dtlRow["ClientPostalAddress"].ToString() != "")
                                    {
                                        readFile = readFile.Replace("{clientAddress}", "Address :" + dtlRow["ClientPostalAddress"].ToString());
                                    }
                                    else
                                    {
                                        readFile = readFile.Replace("{clientAddress}", " ");
                                    }
                                    if (dtlRow["ClientVatRegNo"].ToString() != "")
                                    {
                                        readFile = readFile.Replace("{clientvatNo}", dtlRow["ClientVatRegNo"].ToString());
                                    }
                                    else
                                    {
                                        readFile = readFile.Replace("{clientvatNo}", " ");
                                    }
                                    readFile = readFile.Replace("{Client1}", dtlRow["ClientName"].ToString());
                                    readFile = readFile.Replace("{Client}", dtlRow["ClientName"].ToString());

                                    readFile = readFile.Replace("{OrderNo}", dtlRow["OrderNo"].ToString());
                                }
                                DocuHeader = 1;
                            }

                        }
                        if (objDs.Tables[1].Rows.Count == 0)
                        {
                            readFile = readFile.Replace("{Document_No}", "");
                            readFile = readFile.Replace("{Date}", " ");
                            readFile = readFile.Replace("{Consultant}", " ");
                            readFile = readFile.Replace("{clientAddress}", " ");
                            readFile = readFile.Replace("{Client}", " ");
                            readFile = readFile.Replace("{OrderNo}", " ");
                            readFile = readFile.Replace("{clientvatNo}", " ");
                        }

                        #endregion

                        if (objDs.Tables[2].Rows.Count > 0)
                        {
                            decimal invTotal = 0;
                           
                            decimal allocate = 0;
                            decimal invNet = 0;

                            foreach (DataRow dtlRow in objDs.Tables[2].Rows)
                            {
                                decimal invPaid = 0;

                                if (header == 0)
                                {
                                    //  sbMainrow.Append("<table>");
                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Invoice Details</td>");
                                    sbMainrow.Append("</tr>");

                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Invoice Number</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Client Name</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Invoice Total</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Receipt Amount</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Net Balance</td>");

                                    sbMainrow.Append("</tr>");
                                    header = 1;
                                }
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["InvId"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["ClientName"] + "</td>");

                                invTotal = Math.Abs(Convert.ToDecimal(dtlRow["InvoiceTotal"]) - allocate);
                                allocate = Convert.ToDecimal(dtlRow["AllocatedAmount"]);
                                invNet = invTotal - allocate;

                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'/>" + invTotal + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AllocatedAmount"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + invNet + "</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Mode Of Payment</td>");
                                sbMainrow.Append("<td colspan='4' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["RecDescription"] + "</td></tr>");
                              
                                // sbMainrow.Append("</table>");
                            }
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