using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;

public partial class Admin_CarbonCoefficients : System.Web.UI.Page
{
    EMCarbonCoefficients objCarbon = new EMCarbonCoefficients();
    BACarbonCoefficients objBACarbon = new BACarbonCoefficients();
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CarbonId"]))
            {
                int CarbonId = Convert.ToInt32(Request.QueryString["CarbonId"]);
                GetCarbonCoefficient(CarbonId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCarbon();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarbonCoefficientsList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarbonCoefficients.aspx");
    }
    #region PrivateMethods
    private void InsertUpdateCarbon()
    {
        try
        {
            objCarbon.CarbonId = Convert.ToInt32(hf_CarbonId.Value);
            objCarbon.CarbonKey = txtKey.Text;
            objCarbon.CarbonDesc = txtDescription.Text;
            objCarbon.StartDate = txtStartDate.Text;
            objCarbon.EndDate = txtEndDate.Text;

            objCarbon.Cofficient = Convert.ToDecimal(txtCoefficient.Text);

            objCarbon.CreatedBy = 0;

            int Result = objBACarbon.InsUpdCarbonCoefficients(objCarbon);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtility.ShowMessage("success", "Success", "Carbon Coefficients Created Successfully");
                clearcontrols();
                Response.Redirect("CarbonCoefficientsList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtility.ShowMessage("info", "Info", "Carbon Coefficients Details was not created please try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetCarbonCoefficient(int CarbonId)
    {
        try
        {
            objCarbon.CarbonId = CarbonId;
            DataSet ds = objBACarbon.GetCarbonCoefficients(CarbonId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_CarbonId.Value = ds.Tables[0].Rows[0]["CarbonId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["CarbonKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["CarbonDesc"].ToString();
                txtDescription.Enabled = false;
                txtStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtStartDate.Enabled = false;
                txtEndDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtEndDate.Enabled = false;
                txtCoefficient.Text = ds.Tables[0].Rows[0]["Cofficient"].ToString();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion PrivateMethods
    void clearcontrols()
    {
        hf_CarbonId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtCoefficient.Text = "";
    }
}