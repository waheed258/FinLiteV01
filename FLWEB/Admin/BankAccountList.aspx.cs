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
public partial class Admin_BankAccountList : System.Web.UI.Page
{
    EMBankAccount objBankAc = new EMBankAccount();
    BABankAccount objBABankAc = new BABankAccount();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindBankAccDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankAccount.aspx");
    }
    protected void gvBankAccountList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit Bank Details")
        {
            int BankAccId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("BankAccount.aspx?BankAcId=" + BankAccId);
        }
        if (e.CommandName == "Delete Bank Details")
        {
            int BankAccId = Convert.ToInt32(e.CommandArgument);
            deleteBankAccDetails(BankAccId);
            BindBankAccDetails();
        }
    }
    protected void gvBankAccountList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBankAccountList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindBankAccDetails();
    }
    #region PrivateMethods
    private void BindBankAccDetails()
    {
        try
        {
            gvBankAccountList.PageSize = int.Parse(ViewState["ps"].ToString());
            int bankaccId = 0;
            DataSet ds = objBABankAc.GetBankAccount(bankaccId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvBankAccountList.DataSource = ds;

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

                gvBankAccountList.DataBind();
            }
            else
            {
                gvBankAccountList.DataSource = null;
                gvBankAccountList.DataBind();
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void deleteBankAccDetails(int BankAccId)
    {
        try
        {
            int result = objBABankAc.DeleteBankAccount(BankAccId);
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
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
                    "BankAcKey='" + SearchText +
                    "' OR BankName LIKE '%" + SearchText +
                    "%' OR BankAcType LIKE '%" + SearchText +
                    "%' OR BankAcNo LIKE '%" + SearchText +
                    "%' OR BranchName LIKE '%" + SearchText +
                    "%' OR AccountHolder LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvBankAccountList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvBankAccountList.DataSource = dr.CopyToDataTable();
                    gvBankAccountList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvBankAccountList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindBankAccDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindBankAccDetails();
    }
}