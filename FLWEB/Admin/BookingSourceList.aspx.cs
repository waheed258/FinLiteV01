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

public partial class Admin_BookingSourceList : System.Web.UI.Page
{
    EMBookingSource objEMBookSource = new EMBookingSource();
    BABookingSource objBABookSource = new BABookingSource();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindBookingDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingSource.aspx");
    }
    protected void gvBookList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string BookingId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Booking Details")
        {
            // int BookingId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("BookingSource.aspx?BookId=" + HttpUtility.UrlEncode(_objBOUtiltiy.Encrypts(BookingId,true)));
        }
        if (e.CommandName == "Delete Booking Details")
        {
            // int BookingId = Convert.ToInt32(e.CommandArgument);
            deleteBookingDetails(Convert.ToInt32(BookingId));
            BindBookingDetails();
        }
    }
    protected void gvBookList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindBookingDetails();
    }
    #region PrivateMethods
    private void BindBookingDetails()
    {
        try
        {
            gvBookList.PageSize = int.Parse(ViewState["ps"].ToString());
            int BookingId = 0;
            DataSet ds = objBABookSource.GetBookingSource(BookingId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvBookList.DataSource = ds;
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
                gvBookList.DataBind();
            }
            else
            {
                gvBookList.DataSource = null;
                gvBookList.DataBind();
                Label lblEmptyMessage = gvBookList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void deleteBookingDetails(int BookingId)
    {
        try
        {
            int result = objBABookSource.DeleteBookingSource(BookingId);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindBookingDetails();
    }
    protected void gvBookList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindBookingDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
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
                    "BookingKey='" + SearchText +
                    "' OR BookingName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvBookList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvBookList.DataSource = dr.CopyToDataTable();
                    gvBookList.DataBind();
                }
                else
                {
                    gvBookList.DataSource = null;
                    gvBookList.DataBind();

                    Label lblEmptyMessage = gvBookList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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

}