using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
public partial class Admin_GeneralPaymentList : System.Web.UI.Page
{
    BAGeneralPayment _objGeneralPayment = new BAGeneralPayment();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            GeneralPaymentGridBnd();
        }
    }
    private void GeneralPaymentGridBnd()
    {
        try
        {

            gvPaymentList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = _objGeneralPayment.GeneralPayment_list();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvPaymentList.DataSource = ds;
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
                gvPaymentList.DataBind();
            }
            else
            {
                gvPaymentList.DataSource = null;
                gvPaymentList.DataBind();
                Label lblEmptyMessage = gvPaymentList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void btnPaymentAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralPayment.aspx");
    }
    protected void gvPaymentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPaymentList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // GeneralPaymentGridBnd();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
       // SearchItemFromList(txtSearch.Text.Trim());
        GeneralPaymentGridBnd();
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
                    "MainAcCode='" + SearchText +
                    "' OR Convert(PaymentDate, 'System.String') LIKE '%" + SearchText +
                    "%' OR BankName LIKE '%" + SearchText +
                    "%' OR ToMainAcCode LIKE '%" + SearchText +
                    "%' OR Convert(PaymentAmount, 'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvPaymentList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvPaymentList.DataSource = dr.CopyToDataTable();
                    gvPaymentList.DataBind();
                }
                else
                {
                    gvPaymentList.DataSource = null;
                    gvPaymentList.DataBind();

                    Label lblEmptyMessage = gvPaymentList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvPaymentList_Sorting(object sender, GridViewSortEventArgs e)
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
            GeneralPaymentGridBnd();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}