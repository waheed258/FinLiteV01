using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using DataManager;
using System.Data;
using EntityManager;
public partial class Admin_Generalreceipts : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BALGeneralReceipts _objBALGR = new BALGeneralReceipts();
    EMGeneralReceipts _objEMGeneralReceipts = new EMGeneralReceipts();
    BAGeneralPayment _objBAGencategory = new BAGeneralPayment();
    BALTransactions _objBALTransactions = new BALTransactions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();
            BindAccountTypes();
            txtGRPreparedBy.Text = Session["UserFullName"].ToString();
        }
    }


    private void BindCategory()
    {
        try
        {
            ddlGRCategory.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccountTypeOfSuppl();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlGRCategory.DataSource = ObjDsClients;
                ddlGRCategory.DataValueField = "CategoryId";
                ddlGRCategory.DataTextField = "CategoryName";
                ddlGRCategory.DataBind();
                ddlGRCategory.Items.Insert(0, new ListItem("Select  Type", "0"));
            }
            else
            {
                ddlGRCategory.Items.Insert(0, new ListItem("Select  Type", "0"));
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
                ddlGRFmAccCode.Items.Clear();
                ddlGRFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
                return;
            }
            ddlGRFmAccCode.Items.Clear();
            BAClients objBAClients = new BAClients();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccNoofClientandSuppl(ClientType);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlGRFmAccCode.DataSource = ObjDsClients;
                ddlGRFmAccCode.DataValueField = "id";
                ddlGRFmAccCode.DataTextField = "accountcodde";
                ddlGRFmAccCode.DataBind();
                ddlGRFmAccCode.Items.Insert(0, new ListItem("Select Account Code", "0"));
            }
            else
            {
                ddlGRFmAccCode.Items.Insert(0, new ListItem("Select Account Code", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlGRCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlGRCategory.SelectedValue) > 0)
        {
            BindAutoDepositeAccount(Convert.ToInt32(ddlGRCategory.SelectedValue));
        }
        else
        {
            BindAutoDepositeAccount(0);
        }
    }
    private void BindAccountTypes()
    {
        try
        {
            ddlGRToAccCode.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlGRToAccCode.DataSource = ObjDsClients;
                ddlGRToAccCode.DataValueField = "BankAcId";
                ddlGRToAccCode.DataTextField = "BankName";
                ddlGRToAccCode.DataBind();
                ddlGRToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlGRToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            _objEMGeneralReceipts.GRPaymentDate = Convert.ToDateTime(txtGRDate.Text);
            _objEMGeneralReceipts.GRPreparedBy = txtGRPreparedBy.Text;
            _objEMGeneralReceipts.GRCategoryID = Convert.ToInt32(ddlGRCategory.SelectedValue);
            _objEMGeneralReceipts.GRFromAccount = Convert.ToInt32(ddlGRFmAccCode.SelectedValue);
            _objEMGeneralReceipts.ToAccountID = Convert.ToInt32(ddlGRToAccCode.SelectedValue);
            _objEMGeneralReceipts.GRPaymentAmount = string.IsNullOrEmpty(txtGRPaymentAmount.Text) ? (0.0M) : Convert.ToDecimal(txtGRPaymentAmount.Text);


            DataSet ds = _objBALGR.Get_GRMainAccCode(Convert.ToInt32(ddlGRFmAccCode.SelectedValue));


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _objEMGeneralReceipts.GRSupplierMainAccCode = ds.Tables[0].Rows[0]["MainAcName"].ToString();


            }

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                _objEMGeneralReceipts.GRSupplierFromAccCode = ds.Tables[1].Rows[0]["ChartedAccName"].ToString();
            }

            _objEMGeneralReceipts.CreatedBy = 0;


            int Result = _objBALGR.InsertGeneralReceipts(_objEMGeneralReceipts);
            if (Result > 0)
            {

                Transaction objTransaction = new Transaction();

                objTransaction.FmAccountNoId = Convert.ToInt32(ddlGRFmAccCode.SelectedValue);
                objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlGRToAccCode.SelectedValue);
                string category = ddlGRCategory.SelectedItem.Text;
                DataSet Dsaccount = _objBALTransactions.Transaction_GetAccountsData(Convert.ToInt32(ddlGRFmAccCode.SelectedValue), Convert.ToInt32(ddlGRToAccCode.SelectedValue), "GRT", category);
                string FmAcccode = "";
                string FmMainAccCode = "";
                string RefMainAcc = "";
                string RefAccCode = "";

                if (Dsaccount.Tables[0].Rows.Count > 0)
                {
                    FmAcccode = Dsaccount.Tables[0].Rows[0]["AccCode"].ToString();
                    FmMainAccCode = Dsaccount.Tables[0].Rows[0]["MainAccCode"].ToString();
                }

                if (Dsaccount.Tables[1].Rows.Count > 0)
                {
                    RefAccCode = Dsaccount.Tables[1].Rows[0]["BankGiAccount"].ToString();
                    RefMainAcc = Dsaccount.Tables[1].Rows[0]["MainAccCode"].ToString();
                }


                objTransaction.DebitAmount = string.IsNullOrEmpty(txtGRPaymentAmount.Text) ? (0.0M) : Convert.ToDecimal(txtGRPaymentAmount.Text);
                objTransaction.FmAccountNO = FmAcccode;
                objTransaction.FmMainAccount = FmMainAccCode;
                objTransaction.ReferenceAccountNO = RefAccCode;
                objTransaction.CreditAmount = 0;
                objTransaction.ReferenceNo = "0";
                objTransaction.ToMainAccount = RefMainAcc;
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "GRT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = 0;
                _objBALTransactions.TransactionInsert(objTransaction);



                //objTransaction.CreditAmount = string.IsNullOrEmpty(txtGRPaymentAmount.Text) ? (0.0M) : Convert.ToDecimal(txtGRPaymentAmount.Text);
                //objTransaction.FmAccountNO = RefAccCode;
                //objTransaction.FmMainAccount = RefMainAcc;
                //objTransaction.ReferenceAccountNO = FmAcccode;
                //objTransaction.DebitAmount = 0;
                //objTransaction.ReferenceNo = "0";

                //objTransaction.ReferenceType = "GRT";
                //objTransaction.CreatedBy = 0;

                //objTransaction.BalanceAmount = 0;
                //_objBALTransactions.TransactionInsert(objTransaction);

                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "GeneralPayment created Successfully");

                Response.Redirect("GeneralReceiptsList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "GeneralPayment  was not created please try again");
            }
        }
        catch (Exception ex)
        {
            
              ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlGRCategory.SelectedIndex = 0;
        ddlGRFmAccCode.SelectedIndex = 0;
        ddlGRToAccCode.SelectedIndex = 0;
        txtGRPaymentAmount.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralreceiptsList.aspx");
    }
}