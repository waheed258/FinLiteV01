using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;

public partial class Admin_GroupTypes : System.Web.UI.Page
{
    EMGroupTypes objGroupTypes = new EMGroupTypes();
    BAGroupTypes objBAGroupTypes = new BAGroupTypes();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                int GroupId = Convert.ToInt32(Request.QueryString["Id"]);
                GetGroupTypes(GroupId);
                cmdSubmit.Text = "Update";
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateGroupType();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupTypesList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupTypes.aspx");
    }

    #region PrivateMethods

    private void InsertUpdateGroupType()
    {
        try
        {
            objGroupTypes.Id = Convert.ToInt32(hf_Id.Value);
            objGroupTypes.Code = txtKey.Text;
            objGroupTypes.Name = txtDescription.Text;
            objGroupTypes.GroupType = ddlGroupType.SelectedValue;
            objGroupTypes.CreatedBy = 0;

            int Result = objBAGroupTypes.InsUpdateGroupTypes(objGroupTypes);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "GroupTypes Created Successfully");
                clearcontrols();
                Response.Redirect("GroupTypesList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "GroupTypes Details was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetGroupTypes(int Id)
    {
        try
        {


            objGroupTypes.Id = Id;
            DataSet ds = objBAGroupTypes.GetGroupTypes(Id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_Id.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                txtKey.Enabled= false;
                txtDescription.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                ddlGroupType.SelectedIndex = ddlGroupType.Items.IndexOf(ddlGroupType.Items.FindByValue(ds.Tables[0].Rows[0]["GroupType"].ToString()));
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        
    }

    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_Id.Value = "0";
        txtKey.Text = "";
        txtDescription.Text = "";
        ddlGroupType.SelectedIndex = -1;

    }
}