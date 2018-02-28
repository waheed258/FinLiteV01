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

public partial class Admin_SupplierChoiceList : System.Web.UI.Page
{
    EMSupplierChoice objEMSupChoice = new EMSupplierChoice();
    BALSupplierChoice objBASupChoice = new BALSupplierChoice();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindSuppChoice();
        }
    }

    private void BindSuppChoice()
    {
        try
        {
            gvSupChoiceList.PageSize = int.Parse(ViewState["ps"].ToString());
            int SupplierChoiceId = 0;
            DataSet ds = objBASupChoice.GetSuppChoice(SupplierChoiceId);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvSupChoiceList.DataSource = ds.Tables[0];
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
                gvSupChoiceList.DataBind();
            }
            else
            {
                gvSupChoiceList.DataSource = null;
                gvSupChoiceList.DataBind();

                Label lblEmptyMessage = gvSupChoiceList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierChoice.aspx");
    }
    protected void gvSupChoiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ChoiceId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Choice")
        {
            Response.Redirect("SupplierChoice.aspx?SupplierChoiceId=" + ChoiceId);
        }
        if(e.CommandName == "Delete Choice")
        {
            DeleteSuppChoice(Convert.ToInt32(ChoiceId));
            BindSuppChoice();
        }
    }
    protected void gvSupChoiceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupChoiceList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindSuppChoice();
    }

    private void DeleteSuppChoice(int SupplierChoiceId)
    {
        int Result = objBASupChoice.DeleteSuppChoice(SupplierChoiceId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindSuppChoice();
    }
    //protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    //{
    //    SearchItemFromList(txtSearch.Text.Trim());
    //}

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "ChoiceKey='" + SearchText +
                    "' OR ChoiceDescription LIKE '%" + SearchText + "%'");
               
                if (dr.Count() > 0)
                {
                    gvSupChoiceList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvSupChoiceList.DataSource = dr.CopyToDataTable();
                    gvSupChoiceList.DataBind();
                }
                else
                {
                    gvSupChoiceList.DataSource = null;
                    gvSupChoiceList.DataBind();

                    Label lblEmptyMessage = gvSupChoiceList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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

    protected void gvSupChoiceList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindSuppChoice();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }
}