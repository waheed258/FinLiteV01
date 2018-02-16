using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GeneralRececiptList : System.Web.UI.Page
{
    BALGeneralReceipt ObjBALGenRecept = new BALGeneralReceipt();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            GenReceiptGridBind();
        }
    }
    private void GenReceiptGridBind()
    {
        try
        {

            gvGenReceiptList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = ObjBALGenRecept.GetRecipts();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvGenReceiptList.DataSource = ds;
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
                gvGenReceiptList.DataBind();
            }
            else
            {
                gvGenReceiptList.DataSource = null;
                gvGenReceiptList.DataBind();
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnGenReciptAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralReceipt.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //GenReceiptGridBind();
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
                    "AccountNo='" + SearchText +
                    "' OR Convert(Date, 'System.String') LIKE '%" + SearchText +
                    "%' OR BankName LIKE '%" + SearchText +
                    "%' OR ReceiptType LIKE '%" + SearchText +
                    "%' OR SourceRef LIKE '%" + SearchText +
                    "%' OR Convert(IncAmount, 'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvGenReceiptList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvGenReceiptList.DataSource = dr.CopyToDataTable();
                    gvGenReceiptList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvGenReceiptList_Sorting(object sender, GridViewSortEventArgs e)
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
            GenReceiptGridBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}