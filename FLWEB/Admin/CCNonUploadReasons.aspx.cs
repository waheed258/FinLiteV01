using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;

public partial class Admin_CCNonUploadReasons : System.Web.UI.Page
{

    EMNonUploadReasons objUploadReasons = new EMNonUploadReasons();
    BANonUploadReasons objBAUploadReasons = new BANonUploadReasons();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["NonUploadId"]))
            {
                int UploadId = Convert.ToInt32(Request.QueryString["NonUploadId"]);
                GetNonUploadReasons(UploadId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsUpdNonUploadReasons();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CCNonUploadReasonsList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CCNonUploadReasons.aspx");
    }

    #region PrivateMethods
    private void InsUpdNonUploadReasons()
    {
        try
        {
            objUploadReasons.NonUploadId = Convert.ToInt32(hf_NonUploadId.Value);
            objUploadReasons.NonUploadKey = txtKey.Text;
            objUploadReasons.NonUploadDesc = txtDescription.Text;
            objUploadReasons.CreatedBy = 0;

            int Result = objBAUploadReasons.InsUpdNonUploadReasons(objUploadReasons);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "NonUpload Reasons Created Successfully");
                clearcontrols();
                Response.Redirect("CCNonUploadReasonsList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "NonUpload Reasons Details was not created please try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetNonUploadReasons(int UploadId)
    {
        try
        {
            DataSet ds = objBAUploadReasons.GetNonUploadReasons(UploadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_NonUploadId.Value = ds.Tables[0].Rows[0]["NonUploadId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["NonUploadKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["NonUploadDesc"].ToString();
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
        hf_NonUploadId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
    }
}