using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;


public partial class Admin_Continents : System.Web.UI.Page
{
    EMContinents objContinents = new EMContinents();
    BAContinents objBAContinents = new BAContinents();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["ContinentId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["ContinentId"]);
                qs = _objBOUtiltiy.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["ContinentId"]))
            {
                // int ContId = Convert.ToInt32(Request.QueryString["ContinentId"]);
                GetContinents(Convert.ToInt32(qs));
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsUpdateContinents();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContinentsList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Continents.aspx");
    }

    #region PrivateMethods

    private void InsUpdateContinents()
    {
        try
        {
            objContinents.ContinentId = Convert.ToInt32(hf_ContinentId.Value);
            objContinents.ContinentKey = txtKey.Text;
            objContinents.ContinentDesc = txtDescription.Text;
            objContinents.CreatedBy = 0;

            int Result = objBAContinents.IsuUpdContinents(objContinents);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Continent Details Created Successfully");
                clearcontrols();
                Response.Redirect("ContinentsList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Continent Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetContinents(int ContinentId)
    {
        try
        {
            objContinents.ContinentId = ContinentId;
            DataSet ds = objBAContinents.GetContinents(ContinentId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ContinentId.Value = ds.Tables[0].Rows[0]["ContinentId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["ContinentKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["ContinentDesc"].ToString();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_ContinentId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
    }
}