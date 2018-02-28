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

public partial class Admin_ContactNoteList : System.Web.UI.Page
{
    EMContactNote objemNote = new EMContactNote();
    BAContactNote objBANote = new BAContactNote();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindContactNote();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactNote.aspx");
    }
    protected void gvConNoteList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string NoteId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Note")
        {
            Response.Redirect("ContactNote.aspx?NotePadId=" + NoteId);
        }
        if (e.CommandName == "Delete Note")
        {
            DeleteContactNote(Convert.ToInt32(NoteId));
            BindContactNote();
        }
    }
    protected void gvConNoteList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConNoteList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindContactNote();
    }

    private void BindContactNote()
    {
        try
        {
            gvConNoteList.PageSize = int.Parse(ViewState["ps"].ToString());
            int NotePadId = 0;
            DataSet ds = objBANote.GetContactNote(NotePadId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvConNoteList.DataSource = ds.Tables[0];
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
                gvConNoteList.DataBind();
            }
            else
            {
                gvConNoteList.DataSource = null;
                gvConNoteList.DataBind();

                Label lblEmptyMessage = gvConNoteList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteContactNote(int NotePadId)
    {
        int Result = objBANote.DeleteContactNote(NotePadId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindContactNote();
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
                    "NoteKey='" + SearchText +
                    "' OR NpName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvConNoteList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvConNoteList.DataSource = dr.CopyToDataTable();
                    gvConNoteList.DataBind();
                }
                else
                {
                    gvConNoteList.DataSource = null;
                    gvConNoteList.DataBind();

                    Label lblEmptyMessage = gvConNoteList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvConNoteList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindContactNote();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}