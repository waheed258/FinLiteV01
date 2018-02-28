using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityManager;
using DataManager;
using BusinessManager;

public partial class Admin_ConsultantList : System.Web.UI.Page
{
    BAConsultant objBAConsultant = new BAConsultant();
    EMConsultant objConsultant = new EMConsultant();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindConsultant();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Consultant.aspx");
    }

    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindConsultant();
    }
   
    protected void gvConsultantList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ConsultId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Consultant Details")
        {
           
            Response.Redirect("Consultant.aspx?ConsultantId=" + ConsultId);
        }
        else if (e.CommandName == "Delete Consultant Details")
        {

            DeleteConsultant(Convert.ToInt32(ConsultId));
            BindConsultant();
        }
    }
    protected void gvConsultantList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConsultantList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindConsultant();
    }

    #region PrivateMethods
    private void BindConsultant()
    {
        try
        {
            gvConsultantList.PageSize = int.Parse(ViewState["ps"].ToString());
            int ConsultantId = 0;
            DataSet ds = objBAConsultant.GetConsultant(ConsultantId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvConsultantList.DataSource = ds.Tables[0];
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
                gvConsultantList.DataBind();

            }
            else
            {
                gvConsultantList.DataSource = null;
                gvConsultantList.DataBind();

                Label lblEmptyMessage = gvConsultantList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteConsultant(int ConsultantId)
    {
        try
        {
            int Result = objBAConsultant.DeleteConsultant(ConsultantId);
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
                    "KeyId='" + SearchText +
                    "' OR Name LIKE '%" + SearchText +
                    "%' OR Email LIKE '%" + SearchText +
                    "%' OR TelephoneNo LIKE '%" + SearchText +
                    "%' OR FaxNo LIKE '%" + SearchText +
                    "%' OR ClientDesc LIKE '%" + SearchText +
                    "%' OR GroupDesc LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvConsultantList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvConsultantList.DataSource = dr.CopyToDataTable();
                    gvConsultantList.DataBind();
                }
                else
                {
                    gvConsultantList.DataSource = null;
                    gvConsultantList.DataBind();

                    Label lblEmptyMessage = gvConsultantList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvConsultantList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindConsultant();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}