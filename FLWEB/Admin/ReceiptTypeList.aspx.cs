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

public partial class Admin_ReceiptTypeList : System.Web.UI.Page
{
    EMReceiptTypes objemReceipt = new EMReceiptTypes();
    BAReceiptType objBAReceipt = new BAReceiptType();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindReceiptType();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptTypes.aspx");
    }
    protected void gvReceiptList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string receiptId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Receipt")
        {
            Response.Redirect("ReceiptTypes.aspx?ReceiptId=" + receiptId);
        }
        if(e.CommandName == "Delete Receipt")
        {
            DeleteReceipt(Convert.ToInt32(receiptId));
            BindReceiptType();
        }

    }
    protected void gvReceiptList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReceiptList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindReceiptType();
    }

    private void BindReceiptType()
    {
        try
        {
            gvReceiptList.PageSize = int.Parse(ViewState["ps"].ToString());
            int ReceiptId = 0;
            DataSet ds = objBAReceipt.GetReceiptType(ReceiptId);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvReceiptList.DataSource = ds.Tables[0];
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
                gvReceiptList.DataBind();
            }
            else
            {
                gvReceiptList.DataSource = null;
                gvReceiptList.DataBind();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteReceipt(int ReceiptId)
    {
        int Result = objBAReceipt.DeleteReceiptType(ReceiptId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindReceiptType();
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
                    "ReceiptKey='" + SearchText +
                    "' OR RecDescription LIKE '%" + SearchText +
                    "%' OR DepListName LIKE '%" + SearchText +
                    "%' OR BankName LIKE '%" + SearchText +
                    "%' OR CreditDescription LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvReceiptList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvReceiptList.DataSource = dr.CopyToDataTable();
                    gvReceiptList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvReceiptList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindReceiptType();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    }
