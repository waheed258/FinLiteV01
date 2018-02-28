using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MainAccountList : System.Web.UI.Page
{
    BALMainAccount _objBALMainAccount = new BALMainAccount();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindMainAccounts();
        }
    }
    protected void btnMainAcAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainAccounts.aspx");
    }

    private void BindMainAccounts()
    {
        try
        {
            gvMainAccList.PageSize = int.Parse(ViewState["ps"].ToString());
           // int AirportId = 0;
            DataSet ds = _objBALMainAccount.getMainAccounts();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvMainAccList.DataSource = ds;
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
                gvMainAccList.DataBind();
            }
            else
            {
                gvMainAccList.DataSource = null;
                gvMainAccList.DataBind();
                Label lblEmptyMessage = gvMainAccList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvMainAccList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMainAccList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindMainAccounts();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindMainAccounts();
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
                    "MainAccCode='" + SearchText +
                    "' OR MainAcName LIKE '%" + SearchText +
                    "%' OR AcType LIKE '%" + SearchText +
                    "%' OR BaseCurrency LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvMainAccList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvMainAccList.DataSource = dr.CopyToDataTable();
                    gvMainAccList.DataBind();
                }
                else
                {
                    gvMainAccList.DataSource = null;
                    gvMainAccList.DataBind();

                    Label lblEmptyMessage = gvMainAccList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvMainAccList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindMainAccounts();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}