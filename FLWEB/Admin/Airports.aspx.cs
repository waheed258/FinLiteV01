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

public partial class Admin_Airports : System.Web.UI.Page
{
    EMAirport objemAirport = new EMAirport();
    BAAirport objBAAirport = new BAAirport();
    BALServicefee _objBalservice = new BALServicefee();
    BOUtiltiy _BOUtilities = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindCountries();
            if(!string.IsNullOrEmpty(Request.QueryString["AirportId"]))
            {
                int airportId =Convert.ToInt32(Request.QueryString["AirportId"].ToString());
                GetAirport(airportId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateAirport();
    }

    private void InsertUpdateAirport()
    {
        try
        {
            objemAirport.AirportId = Convert.ToInt32(hf_AirportId.Value);
            objemAirport.AirKey = txtAirKey.Text;
            objemAirport.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
            objemAirport.AirportName = txtAirportName.Text;
            objemAirport.AirCountry = Convert.ToInt32(dropCountry.SelectedValue);
            objemAirport.AirCity = Convert.ToInt32(dropCity.SelectedValue);
            objemAirport.CountryDetails = Convert.ToInt32(chkCountryDetails.Checked);

            int Result = objBAAirport.InsUpdtAirport(objemAirport);
            if(Result > 0)
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
        catch(Exception ex)
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
            if(ds.Tables[0].Rows.Count > 0)
            {
                hf_AirportId.Value = ds.Tables[0].Rows[0]["AirportId"].ToString();
                txtAirKey.Text = ds.Tables[0].Rows[0]["AirKey"].ToString();
                txtAirKey.Enabled = false;
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Deactivate"]);
                txtAirportName.Text = ds.Tables[0].Rows[0]["AirportName"].ToString();
                dropCountry.SelectedIndex = dropCountry.Items.IndexOf(dropCountry.Items.FindByValue(ds.Tables[0].Rows[0]["AirCountry"].ToString()));
                Get_City_Country();
                dropCity.SelectedIndex = dropCity.Items.IndexOf(dropCity.Items.FindByValue(ds.Tables[0].Rows[0]["AirCity"].ToString()));
                chkCountryDetails.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CountryDetails"]);
            }
        }
        catch(Exception ex)
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

    public void Get_City_Country()
    {
        try
        {
            dropCity.Items.Clear();
            DataSet ds = new DataSet();
            int country_id = Convert.ToInt32(dropCountry.SelectedValue.ToString());
            ds = _BOUtilities.get_City_Country(country_id);
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
                dropCity.DataSource = null;
                dropCity.DataBind();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
            Label1.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }

    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_City_Country();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AirportList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Airports.aspx");
    }

    void ClearControls()
    {
        hf_AirportId.Value = "0";
        txtAirKey.Text = "";
        txtAirportName.Text = "";
        chkDeactivate.Checked = false;
        chkCountryDetails.Checked = false;
        dropCity.SelectedValue = "-1";
        dropCountry.SelectedValue = "-1";
    }
}