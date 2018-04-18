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

public partial class Admin_AutoInvoice : System.Web.UI.Page
{
    DOUtility _doUtilities = new DOUtility();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    EmAirTicket _objAirTicket = new EmAirTicket();
    BACommission _objBalCommission = new BACommission();
    EMCommission _objCommission = new EMCommission();

    EmInvoice _objEmInvoice = new EmInvoice();
    DALInvoice _objDalInvoice = new DALInvoice();
    BALInvoice _objBalInvoice = new BALInvoice();
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
    //

    EMAirSuppliers objEMAirsupp = new EMAirSuppliers();
    BAAirSuppliers objAirSupplier = new BAAirSuppliers();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlInvMesg.Enabled = false;
            BindImportedTicketData();
            //general Charge
            DataSet objDs = null; // Single sp
            BindGenServiceTypes();
            //BindGenPassengerNames();
            BindGenCreditCardType();
            txtRateNet.Enabled = false;
            txtVatAmount.Enabled = false;
            txtExcluAmount.Enabled = false;
            txtClientTotal.Enabled = false;
            //service fee---



            BindSerServiceTypes();
            ddlPassengerName.Enabled = false;

            BindPaymentType();

            ddlCreditCardType.Enabled = false;
            ddlCollectVia.Enabled = false;
            txtTASFMPD.Enabled = false;
            txtClientTotal.Enabled = false;
            rfvtxtTASFMPD.Enabled = false;
            rfvddlCollectVia.Enabled = false;
            rfvddlCreditCardType.Enabled = false;


            //land
            BindLandSuppliers();
            BindType();
            BindLandPaymentType();
            BindLandService();
            DDlandCreditCard.Enabled = false;
            txtLandVatPer.Enabled = false;
            txtLandExlVatPer.Enabled = false;
            txtlandDuefromclient.Enabled = false;
            txtlandLessComm.Enabled = false;
            txtlandDuetoSupplier.Enabled = false;
            txtlandCommIncl.Enabled = false;
            txtlandVatAmount.Enabled = false;
            txtlandCommExcl.Enabled = false;

            txtlandExclVatAmount.Enabled = false;
            txtlandCmblIncl.Enabled = false;
            txtlandcmblExcl.Enabled = false;
            txtlandTotalcmblIncl.Enabled = false;
            // Session["TempUniqCode"] = "";

