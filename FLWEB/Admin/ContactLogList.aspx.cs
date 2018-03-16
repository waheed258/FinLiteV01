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

public partial class Admin_ContactLogList : System.Web.UI.Page
{
    EMContactLog objContactLog = new EMContactLog();
    BAContactLog objBALog = new BAContactLog();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindContactLog();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactLog.aspx");
    }
    protected void gvConLogList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string logId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Log")
        {
            Response.Redirect("ContactLog.aspx?LogId=" + HttpUtility.UrlEncode(_BOUtility.Encrypts(logId,true)));
        }
        if(e.CommandName == "Delete Log")
        {
            DeleteLog(Convert.ToInt32(logId));
            BindContactLog();
        }
    }
    protected void gvConLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConLogList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindContactLog();
    }

    private void BindContactLog()
    {
        try
        {
            gvConLogList.PageSize = int.Parse(ViewState["ps"].ToString());
            int LogId = 0;
            DataSet ds = objBALog.GetContactLog(LogId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvConLogList.DataSource = ds.Tables[0];
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
                gvConLogList.DataBind();
            }
            else
            {
                gvConLogList.DataSource = null;
                gvConLogList.DataBind();

                Label lblEmptyMessage = gvConLogList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }

        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteLog(int LogId)
    {
        int Result = objBALog.DeleteContactLog(LogId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindContactLog();
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
                    "LogKey='" + SearchText +
                    "' OR LogDescription LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvConLogList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvConLogList.DataSource = dr.CopyToDataTable();
                    gvConLogList.DataBind();
                }
                else
                {
                    gvConLogList.DataSource = null;
                    gvConLogList.DataBind();

                    Label lblEmptyMessage = gvConLogList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text =_BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvConLogList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindContactLog();
        }
        catch (Exception ex)
        {
            lblMsg.Text =_BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}