using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using EntityManager;
public partial class Admin_GeneralPayment : System.Web.UI.Page
{

    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAGeneralPayment _objBAGencategory = new BAGeneralPayment();
    BALTransactions _objBALTransactions = new BALTransactions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtPreparedBy.Text = Session["UserFullName"].ToString();
            BindAccountTypes();
            //BindMainAccType();
           BindCategory();
            ddlToAccCode.Enabled = false;
        }

    }

    #region Private Methods

    //ddlMainaccounttype

    //private void BindMainAccType()
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        ds = _objBOUtiltiy.GetAccountTypes();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            ddlMainaccounttype.DataSource = ds.Tables[0];
    //            ddlMainaccounttype.DataTextField = "AccountName";
    //            ddlMainaccounttype.DataValueField = "AccountId";

    //            ddlMainaccounttype.DataBind();
    //            ddlMainaccounttype.Items.Insert(0, new ListItem("--Select Type--", "0"));

    //        }
    //        else
    //        {
    //            ddlMainaccounttype.DataSource = null;
    //            ddlMainaccounttype.DataBind();
    //            ddlMainaccounttype.Items.Insert(0, new ListItem("--Select Type--", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
    //    }
    //}

    private void BindAccountTypes()
    {
        try
        {
            ddlFmAccCode.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlFmAccCode.DataSource = ObjDsClients;
                ddlFmAccCode.DataValueField = "BankAcId";
                ddlFmAccCode.DataTextField = "BankName";
                ddlFmAccCode.DataBind();
                ddlFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindCategory()
    {
        try
        {
         //   int mainaccId=Convert.ToInt32(ddlMainaccounttype.SelectedValue.ToString());

            DataSet ds = _objBAGencategory.Get_Category();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = ds;
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
            }
            else
            {
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void InsertGeneralPayment()
    {
        try
        {
            EMGeneralPayment _objEmGenPytmAc = new EMGeneralPayment();
            _objEmGenPytmAc.FromAccCodeId = Convert.ToInt32(ddlFmAccCode.SelectedValue);
          
            _objEmGenPytmAc.ToAccCodeId = Convert.ToInt32(ddlToAccCode.SelectedValue.ToString());
       
            _objEmGenPytmAc.PaymentAmount = Convert.ToDecimal(txtpytmAmount.Text);
            _objEmGenPytmAc.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            _objEmGenPytmAc.PaymentDate = Convert.ToDateTime(txtDate.Text);
            _objEmGenPytmAc.CreatedBy = 0;

            //string category = ddlGRCategory.SelectedItem.Text;

            //DataSet Dsaccount = _objBALTransactions.Transaction_GetAccountsData(Convert.ToInt32(ddlGRFmAccCode.SelectedValue), Convert.ToInt32(ddlGRToAccCode.SelectedValue), "GRT", category);


            //if (Dsaccount.Tables.Count > 0 && Dsaccount.Tables[0].Rows.Count > 0)
            //{
            //    _objEMGeneralReceipts.GRSupplierMainAccCode = Dsaccount.Tables[0].Rows[0]["MainAcName"].ToString();
            //    _objEMGeneralReceipts.GRSupplierFromAccCode = Dsaccount.Tables[0].Rows[0]["ChartedAccName"].ToString();
            //}
            
            DataSet ds = _objBAGencategory.Get_MainAccCode(Convert.ToInt32(ddlToAccCode.SelectedValue));
              if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
            {
                _objEmGenPytmAc.FmMainAcCode = ds.Tables[0].Rows[0]["MainAcName"].ToString();
              
            }
              if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
              {
                  _objEmGenPytmAc.ToMainAcCode = ds.Tables[1].Rows[0]["ChartedAccName"].ToString();
              }
            

            int Result = _objBAGencategory.InsertGeneralPaymrnt(_objEmGenPytmAc);
            if (Result > 0)
            {
                Transaction objTransaction = new Transaction();

                objTransaction.FmAccountNoId = Convert.ToInt32(ddlFmAccCode.SelectedValue);
                objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlToAccCode.SelectedValue);
                string category = "";
                DataSet Dsaccount = _objBALTransactions.Transaction_GetAccountsData(Convert.ToInt32(ddlFmAccCode.SelectedValue), Convert.ToInt32(ddlToAccCode.SelectedValue), "GPT", category);
                string FmAcccode = "";
                string FmMainAccCode = "";
                string RefMainAcc = "";
                string RefAccCode = "";

                if (Dsaccount.Tables[0].Rows.Count > 0)
                {
                    RefAccCode = Dsaccount.Tables[0].Rows[0]["AccCode"].ToString();
                    RefMainAcc = Dsaccount.Tables[0].Rows[0]["MainAccCode"].ToString();
                }

                if (Dsaccount.Tables[1].Rows.Count > 0)
                {
                    FmAcccode = Dsaccount.Tables[1].Rows[0]["BankGiAccount"].ToString();
                    FmMainAccCode = Dsaccount.Tables[1].Rows[0]["MainAccCode"].ToString();
                }


                objTransaction.CreditAmount = string.IsNullOrEmpty(txtpytmAmount.Text) ? (0.0M) : Convert.ToDecimal(txtpytmAmount.Text);
                objTransaction.FmAccountNO = FmAcccode;
                objTransaction.FmMainAccount = FmMainAccCode;
                objTransaction.ToMainAccount = RefAccCode;
                objTransaction.ReferenceAccountNO = RefAccCode;
                objTransaction.DebitAmount = 0;
                objTransaction.ReferenceNo = "0";
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "GPT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = 0;
                _objBALTransactions.TransactionInsert(objTransaction);



                //objTransaction.CreditAmount = string.IsNullOrEmpty(txtpytmAmount.Text) ? (0.0M) : Convert.ToDecimal(txtpytmAmount.Text);
                //objTransaction.FmAccountNO = RefAccCode;
                //objTransaction.FmMainAccount = RefMainAcc;
                //objTransaction.ReferenceAccountNO = FmAcccode;
                //objTransaction.DebitAmount = 0;
                //objTransaction.ReferenceNo = "0";

                //objTransaction.ReferenceType = "GPT";
                //objTransaction.CreatedBy = 0;

                //objTransaction.BalanceAmount = 0;
                //_objBALTransactions.TransactionInsert(objTransaction);

                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "GeneralPayment created Successfully");

                Response.Redirect("GeneralPaymentList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "GeneralPayment  was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion
    protected void ddlFmAccCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlToAccCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlToAccCode.Enabled = true;

       try
       {
           ddlToAccCode.Items.Clear();

           int CategoryId = Convert.ToInt32(ddlCategory.SelectedItem.Value);

           DataSet ds = _objBAGencategory.Get_CategoryAccCode(CategoryId);
           if (ds.Tables[0].Rows.Count > 0)
           {
               ddlToAccCode.DataSource = ds;
               ddlToAccCode.DataValueField = "ChartedAccId";
               ddlToAccCode.DataTextField = "ChartedAccName";
               ddlToAccCode.DataBind();
               ddlToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
           }
           else
           {
               ddlToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
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
        InsertGeneralPayment();

    }
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralPaymentList.aspx");
    }
}