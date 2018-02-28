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

public partial class Admin_ForeignCurrencyList : System.Web.UI.Page
{
    EMForeignCurrency objEMForeignCurrency = new EMForeignCurrency();
    BAForeignCurrency objBAForeignCurrency = new BAForeignCurrency();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCurrencyDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForeignCurrency.aspx");
    }
    protected void gvForeignCurrencyList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit Currency Details")
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ForeignCurrency.aspx?Id=" + Id);
        }
        if (e.CommandName == "Delete Currency Details")
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            deleteCurrencyDetails(Id);
            BindCurrencyDetails();
        }
    }
    protected void gvForeignCurrencyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvForeignCurrencyList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindCurrencyDetails();
    }
    #region PrivateMethods
       private void BindCurrencyDetails()
         {
             try
             {
                 gvForeignCurrencyList.PageSize = int.Parse(ViewState["ps"].ToString());
                 int Id = 0;
                 DataSet ds = objBAForeignCurrency.GetForeignCurrency(Id);
                 Session["dt"] = ds.Tables[0];
                 if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     gvForeignCurrencyList.DataSource = ds.Tables[0];
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
                     gvForeignCurrencyList.DataBind();
                 }
                 else
                 {
                     gvForeignCurrencyList.DataSource = null;
                     gvForeignCurrencyList.DataBind();
                     Label lblEmptyMessage = gvForeignCurrencyList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                     lblEmptyMessage.Text = "Currently there are no records in System";
                 }



             }
             catch(Exception ex)
             {
                 lblMsg.Text = "";
                 ExceptionLogging.SendExcepToDB(ex);
             }
         }
       private void deleteCurrencyDetails(int Id)
         {
            try
            {
                int result = objBAForeignCurrency.DeleteForeignCurrency(Id);
            }
           catch(Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
         }

    #endregion
       protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
       {
           ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
           SearchItemFromList(txtSearch.Text.Trim());
          // BindCurrencyDetails();
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
                       "FC_Key='" + SearchText +
                       "' OR Description LIKE '%" + SearchText +
                       "%' OR ActionDesc LIKE '%" + SearchText +
                       "%' OR TemplateDesc LIKE '%" + SearchText +
                       "%' OR ActivateDesc LIKE '%" + SearchText + "%'");

                   if (dr.Count() > 0)
                   {
                       gvForeignCurrencyList.PageSize = int.Parse(ViewState["ps"].ToString());
                       gvForeignCurrencyList.DataSource = dr.CopyToDataTable();
                       gvForeignCurrencyList.DataBind();
                   }
                   else
                   {
                       gvForeignCurrencyList.DataSource = null;
                       gvForeignCurrencyList.DataBind();

                       Label lblEmptyMessage = gvForeignCurrencyList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
       protected void gvForeignCurrencyList_Sorting(object sender, GridViewSortEventArgs e)
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
               BindCurrencyDetails();
           }
           catch (Exception ex)
           {
               lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
               ExceptionLogging.SendExcepToDB(ex);
           }
       }
}