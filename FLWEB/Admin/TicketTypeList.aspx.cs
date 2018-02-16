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
public partial class Admin_TicketTypeList : System.Web.UI.Page
{
    EMTicketType objEMTicketType = new EMTicketType();
    BATicketType objBATickettype = new BATicketType();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindTicketDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketType.aspx");
    }
    protected void gvTicketTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit Ticket Details")
        {
            int TId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("TicketType.aspx?TId=" + TId);
        }
        if (e.CommandName == "Delete Ticket Details")
        {
            int TId = Convert.ToInt32(e.CommandArgument);
            deleteTicketDetails(TId);
            BindTicketDetails();
        }
    }
    protected void gvTicketTypeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketTypeList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindTicketDetails();
    }
    #region Privatemethods
       private void BindTicketDetails()
         {
             try
             {
                 gvTicketTypeList.PageSize = int.Parse(ViewState["ps"].ToString());
                 int TId = 0;
                 DataSet ds = objBATickettype.GetTicketType(TId);
                 Session["dt"] = ds.Tables[0];
                 if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     gvTicketTypeList.DataSource = ds.Tables[0];
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
                     gvTicketTypeList.DataBind();
                 }
                 else
                 {
                     gvTicketTypeList.DataSource = null;
                     gvTicketTypeList.DataBind();
                 }



             }
             catch(Exception ex)
             {
                 lblMsg.Text = "";
                 ExceptionLogging.SendExcepToDB(ex);
             }
         }
       private void deleteTicketDetails(int TId)
       {
           try
           {
               int result = objBATickettype.DeleteTicketType(TId);
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
          // BindTicketDetails();
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
                       "TKey='" + SearchText +
                       "' OR TDesc LIKE '%" + SearchText + "%'");

                   if (dr.Count() > 0)
                   {
                       gvTicketTypeList.PageSize = int.Parse(ViewState["ps"].ToString());
                       gvTicketTypeList.DataSource = dr.CopyToDataTable();
                       gvTicketTypeList.DataBind();
                   }
               }
           }
           catch (Exception ex)
           {
               lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
               ExceptionLogging.SendExcepToDB(ex);
           }
       }
       protected void gvTicketTypeList_Sorting(object sender, GridViewSortEventArgs e)
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
               BindTicketDetails();
           }
           catch (Exception ex)
           {
               lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
               ExceptionLogging.SendExcepToDB(ex);
           }
       }
}