using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using DataManager;
using BusinessManager;
using System.Data;

public partial class Admin_PaymentTypes : System.Web.UI.Page
{
    EMPaymentTypes objPaymentTypes = new EMPaymentTypes();
    BAPaymentTypes objBAPaymentTypes = new BAPaymentTypes();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PaymentId"]))
            {
                int PayId = Convert.ToInt32(Request.QueryString["PaymentId"]);
                GetPaymentTypes(PayId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdatePaymentTypes();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTypesList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTypes.aspx");
    }

    #region PrivateMethods

    private void InsertUpdatePaymentTypes()
    {
        try
        {
            objPaymentTypes.PaymentId = Convert.ToInt32(hf_PaymentId.Value);
            objPaymentTypes.PaymentName = txtDescription.Text;
            objPaymentTypes.PaymentCode = txtKey.Text;
            objPaymentTypes.IsDefaultPayment = Convert.ToInt32(chkdefaultpayment.Checked);
            objPaymentTypes.CreatedBy = 0;

            int Result = objBAPaymentTypes.InsUpdPaymentTypes(objPaymentTypes);

            if (Result > 0)
            {

                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "PaymentTypes  Created Successfully.");
                clearcontrols();
                Response.Redirect("PaymentTypesList.aspx");
            }

            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "PaymentTypes  was not created plase try again");
            }
        }

        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetPaymentTypes(int PaymentId)
    {
        try
        {
            objPaymentTypes.PaymentId = PaymentId;
            DataSet ds = objBAPaymentTypes.GetPaymentTypes(PaymentId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_PaymentId.Value = ds.Tables[0].Rows[0]["PaymentId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["PaymentCode"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["PaymentName"].ToString();
                chkdefaultpayment.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDefaultPayment"]);
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    #endregion PrivateMethods


    void clearcontrols()
    {
        hf_PaymentId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        chkdefaultpayment.Checked = false;
    }

}