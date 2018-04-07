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

public partial class Admin_CashBookType : System.Web.UI.Page
{
    EMCashBook objEMCash = new EMCashBook();
    BACashBook objBACash = new BACashBook();
    BOUtiltiy _BOUtility = new BOUtiltiy();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTansactionAction();
            var qs = "0";
            if (Request.QueryString["CashBookId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["CashBookId"]);
                qs = _BOUtility.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["CashBookId"]))
            {
                //  int cashBookId = Convert.ToInt32(Request.QueryString["CashBookId"].ToString());
                GetCashBook(Convert.ToInt32(qs));
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCash();
    }

    private void InsertUpdateCash()
    {
        try
        {
            objEMCash.CashBookId = Convert.ToInt32(hf_CashBookId.Value);
            objEMCash.CashBookKey = txtCashKey.Text;
            objEMCash.CashDescription = txtCashDescription.Text;
            objEMCash.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
            objEMCash.DefaultAction = Convert.ToInt32(dropDefaultAction.SelectedValue);
            objEMCash.GICode = txtGICode.Text;
            objEMCash.ReferenceFormat = txtRefFormat.Text;
            objEMCash.VatCodes = txtVatCodes.Text;
            objEMCash.CreatedBy = 0;

            int Result = objBACash.InsUpdCashBook(objEMCash);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "CashBook Types Created Successfully");
                ClearControls();
                Response.Redirect("CashBookList.aspx");

            }

            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "CashBook Types  was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetCashBook(int CashBookId)
    {
        try
        {
            DataSet ds = objBACash.GetCashBook(CashBookId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_CashBookId.Value = ds.Tables[0].Rows[0]["CashBookId"].ToString();
                txtCashKey.Text = ds.Tables[0].Rows[0]["CashBookKey"].ToString();
                txtCashKey.Enabled = false;
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Deactivate"]);
                txtCashDescription.Text = ds.Tables[0].Rows[0]["CashDescription"].ToString();
                dropDefaultAction.SelectedIndex = dropDefaultAction.Items.IndexOf(dropDefaultAction.Items.FindByValue(ds.Tables[0].Rows[0]["DefaultAction"].ToString()));
                txtGICode.Text = ds.Tables[0].Rows[0]["GICode"].ToString();
                txtRefFormat.Text = ds.Tables[0].Rows[0]["ReferenceFormat"].ToString();
                txtVatCodes.Text = ds.Tables[0].Rows[0]["VatCodes"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CashBookList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CashBookType.aspx");
    }
    protected void txtCashKey_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCashDescription_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtGICode_TextChanged(object sender, EventArgs e)
    {

    }
#endregion
    private void BindTansactionAction()
    {
        DataSet ds = _BOUtility.GetTransactionAction();
        if (ds.Tables[0].Rows.Count > 0)
        {
            dropDefaultAction.DataSource = ds.Tables[0];
            dropDefaultAction.DataTextField = "TransName";
            dropDefaultAction.DataValueField = "TransId";
            dropDefaultAction.DataBind();
        }
        else
        {
            dropDefaultAction.DataSource = null;
            dropDefaultAction.DataBind();
        }
    }

    void ClearControls()
    {
        hf_CashBookId.Value = "0";
        txtCashKey.Text = "";
        txtCashDescription.Text = "";
        txtGICode.Text = "";
        txtRefFormat.Text = "";
        txtVatCodes.Text = "";
        chkDeactivate.Checked = false;
        dropDefaultAction.SelectedValue = "-1";
    }
  
}