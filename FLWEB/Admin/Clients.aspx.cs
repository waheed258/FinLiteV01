using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;


public partial class Admin_Clients : System.Web.UI.Page
{
    EMClients objClients = new EMClients();
    BAClients objBAClients = new BAClients();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    public string FileLogo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindClientType();
            BindGroup();
            BindDivisions();
            BindStatement();
            BindOpenItemLoad();
            BindCreditTerms();
            BindLimitTerms();
            BindDepartment();
            BindCalCulAir();
            BindCalCulLand();
            BindConsultant();
            BindNotes();
            BindImportedTicket();
            BindDirectSettleTrans();
            BindClientActions();
            BindMainAccounts();
            BindGroupMaster();

            if (!string.IsNullOrEmpty(Request.QueryString["ClientId"]))
            {
                int ClientId = Convert.ToInt32(Request.QueryString["ClientId"].ToString());
                GetClientDetails(ClientId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateClients();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientsList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Clients.aspx");
    }

    #region PrivateMethods
    private void InsertUpdateClients()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.getMainAccounts();

            objClients.ClientId = Convert.ToInt32(hf_ClientId.Value);
            objClients.ClientType = ddlClientType.SelectedValue;
            objClients.ClientTypeAccCode = txtAccCode.Text + ddlClientType.SelectedItem.ToString() + txtAccountNo.Text;
            objClients.ClientAccCode = txtAccCode.Text;
            objClients.ClientAccountNo = txtAccountNo.Text;
            objClients.ClientName = txtClientName.Text;
            objClients.IsActive = Convert.ToInt32(chkIsActive.Checked);
            objClients.ClientGroup = Convert.ToInt32(ddlGroup.SelectedValue);
            objClients.ClientVatRegNo = txtVatRegNo.Text;
            objClients.ClientNoVatNo = Convert.ToInt32(chkNoVatNo.Checked);
            objClients.ClientConsultant = ddlConsultant.SelectedIndex;
            objClients.ClientDivision = ddlDivision.SelectedIndex;
            objClients.ClientDepartment = ddlDepartment.SelectedIndex;
            objClients.ClientTelephone = txtTelephoneNo.Text;
            objClients.ClientFax = txtFaxNo.Text;
            objClients.ClientEmail = txtEmail.Text;
            objClients.ClientContactPerson = txtContactNo.Text;
            objClients.ClientPostalAddress = txtPostalAddress.Text;
            objClients.ClientPhysicalAddress = txtPhysicalAddress.Text;
            objClients.YearEnd = ddlYearEnd.SelectedValue;
            string filepath = string.Empty;
            if (hfImageLogo.Value != "")
            {
                if (fuLogo.HasFile)
                    lblLogo.Text = fuLogo.FileName;
                else
                    filepath = hfImageLogo.Value.ToString();
            }
            else
                filepath = GetFile(fuLogo);

            objClients.Logo = lblLogo.Text;
            objClients.LogoPath = filepath;

            if (!string.IsNullOrEmpty(txtHtmlWidth.Text))
            {
                objClients.HtmlWidth = Convert.ToInt32(txtHtmlWidth.Text);
            }
            if (!string.IsNullOrEmpty(txtHtmlHeight.Text))
            {
                objClients.HtmlHeight = Convert.ToInt32(txtHtmlHeight.Text);
            }

            objClients.Align = ddlAlign.SelectedValue;
            objClients.IsDisableAutoPrintManual = Convert.ToInt32(chkAutoPrintmanual.Checked);
            objClients.IsDisableAutoPrintTicket = Convert.ToInt32(chkAutoPrintTicket.Checked);

            if (!string.IsNullOrEmpty(txtDefaultOrderNo.Text))
            {
                objClients.DefaultOrderNo = Convert.ToInt32(txtDefaultOrderNo.Text);
            }


            objClients.IsForceOrderNo = Convert.ToInt32(chkForceOrderNo.Checked);
            objClients.IsForceExternalVoucher = Convert.ToInt32(chkForceExternalVoucher.Checked);
            objClients.ClientDuplicateOrderNo = ddlDuplicateOrderNo.SelectedIndex;
            objClients.ActionIsNoServiceFee = ddlNoServiceFees.SelectedIndex;
            objClients.ClientOpenItemLoad = ddlOpenItemLoad.SelectedIndex;
            objClients.ClientBroughtFwdBreakDwn = ddlFwdBreDwn.SelectedIndex;
            objClients.ClientLineItemBreakDwn = ddlLineItemBreakDwn.SelectedIndex;
            objClients.ClientDbOrCd = ddlDbOrCd.SelectedIndex;
            objClients.ClientSuppressNilVal = ddlSuppressNilVal.SelectedIndex;
            objClients.ClientTotalCharge = ddlTotalCharge.SelectedIndex;
            objClients.ClientCreditCard = ddlCreditCard.SelectedIndex;
            objClients.ClientTransactionRef = ddlTransactionRef.SelectedIndex;
            objClients.ClientAllocPaidItems = ddlAllocPaidItems.SelectedIndex;
            objClients.ClientCustmDetail = txtCustmDetail.Text;
            objClients.ClientExcludeFmPrint = Convert.ToInt32(chkExcludeFmPrint.Checked);

            if (!string.IsNullOrEmpty(txtCreditLimit.Text))
            {
                objClients.ClientCreditLimit = Convert.ToDecimal(txtCreditLimit.Text);
            }


            objClients.ClientLimitAction = ddlLimitAction.SelectedIndex;

            if (!string.IsNullOrEmpty(txtMaxInvoiceVal.Text))
            {
                objClients.ClientMaxInvoiceVal = Convert.ToDecimal(txtMaxInvoiceVal.Text);
            }


            objClients.ClientCreditTerms = ddlCreditTerms.SelectedIndex;
            objClients.ClientTermsAction = ddlTermsAction.SelectedIndex;
            objClients.IsForce = Convert.ToInt32(chkIsForce.Checked);
            objClients.ClientCalAir = ddlCalAir.SelectedIndex;
            objClients.ClientCalLand = ddlCalLand.SelectedIndex;

            objClients.CContactKey = txtContactKey.Text;
            objClients.CContactName = txtContactName.Text;
            objClients.CPosition = txtPosition.Text;
            objClients.CTelephone = txtTelephone.Text;
            objClients.CEmailAddress = txtEmailAddress.Text;
            objClients.CAutoMail = Convert.ToInt32(chkAutomail.Checked);
            objClients.CCellNo = txtConCellNo.Text;
            objClients.CFax = txtConFaxNo.Text;
            objClients.CDeactivate = Convert.ToInt32(chkDeActivate.Checked);

            objClients.ClientDirectSettleTrans = ddlDirectSettleTrans.SelectedIndex;
            objClients.ClientCCForCashTrans = ddlCCForCashTrans.SelectedIndex;
            objClients.IsTicketOrLandCreditCard = Convert.ToInt32(chkTicketOrLandCreditCard.Checked);
            objClients.CreditCardImportedTickets = ddlCardImportedTickets.SelectedIndex;
            objClients.ClientNotes = ddlNoteType.SelectedValue;
            objClients.ClientMessage = txtNotes.Text;


            //ChartedAccounts
            if (cmdSubmit.Text == "Submit")
            {
                objClients.ChartedAccName = txtClientName.Text;
                // objClients.ChartedMasterAccName = 2;
                objClients.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["MainAccId"].ToString());
                objClients.Type = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[3].Rows[0]["AcType"].ToString();
                objClients.AccCode = txtAccCode.Text + ddlClientType.SelectedItem.ToString() + txtAccountNo.Text;
                objClients.DepartmentId = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["DepartmentId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["DepartmentId"].ToString());
                objClients.BaseCurrency = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["BaseCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["BaseCurrency"].ToString());
                objClients.TranCurrency = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["TranCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["TranCurrency"].ToString());
                objClients.CategoryId = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["CategoryId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["CategoryId"].ToString());
                objClients.RefType = "Client";
            }
            if (cmdSubmit.Text == "Update")
            {
                objClients.ChartedAccName = txtClientName.Text;
                objClients.AccCode = txtAccCode.Text + ddlClientType.SelectedItem.ToString() + txtAccountNo.Text;
                objClients.Type = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[3].Rows[0]["AcType"].ToString();
                objClients.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[3].Rows[0]["MainAccId"].ToString());
                objClients.Type = string.IsNullOrEmpty(ds.Tables[3].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[3].Rows[0]["AcType"].ToString();
                objClients.RefType = "Client";
                objClients.RefId = hf_ClientId.Value;
            }


            int Result = objBAClients.InsUpdClients(objClients);

            if (cmdSubmit.Text == "Submit")
            {
                objClients.RefId = Result.ToString();
            }
            if (cmdSubmit.Text == "Submit")
            {
                int ResultAcc = objBAClients.InsUpdChartAccounts(objClients);
            }
            if (cmdSubmit.Text == "Update")
            {
                int AccountUpdate = objBAClients.UpdateChartAccounts(objClients);
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
                    CheckBox rchkDeActivate = row.FindControl("chkDeActivate") as CheckBox;

                    HiddenField rhfContactId = (HiddenField)row.FindControl("hfContactId");

                    objClients.ContactId = Convert.ToInt32(rhfContactId.Value);
                    objClients.ContactKey = rtxtContactKey.Text;
                    objClients.ContactName = rtxtContactName.Text;
                    objClients.Position = rtxtPosition.Text;
                    objClients.Telephone = rtxtTelephone.Text;
                    objClients.EmailAddress = rtxtEmailAddress.Text;
                    objClients.AutoMail = Convert.ToBoolean(rchkAutomail.Checked);
                    objClients.ConCellNo = rtxtConCellNo.Text;
                    objClients.ConFax = rtxtConFaxNo.Text;
                    objClients.ConDeactivate = Convert.ToBoolean(rchkDeActivate.Checked);
                    objClients.ClientId = Result;
                    objClients.UserCategory = "Client";

                    int rResult = objBAClients.InsertUpdContact(objClients);

                    if (rResult > 0)
                    {
                        lblMsg.Text = " Added Successfully";
                    }
                    else
                    {
                        lblMsg.Text = "Not Successfully please tryagain";
                    }
                }


                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Client Details created Successfully");
                clearcontrols();
                Response.Redirect("ClientsList.aspx");
            }

            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Client Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetClientDetails(int ClientId)
    {
        try
        {
            objClients.ClientId = ClientId;
            DataSet ds = new DataSet();
            ds = objBAClients.GetClients(ClientId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ClientId.Value = ds.Tables[0].Rows[0]["ClientId"].ToString();
                ddlClientType.SelectedIndex = ddlClientType.Items.IndexOf(ddlClientType.Items.FindByValue(ds.Tables[0].Rows[0]["ClientType"].ToString()));
                ddlClientType.Enabled = false;
                txtAccountNo.Text = ds.Tables[0].Rows[0]["ClientAccountNo"].ToString();
                txtAccountNo.Enabled = false;
                txtClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();
                chkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ClientGroup"].ToString()));
                txtVatRegNo.Text = ds.Tables[0].Rows[0]["ClientVatRegNo"].ToString();
                chkNoVatNo.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ClientNoVatNo"]);
                ddlConsultant.SelectedIndex = ddlConsultant.Items.IndexOf(ddlConsultant.Items.FindByValue(ds.Tables[0].Rows[0]["ClientConsultant"].ToString()));
                ddlDivision.SelectedIndex = ddlDivision.Items.IndexOf(ddlDivision.Items.FindByValue(ds.Tables[0].Rows[0]["ClientDivision"].ToString()));
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["ClientDepartment"].ToString()));
                txtTelephoneNo.Text = ds.Tables[0].Rows[0]["ClientTelephone"].ToString();
                txtFaxNo.Text = ds.Tables[0].Rows[0]["ClientFax"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ClientContactPerson"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["ClientEmail"].ToString();
                txtPostalAddress.Text = ds.Tables[0].Rows[0]["ClientPostalAddress"].ToString();
                txtPhysicalAddress.Text = ds.Tables[0].Rows[0]["ClientPhysicalAddress"].ToString();
                ddlYearEnd.SelectedIndex = ddlYearEnd.Items.IndexOf(ddlYearEnd.Items.FindByValue(ds.Tables[0].Rows[0]["YearEnd"].ToString()));

                lblLogo.Text = ds.Tables[0].Rows[0]["Logo"].ToString();
                hfImageLogo.Value = ds.Tables[0].Rows[0]["LogoPath"].ToString();
                FileLogo = ds.Tables[0].Rows[0]["LogoPath"].ToString();
                logoview.Attributes["href"] = "../Admin/ClientsLogo/" + FileLogo;


                txtHtmlWidth.Text = ds.Tables[0].Rows[0]["HtmlWidth"].ToString();
                txtHtmlHeight.Text = ds.Tables[0].Rows[0]["HtmlHeight"].ToString();
                ddlAlign.SelectedIndex = ddlAlign.Items.IndexOf(ddlAlign.Items.FindByValue(ds.Tables[0].Rows[0]["Align"].ToString()));
                chkAutoPrintmanual.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDisableAutoPrintManual"]);
                chkAutoPrintTicket.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDisableAutoPrintTicket"]);
                txtDefaultOrderNo.Text = ds.Tables[0].Rows[0]["DefaultOrderNo"].ToString();
                chkForceOrderNo.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsForceOrderNo"]);
                chkForceExternalVoucher.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsForceExternalVoucher"]);
                ddlDuplicateOrderNo.SelectedIndex = ddlDuplicateOrderNo.Items.IndexOf(ddlDuplicateOrderNo.Items.FindByValue(ds.Tables[0].Rows[0]["ClientDuplicateOrderNo"].ToString()));
                ddlNoServiceFees.SelectedIndex = ddlNoServiceFees.Items.IndexOf(ddlNoServiceFees.Items.FindByValue(ds.Tables[0].Rows[0]["ActionIsNoServiceFee"].ToString()));
                ddlOpenItemLoad.SelectedIndex = ddlOpenItemLoad.Items.IndexOf(ddlOpenItemLoad.Items.FindByValue(ds.Tables[0].Rows[0]["ClientOpenItemLoad"].ToString()));
                ddlFwdBreDwn.SelectedIndex = ddlFwdBreDwn.Items.IndexOf(ddlFwdBreDwn.Items.FindByValue(ds.Tables[0].Rows[0]["ClientBroughtFwdBreakDwn"].ToString()));
                ddlLineItemBreakDwn.SelectedIndex = ddlLineItemBreakDwn.Items.IndexOf(ddlLineItemBreakDwn.Items.FindByValue(ds.Tables[0].Rows[0]["ClientLineItemBreakDwn"].ToString()));
                ddlDbOrCd.SelectedIndex = ddlDbOrCd.Items.IndexOf(ddlDbOrCd.Items.FindByValue(ds.Tables[0].Rows[0]["ClientDbOrCd"].ToString()));
                ddlSuppressNilVal.SelectedIndex = ddlSuppressNilVal.Items.IndexOf(ddlSuppressNilVal.Items.FindByValue(ds.Tables[0].Rows[0]["ClientSuppressNilVal"].ToString()));
                ddlTotalCharge.SelectedIndex = ddlTotalCharge.Items.IndexOf(ddlTotalCharge.Items.FindByValue(ds.Tables[0].Rows[0]["ClientTotalCharge"].ToString()));
                ddlCreditCard.SelectedIndex = ddlCreditCard.Items.IndexOf(ddlCreditCard.Items.FindByValue(ds.Tables[0].Rows[0]["ClientCreditCard"].ToString()));
                ddlTransactionRef.SelectedIndex = ddlTransactionRef.Items.IndexOf(ddlTransactionRef.Items.FindByValue(ds.Tables[0].Rows[0]["ClientTransactionRef"].ToString()));
                ddlAllocPaidItems.SelectedIndex = ddlAllocPaidItems.Items.IndexOf(ddlAllocPaidItems.Items.FindByValue(ds.Tables[0].Rows[0]["ClientAllocPaidItems"].ToString()));
                txtCustmDetail.Text = ds.Tables[0].Rows[0]["ClientCustmDetail"].ToString();
                chkExcludeFmPrint.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ClientExcludeFmPrint"]);


                txtCreditLimit.Text = ds.Tables[0].Rows[0]["ClientCreditLimit"].ToString();
                ddlLimitAction.SelectedIndex = ddlLimitAction.Items.IndexOf(ddlLimitAction.Items.FindByValue(ds.Tables[0].Rows[0]["ClientLimitAction"].ToString()));
                txtMaxInvoiceVal.Text = ds.Tables[0].Rows[0]["ClientMaxInvoiceVal"].ToString();
                ddlCreditTerms.SelectedIndex = ddlCreditTerms.Items.IndexOf(ddlCreditTerms.Items.FindByValue(ds.Tables[0].Rows[0]["ClientCreditTerms"].ToString()));
                ddlTermsAction.SelectedIndex = ddlTermsAction.Items.IndexOf(ddlTermsAction.Items.FindByValue(ds.Tables[0].Rows[0]["ClientTermsAction"].ToString()));
                chkIsForce.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsForce"]);
                ddlCalAir.SelectedIndex = ddlCalAir.Items.IndexOf(ddlCalAir.Items.FindByValue(ds.Tables[0].Rows[0]["ClientCalAir"].ToString()));
                ddlCalLand.SelectedIndex = ddlCalLand.Items.IndexOf(ddlCalLand.Items.FindByValue(ds.Tables[0].Rows[0]["ClientCalLand"].ToString()));

                txtContactKey.Text = ds.Tables[0].Rows[0]["CContactKey"].ToString();
                txtContactName.Text = ds.Tables[0].Rows[0]["CContactName"].ToString();
                txtPosition.Text = ds.Tables[0].Rows[0]["CPosition"].ToString();
                txtTelephone.Text = ds.Tables[0].Rows[0]["CTelephone"].ToString();
                txtEmailAddress.Text = ds.Tables[0].Rows[0]["CEmailAddress"].ToString();
                chkAutomail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CAutoMail"].ToString());
                txtConCellNo.Text = ds.Tables[0].Rows[0]["CCellNo"].ToString();
                txtConFaxNo.Text = ds.Tables[0].Rows[0]["CFax"].ToString();
                chkDeActivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CDeactivate"]);

                ddlDirectSettleTrans.SelectedIndex = ddlDirectSettleTrans.Items.IndexOf(ddlDirectSettleTrans.Items.FindByValue(ds.Tables[0].Rows[0]["ClientDirectSettleTrans"].ToString()));
                ddlCCForCashTrans.SelectedIndex = ddlCCForCashTrans.Items.IndexOf(ddlCCForCashTrans.Items.FindByValue(ds.Tables[0].Rows[0]["ClientCCForCashTrans"].ToString()));
                chkTicketOrLandCreditCard.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTicketOrLandCreditCard"]);
                ddlCardImportedTickets.SelectedIndex = ddlCardImportedTickets.Items.IndexOf(ddlCardImportedTickets.Items.FindByValue(ds.Tables[0].Rows[0]["CreditCardImportedTickets"].ToString()));
                ddlNoteType.SelectedIndex = ddlNoteType.Items.IndexOf(ddlNoteType.Items.FindByValue(ds.Tables[0].Rows[0]["ClientNotes"].ToString()));
                txtNotes.Text = ds.Tables[0].Rows[0]["ClientMessage"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {


                    BindRepeterContactDetails(ds.Tables[1], rptContactDetails);

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private string GetFile(FileUpload FUName)
    {
        string fileName = string.Empty;
        if (FUName.HasFile)
        {
            string strPath = System.IO.Path.GetExtension(FUName.PostedFile.FileName);
            string date = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            fileName = date + strPath;
            FUName.SaveAs(Server.MapPath("../Admin/ClientsLogo/" + fileName));
            lblLogo.Text = fuLogo.FileName;
        }
        return fileName;
    }


    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_ClientId.Value = "0";
        ddlClientType.SelectedIndex = 0;
        txtAccountNo.Text = "";
        txtClientName.Text = "";
        chkIsActive.Checked = false;
        ddlGroup.SelectedIndex = 0;
        txtVatRegNo.Text = "";
        chkNoVatNo.Checked = false;
        ddlConsultant.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtTelephoneNo.Text = "";
        txtFaxNo.Text = "";
        txtContactNo.Text = "";
        txtEmail.Text = "";
        txtPostalAddress.Text = "";
        txtPhysicalAddress.Text = "";
        ddlYearEnd.SelectedIndex = 0;
        lblLogo.Text = "";
        txtHtmlWidth.Text = "";
        txtHtmlHeight.Text = "";
        ddlAlign.SelectedIndex = 0;
        chkAutoPrintmanual.Checked = false;
        chkAutoPrintTicket.Checked = false;
        txtDefaultOrderNo.Text = "";
        chkForceOrderNo.Checked = false;
        chkForceExternalVoucher.Checked = false;
        ddlDuplicateOrderNo.SelectedIndex = 0;
        ddlNoServiceFees.SelectedIndex = 0;
        ddlOpenItemLoad.SelectedIndex = 0;
        ddlFwdBreDwn.SelectedIndex = 0;
        ddlLineItemBreakDwn.SelectedIndex = 0;
        ddlDbOrCd.SelectedIndex = 0;
        ddlSuppressNilVal.SelectedIndex = 0;
        ddlTotalCharge.SelectedIndex = 0;
        ddlCreditCard.SelectedIndex = 0;
        ddlTransactionRef.SelectedIndex = 0;
        ddlAllocPaidItems.SelectedIndex = 0;
        txtCustmDetail.Text = "";
        chkExcludeFmPrint.Checked = false;
        txtCreditLimit.Text = "";
        ddlLimitAction.SelectedIndex = 0;
        txtMaxInvoiceVal.Text = "";
        ddlCreditTerms.SelectedIndex = 0;
        ddlTermsAction.SelectedIndex = 0;
        chkIsForce.Checked = false;
        ddlCalAir.SelectedIndex = 0;
        ddlCalLand.SelectedIndex = 0;
        txtContactKey.Text = "";
        chkDeActivate.Checked = false;
        txtContactName.Text = "";
        txtPosition.Text = "";
        txtTelephone.Text = "";
        txtConFaxNo.Text = "";
        txtConCellNo.Text = "";
        txtEmailAddress.Text = "";
        chkAutomail.Checked = false;
        ddlDirectSettleTrans.SelectedIndex = 0;
        ddlCCForCashTrans.SelectedIndex = 0;
        chkTicketOrLandCreditCard.Checked = false;
        ddlCardImportedTickets.SelectedIndex = 0;
        ddlNoteType.SelectedIndex = 0;
        txtNotes.Text = "";
    }

    #region PublicMethods
    public void BindClientType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetClienttype();
            if (ds.Tables.Count > 0)
            {

                ddlClientType.DataSource = ds.Tables[0];
                ddlClientType.DataTextField = "Code";
                ddlClientType.DataValueField = "Id";
                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlClientType.DataSource = null;
                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindGroup()
    {

        try
        {
            DataSet ds = new DataSet();
            string Type = "Client";
            ds = _objBOUtiltiy.GetGroup(Type);
            if (ds.Tables.Count > 0)
            {

                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
                //  ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlGroup.DataSource = null;
                ddlGroup.DataBind();
                //   ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindConsultant()
    {

        try
        {
            int ConsultantId = 0;
            BAConsultant objBAConsultant = new BAConsultant();
            DataSet ds = new DataSet();
            ds = objBAConsultant.GetConsultant(ConsultantId);
            if (ds.Tables.Count > 0)
            {

                ddlConsultant.DataSource = ds.Tables[0];
                ddlConsultant.DataTextField = "Name";
                ddlConsultant.DataValueField = "ConsultantId";
                ddlConsultant.DataBind();
                ddlConsultant.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlConsultant.DataSource = null;
                ddlConsultant.DataBind();
                ddlConsultant.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindDivisions()
    {
        try
        {

            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetDivisions();
            if (ds.Tables.Count > 0)
            {

                ddlDivision.DataSource = ds.Tables[0];
                ddlDivision.DataTextField = "DivName";
                ddlDivision.DataValueField = "DivisionId";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlDivision.DataSource = null;
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindOpenItemLoad()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetItemLoadingType();
            if (ds.Tables.Count > 0)
            {
                ddlOpenItemLoad.DataSource = ds.Tables[0];
                ddlOpenItemLoad.DataTextField = "ItmName";
                ddlOpenItemLoad.DataValueField = "ItemLoadId";
                ddlOpenItemLoad.DataBind();
                ddlOpenItemLoad.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlOpenItemLoad.DataSource = null;
                ddlOpenItemLoad.DataBind();
                ddlOpenItemLoad.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindStatement()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetStatement();
            if (ds.Tables.Count > 0)
            {

                ddlFwdBreDwn.DataSource = ds.Tables[0];
                ddlFwdBreDwn.DataTextField = "StatementName";
                ddlFwdBreDwn.DataValueField = "StatementId";
                ddlFwdBreDwn.DataBind();
                ddlFwdBreDwn.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlLineItemBreakDwn.DataSource = ds.Tables[0];
                ddlLineItemBreakDwn.DataTextField = "StatementName";
                ddlLineItemBreakDwn.DataValueField = "StatementId";
                ddlLineItemBreakDwn.DataBind();
                ddlLineItemBreakDwn.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlDbOrCd.DataSource = ds.Tables[0];
                ddlDbOrCd.DataTextField = "StatementName";
                ddlDbOrCd.DataValueField = "StatementId";
                ddlDbOrCd.DataBind();
                ddlDbOrCd.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlSuppressNilVal.DataSource = ds.Tables[0];
                ddlSuppressNilVal.DataTextField = "StatementName";
                ddlSuppressNilVal.DataValueField = "StatementId";
                ddlSuppressNilVal.DataBind();
                ddlSuppressNilVal.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlTotalCharge.DataSource = ds.Tables[0];
                ddlTotalCharge.DataTextField = "StatementName";
                ddlTotalCharge.DataValueField = "StatementId";
                ddlTotalCharge.DataBind();
                ddlTotalCharge.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlCreditCard.DataSource = ds.Tables[0];
                ddlCreditCard.DataTextField = "StatementName";
                ddlCreditCard.DataValueField = "StatementId";
                ddlCreditCard.DataBind();
                ddlCreditCard.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlTransactionRef.DataSource = ds.Tables[0];
                ddlTransactionRef.DataTextField = "StatementName";
                ddlTransactionRef.DataValueField = "StatementId";
                ddlTransactionRef.DataBind();
                ddlTransactionRef.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlAllocPaidItems.DataSource = ds.Tables[0];
                ddlAllocPaidItems.DataTextField = "StatementName";
                ddlAllocPaidItems.DataValueField = "StatementId";
                ddlAllocPaidItems.DataBind();
                ddlAllocPaidItems.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindCreditTerms()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetCreditTerms();
            if (ds.Tables.Count > 0)
            {
                ddlCreditTerms.DataSource = ds.Tables[0];
                ddlCreditTerms.DataTextField = "CreditTermName";
                ddlCreditTerms.DataValueField = "CreditTermId";
                ddlCreditTerms.DataBind();
                ddlCreditTerms.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlCreditTerms.DataSource = null;
                ddlCreditTerms.DataBind();
                ddlCreditTerms.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindLimitTerms()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetLimitTerms();
            if (ds.Tables.Count > 0)
            {
                ddlLimitAction.DataSource = ds.Tables[0];
                ddlLimitAction.DataTextField = "ActionName";
                ddlLimitAction.DataValueField = "ActionId";
                ddlLimitAction.DataBind();
                ddlLimitAction.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlTermsAction.DataSource = ds.Tables[0];
                ddlTermsAction.DataTextField = "ActionName";
                ddlTermsAction.DataValueField = "ActionId";
                ddlTermsAction.DataBind();
                ddlTermsAction.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    public void BindDepartment()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetDepartment();
            if (ds.Tables.Count > 0)
            {
                ddlDepartment.DataSource = ds.Tables[0];
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "Deptid";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindCalCulAir()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetCalCulAir();
            if (ds.Tables.Count > 0)
            {
                ddlCalAir.DataSource = ds.Tables[0];
                ddlCalAir.DataTextField = "AirName";
                ddlCalAir.DataValueField = "AirId";
                ddlCalAir.DataBind();
                ddlCalAir.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlCalAir.DataSource = null;
                ddlCalAir.DataBind();
                ddlCalAir.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindCalCulLand()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetCalCulLand();
            if (ds.Tables.Count > 0)
            {
                ddlCalLand.DataSource = ds.Tables[0];
                ddlCalLand.DataTextField = "LandName";
                ddlCalLand.DataValueField = "LandId";
                ddlCalLand.DataBind();
                ddlCalLand.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlCalLand.DataSource = null;
                ddlCalLand.DataBind();
                ddlCalLand.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindNotes()
    {
        try
        {

            BAContactNote objBANote = new BAContactNote();

            int NotePadId = 0;
            DataSet ds = objBANote.GetContactNote(NotePadId);
            if (ds.Tables.Count > 0)
            {
                ddlNoteType.DataSource = ds.Tables[0];
                ddlNoteType.DataTextField = "NpName";
                ddlNoteType.DataValueField = "NotePadId";
                ddlNoteType.DataBind();
                ddlNoteType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlNoteType.DataSource = null;
                ddlNoteType.DataBind();
                ddlNoteType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindImportedTicket()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetImportedTicket();
            if (ds.Tables.Count > 0)
            {
                ddlCCForCashTrans.DataSource = ds.Tables[0];
                ddlCCForCashTrans.DataTextField = "Name";
                ddlCCForCashTrans.DataValueField = "Id";
                ddlCCForCashTrans.DataBind();
                ddlCCForCashTrans.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                ddlCardImportedTickets.DataSource = ds.Tables[0];
                ddlCardImportedTickets.DataTextField = "Name";
                ddlCardImportedTickets.DataValueField = "Id";
                ddlCardImportedTickets.DataBind();
                ddlCardImportedTickets.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindDirectSettleTrans()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetDirectSettleTrans();
            if (ds.Tables.Count > 0)
            {
                ddlDirectSettleTrans.DataSource = ds.Tables[0];
                ddlDirectSettleTrans.DataTextField = "Name";
                ddlDirectSettleTrans.DataValueField = "Id";
                ddlDirectSettleTrans.DataBind();
                ddlDirectSettleTrans.Items.Insert(0, new ListItem("-- Please Select --", "0"));


            }
            else
            {
                ddlDirectSettleTrans.DataSource = null;
                ddlDirectSettleTrans.DataBind();
                ddlDirectSettleTrans.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindClientActions()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetClientActions();
            if (ds.Tables.Count > 0)
            {
                ddlDuplicateOrderNo.DataSource = ds.Tables[0];
                ddlDuplicateOrderNo.DataTextField = "Name";
                ddlDuplicateOrderNo.DataValueField = "Id";
                ddlDuplicateOrderNo.DataBind();
                ddlDuplicateOrderNo.Items.Insert(0, new ListItem("-- Please Select --", "0"));

                ddlNoServiceFees.DataSource = ds.Tables[0];
                ddlNoServiceFees.DataTextField = "Name";
                ddlNoServiceFees.DataValueField = "Id";
                ddlNoServiceFees.DataBind();
                ddlNoServiceFees.Items.Insert(0, new ListItem("-- Please Select --", "0"));


            }


        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindMainAccounts()
    {
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = _objBOUtiltiy.getMainAccounts();
            dt = ds.Tables[1];
            if (ds.Tables[1].Rows.Count > 0)
            {
                txtAccCode.Text = ds.Tables[3].Rows[0]["MainAccCode"].ToString();
                //txtGiAccount.Text = ds.Tables[1].Rows[0]["MainAccCode"].ToString();
                //txt1.Text = objDS.Tables[0].Rows[0]["ColumnName"].ToString();
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
    public void BindGroupMaster()
    {

        try
        {
            DataSet ds = new DataSet();
            int Id = 0;
            ds = _objBOUtiltiy.GetGroupMaster(Id);
            if (ds.Tables.Count > 0)
            {

                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlGroup.DataSource = null;
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PublicMethods





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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
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
                TextBox txtConCellNo = row.FindControl("txtConCellNo") as TextBox;
                TextBox txtConFaxNo = row.FindControl("txtConFaxNo") as TextBox;
                CheckBox chkDeActivate = row.FindControl("chkDeActivate") as CheckBox;

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
                drexist["ConDeactivate"] = chkDeActivate.Checked;
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void rptContactDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
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
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
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
            CheckBox chkDeActivate = (CheckBox)row.FindControl("chkDeActivate");

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
            drexist["ConDeactivate"] = chkDeActivate.Checked;
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
        try
        {
            int Result = objBAClients.DeleteContact(ContactId);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    # endregion repetors

    protected void txtAccountNo_TextChanged(object sender, EventArgs e)
    {
        AccuntNumberCheck();
    }


    protected void ddlClientType_SelectedIndexChanged(object sender, EventArgs e)
    {
        AccuntNumberCheck();
    }
    private void AccuntNumberCheck()
    {

        string AccCode = txtAccCode.Text + ddlClientType.SelectedItem.Text + txtAccountNo.Text;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        ds = _objBOUtiltiy.CheckAccCodeExitorNot(AccCode, "Client");

        ds.Tables.Add(dt);


        if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
        {
            lblaccnoerr.Text = "Already Exist";
            lblaccnoerr.ForeColor = System.Drawing.Color.Red;
            txtAccountNo.Text = "";
        }
        else
        {
            lblaccnoerr.Text = "Available";
            lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }
}