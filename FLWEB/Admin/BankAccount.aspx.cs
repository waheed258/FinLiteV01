using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using EntityManager;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_BankAccount : System.Web.UI.Page
{
    EMBankAccount objBankAc = new EMBankAccount();
    BABankAccount objBABankAc = new BABankAccount();
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGraphics();
            if (!string.IsNullOrEmpty(Request.QueryString["BankAcId"]))
            {
                int bankAccId = Convert.ToInt32(Request.QueryString["BankAcId"]);
                btnSubmit.Text = "Update";
                GetBankAccDetails(bankAccId);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateBankAccDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankAccountList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankAccount.aspx");
    }
    protected void txtAccountNumber_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _objBOUtility.CheckKeyCodeExitorNot(txtAccountNumber.Text, "BankAccount");

        ds.Tables.Add(dt);

        if (ds.Tables[1].Rows.Count != 0 || ds.Tables[1].Rows.Count > 0)
        {
            lblaccNumber.Text = "Already Exist";
            lblaccNumber.ForeColor = System.Drawing.Color.Red;
            txtAccountNumber.Text = "";
        }
        else
        {
            lblaccNumber.Text = "Available";
            lblaccNumber.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }


    #region PrivateMethods
        private void InsertUpdateBankAccDetails()
         {
             try
             {
                 objBankAc.BankAcId = Convert.ToInt32(hf_BankAcId.Value);
                 objBankAc.BankAcKey = txtKey.Text.Trim();
                 objBankAc.IsDeActivate = Convert.ToInt32(chkDeactivate.Checked);
                 objBankAc.BankName = txtBankName.Text.Trim();
                 objBankAc.BankAcType = ddlAccountType.SelectedItem.Text;
                 objBankAc.BankAcNo = txtAccountNumber.Text.Trim();
                 objBankAc.BankBranchCode = txtBranchCode.Text.Trim();
                 objBankAc.BranchName = txtBranchName.Text.Trim();
                 objBankAc.AccountHolder = txtAccountHolder.Text.Trim();
                 objBankAc.Graphic = Convert.ToInt32(ddlGraphic.SelectedValue);
                 objBankAc.OwnerBranch = Convert.ToInt32(ddlOwnerBranch.SelectedValue);
                 objBankAc.GiCode = txtQuickGICode.Text.Trim();
                 objBankAc.GiDepositBatch = txtQuickGIDepositsBatch.Text.Trim();
                 objBankAc.GiPaymentBatch = txtQuickGIPaymentsBatch.Text.Trim();
                 objBankAc.InternetBankingLink = txtInternetBankingWebLink.Text.Trim();
                 objBankAc.StatementFormat = Convert.ToInt32(ddlStatementFormat.SelectedValue);
                 objBankAc.CreatedBy = 0;
                 int result = objBABankAc.InsUpdBankAccount(objBankAc);
                 if(result > 0)
                 {
                     
                         labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Bank Account Details Created Successfully");
                         clearcontrols();
                         Response.Redirect("BankAccountList.aspx");
                 }
                else
                 {
                     labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Bank Account Details are not created please try again");
                 }
                     
                 
             }
             catch(Exception ex)
             {
                 labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
                 ExceptionLogging.SendExcepToDB(ex);
             }
         }
     
        private void GetBankAccDetails(int BankAccId)
        {
            try
            { 
              objBankAc.BankAcId = BankAccId;
              DataSet ds = objBABankAc.GetBankAccount(BankAccId);
              if(ds.Tables.Count > 0)
              {
                  hf_BankAcId.Value = ds.Tables[0].Rows[0]["BankAcId"].ToString();
                  txtKey.Text = ds.Tables[0].Rows[0]["BankAcKey"].ToString();
                  txtKey.Enabled = false;
                  chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeActivate"]);
                  txtBankName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                  //txtBankName.Enabled = false;
                  ddlAccountType.SelectedIndex = ddlAccountType.Items.IndexOf(ddlAccountType.Items.FindByText(ds.Tables[0].Rows[0]["BankAcType"].ToString()));
                  //ddlAccountType.Enabled = false;
                  txtAccountNumber.Text = ds.Tables[0].Rows[0]["BankAcNo"].ToString();
                  //txtAccountNumber.Enabled = false;
                  txtBranchCode.Text = ds.Tables[0].Rows[0]["BankBranchCode"].ToString();
                  //txtBranchCode.Enabled = false;
                  txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                  //txtBranchName.Enabled = false;
                  txtAccountHolder.Text = ds.Tables[0].Rows[0]["AccountHolder"].ToString();
                  //txtAccountHolder.Enabled = false;
                  ddlGraphic.SelectedIndex = ddlGraphic.Items.IndexOf(ddlGraphic.Items.FindByValue(ds.Tables[0].Rows[0]["Graphic"].ToString()));
                  ddlOwnerBranch.SelectedIndex = ddlOwnerBranch.Items.IndexOf(ddlOwnerBranch.Items.FindByValue(ds.Tables[0].Rows[0]["OwnerBranch"].ToString()));
                  txtQuickGICode.Text = ds.Tables[0].Rows[0]["GiCode"].ToString();
                  //txtQuickGICode.Enabled = false;
                  txtQuickGIDepositsBatch.Text = ds.Tables[0].Rows[0]["GiDepositBatch"].ToString();
                  //txtQuickGIDepositsBatch.Enabled = false;
                  txtQuickGIPaymentsBatch.Text = ds.Tables[0].Rows[0]["GiPaymentBatch"].ToString();
                  //txtQuickGIPaymentsBatch.Enabled = false;
                  txtInternetBankingWebLink.Text = ds.Tables[0].Rows[0]["InternetBankingLink"].ToString();
                  ddlStatementFormat.SelectedIndex = ddlStatementFormat.Items.IndexOf(ddlStatementFormat.Items.FindByValue(ds.Tables[0].Rows[0]["StatementFormat"].ToString()));
              }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
         private void clearcontrols()
         {
             hf_BankAcId.Value = "0";
             txtKey.Text = "";
             chkDeactivate.Checked = false;
             txtBankName.Text = "";
             ddlAccountType.SelectedValue = "0";
             txtAccountNumber.Text = "";
             txtBranchCode.Text = "";
             txtBranchName.Text = "";
             txtAccountHolder.Text = "";
             ddlGraphic.SelectedValue = "0";
             ddlOwnerBranch.SelectedValue = "0";
             txtQuickGICode.Text = "";
             txtQuickGIDepositsBatch.Text = "";
             txtQuickGIPaymentsBatch.Text = "";
             txtInternetBankingWebLink.Text = "";
             ddlStatementFormat.SelectedValue = "0";
         }
    #endregion

    #region PublicMethods
       public void BindGraphics()
         {
             try
             {
                 DataSet ds = _objBOUtility.getGraphics();
                 if(ds.Tables.Count > 0)
                 {
                    // ddlGraphic.Items.Add(new ListItem("-Select-", "0"));
                     ddlGraphic.DataSource = ds;
                     ddlGraphic.DataTextField = "Description";
                     ddlGraphic.DataValueField = "Id";
                     ddlGraphic.DataBind();
                     ddlGraphic.Items.Insert(0, new ListItem("-- Please Select --", "0"));


                 }
             }
             catch (Exception ex)
             {
                 ExceptionLogging.SendExcepToDB(ex);
             
             }
         }
    #endregion
       protected void txtKey_TextChanged(object sender, EventArgs e)
       {

           DataTable dt = new DataTable();
           DataSet ds = new DataSet();
           ds = _objBOUtility.CheckKeyCodeExitorNot(txtKey.Text, "BankAccount");

           ds.Tables.Add(dt);

           if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
           {
               lblKeyerr.Text = "Already Exist";
               lblKeyerr.ForeColor = System.Drawing.Color.Red;
               txtKey.Text = "";
           }
           else
           {
               lblKeyerr.Text = "Available";
               lblKeyerr.ForeColor = System.Drawing.Color.DarkBlue;

           }
       }
   
}