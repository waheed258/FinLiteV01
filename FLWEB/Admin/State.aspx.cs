using System;
using System.Collections.Generic;
using System.Linq;
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
public partial class State : System.Web.UI.Page
{

    EMState objEMState = new EMState();
    BAState objBAState = new BAState();
    BOUtiltiy objBOUtility = new BOUtiltiy();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindCountries();
            if (!string.IsNullOrEmpty(Request.QueryString["StateId"]))
            {
                int Stateid = Convert.ToInt32(Request.QueryString["StateId"]);
                btnSubmit.Text = "Update";
                getStateDetails(Stateid);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateState();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("StateList.aspx");
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("State.aspx");
    }
    #region PrivateMethods
       private void InsertUpdateState()
        {
            try 
            {
                objEMState.Id = Convert.ToInt32(hf_StateId.Value);
                objEMState.Key = txtKey.Text.Trim();
                objEMState.Name = txtDescription.Text.Trim();
                objEMState.Country =Convert.ToInt32(ddlCountry.SelectedValue.ToString());
                objEMState.CreatedBy = 0;
                int result = objBAState.InsUpdState(objEMState);
                if (result > 0)
                    {
                    
                        labelError.Text = objBOUtility.ShowMessage("success", "Success", "State Details Created Successfully");
                        clearcontrols();
                        Response.Redirect("StateList.aspx");
                    }
                else
                    {
                        labelError.Text = objBOUtility.ShowMessage("info", "Info", "State Details Are Not Created please try again");
                    }
                    
               
            }
            catch(Exception ex)
            {

                labelError.Text = objBOUtility.ShowMessage("danger", "Danger", ex.Message);
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    
    private void getStateDetails(int Stateid)
       {
          try
          {
              objEMState.Id = Stateid;
              DataSet ds = objBAState.GetState(Stateid);
              hf_StateId.Value = ds.Tables[0].Rows[0]["Id"].ToString();
              txtKey.Text = ds.Tables[0].Rows[0]["StateKey"].ToString();
              txtKey.Enabled = false;
              txtDescription.Text = ds.Tables[0].Rows[0]["Name"].ToString();
              ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["Country_id"].ToString()));
          }
          catch(Exception ex)
          {
              ExceptionLogging.SendExcepToDB(ex);
          }
         
          
       }
    private void clearcontrols()
       {
           hf_StateId.Value = "0";
           txtKey.Text = "";
           txtDescription.Text = "";
           ddlCountry.SelectedValue = "-1";
       }
    #endregion
    #region PublicMethods
      public void BindCountries()
       {
          try
          {
              
              DataSet ds = objBOUtility.getCountries();
              ddlCountry.DataSource = ds;
              ddlCountry.DataValueField = "Id";
              ddlCountry.DataTextField = "Name";
              ddlCountry.DataBind();
          }
          catch(Exception ex)
          {
              ExceptionLogging.SendExcepToDB(ex);
          }
       }
    #endregion
}