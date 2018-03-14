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

public partial class Admin_VatType : System.Web.UI.Page
{
    EMVatType objEMVatType = new EMVatType();
    BAVatType objBAVatType = new BAVatType();
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Queystring();
        }
    }

    private void Queystring()
    {
        var qs = "0";
        if (Request.QueryString["VatId"] == null)
        {
            qs = "0";
        }
        else
        {
            string getId = Convert.ToString(Request.QueryString["VatId"]);
            qs = _objBOUtility.Decrypt(HttpUtility.UrlDecode(getId));

        }

        if (!string.IsNullOrEmpty(Request.QueryString["VatId"]))
        {
            int VatId = Convert.ToInt32(qs);
            // int VatId = Convert.ToInt32(Request.QueryString["VatId"]);
            btnSubmit.Text = "Update";
            GetVatDetails(VatId);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateVatDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VatTypeList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("VatType.aspx");
    }
    #region PrivateMethods
    private void InsertUpdateVatDetails()
    {
        try
        {
            objEMVatType.VatId = Convert.ToInt32(hf_VatId.Value);
            objEMVatType.VatKey = txtKey.Text.Trim();
            objEMVatType.VatDesc = txtDescription.Text.Trim();
            objEMVatType.VatRate = txtVatRate.Text.Trim();
            objEMVatType.VatAppTo = ddlApplicableTo.SelectedItem.Text;
            objEMVatType.VatEffDate = txtEffectiveDate.Text.Trim();
            objEMVatType.VatGICode = txtGICode.Text.Trim();
            objEMVatType.CreatedBy = 0;
            int result = objBAVatType.InsUpdVatType(objEMVatType);
            if (result > 0)
            {

                labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Vat Type Details Created Successfully");
                clearcontrols();
                Response.Redirect("VatTypeList.aspx");
            }
            else
            {
                labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Vat Type Details are not created please try again");
            }


        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void GetVatDetails(int VatId)
    {
        try
        {
            objEMVatType.VatId = VatId;
            DataSet ds = objBAVatType.GetVatType(VatId);
            if (ds.Tables.Count > 0)
            {
                hf_VatId.Value = ds.Tables[0].Rows[0]["VatId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["VatKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["VatDescription"].ToString();
                txtVatRate.Text = ds.Tables[0].Rows[0]["VatRate"].ToString();
                ddlApplicableTo.SelectedIndex = ddlApplicableTo.Items.IndexOf(ddlApplicableTo.Items.FindByText(ds.Tables[0].Rows[0]["VatAppTo"].ToString()));
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["VatEffectiveDate"].ToString();
                txtGICode.Text = ds.Tables[0].Rows[0]["VatGICode"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void clearcontrols()
    {
        hf_VatId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        txtVatRate.Text = "";
        ddlApplicableTo.SelectedValue = "-1";
        txtEffectiveDate.Text = "";
        txtGICode.Text = "";
    }

    #endregion
    protected void txtKey_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _objBOUtility.CheckKeyCodeExitorNot(txtKey.Text, "VatType");

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