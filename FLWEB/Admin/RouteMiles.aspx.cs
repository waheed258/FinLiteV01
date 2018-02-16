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


public partial class Admin_RouteMiles : System.Web.UI.Page
{
    EMRouteMiles objEMRouteMiles = new EMRouteMiles();
    BARouteMiles objBARouteMiles = new BARouteMiles();
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["RMId"]))
            {
                int RMId = Convert.ToInt32(Request.QueryString["RMId"]);
                btnSubmit.Text = "Update";
                getRouteMiles(RMId);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        InsUpdRouteMiles();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RouteMilesList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("RouteMiles.aspx");
    }
    #region PrivateMethods
       private void InsUpdRouteMiles()
         {
             try 
              {
                  objEMRouteMiles.Id = Convert.ToInt32(hf_RMId.Value);
                  objEMRouteMiles.Key = txtKey.Text;
                  objEMRouteMiles.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
                  objEMRouteMiles.Routing = txtRouting.Text.Trim();
                  objEMRouteMiles.Miles = Convert.ToInt32(txtMiles.Text);
                  objEMRouteMiles.Duration = txtDuration.Text.Trim();
                  int result = objBARouteMiles.InsUpdRouteMiles(objEMRouteMiles);
                  if(result > 0)
                  {
                      
                         labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Routing Details Created Successfully");
                         clearcontrols();
                         Response.Redirect("RouteMilesList.aspx");
                  }
                  else
                  {
                      labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Routing Details are not created please try again");
                  }
                     
                  
              }
             catch (Exception ex)
              {
                  labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
                  ExceptionLogging.SendExcepToDB(ex);
              }

         }
       
    private void getRouteMiles(int RMId)
       {
           try 
           {
               objEMRouteMiles.Id = RMId;
               DataSet ds = objBARouteMiles.GetRouteMiles(RMId);
               if (ds.Tables.Count > 0)
               {
                   hf_RMId.Value = ds.Tables[0].Rows[0]["RMId"].ToString();
                   txtKey.Text = ds.Tables[0].Rows[0]["RMKey"].ToString();
                   txtKey.Enabled = false;
                   chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RMDeactivate"]);
                   txtRouting.Text = ds.Tables[0].Rows[0]["RMRouting"].ToString();
                   txtMiles.Text = ds.Tables[0].Rows[0]["RMMiles"].ToString();
                   txtDuration.Text = ds.Tables[0].Rows[0]["RMDuration"].ToString();
               }
           }
           catch(Exception ex)
           {
               ExceptionLogging.SendExcepToDB(ex);

           }
       }
    private void clearcontrols()
         {
             hf_RMId.Value = "0";
             txtKey.Text = "";
             chkDeactivate.Checked = false;
             txtRouting.Text = "";
             txtMiles.Text = "";
             txtDuration.Text = "";
         }
    #endregion
}