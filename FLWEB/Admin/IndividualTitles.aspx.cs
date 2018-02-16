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
public partial class Admin_IndividualTitles : System.Web.UI.Page
{
    EMIndividualTitles objEMTitles = new EMIndividualTitles();
    BAIndividualTitles objBATitles = new BAIndividualTitles();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           if(!string.IsNullOrEmpty(Request.QueryString["TitleId"]))
           {
               int Titleid = Convert.ToInt32(Request.QueryString["TitleId"].ToString());
               GetIndivTitle(Titleid);
               cmdSubmit.Text = "Update";
           }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateTitles();
    }

    private void InsertUpdateTitles()
    {
        try
        {
            objEMTitles.TitleId = Convert.ToInt32(hf_TitleId.Value);
            objEMTitles.TitleKey = txtTitleKey.Text;
            objEMTitles.TitleDescription = txtTitleDescription.Text;
            objEMTitles.Gender = dropGender.SelectedItem.ToString();
            objEMTitles.CreatedBy = 0;

            int Result = objBATitles.InsUpdtIndivTitles(objEMTitles);
            if(Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Individual Details Created Successfully");
                ClearControls();
                Response.Redirect("IndividualTitlesList.aspx");
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Individual Titles not created.Please try again");
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    public void GetIndivTitle(int TitleId)
    {
        try
        {
            DataSet ds = objBATitles.GetIndivTitle(TitleId);
            if(ds.Tables[0].Rows.Count > 0)
            {
                hf_TitleId.Value = ds.Tables[0].Rows[0]["TitleId"].ToString();
                txtTitleKey.Text = ds.Tables[0].Rows[0]["TitleKey"].ToString();
                txtTitleKey.Enabled = false;
                txtTitleDescription.Text = ds.Tables[0].Rows[0]["TitleDescription"].ToString();
                dropGender.SelectedIndex = dropGender.Items.IndexOf(dropGender.Items.FindByText(ds.Tables[0].Rows[0]["Gender"].ToString()));
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
        Response.Redirect("IndividualTitlesList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndividualTitles.aspx");
    }

    void ClearControls()
    {
        hf_TitleId.Value = "0";
        txtTitleKey.Text = "";
        txtTitleDescription.Text = "";
        dropGender.SelectedIndex = -1;
    }
}