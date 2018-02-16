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

public partial class Admin_RouteMilesList : System.Web.UI.Page
{
    EMRouteMiles objEMRouteMiles = new EMRouteMiles();
    BARouteMiles objBARouteMiles = new BARouteMiles();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindRoutingDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("RouteMiles.aspx");
    }
    protected void gvRMList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit Routing Details")
        {
            int RMId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("RouteMiles.aspx?RMId=" + RMId);
        }
        if (e.CommandName == "Delete Routing Details")
        {
            int RMId = Convert.ToInt32(e.CommandArgument);
            deleteRoutingDetails(RMId);
            BindRoutingDetails();
        }
    }
    protected void gvRMList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRMList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindRoutingDetails();

    }
    #region PrivateMethods
    private void BindRoutingDetails()
    {
        try
        {
            gvRMList.PageSize = int.Parse(ViewState["ps"].ToString());
            int RMId = 0;
            DataSet ds = objBARouteMiles.GetRouteMiles(RMId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvRMList.DataSource = ds.Tables[0];
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
                gvRMList.DataBind();
            }
            else
            {
                gvRMList.DataSource = null;
                gvRMList.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void deleteRoutingDetails(int RMId)
    {
        try
        {
            int result = objBARouteMiles.DeleteRouteMiles(RMId);
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
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
                    "RMKey='" + SearchText +
                    "' OR RMRouting LIKE '%" + SearchText +
                    "%' OR Convert(RMMiles,'System.String') LIKE '%" + SearchText +
                    "%' OR RMDuration LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvRMList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvRMList.DataSource = dr.CopyToDataTable();
                    gvRMList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvRMList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindRoutingDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindRoutingDetails();
    }
}