using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
public partial class Admin_CharteredAccountsList : System.Web.UI.Page
{
    BALCharteredAccounts _objcharAcc = new BALCharteredAccounts();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        //gvCharteredAccountsList

        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCharAccList();
        }
    }


    #region  BindMethods

    private void BindCharAccList()
    {

        try
        {
            gvCharteredAccountsList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = _objcharAcc.BindCharAccList();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCharteredAccountsList.DataSource = ds;
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
                gvCharteredAccountsList.DataBind();
            }

            else
            {
                gvCharteredAccountsList.DataSource = null;
                gvCharteredAccountsList.DataBind();
                Label lblEmptyMessage = gvCharteredAccountsList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("charteredaccountants", false);
    }
    protected void gvCharteredAccountsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCharteredAccountsList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCharAccList();
    }
    protected void gvCharteredAccountsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindCharAccList();
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
                  
                    "ChartedAccName LIKE '%" + SearchText  +  
                    "%' OR MainAcName LIKE '%" + SearchText +
                    "%' OR AccCode LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvCharteredAccountsList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCharteredAccountsList.DataSource = dr.CopyToDataTable();
                    gvCharteredAccountsList.DataBind();
                }
                else
                {
                    gvCharteredAccountsList.DataSource = null;
                    gvCharteredAccountsList.DataBind();

                    Label lblEmptyMessage = gvCharteredAccountsList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvCharteredAccountsList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCharAccList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}