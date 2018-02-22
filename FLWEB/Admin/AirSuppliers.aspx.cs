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
using System.Drawing;

public partial class Admin_AirSuppliers : System.Web.UI.Page
{
    EMAirSuppliers objEMAirsupp = new EMAirSuppliers();
    BAAirSuppliers objAirSupplier = new BAAirSuppliers();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoopTextboxes(Page.Controls);
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

            if (!string.IsNullOrEmpty(Request.QueryString["SupplierId"]))
            {
                string supplierId = Request.QueryString["SupplierId"].ToString();

                GetAirSuppliers(Convert.ToInt32(supplierId));
                cmdSubmit.Text = "Update";

            }

        }

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


    private void LoopTextboxes(ControlCollection controlCollection)
    {

        int langId = Convert.ToInt32(Session["LanguageId"]);
        DataSet ds = _BOUtility.GetLanguageDescription(langId);



        foreach (Control control in controlCollection)
        {


            if (control is TextBox)
            {


                string text = ((TextBox)control).ID;
                string place = (((TextBox)control).FindControl(text) as TextBox).Text;



                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            ((TextBox)control).Text = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
                        }

                    }
                }

            }
            if (control is DropDownList)
            {

                string text = ((DropDownList)control).ID;

                string place = (((DropDownList)control).FindControl(text) as DropDownList).Text;



                if (ds.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            ((DropDownList)control).Text = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
                        }

                    }

                }

            }

            if (control is Button)
            {

                string id = ((Button)control).ID;
                string text = ((Button)control).Text;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            ((Button)control).Text = LatestlabelDescrip;
                        }

                    }
                }
            }


            if (control is Label)
            {

                string id = ((Label)control).ID;
                string text = ((Label)control).Text;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            ((Label)control).Text = LatestlabelDescrip;
                        }

                    }
                }

            }


            System.Web.UI.HtmlControls.HtmlHead chk = control as System.Web.UI.HtmlControls.HtmlHead;

            if (chk != null)
            {
                //string text = chk.InnerHtml
            }



            //Label mylabel;
            //if (control.GetType() == typeof(Label)) //or any other logic
            //{
            //    mylabel = (Label)control;
            //    mylabel.BackColor = Color.Red;
            //}


            if (control.Controls != null)
            {
                LoopTextboxes(control.Controls);
            }
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
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateAirSupplier();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AirSupplierList.aspx", false);
    }

    private void InsertUpdateAirSupplier()
    {
        try
        {
            objEMAirsupp.SupplierId = Convert.ToInt32(hf_SupplierId.Value);
            objEMAirsupp.SupplierName = txtSupplierName.Text;
            objEMAirsupp.SupAccountCode = txtAccountCode.Text;
            objEMAirsupp.IsActive = Convert.ToInt32(dropStatus.SelectedValue);
            objEMAirsupp.ServiceType = Convert.ToInt32(dropServiceType.SelectedValue);
            objEMAirsupp.Country = Convert.ToInt32(dropCountry.SelectedValue);
            objEMAirsupp.GroupId = Convert.ToInt32(dropGroup.SelectedValue);
            objEMAirsupp.StateId = Convert.ToInt32(dropState.SelectedValue);
            objEMAirsupp.Division = Convert.ToInt32(dropDivision.SelectedValue);
            objEMAirsupp.City = Convert.ToInt32(dropCity.SelectedValue);
            objEMAirsupp.Consultant = Convert.ToInt32(dropConsultant.SelectedValue);

            objEMAirsupp.Telephone = txtTelephoneNo.Text;
            objEMAirsupp.Fax = txtFax.Text;
            objEMAirsupp.CellNo = txtCellNo.Text;
            objEMAirsupp.ContactNo = txtContact.Text;
            objEMAirsupp.Email = txtEmail.Text;
            objEMAirsupp.Web = txtWeb.Text;
            objEMAirsupp.PhysicalAddress = txtPhysicalAddress.Text;
            objEMAirsupp.PostalAddress = txtPostalAddress.Text;

            if (chkVATNO.Checked)
            {
                objEMAirsupp.NoVatNo = Convert.ToInt32(chkVATNO.Checked);
                txtVATRegNo.Text = "0";
            }
            objEMAirsupp.VatRegNo = txtVATRegNo.Text.Trim();
            objEMAirsupp.ExtAcc = txtExtAcc.Text;
            objEMAirsupp.QuickTravelCode = txtAlphaCode.Text;

            objEMAirsupp.Bank = Convert.ToInt32(dropBank.SelectedValue);
            objEMAirsupp.BranchCode = txtBranchCode.Text;
            objEMAirsupp.BranchName = txtBranchName.Text;
            objEMAirsupp.AccountNo = txtAccountNo.Text;
            objEMAirsupp.AccountType = Convert.ToInt32(dropAccountType.SelectedValue);
            objEMAirsupp.AccHolder = txtAccHolder.Text;

            objEMAirsupp.QuickGIAccount = txtGiAccountSUb.Text;
            objEMAirsupp.LedgerAccount = txtLedgerAccount.Text;
            objEMAirsupp.CommissMethod = Convert.ToInt32(dropCommiMethod.SelectedValue);


            if (ChkZeroComm.Checked)
            {
                objEMAirsupp.ZeroCommission = Convert.ToInt32(ChkZeroComm.Checked);
            }
            string CommissionPerc = string.IsNullOrEmpty(txtCommPerc.Text.Trim()) ? "0" : txtCommPerc.Text.Trim();
            objEMAirsupp.CommPercentage = Convert.ToDecimal(CommissionPerc);


            objEMAirsupp.PaymentMethod = Convert.ToInt32(dropPaymentMethod.SelectedValue);

            if (chkClientInvoice.Checked)
            {
                objEMAirsupp.ClientTaxInvoice = Convert.ToInt32(chkClientInvoice.Checked);
            }

            objEMAirsupp.ClientInvoiceType = Convert.ToInt32(dropTreatInvType.SelectedValue);



            objEMAirsupp.PrinciTaxInvoice = Convert.ToInt32(chkPrinTaxInvoice.Checked);
            objEMAirsupp.IgnDupInvoiceNo = Convert.ToInt32(chkAlphaTicket.Checked);
            objEMAirsupp.AllocItemType = Convert.ToInt32(dropAllocItemType.SelectedValue);

            objEMAirsupp.EconomyClass = Convert.ToInt32(chkEconomy.Checked);
            objEMAirsupp.BusinessClass = Convert.ToInt32(chkBusiness.Checked);
            objEMAirsupp.FirstClass = Convert.ToInt32(ChkFirstClass.Checked);

            objEMAirsupp.NoteType = Convert.ToInt32(dropNoteType.SelectedValue);
            objEMAirsupp.Notes = txtNotes.Text;
            objEMAirsupp.SupplMainGIAccCode = txtAccountCode.Text + txtGiAccountSUb.Text;

            objEMAirsupp.CreatedBy = 0;

            //Charted Accounts Insert

            DataSet ds = _BOUtility.getMainAccounts();

            if (cmdSubmit.Text == "Update")
            {
                objEMAirsupp.AccCode = txtAccountCode.Text;
                objEMAirsupp.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objEMAirsupp.ChartedAccName = txtSupplierName.Text;
                objEMAirsupp.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objEMAirsupp.RefType = "AirSupplier";
                objEMAirsupp.RefId = hf_SupplierId.Value;
            }
            if (cmdSubmit.Text == "Submit")
            {
                objEMAirsupp.ChartedAccName = txtSupplierName.Text;
                objEMAirsupp.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objEMAirsupp.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objEMAirsupp.BaseCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["BaseCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["BaseCurrency"].ToString());
                objEMAirsupp.TranCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TranCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TranCurrency"].ToString());
                objEMAirsupp.DepartmentId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DepartmentId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["DepartmentId"].ToString());
                objEMAirsupp.AccCode = txtAccountCode.Text + txtGiAccountSUb.Text;
                objEMAirsupp.CategoryId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["CategoryId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["CategoryId"].ToString());
                objEMAirsupp.RefType = "AirSupplier";
              
            }
            int Result = objAirSupplier.InsUpdAirSupplier(objEMAirsupp);

            if (cmdSubmit.Text == "Submit")
            {
                objEMAirsupp.RefId = Result.ToString();
            }
            if (cmdSubmit.Text == "Submit")
            {
                objEMAirsupp.IsClient = 0;
                int ChartedResult = objAirSupplier.InsUpdChartAccounts(objEMAirsupp);
            }
            if (cmdSubmit.Text == "Update")
            {
                int AccountUpdate = objAirSupplier.UpdateChartAccounts(objEMAirsupp);
            }


            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Air Suppliers Created Successfully.");
                ClearControls();
                Response.Redirect("AirSupplierList.aspx", false);
            }
            else
            {

                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Airsuppliers was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void GetAirSuppliers(int SupplierId)
    {
        try
        {
            DataSet objds = objAirSupplier.GetAirSuppliers(SupplierId);
            if (objds.Tables[0].Rows.Count > 0)
            {
                hf_SupplierId.Value = objds.Tables[0].Rows[0]["SupplierId"].ToString();
                txtAccountCode.Text = objds.Tables[0].Rows[0]["SupAccountCode"].ToString();
                txtSupplierName.Text = objds.Tables[0].Rows[0]["SupplierName"].ToString();
                dropStatus.SelectedIndex = dropStatus.Items.IndexOf(dropStatus.Items.FindByValue(objds.Tables[0].Rows[0]["IsActive"].ToString()));
                dropServiceType.SelectedIndex = dropServiceType.Items.IndexOf(dropServiceType.Items.FindByValue(objds.Tables[0].Rows[0]["ServiceType"].ToString()));
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(objds.Tables[0].Rows[0]["Country"].ToString()));
                Get_State_Country();
                dropState.SelectedIndex = dropState.Items.IndexOf(dropState.Items.FindByValue(objds.Tables[0].Rows[0]["StateId"].ToString()));
                Get_City_State();
                dropCity.SelectedIndex = dropCity.Items.IndexOf(dropCity.Items.FindByValue(objds.Tables[0].Rows[0]["City"].ToString()));
                dropDivision.SelectedIndex = dropDivision.Items.IndexOf(dropDivision.Items.FindByValue(objds.Tables[0].Rows[0]["Division"].ToString()));
                dropConsultant.SelectedIndex = dropConsultant.Items.IndexOf(dropConsultant.Items.FindByValue(objds.Tables[0].Rows[0]["Consultant"].ToString()));
                txtTelephoneNo.Text = objds.Tables[0].Rows[0]["Telephone"].ToString();
                txtFax.Text = objds.Tables[0].Rows[0]["Fax"].ToString();
                txtCellNo.Text = objds.Tables[0].Rows[0]["CellNo"].ToString();
                txtContact.Text = objds.Tables[0].Rows[0]["ContactNo"].ToString();
                txtEmail.Text = objds.Tables[0].Rows[0]["Email"].ToString();
                txtWeb.Text = objds.Tables[0].Rows[0]["Web"].ToString();
                txtPhysicalAddress.Text = objds.Tables[0].Rows[0]["PhysicalAddress"].ToString();
                txtPostalAddress.Text = objds.Tables[0].Rows[0]["PostalAddress"].ToString();


                if (Convert.ToInt32(objds.Tables[0].Rows[0]["NoVatNo"].ToString()) == 1)
                {
                    chkVATNO.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["NoVatNo"]);
                    txtVATRegNo.Enabled = false;
                }
                else
                {
                    txtVATRegNo.Text = objds.Tables[0].Rows[0]["VatRegNo"].ToString().Trim();
                }

                txtExtAcc.Text = objds.Tables[0].Rows[0]["ExtAcc"].ToString();
                txtAlphaCode.Text = objds.Tables[0].Rows[0]["QuickTravelCode"].ToString();

                dropBank.SelectedIndex = dropBank.Items.IndexOf(dropBank.Items.FindByValue(objds.Tables[0].Rows[0]["Bank"].ToString()));
                txtBranchCode.Text = objds.Tables[0].Rows[0]["BranchCode"].ToString();
                txtBranchName.Text = objds.Tables[0].Rows[0]["BranchName"].ToString();
                txtAccountNo.Text = objds.Tables[0].Rows[0]["AccountNo"].ToString();
                dropAccountType.SelectedIndex = dropAccountType.Items.IndexOf(dropAccountType.Items.FindByValue(objds.Tables[0].Rows[0]["AccountType"].ToString()));
                txtAccHolder.Text = objds.Tables[0].Rows[0]["AccHolder"].ToString();

                txtGiAccountSUb.Text = objds.Tables[0].Rows[0]["QuickGIAccount"].ToString();
                txtLedgerAccount.Text = objds.Tables[0].Rows[0]["LedgerAccount"].ToString();
                dropCommiMethod.SelectedIndex = dropCommiMethod.Items.IndexOf(dropCommiMethod.Items.FindByValue(objds.Tables[0].Rows[0]["CommissMethod"].ToString()));

                if (Convert.ToInt32(objds.Tables[0].Rows[0]["ZeroCommission"].ToString()) == 1)
                {
                    ChkZeroComm.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["ZeroCommission"]);
                    txtCommPerc.Enabled = false;
                }
                else
                {
                    txtCommPerc.Text = objds.Tables[0].Rows[0]["CommPercentage"].ToString().Trim();
                }

                dropPaymentMethod.SelectedIndex = dropPaymentMethod.Items.IndexOf(dropPaymentMethod.Items.FindByValue(objds.Tables[0].Rows[0]["PaymentMethod"].ToString()));

                if (Convert.ToInt32(objds.Tables[0].Rows[0]["ClientTaxInvoice"].ToString()) == 1)
                {
                    chkClientInvoice.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["ClientTaxInvoice"]);
                    dropTreatInvType.Enabled = false;
                }
                else
                {
                    dropTreatInvType.SelectedIndex = dropPaymentMethod.Items.IndexOf(dropPaymentMethod.Items.FindByValue(objds.Tables[0].Rows[0]["ClientInvoiceType"].ToString()));
                }

                chkPrinTaxInvoice.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["PrinciTaxInvoice"]);
                chkAlphaTicket.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["IgnDupInvoiceNo"]);

                dropAllocItemType.SelectedIndex = dropAllocItemType.Items.IndexOf(dropAllocItemType.Items.FindByValue(objds.Tables[0].Rows[0]["AllocItemType"].ToString()));

                chkEconomy.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["EconomyClass"]);
                chkBusiness.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["BusinessClass"]);
                ChkFirstClass.Checked = Convert.ToBoolean(objds.Tables[0].Rows[0]["FirstClass"]);

                txtNotes.Text = objds.Tables[0].Rows[0]["Notes"].ToString();
                dropNoteType.SelectedIndex = dropCommiMethod.Items.IndexOf(dropCommiMethod.Items.FindByValue(objds.Tables[0].Rows[0]["NoteType"].ToString()));

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #region bind methods
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
            string Type = "Air";
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

    //public void BindMainAccount()
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        ds = _BOUtility.GetMainAccount();
    //        if (ds.Tables.Count > 0)
    //        {

    //            ddlClientType.DataSource = ds.Tables[0];
    //            ddlClientType.DataTextField = "Name";
    //            ddlClientType.DataValueField = "Id";
    //            ddlClientType.DataBind();
    //            ddlClientType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

    //        }
    //        else
    //        {
    //            ddlClientType.DataSource = null;
    //            ddlClientType.DataBind();
    //            ddlClientType.Items.Insert(0, new ListItem("--Please Select --", "0"));

    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //    }
    //}
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
                dropPaymentMethod.Items.Insert(0, new ListItem("--Please Select --", "0"));

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
                // dropState.Items.Add(new ListItem("-- Please Select--", "-1"));
                dropState.DataTextField = "Name";
                dropState.DataValueField = "Id";
                dropState.DataBind();
                dropState.Items.Insert(0, new ListItem("--Select Country--", "0"));

            }
            else
            {
                dropState.DataSource = null;
                dropState.DataBind();
                dropState.Items.Insert(0, new ListItem("--Select Country--", "0"));

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
                //  dropCity.Items.Add(new ListItem("-Select-", "-1"));
                dropCity.DataTextField = "Name";
                dropCity.DataValueField = "Id";
                dropCity.DataBind();
                dropCity.Items.Insert(0, new ListItem("-- Select City --", "0"));

            }
            else
            {
                dropCity.DataSource = null;
                dropCity.DataBind();
                dropCity.Items.Insert(0, new ListItem("-- Select City--", "0"));

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
    # endregion bind methods

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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AirSuppliers.aspx", false);
    }

    void ClearControls()
    {
        hf_SupplierId.Value = "0";
        txtSupplierName.Text = "";
        dropStatus.SelectedValue = "0";
        dropServiceType.SelectedValue = "0";
        dropCountry.SelectedValue = "0";
        dropState.SelectedValue = "0";
        dropDivision.SelectedValue = "0";
        dropCity.SelectedValue = "0";
        dropConsultant.SelectedValue = "0";
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
        txtAlphaCode.Text = "";
        dropBank.SelectedValue = "0";
        txtBranchCode.Text = "";
        txtBranchName.Text = "";
        txtAccountNo.Text = "";
        dropAccountType.SelectedValue = "0";
        txtAccHolder.Text = "";
        txtGiAccountSUb.Text = "";
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
        chkEconomy.Checked = false;
        ChkFirstClass.Checked = false;
        chkBusiness.Checked = false;
        dropNoteType.SelectedValue = "0";
        txtNotes.Text = "";
    }




    protected void txtGiAccountSUb_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _BOUtility.CheckAccCodeExitorNot(txtGiAccountSUb.Text, "AirSup");
        ds.Tables.Add(dt);

        if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
        {
            lblaccnoerr.Text = "Already Exist";
            lblaccnoerr.ForeColor = System.Drawing.Color.Red;
            txtGiAccountSUb.Text = "";
        }
        else
        {

            lblaccnoerr.Text = "Available";
            lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }
}