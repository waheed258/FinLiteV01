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

public partial class Admin_InvoiceList : System.Web.UI.Page
{
    EmInvoice objEmInvoice = new EmInvoice();
    BALInvoice objBALInvoice = new BALInvoice();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    BALTransactions _objBALTransactions = new BALTransactions();

    string readFile;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindInvoiceList();
            BindClienttypes();
            BindReceiptTypes();
            BindDivision();
            BindAutoDepositAccount();
            txtPreparedBy.Text = Session["UserFullName"].ToString();
            txtBody.Enabled = false;
            fuattachment.Enabled = false;


        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Invoice.aspx");
    }
    private void BindInvoiceList()
    {

        try
        {
 
            //int InvId = 0;
            gvInvoiceList.PageSize = int.Parse(ViewState["ps"].ToString());

            DataSet ds = objBALInvoice.GetInvoiceList();

            Session["dt"] = ds.Tables[0];
 

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvInvoiceList.DataSource = ds;

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

                gvInvoiceList.DataBind();
            }

            else
            {
                gvInvoiceList.DataSource = null;
                gvInvoiceList.DataBind();
                Label lblEmptyMessage = gvInvoiceList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    // Invoice List HTML 

    protected void PDFGenarateforEMail(int Invid, int companyid)
    {



        try
        {
            DataSet objDs = objBALInvoice.GetPdfDetails(Invid, companyid);




            StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/NewPdfInvoice.html"));
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

                            // string strImagPath = Server.MapPath("../images/" + dtlRow["comapnylogo"].ToString());
                            //   readFile = readFile.Replace("{Image}", "<img style='height:100px;width:150px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");
                            //readFile = readFile.Replace("{Image3}", "<img style='height:50px;width:70px;margin-left:100px;'  src='" + "http://demofin.swdtcpl.com/img/" + dtlRow["comapnylogo"].ToString() + "'></img>");

                            string strUrl = _BOUtility.LogoUrl();
                            readFile = readFile.Replace("{Image}", "<img   src='" + strUrl + "Logos/" + dtlRow["comapnylogo"].ToString() + "'></img>");
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
                        FlihgtClientTotal = FlihgtClientTotal + Convert.ToDecimal(dtlRow["AirClientTotal"]);
                        FlightExclAmt = FlightExclAmt + Convert.ToDecimal(dtlRow["AirExclusiveFare"]);
                        FlightVat = FlightVat + Convert.ToDecimal(dtlRow["AirVatonFare"]);
                        AirportTaxes = AirportTaxes + Convert.ToDecimal(dtlRow["AirPortTaxes"]);

                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirPnr"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["AirTicketNo"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'/>" + dtlRow["Details"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirExclusiveFare"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirPortTaxes"] + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AirVatonFare"] + "</td>");

                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'><br/><br/>" + dtlRow["AirClientTotal"] + "</td>");
                        sbMainrow.Append("</tr>");
                        Flight = 1;
                    }
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Airport Taxes</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                    sbMainrow.Append("<td colspan='7' style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td></tr>");

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Air Tickets Total(Inclu Airport Taxes)</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
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
                        LandClientTotal = LandClientTotal + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["ClientTotal"].ToString().Trim()) ? ".00" : dtlRow["ClientTotal"].ToString().Trim());
                        LandExclAmt = LandExclAmt + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["Excl Amt"].ToString().Trim()) ? ".00" : dtlRow["Excl Amt"].ToString().Trim());
                        LandVat = LandVat + Convert.ToDecimal(string.IsNullOrEmpty(dtlRow["VAT"].ToString().Trim()) ? ".00" : dtlRow["VAT"].ToString().Trim());


                        //LandClientTotal = LandClientTotal + LandClientTotal;
                        //LandExclAmt = LandExclAmt + LandExclAmt;
                        //LandVat = LandVat + LandVat;

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
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandClientTotal + "</td></tr>");

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

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Service Fee</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>SourceRef</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");

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
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0.00</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatAmount"] + "</td>");

                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ClientTotal"] + "</td>");
                            sbMainrow.Append("</tr>");
                            SF = 1;
                        }
                    }

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

                    //  }
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
                        GeneralChargeClienttotal = GeneralChargeClienttotal + Convert.ToDecimal(dtlRow["ClientTot"]);
                        GeneralChargeExclAmt = GeneralChargeExclAmt + Convert.ToDecimal(dtlRow["ExcluAmt"]);
                        GeneralChargeVat = GeneralChargeVat + Convert.ToDecimal(dtlRow["VatAmount"]);

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
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>General Charge Total</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + GeneralChargeClienttotal + "</td></tr>");

                }
                #endregion GeneralCharge


                decimal TotalInvExclAmt = FlightExclAmt + LandExclAmt + ServiceFeeExclAmt + GeneralChargeExclAmt;
                decimal TotalVat = FlightVat + LandVat + ServiceFeeVat + GeneralChargeVat;
                decimal TotalInclAmount = FlihgtClientTotal + LandClientTotal + ServiceFeeClientTotal + GeneralChargeClienttotal;

                TotalInclAmount = Convert.ToDecimal(_BOUtility.FormatTwoDecimal(TotalInclAmount.ToString()));
                // Invocie Total desing

                //sbMainrow.Append("<tr>");
                //sbMainrow.Append("<td colspan='7'><br/></td>");
                //sbMainrow.Append("</tr>");


                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'><br/></td>");
                sbMainrow.Append("</tr>");

                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Invoice Total</td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInvExclAmt + "</td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalVat + "</td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");
                // Balance From you desing
                sbMainrow.Append("<tr>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Total Due</td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");

            }


            readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    protected void btnSendmailSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // DataSet objDs = objBALInvoice.GetPdfDetails(Convert.ToInt32(lblID.Text));

            int comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());

            int mergeId = 0;
            string TempuniqCode = "";

            //     mergeId = Convert.ToInt32(objBALInvoice.GetServiceFeeMergeValue(Convert.ToInt32(lblID.Text), TempuniqCode));

            PDFGenarateforEMail(Convert.ToInt32(lblID.Text), comapnyId);



            string SmtpHost = "smtp.gmail.com";
            int SmtpPort = 587;
            string MailFrom = txtFrom.Text.ToString();

            string FromPassword = "testing123";
            string MailTo = txtTo.Text.ToString();
            string DisplayNameTo = "Ticket Admin";
            string MailCc = "";//"serendipityflightbookings@gmail.com" + "," + TravellerEmail;
            string DisplayNameCc = "";
            string MailBcc = "";

            string Subject = txtSubject.Text.ToString();


            //string MailText = "<!DOCTYPE html><html><body><h2>COMPUTER GENERATED TAX INVOICE</h2><table><tr><td>Document No</td><td>Hofinvoice143</td></tr><tr><td>Date</td><td>Wednesday,June 28,2017</td></tr><tr><td>Consultant</td><td>Bruce  Rodda</td></tr><tr><td>Client</td><td>Sales Travel & Tours</td></tr><tr><td>Currency</td><td>ZAR(South African Rand</td></tr></table><br /><table border='1'><tr><th>PRN</th><th>Ticket No</th><th>Passenger/Dep Date/Route/Class</th><th>Excl Amt</th><th>VAT</th><th>Incl Amt</th></tr><tr><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td></tr></table></body></html>";


            string MailText = readFile;

            string path = Server.MapPath("~/PdfDocuments");
            FileStream fStream;
            DirectoryInfo dir = new DirectoryInfo(path);
            string filename = "";
            string Attachment = "";
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                filename = flInfo.Name;
                string css = "Invoice " + lblID.Text + ".pdf";
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
    protected void imgPdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            //int invid = Convert.ToInt32(gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString());
            string invid = gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
            //Response.Redirect("InvoicePdf.aspx?id=" + invid);
            string url = "InvoicePdf.aspx?id=" + HttpUtility.UrlEncode(_BOUtility.Encrypts(invid, true));
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "_blank", fullURL, true);

            //ImageButton b = (ImageButton)gvInvoiceList.FindControl("imgSendMail");
            //b.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void imgSendMail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndetails = sender as ImageButton;

        var closeLink = (Control)sender;
        GridViewRow row = (GridViewRow)closeLink.NamingContainer;
        txtTo.Text = row.Cells[2].Text; // here we are

        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        lblID.Text = gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
        txtFrom.Text = "testingformail12@gmail.com";
        txtSubject.Text = "Invoice";
        txtFrom.Enabled = false;
        this.SendMailPopupExtender.Show();
    }



    protected void gvInvoiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();


        if (e.CommandName == "Edit Invoice")
        {
            //  int InvId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("Invoice.aspx?InvId=" + HttpUtility.UrlEncode(_BOUtility.Encrypts(id, true)));
        }
        if (e.CommandName == "Delete Invoice")
        {
            int InvoiceId = Convert.ToInt32(e.CommandArgument);
            deleteInvoice(InvoiceId);
            BindInvoiceList();
        }


    }
    protected void imgAccounting_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            //   txtTo.Text = row.Cells[2].Text; // here we are

            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblID.Text = gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
            int Invid = Convert.ToInt32(lblID.Text);
            //txtFrom.Text = "testingformail12@gmail.com";
            //txtSubject.Text = "Invoice";
            //txtFrom.Enabled = false;
            this.AccountAnalysisPopupExtender.Show();
            BindAcAnalyData(Invid);
            BindAcAnalysisDetails(Invid);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void BindAcAnalyData(int Invid)
    {
        try
        {
            //GetAccAnalysisData
            DataSet ds = objBALInvoice.GetAccAnalysisData(Invid);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in ds.Tables[0].Rows)
                {
                    lblAcanlysisclientname.Text = dtlRow["ClientName"].ToString();

                    decimal totalfare = string.IsNullOrEmpty(dtlRow["InvoiceTotal"].ToString()) ? (0.0M) : Convert.ToDecimal(dtlRow["InvoiceTotal"].ToString());

                    decimal invoicepaiedamt = string.IsNullOrEmpty(dtlRow["InvoicePaiedAmount"].ToString()) ? (0.0M) : Convert.ToDecimal(dtlRow["InvoicePaiedAmount"].ToString());


                    lblAcanAlysisTotalfare.Text = totalfare.ToString();

                    lblAcAnalysisCC.Text = invoicepaiedamt.ToString();

                    lblincrordecreAccRceivable.Text = (totalfare - invoicepaiedamt).ToString();

                    if (invoicepaiedamt > totalfare)
                    {
                        lblincrordecreAccRceivable.Attributes["style"] = "color:red;";
                        lblacReceivable.Attributes["style"] = "color:red;";
                    }




                    decimal tolcommiexclu = string.IsNullOrEmpty(dtlRow["TotalCommExclu"].ToString()) ? (0.0M) : Convert.ToDecimal(dtlRow["TotalCommExclu"].ToString());
                    lblAcAnalcommincome.Text = tolcommiexclu.ToString();

                    decimal tolvatamount = string.IsNullOrEmpty(dtlRow["TotalCommVatAmount"].ToString()) ? (0.0M) : Convert.ToDecimal(dtlRow["TotalCommVatAmount"].ToString());
                    lblAcAnaliability.Text = tolvatamount.ToString();
                    lblAccVatAmount.Text = (tolcommiexclu + tolvatamount).ToString();
                    decimal accPayable = Convert.ToDecimal(lblincrordecreAccRceivable.Text) - (tolcommiexclu + tolvatamount);
                    if (accPayable > 0)
                    {
                        lblAcAnalPayable.Text = accPayable.ToString();
                    }
                    else
                    {
                        accPayable = Math.Abs(accPayable);

                        lblAcAnalPayable.Text = '('+accPayable.ToString()+')';
                    }
                  


                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in ds.Tables[1].Rows)
                {
                    gvAccountanalysis.DataSource = ds.Tables[1];
                    gvAccountanalysis.DataBind();
                }
            }

            else
            {
                gvAccountanalysis.DataSource = null;
                gvAccountanalysis.DataBind();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void BindAcAnalysisDetails(int Invid)
    {
        try
        {
            DataSet ds = objBALInvoice.GetAccAnalysisDetails(Invid);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvAccAnalysisDetails.DataSource = ds;
                gvAccAnalysisDetails.DataBind();

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    //protected void gvAccAnalysisDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
    //protected void cmdClose_Click(object sender, ImageClickEventArgs e)
    //{
    //    AccountAnalysisPopupExtender.Hide();
    //}
    protected void btnCancleACAnalysis_Click(object sender, EventArgs e)
    {
        AccountAnalysisPopupExtender.Hide();
    }

    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //  BindInvoiceList();

    }
    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
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
                    "%' OR InvOrder LIKE '%" + SearchText +
                    "%' OR Convert(InvoiceTotal,'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvInvoiceList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvInvoiceList.DataSource = dr.CopyToDataTable();
                    gvInvoiceList.DataBind();
                }
                else
                {
                    gvInvoiceList.DataSource = null;
                    gvInvoiceList.DataBind();

                    Label lblEmptyMessage = gvInvoiceList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvInvoiceList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindInvoiceList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvInvoiceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoiceList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindInvoiceList();
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }

    private void deleteInvoice(int InvoiceId)
    {
        try
        {
            int result = objBALInvoice.DeleteInvoice(InvoiceId);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvInvoiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnTop = (ImageButton)e.Row.FindControl("imgDelete");
                ibtnTop.Enabled = false;
                ibtnTop.ToolTip = "Can't Delete Invoice";
                DataSet dss = objBALInvoice.Check_Payment_Deposit();

                if (dss.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in dss.Tables[0].Rows)
                    {
                        if (ibtnTop.CommandArgument == dtlRow["InvId"].ToString())
                        {
                            ibtnTop.Enabled = true;
                            ibtnTop.ImageUrl = "~/images/icon-delete.png";
                            ibtnTop.ToolTip = "Delete";
                        }
                    }
                }

                ImageButton ImgeReceiptBtn = (ImageButton)e.Row.FindControl("imgInvReceipt");
                lblID.Text = gvInvoiceList.DataKeys[e.Row.RowIndex].Value.ToString();
                int Invid = Convert.ToInt32(lblID.Text);
                DataSet invoiceData = objBALInvoice.GetInvoice(Invid);
                if (invoiceData.Tables[0].Rows.Count >= 1)
                {
                    if (invoiceData.Tables[0].Rows[0]["PaymentStatus"].ToString() == "1")
                    {
                        ImgeReceiptBtn.Visible = false;
                    }
                    else
                    {
                        ImgeReceiptBtn.Visible = true;
                    }
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            
           ExceptionLogging.SendExcepToDB(ex);
        }  
       
    }
    protected void imgInvReceipt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btndetails = sender as ImageButton;

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            //   txtTo.Text = row.Cells[2].Text; // here we are

            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblID.Text = gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
            int Invid = Convert.ToInt32(lblID.Text);
            DataSet invoiceData = objBALInvoice.GetInvoice(Invid);
            if (invoiceData.Tables[0].Rows.Count >= 1)
            {
                //ddlAccountNo.SelectedValue = invoiceData.Tables[0].Rows[0]["ClientNameId"].ToString();
                ///  string ss =   ddlAccountNo.Items.FindByValue(invoiceData.Tables[0].Rows[0]["ClientTypeId"].ToString()).Text;
                ddlClientType.SelectedIndex = ddlClientType.Items.IndexOf(ddlClientType.Items.FindByValue(invoiceData.Tables[0].Rows[0]["ClientTypeId"].ToString()));
                BindClientAccount(ddlClientType.SelectedItem.Value.ToString());
                ddlAccountNo.SelectedIndex = ddlAccountNo.Items.IndexOf(ddlAccountNo.Items.FindByValue(invoiceData.Tables[0].Rows[0]["ClientNameId"].ToString()));
                txtAmount.Text = invoiceData.Tables[0].Rows[0]["InvoiceOpenAmount"].ToString();
                ddlClientType.Enabled = false;
                ddlAccountNo.Enabled = false;
                txtAmount.Enabled = false;
            }
            else
            {

            }
            ReceiptPopupExtender.Show();
        }
        catch (Exception ex)
        {

            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void ddlClientType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClientType.SelectedIndex > 0)
        {
            BindClientAccount(ddlClientType.SelectedValue);
        }
        else
        {
            BindClientAccount("");
        }
    }


    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Clienttype = Convert.ToInt32(ddlClientType.SelectedValue);
        int ClientId = Convert.ToInt32(ddlAccountNo.SelectedValue);
        txtPayeeDetails.Text = ddlAccountNo.SelectedItem.Text;
        int Status = 0;
        //  lblAllocatedAmount.Text = "0.00";
        // BindInvoiceDetailsByClientAndStatus(Clienttype, ClientId, Status);
        ReceiptPopupExtender.Show();
    }
    private void BindClienttypes()
    {
        try
        {
            ddlClientType.Items.Clear();
            DataSet ObjDsClients = _BOUtility.GetClienttype();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlClientType.DataSource = ObjDsClients;
                ddlClientType.DataValueField = "Id";
                ddlClientType.DataTextField = "Name";

                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("Select Account", "0"));

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindClientAccount(string ClientType)
    {
        try
        {
            if (ClientType == "")
            {
                ddlAccountNo.Items.Clear();
                // ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
                return;
            }
            ddlAccountNo.Items.Clear();
            BAClients objBAClients = new BAClients();
            DataSet ObjDsClients = objBAClients.GetClientByClientType(ClientType);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAccountNo.DataSource = ObjDsClients;
                ddlAccountNo.DataValueField = "ClientId";
                ddlAccountNo.DataTextField = "ClientAccount";
                ddlAccountNo.DataBind();
                ReceiptPopupExtender.Show();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ReceiptPopupExtender.Show();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindDivision()
    {
        try
        {
            ddlDivision.Items.Clear();
            DataSet ObjDsClients = _BOUtility.GetDivisions();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataSource = ObjDsClients;
                ddlDivision.DataValueField = "DivisionId";
                ddlDivision.DataTextField = "DivName";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("Select Division", "0"));

            }
            else
            {
                ddlDivision.Items.Insert(0, new ListItem("Select Division", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindReceiptTypes()
    {
        try
        {
            ddlReceiptType.Items.Clear();
            int ReceiptId = 0;
            DataSet ObjDsClients = _BOUtility.GetReceiptTypes(ReceiptId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlReceiptType.DataSource = ObjDsClients;
                ddlReceiptType.DataValueField = "ReceiptId";
                ddlReceiptType.DataTextField = "RecDescription";
                ddlReceiptType.DataBind();
                ddlReceiptType.Items.Insert(0, new ListItem("Select ReceiptType", "0"));
            }
            else
            {
                ddlReceiptType.Items.Insert(0, new ListItem("Select ReceiptType", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindAutoDepositAccount()
    {
        try
        {
            ddlAutoDepositeAccount.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _BOUtility.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAutoDepositeAccount.DataSource = ObjDsClients;
                ddlAutoDepositeAccount.DataValueField = "BankAcId";
                ddlAutoDepositeAccount.DataTextField = "BankName";
                ddlAutoDepositeAccount.DataBind();
                ddlAutoDepositeAccount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlAutoDepositeAccount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void btnReciptSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Allocatedcount = 0;
            int result = 0;
            decimal ReceiptAmountAfterpaid = 0.0M;
            decimal PreviousAmountAfterpaid = 0.0M;

            DataSet objDsInvLst = objBALInvoice.GetInvoiceDetailsByClientAndStatus(Convert.ToInt32(ddlClientType.SelectedValue), Convert.ToInt32(ddlAccountNo.SelectedValue), 0);
            TransactionMaster objTransactionMaster = new TransactionMaster();
            objTransactionMaster.InvoiceId = Convert.ToInt32(Convert.ToInt32(lblID.Text));
            objTransactionMaster.Divission = ddlDivision.SelectedValue;
            objTransactionMaster.ReceiptType = ddlReceiptType.SelectedValue;
            objTransactionMaster.AutoDepositeId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedItem.Value);
            objTransactionMaster.AutoDepositeAccountNo = ddlAutoDepositeAccount.SelectedItem.Text;
            objTransactionMaster.ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
            objTransactionMaster.ClientAccountNo = ddlAccountNo.SelectedItem.Text;
            objTransactionMaster.ClientAccountNoID = Convert.ToInt32(ddlAccountNo.SelectedValue);
            objTransactionMaster.PayeeDetails = txtPayeeDetails.Text;

            if (objDsInvLst.Tables[1].Rows.Count > 0)
            {

                objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(objDsInvLst.Tables[1].Rows[0]["OpenAmount"].ToString());
            }




            objTransactionMaster.AllocatedAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            objTransactionMaster.InvoiceBalanceAmount = 0.0M;
            objTransactionMaster.Details = txtDetails.Text;
            objTransactionMaster.Messages = "";
            objTransactionMaster.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            objTransactionMaster.PaymentSourceRef = txtSourceRef.Text;


            if (ReceiptAmountAfterpaid != 0 || ReceiptAmountAfterpaid != 0.0M)
                objTransactionMaster.ReceiptAmount = ReceiptAmountAfterpaid;
            else
                objTransactionMaster.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            if (PreviousAmountAfterpaid != 0 || PreviousAmountAfterpaid != 0.0M)
                objTransactionMaster.PrvClientOpenAmount = PreviousAmountAfterpaid;
            else
                objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(objTransactionMaster.PrvClientOpenAmount);
            if (objTransactionMaster.PrvClientOpenAmount > objTransactionMaster.AllocatedAmount)
            {
                objTransactionMaster.ReceiptAmountAfterPaid = objTransactionMaster.ReceiptAmount;
                PreviousAmountAfterpaid = Math.Abs(objTransactionMaster.PrvClientOpenAmount - objTransactionMaster.AllocatedAmount);

            }
            else
            {
                objTransactionMaster.ReceiptAmountAfterPaid = Math.Abs(objTransactionMaster.ReceiptAmount + objTransactionMaster.PrvClientOpenAmount - objTransactionMaster.AllocatedAmount);
                PreviousAmountAfterpaid = 0.0M;
            }

            ReceiptAmountAfterpaid = objTransactionMaster.ReceiptAmountAfterPaid;
            objTransactionMaster.ReceiptBalanceAmount = ReceiptAmountAfterpaid + PreviousAmountAfterpaid;



            result = _objBALTransactions.ReceivedTransactionInsert(objTransactionMaster);






            if (Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue) != 0)
            {

                Transaction objTransaction = new Transaction();

                objTransaction.FmAccountNoId = Convert.ToInt32(ddlAccountNo.SelectedValue);
                objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                string category = "";
                DataSet ds = _objBALTransactions.Transaction_GetAccountsData(Convert.ToInt32(ddlAccountNo.SelectedValue), Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue), "RT", category);
                string FmAcccode = "";
                string FmMainAccCode = "";

                string RefMainAcc = "";
                string RefAccCode = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FmAcccode = ds.Tables[0].Rows[0]["AccCode"].ToString();
                    FmMainAccCode = ds.Tables[0].Rows[0]["MainAccCode"].ToString();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    RefAccCode = ds.Tables[1].Rows[0]["BankGiAccount"].ToString();
                    RefMainAcc = ds.Tables[1].Rows[0]["MainAccCode"].ToString();

                }


                objTransaction.DebitAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                objTransaction.FmAccountNO = FmAcccode;
                objTransaction.FmMainAccount = FmMainAccCode;
                objTransaction.ReferenceAccountNO = RefAccCode;
                objTransaction.CreditAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;
                objTransaction.ToMainAccount = RefMainAcc;
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "RT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                _objBALTransactions.TransactionInsert(objTransaction);
            }

            //objTransaction.CreditAmount = lblAllocatedAmount.Text != "" ? Convert.ToDecimal(lblAllocatedAmount.Text) : 0;
            //objTransaction.FmAccountNO = RefAccCode;
            //objTransaction.MainAccount = RefMainAcc;
            //objTransaction.ReferenceAccountNO = FmAcccode;
            //objTransaction.DebitAmount = 0;
            //objTransaction.ReferenceNo = txtSourceRef.Text;   

            //objTransaction.ReferenceType = "RT";
            //objTransaction.CreatedBy = 0;

            //objTransaction.BalanceAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
            //_objBALTransactions.TransactionInsert(objTransaction);



            if (ddlAutoDepositeAccount.SelectedValue != "0")
            {
                EmDepositMaster objEmDepositMaster = new EmDepositMaster();
                objEmDepositMaster.DepositDate = Convert.ToDateTime(txtDate.Text);
                objEmDepositMaster.DepositClientPrefix = Convert.ToInt32(ddlClientType.SelectedValue);
                objEmDepositMaster.DepositComments = "";
                objEmDepositMaster.DepositConsultant = Session["UserLoginId"].ToString();
                objEmDepositMaster.DepositRecieptType = Convert.ToInt32(ddlReceiptType.SelectedValue);
                objEmDepositMaster.DepositSourceRef = txtSourceRef.Text;
                objEmDepositMaster.TotalRecieptsDeposited = Convert.ToInt32(Allocatedcount);
                objEmDepositMaster.TotalDepositAmount = Convert.ToDecimal(txtAmount.Text);
                objEmDepositMaster.DepositAcId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);

                BADepositTransaction objBADepositTransaction = new BADepositTransaction();
                int DepositInsert = objBADepositTransaction.insertDepositMaster(objEmDepositMaster);

                if (DepositInsert > 0)
                {
                    lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Deposit Added Successfully");
                    // clearcontrols();



                    EMDepositChild objEMDepositChild = new EMDepositChild();
                    objEMDepositChild.ReceiptId = Convert.ToInt32(result);
                    objEMDepositChild.RecieptDate = Convert.ToDateTime(txtDate.Text);
                    objEMDepositChild.ReceiptType = ddlReceiptType.SelectedItem.Text;
                    objEMDepositChild.ReciptClient = (ddlClientType.SelectedItem.Text);
                    objEMDepositChild.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0; ;
                    objEMDepositChild.InvoiceId = Convert.ToInt32(lblID.Text);
                    objEMDepositChild.DepositAcId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                    objEMDepositChild.DepositTransMasterId = DepositInsert;
                    int childResult = objBADepositTransaction.insertDepositChild(objEMDepositChild);
                    if (childResult > 0)
                    {
                        lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Deposit Added Successfully");

                    }
                    else
                    {
                        lblMsg.Text = _BOUtility.ShowMessage("info", "Info", " Child Deposit Was not Added Successfully");
                    }


                }
                else
                {

                }






                OpenAmountDetails objOpenAmountDetails = new OpenAmountDetails();
                objOpenAmountDetails.ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
                objOpenAmountDetails.ClientNameId = Convert.ToInt32(ddlAccountNo.SelectedValue);
                objOpenAmountDetails.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0; ;
                objOpenAmountDetails.PrvOpenAmount = objTransactionMaster.PrvClientOpenAmount.ToString() != "" ? objTransactionMaster.PrvClientOpenAmount : 0;
                objOpenAmountDetails.AlocatedAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                objOpenAmountDetails.ReceiptOpenAmount = objTransactionMaster.PrvClientOpenAmount.ToString() != "" ? objTransactionMaster.PrvClientOpenAmount : 0;
                objOpenAmountDetails.SourceRef = txtSourceRef.Text;
                objOpenAmountDetails.ReceiptType = ddlReceiptType.SelectedValue;
                objOpenAmountDetails.FromAccount = ddlAccountNo.SelectedValue;
                objOpenAmountDetails.ToAccount = ddlAccountNo.SelectedValue;
                objOpenAmountDetails.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                int ChildResult = _objBALTransactions.OpenAmountDetailsInsertUpdateMaster(objOpenAmountDetails);
                if (ChildResult > 0)
                {
                    //ImageButton btndetails = sender as ImageButton;

                 
                    //GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                    //ImageButton ImgeReceiptBtn = (ImageButton)gvrow.FindControl("imgInvReceipt");

                    //lblID.Text = gvInvoiceList.DataKeys[gvrow.RowIndex].Value.ToString();
                    //int Invid = Convert.ToInt32(lblID.Text);
                    //DataSet invoiceData = objBALInvoice.GetInvoice(Invid);
                    //if (invoiceData.Tables[0].Rows.Count >= 1)
                    //{
                    //    if (invoiceData.Tables[0].Rows[0]["PaymentStatus"].ToString() == "1")
                    //    {
                    //        ImgeReceiptBtn.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        ImgeReceiptBtn.Visible = true;
                    //    }
                    //}
                    //else
                    //{

                    //}
                    Response.Redirect("InvoiceList.aspx");
                   
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}

