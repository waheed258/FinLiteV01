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

public partial class Admin_BookingSource : System.Web.UI.Page
{
    EMBookingSource objEMBookSource = new EMBookingSource();
    BABookingSource objBABookSource = new BABookingSource();
    BOUtiltiy objBOUtilty = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["BookId"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["BookId"]);
                qs = objBOUtilty.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["BookId"]))
            {
                int BookingId = Convert.ToInt32(qs);
                btnSubmit.Text = "Update";
                GetBookSource(BookingId);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BookSourceInsertUpdate();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingSourceList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingSource.aspx");
    }
    #region PrivateMethods
        private void BookSourceInsertUpdate()
         {
             try
             {
                 objEMBookSource.Id = Convert.ToInt32(hf_BookId.Value);
                 objEMBookSource.Key = txtKey.Text.Trim();
                 objEMBookSource.Desc = txtDescription.Text.Trim();
                 objEMBookSource.Deactivate = Convert.ToInt32(ChkDeactivate.Checked);
                 objEMBookSource.CreatedBy = 0;
                 int result = objBABookSource.InsUpdBookingSource(objEMBookSource);
                 if (result > 0)
                 {
                     
                         labelError.Text = objBOUtilty.ShowMessage("success", "Success", "Booking Source Details Created Successfully");
                         clearcontrols();
                         Response.Redirect("BookingSourceList.aspx");
                 }
                 else
                 {
                     labelError.Text = objBOUtilty.ShowMessage("info", "Info", "Booking Source Details are not created please try again");
                  }
                     
                 
             }
             catch (Exception ex)
             {
                 labelError.Text = objBOUtilty.ShowMessage("danger", "Danger", ex.Message);
                 ExceptionLogging.SendExcepToDB(ex);
             }
         }
        private void  GetBookSource(int BookingId)
        {
            try
            {
                objEMBookSource.Id = BookingId;
                DataSet ds = objBABookSource.GetBookingSource(BookingId);
                if(ds.Tables.Count > 0)
                {
                    hf_BookId.Value=ds.Tables[0].Rows[0]["BookingId"].ToString();
                    txtKey.Text = ds.Tables[0].Rows[0]["BookingKey"].ToString();
                    txtKey.Enabled = false;
                    txtDescription.Text = ds.Tables[0].Rows[0]["BookingName"].ToString();
                    ChkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
       private void clearcontrols()
          {
              hf_BookId.Value = "0";
              txtKey.Text = "";
              txtDescription.Text = "";
              ChkDeactivate.Checked = false;
          }
    #endregion
}