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

public partial class Admin_ContactLog : System.Web.UI.Page
{
    EMContactLog objContactLog = new EMContactLog();
    BAContactLog objBALog = new BAContactLog();
    BOUtiltiy _BOUtility = new BOUtiltiy();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["LogId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["LogId"]);
                qs = _BOUtility.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if(!string.IsNullOrEmpty(Request.QueryString["LogId"]))
            {
                int logId = Convert.ToInt32(qs);
                GetContactLog(logId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateConLog();
    }
    protected void txtLogKey_TextChanged(object sender, EventArgs e)
    {
        txtLogDescription.Focus();
    }
    protected void txtLogDescription_TextChanged(object sender, EventArgs e)
    {
        txtLogDescription.Focus();
    }

#endregion
    private void InsertUpdateConLog()
    {
        try
        {
            objContactLog.LogId = Convert.ToInt32(hf_ContactLogId.Value);
            objContactLog.LogKey = txtLogKey.Text;            
            objContactLog.LogDescription = txtLogDescription.Text;
            objContactLog.CreatedBy = 0;

            int Result = objBALog.InsUpdContactLog(objContactLog);
            if (Result > 0)
            {            
               lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Contact Log Category Added Successfully");
               ClearControls();
                Response.Redirect("ContactLogList.aspx");
                
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Contact Log Category was not created please try again");
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetContactLog(int LogId)
    {
        try
        {
            DataSet ds = objBALog.GetContactLog(LogId);
            if(ds.Tables[0].Rows.Count > 0)
            {
                hf_ContactLogId.Value = ds.Tables[0].Rows[0]["LogId"].ToString();
                txtLogKey.Text = ds.Tables[0].Rows[0]["LogKey"].ToString();
                txtLogKey.Enabled = false;
                txtLogDescription.Text = ds.Tables[0].Rows[0]["LogDescription"].ToString();
            }
        }
         catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactLogList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactLog.aspx");
    }

    void ClearControls()
    {
        hf_ContactLogId.Value = "0";
        txtLogKey.Text = "";
        txtLogDescription.Text = "";

    }
    
}