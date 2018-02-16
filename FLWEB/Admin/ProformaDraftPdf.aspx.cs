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

public partial class Admin_ProformaDraftPdf : System.Web.UI.Page
{
    BALProformaInvoice objBALPFInvoice = new BALProformaInvoice();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string TempuniqCode = " ";
                int mergeId = 0; int PFInvId = 0;

                int comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
                if (!string.IsNullOrEmpty(Request.QueryString["TempuniqCode"]))
                {
                    TempuniqCode = Request.QueryString["TempuniqCode"];
                }

                // mergeId = Convert.ToInt32(objBALInvoice.GetServiceFeeMergeValue(invid, TempuniqCode));

                DataSet objDs = objBALPFInvoice.PFDraftPdfDetails(TempuniqCode, comapnyId);

                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/DraftPdf.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();

                StringBuilder sbMainrow = new StringBuilder();
                StringBuilder sbMainrow1 = new StringBuilder();
                int ComapanyAddress = 0;
                // int DocuHeader = 0;
                int Flight = 0;
                int Land = 0;
                int SF = 0;
                int GC = 0;


                if (objDs.Tables.Count > 0)
                {

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
                                string strImagPath = Server.MapPath("../images/" + dtlRow["comapnylogo"].ToString());
                                readFile = readFile.Replace("{Image}", "<img style='height:100px;width:150px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                                readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");

                            }
                            ComapanyAddress = 1;
                        }
                    }



                    if (objDs.Tables[0].Rows.Count == 0)
                    {
                        readFile = readFile.Replace("{Document_No}", "123456546256");
                        readFile = readFile.Replace("{Date}", " ");
                        readFile = readFile.Replace("{Consultant}", " ");
                        readFile = readFile.Replace("{Client}", " ");
                        readFile = readFile.Replace("{Currency}", " ");
                    }

                    #endregion

                    decimal LandClientTotal = 0;
                    decimal FlihgtClientTotal = 0;
                    decimal ServiceFeeClientTotal = 0;
                    decimal GeneralChargeClienttotal = 0;


                    decimal FlightExclAmt = 0;
                    decimal LandExclAmt = 0;
                    decimal ServiceFeeExclAmt = 0;
                    decimal GeneralChargeExclAmt = 0;

                    decimal FlightVat = 0;
                    decimal LandVat = 0;
                    decimal ServiceFeeVat = 0;
                    decimal GeneralChargeVat = 0;
                    decimal AirportTaxes = 0;

                    #region AirTicket
                    if (objDs.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[1].Rows)
                        {
                            if (Flight == 0)
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Air Tickets</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Prn</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ticket No</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Passenger/Dep Date/Route/Class</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            FlihgtClientTotal = FlihgtClientTotal + Convert.ToDecimal(dtlRow["AirClientTotal"]);
                            FlightExclAmt = FlightExclAmt + Convert.ToDecimal(dtlRow["AirExclusiveFare"]);
                            FlightVat = FlightVat + Convert.ToDecimal(dtlRow["AirVatonFare"]);
                            AirportTaxes = AirportTaxes + Convert.ToDecimal(dtlRow["AirPortTaxes"]);

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["SupAccountCode"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirTicketNo"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'/>" + dtlRow["Details"] + "<br/><br/>Airport Taxes<style='padding-left: 120px;'></td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirExclusiveFare"] + "<br/><br/> " + dtlRow["AirPortTaxes"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirVatonFare"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'><br/><br/>" + dtlRow["AirClientTotal"] + "</td>");
                            sbMainrow.Append("</tr>");
                            Flight = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>AirTickets Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + FlihgtClientTotal + "</td></tr>");

                    }
                    #endregion AirTicket

                    #region LandArrangement
                    if (objDs.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[2].Rows)
                        {
                            if (Land == 0)
                            {

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Land Arrangement</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            LandClientTotal = LandClientTotal + Convert.ToDecimal(dtlRow["ClientTotal"]);
                            LandExclAmt = LandExclAmt + Convert.ToDecimal(dtlRow["Excl Amt"]);
                            LandVat = LandVat + Convert.ToDecimal(dtlRow["VAT"]);

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["LSupAccountCode"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["BookRefNo"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["Excl Amt"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VAT"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                            sbMainrow.Append("</tr>");
                            Land = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandClientTotal + "</td></tr>");

                    }
                    #endregion LandArrangement

                    #region ServiceFee


                    if (objDs.Tables[3].Rows.Count > 0)
                    {


                        foreach (DataRow dtlRow in objDs.Tables[3].Rows)
                        {
                            if (dtlRow["MergeC"].ToString() == "0")
                            {
                                if (SF == 0)
                                {

                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td colspan='7' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Service Fee</td>");
                                    sbMainrow.Append("</tr>");


                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>SourceRef</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                    sbMainrow.Append("</tr>");
                                }

                                string Clienttotal = string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim();

                                string ExcluAmount = string.IsNullOrEmpty(dtlRow["ExcluAmount"].ToString().Trim()) ? ".00" : dtlRow["ExcluAmount"].ToString().Trim();

                                string VatAmount = string.IsNullOrEmpty(dtlRow["VatAmount"].ToString().Trim()) ? ".00" : dtlRow["VatAmount"].ToString().Trim();


                                ServiceFeeClientTotal = ServiceFeeClientTotal + Convert.ToDecimal(Clienttotal);
                                ServiceFeeExclAmt = ServiceFeeExclAmt + Convert.ToDecimal(ExcluAmount);
                                ServiceFeeVat = ServiceFeeVat + Convert.ToDecimal(VatAmount);

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Typ"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["SourceRef"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ExcluAmount"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                                sbMainrow.Append("</tr>");
                                Flight = 1;
                            }
                        }


                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + ServiceFeeClientTotal + "</td></tr>");

                    }


                    #endregion Service

                    #region GeneralCharge
                    if (objDs.Tables[4].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[4].Rows)
                        {
                            if (GC == 0)
                            {

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>General Charge</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            GeneralChargeClienttotal = GeneralChargeClienttotal + Convert.ToDecimal(dtlRow["ClientTot"]);
                            GeneralChargeExclAmt = GeneralChargeExclAmt + Convert.ToDecimal(dtlRow["ExcluAmt"]);
                            GeneralChargeVat = GeneralChargeVat + Convert.ToDecimal(dtlRow["VatAmount"]);

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Type"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Ref"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ExcluAmt"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTot"] + "</td>");
                            sbMainrow.Append("</tr>");
                            Flight = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>General Charge Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + GeneralChargeClienttotal + "</td></tr>");

                    }
                    #endregion GeneralCharge


                    decimal TotalInvExclAmt = FlightExclAmt + LandExclAmt + ServiceFeeExclAmt + GeneralChargeExclAmt + AirportTaxes;
                    decimal TotalVat = FlightVat + LandVat + ServiceFeeVat + GeneralChargeVat;
                    decimal TotalInclAmount = FlihgtClientTotal + LandClientTotal + ServiceFeeClientTotal + GeneralChargeClienttotal;


                    // Invocie Total desing

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td colspan='7' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'><br/></td>");
                    sbMainrow.Append("</tr>");

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Invoice Total</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInvExclAmt + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalVat + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");
                    // Balance From you desing
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Total Due</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");

                }

                readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());

                readFile = readFile.Replace("{LandArr}", sbMainrow1.ToString());

                string StrContent = readFile;

                GenerateHTML_TO_PDF(StrContent, true, "", false);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
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

            SelectPdf.PdfMargins margin = new SelectPdf.PdfMargins(1, 1, 1, 1);


            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;
            //converter.Options.MarginBottom=margin;
            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

            //doc.AddPage(pageSize, margin, pdfOrientation);\


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