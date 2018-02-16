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

public partial class Admin_SupplierChoice : System.Web.UI.Page
{
    EMSupplierChoice objEMSupChoice = new EMSupplierChoice();
    BALSupplierChoice objBASupChoice = new BALSupplierChoice();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["SupplierChoiceId"]))
            {
                int ChoiceId = Convert.ToInt32(Request.QueryString["SupplierChoiceId"].ToString());
                GetSupplierChoice(ChoiceId);
                cmdSubmit.Text = "Update";
            }

        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateSupChoice();
    }
    private void InsertUpdateSupChoice()
    {
        try
        {
            objEMSupChoice.SupplierChoiceId = Convert.ToInt32(hf_SupChoiceId.Value);
            objEMSupChoice.ChoiceKey = txtChoiceKey.Text;
            objEMSupChoice.ChoiceDescription = txtChoiceDescription.Text;
            objEMSupChoice.ChoiceDeactivate = Convert.ToInt32(chkDeactivate.Checked);
            objEMSupChoice.CreatedBy = 0;

            int Result = objBASupChoice.InsUpdSuppChoice(objEMSupChoice);
            if(Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Supplier Choice Created Successfully");
                ClearControls();
                Response.Redirect("SupplierChoiceList.aspx");
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Supplier Choice not created,Please Try Again");
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetSupplierChoice(int SupplierChoiceId)
    {
        try
        {            
            DataSet ds = objBASupChoice.GetSuppChoice(SupplierChoiceId);

            if(ds.Tables[0].Rows.Count > 0)
            {
                hf_SupChoiceId.Value = ds.Tables[0].Rows[0]["SupplierChoiceId"].ToString();
                txtChoiceKey.Text = ds.Tables[0].Rows[0]["ChoiceKey"].ToString();
                txtChoiceKey.Enabled = false;
                txtChoiceDescription.Text = ds.Tables[0].Rows[0]["ChoiceDescription"].ToString();
                chkDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChoiceDeactivate"]);
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierChoiceList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierChoice.aspx");
    }

    void ClearControls()
    {
        hf_SupChoiceId.Value = "0";
        txtChoiceKey.Text = "";
        txtChoiceDescription.Text = "";
        chkDeactivate.Checked = false;

    }
}