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
public partial class Admin_Branch : System.Web.UI.Page
{
    EMBranch objEMBranch = new EMBranch();
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BABranch objBABranch = new BABranch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getStates();

            if (!string.IsNullOrEmpty(Request.QueryString["BranchId"]))
            {
                int branchid = Convert.ToInt32(Request.QueryString["BranchId"]);
                cmdSubmit.Text = "Update";
                getBranchDetails(branchid);
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateBranchDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchList.aspx");
    }
    protected void cmdReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch.aspx");
    }

    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {

        getCityState();
    }


    #region PublicMethods

    public void getStates()
    {
        try
        {
            BAState objBAState = new BAState();
            int Id = 0;
            DataSet ds = objBAState.GetState(Id);
            if (ds.Tables.Count > 0)
            {
                ddlProvince.Items.Add(new ListItem("-Select-", "-1"));
                ddlProvince.DataSource = ds.Tables[0];
                ddlProvince.DataValueField = "Id";
                ddlProvince.DataTextField = "Name";
                ddlProvince.DataBind();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void getCityState()
    {
        try
        {
            ddlCity.Items.Clear();
            DataSet ds = new DataSet();
            int state_id = Convert.ToInt32(ddlProvince.SelectedValue.ToString());
            ds = _objBOUtility.get_City_State(state_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCity.DataSource = ds.Tables[0];
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("-Select-", "-1"));
                ddlCity.DataTextField = "Name";
                ddlCity.DataValueField = "Id";
                ddlCity.DataBind();
            }
            else
            {
                ddlCity.DataSource = null;
                ddlCity.DataBind();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion



    #region PrivateMethods
    private void InsertUpdateBranchDetails()
    {
        try
        {

            objEMBranch.BranchId = Convert.ToInt32(hf_branchid.Value);
            objEMBranch.BranchCode = txtBranchCode.Text.Trim();
            objEMBranch.BranchName = txtBranchName.Text.Trim();
            objEMBranch.DeActivate = Convert.ToInt32(chkDeActivate.Checked);
            objEMBranch.ContactName = txtContactName.Text.Trim();
            objEMBranch.TelephoneNumber = txtTelephoneNumber.Text.Trim();
            objEMBranch.FaxNumber = txtFaxNumber.Text.Trim();
            objEMBranch.CellNumber = txtCellNumber.Text.Trim();
            objEMBranch.EmailAddress = txtEmailAddress.Text.Trim();
            objEMBranch.WebAddress = txtWebAddress.Text.Trim();
            objEMBranch.Province = Convert.ToInt32(ddlProvince.SelectedValue);
            objEMBranch.city = Convert.ToInt32(ddlCity.SelectedValue);
            objEMBranch.CreatedBy = 0;
            objEMBranch.PostalAddress = txtPostalAddress.Text.Trim();
            objEMBranch.PhysicalAddress = txtPhysicalAddress.Text.Trim();
            objEMBranch.DOCEX = txtDOCEX.Text.Trim();
            objEMBranch.Co_No = txtCoRegNo.Text.Trim();
            objEMBranch.IATA_No = txtIATARegNo.Text.Trim();
            objEMBranch.Vat_No = txtVatRegNo.Text.Trim();
            objEMBranch.ASATA_Member = Convert.ToInt32(chkASATAMember.Checked);
            objEMBranch.Print_Doc = Convert.ToInt32(chkPrintDoc.Checked);
            int result = objBABranch.InsUpdBranch(objEMBranch);
            if (result > 0)
            {

                labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Branch Details Created Successfully");
                clearcontrols();
                Response.Redirect("BranchList.aspx");
            }
            else
            {
                labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Branch Details Are Not Created Successfully Please try again");
            }

        }


        catch (Exception ex)
        {
            labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    private void getBranchDetails(int branchId)
    {
        try
        {
            objEMBranch.BranchId = branchId;
            DataSet ds = objBABranch.GetBranch(branchId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                hf_branchid.Value = ds.Tables[0].Rows[0]["BranchId"].ToString();
                txtBranchCode.Text = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                txtBranchCode.Enabled = false;
                txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                chkDeActivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DeActivate"]);
                txtContactName.Text = ds.Tables[0].Rows[0]["ContactName"].ToString();
                txtTelephoneNumber.Text = ds.Tables[0].Rows[0]["TelephoneNumber"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtCellNumber.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtEmailAddress.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtWebAddress.Text = ds.Tables[0].Rows[0]["WebAddress"].ToString();
                ddlProvince.SelectedIndex = ddlProvince.Items.IndexOf(ddlProvince.Items.FindByValue(ds.Tables[0].Rows[0]["Province"].ToString()));
                getCityState();
                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["City"].ToString()));
                txtPostalAddress.Text = ds.Tables[0].Rows[0]["PostalAddress"].ToString();
                txtPhysicalAddress.Text = ds.Tables[0].Rows[0]["PhysicalAddress"].ToString();
                txtDOCEX.Text = ds.Tables[0].Rows[0]["DOCEX"].ToString();
                txtCoRegNo.Text = ds.Tables[0].Rows[0]["Co_No"].ToString();
                txtIATARegNo.Text = ds.Tables[0].Rows[0]["IATA_No"].ToString();
                txtVatRegNo.Text = ds.Tables[0].Rows[0]["Vat_No"].ToString();
                chkASATAMember.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ASATA_Member"]);
                chkPrintDoc.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Print_Doc"]);
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void clearcontrols()
    {
        hf_branchid.Value = "0";
        txtBranchCode.Text = "";
        txtBranchName.Text = "";
        chkDeActivate.Checked = false;
        txtContactName.Text = "";
        txtTelephoneNumber.Text = "";
        txtFaxNumber.Text = "";
        txtCellNumber.Text = "";
        txtEmailAddress.Text = "";
        txtWebAddress.Text = "";
        ddlProvince.SelectedValue = "-1";
        ddlCity.SelectedValue = "-1";
        txtPostalAddress.Text = "";
        txtPhysicalAddress.Text = "";
        txtDOCEX.Text = "";
        txtCoRegNo.Text = "";
        txtIATARegNo.Text = "";
        txtVatRegNo.Text = "";
        chkASATAMember.Checked = false;
        chkPrintDoc.Checked = false;
    }

    #endregion

}