using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProformaInvoice : System.Web.UI.Page
{
    EmInvoice objEmInvoice = new EmInvoice();
    BALInvoice objBALInvoice = new BALInvoice();
    BALProformaInvoice objBALPFInvoice = new BALProformaInvoice();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    StreamReader reader; string readFile;
    DataSet objDs;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindPFInvoiceList();
            txtPFBody.Enabled = false;
            Profuattachment.Enabled = false;
        }
    }
    private void BindPFInvoiceList()
    {

        try
        {
            //int InvId = 0;
            gvPFInvoiceList.PageSize = int.Parse(ViewState["ps"].ToString());

            DataSet ds = objBALPFInvoice.GetPFInvoiceList();
            Session["dt"] = ds.Tables[0];

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvPFInvoiceList.DataSource = ds;

                string sortDirection = "ASC", sortExpression;
                if (ViewState["so"] != null)
                {
                    sortDirection = ViewState["so"].ToString();
                }
                if (ViewState["se"] != null)
                {
                    sortExpression = ViewState["se"].ToString();
                    ds.Tables[0].DefaultView.Sort = sortExpression + " " + sortDirection;
                }

                gvPFInvoiceList.DataBind();
            }

            else
            {
                gvPFInvoiceList.DataSource = null;
                gvPFInvoiceList.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    // Invoice List HTML 

    protected void PDFGenarateforEMail(int PFInvid, int companyid)
    {



        DataSet objDs = objBALPFInvoice.GetPFPdfDetails(PFInvid, companyid);


        // StreamReader reader = new StreamReader(Server.MapPath("../HtmlTemps/Invoice.html"));
        reader = new StreamReader(Server.MapPath("~/HtmlTemps/Invoice.html"));
        readFile = reader.ReadToEnd();
        reader.Close();

        StringBuilder sbMainrow = new StringBuilder();

        int ComapanyAddress = 0;
        int DocuHeader = 0;
        int Flight = 0;
        int Land = 0;
        int SF = 0;
        int GC = 0;

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
                        string strImagPath = Server.MapPath("../images/" + dtlRow["comapnylogo"].ToString());
                        readFile = readFile.Replace("{Image}", "<img style='height:100px;width:150px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                        readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");

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
                        readFile = readFile.Replace("{Invoice_No}", dtlRow["PFInvId"].ToString());
                        readFile = readFile.Replace("{Date}", dtlRow["InvDate"].ToString());
                        readFile = readFile.Replace("{Consultant}", dtlRow["ConsultantName"].ToString());
                        readFile = readFile.Replace("{Client1}", dtlRow["ClientName"].ToString());
                        readFile = readFile.Replace("{Client}", dtlRow["ClientName"].ToString());
                        readFile = readFile.Replace("{Currency}", dtlRow["currency"].ToString());
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
                    string A_Clienttotal = string.IsNullOrEmpty(dtlRow["AirClientTotal"].ToString().Trim()) ? ".00" : dtlRow["AirClientTotal"].ToString().Trim();
                    string B_Clienttotal = string.IsNullOrEmpty(dtlRow["AirExclusiveFare"].ToString().Trim()) ? ".00" : dtlRow["AirExclusiveFare"].ToString().Trim();
                    string C_Clienttotal = string.IsNullOrEmpty(dtlRow["AirVatonFare"].ToString().Trim()) ? ".00" : dtlRow["AirVatonFare"].ToString().Trim();
                    string D_Clienttotal = string.IsNullOrEmpty(dtlRow["AirPortTaxes"].ToString().Trim()) ? ".00" : dtlRow["AirPortTaxes"].ToString().Trim();

                    FlihgtClientTotal = FlihgtClientTotal + Convert.ToDecimal(A_Clienttotal);
                    FlightExclAmt = FlightExclAmt + Convert.ToDecimal(B_Clienttotal);
                    FlightVat = FlightVat + Convert.ToDecimal(C_Clienttotal);
                    AirportTaxes = AirportTaxes + Convert.ToDecimal(D_Clienttotal);

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirPnr"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirTicketNo"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'/>" + dtlRow["Details"] + "<br/><br/>AirPort Taxes<style='padding-left: 120px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirExclusiveFare"] + "<br/><br/> " + dtlRow["AirPortTaxes"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirVatonFare"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'><br/><br/>" + dtlRow["AirClientTotal"] + "</td>");
                    sbMainrow.Append("</tr>");
                    Flight = 1;
                }
                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>AirTickets Totals</td>");
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
                    string A_Land = string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim();
                    string B_Land = string.IsNullOrEmpty(dtlRow["Excl Amt"].ToString().Trim()) ? ".00" : dtlRow["Excl Amt"].ToString().Trim();
                    string C_Land = string.IsNullOrEmpty(dtlRow["VAT"].ToString().Trim()) ? ".00" : dtlRow["VAT"].ToString().Trim();

                    LandClientTotal = LandClientTotal + Convert.ToDecimal(A_Land);
                    LandExclAmt = LandExclAmt + Convert.ToDecimal(B_Land);
                    LandVat = LandVat + Convert.ToDecimal(C_Land);

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["LandArrId"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["BookRefNo"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["Excl Amt"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VAT"] + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                    sbMainrow.Append("</tr>");
                    Land = 1;
                }
                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Totals</td>");
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
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["TktNumber"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ExcluAmount"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                        sbMainrow.Append("</tr>");
                        Flight = 1;
                    }

                }
                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee Totals</td>");
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
                    string A_GenCharge = string.IsNullOrEmpty(dtlRow["ClientTot"].ToString().Trim()) ? ".00" : dtlRow["ClientTot"].ToString().Trim();
                    string B_GenCharge = string.IsNullOrEmpty(dtlRow["ExcluAmt"].ToString().Trim()) ? ".00" : dtlRow["ExcluAmt"].ToString().Trim();
                    string C_GenCharge = string.IsNullOrEmpty(dtlRow["VatAmount"].ToString().Trim()) ? ".00" : dtlRow["VatAmount"].ToString().Trim();
                    GeneralChargeClienttotal = GeneralChargeClienttotal + Convert.ToDecimal(A_GenCharge);
                    GeneralChargeExclAmt = GeneralChargeExclAmt + Convert.ToDecimal(B_GenCharge);
                    GeneralChargeVat = GeneralChargeVat + Convert.ToDecimal(C_GenCharge);


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
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>General Charge Totals</td>");
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
            sbMainrow.Append("<td colspan='7'><br/></td>");
            sbMainrow.Append("</tr>");

            sbMainrow.Append("<tr>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Invoice Totals</td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInvExclAmt + "</td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalVat + "</td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");
            // Balance From you desing
            sbMainrow.Append("<tr>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Balance From you</td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");

        }

        readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());

    }


    protected void PFSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            // DataSet objDs = objBALInvoice.GetPdfDetails(Convert.ToInt32(lblID.Text));

            int comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());

            int mergeId = 0;
            string TempuniqCode = "";

            //     mergeId = Convert.ToInt32(objBALInvoice.GetServiceFeeMergeValue(Convert.ToInt32(lblID.Text), TempuniqCode));

            PDFGenarateforEMail(Convert.ToInt32(lblPFID.Text), comapnyId);



            string SmtpHost = "smtp.gmail.com";
            int SmtpPort = 587;
            string MailFrom = txtPFFrom.Text.ToString();

            string FromPassword = "testing123";
            string MailTo = txtPFTo.Text.ToString();
            string DisplayNameTo = "Ticket Admin";
            string MailCc = "";//"serendipityflightbookings@gmail.com" + "," + TravellerEmail;
            string DisplayNameCc = "";
            string MailBcc = "";

            string Subject = txtPFSubject.Text.ToString();


            //string MailText = "<!DOCTYPE html><html><body><h2>COMPUTER GENERATED TAX INVOICE</h2><table><tr><td>Document No</td><td>Hofinvoice143</td></tr><tr><td>Date</td><td>Wednesday,June 28,2017</td></tr><tr><td>Consultant</td><td>Bruce  Rodda</td></tr><tr><td>Client</td><td>Sales Travel & Tours</td></tr><tr><td>Currency</td><td>ZAR(South African Rand</td></tr></table><br /><table border='1'><tr><th>PRN</th><th>Ticket No</th><th>Passenger/Dep Date/Route/Class</th><th>Excl Amt</th><th>VAT</th><th>Incl Amt</th></tr><tr><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td></tr></table></body></html>";


            string MailText = readFile;

            string path = Server.MapPath("~/PFPdfDocuments");
            FileStream fStream;
            DirectoryInfo dir = new DirectoryInfo(path);
            string filename = "";
            string Attachment = "";
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                filename = flInfo.Name;
                string css = "PFInvoice " + lblPFID.Text + ".pdf";
                if (filename == css)
                {
                    fStream = File.OpenRead(path + "\\" + filename.ToString());

                    Attachment = fStream.Name;
                    fStream.Close();
                    break;
                }
            }

            // string MailText = "<!DOCTYPE html><html lang='en'><head><meta charset='utf-8'><title>Invoice</title><style>.clearfix:after {content: '';display: table;clear: both;}</style></head><body style=' position: relative;width: 100%;height: 20%;margin: 0 auto;color: #555555;background: #FFFFFF;font-family: Arial, sans-serif;font-size: 14px;font-family: SourceSansPro;'><header class='clearfix' style='padding: 10px 0;margin-bottom: 20px;border-bottom: 1px solid #AAAAAA;'><div id='' style='float:right;'><h1 class=''>Rapid Acconting Program For Travel <br>Industry</h1><address>Albania<br>Flat No:A1,konadpur<br>Hyderabad<br>Telangana,500084</address></div><div id='' style='width:15%;'><img src='Untitled.png'></div></header><main><div id='details' class='clearfix' style='margin-bottom: 50px;'><center><strong><h2>COMPUTER GENERATED TAX INVOICE<h2></strong></center><div id='' style='float: right;text-align: right;'><h3></h3><div><span style='margin-right:100px;font-weight:bold;'>Document No</span><span>Hofinvoice143</span></div><div><span style='margin-right:98px;font-weight:bold'>Date</span><span>Wednesday,June 28,2017 </span></div><div><span style='margin-right:128px;font-weight:bold'>Consultant</span><span>Bruce Rodda</span></div><div><span style='margin-right:110px;font-weight:bold'>Client</span><span>Sales Travel & Tours</span></div><div><span style='margin-right:60px;font-weight:bold'>Currency</span><span>ZAR(South African Rand)</span></div></div></div><table style='border: 1px ridge black; width: 100%;margin-bottom: 20px;border-collapse:collapse;'><thead style='border: 1px ridge black;'><tr><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight; border-bottom: 1px ridge black;border-radius:5px;'>Prn</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight; border-bottom: 1px ridge black;'>Ticket No</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Passenger/Dep Date/Route/Class</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Excl Amt</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>VAT</th><th style='font-weight:bold;border:1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Incl Amt</th></tr></thead><tbody><tr><td style='border: 1px ridge black; font-weight:bold;padding:3px;'>157</td><td style='border: 1px ridge black; font-weight:bold;padding:3px;'>9148253270</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>HANIF/MUHAMMAD 27May<br>2016-01 January 1900<br>LAHORE-DOHA DOHA-<br>JOHANNESBURG Cls 0 <br>Airport Taxes<br><br><span>Ticket Totals</span></td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>8140.00<br><br><br>0.00<br><br>8140.00</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>2423.00<br><br><br><br><br>2423.00</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>10563.00<br><br><br>0.00<br><br>10563.00</td></tr></tbody></table><hr></main><footer><div style='width:33.3%;float:right;'><img src='4.jpg' style='margin-left:200px;'></div><div style='width:33.3%;float:right;'><img src='3.jpg' style='margin-left:100px;'></div><div style='width:33.3%;float:left;'><img src='2.jpg'></div></footer></body></html>";


            //string Sendmail = _BOUtility.SendEmail.SendEmail(SmtpHost, SmtpPort, MailFrom, FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
            //              DisplayNameCc, MailBcc,  MailText, "");

            bool Sendmail = _BOUtility.SendEmail(SmtpHost, SmtpPort, MailFrom, "", FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
                          DisplayNameCc, MailBcc, Subject, MailText, Attachment);

            lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Message Successfully Send..");
        }
        catch (Exception ex)
        {

            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    //protected void imgPdf_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btndetails = sender as ImageButton;
    //    GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
    //    int PFinvid = Convert.ToInt32(gvPFInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString());
    //    Response.Redirect("InvoicePdf.aspx?id=" + PFinvid);

    //}
    protected void imgSendMail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndetails = sender as ImageButton;

        var closeLink = (Control)sender;
        GridViewRow row = (GridViewRow)closeLink.NamingContainer;
        txtPFTo.Text = row.Cells[2].Text; // here we are

        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        lblPFID.Text = gvPFInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
        txtPFFrom.Text = "testingformail12@gmail.com";
        txtPFSubject.Text = "Proforma Invoice";
        txtPFFrom.Enabled = false;
        this.SendMailPopupExtender.Show();
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaInvoice.aspx");
    }
    protected void imgPFPdf_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        int PFInvId = Convert.ToInt32(gvPFInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString());
       // Response.Redirect("ProformaInvoicePdf.aspx?id=" + PFInvId);
        string url = "ProformaInvoicePdf.aspx?id=" + PFInvId;
        string fullURL = "window.open('" + url + "', '_blank');";
        ScriptManager.RegisterStartupScript(this, typeof(string), "_blank", fullURL, true);
    }
    protected void gvPFInvoiceList_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["se"] = e.SortExpression;
            if (ViewState["so"] == null)
                ViewState["so"] = "ASC";
            if (ViewState["so"].ToString() == "ASC")
                ViewState["so"] = "DESC";
            else
                ViewState["so"] = "ASC";
            BindPFInvoiceList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void PFimgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "InvDate='" + SearchText +
                    "' OR clientemail LIKE '%" + SearchText +
                    "%' OR clientname LIKE '%" + SearchText +
                    "%' OR consultantName LIKE '%" + SearchText +
                    "%' OR PFInvOrder LIKE '%" + SearchText +
                    "%' OR Convert(PFInvoiceTotal,'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvPFInvoiceList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvPFInvoiceList.DataSource = dr.CopyToDataTable();
                    gvPFInvoiceList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvPFInvoiceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPFInvoiceList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
    }
}