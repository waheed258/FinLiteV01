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

public partial class Admin_CitiesMaster : System.Web.UI.Page
{
    EMCitiesMaster objemCity = new EMCitiesMaster();
    BACities objBACities = new BACities();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindCountries();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                int CityId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                GetCities(CityId);
                cmdSubmit.Text = "Update";
            }
        }

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCities();
    }
    
    private void InsertUpdateCities()
    {
        try
        {
            objemCity.Id = Convert.ToInt32(hf_CitiesId.Value);
            objemCity.CityKey = txtCityKey.Text;
            objemCity.Name = txtDescription.Text;
            objemCity.CountryId = Convert.ToInt32(dropCountry.SelectedValue);
            objemCity.CityTimezoneUtc = txtTimeZone.Text;
            objemCity.state_id = Convert.ToInt32(dropState.SelectedValue);
            objemCity.CreatedBy = 0;

            int Result = objBACities.InsUpdateCities(objemCity);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Cities Details Created Successfully");
                ClearControls();
                Response.Redirect("CitiesList.aspx");
                
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Cities Details was not created please try again");
            }
        }


        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetCities(int Id)
    {
        try
        {
            
            DataSet ds = objBACities.GetCities(Id);
            if(ds.Tables[0].Rows.Count > 0)
            {
                hf_CitiesId.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                txtCityKey.Text = ds.Tables[0].Rows[0]["CityKey"].ToString();
                txtCityKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryId"].ToString()));
                Get_State_Country();
                dropState.SelectedIndex = dropState.Items.IndexOf(dropState.Items.FindByValue(ds.Tables[0].Rows[0]["state_id"].ToString()));
                txtTimeZone.Text = ds.Tables[0].Rows[0]["CityTimezoneUtc"].ToString();
            }


        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CitiesList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CitiesMaster.aspx");
    }

    private void BindCountries()
    {
        try
        {

            DataSet ds = _BOUtility.getCountries();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCountry.DataSource = ds.Tables[0];
                dropCountry.DataTextField = "Name";
                dropCountry.DataValueField = "Id";
                dropCountry.DataBind();
            }
            else
            {
                dropCountry.DataSource = null;
                dropCountry.DataBind();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void Get_State_Country()
    {
        try
        {

            dropState.Items.Clear();
            DataSet ds = new DataSet();
            int country_id = Convert.ToInt32(dropCountry.SelectedValue.ToString());
            ds = _BOUtility.get_State_Country(country_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropState.DataSource = ds.Tables[0];
                dropState.Items.Clear();
                dropState.Items.Add(new ListItem("-Select-", "-1"));
                dropState.DataTextField = "Name";
                dropState.DataValueField = "Id";
                dropState.DataBind();
            }
            else
            {
                dropState.DataSource = null;
                dropState.DataBind();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_State_Country();
    }

    void ClearControls()
    {
        hf_CitiesId.Value = "0";
        txtCityKey.Text = "";
        txtDescription.Text = "";
        txtTimeZone.Text = "";
        dropCountry.SelectedValue = "-1";
        dropState.SelectedValue = "-1";
    }
}