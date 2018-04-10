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

public partial class CommissionType : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BACommissionType objBACommType = new BACommissionType();
    EMCommissionType objCommType = new EMCommissionType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            BindCategory();
            BindVAT();
            ddlLandSubCategory.Enabled = false;
            var qs = "0";
            if (Request.QueryString["ComId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["ComId"]);
                qs = _objBOUtility.Decrypts(HttpUtility.UrlDecode(getId), true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["ComId"]))
            {
                int commId = Convert.ToInt32(qs);
                btnSubmit.Text = "Update";
                GetCommType(commId);
                
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCommType();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CommissionTypeList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CommissionType.aspx");
    }
    #region PublicMethods
    public void BindType()
    {
        ddlDefaultType.Items.Clear();
        try
        {
            DataSet ds = _objBOUtility.getType();
            ddlDefaultType.DataSource = ds;
            ddlDefaultType.DataTextField = "TypeName";
            ddlDefaultType.DataValueField = "TypeId";
            ddlDefaultType.DataBind();
            ddlDefaultType.Items.Insert(0, new ListItem("--Select Default Type--", "0"));


        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindCategory()
    {
        ddlCategory.Items.Clear();
        try
        {

            DataSet ds = objBACommType.GetCategory();
            ddlCategory.DataSource = ds;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindVAT()
    {
        try
        {
            int vatid = 0;
            DataSet ds = objBACommType.GetVAT(vatid);

            ddlDefaultVAT.DataSource = ds;
            ddlDefaultVAT.DataTextField = "VatRate";
            ddlDefaultVAT.DataValueField = "VatId";
            ddlDefaultVAT.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion

    #region PrivateMethods
    private void InsertUpdateCommType()
    {
        try
        {
            objCommType.Id = Convert.ToInt32(hf_ComId.Value);
            objCommType.key = txtKey.Text.Trim();
            objCommType.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
            objCommType.Desc = txtDescription.Text.Trim();
            objCommType.Category = Convert.ToInt32(ddlCategory.SelectedValue);
            objCommType.LSCategory = Convert.ToInt32(ddlLandSubCategory.SelectedValue);
            objCommType.DType = Convert.ToInt32(ddlDefaultType.SelectedValue);
            if (!string.IsNullOrEmpty(txtDefaultComm.Text))
            {
                objCommType.DComm = Convert.ToDecimal(txtDefaultComm.Text);
            }
            if (!string.IsNullOrEmpty(txtDefaultRate.Text))
            {
                objCommType.DRate = Convert.ToDecimal(txtDefaultRate.Text);
            }
            objCommType.UDesc = txtUnitDescription.Text.Trim();
            objCommType.DVat = Convert.ToInt32(ddlDefaultVAT.SelectedValue);
            objCommType.NTFee = Convert.ToInt32(ChkNonTravelFee.Checked);
            objCommType.ZUType = Convert.ToInt32(ddlZeroUnitsType.SelectedValue);
            objCommType.Income = Convert.ToInt32(ddlIncomeCharges.SelectedValue);
            objCommType.OVat = Convert.ToInt32(ddlOutputVAT.SelectedValue);
            objCommType.CreatedBy = 0;
            int result = objBACommType.InsUpdCommissionType(objCommType);
            if (result > 0)
            {

                labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Commission Type Details Created Successfully");
                clearcontrols();
                Response.Redirect("CommissionTypeList.aspx");
            }
            else
            {
                labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Commission Type Details are not created please try again");
            }


        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void GetCommType(int CommId)
    {
        try
        {
            objCommType.Id = CommId;
            DataSet ds = objBACommType.GetCommissionType(CommId);
            if (ds.Tables.Count > 0)
            {
                hf_ComId.Value = ds.Tables[0].Rows[0]["ComId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["ComKey"].ToString();
                txtKey.Enabled = false;
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ComDeactivate"]);
                txtDescription.Text = ds.Tables[0].Rows[0]["ComDesc"].ToString();
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ComCategory"].ToString()));
                ddlLandSubCategory.SelectedIndex = ddlLandSubCategory.Items.IndexOf(ddlLandSubCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ComLSCategory"].ToString()));
                ddlDefaultType.SelectedIndex = ddlDefaultType.Items.IndexOf(ddlDefaultType.Items.FindByValue(ds.Tables[0].Rows[0]["ComDType"].ToString()));
                txtDefaultComm.Text = ds.Tables[0].Rows[0]["ComDComm"].ToString();
                txtDefaultRate.Text = ds.Tables[0].Rows[0]["ComDRate"].ToString();
                txtUnitDescription.Text = ds.Tables[0].Rows[0]["ComUDesc"].ToString();
                ddlDefaultVAT.SelectedIndex = ddlDefaultVAT.Items.IndexOf(ddlDefaultVAT.Items.FindByValue(ds.Tables[0].Rows[0]["ComDVat"].ToString()));
                ChkNonTravelFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ComNTFee"]);
                ddlZeroUnitsType.SelectedIndex = ddlZeroUnitsType.Items.IndexOf(ddlZeroUnitsType.Items.FindByValue(ds.Tables[0].Rows[0]["ComZUType"].ToString()));
                ddlIncomeCharges.SelectedIndex = ddlIncomeCharges.Items.IndexOf(ddlIncomeCharges.Items.FindByValue(ds.Tables[0].Rows[0]["ComIncome"].ToString()));
                ddlOutputVAT.SelectedIndex = ddlOutputVAT.Items.IndexOf(ddlOutputVAT.Items.FindByValue(ds.Tables[0].Rows[0]["ComOVat"].ToString()));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void clearcontrols()
    {
        hf_ComId.Value = "0";
        txtKey.Text = "";
        chkDeactivate.Checked = false;
        txtDescription.Text = "";
        ddlCategory.SelectedValue = "0";
        ddlLandSubCategory.SelectedValue = "-1";
        ddlDefaultType.SelectedValue = "0";
        txtDefaultComm.Text = "";
        txtDefaultRate.Text = "";
        txtUnitDescription.Text = "";
        ddlDefaultVAT.SelectedValue = "0";
        ChkNonTravelFee.Checked = false;
        ddlZeroUnitsType.SelectedValue = "-1";
        ddlIncomeCharges.SelectedValue = "0";
        ddlOutputVAT.SelectedValue = "0";
    }
    #endregion
    protected void txtKey_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _objBOUtility.CheckKeyCodeExitorNot(txtKey.Text, "CommType");

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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CategoryId = Convert.ToInt32(ddlCategory.SelectedValue.ToString());

        if (CategoryId == 3)
        {
            ddlLandSubCategory.Enabled = true;
            DataSet ds = objBACommType.GetLandSubCategory();
            ddlLandSubCategory.DataSource = ds;
            ddlLandSubCategory.DataTextField = "LSC_Name";
            ddlLandSubCategory.DataValueField = "LSC_Id";
            ddlLandSubCategory.DataBind();
            ddlLandSubCategory.Items.Insert(0, new ListItem("--Select Land SubCategories--", "0"));
        }
        else
        {
            ddlLandSubCategory.Enabled = false;
        }
    }
}