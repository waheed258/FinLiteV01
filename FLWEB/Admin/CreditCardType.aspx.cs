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

public partial class Admin_CreditCardType : System.Web.UI.Page
{
    EMCreditCard objEMCreditCard = new EMCreditCard();
    BACreditCard objBACredit = new BACreditCard();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["CreditCardId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["CreditCardId"]);
                qs = _BOUtility.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["CreditCardId"]))
            {
                //string CreditId = qs.ToString();
                GetCreditCard(Convert.ToInt32(qs));
                cmdSubmit.Text = "Update";
            }

        }

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCreditCard();
    }

    private void InsertUpdateCreditCard()
    {
        try
        {
            objEMCreditCard.CreditCardId = Convert.ToInt32(hf_CreditCardId.Value);
            objEMCreditCard.CreditCardKey = txtCreditKey.Text;
            objEMCreditCard.CreditDescription = txtCreDescription.Text;
            objEMCreditCard.NumberPrefix = Convert.ToInt32(txtNumberPrefix.Text);
            objEMCreditCard.CreatedBy = 0;

            int Result = objBACredit.InsUpdCreditType(objEMCreditCard);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "CreditCard Details Created Successfully");
                ClearControls();
                Response.Redirect("CreditCardList.aspx", false);
                
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "CreditCard Details was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetCreditCard(int CreditCardId)
    {
        try
        {
            DataSet ds = objBACredit.GetCreditCard(CreditCardId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_CreditCardId.Value = ds.Tables[0].Rows[0]["CreditCardId"].ToString();
                txtCreditKey.Text = ds.Tables[0].Rows[0]["CreditCardKey"].ToString();
                txtCreditKey.Enabled = false;
                txtCreDescription.Text = ds.Tables[0].Rows[0]["CreditDescription"].ToString();
                txtNumberPrefix.Text = ds.Tables[0].Rows[0]["NumberPrefix"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void txtCreditKey_TextChanged(object sender, EventArgs e)
    {
        txtCreDescription.Focus();
    }
    protected void txtCreDescription_TextChanged(object sender, EventArgs e)
    {
        txtNumberPrefix.Focus();
    }
    protected void txtNumberPrefix_TextChanged(object sender, EventArgs e)
    {
        txtNumberPrefix.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreditCardList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreditCardType.aspx");
    }

    void ClearControls()
    {
        hf_CreditCardId.Value = "0";
        txtCreditKey.Text = "";
        txtCreDescription.Text = "";
        txtNumberPrefix.Text = "";
    }
   
}