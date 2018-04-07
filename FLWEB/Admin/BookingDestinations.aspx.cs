using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;


public partial class Admin_BookingDestinations : System.Web.UI.Page
{
    EMBookDestinations objDestinations = new EMBookDestinations();
    BABookDestinations objBADestinations = new BABookDestinations();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["BookDestId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["BookDestId"]);
                qs = _objBOUtiltiy.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["BookDestId"]))
            {
                int DestId = Convert.ToInt32(qs);
                GetDestinationsDetails(DestId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsUpdDestinations();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingDestinationsList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingDestinations.aspx");
    }

    protected void txtKey_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {

    }
#endregion

    #region PrivateMethods
    private void InsUpdDestinations()
    {
        try
        {
            objDestinations.BookDestId = Convert.ToInt32(hf_BookDestId.Value);
            objDestinations.BookDestKey = txtKey.Text;
            objDestinations.BookDestName = txtDescription.Text;
            objDestinations.BookDestStatus = Convert.ToInt32(chkDeActivate.Checked);
            objDestinations.CreatedBy = 0;

            int Result = objBADestinations.InsUpdBookDestinations(objDestinations);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Destinations Details created Successfully");
                clearcontrols();
                Response.Redirect("BookingDestinationsList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Destination Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetDestinationsDetails(int BookDestId)
    {

        try
        {
            objDestinations.BookDestId = BookDestId;
            DataSet ds = new DataSet();
            ds = objBADestinations.GetBookDestinations(BookDestId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_BookDestId.Value = ds.Tables[0].Rows[0]["BookDestId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["BookDestKey"].ToString();
                txtKey.Enabled = false;
                txtDescription.Text = ds.Tables[0].Rows[0]["BookDestName"].ToString();
                chkDeActivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["BookDestStatus"]);
            }
        }
        catch (Exception ex)
        {
            
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_BookDestId.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        chkDeActivate.Checked = false;
    }
   
}