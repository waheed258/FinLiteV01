using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_ContinentsList : System.Web.UI.Page
{
    BAContinents objBAContinents = new BAContinents();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindContinentDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Continents.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindContinentDetails();
    }
    protected void gvContinentsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ContId = e.CommandArgument.ToString();
        if(e.CommandName=="Edit Continents Details")
        {
           Response.Redirect("Continents.aspx?ContinentId=" + ContId);
        }
        else if(e.CommandName=="Delete Continents Details")
        {
            DeleteContinent(Convert.ToInt32(ContId));
            BindContinentDetails();
        }
    }
    protected void gvContinentsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvContinentsList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindContinentDetails();
    }

    private void BindContinentDetails()
    {
        try
        {
            gvContinentsList.PageSize = int.Parse(ViewState["ps"].ToString());
            int ContinentId = 0;
            DataSet ds = objBAContinents.GetContinents(ContinentId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvContinentsList.DataSource = ds.Tables[0];
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
                gvContinentsList.DataBind();
            }
            else
            {
                gvContinentsList.DataSource = null;
                gvContinentsList.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    private void DeleteContinent(int ContinentId)
    {
        try
        {
            int Result = objBAContinents.DeleteContinents(ContinentId);
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
                    "ContinentKey='" + SearchText +
                    "' OR ContinentDesc LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvContinentsList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvContinentsList.DataSource = dr.CopyToDataTable();
                    gvContinentsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvContinentsList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindContinentDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}