            // BindInvoiceLineItemsCount();
            //invoice Loading
            BindInvoiceDropDown();

        }
    }
    //importing tickets data form text files

    private void BindImportedTicketData()
    {

        try
        {
            int t5id = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                t5id = Convert.ToInt32(Request.QueryString["id"]);
            }
            BAAutoInvoice objBAAutoInvoice = new BAAutoInvoice();
            EmAirTicket objEmAirTicket = new EmAirTicket();
            //int InvId = 0;
            DataSet ds = objBAAutoInvoice.GetTicketLevlData(t5id);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //InvListGrid.DataSource = ds;
                //InvListGrid.DataBind();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //  objEmAirTicket.Airline = Convert.ToInt32(ds.Tables[0].Rows[i]["Airline"].ToString());
                    objEmAirTicket.AirTicketNo = ds.Tables[0].Rows[i]["TicketNO"].ToString();
                    objEmAirTicket.AirPnr = ds.Tables[0].Rows[i]["PNR"].ToString();
                    objEmAirTicket.AirPassenger = ds.Tables[0].Rows[i]["PassengerName"].ToString();
                    objEmAirTicket.AirClientTotal = ds.Tables[0].Rows[i]["ClientTotal"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["ClientTotal"].ToString()) : 0;
                    objEmAirTicket.SupplOpenAmount = ds.Tables[0].Rows[i]["ClientTotal"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["ClientTotal"].ToString()) : 0;

                    objEmAirTicket.AirDueToBsp = ds.Tables[0].Rows[i]["ClientTotal"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["ClientTotal"].ToString()) : 0;

                    objEmAirTicket.AirExclusiveFare = ds.Tables[0].Rows[i]["ExclAmt"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["ExclAmt"].ToString()) : 0;

                    objEmAirTicket.AirportTaxes = (ds.Tables[0].Rows[i]["tax1"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["tax1"].ToString()) : 0)
                        + (ds.Tables[0].Rows[i]["tax2"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["tax2"].ToString()) : 0)
                        + (ds.Tables[0].Rows[i]["tax3"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["tax3"].ToString()) : 0);
                    objEmAirTicket.AirRouting = ds.Tables[0].Rows[i]["Route1"].ToString();

                    objEmAirTicket.InvoiceType = "Air";
                    _objAirTicket.RefundAmount = 0;
                    string TypeCode = ds.Tables[0].Rows[i]["Type"].ToString() != "" ? ds.Tables[0].Rows[i]["Type"].ToString() : "0";

                    if (TypeCode != "")
                    {
                        try
                        {
                            DataSet DsTypeMaster = new DataSet();
                            DsTypeMaster = objBAAutoInvoice.GetAirSupllierId(TypeCode);
                            if (DsTypeMaster.Tables[1].Rows.Count > 0)
                            {
                                objEmAirTicket.Type = DsTypeMaster.Tables[1].Rows[0]["TypeId"].ToString() != "" ? Convert.ToInt32(DsTypeMaster.Tables[1].Rows[0]["TypeId"]) : 0;
                            }
                            else
                            {
                                objEmAirTicket.Type = 0;
                            }
                        }
                        catch (Exception Ex)
                        {
                            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", Ex.Message);
                        }
                    }
                    else
                    {
                        objEmAirTicket.Type = 0;
                    }
                    objEmAirTicket.AirService = 0;
                    objEmAirTicket.AirTicketType = 0;
                    objEmAirTicket.AirVatonFare = ds.Tables[0].Rows[i]["VAT"].ToString() != "" ? Convert.ToDecimal(ds.Tables[0].Rows[i]["VAT"].ToString()) : 0; ;
                    objEmAirTicket.AirVatInTaxes = 0;
                    objEmAirTicket.AirPayment = 0;
                    objEmAirTicket.AirCreditCardType = 0;
                    objEmAirTicket.AirCommPer = 0;
                    objEmAirTicket.AirCommExcl = 0;
                    objEmAirTicket.AirCommInclu = 0;
                    objEmAirTicket.AirCommVatPer = 0;
                    objEmAirTicket.AirAgentVat = 0;
                    objEmAirTicket.invDocumentNo = "0";
                    objEmAirTicket.AirRoutTempID = "0";
                    string AirLineName = ds.Tables[0].Rows[i]["prn"].ToString() != "" ? ds.Tables[0].Rows[i]["prn"].ToString() : "";

                    string chartedaccount = "800/" + AirLineName;
                    string AirlineNames= ds.Tables[0].Rows[i]["Airline"].ToString();
                    if (AirLineName != "")
                    {
                        try
                        {
                            //Check AirSupplier
                            DataSet DsAirLine = new DataSet();
                            DsAirLine = objBAAutoInvoice.GetAirSupllierId(AirLineName);
                            if (DsAirLine.Tables[0].Rows.Count > 0)
                            {
                                objEmAirTicket.Airline = DsAirLine.Tables[0].Rows[0]["SupplierId"].ToString() != "" ? Convert.ToInt32(DsAirLine.Tables[0].Rows[0]["SupplierId"]) : 0;
                            }
                            else
                            {
                                //Insert AirSupplier
                                objEmAirTicket.SupAccountCode = "800/";
                                objEmAirTicket.AirlineID = AirLineName;
                                objEmAirTicket.IsActive = 1;
                                objEmAirTicket.SupMainGIAccCode = "800/" + AirLineName;
                                objEmAirTicket.AirlineName =AirlineNames;
                                objEmAirTicket.AirService = 81;
                                int Result12 = _objBALAirTicket.InsertSupplier(objEmAirTicket);


                                //Check Charted Account 
                                DataSet DsCharteAccount = new DataSet();
                                DsCharteAccount = objBAAutoInvoice.GetChartedAccount(chartedaccount);
                                
                                //Charted Accounts Insert
                                DataSet dset = _objBOUtiltiy.getMainAccounts();
                                if (DsCharteAccount.Tables[0].Rows.Count == 0)
                                {

                                    objEMAirsupp.ChartedAccName = AirlineNames;
                                    objEMAirsupp.ChartedMasterAccName = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(dset.Tables[1].Rows[0]["MainAccId"].ToString());
                                    objEMAirsupp.Type = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : dset.Tables[1].Rows[0]["AcType"].ToString();
                                    objEMAirsupp.BaseCurrency = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["BaseCurrency"].ToString()) ? 0 : Convert.ToInt32(dset.Tables[1].Rows[0]["BaseCurrency"].ToString());
                                    objEMAirsupp.TranCurrency = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["TranCurrency"].ToString()) ? 0 : Convert.ToInt32(dset.Tables[1].Rows[0]["TranCurrency"].ToString());
                                    objEMAirsupp.DepartmentId = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["DepartmentId"].ToString()) ? 0 : Convert.ToInt32(dset.Tables[1].Rows[0]["DepartmentId"].ToString());
                                    objEMAirsupp.AccCode = "800/" + AirLineName;
                                    objEMAirsupp.CategoryId = string.IsNullOrEmpty(dset.Tables[1].Rows[0]["CategoryId"].ToString()) ? 0 : Convert.ToInt32(dset.Tables[1].Rows[0]["CategoryId"].ToString());
                                    objEMAirsupp.RefType = "AirSupplier";

                                    int ChartedResult = objAirSupplier.InsUpdChartAccounts(objEMAirsupp);

                                }

                                DsAirLine = objBAAutoInvoice.GetAirSupllierId(AirLineName);
                                if (DsAirLine.Tables[0].Rows.Count > 0)
                                {
                                    objEmAirTicket.Airline = DsAirLine.Tables[0].Rows[0]["SupplierId"].ToString() != "" ? Convert.ToInt32(DsAirLine.Tables[0].Rows[0]["SupplierId"]) : 0;
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", Ex.Message);
                        }
                    }
                    else
                    {
                        objEmAirTicket.Airline = 0;
                    }

                    objEmAirTicket.AirMiles = "0";
                    objEmAirTicket.Conjunction = "";
                    objEmAirTicket.AirTravelDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TravelDate"].ToString());
                    _objAirTicket.AirReturnDate = null;

                    //  objEmAirTicket.AirDueToBsp = 0;

                    if (Session["TempUniqCode"] == null || Session["TempUniqCode"] == "")
                    {
                        objEmAirTicket.TempUniqCode = uniqueIdSession();
                    }
                    else
                    {
                        objEmAirTicket.TempUniqCode = Session["TempUniqCode"].ToString();
                    }
                    if (Session["RoutTempID"] == null || Session["RoutTempID"] == "")
                    {
                        _objAirTicket.AirRoutTempID = uniqueIdSession();
                    }
                    else
                    {
                        _objAirTicket.AirRoutTempID = Session["RoutTempID"].ToString();
                    }

                    _objAirTicket.invDocumentNo = "0";

                    objEmAirTicket.CreatedBy = 0;
                    int Result = _objBALAirTicket.InsertAirTicket(objEmAirTicket);
                    if (Result > 0)
                    {


                        lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Airticket created Successfully");
                        // clearcontrols();
                        Session["TempUniqCode"] = objEmAirTicket.TempUniqCode;

                        BindInvoiceLineItems();
                        BindInvoiceLineItemsCount();
                        ddlSoureceref.Items.Clear();
                        ddlPassengerName.Items.Clear();
                        BindSerTicketNumber();
                        ddlPassengerNames.Items.Clear();
                        BindGenPassengerNames();
                    }
                    else
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Airticket Details was not created plase try again");
                    }

                }



                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[1].Rows)
                    {
                        int ConsultantId = Convert.ToInt32(dtlRow["Consultant"].ToString());
                        // ddlInvCosultant.Items.FindByValue().Selected = true;
                        ddlInvCosultant.SelectedValue = ConsultantId.ToString();

                    }


                }

                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[2].Rows)
                    {
                        int clienttype = Convert.ToInt32(dtlRow["ClientType"].ToString());
                        if (clienttype == null)
                        {
                            clienttype = 0;
                        }
                        int clientname = Convert.ToInt32(dtlRow["ClientId"].ToString());
                        string messagetype = dtlRow["ClientMessage"].ToString();
                        int note = Convert.ToInt32(dtlRow["ClientNotes"].ToString());

                        // ddlInvCosultant.Items.FindByValue().Selected = true;
                        drpInvClientType.SelectedValue = clienttype.ToString();
                        BindClientData(clienttype);
                        ddlInvMesg.SelectedValue = note.ToString();
                        txtInvClntMesg.Text = messagetype;
                        drpInvClientName.SelectedValue = clientname.ToString();

                    }


                }
                else
                {
                    // BindClientData(0);
                    //InvListGrid.DataSource = null;
                    //InvListGrid.DataBind();
                }
            }

            else
            {
                InvListGrid.DataSource = null;
                InvListGrid.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindClientData(int clienttype)
    {
        try
        {


            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.InvoiceDdlBinding(clienttype);

            if (ds.Tables[0].Rows.Count > 0)
            {

                drpInvClientName.DataSource = ds.Tables[0];
                drpInvClientName.DataTextField = "ClientName";
                drpInvClientName.DataValueField = "ClientId";
                drpInvClientName.DataBind();
                drpInvClientName.Items.Insert(0, new ListItem("--Select Consultant--", "0"));
            }
            else
            {

                drpInvClientName.DataSource = null;
                drpInvClientName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void cmdClose_Click(object sender, ImageClickEventArgs e)
    {
        landPopExtender.Hide();
    }
    protected void btnInvSave_Click(object sender, EventArgs e)
    {

        InsertInvoice();

    }

    private void InsertInvoice()
    {

        try
        {
            _objEmInvoice.InvDate = string.IsNullOrEmpty(txtInvDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtInvDate.Text);
            _objEmInvoice.ClientTypeId = Convert.ToInt32(drpInvClientType.SelectedValue.ToString());
            _objEmInvoice.ClientNameId = Convert.ToInt32(drpInvClientName.SelectedValue.ToString());

            _objEmInvoice.ConsultantId = Convert.ToInt32(ddlInvCosultant.SelectedValue.ToString());
            _objEmInvoice.InvOrder = string.IsNullOrEmpty(txtInvOrder.Text) ? "" : (txtInvOrder.Text);
            _objEmInvoice.BookingNo = string.IsNullOrEmpty(txtInvBookNo.Text) ? 0 : Convert.ToInt32(txtInvOrder.Text);
            _objEmInvoice.BookSourceId = string.IsNullOrEmpty(drpInvBookingSrc.SelectedValue.ToString()) ? 0 : Convert.ToInt32(drpInvBookingSrc.SelectedValue.ToString());
            _objEmInvoice.BookDestinationId = string.IsNullOrEmpty(drpInvBookingSrc.SelectedValue.ToString()) ? 0 : Convert.ToInt32(drpInvBookingSrc.SelectedValue.ToString());

            _objEmInvoice.TempUniqCode = Session["TempUniqCode"].ToString();
            _objEmInvoice.invDocumentNo = "Hof." + Session["TempUniqCode"].ToString();

            _objEmInvoice.InvoiceTotal = Convert.ToDecimal(txtInvoiceTotalAmount.Text);
            _objEmInvoice.InvoiceOpenAmount = Convert.ToDecimal(txtInvoiceTotalAmount.Text);
            _objEmInvoice.TotalCommInclu = Convert.ToDecimal(lblcommissionInclu.Text);
            _objEmInvoice.TotalCommExclu = Convert.ToDecimal(lblcommissionExclu.Text);
            _objEmInvoice.TotalCommVatAmount = Convert.ToDecimal(lblcommissionVatamt.Text);
            _objEmInvoice.AirCommi = Convert.ToDecimal(string.IsNullOrEmpty(lblaircommiinclu.Text) ? 0 : Convert.ToDecimal(lblaircommiinclu.Text));
            _objEmInvoice.LandCommi = Convert.ToDecimal(string.IsNullOrEmpty(lbllandcommiInclu.Text) ? 0 : Convert.ToDecimal(lbllandcommiInclu.Text));
            _objEmInvoice.ServicefeeCommi = Convert.ToDecimal(string.IsNullOrEmpty(lblservcommi.Text) ? 0 : Convert.ToDecimal(lblservcommi.Text));

            _objEmInvoice.AirTotal = 0;
            _objEmInvoice.LandTotal = 0;
            _objEmInvoice.ServiceTot = 0;
            _objEmInvoice.GenChargeTot = 0;


            _objEmInvoice.Message = txtInvClntMesg.Text;
            _objEmInvoice.MessageType = string.IsNullOrEmpty(ddlInvMesg.SelectedValue.ToString()) ? 0 : Convert.ToInt32(ddlInvMesg.SelectedValue.ToString());
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
            _objEmInvoice.PrintStyleId = Convert.ToInt32(ddlInvPdfPrintStyle.SelectedValue.ToString());
            _objEmInvoice.CreatedBy = 0;
            _objEmInvoice.RefundAmount = 0;
            int Result = _objBalInvoice.InsertInvoice(_objEmInvoice);

            if (Result > 0)
            {

                string invDocumentNo = _objEmInvoice.invDocumentNo;

                // DataSet dst = new DataSet();
                DataSet dsMainacc = new DataSet();
                int mainAccount = 0;
                dsMainacc = _objBalCommission.GetCommiChartedAccId("Commision Received");

                if (dsMainacc.Tables[0].Rows.Count > 0)
                {
                    mainAccount = Convert.ToInt32(dsMainacc.Tables[0].Rows[0]["ChartedAccId"].ToString());
                }



                if (lblaircommType.Text == "Air")
                {
                    int ChartedAccId = 0;
                    DataSet ds = new DataSet();
                    ds = _objBalCommission.GetCommiChartedAccId("Commission-Flight");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ChartedAccId = Convert.ToInt32(ds.Tables[0].Rows[0]["ChartedAccId"].ToString());
                        _objCommission.TolCommiAccount = mainAccount;
                        _objCommission.ChartedAccCode = ChartedAccId;
                        _objCommission.CommiAmount = Convert.ToDecimal(lblaircommiinclu.Text);
                        _objCommission.TicketType = lblaircommType.Text;
                        _objCommission.Invid = Result;
                        _objCommission.InvDocumentNo = invDocumentNo;

                        int commiresult = _objBalCommission.InsertUpdateCommission(_objCommission);
                    }



                }
                if (lbllandcommType.Text == "Land")
                {
                    int ChartedAccId = 0;
                    DataSet ds = new DataSet();
                    ds = _objBalCommission.GetCommiChartedAccId("Commission-Hotels");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ChartedAccId = Convert.ToInt32(ds.Tables[0].Rows[0]["ChartedAccId"].ToString());
                        _objCommission.TolCommiAccount = mainAccount;
                        _objCommission.ChartedAccCode = ChartedAccId;
                        _objCommission.CommiAmount = Convert.ToDecimal(lbllandcommiInclu.Text);
                        _objCommission.TicketType = lbllandcommType.Text;
                        _objCommission.Invid = Result;
                        _objCommission.InvDocumentNo = invDocumentNo;

                        int commiresult = _objBalCommission.InsertUpdateCommission(_objCommission);
                    }

                }

                if (lblsercommType.Text == "SF")
                {
                    int ChartedAccId = 0;
                    DataSet ds = new DataSet();
                    ds = _objBalCommission.GetCommiChartedAccId("Service Fees");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ChartedAccId = Convert.ToInt32(ds.Tables[0].Rows[0]["ChartedAccId"].ToString());
                        _objCommission.TolCommiAccount = mainAccount;
                        _objCommission.ChartedAccCode = ChartedAccId;
                        _objCommission.CommiAmount = Convert.ToDecimal(lblservcommi.Text);
                        _objCommission.TicketType = lblsercommType.Text;
                        _objCommission.Invid = Result;
                        _objCommission.InvDocumentNo = invDocumentNo;

                        int commiresult = _objBalCommission.InsertUpdateCommission(_objCommission);
                    }

                }


                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Invoice created Successfully");
                // clearcontrols();

                int t5id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    t5id = Convert.ToInt32(Request.QueryString["id"]);
                }


                int UpdateInvResult = _objBalInvoice.updateInvId(Result, Session["TempUniqCode"].ToString(), invDocumentNo, t5id);


                if (UpdateInvResult > 0)
                {
                    //lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "AirTicket created Successfully");
                    Response.Redirect("InvoiceList.aspx");
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
            ExceptionLogging.SendExcepToDB(ex);
        }
    }




    private static string uniqueIdSession()
    {
        var now = DateTime.Now;
        DateTime zeroDate = DateTime.MinValue.AddDays(now.Day).AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
        string uniqueId = (zeroDate.Ticks / 10000).ToString();
        return uniqueId;
    }


    //general Charge And service fee
    /////---------General Charge------------------///////////////////

    private void InsertGeneralCharge()
    {
        try
        {
            _objEmGenCharge.GenChgId = Convert.ToInt32(hf_GC_TicketNo.Value);

            _objEmGenCharge.Type = Convert.ToInt32(ddlGenchrgType.SelectedValue.ToString());
            _objEmGenCharge.PassengerName = ddlPassengerNames.SelectedValue.ToString();
            _objEmGenCharge.Details = txtDetails.Text;
            _objEmGenCharge.CreditCard = Convert.ToInt32(ddlCrdCardType.SelectedValue.ToString());
            _objEmGenCharge.Units = Convert.ToInt32(txtUnits.Text);
            _objEmGenCharge.RateNet = Convert.ToDecimal(txtRateNet.Text);
            _objEmGenCharge.VatPer = string.IsNullOrEmpty(txtgenvat.Text) ? 0 : Convert.ToDecimal(txtgenvat.Text);
            _objEmGenCharge.VatAmount = string.IsNullOrEmpty(txtVatAmount.Text) ? 0 : Convert.ToDecimal(txtVatAmount.Text);

            _objEmGenCharge.ExcluAmt = Convert.ToDecimal(txtExcluAmount.Text);
            _objEmGenCharge.ClientTot = Convert.ToDecimal(txtClientTotal.Text);
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
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "General Charge created Successfully");
                clearcontrols();
                BindInvoiceLineItems();
                BindInvoiceLineItemsCount();
                GeneralChargeClear();

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "General Charge Details was not created plase try again");
            }
            GenPopupExtender.Hide();
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    private void clearcontrols()
    {

    }

    private void BindGenPassengerNames()
    {
        try
        {
            DataSet ds = new DataSet();
            string tempuniqcode = Session["TempUniqCode"].ToString();
            int airtickno = 0;
            ds = _objBalservice.BindPassengerNames(tempuniqcode, airtickno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPassengerNames.DataSource = ds.Tables[0];
                ddlPassengerNames.DataTextField = "AirPassenger";
                ddlPassengerNames.DataValueField = "TicketId";
                ddlPassengerNames.DataBind();
                ddlPassengerNames.Items.Insert(0, new ListItem("--Select Passenger Name--", "0"));
            }
            else
            {
                ddlPassengerNames.Items.Insert(0, new ListItem("--Select Passenger Name--", "0"));
                ddlPassengerNames.DataSource = null;
                ddlPassengerNames.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindGenServiceTypes()
    {
        try
        {
            string Type = "GC";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlGenchrgType.DataSource = ds.Tables[0];
                ddlGenchrgType.DataTextField = "ComDesc";
                ddlGenchrgType.DataValueField = "ComId";
                ddlGenchrgType.DataBind();
                ddlGenchrgType.Items.Insert(0, new ListItem("--Select Payment--", "0"));

            }
            else
            {
                ddlGenchrgType.Items.Insert(0, new ListItem("--Select Payment--", "0"));
                ddlGenchrgType.DataSource = null;
                ddlGenchrgType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindGenCreditCardType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.BindCreditCardType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCrdCardType.DataSource = ds.Tables[0];
                ddlCrdCardType.DataTextField = "CardDescription";
                ddlCrdCardType.DataValueField = "CrdCardId";
                ddlCrdCardType.DataBind();
                ddlCrdCardType.Items.Insert(0, new ListItem("--Select Payment--", "0"));
            }
            else
            {
                ddlCrdCardType.Items.Insert(0, new ListItem("--Select Payment--", "0"));
                ddlCrdCardType.DataSource = null;
                ddlCrdCardType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindGenTicketNumber()
    {
        try
        {
            DataSet ds = new DataSet();
            string tempuniqcode = Session["TempUniqCode"].ToString();
            ds = _objDalService.BindTicketNumber(tempuniqcode);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlPassengerName.DataSource = ds.Tables[0];
                ddlPassengerName.DataTextField = "TktNumber";
                ddlPassengerName.DataValueField = "TktIdId";
                ddlPassengerName.DataBind();
                ddlPassengerName.Items.Insert(0, new ListItem("--Select Payment--", "0"));


            }
            else
            {
                ddlPassengerName.Items.Insert(0, new ListItem("--Select Payment--", "0"));
                ddlPassengerName.DataSource = null;
                ddlPassengerName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void ddlGenchrgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ddlText = ddlGenchrgType.SelectedItem.Text;
            int ddlValue = Convert.ToInt32(ddlGenchrgType.SelectedItem.Value);
            txtDetails.Text = ddlText;

            DataTable dt = (DataTable)ViewState["ServiceType_GetDataByTYpe"];
            string VatPer = (dt.AsEnumerable()
                .Where(p => p["ComId"].ToString() == ddlValue.ToString())
                .Where(p => p["ComDesc"].ToString() == ddlText.ToString())
                .Select(p => p["VatRate"].ToString())).FirstOrDefault();


            if (VatPer != "" && VatPer != null)
            {
                txtgenvat.Text = VatPer.ToString();
            }
            if (txtUnits.Text != "")

                txtRateNet_TextChanged(null, null);
            GenPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtRateNet_TextChanged(object sender, EventArgs e)
    {

        try
        {
            decimal units = Convert.ToDecimal(txtUnits.Text);
            txtRateNet.Text = _objBOUtiltiy.FormatTwoDecimal(txtRateNet.Text);
            decimal RateNet = Convert.ToDecimal(txtRateNet.Text);
            decimal ExclTotal = (units) * (RateNet);
            txtExcluAmount.Text = ExclTotal.ToString();
            if (txtgenvat.Text == "" || txtgenvat.Text == "0")
            {
                txtClientTotal.Text = ExclTotal.ToString();

            }
            else
            {

                txtExcluAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ExclTotal.ToString());
                decimal Vatper = Convert.ToDecimal(txtgenvat.Text);
                decimal vatAmount = ((Vatper / 100) * ExclTotal);
                txtVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(vatAmount.ToString());
                decimal clientTotal = ExclTotal + vatAmount;
                txtClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
            }
            GenPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void btnGencharge_click(object sender, EventArgs e)
    {
        InsertGeneralCharge();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void txtUnits_TextChanged(object sender, EventArgs e)
    {
        txtRateNet.Enabled = true;
        if (txtRateNet.Text != "")

            txtRateNet_TextChanged(null, null);

        GenPopupExtender.Show();
    }

    //------------Service Fee--------//


    protected void ServFee_click(object sender, EventArgs e)
    {
        InsertUpdateServiceFee();

    }

    protected void btnSerCancel_Click(object sender, EventArgs e)
    {

    }
    private void InsertUpdateServiceFee()
    {
        try
        {

            //hf_SF_TicketNo
            _objEmService.ServiceFeeId = Convert.ToInt32(hf_SF_TicketNo.Value);
            DateTime date = DateTime.ParseExact(txtSerTravelDate.Text, "yyyy-MM-dd", null);

            _objEmService.Type = Convert.ToInt32(ddlServiceType.SelectedValue.ToString());
            _objEmService.SourceRef = ddlSoureceref.SelectedValue.ToString();

            if (chkMerge.Checked)
                _objEmService.MergeC = Convert.ToInt32(chkMerge.Checked);

            decimal mergeAmt = 0;

            if (chkMerge.Checked == true)
            {
                var mergeAmount = _objDalService.GetSFMergeAmount(Convert.ToInt32(ddlSoureceref.SelectedValue.ToString()), Convert.ToInt32(chkMerge.Checked));

                mergeAmt = (Convert.ToDecimal(string.IsNullOrEmpty(mergeAmount.ToString().Trim()) ? "0" : mergeAmount.ToString().Trim())) + Convert.ToDecimal(txtSerClientTotal.Text);
            }


            _objAirTicket.AirTravelDate = string.IsNullOrEmpty(txtAirTravelDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtAirTravelDate.Text);

            // _objEmService.TravelDate = System.DateTime.Now;
            _objEmService.TravelDate = date;
            _objEmService.PassengerName = Convert.ToInt32(string.IsNullOrEmpty(ddlPassengerName.SelectedValue.ToString()) ? "0" : ddlPassengerName.SelectedValue.ToString().Trim());
            string Exclusu = string.IsNullOrEmpty(txtExclusAmount.Text.Trim()) ? ".0000" : txtExclusAmount.Text.Trim();
            _objEmService.ExcluAmount = Convert.ToDecimal(Exclusu);
            _objEmService.Details = txtserDetails.Text;
            string Vartxt = string.IsNullOrEmpty(txtSerVatPer.Text.Trim()) ? "0" : txtSerVatPer.Text.Trim();
            _objEmService.VatPer = Convert.ToDecimal(Vartxt);
            _objEmService.VatAmount = Convert.ToDecimal(txtSerVatAmount.Text);
            _objEmService.PaymentMethod = Convert.ToInt32(ddlPaymentMethod.SelectedValue.ToString());
            string clientTotal = string.IsNullOrEmpty(txtSerClientTotal.Text.Trim()) ? ".0000" : txtSerClientTotal.Text.Trim();
            _objEmService.ClientTot = Convert.ToDecimal(clientTotal);
            _objEmService.CreditCardType = Convert.ToInt32(string.IsNullOrEmpty(ddlCreditCardType.SelectedValue.ToString().Trim()) ? "0" : ddlCreditCardType.SelectedValue.ToString().Trim());
            _objEmService.CollectVia = Convert.ToInt32(string.IsNullOrEmpty(ddlCollectVia.SelectedValue.ToString().Trim()) ? "0" : ddlCollectVia.SelectedValue.ToString().Trim());

            _objEmService.TasfMpd = txtTASFMPD.Text;
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
                BindInvoiceLineItems();
                BindInvoiceLineItemsCount();
                ServiceFeeClear();

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "ServiceFee Details was not created plase try again");
            }
            SerPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    private void BindSerServiceTypes()
    {
        try
        {
            string Type = "SF";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);
            ViewState["ServiceType_GetDataByTYpe"] = ds.Tables[1];
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlServiceType.DataSource = ds.Tables[0];
                ddlServiceType.DataTextField = "ComDesc";
                ddlServiceType.DataValueField = "ComId";
                ddlServiceType.DataBind();
                ddlServiceType.Items.Insert(0, new ListItem("--Select Payment--", "0"));
            }
            else
            {
                ddlServiceType.DataSource = null;
                ddlServiceType.DataBind();
                ddlServiceType.Items.Insert(0, new ListItem("--Select Payment--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    public void BindPaymentType()
    {
        try
        {
            DataSet ds = new DataSet();
            int payemntid = 0;
            ds = _doUtilities.BindPaymentType(payemntid);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPaymentMethod.DataSource = ds.Tables[0];
                ddlPaymentMethod.DataTextField = "PaymentName";
                ddlPaymentMethod.DataValueField = "PaymentId";
                ddlPaymentMethod.DataBind();
                ddlPaymentMethod.Items.Insert(0, new ListItem("--Select--", "0"));




            }
            else
            {
                ddlPaymentMethod.DataSource = null;
                ddlPaymentMethod.DataBind();
                ddlPaymentMethod.Items.Insert(0, new ListItem("--Select--", "0"));


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    private void BindSerCreditCardType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.BindCreditCardType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCreditCardType.DataSource = ds.Tables[0];
                ddlCreditCardType.DataTextField = "CardDescription";
                ddlCreditCardType.DataValueField = "CrdCardId";
                ddlCreditCardType.DataBind();
                ddlCreditCardType.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlCreditCardType.DataSource = null;
                ddlCreditCardType.DataBind();
                ddlCreditCardType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindSerTicketNumber()
    {
        try
        {
            DataSet ds = new DataSet();

            string tempuniqcode = Session["TempUniqCode"].ToString();
            ds = _objDalService.BindTicketNumber(tempuniqcode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSoureceref.DataSource = ds.Tables[0];
                ddlSoureceref.DataTextField = "AirTicketNo";
                ddlSoureceref.DataValueField = "TicketId";
                ddlSoureceref.DataBind();
                ddlSoureceref.Items.Insert(0, new ListItem("--Select Ticket No--", "0"));
            }
            else
            {
                ddlSoureceref.DataSource = null;
                ddlSoureceref.DataBind();
                ddlSoureceref.Items.Insert(0, new ListItem("--Select Ticket No--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindSerPassengerNames()
    {
        try
        {
            DataSet ds = new DataSet();
            int airtickno = Convert.ToInt32(ddlSoureceref.SelectedItem.Value.ToString());
            string tempuniqcode = " ";
            ds = _objBalservice.BindPassengerNames(tempuniqcode, airtickno);

            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlPassengerName.DataSource = ds.Tables[1];
                ddlPassengerName.DataTextField = "AirPassenger";
                ddlPassengerName.DataValueField = "TicketId";
                ddlPassengerName.DataBind();

            }
            else
            {
                ddlPassengerName.DataSource = null;
                ddlPassengerName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindCollectVia()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objDalService.BindCollectVia();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollectVia.DataSource = ds.Tables[0];
                ddlCollectVia.DataTextField = "CollectName";
                ddlCollectVia.DataValueField = "ColletViaId";
                ddlCollectVia.DataBind();
                ddlCollectVia.Items.Insert(0, new ListItem("--Select Collect Via --", "0"));
            }
            else
            {
                ddlCollectVia.Items.Insert(0, new ListItem("--Select Collect Via--", "0"));
                ddlCollectVia.DataSource = null;
                ddlCollectVia.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentMethod.SelectedValue == "2")
            {
                BindCollectVia();

                BindSerCreditCardType();

                ddlCreditCardType.Enabled = true;
                ddlCollectVia.Enabled = true;
                txtTASFMPD.Enabled = false;

                rfvddlCollectVia.Enabled = true;
                rfvddlCreditCardType.Enabled = true;
                rfvtxtTASFMPD.Enabled = false;
            }
            else
            {
                ddlCreditCardType.Items.Clear();
                ddlCollectVia.Items.Clear();
                txtTASFMPD.Text = "";
                ddlCreditCardType.Enabled = false;
                ddlCollectVia.Enabled = false;
                txtTASFMPD.Enabled = false;

                rfvddlCollectVia.Enabled = false;
                rfvddlCreditCardType.Enabled = false;
                rfvtxtTASFMPD.Enabled = false;
            }
            SerPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlCollectVia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollectVia.SelectedValue == "2")
        {
            txtTASFMPD.Enabled = true;
            rfvtxtTASFMPD.Enabled = true;
        }
        else
        {
            txtTASFMPD.Enabled = false;
            rfvtxtTASFMPD.Enabled = false;
        }

        SerPopupExtender.Show();
    }
    protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            txtserDetails.Text = ddlServiceType.SelectedItem.Text;

            DataTable dt = (DataTable)ViewState["ServiceType_GetDataByTYpe"];
            string VatPer = (dt.AsEnumerable()
                .Where(p => p["ComId"].ToString() == Convert.ToInt32(ddlServiceType.SelectedItem.Value).ToString())
                .Where(p => p["ComDesc"].ToString() == txtserDetails.Text.ToString())
                .Select(p => p["VatRate"].ToString())).FirstOrDefault();


            if (VatPer != null)
            {
                txtSerVatPer.Text = VatPer;
            }
            else
            {
                txtSerVatPer.Text = "0.00";
                txtSerVatAmount.Text = "0.00";
                txtDetails.Text = "";
                txtExclusAmount.Text = txtSerClientTotal.Text;

            }

            if (txtSerClientTotal.Text != "")
            {

                getClientTotal();
            }
            SerPopupExtender.Show();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //protected void txtExclusAmount_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtExclusAmount.Text = _objBOUtiltiy.FormatTwoDecimal(txtExclusAmount.Text.ToString());

    //        getClientTotal();
    //        SerPopupExtender.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //    }
    //}

    protected void getClientTotal()
    {
        try
        {
            ExclusiveAmount = Convert.ToDecimal(txtExclusAmount.Text);

            // txtSerClientTotal.Text = txtExclusAmount.Text;

            if (txtSerVatPer.Text == "")
            {
                txtClientTotal.Text = ExclusiveAmount.ToString();
            }
            else
            {
                ExcusiveAmount();
                //decimal Vatper = Convert.ToDecimal(txtSerVatPer.Text);
                //decimal vatAmount = ((Vatper / 100) * ExclusiveAmount);
                ////decimal clientTotal = ExclusiveAmount + vatAmount;
                //decimal clientTotal = ExclusiveAmount;
                //txtSerVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(vatAmount.ToString());
                //txtSerClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(clientTotal.ToString());
                SerPopupExtender.Show();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    //land methods
    protected void DDType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["get_VatRateByType"] != null)
            {
                DataTable dt = (DataTable)ViewState["get_VatRateByType"];

                string vatRate = (dt.AsEnumerable()
                    .Where(p => p["TypeId"].ToString() == DDlandType.SelectedItem.Value.ToString())
                    .Select(p => p["VatRate"].ToString())).FirstOrDefault();
                vatRate = _objBOUtiltiy.FormatTwoDecimal(vatRate.ToString());

                txtLandExlVatPer.Text = vatRate;
                txtLandVatPer.Text = vatRate;
            }


            if (txtLandVatPer.Text != "0.00")
            {
                txtlandExclVatAmount.Text = ((Convert.ToDecimal(txtlandTotalExcl.Text) * Convert.ToDecimal(txtLandVatPer.Text)) / 100).ToString();
                txtlandTotalIncl.Text = (Convert.ToDecimal(txtlandExclVatAmount.Text) + Convert.ToDecimal(txtlandTotalExcl.Text)).ToString();

                if (txtlandCommPer.Text != "")
                {
                    txtlandTotalExcl_TextChanged(null, null);
                    txtlandCommPer_TextChanged(null, null);
                }


            }
            else
            {
                txtlandTotalIncl.Text = txtlandTotalExcl.Text;

            }
            txtlandTotalIncl.Text = _objBOUtiltiy.FormatTwoDecimal(txtlandTotalIncl.Text);
            txtlandDuefromclient.Text = txtlandTotalIncl.Text;
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        landPopExtender.Show();

    }

    // Payment Menthod Select Payment Bind Credit card
    protected void DDlandPayment_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _doUtilities.BindCreditCardType();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDlandCreditCard.DataSource = ds.Tables[0];
                DDlandCreditCard.DataTextField = "CardDescription";
                DDlandCreditCard.DataValueField = "CrdCardId";
                DDlandCreditCard.DataBind();
                DDlandCreditCard.Items.Insert(0, new ListItem("--Select Card--", "0"));

            }
            else
            {
                DDlandCreditCard.DataSource = null;
                DDlandCreditCard.DataBind();
                DDlandCreditCard.Items.Insert(0, new ListItem("--Select Card--", "0"));

            }

            var payment = Convert.ToDecimal(DDlandPayment.SelectedItem.Value);

            if (payment == 2)
            {
                DDlandCreditCard.Enabled = true;

            }
            else
            {
                DDlandCreditCard.Enabled = false;

            }
            landPopExtender.Show();
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void DDlandService_SelectedIndexChanged(object sender, EventArgs e)
    {
        int commId = Convert.ToInt32(DDlandService.SelectedItem.Value);
        DataSet ds = _objBOUtiltiy.GetCommissionPerc(commId);

        decimal commperc = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ComDComm"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0]["ComDComm"].ToString());

        txtlandCommPer.Text = _objBOUtiltiy.FormatTwoDecimal(commperc.ToString());

        landPopExtender.Show();
    }
    protected void LandArrSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objEMLandarrangement.LandArrId = 0;
            if (LandArrSubmit.Text == "Update")
            {
                objEMLandarrangement.LandArrId = Convert.ToInt32(hf_Land_TicketNo.Value);
            }


            objEMLandarrangement.LandSupplier = Convert.ToInt32(DDlandSupplier.SelectedIndex);
            objEMLandarrangement.Services = Convert.ToInt32(DDlandService.SelectedIndex);
            objEMLandarrangement.Type = Convert.ToInt32(DDlandType.SelectedIndex);
            objEMLandarrangement.PassengerName = txtlandPassName.Text.Trim();
            objEMLandarrangement.TravelFrDate = string.IsNullOrEmpty(txtlandTravelFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtlandTravelFrom.Text);
            objEMLandarrangement.TravelToDate = string.IsNullOrEmpty(txtlandTravelto.Text) ? (DateTime?)null : Convert.ToDateTime(txtlandTravelto.Text);


            objEMLandarrangement.BookRefNo = txtlandBookingRef.Text.Trim();
            objEMLandarrangement.Voucher = txtlandVocher.Text.Trim();
            objEMLandarrangement.SuppRef = txtlandSupplierRef.Text.Trim();
            objEMLandarrangement.SuppInvNo = txtlandSuppInvNo.Text.Trim();
            string Units = string.IsNullOrEmpty(txtlandUnits.Text.Trim()) ? "0" : txtlandUnits.Text.Trim();
            objEMLandarrangement.Units = Convert.ToInt32(Units);
            string CommabExclu = string.IsNullOrEmpty(txtlandUnits.Text.Trim()) ? "0" : txtlandUnits.Text.Trim();
            objEMLandarrangement.CommabExclu = Convert.ToDecimal(CommabExclu);
            string TotIncl = string.IsNullOrEmpty(txtlandTotalIncl.Text.Trim()) ? "0" : txtlandTotalIncl.Text.Trim();
            objEMLandarrangement.TotIncl = Convert.ToDecimal(TotIncl);
            string RateInclu = string.IsNullOrEmpty(txtlandRateIncl.Text.Trim()) ? "0" : txtlandRateIncl.Text.Trim();
            objEMLandarrangement.RateInclu = Convert.ToDecimal(RateInclu);
            string CommPer = string.IsNullOrEmpty(txtlandCommPer.Text.Trim()) ? "0" : txtlandCommPer.Text.Trim();
            objEMLandarrangement.CommPer = Convert.ToDecimal(CommPer);
            //   string CommPer = string.IsNullOrEmpty(txtlandCommPer.Text.Trim()) ? "0" : txtlandCommPer.Text.Trim();
            objEMLandarrangement.ExclVatPer = string.IsNullOrEmpty(txtLandExlVatPer.Text) ? 0 : Convert.ToDecimal(txtLandExlVatPer.Text);
            string ExclusiveVatAmount = string.IsNullOrEmpty(txtlandExclVatAmount.Text.Trim()) ? "0" : txtlandExclVatAmount.Text.Trim();
            objEMLandarrangement.ExclusiveVatAmount = Convert.ToDecimal(ExclusiveVatAmount);
            string CommissionableInclu = string.IsNullOrEmpty(txtlandCmblIncl.Text.Trim()) ? "0" : txtlandCmblIncl.Text.Trim();
            objEMLandarrangement.CommissionableInclu = Convert.ToDecimal(CommissionableInclu);
            string CommissionExclu = string.IsNullOrEmpty(txtlandCommExcl.Text.Trim()) ? "0" : txtlandCommExcl.Text.Trim();
            objEMLandarrangement.CommissionExclu = Convert.ToDecimal(CommissionExclu);
            string TotExclu = string.IsNullOrEmpty(txtlandTotalExcl.Text.Trim()) ? "0" : txtlandTotalExcl.Text.Trim();
            objEMLandarrangement.TotExclu = Convert.ToDecimal(TotExclu);
            string OthCommabInclu = string.IsNullOrEmpty(txtlandOtherCmblIncl.Text.Trim()) ? "0" : txtlandOtherCmblIncl.Text.Trim();
            objEMLandarrangement.OthCommabInclu = Convert.ToDecimal(OthCommabInclu);
            objEMLandarrangement.VatPer = string.IsNullOrEmpty(txtLandVatPer.Text.Trim()) ? 0 : Convert.ToDecimal(txtLandVatPer.Text);
            objEMLandarrangement.Payment = Convert.ToInt32(DDlandPayment.SelectedIndex);
            string TotCommabInclu = string.IsNullOrEmpty(txtlandTotalcmblIncl.Text.Trim()) ? "0" : txtlandTotalcmblIncl.Text.Trim();
            objEMLandarrangement.TotCommabInclu = Convert.ToDecimal(TotCommabInclu);
            string CommVatAmount = string.IsNullOrEmpty(txtlandVatAmount.Text.Trim()) ? "0" : txtlandVatAmount.Text.Trim();
            objEMLandarrangement.CommVatAmount = Convert.ToDecimal(CommVatAmount);
            objEMLandarrangement.CreditCard = Convert.ToInt32(DDlandCreditCard.SelectedIndex);
            string NonCommabInclu = string.IsNullOrEmpty(txtlandNoncmbl.Text.Trim()) ? "0" : txtlandNoncmbl.Text.Trim();
            objEMLandarrangement.NonCommabInclu = Convert.ToDecimal(NonCommabInclu);
            string CommissionInclu = string.IsNullOrEmpty(txtlandCommIncl.Text.Trim()) ? "0" : txtlandCommIncl.Text.Trim();
            objEMLandarrangement.CommissionInclu = Convert.ToDecimal(CommissionInclu);
            string DueToClient = string.IsNullOrEmpty(txtlandDuefromclient.Text.Trim()) ? "0" : txtlandDuefromclient.Text.Trim();
            objEMLandarrangement.DueToClient = Convert.ToDecimal(DueToClient);

            string LessCommission = string.IsNullOrEmpty(txtlandLessComm.Text.Trim()) ? "0" : txtlandLessComm.Text.Trim();
            objEMLandarrangement.LessCommission = Convert.ToDecimal(LessCommission);

            string DueTOSupplier = string.IsNullOrEmpty(txtlandDuetoSupplier.Text.Trim()) ? "0" : txtlandDuetoSupplier.Text.Trim();
            objEMLandarrangement.DueTOSupplier = Convert.ToDecimal(DueTOSupplier);
            objEMLandarrangement.SupplOpenAmount = Convert.ToDecimal(DueTOSupplier);
            objEMLandarrangement.InvoiceType = "Land";
            objEMLandarrangement.invDocumentNo = "0";
            string CO2Emis = string.IsNullOrEmpty(txtlandCO2.Text.Trim()) ? "0" : txtlandCO2.Text.Trim();
            objEMLandarrangement.CO2Emis = Convert.ToDecimal(CO2Emis);


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
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "LandTicket created Successfully");

                BindInvoiceLineItems();
                BindInvoiceLineItemsCount();
                LandArrangemntsClear();
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "LandTicket Details was not created plase try again");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        landPopExtender.Hide();
    }



    protected void Cancel_Click(object sender, EventArgs e)
    {

    }
    protected void Reset_Click(object sender, EventArgs e)
    {

    }




    // Bind Land Supplier Name
    public void BindLandSuppliers()
    {
        try
        {
            BALandSuppliers objBalandSuppliers = new BALandSuppliers();
            int landSupId = 0;
            DataSet datasetland = new DataSet();
            datasetland = objBalandSuppliers.GetLandSupplier(landSupId);
            if (datasetland.Tables[2].Rows.Count > 0)
            {
                DDlandSupplier.DataSource = datasetland.Tables[2];
                DDlandSupplier.DataTextField = "LSupplierName";
                DDlandSupplier.DataValueField = "LSupplierId";
                DDlandSupplier.DataBind();
                DDlandSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
            else
            {
                DDlandSupplier.DataSource = null;
                DDlandSupplier.DataBind();
                DDlandSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    public void BindType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objDALandarrangement.Get_Type();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDlandType.DataSource = ds.Tables[0];
                DDlandType.DataTextField = "TypeName";
                DDlandType.DataValueField = "TypeName";
                DDlandType.DataBind();
                DDlandType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }
            else
            {
                DDlandType.DataSource = null;
                DDlandType.DataBind();
                DDlandType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }

    public void BindLandService()
    {
        try
        {
            string Type = "Land";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDlandService.DataSource = ds.Tables[0];
                DDlandService.DataTextField = "ComDesc";
                DDlandService.DataValueField = "ComId";
                DDlandService.DataBind();
                DDlandService.Items.Insert(0, new ListItem("--Select Service--", "0"));
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    public void BindLandPaymentType()
    {
        try
        {
            DataSet ds = new DataSet();
            int paymentid = 0;
            ds = _doUtilities.BindPaymentType(paymentid);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDlandPayment.DataSource = ds.Tables[0];
                DDlandPayment.DataTextField = "PaymentName";
                DDlandPayment.DataValueField = "PaymentId";
                DDlandPayment.DataBind();
                DDlandPayment.Items.Insert(0, new ListItem("--Select Payment--", "0"));
            }
            else
            {
                DDlandPayment.DataSource = null;
                DDlandPayment.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    protected void txtlandTotalIncl_TextChanged(object sender, EventArgs e)
    {


    }
    protected void txtlandTotalExcl_TextChanged(object sender, EventArgs e)
    {

        try
        {
            txtlandTotalExcl.Text = _objBOUtiltiy.FormatTwoDecimal(txtlandTotalExcl.Text);
            txtlandTotalIncl.Text = _objBOUtiltiy.ExcluAssignInclu(txtlandTotalExcl.Text);
            txtlandExclVatAmount.Text = _objBOUtiltiy.ExcluvatCau(txtlandTotalExcl.Text, txtLandExlVatPer.Text);
            txtlandcmblExcl.Text = txtlandTotalExcl.Text;
            if (txtlandTotalExcl.Text != "" && txtlandExclVatAmount.Text != "")
            {
                txtlandTotalIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtlandTotalExcl.Text) + Convert.ToDecimal(txtlandExclVatAmount.Text)).ToString());

            }
            if (txtlandCommPer.Text != "")
            {
                txtlandCommPer_TextChanged(null, null);
            }
            landPopExtender.Show();

            txtlandDuefromclient.Text = txtlandTotalIncl.Text;

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtlandRateIncl_TextChanged(object sender, EventArgs e)
    {
        txtlandRateIncl.Text = _objBOUtiltiy.FormatTwoDecimal(txtlandRateIncl.Text);
        if (txtlandUnits.Text != "" && txtlandRateIncl.Text != "")
        {
            txtlandCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtlandUnits.Text) * Convert.ToDecimal(txtlandRateIncl.Text)).ToString());

            txtlandTotalcmblIncl.Text = txtlandCmblIncl.Text;
        }
        landPopExtender.Show();
    }
    protected void txtlandUnits_TextChanged(object sender, EventArgs e)
    {
        if (txtlandUnits.Text != "" && txtlandRateIncl.Text != "")
        {
            txtlandCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtlandUnits.Text) * Convert.ToDecimal(txtlandRateIncl.Text)).ToString());

            txtlandTotalcmblIncl.Text = txtlandCmblIncl.Text;
        }
        landPopExtender.Show();
    }
    protected void txtlandOtherCmblIncl_TextChanged(object sender, EventArgs e)
    {

        if (txtlandCmblIncl.Text != "" && txtlandOtherCmblIncl.Text != "")
        {
            txtlandTotalcmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(txtlandCmblIncl.Text) + Convert.ToDecimal(txtlandOtherCmblIncl.Text)).ToString());

        }
        landPopExtender.Show();
    }
    protected void txtlandcmblExcl_TextChanged(object sender, EventArgs e)
    {
        landPopExtender.Show();
    }
    protected void txtlandCommPer_TextChanged(object sender, EventArgs e)
    {
        if (txtlandcmblExcl.Text != "" && txtlandCommPer.Text != "")
        {
            txtlandCommExcl.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.ExcluvatCau(txtlandcmblExcl.Text, txtlandCommPer.Text));
        }
        if (txtlandCommExcl.Text != "")
        {
            txtlandVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.ExcluvatCau(txtlandCommExcl.Text, txtLandVatPer.Text));

        }
        else
        {

        }
        if (txtlandCommExcl.Text != "" && txtlandVatAmount.Text != "")
        {
            txtlandCommIncl.Text = _objBOUtiltiy.FormatTwoDecimal(_objBOUtiltiy.InclusiveAmount(txtlandCommExcl.Text, txtlandVatAmount.Text).ToString());
        }
        txtlandLessComm.Text = txtlandCommIncl.Text;
        if (txtlandDuefromclient.Text != "")
        {
            txtlandDuetoSupplier.Text = (Convert.ToDecimal(txtlandDuefromclient.Text) - Convert.ToDecimal(txtlandLessComm.Text)).ToString();
        }
        else
        {
            txtlandDuetoSupplier.Text = txtlandLessComm.Text;
        }
        landPopExtender.Show();
    }

    protected void InvListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    //protected void InvListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    InvListGrid.PageIndex = e.NewPageIndex;

    //}
    //invoice Line items
    private void BindInvoiceLineItems()
    {
        try
        {

            string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();

            DataSet ds = _objBalInvoice.getInvoiceLines(tempUniqueCode);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                InvListGrid.DataSource = ds;
                InvListGrid.DataBind();
            }
            else
            {
                InvListGrid.DataSource = ds;
                InvListGrid.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindInvoiceLineItemsCount()
    {
        try
        {
            decimal total = 0;
            decimal commInclu = 0;
            decimal commExcl = 0;
            decimal commVatamt = 0;
            decimal aircommiinclu = 0;
            decimal landcommiInclu = 0;
            decimal servcommi = 0;

            DataTable dt = new DataTable();
            string tempUniqueCode = (string)Session["TempUniqCode"] == "" ? "0" : Session["TempUniqCode"].ToString();
            DataSet ds = _objBalInvoice.getInvoiceLinesCount(tempUniqueCode);

            dt = ds.Tables[0];

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                InvoiceLineItemCountGrid.DataSource = ds;
                InvoiceLineItemCountGrid.DataBind();

                foreach (DataRow dtlRow in ds.Tables[0].Rows)
                {

                    if (dtlRow["Type"].ToString() == "Air")
                    {
                        aircommiinclu = Convert.ToDecimal(dtlRow["comminclu"].ToString()) + aircommiinclu;
                        lblaircommiinclu.Text = aircommiinclu.ToString();
                        lblaircommType.Text = dtlRow["Type"].ToString();
                    }

                    if (dtlRow["Type"].ToString() == "Land")
                    {
                        landcommiInclu = Convert.ToDecimal(dtlRow["comminclu"].ToString()) + landcommiInclu;
                        lbllandcommiInclu.Text = landcommiInclu.ToString();
                        lbllandcommType.Text = dtlRow["Type"].ToString();
                    }

                    if (dtlRow["Type"].ToString() == "SF")
                    {
                        servcommi = Convert.ToDecimal(dtlRow["comminclu"].ToString()) + servcommi;
                        lblservcommi.Text = servcommi.ToString();
                        lblsercommType.Text = dtlRow["Type"].ToString();
                    }
                }
                //Invoice Total Sum Ex: Air,Land,Service ....
                total = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                txtInvoiceTotalAmount.Text = total.ToString();

                commInclu = dt.AsEnumerable().Sum(row => row.Field<decimal>("comminclu"));
                lblcommissionInclu.Text = commInclu.ToString();

                commExcl = dt.AsEnumerable().Sum(row => row.Field<decimal>("commexclu"));
                lblcommissionExclu.Text = commExcl.ToString();

                commVatamt = dt.AsEnumerable().Sum(row => row.Field<decimal>("vatamount"));
                lblcommissionVatamt.Text = commVatamt.ToString();


            }
            else
            {
                InvoiceLineItemCountGrid.DataSource = ds;
                InvoiceLineItemCountGrid.DataBind();
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
        foreach (var item in Page.Controls)
        {
            if (item is TextBox)
            {
                ((TextBox)item).Text = "";
            }
        }
    }


    //invoice Bind Methods
    private void BindInvoiceDropDown()
    {
        try
        {
            DataSet ds = new DataSet();
            int clientTypeId = 0;
            ds = _objBOUtiltiy.InvoiceDdlBinding(clientTypeId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlInvCosultant.DataSource = ds.Tables[0];
                ddlInvCosultant.DataTextField = "Name";
                ddlInvCosultant.DataValueField = "ConsultantId";
                ddlInvCosultant.DataBind();
                ddlInvCosultant.Items.Insert(0, new ListItem("--Select Consultant--", "0"));

                ddlInvMesg.DataSource = ds.Tables[1];
                ddlInvMesg.DataTextField = "NpName";
                ddlInvMesg.DataValueField = "NotePadId";
                ddlInvMesg.DataBind();
                ddlInvMesg.Items.Insert(0, new ListItem("--Select Consultant--", "0"));

                drpInvClientType.DataSource = ds.Tables[2];
                drpInvClientType.DataTextField = "Name";
                drpInvClientType.DataValueField = "Id";
                drpInvClientType.DataBind();
                drpInvClientType.Items.Insert(0, new ListItem("--Select Client Type--", "0"));

                drpInvBookingSrc.DataSource = ds.Tables[3];
                drpInvBookingSrc.DataTextField = "BookingName";
                drpInvBookingSrc.DataValueField = "BookingId";
                drpInvBookingSrc.DataBind();
                drpInvBookingSrc.Items.Insert(0, new ListItem("--Select Booking Source--", "0"));


                drpInvBookDest.DataSource = ds.Tables[4];
                drpInvBookDest.DataTextField = "BookDestName";
                drpInvBookDest.DataValueField = "BookDestId";
                drpInvBookDest.DataBind();
                drpInvBookDest.Items.Insert(0, new ListItem("--Select Booking Destination--", "0"));

            }
            else
            {

                ddlInvCosultant.DataSource = null;
                ddlInvCosultant.DataBind();
                ddlInvCosultant.Items.Insert(0, new ListItem("--Select Consultant--", "0"));

                ddlInvMesg.DataSource = null;
                ddlInvMesg.DataBind();


                drpInvClientType.DataSource = null;
                drpInvClientType.DataBind();
                drpInvClientType.Items.Insert(0, new ListItem("--Select Client Type--", "0"));

                drpInvBookingSrc.DataSource = null;
                drpInvBookingSrc.DataBind();
                drpInvBookingSrc.Items.Insert(0, new ListItem("--Select Booking Source--", "0"));

                drpInvBookDest.DataSource = null;
                drpInvBookDest.DataBind();
                drpInvBookDest.Items.Insert(0, new ListItem("--Select Booking Destination--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void drpInvClientType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            drpInvClientName.Items.Clear();

            int cllienttypeId = Convert.ToInt32(drpInvClientType.SelectedValue.ToString());

            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.InvoiceDdlBinding(cllienttypeId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                drpInvClientName.DataSource = ds.Tables[0];
                drpInvClientName.DataTextField = "ClientName";
                drpInvClientName.DataValueField = "ClientId";
                drpInvClientName.DataBind();
                drpInvClientName.Items.Insert(0, new ListItem("--Select Client Name--", "0"));
            }
            else
            {
                drpInvClientName.Items.Insert(0, new ListItem("--Select Client--", "0"));
                drpInvClientName.DataSource = null;
                drpInvClientName.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void drpInvClientName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlInvMesg.Items.Clear();

            int clientId = Convert.ToInt32(drpInvClientName.SelectedItem.Value.ToString());

            DataSet ds = new DataSet();
            ds = _objBalInvoice.Get_ClientmessageandNote(clientId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlInvMesg.DataSource = ds.Tables[0];
                ddlInvMesg.DataTextField = "NpName";
                ddlInvMesg.DataValueField = "NotePadId";
                ddlInvMesg.DataBind();
                ddlInvMesg.Items.Insert(0, new ListItem("--Select Message--", "0"));
                txtInvClntMesg.Text = ds.Tables[0].Rows[0]["ClientMessage"].ToString();


            }
            else
            {
                ddlInvMesg.Items.Insert(0, new ListItem("--Select Message--", "0"));
                ddlInvMesg.DataSource = null;
                ddlInvMesg.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    //protected void txtAirportTax_TextChanged(object sender, EventArgs e)
    //{
    //    txtAirClientTot.Text  = Convert.ToDecimal( txtAirportTax.Text)  + Convert.ToDecimal(txtAirExcluisvefare.Text) +txtAirVatOnFare.Text
    //}

    private void BindInvoiceACAnalysis()
    {

        try
        {
            int TempuniqID = Convert.ToInt32(Session["TempUniqCode"]);
            DataSet ds = _objBalInvoice.GetInvoiceACAnalasis(TempuniqID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ACAnalysisGrid.DataSource = ds;
                ACAnalysisGrid.DataBind();
            }

            else
            {
                ACAnalysisGrid.DataSource = null;
                ACAnalysisGrid.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    // AC Analysis
    protected void btnSubmitACAnalsys_Click(object sender, EventArgs e)
    {
        BindInvoiceACAnalysis();


    }
    protected void btnInvCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceList.aspx");
    }
    protected void btnDraftPdf_Click(object sender, EventArgs e)
    {
        string Tempuniqcode = Session["TempUniqCode"].ToString();

        //Response.Redirect("DraftPdf.aspx?TempuniqCode=" + Tempuniqcode);

        string url = "DraftPdf.aspx?TempuniqCode=" + Tempuniqcode;
        string fullURL = "window.open('" + url + "', '_blank');";
        ScriptManager.RegisterStartupScript(this, typeof(string), "_blank", fullURL, true);
    }



    private void LandArrangemntsClear()
    {
        DDlandSupplier.SelectedIndex = -1;
        DDlandService.SelectedIndex = -1;
        DDlandType.SelectedIndex = -1;
        txtlandPassName.Text = "";
        txtlandTravelFrom.Text = "";
        txtlandTravelto.Text = "";
        txtlandBookingRef.Text = "";
        txtlandVocher.Text = "";
        txtlandSupplierRef.Text = "";
        txtlandSuppInvNo.Text = "";
        txtlandUnits.Text = "";
        txtlandcmblExcl.Text = "";
        txtlandTotalExcl.Text = "";
        txtlandRateIncl.Text = "";
        txtlandCommPer.Text = "";
        txtLandExlVatPer.Text = "";
        txtlandExclVatAmount.Text = "";
        txtlandCmblIncl.Text = "";
        txtlandCommExcl.Text = "";
        txtlandTotalIncl.Text = "";
        txtlandOtherCmblIncl.Text = "";
        txtLandVatPer.Text = "";
        DDlandPayment.SelectedIndex = -1;
        txtlandTotalcmblIncl.Text = "";
        txtlandVatAmount.Text = "";
        DDlandCreditCard.SelectedIndex = -1;
        txtlandNoncmbl.Text = "";
        txtlandCommIncl.Text = "";
        txtlandDuefromclient.Text = "";
        txtlandLessComm.Text = "";
        txtlandDuetoSupplier.Text = "";
        txtlandCO2.Text = "";

    }
    private void ServiceFeeClear()
    {
        ddlServiceType.SelectedIndex = -1;
        ddlSoureceref.SelectedIndex = -1;
        txtSerTravelDate.Text = "";
        ddlPassengerName.SelectedIndex = -1;
        txtExclusAmount.Text = "";
        txtserDetails.Text = "";
        txtSerVatPer.Text = "";
        txtSerVatAmount.Text = "";
        ddlPaymentMethod.SelectedIndex = -1;
        txtSerClientTotal.Text = "";
        ddlCreditCardType.SelectedIndex = -1;
        ddlCollectVia.SelectedIndex = -1;
        txtTASFMPD.Text = "";

    }
    private void GeneralChargeClear()
    {
        ddlGenchrgType.SelectedIndex = -1;
        ddlPassengerNames.SelectedIndex = -1;
        txtDetails.Text = "";
        ddlCrdCardType.SelectedIndex = -1;
        txtUnits.Text = "";
        txtRateNet.Text = "";
        txtgenvat.Text = "";
        txtVatAmount.Text = "";
        txtExcluAmount.Text = "";
        txtClientTotal.Text = "";


    }

    protected void ddlSoureceref_TextChanged(object sender, EventArgs e)
    {
        ddlPassengerName.Items.Clear();
        BindSerPassengerNames();
        SerPopupExtender.Show();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton btnDetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)btnDetails.NamingContainer;
        int TicketNo = Convert.ToInt32(InvListGrid.DataKeys[gvrow.RowIndex].Values[1]);
        string TicketType = InvListGrid.DataKeys[gvrow.RowIndex].Values[0].ToString();


        if (TicketType == "Air")
        {
            //  btnAirSubmit.Text = "Update";
            AirPopupExtender.Show();
            GetTicketDetails(TicketNo, TicketType);


        }
        else if (TicketType == "Land")
        {
            LandArrSubmit.Text = "Update";
            landPopExtender.Show();
            GetTicketDetails(TicketNo, TicketType);
        }
        else if (TicketType == "SF")
        {
            SerSubmit.Text = "Update";
            SerPopupExtender.Show();
            GetTicketDetails(TicketNo, TicketType);
        }
        else
        {
            GenSubmit.Text = "Update";
            GenPopupExtender.Show();
            GetTicketDetails(TicketNo, TicketType);
        }

    }
    private void GetTicketDetails(int TicketNo, string TicketType)
    {
        try
        {
            DataSet ds = _objBalInvoice.GetTicket(TicketNo, TicketType);

            if (TicketType == "Air")
            {

                hf_Air_TicketNo.Value = ds.Tables[5].Rows[0]["TicketId"].ToString();
                txtAirTicketNo.Text = ds.Tables[5].Rows[0]["AirTicketNo"].ToString();
                txtTicketType.Text = ds.Tables[5].Rows[0]["TDesc1"].ToString();
                txtPnr.Text = ds.Tables[5].Rows[0]["AirPnr"].ToString();
                txtAirLine.Text = ds.Tables[5].Rows[0]["SupplierName1"].ToString();
                drpAirPassenger.Text = ds.Tables[5].Rows[0]["AirPassenger"].ToString();
                txtAirConjunction.Text = ds.Tables[5].Rows[0]["Conjunction"].ToString();
                txtAirService.Text = ds.Tables[5].Rows[0]["ComDesc1"].ToString();
                txtAirType.Text = ds.Tables[5].Rows[0]["TypeName1"].ToString();
                txtAirMiles.Text = ds.Tables[5].Rows[0]["AirMiles"].ToString();
                txtAirRouting.Text = ds.Tables[5].Rows[0]["AirRouting"].ToString();

                // Routing  
                if (ds.Tables[4].Rows.Count > 3)
                {
                    lblRoutes1.Text = ds.Tables[4].Rows[0]["Routs"].ToString();
                    txtFlightNo1.Text = ds.Tables[4].Rows[0]["FlightNo"].ToString();
                    txtClass1.Text = ds.Tables[4].Rows[0]["Class"].ToString();
                    txtDate1.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[0]["Date"].ToString());

                    lblRoutes2.Text = ds.Tables[4].Rows[1]["Routs"].ToString();
                    txtFlightNo2.Text = ds.Tables[4].Rows[1]["FlightNo"].ToString();
                    txtClass2.Text = ds.Tables[4].Rows[1]["Class"].ToString();
                    txtDate2.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[1]["Date"].ToString());

                    lblRoutes3.Text = ds.Tables[4].Rows[2]["Routs"].ToString();
                    txtFlightNo3.Text = ds.Tables[4].Rows[2]["FlightNo"].ToString();
                    txtClass3.Text = ds.Tables[4].Rows[2]["Class"].ToString();
                    txtDate3.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[2]["Date"].ToString());

                    lblRoutes4.Text = ds.Tables[4].Rows[3]["Routs"].ToString();
                    txtFlightNo4.Text = ds.Tables[4].Rows[3]["FlightNo"].ToString();
                    txtClass4.Text = ds.Tables[4].Rows[3]["Class"].ToString();
                    txtDate4.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[3]["Date"].ToString());
                }
                if (ds.Tables[4].Rows.Count > 2)
                {
                    lblRoutes1.Text = ds.Tables[4].Rows[0]["Routs"].ToString();
                    txtFlightNo1.Text = ds.Tables[4].Rows[0]["FlightNo"].ToString();
                    txtClass1.Text = ds.Tables[4].Rows[0]["Class"].ToString();
                    txtDate1.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[0]["Date"].ToString());

                    lblRoutes2.Text = ds.Tables[4].Rows[1]["Routs"].ToString();
                    txtFlightNo2.Text = ds.Tables[4].Rows[1]["FlightNo"].ToString();
                    txtClass2.Text = ds.Tables[4].Rows[1]["Class"].ToString();
                    txtDate2.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[1]["Date"].ToString());

                    lblRoutes3.Text = ds.Tables[4].Rows[2]["Routs"].ToString();
                    txtFlightNo3.Text = ds.Tables[4].Rows[2]["FlightNo"].ToString();
                    txtClass3.Text = ds.Tables[4].Rows[2]["Class"].ToString();
                    txtDate3.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[2]["Date"].ToString());


                }
                if (ds.Tables[4].Rows.Count > 1)
                {
                    lblRoutes1.Text = ds.Tables[4].Rows[0]["Routs"].ToString();
                    txtFlightNo1.Text = ds.Tables[4].Rows[0]["FlightNo"].ToString();
                    txtClass1.Text = ds.Tables[4].Rows[0]["Class"].ToString();
                    txtDate1.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[0]["Date"].ToString());

                    lblRoutes2.Text = ds.Tables[4].Rows[1]["Routs"].ToString();
                    txtFlightNo2.Text = ds.Tables[4].Rows[1]["FlightNo"].ToString();
                    txtClass2.Text = ds.Tables[4].Rows[1]["Class"].ToString();
                    txtDate2.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[1]["Date"].ToString());



                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    lblRoutes1.Text = ds.Tables[4].Rows[0]["Routs"].ToString();
                    txtFlightNo1.Text = ds.Tables[4].Rows[0]["FlightNo"].ToString();
                    txtClass1.Text = ds.Tables[4].Rows[0]["Class"].ToString();
                    txtDate1.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[4].Rows[0]["Date"].ToString());

                }

                //Routing 

                txtAirTravelDate.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[5].Rows[0]["AirTravelDate"].ToString());
                txtAirReturnDate.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[5].Rows[0]["AirReturnDate"].ToString());
                txtAirExcluisvefare.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirExclusiveFare"].ToString());
                txtAirCommisionper.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirCommPer"].ToString());
                txtVatPer.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirVatPer"].ToString());
                txtAirVatOnFare.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirVatonFare"].ToString());
                txtAirCommExclu.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirCommExcl"].ToString());
                txtAirportTax.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirportTaxes"].ToString());
                txtAirCommVat.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirVatPer"].ToString());
                txtAirVatinclTax.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirVatInTaxes"].ToString());
                txtAirAgentVat.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirAgentVat"].ToString());
                txtAirClientTot.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirClientTotal"].ToString());
                txtAirCommInclu.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirCommInclu"].ToString());
                txtAirPayment.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[5].Rows[0]["AirPayment"].ToString());

                txtAirDueToBsp.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[0].Rows[0]["AirDueToBsp"].ToString());
            }
            else if (TicketType == "Land")
            {
                hf_Land_TicketNo.Value = ds.Tables[1].Rows[0]["LandArrId"].ToString();
                DDlandSupplier.SelectedIndex = DDlandSupplier.Items.IndexOf(DDlandSupplier.Items.FindByValue(ds.Tables[1].Rows[0]["LandSupplier"].ToString()));
                DDlandService.SelectedIndex = DDlandService.Items.IndexOf(DDlandService.Items.FindByValue(ds.Tables[1].Rows[0]["Services"].ToString()));
                DDlandType.SelectedIndex = DDlandType.Items.IndexOf(DDlandType.Items.FindByValue(ds.Tables[1].Rows[0]["Type"].ToString()));
                txtlandPassName.Text = ds.Tables[1].Rows[0]["PassengerName"].ToString();
                txtlandTravelFrom.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[1].Rows[0]["TravelFrDate"].ToString());
                txtlandTravelto.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[1].Rows[0]["TravelToDate"].ToString());
                txtlandBookingRef.Text = ds.Tables[1].Rows[0]["BookRefNo"].ToString();
                txtlandVocher.Text = ds.Tables[1].Rows[0]["Voucher"].ToString();
                txtlandSupplierRef.Text = ds.Tables[1].Rows[0]["SuppRef"].ToString();
                txtlandSuppInvNo.Text = ds.Tables[1].Rows[0]["SuppInvNo"].ToString();
                txtlandUnits.Text = ds.Tables[1].Rows[0]["Units"].ToString();
                txtlandcmblExcl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["TotExclu"].ToString());
                txtlandTotalExcl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["TotExclu"].ToString());
                txtlandRateIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["RateInclu"].ToString());
                txtlandCommPer.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CommPer"].ToString());
                txtLandExlVatPer.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["VatPer"].ToString());
                txtlandExclVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["ExclusiveVatAmount"].ToString());
                txtlandCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CommissionableInclu"].ToString());
                txtlandCommExcl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CommissionExclu"].ToString());
                txtlandTotalIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["TotIncl"].ToString());
                txtlandOtherCmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["OthCommabInclu"].ToString());
                txtLandVatPer.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["VatPer"].ToString());
                DDlandPayment.SelectedIndex = DDlandPayment.Items.IndexOf(DDlandPayment.Items.FindByValue(ds.Tables[1].Rows[0]["Payment"].ToString()));
                txtlandTotalcmblIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["TotCommabInclu"].ToString());
                txtlandVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CommVatAmount"].ToString());
                DDlandCreditCard.SelectedIndex = DDlandCreditCard.Items.IndexOf(DDlandCreditCard.Items.FindByValue(ds.Tables[1].Rows[0]["CreditCard"].ToString()));
                txtlandNoncmbl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["NonCommabInclu"].ToString());
                txtlandCommIncl.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CommissionInclu"].ToString());
                txtlandDuefromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["DueTOClient"].ToString());
                txtlandLessComm.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["LessCommission"].ToString());
                txtlandDuetoSupplier.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["DueTOSupplier"].ToString());
                txtlandCO2.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[1].Rows[0]["CO2Emis"].ToString());
            }
            else if (TicketType == "SF")
            {
                hf_SF_TicketNo.Value = ds.Tables[2].Rows[0]["ServiceFeeId"].ToString();
                if (ds.Tables[2].Rows[0]["MergeC"].ToString() == "0")
                {
                    chkMerge.Checked = false;
                }
                else
                {
                    chkMerge.Checked = true;
                }
                ddlServiceType.SelectedIndex = ddlServiceType.Items.IndexOf(ddlServiceType.Items.FindByValue(ds.Tables[2].Rows[0]["Type"].ToString()));
                ddlSoureceref.SelectedIndex = ddlSoureceref.Items.IndexOf(ddlSoureceref.Items.FindByValue(ds.Tables[2].Rows[0]["SourceRef"].ToString()));
                txtSerTravelDate.Text = _objBOUtiltiy.ConvertDateFormat(ds.Tables[2].Rows[0]["TravelDate"].ToString());
                BindSerPassengerNames();
                ddlPassengerName.SelectedIndex = ddlPassengerName.Items.IndexOf(ddlPassengerName.Items.FindByValue(ds.Tables[2].Rows[0]["PassengerName"].ToString()));
                txtExclusAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[2].Rows[0]["ExcluAmount"].ToString());
                txtserDetails.Text = ds.Tables[2].Rows[0]["Details"].ToString();
                txtSerVatPer.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[2].Rows[0]["VatPer"].ToString());
                txtSerVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[2].Rows[0]["VatAmount"].ToString());
                ddlPaymentMethod.SelectedIndex = ddlPaymentMethod.Items.IndexOf(ddlPaymentMethod.Items.FindByValue(ds.Tables[2].Rows[0]["PaymentMethod"].ToString()));
                BindCollectVia();
                BindSerCreditCardType();
                txtSerClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[2].Rows[0]["ClientTot"].ToString());
                ddlCreditCardType.SelectedIndex = ddlCreditCardType.Items.IndexOf(ddlCreditCardType.Items.FindByValue(ds.Tables[2].Rows[0]["CreditCardType"].ToString()));
                ddlCollectVia.SelectedIndex = ddlCollectVia.Items.IndexOf(ddlCollectVia.Items.FindByValue(ds.Tables[2].Rows[0]["CollectVia"].ToString()));
                txtTASFMPD.Text = ds.Tables[2].Rows[0]["TasfMpd"].ToString();
            }
            else
            {
                hf_GC_TicketNo.Value = ds.Tables[3].Rows[0]["GenChgId"].ToString();
                ddlGenchrgType.SelectedIndex = ddlGenchrgType.Items.IndexOf(ddlGenchrgType.Items.FindByValue(ds.Tables[3].Rows[0]["Type"].ToString()));
                ddlPassengerNames.SelectedIndex = ddlPassengerNames.Items.IndexOf(ddlPassengerNames.Items.FindByValue(ds.Tables[3].Rows[0]["PassengerName"].ToString()));
                txtDetails.Text = ds.Tables[3].Rows[0]["Details"].ToString();
                ddlCrdCardType.SelectedIndex = ddlCrdCardType.Items.IndexOf(ddlCrdCardType.Items.FindByValue(ds.Tables[3].Rows[0]["CreditCard"].ToString()));
                txtUnits.Text = ds.Tables[3].Rows[0]["Units"].ToString();
                txtRateNet.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[3].Rows[0]["RateNet"].ToString());
                txtgenvat.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[3].Rows[0]["VatPer"].ToString());
                txtVatAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[3].Rows[0]["VatAmount"].ToString());
                txtExcluAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[3].Rows[0]["Excluamt"].ToString());
                txtClientTotal.Text = _objBOUtiltiy.FormatTwoDecimal(ds.Tables[3].Rows[0]["ClientTot"].ToString());
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnIccCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnIccCreate_Click(object sender, EventArgs e)
    {

    }

    protected void txtSerClientTotal_TextChanged(object sender, EventArgs e)
    {
        ExcusiveAmount();
    }


    private void ExcusiveAmount()
    {
        decimal ClientTotal = Convert.ToDecimal(txtSerClientTotal.Text);

        string VatPer = txtSerVatPer.Text;

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

        txtExclusAmount.Text = _objBOUtiltiy.FormatTwoDecimal(exclAmount.ToString());
        txtSerVatAmount.Text = vatamount.ToString();
        SerPopupExtender.Show();
    }


}