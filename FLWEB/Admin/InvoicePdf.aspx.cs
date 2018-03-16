using BusinessManager;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_InvoicePdf : System.Web.UI.Page
{
    BALInvoice objBALInvoice = new BALInvoice();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserCompanyId"] == null)
        //{
        //    string script = string.Format("alert('Your session has expired');window.location ='InvoiceList.aspx';");
        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script, true);
        //    //Response.Redirect("Login.aspx");
        //}
        // else
        // {
        try
        {
            if (!IsPostBack)
            {
                int invid = 0;
                int companyId = 0;
                var qs = "0";

                string TempuniqCode = "";

                if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
                {
                    companyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
                }

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string getId = Convert.ToString(Request.QueryString["id"]);
                    qs = _objBOUtiltiy.Decrypts(HttpUtility.UrlDecode(getId),true);
                    invid = Convert.ToInt32(qs);
                }



                DataSet objds = objBALInvoice.GetServiceFeeMergeValue(invid, TempuniqCode);


                DataSet objDs = objBALInvoice.GetPdfDetails(invid, companyId);

                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/NewPdfInvoice.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();

                StringBuilder sbMainrow = new StringBuilder();
                StringBuilder sbLandrow = new StringBuilder();
                int ComapanyAddress = 0;
                int DocuHeader = 0;
                int Flight = 0;
                int Land = 0;
                int LandOnly = 0;

                int SF = 0;
                int GC = 0;
                int PrintStyleId = 0;

                if (objDs.Tables.Count > 0)
                {

                    #region Company Deatils
                    if (objDs.Tables[5].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[5].Rows)
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


                                //string strUrl = _objBOUtiltiy.LogoUrl();
                                //readFile = readFile.Replace("{Image}", "<img   src='" + strUrl + "Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                                //readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + strUrl + "Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");

                            }
                            ComapanyAddress = 1;
                        }
                    }
                    if (objDs.Tables[5].Rows.Count == 0)
                    {
                        readFile = readFile.Replace("{CompanyName}", " ");
                        readFile = readFile.Replace("{address}", " ");
                        readFile = readFile.Replace("{Country}", " ");
                        readFile = readFile.Replace("{State}", " ");
                        readFile = readFile.Replace("{City}", " ");
                        readFile = readFile.Replace("{Image}", " ");
                        readFile = readFile.Replace("{Image3}", " ");
                    }

                    if (objDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[0].Rows)
                        {
                            if (DocuHeader == 0)
                            {
                                PrintStyleId = Convert.ToInt32(dtlRow["PrintStyleId"].ToString());
                                readFile = readFile.Replace("{Invoice_No}", dtlRow["InvId"].ToString());
                                readFile = readFile.Replace("{Date}", dtlRow["InvDate"].ToString());
                                readFile = readFile.Replace("{Consultant}", dtlRow["ConsultantName"].ToString());
                                readFile = readFile.Replace("{Client1}", dtlRow["ClientName"].ToString());
                                readFile = readFile.Replace("{Client}", dtlRow["ClientName"].ToString());
                                readFile = readFile.Replace("{OrderNo}", dtlRow["OrderNo"].ToString());
                            }
                            DocuHeader = 1;
                        }

                    }
                    if (objDs.Tables[0].Rows.Count == 0)
                    {
                        readFile = readFile.Replace("{Document_No}", "123456546256");
                        readFile = readFile.Replace("{Date}", " ");
                        readFile = readFile.Replace("{Consultant}", " ");
                        readFile = readFile.Replace("{Client}", " ");
                        readFile = readFile.Replace("{OrderNo}", " ");
                    }

                    #endregion

                    decimal LandClientTotal = 0;
                    decimal FlihgtClientTotal = 0;
                    decimal ServiceFeeClientTotal = 0;
                    decimal GeneralChargeClienttotal = 0;


                    decimal SepLandClientTotal = 0;
                    decimal SepLandExclAmt = 0;
                    decimal SepLandVat = 0;

                    decimal FlightExclAmt = 0;
                    decimal LandExclAmt = 0;
                    decimal ServiceFeeExclAmt = 0;
                    decimal GeneralChargeExclAmt = 0;

                    decimal FlightVat = 0;
                    decimal LandVat = 0;
                    decimal ServiceFeeVat = 0;
                    decimal GeneralChargeVat = 0;
                    decimal AirportTaxes = 0;



                    #region LandArrangement
                    if (objDs.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[2].Rows)
                        {
                            if (PrintStyleId == 0)
                            {
                                if (LandOnly == 0)
                                {
                                    //sbLandrow.Append("<h3 class='text-center'><strong>Land Invoice Summary</strong></h3>");

                                    sbLandrow.Append("<tr>");
                                    sbLandrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Land Invoice Summary</td>");
                                    sbLandrow.Append("</tr>");


                                    sbLandrow.Append("<tr>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");

                                    sbLandrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                    sbLandrow.Append("</tr>");


                                }
                            }


                            SepLandClientTotal = SepLandClientTotal + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim());
                            SepLandExclAmt = SepLandExclAmt + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["Excl Amt"].ToString().Trim()) ? ".00" : dtlRow["Excl Amt"].ToString().Trim());
                            SepLandVat = SepLandVat + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["VAT"].ToString().Trim()) ? ".00" : dtlRow["VAT"].ToString().Trim());


                            readFile = readFile.Replace("{SupplName}", dtlRow["LSupplierName"].ToString());


                            //LandClientTotal = LandClientTotal + LandClientTotal;
                            //LandExclAmt = LandExclAmt + LandExclAmt;
                            //LandVat = LandVat + LandVat;


                            if (PrintStyleId == 0)
                            {
                                sbLandrow.Append("<tr>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["LandArrId"] + "</td>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["BookRefNo"] + "</td>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["Excl Amt"] + "</td>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VAT"] + "</td>");

                                sbLandrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                                sbLandrow.Append("</tr>");
                                LandOnly = 1;
                            }
                        }
                        //sbMainrow.Append("<tr>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandClientTotal + "</td></tr>");
                        if (PrintStyleId == 0)
                        {

                            sbLandrow.Append("<tr>");
                            sbLandrow.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                            sbLandrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + SepLandClientTotal + "</td></tr>");
                        }

                    }


                    readFile = readFile.Replace("{LandData}", sbLandrow.ToString());

                    #endregion LandArrangement


                    #region AirTicket
                    if (objDs.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[1].Rows)
                        {
                            if (PrintStyleId == 0)
                            {
                                if (Flight == 0)
                                {
                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Air Tickets</td>");
                                    sbMainrow.Append("</tr>");


                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Prn</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ticket No</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Passenger/Route/Dep Date</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Airport Taxes</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");

                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                    sbMainrow.Append("</tr>");
                                }
                            }
                            FlihgtClientTotal = FlihgtClientTotal + Convert.ToDecimal(dtlRow["AirClientTotal"]);
                            FlightExclAmt = FlightExclAmt + Convert.ToDecimal(dtlRow["AirExclusiveFare"]);
                            FlightVat = FlightVat + Convert.ToDecimal(dtlRow["AirVatonFare"]);
                            AirportTaxes = AirportTaxes + Convert.ToDecimal(dtlRow["AirPortTaxes"]);

                            if (PrintStyleId == 0)
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirPnr"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirTicketNo"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'/>" + dtlRow["Details"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirExclusiveFare"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirPortTaxes"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirVatonFare"] + "</td>");

                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirClientTotal"] + "</td>");
                                sbMainrow.Append("</tr>");
                                Flight = 1;

                            }
                        }
                        //sbMainrow.Append("<tr>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Airport Taxes</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                        //sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td></tr>");

                        //  sbMainrow.Append("<tr>");
                        //  sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Air Tickets Total(Inclu Airport Taxes)</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //   sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + FlihgtClientTotal + "</td></tr>");
                        if (PrintStyleId == 0)
                        {
                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:3px;'>Air Tickets Total(Inclu Airport Taxes)</td>");
                            sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + FlihgtClientTotal + "</td></tr>");
                            //sbMainrow.Append("</tr>");

                        }

                    }
                    #endregion AirTicket

                    #region LandArrangement
                    if (objDs.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[2].Rows)
                        {
                            if (PrintStyleId == 0)
                            {
                                if (Land == 0)
                                {

                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Land Arrangement</td>");
                                    sbMainrow.Append("</tr>");


                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");

                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                    sbMainrow.Append("</tr>");
                                }
                            }


                            LandClientTotal = LandClientTotal + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim());
                            LandExclAmt = LandExclAmt + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["Excl Amt"].ToString().Trim()) ? ".00" : dtlRow["Excl Amt"].ToString().Trim());
                            LandVat = LandVat + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["VAT"].ToString().Trim()) ? ".00" : dtlRow["VAT"].ToString().Trim());




                            //LandClientTotal = LandClientTotal + LandClientTotal;
                            //LandExclAmt = LandExclAmt + LandExclAmt;
                            //LandVat = LandVat + LandVat;


                            if (PrintStyleId == 0)
                            {


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["LandArrId"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["BookRefNo"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["Excl Amt"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VAT"] + "</td>");

                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                                sbMainrow.Append("</tr>");
                                Land = 1;
                            }
                        }
                        //sbMainrow.Append("<tr>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandClientTotal + "</td></tr>");
                        if (PrintStyleId == 0)
                        {

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                            sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandClientTotal + "</td></tr>");
                        }


                    }

                    #endregion LandArrangement

                    #region ServiceFee

                    string merge = "";

                    if (objDs.Tables[3].Rows.Count > 0)
                    {

                        foreach (DataRow dtlRow in objDs.Tables[3].Rows)
                        {
                            merge = dtlRow["MergeC"].ToString();

                            if (dtlRow["MergeC"].ToString() == "0")
                            {
                                if (SF == 0)
                                {
                                    if (PrintStyleId == 0)
                                    {
                                        sbMainrow.Append("<tr>");
                                        sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Service Fee</td>");
                                        sbMainrow.Append("</tr>");


                                        sbMainrow.Append("<tr>");
                                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>SourceRef</td>");
                                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                        sbMainrow.Append("<td colspan='2' style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;text-align:center'>Excl Amt</td>");
                                        //sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");

                                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                        sbMainrow.Append("</tr>");
                                    }
                                }
                                string Clienttotal = string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim();

                                string ExcluAmount = string.IsNullOrEmpty(dtlRow["ExcluAmount"].ToString().Trim()) ? ".00" : dtlRow["ExcluAmount"].ToString().Trim();

                                string VatAmount = string.IsNullOrEmpty(dtlRow["VatAmount"].ToString().Trim()) ? ".00" : dtlRow["VatAmount"].ToString().Trim();


                                ServiceFeeClientTotal = ServiceFeeClientTotal + Convert.ToDecimal(Clienttotal);
                                ServiceFeeExclAmt = ServiceFeeExclAmt + Convert.ToDecimal(ExcluAmount);
                                ServiceFeeVat = ServiceFeeVat + Convert.ToDecimal(VatAmount);
                                if (PrintStyleId == 0)
                                {
                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Typ"] + "</td>");
                                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["TktNumber"] + "</td>");
                                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                                    sbMainrow.Append("<td colspan='2' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ExcluAmount"] + "</td>");
                                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");

                                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                                    sbMainrow.Append("</tr>");
                                    SF = 1;
                                }
                            }
                        }
                        //  if (merge == "0")
                        //   {
                        //sbMainrow.Append("<tr>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee Total</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + ServiceFeeClientTotal + "</td></tr>");

                        if (PrintStyleId == 0)
                        {
                            if (ServiceFeeClientTotal != 0)
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee Total</td>");
                                if (ServiceFeeClientTotal == 0)
                                {
                                    sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'> 0.00 </td></tr>");

                                }
                                else
                                {
                                    sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + ServiceFeeClientTotal + "</td></tr>");

                                }
                            }
                        }
                        //  }
                    }


                    #endregion Service

                    #region GeneralCharge
                    if (objDs.Tables[4].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[4].Rows)
                        {
                            if (PrintStyleId == 0)
                            {
                                if (GC == 0)
                                {

                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>General Charge</td>");
                                    sbMainrow.Append("</tr>");


                                    sbMainrow.Append("<tr>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");

                                    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                    sbMainrow.Append("</tr>");
                                }
                            }
                            GeneralChargeClienttotal = GeneralChargeClienttotal + Convert.ToDecimal(dtlRow["ClientTot"]);
                            GeneralChargeExclAmt = GeneralChargeExclAmt + Convert.ToDecimal(dtlRow["ExcluAmt"]);
                            GeneralChargeVat = GeneralChargeVat + Convert.ToDecimal(dtlRow["VatAmount"]);

                            if (PrintStyleId == 0)
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Type"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Ref"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ExcluAmt"] + "</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");

                                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTot"] + "</td>");
                                sbMainrow.Append("</tr>");
                                GC = 1;
                            }
                        }
                        //sbMainrow.Append("<tr>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>General Charge Total</td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + GeneralChargeClienttotal + "</td></tr>");
                        if (PrintStyleId == 0)
                        {

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:3px;'>General Charge Total</td>");
                            sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + GeneralChargeClienttotal + "</td></tr>");
                        }
                    }
                    #endregion GeneralCharge


                    decimal TotalInvExclAmt = FlightExclAmt + LandExclAmt + ServiceFeeExclAmt + GeneralChargeExclAmt;
                    decimal TotalVat = FlightVat + LandVat + ServiceFeeVat + GeneralChargeVat;
                    decimal TotalInclAmount = FlihgtClientTotal + LandClientTotal + ServiceFeeClientTotal + GeneralChargeClienttotal;

                    TotalInclAmount = Convert.ToDecimal(_objBOUtiltiy.FormatTwoDecimal(TotalInclAmount.ToString()));
                    // Invocie Total desing

                    //sbMainrow.Append("<tr>");
                    //sbMainrow.Append("<td colspan='7'><br/></td>");
                    //sbMainrow.Append("</tr>");


                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;text-align:center'>Summary<br/></td>");
                    sbMainrow.Append("</tr>");

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td colspan='3' style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Invoice Total</td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInvExclAmt + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalVat + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");
                    // Balance From you desing
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td colspan='6'  style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Total Due</td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");

                }

                readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());


                string StrContent = readFile;

                //GenerateHTML_TO_PDF(StrContent, true, "", false);

                string strFileName = "Invoice" + " " + invid;
                string strFileSavePath = Server.MapPath("../PdfDocuments/" + strFileName + ".pdf");

                GenerateHTML_TO_PDF(StrContent, true, strFileSavePath, true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        // }
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
            converter.Options.DisplayFooter = true;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");


            doc.Save(FileName);


            string FilePath = FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }


            //if (FileName != "")
            //    doc.Save(FileName);

            doc.Close();

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}