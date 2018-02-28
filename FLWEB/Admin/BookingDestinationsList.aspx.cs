using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_BookingDestinationsList : System.Web.UI.Page
{
    BABookDestinations objBADestinations = new BABookDestinations();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindDestinationDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingDestinations.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindDestinationDetails();
    }
    protected void gvDestinationsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DestId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Destination Details")
        {
            
            Response.Redirect("BookingDestinations.aspx?BookDestId=" + DestId);
        }
        else if (e.CommandName == "Delete Destination Details")
        {
            DeleteDestinationDetails(Convert.ToInt32(DestId));
            BindDestinationDetails();
        }
    }
    protected void gvDestinationsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDestinationsList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindDestinationDetails();
    }

    #region PrivateMethods
    private void BindDestinationDetails()
    {
        try
        {
            gvDestinationsList.PageSize = int.Parse(ViewState["ps"].ToString());
            int BookDestId = 0;
            DataSet ds = objBADestinations.GetBookDestinations(BookDestId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvDestinationsList.DataSource = ds.Tables[0];
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
                gvDestinationsList.DataBind();
            }
            else
            {
                gvDestinationsList.DataSource = null;
                gvDestinationsList.DataBind();
                Label lblEmptyMessage = gvDestinationsList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteDestinationDetails(int BookDestId)
    {
        try
        {
            int Result = objBADestinations.DeleteBookDestinations(BookDestId);
        }
        catch (Exception ex)
        {
            
          lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
          ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods
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
                    "BookDestKey='" + SearchText +
                    "' OR BookDestName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvDestinationsList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvDestinationsList.DataSource = dr.CopyToDataTable();
                    gvDestinationsList.DataBind();
                }
                else
                {
                    gvDestinationsList.DataSource = null;
                    gvDestinationsList.DataBind();

                    Label lblEmptyMessage = gvDestinationsList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text =_objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvDestinationsList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindDestinationDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}