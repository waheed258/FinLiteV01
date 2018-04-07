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

public partial class Admin_ForeignCurrency : System.Web.UI.Page
{
    EMForeignCurrency objEMForeignCurrency = new EMForeignCurrency();
    BAForeignCurrency objBAForeignCurrency = new BAForeignCurrency();
    BOUtiltiy objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var qs = "0";
            if (Request.QueryString["Id"] == null)
            {
                qs = "0";
            }
            else
            {
                string getId = Convert.ToString(Request.QueryString["Id"]);
                qs = objBOUtility.Decrypts(HttpUtility.UrlDecode(getId),true);

            }
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                int Id = Convert.ToInt32(qs);
                btnSubmit.Text = "Update";
                GetCurrencyDetails(Id);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateForeignCurrency();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForeignCurrencyList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForeignCurrency.aspx");
    }
    #region PrivateMethods
        private void InsertUpdateForeignCurrency()
         {
           try
           {
               objEMForeignCurrency.Id = Convert.ToInt32(hf_FcId.Value);
               objEMForeignCurrency.Key = txtKey.Text.Trim();
               objEMForeignCurrency.Description = txtDescription.Text.Trim();
               objEMForeignCurrency.Action = Convert.ToInt32(ddlAction.SelectedValue.ToString());
               objEMForeignCurrency.Template = Convert.ToInt32(chkTemplate.Checked);
               objEMForeignCurrency.Deactivate = Convert.ToInt32(chkDeactivate.Checked);
               objEMForeignCurrency.CreatedBy = 0;
               int Result = objBAForeignCurrency.InsertUpdateForeignCurrency(objEMForeignCurrency);
               if (Result > 0)
               {

                   labelError.Text = objBOUtility.ShowMessage("success", "Success", "Foreign Currency Details Created Successfully.");
                   clearcontrols();
                   Response.Redirect("ForeignCurrencyList.aspx");
               }

               else
               {
                   labelError.Text = objBOUtility.ShowMessage("info", "Info", "Foreign Currency Details are not created please try again");
               }
           }

           catch (Exception ex)
           {

               labelError.Text = objBOUtility.ShowMessage("danger", "Danger", ex.Message);
               ExceptionLogging.SendExcepToDB(ex);
           }
        }
    private void  GetCurrencyDetails(int Id)
        {
            try 
            {
                objEMForeignCurrency.Id = Id;
                DataSet ds = objBAForeignCurrency.GetForeignCurrency(Id);
                if(ds.Tables.Count > 0)
                 {
                     hf_FcId.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                     txtKey.Text = ds.Tables[0].Rows[0]["FC_Key"].ToString();
                     txtKey.Enabled = false;
                     txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                     ddlAction.SelectedIndex = ddlAction.Items.IndexOf(ddlAction.Items.FindByValue(ds.Tables[0].Rows[0]["Action"].ToString()));
                     chkTemplate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Template"]);
                     chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DeActivate"]);
                 }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    private void clearcontrols()
        {
            hf_FcId.Value = "0";
            txtKey.Text = "";
            txtDescription.Text = "";
            ddlAction.SelectedValue = "-1";
            chkTemplate.Checked = false;
            chkDeactivate.Checked = false;
        }
    #endregion
    protected void txtKey_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}