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
public partial class Admin_StateList : System.Web.UI.Page
{
    EMState objEMState = new EMState();
    BAState objBAState = new BAState();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindStateDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("State.aspx");
    }
    protected void gvStateList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string StateId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit State Details")
        {
            // int StateId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("State.aspx?StateId=" + HttpUtility.UrlEncode(_objBOUtiltiy.Encrypts(StateId,true)));
        }
        if (e.CommandName == "Delete State Details")
        {
            // int StateId = Convert.ToInt32(e.CommandArgument);
            deleteStateDetails(Convert.ToInt32(StateId));
            BindStateDetails();
        }
    }
    protected void gvStateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStateList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        // BindStateDetails();
    }
    #region PrivateMethods
    private void BindStateDetails()
    {
        try
        {
            gvStateList.PageSize = int.Parse(ViewState["ps"].ToString());
            int StateId = 0;
            DataSet ds = objBAState.GetState(StateId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvStateList.DataSource = ds.Tables[0];
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
                gvStateList.DataBind();
            }
            else
            {
                gvStateList.DataSource = null;
                gvStateList.DataBind();
                Label lblEmptyMessage = gvStateList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void deleteStateDetails(int StateId)
    {
        try
        {
            int result = objBAState.DeleteState(StateId);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindStateDetails();
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
                    "StateKey='" + SearchText +
                    "' OR Name LIKE '%" + SearchText +
                    "%' OR Country LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvStateList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvStateList.DataSource = dr.CopyToDataTable();
                    gvStateList.DataBind();
                }
                else
                {
                    gvStateList.DataSource = null;
                    gvStateList.DataBind();

                    Label lblEmptyMessage = gvStateList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvStateList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindStateDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}