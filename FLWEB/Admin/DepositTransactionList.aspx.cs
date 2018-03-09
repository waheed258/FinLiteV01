using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
public partial class Admin_DepositTransactionList : System.Web.UI.Page
{
    BADepositTransaction _objDeposit = new BADepositTransaction();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindDepositList();
        }

    }



    #region  BindMethods

    private void BindDepositList()
    {

        try
        {
            gvDepositTransactionList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = _objDeposit.DepositList();
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvDepositTransactionList.DataSource = ds;
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
                gvDepositTransactionList.DataBind();
            }

            else
            {
                gvDepositTransactionList.DataSource = null;
                gvDepositTransactionList.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion

    #region Change Events 

  
    protected void gvDepositTransactionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepositTransactionList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindDepositList();

    }
    protected void gvDepositTransactionList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepositTransaction.aspx", false);
    }
    #endregion

    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindDepositList();
    }
    protected void gvDepositTransactionList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindDepositList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
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
                    "DepositSourceRef='" + SearchText +
                    "' OR RecDescription LIKE '%" + SearchText +                   
                    "%' OR Convert(DepositDate, 'System.String') LIKE '%" + SearchText +
                    "%' OR Name LIKE '%" + SearchText +
                    "%' OR DepositConsultant LIKE '%" + SearchText +
                    "%' OR Convert(TotalDepositAmount, 'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvDepositTransactionList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvDepositTransactionList.DataSource = dr.CopyToDataTable();
                    gvDepositTransactionList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);

        }
    }
}