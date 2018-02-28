using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_TemplateCategoryList : System.Web.UI.Page
{
    BATemplateCategory objBATemplateCategory = new BATemplateCategory();
    EMTemplateCategory objEMTemplatecategory = new EMTemplateCategory();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindTemplateCategoryDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TemplateCategory.aspx");
    }
   
    #region PrivateMethods
    private void BindTemplateCategoryDetails()
         {
             try
             {
                 gvTemplateCategoryList.PageSize = int.Parse(ViewState["ps"].ToString());
                 int Id = 0;
                 DataSet ds = objBATemplateCategory.GetTemplateCategory(Id);
                 Session["dt"] = ds.Tables[0];
                 if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     gvTemplateCategoryList.DataSource = ds.Tables[0];
                     string sortDirection = "ASC", sortExpression;
                     if (ViewState["so"] != null)
                     {
                         sortDirection = ViewState["so"].ToString();
                     }
                     if (ViewState["se"] != null)
                     {
                         sortExpression = ViewState["se"].ToString();
                         ds.Tables[0].DefaultView.Sort = sortExpression + " " + sortDirection;
                     }
                     gvTemplateCategoryList.DataBind();
                 }
                 else
                 {
                     gvTemplateCategoryList.DataSource = null;
                     gvTemplateCategoryList.DataBind();
                 }



             }
             catch(Exception ex)
             {
                 lblMsg.Text = "";
                 ExceptionLogging.SendExcepToDB(ex);
             }
         }
    private void deleteTemplateCategoryDetails(int Id)
    {
        try
        {
            int result = objBATemplateCategory.DeleteTemplateCategory(Id);
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
    protected void gvTemplateCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Edit Template")
        {

            Response.Redirect("TemplateCategory.aspx?Id=" + Id);
        }
        if (e.CommandName == "Delete Template")
        {
           
            deleteTemplateCategoryDetails(Id);
            BindTemplateCategoryDetails();
        }
   }
    protected void gvTemplateCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTemplateCategoryList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindTemplateCategoryDetails();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindTemplateCategoryDetails();
    }
    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "TC_Key='" + SearchText +
                    "' OR Description LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvTemplateCategoryList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvTemplateCategoryList.DataSource = dr.CopyToDataTable();
                    gvTemplateCategoryList.DataBind();
                }
                else
                {
                    gvTemplateCategoryList.DataSource = null;
                    gvTemplateCategoryList.DataBind();

                    Label lblEmptyMessage = gvTemplateCategoryList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvTemplateCategoryList_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["se"] = e.SortExpression;
            if (ViewState["so"] == null)
                ViewState["so"] = "ASC";
            if (ViewState["so"].ToString() == "ASC")
                ViewState["so"] = "DESC";
            else
                ViewState["so"] = "ASC";
            BindTemplateCategoryDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}