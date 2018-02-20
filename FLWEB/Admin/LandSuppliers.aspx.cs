using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using EntityManager;
using BusinessManager;

public partial class Admin_LandSuppliers : System.Web.UI.Page
{
    EMLandSuppliers objEmLandSupp = new EMLandSuppliers();
    BALandSuppliers objLandSuppliers = new BALandSuppliers();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStatus();
            BindServiceType();
            BindDivisions();
            BindConsultants();
            BindBankNames();
            BindCommsType();
            BindPaymentMethods();
            BindAccountTypes();
            BindCountry();
            BindSupplierInvoice();
            BindItemLoadType();
            BindNotepadType();
            BindMainAccounts();
            BindGroupMaster();

            if (!string.IsNullOrEmpty(Request.QueryString["LSupplierId"]))
            {
                string LsupplierId = Request.QueryString["LSupplierId"].ToString();


                GetLandSuppliers(Convert.ToInt32(LsupplierId));
                cmdSubmit.Text = "Update";

            }
        }

    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateLandSupplier();
    }


    private void InsertUpdateLandSupplier()
    {
        try
        {
            objEmLandSupp.LSupplierId = Convert.ToInt32(hf_LSupplierId.Value);
            objEmLandSupp.LSupplierName = txtSupplierName.Text;
            objEmLandSupp.LSupAccountCode = txtAccountCode.Text;
            objEmLandSupp.LIsActive = Convert.ToInt32(dropStatus.SelectedValue);
            objEmLandSupp.LServiceType = Convert.ToInt32(dropServiceType.SelectedValue);
            objEmLandSupp.LCountry = Convert.ToInt32(dropCountry.SelectedValue);
            objEmLandSupp.LGroupId = Convert.ToInt32(dropGroup.SelectedValue);
            objEmLandSupp.LStateId = Convert.ToInt32(dropState.SelectedValue);
            objEmLandSupp.LDivision = Convert.ToInt32(dropDivision.SelectedValue);
            objEmLandSupp.LCity = Convert.ToInt32(dropCity.SelectedValue);
            objEmLandSupp.LConsultant = Convert.ToInt32(dropConsultant.SelectedValue);
            objEmLandSupp.LLatitute = txtLatitude.Text;
            objEmLandSupp.LLongitude = txtLongitude.Text;

            objEmLandSupp.LTelephone = txtTelephoneNo.Text;
            objEmLandSupp.LFax = txtFax.Text;
            objEmLandSupp.LCellNo = txtCellNo.Text;
            objEmLandSupp.LContactNo = txtContact.Text;
            objEmLandSupp.LEmail = txtEmail.Text;
            objEmLandSupp.LWeb = txtWeb.Text;
            objEmLandSupp.LPhysicalAddress = txtPhysicalAddress.Text;
            objEmLandSupp.LPostalAddress = txtPostalAddress.Text;

            if (chkVATNO.Checked)
            {
                objEmLandSupp.LNoVatNo = Convert.ToInt32(chkVATNO.Checked);
                txtVATRegNo.Text = "0";
            }
            objEmLandSupp.LVatRegNo = txtVATRegNo.Text.Trim();
            objEmLandSupp.LExtAcc = txtExtAcc.Text;

            objEmLandSupp.LIATAReg = txtIataReg.Text;
            objEmLandSupp.LAlphaCode = txtAlphaCode.Text;
            objEmLandSupp.LAmadeus = txtAmadeus.Text;
            objEmLandSupp.LGalileo = txtGalileo.Text;
            objEmLandSupp.LSabre = txtSabre.Text;
            objEmLandSupp.LWorldSpan = txtWorldSpan.Text;
            objEmLandSupp.LCarHire = txtCarHire.Text;
            objEmLandSupp.LFrontDesk = txtFrontDesk.Text;
            objEmLandSupp.LOtherPropertyNo = txtPropertyNo.Text;

            objEmLandSupp.LBank = Convert.ToInt32(dropBank.SelectedValue);
            objEmLandSupp.LBranchCode = txtBranchCode.Text;
            objEmLandSupp.LBranchName = txtBranchName.Text;
            objEmLandSupp.LAccountNo = txtAccountNo.Text;
            objEmLandSupp.LAccountType = Convert.ToInt32(dropAccountType.SelectedValue);
            objEmLandSupp.LAccHolder = txtAccHolder.Text;

            objEmLandSupp.LQuickGIAccount = txtGiAccountSub.Text;
            objEmLandSupp.LLedgerAccount = txtLedgerAccount.Text;
            objEmLandSupp.LCommissMethod = Convert.ToInt32(dropCommiMethod.SelectedValue);


            if (ChkZeroComm.Checked)
            {
                objEmLandSupp.LZeroCommission = Convert.ToInt32(ChkZeroComm.Checked);
            }
            string CommissionPerc = string.IsNullOrEmpty(txtCommPerc.Text.Trim()) ? "0" : txtCommPerc.Text.Trim();
            objEmLandSupp.LCommPercentage = Convert.ToDecimal(CommissionPerc);


            objEmLandSupp.LPaymentMethod = Convert.ToInt32(dropPaymentMethod.SelectedValue);

            if (chkClientInvoice.Checked)
            {
                objEmLandSupp.LClientTaxInvoice = Convert.ToInt32(chkClientInvoice.Checked);
            }

            objEmLandSupp.LClientInvoiceType = Convert.ToInt32(dropTreatInvType.SelectedValue);



            objEmLandSupp.LPrinciTaxInvoice = Convert.ToInt32(chkPrinTaxInvoice.Checked);
            objEmLandSupp.LIgnDupInvoiceNo = Convert.ToInt32(chkAlphaTicket.Checked);
            objEmLandSupp.LAllocItemType = Convert.ToInt32(dropAllocItemType.SelectedValue);



            objEmLandSupp.LNoteType = Convert.ToInt32(dropNoteType.SelectedValue);
            objEmLandSupp.LNotes = txtNotes.Text;


            objEmLandSupp.LContactKey = txtContactKey.Text;
            objEmLandSupp.LContactName = txtContactName.Text;
            objEmLandSupp.LContactPosition = txtPosition.Text;
            objEmLandSupp.LContTelephone = txtTelephone.Text;
            objEmLandSupp.LContEmailAddress = txtEmailAddress.Text;
            objEmLandSupp.LContAutoMail = Convert.ToInt32(chkAutomail.Checked);
            objEmLandSupp.ContactCellNo = txtConCellNo.Text;
            objEmLandSupp.ContactFax = txtConFaxNo.Text;
            objEmLandSupp.ContactDeactivate = Convert.ToInt32(chkConDeactivate.Checked);
            objEmLandSupp.SupplMainGIAccCode = txtAccountCode.Text + txtGiAccountSub.Text;

            objEmLandSupp.CreatedBy = 0;


            //Charted Accounts.

            DataSet ds = _BOUtility.getMainAccounts();

            //objEmLandSupp.ChartedAccName = txtSupplierName.Text;
            //objEmLandSupp.ChartedMasterAccName = 1;
            //objEmLandSupp.Type = "2";
            //objEmLandSupp.BaseCurrency = 0;
            //objEmLandSupp.TranCurrency = 0;
            //objEmLandSupp.DepartmentId = 0;
            //objEmLandSupp.AccCode = txtAccountCode.Text + txtGiAccount1.Text;
            //objEmLandSupp.CategoryId = 3;

            if (cmdSubmit.Text == "Update")
            {
                objEmLandSupp.AccCode = txtAccountCode.Text;
                objEmLandSupp.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objEmLandSupp.ChartedAccName = txtSupplierName.Text;
                objEmLandSupp.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objEmLandSupp.RefType = "LandSupplier";
                objEmLandSupp.RefId = hf_LSupplierId.Value;
            }
            if (cmdSubmit.Text == "Submit")
            {
                objEmLandSupp.ChartedAccName = txtSupplierName.Text;
                objEmLandSupp.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objEmLandSupp.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objEmLandSupp.BaseCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["BaseCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["BaseCurrency"].ToString());
                objEmLandSupp.TranCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TranCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TranCurrency"].ToString());
                objEmLandSupp.DepartmentId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DepartmentId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["DepartmentId"].ToString());
                objEmLandSupp.AccCode = txtAccountCode.Text + txtGiAccountSub.Text;
                objEmLandSupp.CategoryId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["CategoryId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["CategoryId"].ToString());
                objEmLandSupp.RefType = "LandSupplier";
            }

            int Result = objLandSuppliers.InsUpdlandSupplier(objEmLandSupp);

            if (cmdSubmit.Text == "Submit")
            {
                objEmLandSupp.RefId = Result.ToString();
            }
            if (cmdSubmit.Text == "Submit")
            {
                objEmLandSupp.IsClient = 0;
                int ChartedLResult = objLandSuppliers.InsUpdChartAccounts(objEmLandSupp);
            }
            if (cmdSubmit.Text == "Update")
            {
                int AccountUpdate = objLandSuppliers.UpdateChartAccounts(objEmLandSupp);
            }
            if (Result > 0)
            {

                foreach (RepeaterItem row in rptContactDetails.Items)
                {

                    TextBox rtxtContactKey = row.FindControl("txtContactKey") as TextBox;
                    TextBox rtxtContactName = row.FindControl("txtContactName") as TextBox;
                    TextBox rtxtPosition = row.FindControl("txtPosition") as TextBox;
                    TextBox rtxtTelephone = row.FindControl("txtTelephone") as TextBox;
                    TextBox rtxtEmailAddress = row.FindControl("txtEmailAddress") as TextBox;
                    CheckBox rchkAutomail = row.FindControl("chkAutomail") as CheckBox;
                    TextBox rtxtConCellNo = row.FindControl("txtConCellNo") as TextBox;
                    TextBox rtxtConFaxNo = row.FindControl("txtConFaxNo") as TextBox;
                    CheckBox rchkConDeactivate = row.FindControl("chkConDeactivate") as CheckBox;


                    HiddenField rhfContactId = (HiddenField)row.FindControl("hfContactId");

                    objEmLandSupp.ContactId = Convert.ToInt32(rhfContactId.Value);
                    objEmLandSupp.ContactKey = rtxtContactKey.Text;
                    objEmLandSupp.ContactName = rtxtContactName.Text;
                    objEmLandSupp.Position = rtxtPosition.Text;
                    objEmLandSupp.Telephone = rtxtTelephone.Text;
                    objEmLandSupp.EmailAddress = rtxtEmailAddress.Text;
                    objEmLandSupp.AutoMail = Convert.ToInt32(chkAutomail.Checked);
                    objEmLandSupp.ConCellNo = rtxtConCellNo.Text;
                    objEmLandSupp.ConFax = rtxtConFaxNo.Text;
                    objEmLandSupp.ConDeactivate = Convert.ToInt32(chkConDeactivate.Checked);
                    objEmLandSupp.LSupplierId = Result;
                    objEmLandSupp.UserCategory = "Land";

                    int rResult = objLandSuppliers.InsertUpdContact(objEmLandSupp);
                    if (rResult > 0)
                    {
                        lblMsg.Text = " Added Successfully";
                    }
                    else
                    {
                        lblMsg.Text = "Not Successfully please tryagain";
                    }
                }

                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Land Suppliers Created Successfully.");
                ClearControls();
                Response.Redirect("LandSupplierList.aspx", false);
            }
            else
            {

                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Land suppliers was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void GetLandSuppliers(int LSupplierId)
    {
        try
        {
            objEmLandSupp.LSupplierId = LSupplierId;
            DataSet objds = objLandSuppliers.GetLandSupplier(LSupplierId);

            if (objds.Tables[0].Rows.Count > 0)
            {
                hf_LSupplierId.Value = objds.Tables[0].Rows[0]["LSupplierId"].ToString();

                txtAccountCode.Text = objds.Tables[0].Rows[0]["LSupAccountCode"].ToString();
                txtSupplierName.Text = objds.Tables[0].Rows[0]["LSupplierName"].ToString();
                dropStatus.SelectedIndex = dropStatus.Items.IndexOf(dropStatus.Items.FindByValue(objds.Tables[0].Rows[0]["LIsActive"].ToString()));
                dropServiceType.SelectedIndex = dropServiceType.Items.IndexOf(dropServiceType.Items.FindByValue(objds.Tables[0].Rows[0]["LServiceType"].ToString()));
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(objds.Tables[0].Rows[0]["LCountry"].ToString()));
                Get_State_Country();
                dropState.SelectedIndex = dropState.Items.IndexOf(dropState.Items.FindByValue(objds.Tables[0].Rows[0]["LStateId"].ToString()));
                Get_City_State();
                dropCity.SelectedIndex = dropCity.Items.IndexOf(dropCity.Items.FindByValue(objds.Tables[0].Rows[0]["LCity"].ToString()));
                dropDivision.SelectedIndex = dropDivision.Items.IndexOf(dropDivision.Items.FindByValue(objds.Tables[0].Rows[0]["LDivision"].ToString()));
                dropConsultant.SelectedIndex = dropConsultant.Items.IndexOf(dropConsultant.Items.FindByValue(objds.Tables[0].Rows[0]["LConsultant"].ToString()));

                txtLatitude.Text = objds.Tables[0].Rows[0]["LLatitute"].ToString();
                txtLongitude.Text = objds.Tables[0].Rows[0]["LLongitude"].ToString();

                txtTelephoneNo.Text = objds.Tables[0].Rows[0]["LTelephone"].ToString();
                txtFax.Text = objds.Tables[0].Rows[0]["LFax"].ToString();
                txtCellNo.Text = objds.Tables[0].Rows[0]["LCellNo"].ToString();
                txtContact.Text = objds.Tables[0].Rows[0]["LContactNo"].ToString();
                txtEmail.Text = objds.Tables[0].Rows[0]["LEmail"].ToString();
                txtWeb.Text = objds.Tables[0].Rows[0]["LWeb"].ToString();
                txtPhysicalAddress.Text = objds.Tables[0].Rows[0]["LPhysicalAddress"].ToString();
                txtPostalAddress.Text = objds.Tables[0].Rows[0]["LPostalAddress"].ToString();


                if (Convert.ToInt32(objds.Tables[0].Rows[0]["LNoVatNo"].ToString()) == 1)
                {
                    chkVATNO.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LNoVatNo"]);
                    txtVATRegNo.Enabled = false;
                }
                else
                {
                    txtVATRegNo.Text = objds.Tables[0].Rows[0]["LVatRegNo"].ToString().Trim();
                }

                txtExtAcc.Text = objds.Tables[0].Rows[0]["LExtAcc"].ToString();

                txtIataReg.Text = objds.Tables[0].Rows[0]["LIATAReg"].ToString();
                txtAlphaCode.Text = objds.Tables[0].Rows[0]["LAlphaCode"].ToString();
                txtAmadeus.Text = objds.Tables[0].Rows[0]["LAmadeus"].ToString();
                txtGalileo.Text = objds.Tables[0].Rows[0]["LGalileo"].ToString();
                txtSabre.Text = objds.Tables[0].Rows[0]["LSabre"].ToString();
                txtWorldSpan.Text = objds.Tables[0].Rows[0]["LWorldSpan"].ToString();
                txtCarHire.Text = objds.Tables[0].Rows[0]["LCarHire"].ToString();
                txtFrontDesk.Text = objds.Tables[0].Rows[0]["LFrontDesk"].ToString();
                txtPropertyNo.Text = objds.Tables[0].Rows[0]["LOtherPropertyNo"].ToString();

                dropBank.SelectedIndex = dropBank.Items.IndexOf(dropBank.Items.FindByValue(objds.Tables[0].Rows[0]["LBank"].ToString()));
                txtBranchCode.Text = objds.Tables[0].Rows[0]["LBranchCode"].ToString();
                txtBranchName.Text = objds.Tables[0].Rows[0]["LBranchName"].ToString();
                txtAccountNo.Text = objds.Tables[0].Rows[0]["LAccountNo"].ToString();
                dropAccountType.SelectedIndex = dropAccountType.Items.IndexOf(dropAccountType.Items.FindByValue(objds.Tables[0].Rows[0]["LAccountType"].ToString()));
                txtAccHolder.Text = objds.Tables[0].Rows[0]["LAccHolder"].ToString();

                txtGiAccountSub.Text = objds.Tables[0].Rows[0]["LQuickGIAccount"].ToString();
                txtLedgerAccount.Text = objds.Tables[0].Rows[0]["LLedgerAccount"].ToString();
                dropCommiMethod.SelectedIndex = dropCommiMethod.Items.IndexOf(dropCommiMethod.Items.FindByValue(objds.Tables[0].Rows[0]["LCommissMethod"].ToString()));

                if (Convert.ToInt32(objds.Tables[0].Rows[0]["LZeroCommission"].ToString()) == 1)
                {
                    ChkZeroComm.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LZeroCommission"]);
                    txtCommPerc.Enabled = false;
                }
                else
                {
                    txtCommPerc.Text = objds.Tables[0].Rows[0]["LCommPercentage"].ToString().Trim();
                }

                dropPaymentMethod.SelectedIndex = dropPaymentMethod.Items.IndexOf(dropPaymentMethod.Items.FindByValue(objds.Tables[0].Rows[0]["LPaymentMethod"].ToString()));

                if (Convert.ToInt32(objds.Tables[0].Rows[0]["LClientTaxInvoice"].ToString()) == 1)
                {
                    chkClientInvoice.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LClientTaxInvoice"]);
                    dropTreatInvType.Enabled = false;
                }
                else
                {
                    dropTreatInvType.SelectedIndex = dropPaymentMethod.Items.IndexOf(dropPaymentMethod.Items.FindByValue(objds.Tables[0].Rows[0]["LClientInvoiceType"].ToString()));
                }

                chkPrinTaxInvoice.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LPrinciTaxInvoice"]);
                chkAlphaTicket.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LIgnDupInvoiceNo"]);

                dropAllocItemType.SelectedIndex = dropAllocItemType.Items.IndexOf(dropAllocItemType.Items.FindByValue(objds.Tables[0].Rows[0]["LAllocItemType"].ToString()));

                txtNotes.Text = objds.Tables[0].Rows[0]["LNotes"].ToString();
                dropNoteType.SelectedIndex = dropCommiMethod.Items.IndexOf(dropCommiMethod.Items.FindByValue(objds.Tables[0].Rows[0]["LNoteType"].ToString()));


                txtContactKey.Text = objds.Tables[0].Rows[0]["LContactKey"].ToString();
                txtContactName.Text = objds.Tables[0].Rows[0]["LContactName"].ToString();
                txtPosition.Text = objds.Tables[0].Rows[0]["LContactPosition"].ToString();
                txtTelephone.Text = objds.Tables[0].Rows[0]["LContTelephone"].ToString();
                txtEmailAddress.Text = objds.Tables[0].Rows[0]["LContEmailAddress"].ToString();
                chkAutomail.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["LContAutoMail"]);
                txtConFaxNo.Text = objds.Tables[0].Rows[0]["ContactFax"].ToString();
                txtConCellNo.Text = objds.Tables[0].Rows[0]["ContactCellNo"].ToString();
                chkConDeactivate.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["ContactDeactivate"]);


                if (objds.Tables[1].Rows.Count > 0)
                {
                    BindRepeterContactDetails(objds.Tables[1], rptContactDetails);
                }
            }
        }



        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandSupplierList.aspx", false);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandSuppliers.aspx", false);
    }
    protected void chkVATNO_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVATNO.Checked)
        {
            txtVATRegNo.Enabled = false;
            txtVATRegNo.Text = string.Empty;
        }
        else
        {
            txtVATRegNo.Enabled = true;
        }
    }
    protected void txtCommPerc_TextChanged(object sender, EventArgs e)
    {
        if (txtCommPerc.Text.Contains("."))
        {
            txtCommPerc.Text = txtCommPerc.Text;
        }
        else
        {
            txtCommPerc.Text = txtCommPerc.Text + ".00";
        }
    }
    protected void ChkZeroComm_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkZeroComm.Checked)
        {
            txtCommPerc.Enabled = false;
            txtCommPerc.Text = string.Empty;
        }
        else
        {
            txtCommPerc.Enabled = true;
        }
    }
    protected void chkClientInvoice_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClientInvoice.Checked)
        {
            dropTreatInvType.Enabled = false;

            dropTreatInvType.SelectedValue = "0";
        }
        else
        {
            dropTreatInvType.Enabled = true;
        }
    }


    #region Bind Methods
    public void BindStatus()
    {
        try
        {
            DataSet ds = _BOUtility.getStatus();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropStatus.DataSource = ds.Tables[0];
                dropStatus.DataTextField = "Description";
                dropStatus.DataValueField = "Id";
                dropStatus.DataBind();
                dropStatus.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropStatus.DataSource = null;
                dropStatus.DataBind();
                dropStatus.Items.Insert(0, new ListItem("-- Please Select --", "0"));


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindServiceType()
    {
        try
        {
            string Type = "Land";
            DataSet ds = new DataSet();
            ds = _BOUtility.GetServiceTypeByType(Type);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dropServiceType.DataSource = ds.Tables[0];
                dropServiceType.DataTextField = "ComDesc";
                dropServiceType.DataValueField = "ComId";
                dropServiceType.DataBind();
                dropServiceType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropServiceType.DataSource = null;
                dropServiceType.DataBind();
                dropServiceType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindDivisions()
    {
        try
        {
            DataSet ds = _BOUtility.GetDivisions();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropDivision.DataSource = ds.Tables[0];
                dropDivision.DataTextField = "DivName";
                dropDivision.DataValueField = "DivisionId";
                dropDivision.DataBind();
                dropDivision.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropDivision.DataSource = null;
                dropDivision.DataBind();
                dropDivision.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindConsultants()
    {
        try
        {
            DataSet ds = _BOUtility.GetConsultants();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropConsultant.DataSource = ds.Tables[0];
                dropConsultant.DataTextField = "Name";
                dropConsultant.DataValueField = "ConsultantId";
                dropConsultant.DataBind();
                dropConsultant.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropConsultant.DataSource = null;
                dropConsultant.DataBind();
                dropConsultant.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindBankNames()
    {
        try
        {
            DataSet ds = _BOUtility.GetBankNames();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropBank.DataSource = ds.Tables[0];
                dropBank.DataTextField = "BankName";
                dropBank.DataValueField = "BankId";
                dropBank.DataBind();
                dropBank.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropBank.DataSource = null;
                dropBank.DataBind();
                dropBank.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindAccountTypes()
    {
        try
        {
            DataSet ds = _BOUtility.GetAccountTypes();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropAccountType.DataSource = ds.Tables[0];
                dropAccountType.DataTextField = "AccountName";
                dropAccountType.DataValueField = "AccountId";
                dropAccountType.DataBind();
                dropAccountType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropAccountType.DataSource = null;
                dropAccountType.DataBind();
                dropAccountType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindCommsType()
    {
        try
        {
            DataSet ds = _BOUtility.GetCommissionTypes();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCommiMethod.DataSource = ds.Tables[0];
                dropCommiMethod.DataTextField = "ComDesc";
                dropCommiMethod.DataValueField = "ComId";
                dropCommiMethod.DataBind();
                dropCommiMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropCommiMethod.DataSource = null;
                dropCommiMethod.DataBind();
                dropCommiMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindPaymentMethods()
    {
        try
        {
            BAPaymentTypes objBAPaymentTypes = new BAPaymentTypes();
            int PaymentId = 0;
            DataSet ds = objBAPaymentTypes.GetPaymentTypes(PaymentId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropPaymentMethod.DataSource = ds.Tables[0];
                dropPaymentMethod.DataTextField = "PaymentName";
                dropPaymentMethod.DataValueField = "PaymentId";
                dropPaymentMethod.DataBind();
                dropPaymentMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropPaymentMethod.DataSource = null;
                dropPaymentMethod.DataBind();
                dropPaymentMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindCountry()
    {
        try
        {

            DataSet ds = _BOUtility.getCountries();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCountry.DataSource = ds.Tables[0];
                dropCountry.DataTextField = "Name";
                dropCountry.DataValueField = "Id";
                dropCountry.DataBind();
                dropCountry.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropCountry.DataSource = null;
                dropCountry.DataBind();
                dropCountry.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_State_Country();
    }
    protected void dropState_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_City_State();
    }




    public void BindSupplierInvoice()
    {
        try
        {
            DataSet ds = _BOUtility.GetSupplierInvoice();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropTreatInvType.DataSource = ds.Tables[0];
                dropTreatInvType.DataTextField = "SupVatInvoName";
                dropTreatInvType.DataValueField = "SupVatInvoId";
                dropTreatInvType.DataBind();
                dropTreatInvType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropTreatInvType.DataSource = null;
                dropTreatInvType.DataBind();
                dropTreatInvType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindItemLoadType()
    {
        try
        {
            DataSet ds = _BOUtility.GetItemLoadingType();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropAllocItemType.DataSource = ds.Tables[0];
                dropAllocItemType.DataTextField = "ItmName";
                dropAllocItemType.DataValueField = "ItemLoadId";
                dropAllocItemType.DataBind();
                dropAllocItemType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropAllocItemType.DataSource = null;
                dropAllocItemType.DataBind();
                dropAllocItemType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindNotepadType()
    {
        try
        {
            BAContactNote objBANote = new BAContactNote();

            int NotePadId = 0;
            DataSet ds = objBANote.GetContactNote(NotePadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropNoteType.DataSource = ds.Tables[0];
                dropNoteType.DataTextField = "NpName";
                dropNoteType.DataValueField = "NotePadId";
                dropNoteType.DataBind();
                dropNoteType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropNoteType.DataSource = null;
                dropNoteType.DataBind();
                dropNoteType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void Get_State_Country()
    {
        try
        {
            dropState.Items.Clear();
            DataSet ds = new DataSet();
            int country_id = Convert.ToInt32(dropCountry.SelectedValue.ToString());
            ds = _BOUtility.get_State_Country(country_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropState.DataSource = ds.Tables[0];
                dropState.Items.Clear();
                //   dropState.Items.Add(new ListItem("-Please Select-", "0"));
                dropState.DataTextField = "Name";
                dropState.DataValueField = "Id";
                dropState.DataBind();
                dropState.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropState.DataSource = null;
                dropState.DataBind();
                dropState.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    public void Get_City_State()
    {
        try
        {
            dropCity.Items.Clear();
            DataSet ds = new DataSet();
            int state_id = Convert.ToInt32(dropState.SelectedValue.ToString());
            ds = _BOUtility.get_City_State(state_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCity.DataSource = ds.Tables[0];
                dropCity.Items.Clear();
                dropCity.Items.Insert(0, new ListItem("-- Please Select --", "0"));
                // dropCity.Items.Add(new ListItem("-Please Select-", "0"));
                dropCity.DataTextField = "Name";
                dropCity.DataValueField = "Id";
                dropCity.DataBind();
                //dropCity.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropCity.DataSource = null;
                dropCity.DataBind();
                dropCity.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindMainAccounts()
    {
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = _BOUtility.getMainAccounts();
            dt = ds.Tables[1];
            if (ds.Tables[1].Rows.Count > 0)
            {
                txtAccountCode.Text = ds.Tables[1].Rows[0]["MainAccCode"].ToString();
                txtGiAccount.Text = ds.Tables[1].Rows[0]["MainAccCode"].ToString();
                //txt1.Text = objDS.Tables[0].Rows[0]["ColumnName"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindGroupMaster()
    {

        try
        {
            DataSet ds = new DataSet();
            int Id = 0;
            ds = _BOUtility.GetGroupMaster(Id);
            if (ds.Tables.Count > 0)
            {

                dropGroup.DataSource = ds.Tables[0];
                dropGroup.DataTextField = "Name";
                dropGroup.DataValueField = "Id";
                dropGroup.DataBind();
                dropGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropGroup.DataSource = null;
                dropGroup.DataBind();
                dropGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion Bind Methods


    void ClearControls()
    {
        hf_LSupplierId.Value = "0";
        txtSupplierName.Text = "";
        dropStatus.SelectedValue = "0";
        dropServiceType.SelectedValue = "0";
        dropCountry.SelectedValue = "0";
        dropState.SelectedValue = "0";
        dropDivision.SelectedValue = "0";
        dropCity.SelectedValue = "0";
        dropConsultant.SelectedValue = "0";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtTelephoneNo.Text = "";
        txtFax.Text = "";
        txtCellNo.Text = "";
        txtContact.Text = "";
        txtEmail.Text = "";
        txtWeb.Text = "";
        txtPhysicalAddress.Text = "";
        txtPostalAddress.Text = "";
        chkVATNO.Checked = false;
        txtVATRegNo.Text = "";
        txtVATRegNo.Enabled = true;
        txtExtAcc.Text = "";
        txtIataReg.Text = "";
        txtAlphaCode.Text = "";
        txtAmadeus.Text = "";
        txtGalileo.Text = "";
        txtSabre.Text = "";
        txtWorldSpan.Text = "";
        txtFrontDesk.Text = "";
        txtCarHire.Text = "";
        txtPropertyNo.Text = "";
        dropBank.SelectedValue = "0";
        txtBranchCode.Text = "";
        txtBranchName.Text = "";
        txtAccountNo.Text = "";
        dropAccountType.SelectedValue = "0";
        txtAccHolder.Text = "";
        txtGiAccountSub.Text = "";
        txtLedgerAccount.Text = "";
        dropCommiMethod.SelectedValue = "0";
        txtCommPerc.Text = "";
        ChkZeroComm.Checked = false;
        txtCommPerc.Enabled = true;
        dropPaymentMethod.SelectedValue = "0";
        chkClientInvoice.Checked = false;
        dropTreatInvType.Enabled = true;
        dropTreatInvType.SelectedValue = "0";
        chkPrinTaxInvoice.Checked = false;
        chkAlphaTicket.Checked = false;
        dropAllocItemType.SelectedValue = "0";
        dropNoteType.SelectedValue = "0";
        txtNotes.Text = "";
        txtContactKey.Text = "";
        txtContactName.Text = "";
        txtEmailAddress.Text = "";
        txtTelephoneNo.Text = "";
        chkConDeactivate.Checked = false;
        chkAutomail.Checked = false;
    }



    #region repetors

    private void BindRepeterContactDetails(DataTable obDs, Repeater rpt)
    {
        try
        {
            rpt.DataSource = obDs;
            rpt.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindContactDetails()
    {
        try
        {
            DataTable dtContent = new DataTable();
            DataRow dr;
            dtContent.Columns.Add("ContactId");
            dtContent.Columns.Add("ContactKey");
            dtContent.Columns.Add("ContactName");
            dtContent.Columns.Add("Position");
            dtContent.Columns.Add("Telephone");
            dtContent.Columns.Add("EmailAddress");
            dtContent.Columns.Add("AutoMail");
            dtContent.Columns.Add("ConCellNo");
            dtContent.Columns.Add("ConFax");
            dtContent.Columns.Add("ConDeactivate");
            int count = rptContactDetails.Items.Count;
            foreach (RepeaterItem row in rptContactDetails.Items)
            {
                TextBox txtContactKey = (TextBox)row.FindControl("txtContactKey");
                TextBox txtContactName = (TextBox)row.FindControl("txtContactName");
                TextBox txtPosition = (TextBox)row.FindControl("txtPosition");
                TextBox txtTelephone = (TextBox)row.FindControl("txtTelephone");
                TextBox txtEmailAddress = (TextBox)row.FindControl("txtEmailAddress");
                CheckBox chkAutomail = (CheckBox)row.FindControl("chkAutomail");
                TextBox txtConCellNo = (TextBox)row.FindControl("txtConCellNo");
                TextBox txtConFaxNo = (TextBox)row.FindControl("txtConFaxNo");
                CheckBox chkConDeactivate = (CheckBox)row.FindControl("chkConDeactivate");

                HiddenField hfContactId = (HiddenField)row.FindControl("hfContactId");

                DataRow drexist = dtContent.NewRow();
                drexist["ContactId"] = hfContactId.Value;
                drexist["ContactKey"] = txtContactKey.Text;
                drexist["ContactName"] = txtContactName.Text;
                drexist["Position"] = txtPosition.Text;
                drexist["Telephone"] = txtTelephone.Text;
                drexist["EmailAddress"] = txtEmailAddress.Text;
                drexist["AutoMail"] = chkAutomail.Checked;
                drexist["ConCellNo"] = txtConCellNo.Text;
                drexist["ConFax"] = txtConFaxNo.Text;
                drexist["ConDeactivate"] = chkConDeactivate.Checked;
                dtContent.Rows.Add(drexist);
            }
            dr = dtContent.NewRow();
            dr["ContactId"] = "0";
            dr["ContactKey"] = "";
            dr["ContactName"] = "";
            dr["Position"] = "";
            dr["Telephone"] = "";
            dr["EmailAddress"] = "";
            dr["AutoMail"] = "";
            dr["ConCellNo"] = "";
            dr["ConFax"] = "";
            dr["ConDeactivate"] = "";
            dtContent.Rows.Add(dr);
            rptContactDetails.DataSource = dtContent;
            rptContactDetails.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void rptContactDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string strCommandName = string.Empty;
        string strCommandArgu = "0";
        strCommandName = e.CommandName;
        HiddenField hfContactId = (HiddenField)e.Item.FindControl("ContactId");
        strCommandArgu = e.CommandArgument.ToString();
        int index = e.Item.ItemIndex;
        if (strCommandName == "Delete")
        {
            if (strCommandArgu == "0")
            {
                DeleteContactt(index);
            }
            else
            {
                DeleteContact(Convert.ToInt32(strCommandArgu));
                DeleteContactt(index);
            }

        }
    }

    protected void ImgbtnContactDetails_Click(object sender, ImageClickEventArgs e)
    {
        BindContactDetails();
    }

    private void DeleteContactt(int nRowIndex)
    {
        DataTable dtContent = new DataTable();

        dtContent.Columns.Add("ContactId");
        dtContent.Columns.Add("ContactKey");
        dtContent.Columns.Add("ContactName");
        dtContent.Columns.Add("Position");
        dtContent.Columns.Add("Telephone");
        dtContent.Columns.Add("EmailAddress");
        dtContent.Columns.Add("AutoMail");
        dtContent.Columns.Add("ConCellNo");
        dtContent.Columns.Add("ConFax");
        dtContent.Columns.Add("ConDeactivate");

        foreach (RepeaterItem row in rptContactDetails.Items)
        {
            TextBox txtContactKey = (TextBox)row.FindControl("txtContactKey");
            TextBox txtContactName = (TextBox)row.FindControl("txtContactName");
            TextBox txtPosition = (TextBox)row.FindControl("txtPosition");
            TextBox txtTelephone = (TextBox)row.FindControl("txtTelephone");
            TextBox txtEmailAddress = (TextBox)row.FindControl("txtEmailAddress");
            CheckBox chkAutomail = (CheckBox)row.FindControl("chkAutomail");
            TextBox txtConCellNo = (TextBox)row.FindControl("txtConCellNo");
            TextBox txtConFaxNo = (TextBox)row.FindControl("txtConFaxNo");
            CheckBox chkConDeactivate = (CheckBox)row.FindControl("chkConDeactivate");


            HiddenField hfContactId = (HiddenField)row.FindControl("hfContactId");

            DataRow drexist = dtContent.NewRow();
            drexist["ContactId"] = hfContactId.Value;
            drexist["ContactKey"] = txtContactKey.Text;
            drexist["ContactName"] = txtContactName.Text;
            drexist["Position"] = txtPosition.Text;
            drexist["Telephone"] = txtTelephone.Text;
            drexist["EmailAddress"] = txtEmailAddress.Text;
            drexist["AutoMail"] = chkAutomail.Checked;
            drexist["ConCellNo"] = txtConCellNo.Text;
            drexist["ConFax"] = txtConFaxNo.Text;
            drexist["ConDeactivate"] = chkConDeactivate.Checked;

            dtContent.Rows.Add(drexist);
        }
        dtContent.Rows.RemoveAt(nRowIndex);
        dtContent.AcceptChanges();

        rptContactDetails.DataSource = dtContent;
        rptContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Action Completed');", true);
    }


    private void DeleteContact(int ContactId)
    {

        int Result = objLandSuppliers.DeleteContact(ContactId);
    }

    # endregion repetors

    protected void txtGiAccountSub_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _BOUtility.CheckAccCodeExitorNot(txtGiAccountSub.Text, "LandSup");

        ds.Tables.Add(dt);

        if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
        {
            lblaccnoerr.Text = "Already Exist";
            lblaccnoerr.ForeColor = System.Drawing.Color.Red;
            txtGiAccountSub.Text = "";
        }
        else
        {
            lblaccnoerr.Text = "Available";
            lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }
}