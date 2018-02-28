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

public partial class Admin_CashBookList : System.Web.UI.Page
{
    EMCashBook objEMCash = new EMCashBook();
    BACashBook objBACash = new BACashBook();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCashBook();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CashBookType.aspx");
    }
    protected void gvCashBookList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cashBookId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Cash")
        {
            Response.Redirect("CashBookType.aspx?CashBookId=" + cashBookId);
           
        }
        if(e.CommandName == "Delete Cash")
        {
            DeleteCashBook(Convert.ToInt32(cashBookId));
            BindCashBook();
        }
    }

    private void DeleteCashBook(int CashBookId)
    {
        int Result = objBACash.DeleteCashBook(CashBookId);
    }
    protected void gvCashBookList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCashBookList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCashBook();
    }

    private void BindCashBook()
    {
        try
        {
            gvCashBookList.PageSize = int.Parse(ViewState["ps"].ToString());
            int CashBookId = 0;
            DataSet ds = objBACash.GetCashBook(CashBookId);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCashBookList.DataSource = ds.Tables[0];
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
                gvCashBookList.DataBind();
            }
            else
            {
                gvCashBookList.DataSource = null;
                gvCashBookList.DataBind();

                Label lblEmptyMessage = gvCashBookList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
              
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCashBook();
    }
    protected void gvCashBookList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCashBook();
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

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "CashBookKey='" + SearchText +
                    "' OR CashDescription LIKE '%" + SearchText +
                    "%' OR TransName LIKE '%" + SearchText +
                    "%' OR GICode LIKE '%" + SearchText +
                    "%' OR ReferenceFormat LIKE '%" + SearchText +
                    "%' OR VatCodes LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvCashBookList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCashBookList.DataSource = dr.CopyToDataTable();
                    gvCashBookList.DataBind();
                }
                else
                {
                    gvCashBookList.DataSource = null;
                    gvCashBookList.DataBind();

                    Label lblEmptyMessage = gvCashBookList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
}