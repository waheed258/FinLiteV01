using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_MultipleSupplierPayment : System.Web.UI.Page
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
            txtPreparedBy.Text = Session["UserFullName"].ToString();
            gvData.DataSource = null;
            gvData.DataBind();
            BindPaymentTypes();
            BindDivision();
            BindAccountTypes();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {



                    TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
                    HiddenField hfTicketId = row.FindControl("hfTicketId") as HiddenField;
                    HiddenField hfInvId = row.FindControl("hfInvId") as HiddenField;



                    if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                    {
                        txtThisEntry.Text = "0";

                    }

                    if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                    {
                        PaymentTransaction objTransactionMaster = new PaymentTransaction();

                        objTransactionMaster.TicketId = Convert.ToInt32(hfTicketId.Value);
                        objTransactionMaster.InvoiceId = Convert.ToInt32(hfInvId.Value);
                        objTransactionMaster.Divission = ddlDivision.SelectedValue;
                        objTransactionMaster.PaymentType = ddlPaymentType.SelectedValue;
                        objTransactionMaster.AutoDepositeId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                        objTransactionMaster.AutoDepositeAccountNo = ddlAutoDepositeAccount.SelectedItem.Text;
                        objTransactionMaster.ClientSupplTypeId = Convert.ToInt32(ddlAccType.SelectedValue);
                        if (Convert.ToInt32(ddlAccountNo.SelectedValue) == 0)
                        {
                            objTransactionMaster.ClientSupplAccountNo = " ";
                        }
                        else
                        {
                            objTransactionMaster.ClientSupplAccountNo = ddlAccountNo.SelectedItem.Text;
                        }
                       
                        objTransactionMaster.ClientSupplAccountNoID = Convert.ToInt32(ddlAccountNo.SelectedValue);
                        objTransactionMaster.PayeeDetails = txtPayeeDetails.Text;
                        objTransactionMaster.PaymentAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                        objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(lblPrvClientOpenAmount.Text);
                        objTransactionMaster.PaymentBalanceAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
                        objTransactionMaster.AllocatedAmount = txtThisEntry.Text != "" ? Convert.ToDecimal(txtThisEntry.Text) : 0;
                        objTransactionMaster.InvoiceBalanceAmount = Convert.ToDecimal(row.Cells[6].Text);
                        objTransactionMaster.Details = txtDetails.Text;
                        objTransactionMaster.Messages = txtMessage.Text;
                        objTransactionMaster.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                        objTransactionMaster.PaymentSourceRef = txtSourceRef.Text;
                        objTransactionMaster.categoryName = ddlAccType.SelectedItem.Text;
                        objTransactionMaster.MainAccount = lblMainAcc.Text;
                        objTransactionMaster.CategoryId = Convert.ToInt32(ddlAccType.SelectedValue.ToString());
                        _objBALTransactions.PaymentTransactionInsert(objTransactionMaster);
                    }
                }
            }


            Transaction objTransaction = new Transaction();
            int FmAccountNoId = 0;
            objTransaction.FmAccountNoId = 0;
            objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
            string category = ddlAccType.SelectedItem.Text;
            DataSet ds = _objBALTransactions.Transaction_GetAccountsData(FmAccountNoId, Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue), "PT", category);
          
            List<string> FmAcccode = new List<string>();
            List<string> FmMainAccCode = new List<string>();
            List<string> RefMainAcc = new List<string>();
            List<string> RefAccCode = new List<string>();
            List<string> AllocateAmt = new List<string>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in ds.Tables[0].Rows)
                {
                    FmAcccode.Add(dtlRow["AccCode"].ToString());
                    FmMainAccCode.Add(dtlRow["MainAccCode"].ToString());
                    AllocateAmt.Add(dtlRow["AllocatedAmount"].ToString());
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in ds.Tables[1].Rows)
                {
                    RefAccCode.Add(dtlRow["BankAcNo"].ToString());
                    RefMainAcc.Add(dtlRow["MainAccCode"].ToString());
                }
            }
            int count = FmAcccode.Count;

            for (int i = 0; i <= count-1; i++)
            {
                objTransaction.DebitAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                objTransaction.FmAccountNO = FmAcccode[i].ToString();
                //objTransaction.MainAccount = FmMainAccCode[i].ToString();
                objTransaction.ReferenceAccountNO = RefAccCode[i].ToString();
                objTransaction.CreditAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "PT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                _objBALTransactions.TransactionInsert(objTransaction);



                objTransaction.CreditAmount = Convert.ToDecimal(AllocateAmt[i].ToString());
                objTransaction.FmAccountNO = RefAccCode[i].ToString();
               // objTransaction.MainAccount = RefMainAcc[i].ToString();
                objTransaction.ReferenceAccountNO = FmAcccode[i].ToString();
                objTransaction.DebitAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;

                objTransaction.ReferenceType = "PT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
                _objBALTransactions.TransactionInsert(objTransaction);


            }



           

            ReceivedTransaction objOpenAmountDetails = new ReceivedTransaction();
            objOpenAmountDetails.SupplTypeId = Convert.ToInt32(ddlAccType.SelectedValue);
            objOpenAmountDetails.SupplNameId = Convert.ToInt32(ddlAccountNo.SelectedValue);
            objOpenAmountDetails.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0; ;
            objOpenAmountDetails.PrvOpenAmount = lblPrvClientOpenAmount.Text != "" ? Convert.ToDecimal(lblPrvClientOpenAmount.Text) : 0;
            objOpenAmountDetails.AlocatedAmount = lblAllocatedAmount.Text != "" ? Convert.ToDecimal(lblAllocatedAmount.Text) : 0;
            objOpenAmountDetails.PaymentOpenAmount = lblReceiptOpenAmount.Text != "" ? Convert.ToDecimal(lblReceiptOpenAmount.Text) : 0;
            objOpenAmountDetails.SourceRef = txtSourceRef.Text;
            objOpenAmountDetails.PaymentType = ddlPaymentType.SelectedItem.Text;
            objOpenAmountDetails.FromAccount = ddlAutoDepositeAccount.SelectedItem.Text;
            objOpenAmountDetails.ToAccount = ddlAccountNo.SelectedItem.Text;
            objOpenAmountDetails.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
            int result = _objBALTransactions.OpenAmountSupplDetailsInsertUpdateMaster(objOpenAmountDetails);
            if (result > 0)
            {
                Response.Redirect("PaymentTransactionList.aspx");
            }
            PaymentTransactionClear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }

    public void PaymentTransactionClear()
    {
        try
        {

            txtDate.Text = "";
            txtSourceRef.Text = "";
            txtAmount.Text = "";
            ddlDivision.Items.Clear();
            ddlPaymentType.Items.Clear();
            ddlAccType.Items.Clear();
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
        catch (Exception ex)
        {
            
             ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        lblTotalAvailable.Text = "0.00";
        lblReceiptOpenAmount.Text = "0.00";
        lblAllocatedAmount.Text = "0.00";
        lblTotalAvailable.Text = "0.00";
     
        lblAllocated.Text = "Allocated(" + 0 + ")";
        chkSelect.Checked = false;

        decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
        lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(lblPrvClientOpenAmount.Text) + CurrentReceiptAmount).ToString());

        lblReceiptOpenAmount.Text = (Convert.ToDecimal(lblTotalAvailable.Text) - Convert.ToDecimal(lblAllocatedAmount.Text)).ToString();
       // ddlAccType_SelectedIndexChanged(null, null);

        //int Id = Convert.ToInt32(ddlAccType.SelectedValue.ToString());
        //string name = ddlAccType.SelectedItem.Text;

        //DataSet objDsInvLst = _objBALTransactions.PaymentTransaction_GetDataBycategory(Id, name);
        //if (objDsInvLst.Tables[0].Rows.Count > 0)
        //{
        //    decimal  paiedAmount = Convert.ToDecimal(objDsInvLst.Tables[1].Rows[0]["SupplPaiedAmount"].ToString());
        
        //}

    }

    protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccType.SelectedIndex > 0)
        {
            BindAutoDepositeAccount(Convert.ToInt32(ddlAccType.SelectedValue.ToString()));
        }
        else
        {
            BindAutoDepositeAccount(0);
        }
    }

    //protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    decimal ClientTotalAvailable = 0;
    //    decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;

    //    if (ddlAccType.SelectedIndex > 0)
    //    {
    //        int Id= Convert.ToInt32(ddlAccType.SelectedValue.ToString());
    //        string name = ddlAccType.SelectedItem.Text;

    //        DataSet objDsInvLst = _objBALTransactions.PaymentTransaction_GetDataBycategory(Id, name);

    //       if (objDsInvLst.Tables[0].Rows.Count > 0)
    //       {
    //           gvData.DataSource = objDsInvLst.Tables[0];
    //           gvData.DataBind();
    //       }
    //       else
    //       {
    //           gvData.DataSource = null;
    //           gvData.DataBind();
    //           lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
    //       }
    //       if (objDsInvLst.Tables[1].Rows.Count > 0)
    //       {
    //           lblPrvClientOpenAmount.Text = objDsInvLst.Tables[1].Rows[0]["OpenAmount"].ToString();
    //           ClientTotalAvailable = CurrentReceiptAmount + Convert.ToDecimal(lblPrvClientOpenAmount.Text);
    //       }
    //       else
    //       {
    //           ClientTotalAvailable = CurrentReceiptAmount;
    //       }
    //       lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
    //       lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
    //       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
    //        BindAutoDepositeAccount(Convert.ToInt32(ddlAccType.SelectedValue.ToString()));
    //    }
    //    else
    //    {
    //       BindAutoDepositeAccount(0);
    //    }

    //    chkSelect.Checked = false;
    //}
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        int suppltypeid = Convert.ToInt32(ddlAccountNo.SelectedValue);
        string supplname = ddlAccType.SelectedItem.Text;
        int categoryId = Convert.ToInt32(ddlAccType.SelectedValue);
        int Status = 0;
        lblAllocatedAmount.Text = "0.00";
        BindInvoiceDetailsBySupplAndStatus(suppltypeid, supplname, categoryId, Status);
        txtPayeeDetails.Text = ddlAccountNo.SelectedItem.Text;

    }

    #endregion Events

    #region PrivateMethods

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
    private void BindPaymentTypes()
    {
        try
        {
            ddlPaymentType.Items.Clear();
            int pymentId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetPaymentTypes(pymentId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlPaymentType.DataSource = ObjDsClients;
                ddlPaymentType.DataValueField = "PaymentId";
                ddlPaymentType.DataTextField = "PaymentName";
                ddlPaymentType.DataBind();
                ddlPaymentType.Items.Insert(0, new ListItem("Select PaymentType", "0"));
            }
            else
            {
                ddlPaymentType.Items.Insert(0, new ListItem("Select PaymentType", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindInvoiceDetailsBySupplAndStatus(int suppltypeid, string suppltypename, int categoryId, int Status)
    {
        try
        {
            decimal ClientTotalAvailable = 0;
            decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            if (suppltypeid != 0 && categoryId != 0)
            {
                DataSet objDsInvLst = _objBALInvoice.GetInvoiceDetailsBySUpplAndStatus(suppltypeid, suppltypename, categoryId, Status);
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

    private void BindAutoDepositeAccount(int ClientType)
    {
        try
        {
            if (ClientType == 0)
            {
                ddlAccountNo.Items.Clear();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
                return;
            }
            ddlAccountNo.Items.Clear();
            BAClients objBAClients = new BAClients();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccNoofClientandSuppl(ClientType,ddlAccType.SelectedItem.Text);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAccountNo.DataSource = ObjDsClients;
                ddlAccountNo.DataValueField = "id";
                ddlAccountNo.DataTextField = "accountcode";

                ddlAccountNo.DataBind();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));
            }
            else
            {
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //private void BindAutoDepositeAccount(int ClientType)
    //{
    //    try
    //    {
    //        if (ClientType == 0)
    //        {
    //            ddlAccountNo.Items.Clear();
    //            ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
    //            return;
    //        }
    //        ddlAccountNo.Items.Clear();
    //        BAClients objBAClients = new BAClients();
    //        DataSet ObjDsClients = _objBOUtiltiy.GetAccNoofClientandSuppl(ClientType);
    //        if (ObjDsClients.Tables[0].Rows.Count > 0)
    //        {
    //            ddlAccountNo.DataSource = ObjDsClients;
    //            ddlAccountNo.DataValueField = "id";
    //            ddlAccountNo.DataTextField = "accountcodde";
    //            lblMainAcc.Text = ObjDsClients.Tables[0].Rows[0]["MainAcc"].ToString();
    //            ddlAccountNo.DataBind();
    //            ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));

               

    //            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
    //        }
    //        else
    //        {
    //            ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
    //    }
    //}
    private void BindClienttypes()
    {
        try
        {
            ddlAccType.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccountTypeOfSuppl();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAccType.DataSource = ObjDsClients;
                ddlAccType.DataValueField = "CategoryId";
                ddlAccType.DataTextField = "CategoryName";
                ddlAccType.DataBind();
                ddlAccType.Items.Insert(0, new ListItem("Select  Type", "0"));
            }
            else
            {
                ddlAccType.Items.Insert(0, new ListItem("Select  Type", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods

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

                    if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                    {
                        txtThisEntry.Text = "0";

                    }

                    if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                    {

                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
                        InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

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

                            row.Cells[6].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());

                        }


                    }

                }
            }

            OpenAmount = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);

            lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
            lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());

            lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());

            lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
           // lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion GridEvents

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelect.Checked == true)
        {
            if (txtAmount.Text != "")
            {
                Allocateallamount();

            }
        }
        
    }

    private void Allocateallamount()
    {
        try
        {
            decimal ReceiptTotalAmount = lblTotalAvailable.Text != "" ? Convert.ToDecimal(lblTotalAvailable.Text) : 0;//txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            int Allocatedcount = 0;
            decimal AllocatedTotalAmount = 0;
            decimal OpenAmount = 0;
            decimal allocateamt = 0;

            decimal ClientOpenAmount = 0;
            decimal InvoiceOpenAmount = 0;
            decimal ThisEntryAmount = 0;

            allocateamt = Convert.ToDecimal(txtAmount.Text);


            foreach (GridViewRow row in gvData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
                    HiddenField hfAllocatedAmount = row.FindControl("hfAllocatedAmount") as HiddenField;

                    //if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                    //{
                    //    txtThisEntry.Text = "0";

                    //}
                   
                     InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

                     if (allocateamt >= InvoiceOpenAmount)
                    {

                        txtThisEntry.Text = InvoiceOpenAmount.ToString();


                    if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                    {

                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
                       // InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

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

                            row.Cells[6].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());

                        }


                    }
                    allocateamt = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);
                 //  txtAmount.Text = allocateamt.ToString();
                }
            }
            }
             OpenAmount = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);
                  
            lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
            lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());

            lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());

            lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
            // lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTransactionList.aspx");
    }
   
}