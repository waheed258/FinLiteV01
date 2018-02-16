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

public partial class Admin_ReceiptTypes : System.Web.UI.Page
{
    EMReceiptTypes objemReceipt = new EMReceiptTypes();
    BAReceiptType objBAReceipt = new BAReceiptType();
    BOUtiltiy _BOUtility = new BOUtiltiy();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCreditType();
            BindBankAccount();
            BindDepListMethod();
            if (!string.IsNullOrEmpty(Request.QueryString["ReceiptId"]))
            {
                string receiptid = Request.QueryString["ReceiptId"].ToString();
                GetReceiptTypes(Convert.ToInt32(receiptid));
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateReceipt();
    }

    private void InsertUpdateReceipt()
    {
        try
        {
            objemReceipt.ReceiptId = Convert.ToInt32(hf_ReceiptTypeId.Value);
            objemReceipt.ReceiptKey = txtReceiptKey.Text;
            objemReceipt.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
            objemReceipt.RecDescription = txtDescription.Text;
            objemReceipt.DepListMethod = Convert.ToInt32(dropDepMethod.SelectedValue);
            objemReceipt.BankAccount = Convert.ToInt32(dropBankAccount.SelectedValue);
            objemReceipt.CreditCardType = Convert.ToInt32(dropCreditType.SelectedValue);
            objemReceipt.NewReceipt = Convert.ToInt32(ChkDefaultReciepts.Checked);

            int Result = objBAReceipt.InsUpdReceiptType(objemReceipt);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Receipt Types Created Successfully");
                ClearControls();
                Response.Redirect("ReceiptTypeList.aspx");
                
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Receipt Types was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetReceiptTypes(int ReceiptId)
    {
        try
        {
            DataSet ds = objBAReceipt.GetReceiptType(ReceiptId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ReceiptTypeId.Value = ds.Tables[0].Rows[0]["ReceiptId"].ToString();
                txtReceiptKey.Text = ds.Tables[0].Rows[0]["ReceiptKey"].ToString();
                txtReceiptKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["RecDescription"].ToString();
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Deactivate"]);
                dropBankAccount.SelectedIndex = dropBankAccount.Items.IndexOf(dropBankAccount.Items.FindByValue(ds.Tables[0].Rows[0]["BankAccount"].ToString()));
                dropDepMethod.SelectedIndex = dropDepMethod.Items.IndexOf(dropDepMethod.Items.FindByValue(ds.Tables[0].Rows[0]["DepListMethod"].ToString()));
                dropCreditType.SelectedIndex = dropCreditType.Items.IndexOf(dropCreditType.Items.FindByValue(ds.Tables[0].Rows[0]["CreditCardType"].ToString()));
                ChkDefaultReciepts.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["NewReceipt"]);
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
        Response.Redirect("ReceiptTypeList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptTypes.aspx");
    }
    #region Bind Methods
    private void BindBankAccount()
    {
        try
        {
            BABankAccount objBankAccount = new BABankAccount();
            int BankAcId = 0;
            DataSet ds = objBankAccount.GetBankAccount(BankAcId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropBankAccount.DataSource = ds.Tables[0];
                dropBankAccount.DataTextField = "BankName";
                dropBankAccount.DataValueField = "BankAcId";
                dropBankAccount.DataBind();
                dropBankAccount.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropBankAccount.DataSource = null;
                dropBankAccount.DataBind();
                dropBankAccount.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindCreditType()
    {
        try
        {
            BACreditCard objcreditCard = new BACreditCard();
            int CreditCardId = 0;
            DataSet ds = objcreditCard.GetCreditCard(CreditCardId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCreditType.DataSource = ds.Tables[0];
                dropCreditType.DataTextField = "CreditDescription";
                dropCreditType.DataValueField = "CreditCardId";
                dropCreditType.DataBind();
                dropCreditType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropCreditType.DataSource = null;
                dropCreditType.DataBind();
                dropCreditType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    private void BindDepListMethod()
    {
        try
        {

            DataSet ds = _BOUtility.GetDepListMethods();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropDepMethod.DataSource = ds.Tables[0];
                dropDepMethod.DataTextField = "DepListName";
                dropDepMethod.DataValueField = "DepListId";
                dropDepMethod.DataBind();
                dropDepMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                dropDepMethod.DataSource = null;
                dropDepMethod.DataBind();
                dropDepMethod.Items.Insert(0, new ListItem("-- Please Select --", "0"));

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
        hf_ReceiptTypeId.Value = "0";
        txtReceiptKey.Text = "";
        txtDescription.Text = "";
        dropBankAccount.SelectedValue = "0";
        dropCreditType.SelectedValue = "0";
        dropDepMethod.SelectedValue = "0";
        chkDeactivate.Checked = false;
        ChkDefaultReciepts.Checked = false;
    }
}