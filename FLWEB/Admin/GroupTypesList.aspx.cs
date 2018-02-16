using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_GroupTypesList : System.Web.UI.Page
{
    BAGroupTypes objBAGroupTypes = new BAGroupTypes();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindGroupTypes();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupTypes.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindGroupTypes();
    }
    protected void gvGroupTypesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string GroupId = e.CommandArgument.ToString();
            if (e.CommandName == "Edit GroupTypes Details")
            {

                Response.Redirect("GroupTypes.aspx?Id=" + GroupId);
            }
            else if (e.CommandName == "Delete GroupTypes Details")
            {
                DeleteGroupTypes(Convert.ToInt32(GroupId));
                BindGroupTypes();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvGroupTypesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGroupTypesList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindGroupTypes();
    }

    #region PrivateMethods
    private void BindGroupTypes()
    {
        try
        {
            gvGroupTypesList.PageSize = int.Parse(ViewState["ps"].ToString());
            int Id = 0;
            DataSet ds = objBAGroupTypes.GetGroupTypes(Id);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvGroupTypesList.DataSource = ds.Tables[0];
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
                gvGroupTypesList.DataBind();
            }
            else
            {
                gvGroupTypesList.DataSource = null;
                gvGroupTypesList.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void DeleteGroupTypes(int Id)
    {
        try
        {
            int Result = objBAGroupTypes.DeleteGroupTypes(Id);
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
                    "Code='" + SearchText +
                    "' OR Name LIKE '%" + SearchText +
                    "%' OR GroupType LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvGroupTypesList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvGroupTypesList.DataSource = dr.CopyToDataTable();
                    gvGroupTypesList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvGroupTypesList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindGroupTypes();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}