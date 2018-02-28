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

public partial class Admin_IndividualTitlesList : System.Web.UI.Page
{
    EMIndividualTitles objEMTitles = new EMIndividualTitles();
    BAIndividualTitles objBATitles = new BAIndividualTitles();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindTitlesList();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndividualTitles.aspx");
    }

    private void BindTitlesList()
    {
        try
        {
            gvTitlesList.PageSize = int.Parse(ViewState["ps"].ToString());
            int TitleId = 0;
            DataSet ds = objBATitles.GetIndivTitle(TitleId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvTitlesList.DataSource = ds.Tables[0];
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
                gvTitlesList.DataBind();
            }
            else
            {
                gvTitlesList.DataSource = null;
                gvTitlesList.DataBind();
                Label lblEmptyMessage = gvTitlesList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    protected void gvTitlesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Titleid = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Title")
        {
            Response.Redirect("IndividualTitles.aspx?TitleId=" + Titleid);
        }
        if (e.CommandName == "Delete Title")
        {
            DeleteTitle(Convert.ToInt32(Titleid));
            BindTitlesList();
        }

    }

    private void DeleteTitle(int TitleId)
    {
        int Result = objBATitles.DeleteIndivTitle(TitleId);
    }
    protected void gvTitlesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTitlesList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindTitlesList();
    }

    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindTitlesList();
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
                    "TitleKey='" + SearchText +
                    "' OR TitleDescription LIKE '%" + SearchText +
                    "%' OR Gender LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvTitlesList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvTitlesList.DataSource = dr.CopyToDataTable();
                    gvTitlesList.DataBind();
                }
                else
                {
                    gvTitlesList.DataSource = null;
                    gvTitlesList.DataBind();

                    Label lblEmptyMessage = gvTitlesList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvTitlesList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindTitlesList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}