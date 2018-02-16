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

public partial class Admin_TemplateCategory : System.Web.UI.Page
{
     BOUtiltiy _objBOUtility = new BOUtiltiy();
     BATemplateCategory objBATemplateCategory=new BATemplateCategory();
     EMTemplateCategory objEMTemplatecategory=new EMTemplateCategory();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);
                btnSubmit.Text = "Update";
                GetTemplateCategory(Id);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateTemplateCategory();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemplateCategoryList.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemplateCategory.aspx");
    }
    #region PrivateMethods
       private void InsertUpdateTemplateCategory()
       {
           try
           {
               objEMTemplatecategory.Id=Convert.ToInt32(hf_Id.Value);
               objEMTemplatecategory.Key=txtKey.Text.Trim();
               objEMTemplatecategory.Description=txtDescription.Text.Trim();
               objEMTemplatecategory.CreatedBy=0;
               int result=objBATemplateCategory.InsUpdTemplateCategory(objEMTemplatecategory);
               if (result > 0)
                {

                    labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Template Category Details Created Successfully");
                    clearcontrols();
                    Response.Redirect("TemplateCategoryList.aspx");
                }
                else
                {
                    labelError.Text = _objBOUtility.ShowMessage("info", "Info", "Template Category Details are not created Successfully please try again");
                }
           }
           catch(Exception ex)
           {
               ExceptionLogging.SendExcepToDB(ex);
           }
       }
       private void GetTemplateCategory(int Id)
       {
           try
           {
               objEMTemplatecategory.Id = Id;
               DataSet ds = objBATemplateCategory.GetTemplateCategory(Id);
               if (ds.Tables.Count > 0)
               {
                   hf_Id.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                   txtKey.Text = ds.Tables[0].Rows[0]["TC_Key"].ToString();
                   txtKey.Enabled = false;
                   txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
               }
           }
           catch(Exception ex)
           {
               ExceptionLogging.SendExcepToDB(ex);
           }
       }
       private void clearcontrols()
       {
           hf_Id.Value = "0";
           txtKey.Text = "";
           txtDescription.Text = "";
       }
    #endregion
}