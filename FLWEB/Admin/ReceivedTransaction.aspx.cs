using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReceivedTransaction : System.Web.UI.Page
{
    BALInvoice _objBALInvoice = new BALInvoice();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BALTransactions _objBALTransactions = new BALTransactions();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
          
            BindClienttypes();
            BindDivision();
            BindReceiptTypes();
            BindAccountTypes();
            
            txtPreparedBy.Text = Session["UserFullName"].ToString();
            gvData.DataSource = null;
            gvData.DataBind();
        }
    }
     protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ChkAllocate.Checked == false)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(),
     "Msg", "alert('Please Allocate Current Amount');", true);

        }
        else
        {

            try
            {
                int Allocatedcount = 0;
                int result = 0;
                decimal ReceiptAmountAfterpaid = 0.0M;
                decimal PreviousAmountAfterpaid = 0.0M;
                foreach (GridViewRow row in gvData.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {



                        TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
                        HiddenField hfInvId = row.FindControl("hfInvId") as HiddenField;

                        if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                        {
                            txtThisEntry.Text = "0";

                        }

                        if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                        {
                            Allocatedcount = Allocatedcount + 1;
                            TransactionMaster objTransactionMaster = new TransactionMaster();
                            objTransactionMaster.InvoiceId = Convert.ToInt32(hfInvId.Value);
                            objTransactionMaster.Divission = ddlDivision.SelectedValue;
                            objTransactionMaster.ReceiptType = ddlReceiptType.SelectedValue;
                            objTransactionMaster.AutoDepositeId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                            objTransactionMaster.AutoDepositeAccountNo = ddlAutoDepositeAccount.SelectedItem.Text;
                            objTransactionMaster.ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
                            objTransactionMaster.ClientAccountNo = ddlAccountNo.SelectedItem.Text;
                            objTransactionMaster.ClientAccountNoID = Convert.ToInt32(ddlAccountNo.SelectedValue);
                            objTransactionMaster.PayeeDetails = txtPayeeDetails.Text;


                            objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(lblPrvClientOpenAmount.Text);



                            objTransactionMaster.AllocatedAmount = txtThisEntry.Text != "" ? Convert.ToDecimal(txtThisEntry.Text) : 0;
                            objTransactionMaster.InvoiceBalanceAmount = Convert.ToDecimal(row.Cells[7].Text);
                            objTransactionMaster.Details = txtDetails.Text;
                            objTransactionMaster.Messages = txtMessage.Text;
                            objTransactionMaster.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                            objTransactionMaster.PaymentSourceRef = txtSourceRef.Text;


                            if (txtThisEntry.Text != "" || txtThisEntry.Text != "0" || txtThisEntry.Text != "0.00")
                            {
                                if (ReceiptAmountAfterpaid != 0 || ReceiptAmountAfterpaid != 0.0M)
                                    objTransactionMaster.ReceiptAmount = ReceiptAmountAfterpaid;
                                else
                                    objTransactionMaster.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                                if (PreviousAmountAfterpaid != 0 || PreviousAmountAfterpaid != 0.0M)
                                    objTransactionMaster.PrvClientOpenAmount = PreviousAmountAfterpaid;
                                else
                                    objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(lblPrvClientOpenAmount.Text);
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
                            }
                            else
                            {

                                //objTransactionMaster.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                                ////if (objTransactionMaster.PrvClientOpenAmount != 0 || objTransactionMaster.PrvClientOpenAmount != 0.0M)
                                ////{
                                ////    objTransactionMaster.ReceiptAmount = objTransactionMaster.ReceiptAmount + objTransactionMaster.PrvClientOpenAmount;
                                ////}

                                //objTransactionMaster.ReceiptAmountAfterPaid =Math.Abs( objTransactionMaster.ReceiptAmount - objTransactionMaster.AllocatedAmount);
                                ////objTransactionMaster.ReceiptAmountAfterPaid = objTransactionMaster.ReceiptAmountAfterPaid + objTransactionMaster.PrvClientOpenAmount != 0.0M ? Convert.ToDecimal(objTransactionMaster.PrvClientOpenAmount) : 0;
                                //ReceiptAmountAfterpaid = objTransactionMaster.ReceiptAmountAfterPaid;
                                //objTransactionMaster.ReceiptBalanceAmount = ReceiptAmountAfterpaid;
                            }

                            result = _objBALTransactions.ReceivedTransactionInsert(objTransactionMaster);




                        }
                    }
                }

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
                    RefAccCode = ds.Tables[1].Rows[0]["BankAcNo"].ToString();
                    RefMainAcc = ds.Tables[1].Rows[0]["MainAccCode"].ToString();
                }


                objTransaction.DebitAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                objTransaction.FmAccountNO = FmAcccode;
                objTransaction.MainAccount = FmMainAccCode;
                objTransaction.ReferenceAccountNO = RefAccCode;
                objTransaction.CreditAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "RT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                _objBALTransactions.TransactionInsert(objTransaction);



                objTransaction.CreditAmount = lblAllocatedAmount.Text != "" ? Convert.ToDecimal(lblAllocatedAmount.Text) : 0;
                objTransaction.FmAccountNO = RefAccCode;
                objTransaction.MainAccount = RefMainAcc;
                objTransaction.ReferenceAccountNO = FmAcccode;
                objTransaction.DebitAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;

                objTransaction.ReferenceType = "RT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
                _objBALTransactions.TransactionInsert(objTransaction);



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
                    objEmDepositMaster.TotalDepositAmount = Convert.ToDecimal(lblAllocatedAmount.Text);
                    objEmDepositMaster.DepositAcId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);

                    BADepositTransaction objBADepositTransaction = new BADepositTransaction();
                    int DepositInsert = objBADepositTransaction.insertDepositMaster(objEmDepositMaster);

                    if (DepositInsert > 0)
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");
                        // clearcontrols();
                        foreach (GridViewRow Row in gvData.Rows)
                        {
                            if (Row.RowType == DataControlRowType.DataRow)
                            {
                                TextBox txtThisEntry = Row.FindControl("txtThisEntry") as TextBox;
                                HiddenField hfInvId = Row.FindControl("hfInvId") as HiddenField;

                                EMDepositChild objEMDepositChild = new EMDepositChild();
                                objEMDepositChild.ReceiptId = Convert.ToInt32(result);
                                objEMDepositChild.RecieptDate = Convert.ToDateTime(txtDate.Text);
                                objEMDepositChild.ReceiptType = ddlReceiptType.SelectedItem.Text;
                                objEMDepositChild.ReciptClient = (ddlClientType.SelectedItem.Text);
                                objEMDepositChild.ReceiptAmount = txtThisEntry.Text != "" ? Convert.ToDecimal(txtThisEntry.Text) : 0; ;
                                objEMDepositChild.InvoiceId = Convert.ToInt32(hfInvId.Value);
                                objEMDepositChild.DepositAcId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                                objEMDepositChild.DepositTransMasterId = DepositInsert;
                                int childResult = objBADepositTransaction.insertDepositChild(objEMDepositChild);
                                if (childResult > 0)
                                {
                                    lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");

                                }
                                else
                                {
                                    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", " Child Deposit Was not Added Successfully");
                                }

                            }
                            //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                            //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                        }

                    }
                }
                else
                {

                }






                OpenAmountDetails objOpenAmountDetails = new OpenAmountDetails();
                objOpenAmountDetails.ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
                objOpenAmountDetails.ClientNameId = Convert.ToInt32(ddlAccountNo.SelectedValue);
                objOpenAmountDetails.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0; ;
                objOpenAmountDetails.PrvOpenAmount = lblPrvClientOpenAmount.Text != "" ? Convert.ToDecimal(lblPrvClientOpenAmount.Text) : 0;
                objOpenAmountDetails.AlocatedAmount = lblAllocatedAmount.Text != "" ? Convert.ToDecimal(lblAllocatedAmount.Text) : 0;
                objOpenAmountDetails.ReceiptOpenAmount = lblReceiptOpenAmount.Text != "" ? Convert.ToDecimal(lblReceiptOpenAmount.Text) : 0;
                objOpenAmountDetails.SourceRef = txtSourceRef.Text;
                objOpenAmountDetails.ReceiptType = ddlReceiptType.SelectedValue;
                objOpenAmountDetails.FromAccount = ddlAccountNo.SelectedValue;
                objOpenAmountDetails.ToAccount = ddlAccountNo.SelectedValue;
                objOpenAmountDetails.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                int ChildResult = _objBALTransactions.OpenAmountDetailsInsertUpdateMaster(objOpenAmountDetails);
                if (ChildResult > 0)
                {
                    Response.Redirect("ReceiptsList.aspx");
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtDate.Text = "";
        txtSourceRef.Text = "";
        txtAmount.Text = "";
        ddlDivision.SelectedIndex = 0;
        ddlReceiptType.SelectedIndex = 0;
        ddlClientType.SelectedIndex = 0;
        ddlAccountNo.SelectedIndex = 0;
        ddlAutoDepositeAccount.SelectedIndex = 0;
        txtAgeing.Text = "";
        txtPayeeDetails.Text = "";
        txtDetails.Text = "";
        lblReceiptOpenAmount.Text = "0.00";
        lblAllocatedAmount.Text = "0.00";
        lblPrvClientOpenAmount.Text = "0.00";
        lblTotalAvailable.Text = "0.00";
       
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void ddlClientType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClientType.SelectedIndex > 0)
        {
            BindAutoDepositeAccount(ddlClientType.SelectedValue);
        }
        else
        {
            BindAutoDepositeAccount("");
        }
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        lblTotalAvailable.Text = "0";
        decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
        lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(lblPrvClientOpenAmount.Text) + CurrentReceiptAmount).ToString());

        lblReceiptOpenAmount.Text = (Convert.ToDecimal(lblTotalAvailable.Text) - Convert.ToDecimal(lblAllocatedAmount.Text)).ToString();

        if (txtAmount.Text != "" || txtAmount.Text != null)
        {
            ddlAccountNo_SelectedIndexChanged(null, null);
            ChkAllocate_CheckedChanged(null, null);
        }
    }
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Clienttype = Convert.ToInt32(ddlClientType.SelectedValue);
        int ClientId = Convert.ToInt32(ddlAccountNo.SelectedValue);
        int Status = 0;
        lblAllocatedAmount.Text = "0.00";
        BindInvoiceDetailsByClientAndStatus(Clienttype, ClientId, Status);
        txtPayeeDetails.Text = ddlAccountNo.SelectedItem.Text;

    }
    #endregion Events
   

    #region GridEvents
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            decimal ReceiptTotalAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            int Allocatedcount = 0;
            decimal AllocatedTotalAmount = 0;
            decimal OpenAmount = 0;
            decimal ClientOpenAmount = 0;

            foreach (GridViewRow row in gvData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Allocatedcount = Allocatedcount + 1;
                        AllocatedTotalAmount = AllocatedTotalAmount + Convert.ToDecimal(row.Cells[3].Text);


                    }
                    else
                    {
                        ClientOpenAmount = ClientOpenAmount + Convert.ToDecimal(row.Cells[3].Text);
                    }
                }
            }

            OpenAmount = ReceiptTotalAmount - AllocatedTotalAmount;

            lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
            lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());
            lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());
            lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
            lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void txtThisEntry_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BindingAmounts();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptsList.aspx");
    }
    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    AllocateWithGridRowCheck();
    //}
    private void AllocateWithGridRowCheck()
    {
        try
        {
            foreach (GridViewRow row in gvData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;

                     txtThisEntry.Text = row.Cells[5].Text.ToString();
                    BindingAmounts();


                }
            }
        }

        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
       
    }
    protected void ChkAllocate_CheckedChanged(object sender, EventArgs e)
    {

        //BindingAmounts();
        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;

                //CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                if (ChkAllocate.Checked)
                {

                    txtThisEntry.Text = row.Cells[5].Text.ToString();
                    BindingAmounts();

                }
                else
                {
                    //chk.Checked = false;

                    txtThisEntry.Text = "0.00";
                }

            }
        }
    }

    #endregion GridEvents


 #region PrivateMethods
    private void BindInvoiceDetailsByClientAndStatus(int ClientType, int ClientId, int Status)
    {
        try
        {
            decimal ClientTotalAvailable = 0;
            decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            if (ClientType != 0 && ClientId != 0)
            {
                DataSet objDsInvLst = _objBALInvoice.GetInvoiceDetailsByClientAndStatus(ClientType, ClientId, Status);
                if (objDsInvLst.Tables[0].Rows.Count > 0)
                {
                    gvData.DataSource = objDsInvLst.Tables[0];
                    gvData.DataBind();
                }
                else
                {
                    gvData.DataSource = null;
                    gvData.DataBind();
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
                }
                if (objDsInvLst.Tables[1].Rows.Count > 0)
                {
                    lblPrvClientOpenAmount.Text = objDsInvLst.Tables[1].Rows[0]["OpenAmount"].ToString();
                    ClientTotalAvailable = CurrentReceiptAmount + Convert.ToDecimal(lblPrvClientOpenAmount.Text);
                }
                else
                {
                    lblPrvClientOpenAmount.Text = "0.00";
                    ClientTotalAvailable = CurrentReceiptAmount;
                }
                lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
                lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());

            }
            else
            {
                gvData.DataSource = null;
                gvData.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindAutoDepositeAccount(string ClientType)
    {
        try
        {
            if (ClientType == "")
            {
                ddlAccountNo.Items.Clear();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
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
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindClienttypes()
    {
        try
        {
            ddlClientType.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetClienttype();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlClientType.DataSource = ObjDsClients;
                ddlClientType.DataValueField = "Id";
                ddlClientType.DataTextField = "Name";
                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("Select Client", "0"));
            }
            else
            {
                ddlClientType.Items.Insert(0, new ListItem("Select Client", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindDivision()
    {
        try
        {
            ddlDivision.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetDivisions();
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindReceiptTypes()
    {
        try
        {
            ddlReceiptType.Items.Clear();
            int ReceiptId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetReceiptTypes(ReceiptId);
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindAccountTypes()
    {
        try
        {
            ddlAutoDepositeAccount.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    public void ReceivedTransactionClear()
    {

        txtDate.Text = "";
        txtSourceRef.Text = "";
        txtAmount.Text = "";
        ddlDivision.Items.Clear();
        // ddlDevission.Items.Clear();
        ddlReceiptType.Items.Clear();
        ddlClientType.Items.Clear();
        ddlAccountNo.Items.Clear();
        ddlAutoDepositeAccount.Items.Clear();
        txtAgeing.Text = "";
        txtPayeeDetails.Text = "";
        txtDetails.Text = "";
        lblAllocatedAmount.Text = "";
        lblReceiptOpenAmount.Text = "";
        lblPrvClientOpenAmount.Text = "";
        lblTotalAvailable.Text = "";
        lblOpenItemAmounFromclient.Text = "";

    }

    private void  BindingAmounts()
    {
        try
        {

        decimal ReceiptTotalAmount = lblTotalAvailable.Text != "" ? Convert.ToDecimal(lblTotalAvailable.Text) : 0;//txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
        int Allocatedcount = 0;
        decimal AllocatedTotalAmount = 0;
        decimal OpenAmount = 0;

        decimal ClientOpenAmount = 0;
        decimal InvoiceOpenAmount = 0;
        decimal ThisEntryAmount = 0;
        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
                HiddenField hfAllocatedAmount = row.FindControl("hfAllocatedAmount") as HiddenField;
                CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                //if (chk.Checked || ChkAllocate.Checked)
                //{

                //    txtThisEntry.Text = row.Cells[6].Text.ToString();


                //}
                //else
                //{
                //    txtThisEntry.Text = "0.00";
                //}
               
             
                   // txtThisEntry.Text = row.Cells[6].Text.ToString();

                if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                {
                    txtThisEntry.Text = "0";

                }

                //added receipts
                if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                {

                    ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
                    InvoiceOpenAmount = Convert.ToDecimal(row.Cells[5].Text);

                    OpenAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
                    OpenAmount = OpenAmount + Convert.ToDecimal(hfAllocatedAmount.Value);

                    if (ThisEntryAmount > InvoiceOpenAmount)
                    {
                        txtThisEntry.Text = "";

                    }
                    else if (ThisEntryAmount > OpenAmount)
                    {
                        txtThisEntry.Text = "";
                    }
                    else
                    {
                        hfAllocatedAmount.Value = txtThisEntry.Text;

                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
                        Allocatedcount = Allocatedcount + 1;
                        AllocatedTotalAmount = AllocatedTotalAmount + ThisEntryAmount;

                        row.Cells[7].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());
                        ChkAllocate.Checked = true;
                    }

                
                }

            }
        }

        OpenAmount = ReceiptTotalAmount - AllocatedTotalAmount;

        lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
        lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());

        lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());

        lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
        lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

   
}
 #endregion PrivateMethods