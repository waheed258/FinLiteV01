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

public partial class Admin_TicketType : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BATicketType objBATicketType = new BATicketType();
    EMTicketType objEMTicketType = new EMTicketType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["TId"]))
            {
                int TId = Convert.ToInt32(Request.QueryString["TId"]);
                btnSubmit.Text = "Update";
                GetTicketType(TId);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateTicketType();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketTypeList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketType.aspx");
    }
    #region PrivateMethods
       private void InsertUpdateTicketType()
        {
            try
            {
                objEMTicketType.TId = Convert.ToInt32(hf_TId.Value);
                objEMTicketType.TKey = txtKey.Text.Trim();
                objEMTicketType.TDesc = txtDescription.Text.Trim();
                objEMTicketType.CreatedBy = 0;
                int result = objBATicketType.InsUpdTicketType(objEMTicketType);
                if (result > 0)
                {

                    labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Ticket Type Details Created Successfully");
                    clearcontrols();
                    Response.Redirect("TicketTypeList.aspx");
                }
                else
                {
                    labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Ticket Type Details are not created Successfully please try again");
                }
                    
                
            }
            catch (Exception ex)
            {
                labelError.Text = _objBOUtility.ShowMessage("danger", "Danger", ex.Message);
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    private void GetTicketType(int TId)
    {
        try
        {
            objEMTicketType.TId = TId;
            DataSet ds = objBATicketType.GetTicketType(TId);
            if(ds.Tables.Count > 0)
            {
                hf_TId.Value = ds.Tables[0].Rows[0]["TId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["TKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["TDesc"].ToString();
            }
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void clearcontrols()
       {
           hf_TId.Value = "0";
           txtKey.Text = "";
           txtDescription.Text = "";
       }
    #endregion
}