using BusinessManager;
using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProformaInvoice : System.Web.UI.Page
{
    DOUtility _doUtilities = new DOUtility();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    EmAirTicket _objAirTicket = new EmAirTicket();
    EmAirticketRouting _objAirtickRouting = new EmAirticketRouting();

    EmInvoice _objEmInvoice = new EmInvoice();
    DALInvoice _objDalInvoice = new DALInvoice();
    BALInvoice _objBalInvoice = new BALInvoice();

    EMProformaInvoice _objEMPFInvoice = new EMProformaInvoice();
    BALProformaInvoice _objBALPFInvoice = new BALProformaInvoice();
    DALProformaInvoice _objDALPFInvoice = new DALProformaInvoice();


    BALAirTicket _objBALAirTicket = new BALAirTicket();

    BALServicefee _objBalservice = new BALServicefee();
    DALServicefee _objDalService = new DALServicefee();
    DALGeneralCharge _objDalGenchrge = new DALGeneralCharge();
    EMGeneralCharge _objEmGenCharge = new EMGeneralCharge();

    EMServicefee _objEmService = new EMServicefee();
    decimal ExclusiveAmount;

    //land
    EMLandarrangement objEMLandarrangement = new EMLandarrangement();
    DALandarrangement objDALandarrangement = new DALandarrangement();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            lblPFRoutes1.Enabled = false;
            lblPFRoutes2.Enabled = false;
            lblPFRoutes3.Enabled = false;
            lblPFRoutes4.Enabled = false;
            txtPFAirCommInclu.Enabled = false;
            txtPFVatPer.Enabled = false;
            txtPFAirVatOnFare.Enabled = false;
            txtPFAirClientTot.Enabled = false;
            txtPFAirDueToBsp.Enabled = false;
            txtPFAirCommVat.Enabled = false;
            txtPFAirAgentVat.Enabled = false;
            ddlPFInvMesg.Enabled = false;
            BindPFTypes();
            BindPFAirServiceTypes();
            BindPFAirLine();

            txtPFDate1.Enabled = false;
            txtPFDate2.Enabled = false;
            txtPFDate3.Enabled = false;
            txtPFDate4.Enabled = false;
            txtPFFlightNo1.Enabled = false;
            txtPFFlightNo2.Enabled = false;
            txtPFFlightNo3.Enabled = false;
            txtPFFlightNo4.Enabled = false;
            txtPFClass1.Enabled = false;
            txtPFClass2.Enabled = false;
            txtPFClass3.Enabled = false;
            txtPFClass4.Enabled = false;







            //general Charge
            DataSet objDs = null; // Single sp
            BindPFGenServiceTypes();

            BindPFGenCreditCardType();
            txtPFRateNet.Enabled = false;
            txtPFVatAmount.Enabled = false;
            txtPFExcluAmount.Enabled = false;
            txtPFClientTotal.Enabled = false;
            //service fee---
            BindPFSerServiceTypes();
            ddlPFPassengerName.Enabled = false;

            BindPFPaymentType();

            ddlPFCreditCardType.Enabled = false;
            ddlPFCollectVia.Enabled = false;
            txtPFTASFMPD.Enabled = false;
            txtPFClientTotal.Enabled = false;
            rfvtxtPFTASFMPD.Enabled = false;
            rfvddlPFCollectVia.Enabled = false;
            rfvddlPFCreditCardType.Enabled = false;
            //land
            BindPFLandSuppliers();
            BindPFType();
            BindPFLandPaymentType();
            BindPFLandService();
            DDPFlandCreditCard.Enabled = false;
            txtPFLandVatPer.Enabled = false;
            txtPFLandExlVatPer.Enabled = false;
            txtPFlandDuefromclient.Enabled = false;
            txtPFlandLessComm.Enabled = false;
            txtPFlandDuetoSupplier.Enabled = false;
            txtPFlandCommIncl.Enabled = false;
            txtPFlandVatAmount.Enabled = false;
            txtPFlandCommExcl.Enabled = false;
            txtPFlandExclVatAmount.Enabled = false;
            txtPFlandCmblIncl.Enabled = false;
            txtPFlandcmblExcl.Enabled = false;
            txtPFlandTotalcmblIncl.Enabled = false;
            Session["TempUniqCode"] = "";
            BindPFInvoiceLineItems();
            BindPFInvoiceLineItemsCount();
            //invoice Loading
            BindPFInvoiceDropDown();

        }
    }
    protected void cmdClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            PFAirticketClear();
            AirPFPopupExtender.Hide();

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void txtPFExcluisvefare_TextChanged(object sender, EventArgs e)
    {
        try
        {
            exclPFVatCal();
            AirPFPopupExtender.Show();
            if (txtPFAirCommisionper.Text != "")
            {
                txtPFAirCommisionper_TextChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void PFExcusiveAmount()
    {
        decimal ClientTotal = Convert.ToDecimal(txtPFSerClientTotal.Text);

        string VatPer = txtPFSerVatPer.Text;

        if (VatPer.Length > 1)
        {
            VatPer = VatPer.Substring(0, VatPer.Length - 3);
        }
        else
        {
            VatPer = "0";
        }
        var ts = "1." + VatPer;

        decimal dm = Convert.ToDecimal(ts.ToString());
        decimal exclAmount = (ClientTotal / dm);
        decimal vatamount = (ClientTotal - exclAmount);

        txtPFExclusAmount.Text = _objBOUtiltiy.FormatTwoDecimal(exclAmount.ToString());
        txtPFSerVatAmount.Text = vatamount.ToString();
        PFSerPopupExtender.Show();
    }
    protected void getClientTotal()
    {
        try
        {
            ExclusiveAmount = Convert.ToDecimal(txtPFExclusAmount.Text);

            // txtSerClientTotal.Text = txtExclusAmount.Text;

            if (txtPFSerVatPer.Text == "")
            {
                txtPFClientTotal.Text = ExclusiveAmount.ToString();
            }
            else
            {
                PFExcusiveAmount();
                //decimal Vatper = Convert.ToDecimal(txtSerVatPer.Text);
                //decimal vatAmount = ((Vatper / 100) * ExclusiveAmount);
                ////decimal clientTotal = ExclusiveAmount + vatAmount;
                //decimal clientTotal = ExclusiveAmount;
                //txtSerVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(vatAmount.ToString());
                //txtSerClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
                PFSerPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlPFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPFVatByType();
            exclPFVatCal();
            if (txtPFAirCommisionper.Text != "")
            {
                txtPFAirCommisionper_TextChanged(null, null);
            }

            AirPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindPFAirServiceTypes()
    {
        try
        {
            string Type = "Air";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);
            ViewState["ddlPFAirService"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFAirService.DataSource = ds.Tables[0];
                ddlPFAirService.DataTextField = "ComDesc";
                ddlPFAirService.DataValueField = "ComId";
                ddlPFAirService.DataBind();
                ddlPFAirService.Items.Insert(0, new ListItem("--Select Service--", "0"));

            }
            else
            {
                ddlPFAirService.DataSource = null;
                ddlPFAirService.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFAirLine()
    {
        try
        {
            int supplierid = 0;
            BAAirSuppliers _boAirSupplier = new BAAirSuppliers();
            DataSet ds = new DataSet();
            ds = _boAirSupplier.GetAirSuppliers(supplierid);
            ViewState["PFCommissionBasedonAirline"] = ds.Tables[2];

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFAirLine.DataSource = ds.Tables[0];
                ddlPFAirLine.DataTextField = "SupplierName";
                ddlPFAirLine.DataValueField = "SupplierId";
                ddlPFAirLine.DataBind();
                ddlPFAirLine.Items.Insert(0, new ListItem("--Select AirLine--", "0"));

            }
            else
            {
                ddlPFAirLine.DataSource = null;
                ddlPFAirLine.DataBind();
            }

            DataSet dsvat = _doUtilities.getVatByType();

            if (dsvat.Tables[0].Rows.Count > 0)
            {
                ViewState["PFget_VatRateByType"] = dsvat.Tables[0];
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void exclPFVatCal()
    {
        try
        {

            decimal clientmount;

            if (txtPFAirExcluisvefare.Text != "")
            {
                decimal exclusiveFare = Convert.ToDecimal(txtPFAirExcluisvefare.Text);
                txtPFAirExcluisvefare.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFAirExcluisvefare.Text);
                if (txtPFAirportTax.Text != "")
                {
                    clientmount = Convert.ToDecimal(txtPFAirExcluisvefare.Text) + Convert.ToDecimal(txtPFAirportTax.Text);
                    txtPFAirClientTot.Text = _objBOUtiltiy.FormatTwoDecimal(clientmount.ToString());
                }
                else
                {
                    txtPFAirClientTot.Text = txtPFAirExcluisvefare.Text;
                }
            }

            if (txtPFVatPer.Text != "" && txtPFAirExcluisvefare.Text != "")
            {
                decimal inclusiveAmount = Convert.ToDecimal(GlobalClass.exclVatSum(txtPFAirExcluisvefare.Text, txtPFVatPer.Text));
                // decimal inclusiveAmount = (exclusiveFare * Convert.ToDecimal(txtVatPer.Text)) / 100;
                txtPFAirVatOnFare.Text = inclusiveAmount.ToString();
                if (txtPFAirportTax.Text != "")
                {

                    txtPFAirClientTot.Text = (inclusiveAmount + Convert.ToDecimal(txtPFAirExcluisvefare.Text) + Convert.ToDecimal(txtPFAirportTax.Text)).ToString();
                }
                else
                {
                    txtPFAirClientTot.Text = (inclusiveAmount + Convert.ToDecimal(txtPFAirExcluisvefare.Text)).ToString();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    private void BindPFTypes()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.get_Type();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFType.DataSource = ds.Tables[0];
                ddlPFType.DataTextField = "TypeName";
                ddlPFType.DataValueField = "TypeId";
                ddlPFType.DataBind();
                ddlPFType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }
            else
            {
                ddlPFType.DataSource = null;
                ddlPFType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFVatByType()
    {
        try
        {
            if (ViewState["PFget_VatRateByType"].ToString() != null)
            {
                DataTable dt = (DataTable)ViewState["PFget_VatRateByType"];
                //string vatRate = Convert.ToString(_doUtilities.getVatByType(Convert.ToInt32(ddlType.SelectedValue)));
                string vatRate = (dt.AsEnumerable()
                    .Where(p => p["TypeId"].ToString() == Convert.ToInt32(ddlPFType.SelectedValue).ToString())
                    .Select(p => p["VatRate"].ToString())).FirstOrDefault();

                txtPFAirCommVat.Text = vatRate;
                txtPFVatPer.Text = vatRate;
            }

           

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void commPFExcAmount()
    {
        try
        {
            if (txtPFAirExcluisvefare.Text != "" && txtPFAirCommisionper.Text != "")
            {
                decimal exclusiveFare = Convert.ToDecimal(txtPFAirExcluisvefare.Text);
                txtPFAirCommExclu.Text = ((Convert.ToDecimal(txtPFAirCommisionper.Text) * exclusiveFare) / 100).ToString();


            }

            if (txtPFAirCommExclu.Text != "" && txtPFAirCommVat.Text != "")
            {
                decimal commisionInclAmount = Convert.ToDecimal(GlobalClass.exclVatSum(txtPFAirCommExclu.Text, txtPFAirCommVat.Text));
                // decimal inclusiveAmount = (exclusiveFare * Convert.ToDecimal(txtVatPer.Text)) / 100;
                txtPFAirAgentVat.Text = commisionInclAmount.ToString();
                txtPFAirCommInclu.Text = (commisionInclAmount + Convert.ToDecimal(txtPFAirCommExclu.Text)).ToString();
                txtPFAirDueToBsp.Text = (Convert.ToDecimal(txtPFAirClientTot.Text) - Convert.ToDecimal(txtPFAirCommInclu.Text)).ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnPFInvSave_Click(object sender, EventArgs e)
    {

        try
        {
            InsertPFInvoice();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    private void InsertPFInvoice()
    {

        try
        {
            _objEMPFInvoice.PFInvDate = string.IsNullOrEmpty(txtPFInvDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPFInvDate.Text);
            _objEMPFInvoice.ClientTypeId = Convert.ToInt32(drpPFInvClientType.SelectedValue.ToString());
            _objEMPFInvoice.ClientNameId = Convert.ToInt32(drpPFInvClientName.SelectedValue.ToString());

            _objEMPFInvoice.ConsultantId = Convert.ToInt32(ddlPFInvCosultant.SelectedValue.ToString());
            _objEMPFInvoice.PFInvOrder = string.IsNullOrEmpty(txtPFInvOrder.Text) ? "" : (txtPFInvOrder.Text);
            _objEMPFInvoice.BookingNo = string.IsNullOrEmpty(txtPFInvBookNo.Text) ? 0 : Convert.ToInt32(txtPFInvOrder.Text);
            _objEMPFInvoice.BookSourceId = Convert.ToInt32(drpPFInvBookingSrc.SelectedValue.ToString());
            _objEMPFInvoice.BookDestinationId = Convert.ToInt32(drpPFInvBookDest.SelectedValue.ToString());

            _objEMPFInvoice.PFInvoiceTotal = Convert.ToDecimal(txtPFInvoiceTotalAmount.Text);
            _objEMPFInvoice.AirTotal = Convert.ToDecimal(string.IsNullOrEmpty(txtPFAirClientTot.Text) ? 0 : Convert.ToDecimal(txtPFAirClientTot.Text));
            _objEMPFInvoice.LandTotal = Convert.ToDecimal(string.IsNullOrEmpty(txtPFlandDuefromclient.Text) ? 0 : Convert.ToDecimal(txtPFlandDuefromclient.Text));
            _objEMPFInvoice.ServiceTot = Convert.ToDecimal(string.IsNullOrEmpty(txtPFSerClientTotal.Text) ? 0 : Convert.ToDecimal(txtPFSerClientTotal.Text));
            _objEMPFInvoice.GenChargeTot = Convert.ToDecimal(string.IsNullOrEmpty(txtPFClientTotal.Text) ? 0 : Convert.ToDecimal(txtPFClientTotal.Text));
            _objEMPFInvoice.Message = txtPFInvClntMesg.Text;
            _objEMPFInvoice.MessageType = (string.IsNullOrEmpty(ddlPFInvMesg.SelectedValue.ToString())) ? 0 : Convert.ToInt32(ddlPFInvMesg.SelectedValue.ToString());

            _objEMPFInvoice.TempUniqCode = Session["TempUniqCode"].ToString();
            //_objEmInvoice.Message = string.IsNullOrEmpty(txtAirRouting.Text) ? "" : txtAirRouting.Text;
            //_objEmInvoice.AirMiles = string.IsNullOrEmpty(txtAirMiles.Text) ? "" : txtAirMiles.Text;

            //_objEmInvoice.AirTravelDate = string.IsNullOrEmpty(txtAirTravelDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtAirTravelDate.Text);
            //_objEmInvoice.AirReturnDate = string.IsNullOrEmpty(txtAirReturnDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtAirReturnDate.Text);

            //_objEmInvoice.AirExclusiveFare = string.IsNullOrEmpty(txtAirExcluisvefare.Text) ? (0.0M) : Convert.ToDecimal(txtAirExcluisvefare.Text);
            //_objEmInvoice.AirVatonFare = string.IsNullOrEmpty(txtAirVatOnFare.Text) ? (0.0M) : Convert.ToDecimal(txtAirVatOnFare.Text);

            //_objAirTicket.AirportTaxes = string.IsNullOrEmpty(txtAirportTax.Text) ? (0.0M) : Convert.ToDecimal(txtAirportTax.Text);
            //_objAirTicket.AirVatInTaxes = string.IsNullOrEmpty(txtAirVatinclTax.Text) ? (0.0M) : Convert.ToDecimal(txtAirVatinclTax.Text);
            //_objAirTicket.AirClientTotal = string.IsNullOrEmpty(txtAirClientTot.Text) ? (0.0M) : Convert.ToDecimal(txtAirClientTot.Text);
            //_objAirTicket.AirPayment = Convert.ToInt32(drpAirPayment.SelectedValue.ToString());


            //// _objAirTicket.AirCreditCardType = Convert.ToDecimal(txtAirportTax.Text);
            //_objAirTicket.AirCommPer = string.IsNullOrEmpty(txtAirCommisionper.Text) ? (0.0M) : Convert.ToDecimal(txtAirCommisionper.Text);
            //_objAirTicket.AirCommExcl = string.IsNullOrEmpty(txtAirCommExclu.Text) ? (0.0M) : Convert.ToDecimal(txtAirCommExclu.Text);
            //_objAirTicket.AirCommVatPer = string.IsNullOrEmpty(txtAirCommVat.Text) ? (0.0M) : Convert.ToDecimal(txtAirCommVat.Text);

            //_objAirTicket.AirAgentVat = string.IsNullOrEmpty(txtAirAgentVat.Text) ? (0.0M) : Convert.ToDecimal(txtAirAgentVat.Text);
            //_objAirTicket.AirCommInclu = string.IsNullOrEmpty(txtAirCommInclu.Text) ? (0.0M) : Convert.ToDecimal(txtAirCommInclu.Text);
            //_objAirTicket.AirDueToBsp = string.IsNullOrEmpty(txtAirDueToBsp.Text) ? 0 : Convert.ToDecimal(txtAirDueToBsp.Text);
            //_objAirTicket.TempUniqCode = uniqueIdSession();
            _objEMPFInvoice.CreatedBy = 0;
            _objEMPFInvoice.invDocumentNo = "0";


            if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
            {
                Session["TempUniqCode"] = uniqueIdSession();
                _objEMPFInvoice.TempUniqCode = Session["TempUniqCode"].ToString();

                _objEMPFInvoice.invDocumentNo = "Hfo." + Session["TempUniqCode"].ToString();
            }
            else
            {
                _objEMPFInvoice.TempUniqCode = Session["TempUniqCode"].ToString();

                _objEMPFInvoice.invDocumentNo = "Hfo." + Session["TempUniqCode"].ToString();
            }




            int Result = _objBALPFInvoice.InsertPFInvoice(_objEMPFInvoice);

            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Invoice created Successfully");
                // clearcontrols();
                int UpdateInvResult = _objBALPFInvoice.UpdatePFInvId(Result, Session["TempUniqCode"].ToString());
                if (UpdateInvResult > 0)
                {
                    //lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "AirTicket created Successfully");
                    Response.Redirect("ProformaInvoiceList.aspx");
                    Session["TempUniqCode"] = "";

                }

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice  was not created plase try again");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void InsertPFAirTicket()
    {

        try
        {
            _objAirTicket.AirTicketNo = string.IsNullOrEmpty(txtPFAirTicketNo.Text) ? "" : txtPFAirTicketNo.Text;
            _objAirTicket.Type = Convert.ToInt32(ddlPFType.SelectedValue.ToString());
            _objAirTicket.AirPnr = string.IsNullOrEmpty(txtPFPnr.Text) ? "" : txtPFPnr.Text;
            _objAirTicket.AirPassenger = string.IsNullOrEmpty(drpPFAirPassenger.Text) ? "" : drpPFAirPassenger.Text;
            _objAirTicket.Conjunction = string.IsNullOrEmpty(txtPFAirConjunction.Text) ? "" : txtPFAirConjunction.Text;
            _objAirTicket.Airline = Convert.ToInt32(ddlPFAirLine.SelectedValue.ToString());
            _objAirTicket.AirService = Convert.ToInt32(ddlPFAirService.SelectedValue.ToString());
            _objAirTicket.AirTicketType = Convert.ToInt32(drpPFTicketType.SelectedValue.ToString());

            _objAirTicket.AirRouting = string.IsNullOrEmpty(txtPFAirRouting.Text) ? "" : txtPFAirRouting.Text;
            _objAirTicket.AirMiles = string.IsNullOrEmpty(txtPFAirMiles.Text) ? "" : txtPFAirMiles.Text;

            _objAirTicket.AirTravelDate = string.IsNullOrEmpty(txtPFAirTravelDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPFAirTravelDate.Text);
            _objAirTicket.AirReturnDate = string.IsNullOrEmpty(txtPFAirReturnDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPFAirReturnDate.Text);

            _objAirTicket.AirExclusiveFare = string.IsNullOrEmpty(txtPFAirExcluisvefare.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirExcluisvefare.Text);
            _objAirTicket.AirVatonFare = string.IsNullOrEmpty(txtPFAirVatOnFare.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirVatOnFare.Text);

            _objAirTicket.AirportTaxes = string.IsNullOrEmpty(txtPFAirportTax.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirportTax.Text);
            _objAirTicket.AirVatInTaxes = string.IsNullOrEmpty(txtPFAirVatinclTax.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirVatinclTax.Text);
            _objAirTicket.AirClientTotal = string.IsNullOrEmpty(txtPFAirClientTot.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirClientTot.Text);
            _objAirTicket.AirPayment = Convert.ToInt32(ddlPFAirPayment.SelectedValue.ToString());


            // _objAirTicket.AirCreditCardType = Convert.ToDecimal(txtAirportTax.Text);
            _objAirTicket.AirCommPer = string.IsNullOrEmpty(txtPFAirCommisionper.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirCommisionper.Text);
            _objAirTicket.AirCommExcl = string.IsNullOrEmpty(txtPFAirCommExclu.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirCommExclu.Text);
            _objAirTicket.AirCommVatPer = string.IsNullOrEmpty(txtPFAirCommVat.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirCommVat.Text);

            _objAirTicket.AirAgentVat = string.IsNullOrEmpty(txtPFAirAgentVat.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirAgentVat.Text);
            _objAirTicket.AirCommInclu = string.IsNullOrEmpty(txtPFAirCommInclu.Text) ? (0.0M) : Convert.ToDecimal(txtPFAirCommInclu.Text);
            _objAirTicket.AirDueToBsp = string.IsNullOrEmpty(txtPFAirDueToBsp.Text) ? 0 : Convert.ToDecimal(txtPFAirDueToBsp.Text);
            _objAirTicket.SupplOpenAmount = string.IsNullOrEmpty(txtPFAirDueToBsp.Text) ? 0 : Convert.ToDecimal(txtPFAirDueToBsp.Text);
            _objAirTicket.InvoiceType = "Air";
            _objAirTicket.invDocumentNo = "0";
            if (Session["RoutTempID"] == null || Session["RoutTempID"] == "")
            {
                _objAirTicket.AirRoutTempID = uniqueIdSession();
            }
            else
            {
                _objAirTicket.AirRoutTempID = Session["RoutTempID"].ToString();
            }
            if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
            {
                _objAirTicket.TempUniqCode = uniqueIdSession();
            }
            else
            {
                _objAirTicket.TempUniqCode = Session["TempUniqCode"].ToString();
            }


            _objAirTicket.CreatedBy = 0;

            int Result = _objBALAirTicket.InsertAirTicket(_objAirTicket);

            //if (Result > 0)
            //{
            //    lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "AirTicket created Successfully");
            //    // clearcontrols();
            //    Session["TempUniqCode"] = _objAirTicket.TempUniqCode;
            //    BindPFInvoiceLineItems();
            //    BindPFInvoiceLineItemsCount();
            //    PFAirticketClear();
            //    ddlPFSoureceref.Items.Clear();
            //    ddlPFPassengerName.Items.Clear();
            //    BindPFSerTicketNumber();
            //    ddlPFPassengerNames.Items.Clear();
            //    BindPFGenPassengerNames();
            //}
            //else
            //{
            //    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "AirTicket Details was not created plase try again");
            //}
            //  AirPFPopupExtender.Hide();
            Session["TempUniqCode"] = _objAirTicket.TempUniqCode;

            Session["RoutTempID"] = _objAirTicket.AirRoutTempID;

            for (int i = 1; i <= 4; i++)
            {
                //_objAirtickRouting.ClassId = 0;
                //if (btnAirSubmit.Text == "Update")
                //{

                //    if (i == 1)
                //    {
                //        _objAirtickRouting.ClassId = Convert.ToInt32(hf_Rout_Id1.Value);
                //    }
                //    else if (i == 2)
                //    {
                //        _objAirtickRouting.ClassId = Convert.ToInt32(hf_Rout_Id2.Value);
                //    }
                //    else if (i == 3)
                //    {
                //        _objAirtickRouting.ClassId = Convert.ToInt32(hf_Rout_Id3.Value);
                //    }
                //    else
                //    {
                //        _objAirtickRouting.ClassId = Convert.ToInt32(hf_Rout_Id4.Value);
                //    }

                //}
                // TextBox flight = FindControl(string.Concat("txtFlightNo", i.ToString())) as TextBox;

                TextBox Routs = (TextBox)updatepanelContacts.FindControl("lblPFRoutes" + i);

                _objAirtickRouting.Routs = string.IsNullOrEmpty(Routs.Text) ? "" : Routs.Text;


                TextBox txt = (TextBox)updatepanelContacts.FindControl("txtPFFlightNo" + i);

                _objAirtickRouting.FlightNo = string.IsNullOrEmpty(txt.Text) ? "" : txt.Text;

                TextBox classes = (TextBox)updatepanelContacts.FindControl("txtPFClass" + i);



                _objAirtickRouting.Class = string.IsNullOrEmpty(classes.Text) ? "" : classes.Text;


                TextBox routing = (TextBox)updatepanelContacts.FindControl("txtPFAirRouting");

                _objAirtickRouting.Routing = string.IsNullOrEmpty(routing.Text) ? "" : routing.Text;


                TextBox miles = (TextBox)updatepanelContacts.FindControl("txtPFAirMiles");
                _objAirtickRouting.Miles = string.IsNullOrEmpty(miles.Text) ? "" : miles.Text;

                //    _objAirtickRouting.AirticketId = _objAirTicket.TicketId;
                _objAirtickRouting.InvoiceId = 0;
                _objAirtickRouting.TicketType = "Air";
                _objAirtickRouting.TempUniqCode = Session["RoutTempID"].ToString();

                TextBox date = (TextBox)updatepanelContacts.FindControl("txtPFDate" + i);
                //_objAirtickRouting.Date = Convert.ToDateTime(string.IsNullOrEmpty(date.Text) ? "0" : date.Text);

                _objAirtickRouting.Date = string.IsNullOrEmpty(date.Text) ? (DateTime?)null : Convert.ToDateTime(date.Text);
                if (txt.Text == "" && classes.Text == "" && date.Text == "")
                {

                }
                else
                {
                    int airrouting = _objBALAirTicket.InsertAirticketRouting(_objAirtickRouting);

                }
            }
            int UpdateAirResult = _objBALAirTicket.updateAirticketId(Result, Session["RoutTempID"].ToString()); ;

            //Session["RoutTempID"] = "";
            //BindInvoiceLineItems();
            //BindInvoiceLineItemsCount();
            //AirticketClear();
            //ddlSoureceref.Items.Clear();
            //ddlPassengerName.Items.Clear();
            //BindSerTicketNumber();
            //ddlPassengerNames.Items.Clear();
            //BindGenPassengerNames();

            //}
            //else
            //{
            //    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "AirTckt Routing Details was not created plase try again");
            //}
            Session["RoutTempID"] = "";
            BindPFInvoiceLineItems();
            BindPFInvoiceLineItemsCount();
            PFAirticketClear();
            ddlPFSoureceref.Items.Clear();
            ddlPFPassengerName.Items.Clear();
            BindPFSerTicketNumber();
            ddlPFPassengerNames.Items.Clear();
            BindPFGenPassengerNames();
            AirPFPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    private static string uniqueIdSession()
    {
        //try
        //{
        var now = DateTime.Now;
        DateTime zeroDate = DateTime.MinValue.AddDays(now.Day).AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
        string uniqueId = (zeroDate.Ticks / 10000).ToString();
        return uniqueId;
        //  }
        // catch (Exception ex)
        //  {
        //      lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);

        //  }
    }
    protected void txtPFAirCommisionper_TextChanged(object sender, EventArgs e)
    {
        try
        {
            commPFExcAmount();
            AirPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void txtPFDate_TextChanged(object sender, EventArgs e)
    {
        txtPFAirTravelDate.Text = txtPFDate1.Text;
        AirPFPopupExtender.Show();
    }

    private void PFAirticketRouting_Disabled()
    {

        try
        {
            lblPFRoutes1.Text = "";
            lblPFRoutes2.Text = "";
            lblPFRoutes3.Text = "";
            lblPFRoutes4.Text = "";
            txtPFDate1.Enabled = false;
            txtPFDate2.Enabled = false;
            txtPFDate3.Enabled = false;
            txtPFDate4.Enabled = false;
            txtPFFlightNo1.Enabled = false;
            txtPFFlightNo2.Enabled = false;
            txtPFFlightNo3.Enabled = false;
            txtPFFlightNo4.Enabled = false;
            txtPFClass1.Enabled = false;
            txtPFClass2.Enabled = false;
            txtPFClass3.Enabled = false;
            txtPFClass4.Enabled = false;

            rfvtxtPFFlightNo1.Enabled = false;
            rfvtxtPFClass1.Enabled = false;
            rfvtxtPFDate1.Enabled = false;

            rfvtxtPFFlightNo2.Enabled = false;
            rfvtxtPFClass2.Enabled = false;
            rfvtxtPFDate2.Enabled = false;

            rfvtxtPFFlightNo3.Enabled = false;
            rfvtxtPFClass3.Enabled = false;
            rfvtxtPFDate3.Enabled = false;

            rfvtxtPFFlightNo4.Enabled = false;
            rfvtxtPFClass4.Enabled = false;
            rfvtxtPFDate4.Enabled = false;
        }

        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void PFroutingLablesShow()
    {
        try
        {
            string routing = txtPFAirRouting.Text;
            if (routing.Contains("/"))
            {

                String[] RoutingArray = routing.Split('/');
                for (int i = 0; i < RoutingArray.Length - 1; i++)
                {

                    if (i == 0)
                    {
                        lblPFRoutes1.Text = RoutingArray[0] + "/" + RoutingArray[1];
                        txtPFFlightNo1.Enabled = true;
                        txtPFDate1.Enabled = true;
                        txtPFClass1.Enabled = true;

                        rfvtxtPFFlightNo1.Enabled = true;
                        rfvtxtPFClass1.Enabled = true;
                        rfvtxtPFDate1.Enabled = true;

                    }
                    if (i == 1)
                    {
                        lblPFRoutes2.Text = RoutingArray[1] + "/" + RoutingArray[2];
                        txtPFDate2.Enabled = true;
                        txtPFFlightNo2.Enabled = true;
                        txtPFClass2.Enabled = true;

                        rfvtxtPFFlightNo2.Enabled = true;
                        rfvtxtPFClass2.Enabled = true;
                        rfvtxtPFDate2.Enabled = true;

                    }

                    if (i == 2)
                    {
                        lblPFRoutes3.Text = RoutingArray[2] + "/" + RoutingArray[3];
                        txtPFDate3.Enabled = true;
                        txtPFFlightNo3.Enabled = true;
                        txtPFClass3.Enabled = true;

                        rfvtxtPFFlightNo3.Enabled = true;
                        rfvtxtPFClass3.Enabled = true;
                        rfvtxtPFDate3.Enabled = true;

                    }

                    if (i == 3)
                    {
                        lblPFRoutes4.Text = RoutingArray[3] + "/" + RoutingArray[4];
                        txtPFDate4.Enabled = true;
                        txtPFFlightNo4.Enabled = true;
                        txtPFClass4.Enabled = true;

                        rfvtxtPFFlightNo4.Enabled = true;
                        rfvtxtPFClass4.Enabled = true;
                        rfvtxtPFDate4.Enabled = true;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFAirRouting_TextChanged(object sender, EventArgs e)
    {
        try
        {
            PFAirticketRouting_Disabled();
            //divRouteHead.Visible = true;
            //divRouting.Visible = true;
            PFroutingLablesShow();

            AirPFPopupExtender.Show();
            txtPFFlightNo1.Text = "";
            txtPFFlightNo2.Text = "";
            txtPFFlightNo3.Text = "";
            txtPFFlightNo4.Text = "";
            txtPFClass1.Text = "";
            txtPFClass2.Text = "";
            txtPFClass3.Text = "";
            txtPFClass4.Text = "";
            txtPFDate1.Text = "";
            txtPFDate2.Text = "";
            txtPFDate3.Text = "";
            txtPFDate4.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    protected void btnPFAirSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            InsertPFAirTicket();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //general Charge And service fee
    /////---------General Charge------------------///////////////////

    private void InsertPFGeneralCharge()
    {
        try
        {
            _objEmGenCharge.Type = Convert.ToInt32(ddlPFGenchrgType.SelectedValue.ToString());
            _objEmGenCharge.PassengerName = ddlPFPassengerNames.SelectedValue.ToString();
            _objEmGenCharge.Details = txtPFDetails.Text;
            _objEmGenCharge.CreditCard = Convert.ToInt32(ddlPFCrdCardType.SelectedValue.ToString());
            _objEmGenCharge.Units = Convert.ToInt32(txtPFUnits.Text);
            _objEmGenCharge.RateNet = Convert.ToDecimal(txtPFRateNet.Text);
            _objEmGenCharge.VatPer = string.IsNullOrEmpty(txtPFgenvat.Text) ? 0 : Convert.ToDecimal(txtPFgenvat.Text);
            _objEmGenCharge.VatAmount = string.IsNullOrEmpty(txtPFVatAmount.Text) ? 0 : Convert.ToDecimal(txtPFVatAmount.Text);

            _objEmGenCharge.ExcluAmt = Convert.ToDecimal(txtPFExcluAmount.Text);
            _objEmGenCharge.ClientTot = Convert.ToDecimal(txtPFClientTotal.Text);
            _objEmGenCharge.InvoiceType = "GC";
            _objEmGenCharge.CreatedBy = 0;
            _objEmGenCharge.invDocumentNo = "0";
            if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
            {
                _objEmGenCharge.GenTempUniqCode = uniqueIdSession();
            }
            else
            {
                _objEmGenCharge.GenTempUniqCode = Session["TempUniqCode"].ToString();
            }
            int Result = _objDalGenchrge.InsertGencharge(_objEmGenCharge);

            if (Result > 0)
            {
                if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
                {
                    Session["TempUniqCode"] = _objEmGenCharge.GenTempUniqCode;
                }
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "GeneralCharge created Successfully");
                clearcontrols();
                BindPFInvoiceLineItems();
                BindPFInvoiceLineItemsCount();
                PFGeneralChargeClear();

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "GeneralCharge Details was not created plase try again");
            }
            GenPFPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void clearcontrols()
    {

    }

    private void BindPFGenPassengerNames()
    {
        try
        {
            DataSet ds = new DataSet();
            string tempuniqcode = Session["TempUniqCode"].ToString();
            int airtickno = 0;
            ds = _objBalservice.BindPassengerNames(tempuniqcode, airtickno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFPassengerNames.DataSource = ds.Tables[0];
                ddlPFPassengerNames.DataTextField = "AirPassenger";
                ddlPFPassengerNames.DataValueField = "TicketId";
                ddlPFPassengerNames.DataBind();
                ddlPFPassengerNames.Items.Insert(0, new ListItem("--Select Passenger Name--", "0"));
            }
            else
            {
                ddlPFPassengerNames.DataSource = null;
                ddlPFPassengerNames.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindPFGenServiceTypes()
    {
        try
        {
            string Type = "GC";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFGenchrgType.DataSource = ds.Tables[0];
                ddlPFGenchrgType.DataTextField = "ComDesc";
                ddlPFGenchrgType.DataValueField = "ComId";
                ddlPFGenchrgType.DataBind();
                ddlPFGenchrgType.Items.Insert(0, new ListItem("--Select Service Type--", "0"));

            }
            else
            {
                ddlPFGenchrgType.DataSource = null;
                ddlPFGenchrgType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFGenCreditCardType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.BindCreditCardType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFCrdCardType.DataSource = ds.Tables[0];
                ddlPFCrdCardType.DataTextField = "CardDescription";
                ddlPFCrdCardType.DataValueField = "CrdCardId";
                ddlPFCrdCardType.DataBind();
                ddlPFCrdCardType.Items.Insert(0, new ListItem("--Select CreditCard --", "0"));
            }
            else
            {
                ddlPFCrdCardType.DataSource = null;
                ddlPFCrdCardType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFGenTicketNumber()
    {
        try
        {
            DataSet ds = new DataSet();
            string tempuniqcode = Session["TempUniqCode"].ToString();

            ds = _objDalService.BindTicketNumber(tempuniqcode);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFPassengerName.DataSource = ds.Tables[0];
                ddlPFPassengerName.DataTextField = "TktNumber";
                ddlPFPassengerName.DataValueField = "TktIdId";
                ddlPFPassengerName.DataBind();
                ddlPFPassengerName.Items.Insert(0, new ListItem("--Select TicketNumber--", "0"));


            }
            else
            {
                ddlPFPassengerName.DataSource = null;
                ddlPFPassengerName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void ddlPFGenchrgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ddlText = ddlPFGenchrgType.SelectedItem.Text;
            int ddlValue = Convert.ToInt32(ddlPFGenchrgType.SelectedItem.Value);
            txtPFDetails.Text = ddlText;
            var VatPer = _objDalService.getVatPercentage();
            if (VatPer != "" && VatPer != null)
            {
                txtPFgenvat.Text = VatPer.ToString();
            }
            if (txtPFUnits.Text != "")

                txtPFRateNet_TextChanged(null, null);
            GenPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFRateNet_TextChanged(object sender, EventArgs e)
    {

        try
        {
            int units = Convert.ToInt32(txtPFUnits.Text);
            txtPFRateNet.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFRateNet.Text);
            decimal RateNet = Convert.ToDecimal(txtPFRateNet.Text);
            decimal ExclTotal = (units) * (RateNet);
            txtPFExcluAmount.Text = ExclTotal.ToString();
            if (txtPFgenvat.Text == "" || txtPFgenvat.Text == "0")
            {
                txtPFClientTotal.Text = ExclTotal.ToString();

            }
            else
            {

                txtPFExcluAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ExclTotal.ToString());
                decimal Vatper = Convert.ToDecimal(txtPFgenvat.Text);
                decimal vatAmount = ((Vatper / 100) * ExclTotal);
                txtPFVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(vatAmount.ToString());
                decimal clientTotal = ExclTotal + vatAmount;
                txtPFClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
            }
            GenPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnPFGencharge_click(object sender, EventArgs e)
    {
        try
        {
            InsertPFGeneralCharge();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PFAirticketClear();
    }
    protected void txtPFUnits_TextChanged(object sender, EventArgs e)
    {
        try
        {

            txtPFRateNet.Enabled = true;
            if (txtPFRateNet.Text != "")

                txtPFRateNet_TextChanged(null, null);

            GenPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //------------Service Fee--------//


    protected void PFServFee_click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdatePFServiceFee();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void btnSerCancel_Click(object sender, EventArgs e)
    {

    }
    private void InsertUpdatePFServiceFee()
    {
        try
        {

            DateTime date = DateTime.ParseExact(txtPFSerTravelDate.Text, "yyyy-MM-dd", null);

            _objEmService.Type = Convert.ToInt32(ddlPFServiceType.SelectedValue.ToString());
            _objEmService.SourceRef = ddlPFSoureceref.SelectedValue.ToString();

            if (chkPFMerge.Checked)
                _objEmService.MergeC = Convert.ToInt32(chkPFMerge.Checked);

            decimal mergeAmt = 0;

            if (chkPFMerge.Checked == true)
            {
                var mergeAmount = _objDalService.GetSFMergeAmount(Convert.ToInt32(ddlPFSoureceref.SelectedValue.ToString()), Convert.ToInt32(chkPFMerge.Checked));

                mergeAmt = (Convert.ToDecimal(string.IsNullOrEmpty(mergeAmount.ToString().Trim()) ? "0" : mergeAmount.ToString().Trim())) + Convert.ToDecimal(txtPFSerClientTotal.Text);
            }


            _objEmService.TravelDate = date;
            _objEmService.PassengerName = Convert.ToInt32(string.IsNullOrEmpty(ddlPFPassengerName.SelectedValue.ToString()) ? "0" : ddlPFPassengerName.SelectedValue.ToString().Trim());
            string Exclusu = string.IsNullOrEmpty(txtPFExclusAmount.Text.Trim()) ? ".0000" : txtPFExclusAmount.Text.Trim();
            _objEmService.ExcluAmount = Convert.ToDecimal(Exclusu);
            _objEmService.Details = txtPFserDetails.Text;
            string Vartxt = string.IsNullOrEmpty(txtPFSerVatPer.Text.Trim()) ? "0" : txtPFSerVatPer.Text.Trim();
            _objEmService.VatPer = Convert.ToDecimal(Vartxt);
            _objEmService.VatAmount = Convert.ToDecimal(txtPFSerVatAmount.Text);
            _objEmService.PaymentMethod = Convert.ToInt32(ddlPFPaymentMethod.SelectedValue.ToString());
            string clientTotal = string.IsNullOrEmpty(txtPFSerClientTotal.Text.Trim()) ? ".0000" : txtPFSerClientTotal.Text.Trim();
            _objEmService.ClientTot = Convert.ToDecimal(clientTotal);
            _objEmService.CreditCardType = Convert.ToInt32(string.IsNullOrEmpty(ddlPFCreditCardType.SelectedValue.ToString().Trim()) ? "0" : ddlPFCreditCardType.SelectedValue.ToString().Trim());
            _objEmService.CollectVia = Convert.ToInt32(string.IsNullOrEmpty(ddlPFCollectVia.SelectedValue.ToString().Trim()) ? "0" : ddlPFCollectVia.SelectedValue.ToString().Trim());

            _objEmService.TasfMpd = txtPFTASFMPD.Text;
            _objEmService.CreatedBy = 0;
            _objEmService.InvoiceType = "SF";
            _objEmService.invDocumentNo = "0";
            if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
            {
                _objEmService.SerTempUniqCode = uniqueIdSession();
            }
            else
            {
                _objEmService.SerTempUniqCode = Session["TempUniqCode"].ToString();
            }

            int Result = _objDalService.InsertUpdateSerfee(_objEmService, mergeAmt);

            if (Result > 0)
            {
                if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
                {
                    Session["TempUniqCode"] = _objEmService.SerTempUniqCode;
                }

                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "ServiceFee created Successfully");
                BindPFInvoiceLineItems();
                BindPFInvoiceLineItemsCount();
                PFServiceFeeClear();

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "ServiceFee Details was not created plase try again");
            }
            PFSerPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void BindPFSerServiceTypes()
    {
        try
        {
            string Type = "SF";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);
            ViewState["PFServiceType_GetDataByTYpe"] = ds.Tables[1];

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFServiceType.DataSource = ds.Tables[0];
                ddlPFServiceType.DataTextField = "ComDesc";
                ddlPFServiceType.DataValueField = "ComId";
                ddlPFServiceType.DataBind();
                ddlPFServiceType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }
            else
            {
                ddlPFServiceType.DataSource = null;
                ddlPFServiceType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    public void BindPFPaymentType()
    {
        try
        {
            DataSet ds = new DataSet();
            int paymentId = 0;
            ds = _doUtilities.BindPaymentType(paymentId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFPaymentMethod.DataSource = ds.Tables[0];
                ddlPFPaymentMethod.DataTextField = "PaymentName";
                ddlPFPaymentMethod.DataValueField = "PaymentId";
                ddlPFPaymentMethod.DataBind();
                ddlPFPaymentMethod.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlPFAirPayment.DataSource = ds.Tables[0];
                ddlPFAirPayment.DataTextField = "PaymentName";
                ddlPFAirPayment.DataValueField = "PaymentId";
                ddlPFAirPayment.DataBind();
                ddlPFAirPayment.Items.Insert(0, new ListItem("--Select Payment--", "0"));


            }
            else
            {
                ddlPFPaymentMethod.DataSource = null;
                ddlPFPaymentMethod.DataBind();
                ddlPFAirPayment.DataSource = null;
                ddlPFAirPayment.DataBind();
                ddlPFPaymentMethod.Items.Insert(0, new ListItem("Select Payment", "0"));
                ddlPFAirPayment.Items.Insert(0, new ListItem("Select Payment", "0"));


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    private void BindPFSerCreditCardType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.BindCreditCardType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFCreditCardType.DataSource = ds.Tables[0];
                ddlPFCreditCardType.DataTextField = "CardDescription";
                ddlPFCreditCardType.DataValueField = "CrdCardId";
                ddlPFCreditCardType.DataBind();
                ddlPFCreditCardType.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlPFCreditCardType.DataSource = null;
                ddlPFCreditCardType.DataBind();
                ddlPFCreditCardType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFSerTicketNumber()
    {
        try
        {
            DataSet ds = new DataSet();

            string tempuniqcode = Session["TempUniqCode"].ToString();
            ds = _objDalService.BindTicketNumber(tempuniqcode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFSoureceref.DataSource = ds.Tables[0];
                ViewState["ddlPFSoureceref"] = ds.Tables[0];

                ddlPFSoureceref.DataTextField = "AirTicketNo";
                ddlPFSoureceref.DataValueField = "TicketId";
                ddlPFSoureceref.DataBind();
                ddlPFSoureceref.Items.Insert(0, new ListItem("--Select Ticket No--", "0"));
            }
            else
            {
                ddlPFSoureceref.DataSource = null;
                ddlPFSoureceref.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFSerPassengerNames()
    {
        try
        {
           
            int airtickno = Convert.ToInt32(ddlPFSoureceref.SelectedItem.Value.ToString());
           
            if (ViewState["ddlPFSoureceref"].ToString() != null)
            {
                DataTable dt = (DataTable)ViewState["ddlPFSoureceref"];

                var PassengerList = from list in dt.AsEnumerable()
                                    where Convert.ToInt32(list["TicketId"]) == Convert.ToInt32(ddlPFSoureceref.SelectedValue)
                                    select new
                                    {
                                        PaxName = list["AirPassenger"].ToString(),
                                        TicketId = list["TicketId"].ToString(),
                                        // type = list["Type"].ToString()
                                    };


                string type = (dt.AsEnumerable()
                    .Where(p => p["TicketId"].ToString() == Convert.ToInt32(ddlPFSoureceref.SelectedValue).ToString())
                    .Select(p => p["type"].ToString())).FirstOrDefault();


                if (PassengerList.ToString() != null)
                {
                    //BindVatBasedOnTicket(Convert.ToInt32(type));
                    ddlPFPassengerName.DataSource = PassengerList;
                    ddlPFPassengerName.DataTextField = "PaxName";
                    ddlPFPassengerName.DataValueField = "TicketId";

                    ddlPFPassengerName.DataBind();
                }

            }

            else
            {
                ddlPFPassengerName.DataSource = null;
                ddlPFPassengerName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFCollectVia()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objDalService.BindCollectVia();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPFCollectVia.DataSource = ds.Tables[0];
                ddlPFCollectVia.DataTextField = "CollectName";
                ddlPFCollectVia.DataValueField = "ColletViaId";
                ddlPFCollectVia.DataBind();
                ddlPFCollectVia.Items.Insert(0, new ListItem("--Select --", "0"));
            }
            else
            {
                ddlPFCollectVia.DataSource = null;
                ddlPFCollectVia.DataBind();
                ddlPFCollectVia.Items.Insert(0, new ListItem("--Select Collect Via-", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    protected void ddlPFPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPFPaymentMethod.SelectedValue == "2")
            {
                BindPFCollectVia();

                BindPFSerCreditCardType();

                ddlPFCreditCardType.Enabled = true;
                ddlPFCollectVia.Enabled = true;
                txtPFTASFMPD.Enabled = false;

                rfvddlPFCollectVia.Enabled = true;
                rfvddlPFCreditCardType.Enabled = true;
                rfvtxtPFTASFMPD.Enabled = false;
            }
            else
            {
                ddlPFCreditCardType.Items.Clear();
                ddlPFCollectVia.Items.Clear();
                txtPFTASFMPD.Text = "";
                ddlPFCreditCardType.Enabled = false;
                ddlPFCollectVia.Enabled = false;
                txtPFTASFMPD.Enabled = false;

                rfvddlPFCollectVia.Enabled = false;
                rfvddlPFCreditCardType.Enabled = false;
                rfvtxtPFTASFMPD.Enabled = false;
            }
            PFSerPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlPFCollectVia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPFCollectVia.SelectedValue == "2")
            {
                txtPFTASFMPD.Enabled = true;
                rfvtxtPFTASFMPD.Enabled = true;
            }
            else
            {
                txtPFTASFMPD.Enabled = false;
                rfvtxtPFTASFMPD.Enabled = false;
            }

            PFSerPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlPFServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtPFserDetails.Text = ddlPFServiceType.SelectedItem.Text;
          //  var VatPer = _objDalService.getVatPercentage();

            DataTable dt = (DataTable)ViewState["PFServiceType_GetDataByTYpe"];
            string VatPer = (dt.AsEnumerable()
                .Where(p => p["ComId"].ToString() == Convert.ToInt32(ddlPFServiceType.SelectedItem.Value).ToString())
                .Where(p => p["ComDesc"].ToString() == txtPFserDetails.Text.ToString())
                .Select(p => p["VatRate"].ToString())).FirstOrDefault();


            if (VatPer != null)
            {
                txtPFSerVatPer.Text = VatPer.ToString();
            }
            else
            {
                txtPFSerVatPer.Text = "0.00";
                txtPFSerVatAmount.Text = "0.00";
                txtPFDetails.Text = "";
                txtPFExclusAmount.Text = txtPFSerClientTotal.Text;

            }

            if (txtPFSerClientTotal.Text != "")
            {

                getClientTotal();
            }
            PFSerPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    //protected void txtPFExclusAmount_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtPFExclusAmount.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFExclusAmount.Text.ToString());

    //        getPFClientTotal();
    //        PFSerPopupExtender.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //        ExceptionLogging.SendExcepToDB(ex);
    //    }
    //}

    protected void getPFClientTotal()
    {
        try
        {
            ExclusiveAmount = Convert.ToDecimal(txtPFExclusAmount.Text);

            //  txtPFSerClientTotal.Text = txtPFExclusAmount.Text;

            if (txtPFSerVatPer.Text == "")
            {
                //txtPFClientTotal.Text = ExclusiveAmount.ToString();
            }
            else
            {

                PFExcusiveAmount();

                //decimal Vatper = Convert.ToDecimal(txtPFSerVatPer.Text);
                //decimal vatAmount = ((Vatper / 100) * ExclusiveAmount);
                //decimal clientTotal = ExclusiveAmount + vatAmount;
                //txtPFSerVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(vatAmount.ToString());
                //   txtPFSerClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
                PFSerPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //land methods
    protected void DDPFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["PFget_VatRateByType"] != null)
            {
                DataTable dt = (DataTable)ViewState["PFget_VatRateByType"];

                string vatRate = (dt.AsEnumerable()
                    .Where(p => p["TypeId"].ToString() == DDPFlandType.SelectedItem.Value.ToString())
                    .Select(p => p["VatRate"].ToString())).FirstOrDefault();
                vatRate = _objBOUtiltiy.FormatTwoDecimal(vatRate.ToString());

                txtPFLandExlVatPer.Text = vatRate;
                txtPFLandVatPer.Text = vatRate;
            }

            if (txtPFLandVatPer.Text != "0.00")
            {
                txtPFlandExclVatAmount.Text = ((Convert.ToDecimal(txtPFlandTotalExcl.Text) * Convert.ToDecimal(txtPFLandVatPer.Text)) / 100).ToString();
                txtPFlandTotalIncl.Text = (Convert.ToDecimal(txtPFlandExclVatAmount.Text) + Convert.ToDecimal(txtPFlandTotalExcl.Text)).ToString();

                if (txtPFlandCommPer.Text != "")
                {
                    txtPFlandTotalExcl_TextChanged(null, null);
                    txtPFlandCommPer_TextChanged(null, null);
                }


            }
            else
            {
                txtPFlandTotalIncl.Text = txtPFlandTotalExcl.Text;

            }

           
          
            txtPFlandTotalIncl.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFlandTotalIncl.Text);
            txtPFlandDuefromclient.Text = txtPFlandTotalIncl.Text;
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        landPFPopExtender.Show();

    }
    // Payment Menthod Select Payment Bind Credit card
    protected void DDPFlandPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DDPFlandCreditCard.DataSource = _doUtilities.BindCreditCardType();
            DDPFlandCreditCard.DataTextField = "CardDescription";
            DDPFlandCreditCard.DataValueField = "CrdCardId";
            DDPFlandCreditCard.DataBind();
            DDPFlandCreditCard.Items.Insert(0, new ListItem("--Select Card--", "0"));



            var payment = Convert.ToDecimal(DDPFlandPayment.SelectedItem.Value);

            if (payment == 2)
            {
                DDPFlandCreditCard.Enabled = true;
            }
            else
            {
                DDPFlandCreditCard.Enabled = false;
            }
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void PFLandArrSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objEMLandarrangement.LandSupplier = Convert.ToInt32(DDPFlandSupplier.SelectedIndex);
            objEMLandarrangement.Services = Convert.ToInt32(DDPFlandService.SelectedIndex);
            objEMLandarrangement.Type = Convert.ToInt32(DDPFlandType.SelectedIndex);
            objEMLandarrangement.PassengerName = txtPFlandPassName.Text.Trim();
            objEMLandarrangement.TravelFrDate = string.IsNullOrEmpty(txtPFlandTravelFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtPFlandTravelFrom.Text);
            objEMLandarrangement.TravelToDate = string.IsNullOrEmpty(txtPFlandTravelto.Text) ? (DateTime?)null : Convert.ToDateTime(txtPFlandTravelto.Text);


            objEMLandarrangement.BookRefNo = txtPFlandBookingRef.Text.Trim();
            objEMLandarrangement.Voucher = txtPFlandVocher.Text.Trim();
            objEMLandarrangement.SuppRef = txtPFlandSupplierRef.Text.Trim();
            objEMLandarrangement.SuppInvNo = txtPFlandSuppInvNo.Text.Trim();
            string Units = string.IsNullOrEmpty(txtPFlandUnits.Text.Trim()) ? "0" : txtPFlandUnits.Text.Trim();
            objEMLandarrangement.Units = Convert.ToInt32(Units);
            string CommabExclu = string.IsNullOrEmpty(txtPFlandUnits.Text.Trim()) ? "0" : txtPFlandUnits.Text.Trim();
            objEMLandarrangement.CommabExclu = Convert.ToDecimal(CommabExclu);
            string TotIncl = string.IsNullOrEmpty(txtPFlandTotalIncl.Text.Trim()) ? "0" : txtPFlandTotalIncl.Text.Trim();
            objEMLandarrangement.TotIncl = Convert.ToDecimal(TotIncl);
            string RateInclu = string.IsNullOrEmpty(txtPFlandRateIncl.Text.Trim()) ? "0" : txtPFlandRateIncl.Text.Trim();
            objEMLandarrangement.RateInclu = Convert.ToDecimal(RateInclu);
            string CommPer = string.IsNullOrEmpty(txtPFlandCommPer.Text.Trim()) ? "0" : txtPFlandCommPer.Text.Trim();
            objEMLandarrangement.CommPer = Convert.ToDecimal(CommPer);
            //   string CommPer = string.IsNullOrEmpty(txtlandCommPer.Text.Trim()) ? "0" : txtlandCommPer.Text.Trim();
            objEMLandarrangement.ExclVatPer = string.IsNullOrEmpty(txtPFLandExlVatPer.Text) ? 0 : Convert.ToDecimal(txtPFLandExlVatPer.Text);
            string ExclusiveVatAmount = string.IsNullOrEmpty(txtPFlandExclVatAmount.Text.Trim()) ? "0" : txtPFlandExclVatAmount.Text.Trim();
            objEMLandarrangement.ExclusiveVatAmount = Convert.ToDecimal(ExclusiveVatAmount);
            string CommissionableInclu = string.IsNullOrEmpty(txtPFlandCmblIncl.Text.Trim()) ? "0" : txtPFlandCmblIncl.Text.Trim();
            objEMLandarrangement.CommissionableInclu = Convert.ToDecimal(CommissionableInclu);
            string CommissionExclu = string.IsNullOrEmpty(txtPFlandCommExcl.Text.Trim()) ? "0" : txtPFlandCommExcl.Text.Trim();
            objEMLandarrangement.CommissionExclu = Convert.ToDecimal(CommissionExclu);
            string TotExclu = string.IsNullOrEmpty(txtPFlandTotalExcl.Text.Trim()) ? "0" : txtPFlandTotalExcl.Text.Trim();
            objEMLandarrangement.TotExclu = Convert.ToDecimal(TotExclu);
            string OthCommabInclu = string.IsNullOrEmpty(txtPFlandOtherCmblIncl.Text.Trim()) ? "0" : txtPFlandOtherCmblIncl.Text.Trim();
            objEMLandarrangement.OthCommabInclu = Convert.ToDecimal(OthCommabInclu);
            objEMLandarrangement.VatPer = string.IsNullOrEmpty(txtPFLandVatPer.Text.Trim()) ? 0 : Convert.ToDecimal(txtPFLandVatPer.Text);
            objEMLandarrangement.Payment = Convert.ToInt32(DDPFlandPayment.SelectedIndex);
            string TotCommabInclu = string.IsNullOrEmpty(txtPFlandTotalcmblIncl.Text.Trim()) ? "0" : txtPFlandTotalcmblIncl.Text.Trim();
            objEMLandarrangement.TotCommabInclu = Convert.ToDecimal(TotCommabInclu);
            string CommVatAmount = string.IsNullOrEmpty(txtPFlandVatAmount.Text.Trim()) ? "0" : txtPFlandVatAmount.Text.Trim();
            objEMLandarrangement.CommVatAmount = Convert.ToDecimal(CommVatAmount);
            objEMLandarrangement.CreditCard = Convert.ToInt32(DDPFlandCreditCard.SelectedIndex);
            string NonCommabInclu = string.IsNullOrEmpty(txtPFlandNoncmbl.Text.Trim()) ? "0" : txtPFlandNoncmbl.Text.Trim();
            objEMLandarrangement.NonCommabInclu = Convert.ToDecimal(NonCommabInclu);
            string CommissionInclu = string.IsNullOrEmpty(txtPFlandCommIncl.Text.Trim()) ? "0" : txtPFlandCommIncl.Text.Trim();
            objEMLandarrangement.CommissionInclu = Convert.ToDecimal(CommissionInclu);
            string DueToClient = string.IsNullOrEmpty(txtPFlandDuefromclient.Text.Trim()) ? "0" : txtPFlandDuefromclient.Text.Trim();
            objEMLandarrangement.DueToClient = Convert.ToDecimal(DueToClient);

            string LessCommission = string.IsNullOrEmpty(txtPFlandLessComm.Text.Trim()) ? "0" : txtPFlandLessComm.Text.Trim();
            objEMLandarrangement.LessCommission = Convert.ToDecimal(LessCommission);

            string DueTOSupplier = string.IsNullOrEmpty(txtPFlandDuetoSupplier.Text.Trim()) ? "0" : txtPFlandDuetoSupplier.Text.Trim();
            objEMLandarrangement.DueTOSupplier = Convert.ToDecimal(DueTOSupplier);
            objEMLandarrangement.InvoiceType = "Land";
            string CO2Emis = string.IsNullOrEmpty(txtPFlandCO2.Text.Trim()) ? "0" : txtPFlandCO2.Text.Trim();
            objEMLandarrangement.CO2Emis = Convert.ToInt32(CO2Emis);
            objEMLandarrangement.invDocumentNo = "0";


            if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
            {
                objEMLandarrangement.LandTempUniqCode = uniqueIdSession();
            }
            else
            {
                objEMLandarrangement.LandTempUniqCode = Session["TempUniqCode"].ToString();
            }

            int Result = objDALandarrangement.InsertLandArrangement(objEMLandarrangement);

            if (Result > 0)
            {
                if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
                {
                    Session["TempUniqCode"] = objEMLandarrangement.LandTempUniqCode;
                }
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "LandSupplier created Successfully");
                BindPFInvoiceLineItems();
                BindPFInvoiceLineItemsCount();
                LandArrangemntsClear();
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "LandSupplier Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        landPFPopExtender.Hide();
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {

    }
    protected void Reset_Click(object sender, EventArgs e)
    {

    }




    // Bind Land Supplier Name
    public void BindPFLandSuppliers()
    {
        try
        {
            BALandSuppliers objBalandSuppliers = new BALandSuppliers();
            int landSupId = 0;
            DataSet datasetland = new DataSet();
            datasetland = objBalandSuppliers.GetLandSupplier(landSupId);

            ViewState["PFAllCommissionTypes_Land"] = datasetland.Tables[1];

            if (datasetland.Tables[0].Rows.Count > 0)
            {
                DDPFlandSupplier.DataSource = datasetland.Tables[0];
                DDPFlandSupplier.DataTextField = "LSupplierName";
                DDPFlandSupplier.DataValueField = "LSupplierId";
                DDPFlandSupplier.DataBind();
                DDPFlandSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
            else
            {
                DDPFlandSupplier.DataSource = null;
                DDPFlandSupplier.DataBind();
                DDPFlandSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    public void BindPFType()
    {
        try
        {

            DataSet ds = new DataSet();
            ds = _doUtilities.get_Type();

            if (ds.Tables[0].Rows.Count > 0)
            {

                DDPFlandType.DataSource = ds.Tables[0];
                DDPFlandType.DataTextField = "TypeName";
                DDPFlandType.DataValueField = "TypeId";
                DDPFlandType.DataBind();
                DDPFlandType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }
            else
            {
                DDPFlandType.DataSource = null;
                DDPFlandType.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }

    public void BindPFLandService()
    {
        try
        {
            string Type = "Land";
            DDPFlandService.DataSource = _doUtilities.GetServiceTypeByType(Type);
            DDPFlandService.DataTextField = "ComDesc";
            DDPFlandService.DataValueField = "ComId";
            DDPFlandService.DataBind();
            DDPFlandService.Items.Insert(0, new ListItem("--Select Service--", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    public void BindPFLandPaymentType()
    {
        try
        {
            DataSet ds = new DataSet();
            int payemntId = 0;
            ds = _doUtilities.BindPaymentType(payemntId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDPFlandPayment.DataSource = ds.Tables[0];
                DDPFlandPayment.DataTextField = "PaymentName";
                DDPFlandPayment.DataValueField = "PaymentId";
                DDPFlandPayment.DataBind();
                DDPFlandPayment.Items.Insert(0, new ListItem("--Select Payment--", "0"));
            }
            else
            {
                DDPFlandPayment.DataSource = null;
                DDPFlandPayment.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    protected void txtPFlandTotalIncl_TextChanged(object sender, EventArgs e)
    {


    }
    protected void txtPFlandTotalExcl_TextChanged(object sender, EventArgs e)
    {

        try
        {
            txtPFlandTotalExcl.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFlandTotalExcl.Text);
            txtPFlandTotalIncl.Text = _objBOUtiltiy.ExcluAssignInclu(txtPFlandTotalExcl.Text);
            txtPFlandExclVatAmount.Text = _objBOUtiltiy.ExcluvatCau(txtPFlandTotalExcl.Text, txtPFLandExlVatPer.Text);
            txtPFlandcmblExcl.Text = txtPFlandTotalExcl.Text;
            if (txtPFlandTotalExcl.Text != "" && txtPFlandExclVatAmount.Text != "")
            {
                txtPFlandTotalIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtPFlandTotalExcl.Text) + Convert.ToDecimal(txtPFlandExclVatAmount.Text)).ToString());

            }
            if (txtPFlandCommPer.Text != "")
            {
                txtPFlandCommPer_TextChanged(null, null);
            }
            landPFPopExtender.Show();

            txtPFlandDuefromclient.Text = txtPFlandTotalIncl.Text;

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFlandRateIncl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtPFlandRateIncl.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFlandRateIncl.Text);
            if (txtPFlandUnits.Text != "" && txtPFlandRateIncl.Text != "")
            {
                txtPFlandCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtPFlandUnits.Text) * Convert.ToDecimal(txtPFlandRateIncl.Text)).ToString());

                txtPFlandTotalcmblIncl.Text = txtPFlandCmblIncl.Text;
            }
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFlandUnits_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPFlandUnits.Text != "" && txtPFlandRateIncl.Text != "")
            {
                txtPFlandCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtPFlandUnits.Text) * Convert.ToDecimal(txtPFlandRateIncl.Text)).ToString());

                txtPFlandTotalcmblIncl.Text = txtPFlandCmblIncl.Text;
            }
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFlandOtherCmblIncl_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (txtPFlandCmblIncl.Text != "" && txtPFlandOtherCmblIncl.Text != "")
            {
                txtPFlandTotalcmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtPFlandCmblIncl.Text) + Convert.ToDecimal(txtPFlandOtherCmblIncl.Text)).ToString());

            }
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFlandcmblExcl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtPFlandCommPer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPFlandcmblExcl.Text != "" && txtPFlandCommPer.Text != "")
            {
                txtPFlandCommExcl.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.ExcluvatCau(txtPFlandcmblExcl.Text, txtPFlandCommPer.Text));
            }
            if (txtPFlandCommExcl.Text != "")
            {
                txtPFlandVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.ExcluvatCau(txtPFlandCommExcl.Text, txtPFLandVatPer.Text));

            }
            else
            {

            }
            if (txtPFlandCommExcl.Text != "" && txtPFlandVatAmount.Text != "")
            {
                txtPFlandCommIncl.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.InclusiveAmount(txtPFlandCommExcl.Text, txtPFlandVatAmount.Text).ToString());
            }
            txtPFlandLessComm.Text = txtPFlandCommIncl.Text;
            txtPFlandDuetoSupplier.Text = (Convert.ToDecimal(txtPFlandDuefromclient.Text) - Convert.ToDecimal(txtPFlandLessComm.Text)).ToString();
            landPFPopExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void PFInvListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void PFInvListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            PFInvListGrid.PageIndex = e.NewPageIndex;
            BindPFInvoiceLineItems();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    //invoice Line items
    private void BindPFInvoiceLineItems()
    {
        try
        {

            string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();

            DataSet ds = _objBalInvoice.getInvoiceLines(tempUniqueCode);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PFInvListGrid.DataSource = ds;
                PFInvListGrid.DataBind();
            }
            else
            {
                PFInvListGrid.DataSource = ds;
                PFInvListGrid.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindPFInvoiceLineItemsCount()
    {
        try
        {
            decimal total = 0;
            DataTable dt = new DataTable();
            string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();
            DataSet ds = _objBalInvoice.getInvoiceLinesCount(tempUniqueCode);

            dt = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PFInvoiceLineItemCountGrid.DataSource = ds;
                PFInvoiceLineItemCountGrid.DataBind();

                //Invoice Total Sum Ex: Air,Land,Service ....
                total = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                txtPFInvoiceTotalAmount.Text = total.ToString();

            }
            else
            {
                PFInvoiceLineItemCountGrid.DataSource = ds;
                PFInvoiceLineItemCountGrid.DataBind();
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void CleanForm()
    {
        try
        {
            foreach (var item in Page.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    //invoice Bind Methods
    private void BindPFInvoiceDropDown()
    {
        try
        {
            DataSet ds = new DataSet();
            int clientTypeId = 0;
            ds = _objBOUtiltiy.InvoiceDdlBinding(clientTypeId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFInvCosultant.DataSource = ds.Tables[0];
                ddlPFInvCosultant.DataTextField = "Name";
                ddlPFInvCosultant.DataValueField = "ConsultantId";
                ddlPFInvCosultant.DataBind();
                ddlPFInvCosultant.Items.Insert(0, new ListItem("--Select Consultant--", "0"));

                //ddlInvMesg.DataSource = ds.Tables[1];
                //ddlInvMesg.DataTextField = "NpName";
                //ddlInvMesg.DataValueField = "NotePadId";
                //ddlInvMesg.DataBind();
                //ddlInvMesg.Items.Insert(0, new ListItem("--Select Messages--", "0"));

                drpPFInvClientType.DataSource = ds.Tables[2];
                drpPFInvClientType.DataTextField = "Name";
                drpPFInvClientType.DataValueField = "Id";
                drpPFInvClientType.DataBind();
                drpPFInvClientType.Items.Insert(0, new ListItem("--Select Client Type--", "0"));

                drpPFInvBookingSrc.DataSource = ds.Tables[3];
                drpPFInvBookingSrc.DataTextField = "BookingName";
                drpPFInvBookingSrc.DataValueField = "BookingId";
                drpPFInvBookingSrc.DataBind();
                drpPFInvBookingSrc.Items.Insert(0, new ListItem("--Select Booking Source--", "0"));


                drpPFInvBookDest.DataSource = ds.Tables[4];
                drpPFInvBookDest.DataTextField = "BookDestName";
                drpPFInvBookDest.DataValueField = "BookDestId";
                drpPFInvBookDest.DataBind();
                drpPFInvBookDest.Items.Insert(0, new ListItem("--Select Booking Destination--", "0"));

                ViewState["drpPFInvClientName"] = ds.Tables[5];
                ViewState["PFCommissionType"] = ds.Tables[6];
            }
            else
            {

                ddlPFInvCosultant.DataSource = null;
                ddlPFInvCosultant.DataBind();


                drpPFInvClientType.DataSource = null;
                drpPFInvClientType.DataBind();

                drpPFInvBookingSrc.DataSource = null;
                drpPFInvBookingSrc.DataBind();

                drpPFInvBookDest.DataSource = null;
                drpPFInvBookDest.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void drpPFInvClientType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int clientTypeId = Convert.ToInt32(drpPFInvClientType.SelectedItem.Value.ToString());

            if (clientTypeId > 0)
            {
                //DataSet ds = new DataSet();
                //ds = _objBOUtiltiy.InvoiceDdlBinding(clientTypeId);

                DataTable dt = (DataTable)ViewState["drpPFInvClientName"];

                var clientNameList = from list in dt.AsEnumerable()
                                     where Convert.ToInt32(list["ClientType"]) == clientTypeId
                                     select new
                                     {
                                         ClientName = list["ClientNameAccount"].ToString(),
                                         ClientId = list["ClientId"].ToString()
                                     };

                if (clientNameList.ToString() != null)
                {

                    drpPFInvClientName.DataSource = clientNameList;
                    drpPFInvClientName.DataTextField = "ClientName";
                    drpPFInvClientName.DataValueField = "ClientId";
                    drpPFInvClientName.DataBind();
                    drpPFInvClientName.Items.Insert(0, new ListItem("--Select Client--", "0"));
                }
                else
                {
                    drpPFInvClientName.Items.Insert(0, new ListItem("--Select Client Name--", "0"));
                    drpPFInvClientName.DataSource = null;
                    drpPFInvClientName.DataBind();
                }
            }
            else
            {
                drpPFInvClientName.Items.Insert(0, new ListItem("--Select Client Name--", "0"));
            }


            //DataSet ds = new DataSet();
            //ds = _objBOUtiltiy.InvoiceDdlBinding(clientTypeId);

            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    drpPFInvClientName.DataSource = ds.Tables[0];
            //    drpPFInvClientName.DataTextField = "ClientName";
            //    drpPFInvClientName.DataValueField = "ClientId";
            //    drpPFInvClientName.DataBind();
            //    drpPFInvClientName.Items.Insert(0, new ListItem("--Select Client Name--", "0"));
            //}
            //else
            //{

            //    drpPFInvClientName.DataSource = null;
            //    drpPFInvClientName.DataBind();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void drpPFInvClientName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlPFInvMesg.Items.Clear();

            int clientId = Convert.ToInt32(drpPFInvClientName.SelectedItem.Value.ToString());

            DataSet ds = new DataSet();
            ds = _objBalInvoice.Get_ClientmessageandNote(clientId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPFInvMesg.DataSource = ds.Tables[0];
                ddlPFInvMesg.DataTextField = "NpName";
                ddlPFInvMesg.DataValueField = "NotePadId";
                ddlPFInvMesg.DataBind();

                txtPFInvClntMesg.Text = ds.Tables[0].Rows[0]["ClientMessage"].ToString();


            }
            else
            {
                ddlPFInvMesg.Items.Insert(0, new ListItem("--Select Message--", "0"));
                ddlPFInvMesg.DataSource = null;
                ddlPFInvMesg.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    //private void BindPFInvoiceACAnalysis()
    //{

    //    try
    //    {
    //        int TempuniqID = Convert.ToInt32(Session["TempUniqCode"]);
    //        DataSet ds = _objBalInvoice.GetInvoiceACAnalasis(TempuniqID);
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            PFACAnalysisGrid.DataSource = ds;
    //            PFACAnalysisGrid.DataBind();
    //        }

    //        else
    //        {
    //            PFACAnalysisGrid.DataSource = null;
    //            PFACAnalysisGrid.DataBind();
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //    }
    //}
    // AC Analysis
    //protected void btnSubmitPFACAnalsys_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindPFInvoiceACAnalysis();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //    }
    //}
    protected void txtPFAirportTax_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtPFAirportTax.Text = _objBOUtiltiy.FormatTwoDecimal(txtPFAirportTax.Text);
            decimal clientTotal = Convert.ToDecimal(txtPFAirportTax.Text) + Convert.ToDecimal(txtPFAirExcluisvefare.Text);
            if (txtPFAirVatOnFare.Text != "")
            {
                clientTotal = clientTotal + Convert.ToDecimal(txtPFAirVatOnFare.Text);
            }
            txtPFAirClientTot.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
            if (txtPFAirCommisionper.Text != "")
            {
                txtPFAirCommisionper_TextChanged(null, null);
            }

            AirPFPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //protected void btnPFDraftPdf_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string Tempuniqcode = Session["TempUniqCode"].ToString();

    //        // Response.Redirect("DraftPdf.aspx?TempuniqCode=" + Tempuniqcode);
    //        string url = "ProformaDraftPdf.aspx?TempuniqCode=" + Tempuniqcode;
    //        string fullURL = "window.open('" + url + "', '_blank');";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "_blank", fullURL, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //        ExceptionLogging.SendExcepToDB(ex);
    //    }
    //}

    private void PFAirticketClear()
    {
        try
        {
            txtPFAirTicketNo.Text = "";
            drpPFTicketType.SelectedIndex = -1;
            txtPFPnr.Text = "";
            ddlPFAirLine.SelectedIndex = -1;
            drpPFAirPassenger.Text = "";
            txtPFAirConjunction.Text = "";
            ddlPFAirService.SelectedIndex = -1;
            ddlPFType.SelectedIndex = -1;
            txtPFAirRouting.Text = "";
            txtPFAirMiles.Text = "";
            txtPFAirTravelDate.Text = "";
            txtPFAirReturnDate.Text = "";
            txtPFAirExcluisvefare.Text = "";
            txtPFAirCommisionper.Text = "";
            txtPFVatPer.Text = "";
            txtPFAirVatOnFare.Text = "";
            txtPFAirCommExclu.Text = "";
            txtPFAirportTax.Text = "";
            txtPFAirCommVat.Text = "";
            txtPFAirVatinclTax.Text = "";
            txtPFAirAgentVat.Text = "";
            // txtAirClientTot.Text = "";
            txtPFAirCommInclu.Text = "";
            ddlPFAirPayment.SelectedIndex = -1;
            txtPFAirDueToBsp.Text = "";
            ddlPFSoureceref.SelectedIndex = -1;
            lblPFRoutes1.Text = "";
            lblPFRoutes2.Text = "";
            lblPFRoutes3.Text = "";
            lblPFRoutes4.Text = "";
            txtPFFlightNo1.Text = "";
            txtPFClass1.Text = "";
            txtPFDate1.Text = "";
            txtPFFlightNo2.Text = "";
            txtPFClass2.Text = "";
            txtPFDate2.Text = "";
            txtPFFlightNo3.Text = "";
            txtPFClass3.Text = "";
            txtPFDate3.Text = "";
            txtPFFlightNo4.Text = "";
            txtPFClass4.Text = "";
            txtPFDate4.Text = "";
            txtPFFlightNo1.Enabled = false;
            txtPFFlightNo2.Enabled = false;
            txtPFFlightNo3.Enabled = false;
            txtPFFlightNo4.Enabled = false;
            txtPFClass1.Enabled = false;
            txtPFClass2.Enabled = false;
            txtPFClass3.Enabled = false;
            txtPFClass4.Enabled = false;
            txtPFDate1.Enabled = false;
            txtPFDate2.Enabled = false;
            txtPFDate3.Enabled = false;
            txtPFDate4.Enabled = false;
            txtPFAirTravelDate.Enabled = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }

    private void LandArrangemntsClear()
    {
        try
        {

            DDPFlandSupplier.SelectedIndex = -1;
            DDPFlandService.SelectedIndex = -1;
            DDPFlandType.SelectedIndex = -1;
            txtPFlandPassName.Text = "";
            txtPFlandTravelFrom.Text = "";
            txtPFlandTravelto.Text = "";
            txtPFlandBookingRef.Text = "";
            txtPFlandVocher.Text = "";
            txtPFlandSupplierRef.Text = "";
            txtPFlandSuppInvNo.Text = "";
            txtPFlandUnits.Text = "";
            txtPFlandcmblExcl.Text = "";
            txtPFlandTotalExcl.Text = "";
            txtPFlandRateIncl.Text = "";
            txtPFlandCommPer.Text = "";
            txtPFLandExlVatPer.Text = "";
            txtPFlandExclVatAmount.Text = "";
            txtPFlandCmblIncl.Text = "";
            txtPFlandCommExcl.Text = "";
            txtPFlandTotalIncl.Text = "";
            txtPFlandOtherCmblIncl.Text = "";
            txtPFLandVatPer.Text = "";
            DDPFlandPayment.SelectedIndex = -1;
            txtPFlandTotalcmblIncl.Text = "";
            txtPFlandVatAmount.Text = "";
            DDPFlandCreditCard.SelectedIndex = -1;
            txtPFlandNoncmbl.Text = "";
            txtPFlandCommIncl.Text = "";
            //txtlandDuefromclient.Text = "";
            txtPFlandLessComm.Text = "";
            txtPFlandDuetoSupplier.Text = "";
            txtPFlandCO2.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void PFServiceFeeClear()
    {
        try
        {
            ddlPFServiceType.SelectedIndex = -1;
            ddlPFSoureceref.SelectedIndex = -1;
            txtPFSerTravelDate.Text = "";
            ddlPFPassengerName.Items.Clear();
            txtPFExclusAmount.Text = "";
            txtPFserDetails.Text = "";
            txtPFSerVatPer.Text = "";
            txtPFSerVatAmount.Text = "";
            ddlPFPaymentMethod.SelectedIndex = -1;
            chkPFMerge.Checked = false;
            // txtSerClientTotal.Text = "";
            ddlPFCreditCardType.SelectedIndex = -1;
            ddlPFCollectVia.SelectedIndex = -1;
            txtPFTASFMPD.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void PFGeneralChargeClear()
    {
        try
        {
            ddlPFGenchrgType.SelectedIndex = -1;
            ddlPFPassengerNames.SelectedIndex = -1;
            txtPFDetails.Text = "";
            ddlPFCrdCardType.SelectedIndex = -1;
            txtPFUnits.Text = "";
            txtPFRateNet.Text = "";
            txtPFgenvat.Text = "";
            txtPFVatAmount.Text = "";
            txtPFExcluAmount.Text = "";
            // txtClientTotal.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void ddlPFSoureceref_TextChanged(object sender, EventArgs e)
    {
        ddlPFPassengerName.Items.Clear();
        BindPFSerPassengerNames();
        PFSerPopupExtender.Show();
    }

    protected void btnInvCancel_Click(object sender, EventArgs e)
    {
        LandArrangemntsClear();
        Response.Redirect("ProformaInvoiceList.aspx");
    }
    protected void txtPFSerClientTotal_TextChanged(object sender, EventArgs e)
    {
        PFExcusiveAmount();
    }
    protected void btnPFserFee_ServerClick(object sender, EventArgs e)
    {
        //  pnlServiceFee.Visible = false;

        //  SerPopupExtender.Hide();

        string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();

        DataSet ds = _objBalInvoice.getInvoiceLines(tempUniqueCode);
        // first Check Table Count
        if (ds.Tables[0].Rows.Count > 0)
        {
            string TicketType = ds.Tables[0].Rows[0]["Type"].ToString();
            // Check Ticket Type === Air
            if (TicketType == "Air")
            {
                pnlPFServiceFee.Visible = true;
                PFSerPopupExtender.Show();

            }
            else
            {
                pnlPFServiceFee.Visible = false;
                string script = string.Format("alert('Please Book Air Ticket. ');");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script, true);

            }
        }
        else
        {
            // pnlServiceFee.Visible = false;
            string script1 = string.Format("alert('Please Book Air Ticket. ');");
            ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script1, true);

        }
    }
    protected void btnPFGencharge_ServerClick(object sender, EventArgs e)
    {
        string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();

        DataSet ds = _objBalInvoice.getInvoiceLines(tempUniqueCode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string TicketType = ds.Tables[0].Rows[0]["Type"].ToString();
            if (TicketType == "Air")
            {
                GenPFPopupExtender.Show();
            }
            else
            {
                string script = string.Format("alert('Please Book Air Ticket. ');");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script, true);
                GenPFPopupExtender.Hide();
            }
        }
        else
        {
            string script1 = string.Format("alert('Please Book Air Ticket. ');");
            ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script1, true);
            GenPFPopupExtender.Hide();
        }
    }
    protected void ddlPFAirLine_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlAirService.Items.Clear();

            int supplierid = Convert.ToInt32(ddlPFAirLine.SelectedValue.ToString());

            if (ViewState["PFCommissionBasedonAirline"].ToString() != null)
            {
                DataTable dt = (DataTable)ViewState["PFCommissionBasedonAirline"];
            
              string ComId = (dt.AsEnumerable()
                    .Where(p => p["SupplierId"].ToString() == supplierid.ToString())
                    .Select(p => p["ComId"].ToString())).FirstOrDefault();

              if (ComId.ToString() != "0")
              {

                  ddlPFAirService.SelectedValue = ComId.ToString();
              }
              else
              {
                  if (ViewState["ddlPFAirService"].ToString() != null)
                  {
                      //DataTable dtair = (DataTable)ViewState["ddlPFAirService"];

                      //ddlPFAirService.DataSource = dtair;
                      //ddlPFAirService.DataTextField = "ComDesc";
                      //ddlPFAirService.DataValueField = "ComId";
                      //ddlPFAirService.DataBind();
                      //ddlPFAirService.Items.Insert(0, new ListItem("--Select Service--", "0"));

                      AirPFPopupExtender.Show();
                      BindPFAirServiceTypes();
                      ddlPFAirService.SelectedValue = "0";

                  }

                  ddlPFAirService.SelectedValue = "0";
                  //  VASPopupExtender.Show();
              }

              AirPFPopupExtender.Show();
            }

            decimal commperc = 0;
            int commId = Convert.ToInt32(ddlPFAirService.SelectedValue);


            DataTable commdt = (DataTable)ViewState["PFCommissionType"];
            commperc = Convert.ToDecimal((commdt.AsEnumerable()
                .Where(p => p["ComId"].ToString() == commId.ToString())
                .Select(p => p["ComDComm"].ToString())).FirstOrDefault());

            commperc = string.IsNullOrEmpty(commperc.ToString()) ? 0 : Convert.ToDecimal(commperc.ToString());

            txtPFAirCommisionper.Text = _objBOUtiltiy.FormatTwoDecimal(commperc.ToString());
            txtPFAirCommisionper_TextChanged(null, null);

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void DDPFlandSupplier_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //  DDlandService.Items.Clear();
            int supplierid = Convert.ToInt32(DDPFlandSupplier.SelectedValue.ToString());
          
            DataTable dt = (DataTable)ViewState["PFAllCommissionTypes_Land"];
            string comId = (dt.AsEnumerable()
                .Where(p => p["LSupplierId"].ToString() == supplierid.ToString())
                .Select(p => p["ComId"].ToString())).FirstOrDefault();

            if (comId.ToString() != "0")
            {
                DDPFlandService.SelectedValue = comId;
                landPFPopExtender.Show();
            }

            else
            {
                landPFPopExtender.Show();
                BindPFAirServiceTypes();
                DDPFlandService.SelectedValue = "0";
            }

            decimal commperc = 0;

            int commId = Convert.ToInt32(DDPFlandService.SelectedItem.Value);
            DataTable commdt = (DataTable)ViewState["PFCommissionType"];
            commperc = Convert.ToDecimal((commdt.AsEnumerable()
                .Where(p => p["ComId"].ToString() == commId.ToString())
                .Select(p => p["ComDComm"].ToString())).FirstOrDefault());

            commperc = string.IsNullOrEmpty(commperc.ToString()) ? 0 : Convert.ToDecimal(commperc.ToString());

            txtPFlandCommPer.Text = _objBOUtiltiy.FormatTwoDecimal(commperc.ToString());


        }


        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void DDPFlandService_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        decimal commperc = 0;

        int commId = Convert.ToInt32(DDPFlandService.SelectedItem.Value);
        DataTable commdt = (DataTable)ViewState["PFCommissionType"];
        commperc = Convert.ToDecimal((commdt.AsEnumerable()
            .Where(p => p["ComId"].ToString() == commId.ToString())
            .Select(p => p["ComDComm"].ToString())).FirstOrDefault());

        commperc = string.IsNullOrEmpty(commperc.ToString()) ? 0 : Convert.ToDecimal(commperc.ToString());

        txtPFlandCommPer.Text = _objBOUtiltiy.FormatTwoDecimal(commperc.ToString());

        landPFPopExtender.Show();
    }
}
