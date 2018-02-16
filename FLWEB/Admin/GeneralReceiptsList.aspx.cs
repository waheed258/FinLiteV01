using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GeneralReceiptsList : System.Web.UI.Page
{
    BALGeneralReceipts ObjBALGenRecepts = new BALGeneralReceipts();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            GenReceiptsGridBind();
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
                   "GRSupplierMainAccCode='" + SearchText +
                    "' OR Convert(GRPaymentDate, 'System.String') LIKE '%" + SearchText +
                    "%' OR GRSupplierFromAccCode LIKE '%" + SearchText +
                    "%' OR BankName LIKE '%" + SearchText +                 
                    "%' OR Convert(GRPaymentAmount, 'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvGeneralReceiptsList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvGeneralReceiptsList.DataSource = dr.CopyToDataTable();
                    gvGeneralReceiptsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Generalreceipts.aspx");
    }


    private void GenReceiptsGridBind()
    {
        try
        {

            gvGeneralReceiptsList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = ObjBALGenRecepts.GetRecipts();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralReceiptsList.DataSource = ds;
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
                gvGeneralReceiptsList.DataBind();
            }
            else
            {
                gvGeneralReceiptsList.DataSource = null;
                gvGeneralReceiptsList.DataBind();
            }



        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
           // lblMsg.Text = "";
        }
    }
    protected void gvGeneralReceiptsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGeneralReceiptsList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // GenReceiptsGridBind();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //GenReceiptsGridBind();
    }
    protected void gvGeneralReceiptsList_Sorting(object sender, GridViewSortEventArgs e)
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
            GenReceiptsGridBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}