using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Airports : System.Web.UI.Page
{
    EMAirport objemAirport = new EMAirport();
    BAAirport objBAAirport = new BAAirport();
  
    BOUtiltiy _BOUtilities = new BOUtiltiy();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCountries();
            var qs = "0";
            if (Request.QueryString["AirportId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["AirportId"]);
                qs = _BOUtilities.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["AirportId"]))
            {
                int airportId = Convert.ToInt32(qs);
                GetAirport(airportId);
                cmdSubmit.Text = "Update";
            }
        }
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateAirport();
    }


    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropState.Focus();
        dropState.Items.Clear();
        dropCity.Items.Clear();
        Get_State_Country();
       
    }
    protected void dropState_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropCity.Focus();
        Get_City_State();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AirportList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Airports.aspx");
    }
    
    #endregion

    #region PrivateMethods
    private void InsertUpdateAirport()
    {
        try
        {
            objemAirport.AirportId = Convert.ToInt32(hf_AirportId.Value);
            objemAirport.AirKey = txtAirKey.Text;
            objemAirport.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
            objemAirport.AirportName = txtAirportName.Text;
            objemAirport.AirCountry = Convert.ToInt32(dropCountry.SelectedValue);
            objemAirport.AirState = Convert.ToInt32(dropState.SelectedValue);
            objemAirport.AirCity = Convert.ToInt32(dropCity.SelectedValue);
            objemAirport.CountryDetails = Convert.ToInt32(chkCountryDetails.Checked);

            int Result = objBAAirport.InsUpdtAirport(objemAirport);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtilities.ShowMessage("success", "Success", "Airport Details created Successfully");
                ClearControls();
                Response.Redirect("AirportList.aspx");

            }
            else
            {
                lblMsg.Text = _BOUtilities.ShowMessage("info", "Info", "Airport Details  was not created please try again");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtilities.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);

        }
    }

    private void GetAirport(int AirportId)
    {
        try
        {
            DataSet ds = objBAAirport.GetAirport(AirportId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_AirportId.Value = ds.Tables[0].Rows[0]["AirportId"].ToString();
                txtAirKey.Text = ds.Tables[0].Rows[0]["AirKey"].ToString();
                txtAirKey.Enabled = false;
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Deactivate"]);
                txtAirportName.Text = ds.Tables[0].Rows[0]["AirportName"].ToString();
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(ds.Tables[0].Rows[0]["AirCountry"].ToString()));
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(ds.Tables[0].Rows[0]["AirCountry"].ToString()));
                Get_State_Country();
                dropState.SelectedIndex = dropState.Items.IndexOf(dropState.Items.FindByValue(ds.Tables[0].Rows[0]["AirState"].ToString()));
                Get_City_State();
                dropCity.SelectedIndex = dropCity.Items.IndexOf(dropCity.Items.FindByValue(ds.Tables[0].Rows[0]["AirCity"].ToString()));
                chkCountryDetails.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CountryDetails"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
            Label1.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }

    private void BindCountries()
    {
        try
        {
            BOUtiltiy _BOUtilities = new BOUtiltiy();
            DataSet ds = _BOUtilities.getCountries();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCountry.DataSource = ds.Tables[0];
                dropCountry.Items.Add(new ListItem("-Select-", "-1"));
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
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
            Label1.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }

    private void Get_State_Country()
    {
        try
        {

            dropState.Items.Clear();
            DataSet ds = new DataSet();
            int country_id = Convert.ToInt32(dropCountry.SelectedValue.ToString());
            ds = _BOUtilities.get_State_Country(country_id);
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
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void Get_City_State()
    {
        try
        {

           
            DataSet ds = new DataSet();
            int state_id = Convert.ToInt32(dropState.SelectedValue.ToString());
            ds = _BOUtilities.get_City_State(state_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropCity.DataSource = ds.Tables[0];
                dropCity.Items.Clear();
                dropCity.Items.Add(new ListItem("-Select-", "-1"));
                dropCity.DataTextField = "Name";
                dropCity.DataValueField = "Id";
                dropCity.DataBind();
            }
            else
            {
                dropState.DataSource = null;
                dropState.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void ClearControls()
    {
        hf_AirportId.Value = "0";
        txtAirKey.Text = "";
        txtAirportName.Text = "";
        chkDeactivate.Checked = false;
        chkCountryDetails.Checked = false;
        dropCity.SelectedValue = "-1";
        dropState.SelectedValue = "-1";
        dropCountry.SelectedValue = "-1";
    } 

    #endregion

    protected void txtAirKey_TextChanged(object sender, EventArgs e)
    {
        chkDeactivate.Focus();
    }
    protected void txtAirportName_TextChanged(object sender, EventArgs e)
    {
        dropCountry.Focus();
    }
    protected void dropCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropCity.Focus();
    }
}