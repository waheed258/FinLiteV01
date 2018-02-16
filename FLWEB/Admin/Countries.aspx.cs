using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;

public partial class Admin_Countries : System.Web.UI.Page
{
    EMCountries objCountries = new EMCountries();
    BACountries objBACountries = new BACountries();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindContinents();
            BindTravelCategory();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                int CountryId = Convert.ToInt32(Request.QueryString["Id"]);
                GetCountries(CountryId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsUpdateCountries();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CountriesList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Countries.aspx");
    }

    #region PrivateMethods

    private void InsUpdateCountries()
    {
        try
        {
            objCountries.Id = Convert.ToInt32(hf_Id.Value);
            objCountries.Name = txtDescription.Text;
            objCountries.CountryKey = txtKey.Text;
            objCountries.Continent = Convert.ToInt32(ddlContinent.SelectedValue);
            objCountries.TimeZoneUTC = txtTimeZone.Text;

            if (!string.IsNullOrEmpty(txtDialCode.Text))
            {
                objCountries.DialCode = Convert.ToInt32(txtDialCode.Text);
            }

            if (!string.IsNullOrEmpty(txtVatOrGstRate.Text))
            {
                objCountries.VATOrGSTRate = Convert.ToDecimal(txtVatOrGstRate.Text);
            }

          
            objCountries.TravelCategory = Convert.ToInt32(ddlTravelCategory.SelectedValue);
            objCountries.CreatedBy = 0;

            int Result = objBACountries.InsUpdCountries(objCountries);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Countries Details Created Successfully");
                clearcontrols();
                Response.Redirect("CountriesList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Countries Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void GetCountries(int Id)
    {
        try
        {


            objCountries.Id = Id;
            DataSet ds = objBACountries.GetCountries(Id);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                    hf_Id.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                    txtKey.Text = ds.Tables[0].Rows[0]["CountryKey"].ToString();
                    txtKey.Enabled = false;
                    txtDescription.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    ddlContinent.SelectedIndex = ddlContinent.Items.IndexOf(ddlContinent.Items.FindByValue(ds.Tables[0].Rows[0]["Continent"].ToString()));
                    txtTimeZone.Text = ds.Tables[0].Rows[0]["TimeZoneUTC"].ToString();
                    txtDialCode.Text = ds.Tables[0].Rows[0]["DialCode"].ToString();
                    txtVatOrGstRate.Text = ds.Tables[0].Rows[0]["VATOrGSTRate"].ToString();
                    ddlTravelCategory.SelectedIndex = ddlTravelCategory.Items.IndexOf(ddlTravelCategory.Items.FindByValue(ds.Tables[0].Rows[0]["TravelCategory"].ToString()));
                
             
            }
        }
        catch(Exception ex)
        {
                 lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
                 ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_Id.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        ddlContinent.SelectedIndex = -1;
        txtTimeZone.Text = "";
        txtDialCode.Text = "";
        txtVatOrGstRate.Text = "";
        ddlTravelCategory.SelectedIndex = -1;
    }

    #region PublicMethods

    public void BindContinents()
    {
        try
        {
            int ContinentId = 0;
            BAContinents objBAContinents = new BAContinents();
            DataSet ds = objBAContinents.GetContinents(ContinentId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlContinent.DataSource = ds.Tables[0];
                ddlContinent.DataTextField = "ContinentDesc";
                ddlContinent.DataValueField = "ContinentId";
                ddlContinent.DataBind();
            }
            else
            {
                ddlContinent.DataSource = null;
                ddlContinent.DataBind();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindTravelCategory()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.getType();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlTravelCategory.DataSource = ds.Tables[0];
                ddlTravelCategory.DataTextField = "TypeName";
                ddlTravelCategory.DataValueField = "TypeId";
                ddlTravelCategory.DataBind();
            }
            else
            {
                ddlTravelCategory.DataSource = null;
                ddlTravelCategory.DataBind();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PublicMethods
}