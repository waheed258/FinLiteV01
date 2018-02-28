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

public partial class Admin_CreditCardList : System.Web.UI.Page
{
    EMCreditCard objEMCreditCard = new EMCreditCard();
    BACreditCard objBACredit = new BACreditCard();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCreditCard();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreditCardType.aspx");
    }
    protected void gvCreditCardList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CreditId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Credit")
        {
            Response.Redirect("CreditCardType.aspx?CreditCardId=" + CreditId);
        }
        if(e.CommandName == "Delete Credit")
        {
            DeleteCreditCard(Convert.ToInt32(CreditId));
            BindCreditCard();
        }
    }
    protected void gvCreditCardList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCreditCardList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCreditCard();
    }

    private void BindCreditCard()
    {
        try
        {
            gvCreditCardList.PageSize = int.Parse(ViewState["ps"].ToString());
            int CreditCardId = 0;
            DataSet ds = objBACredit.GetCreditCard(CreditCardId);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count> 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCreditCardList.DataSource = ds.Tables[0];
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
                gvCreditCardList.DataBind();
            }
            else
            {
                gvCreditCardList.DataSource = null;
                gvCreditCardList.DataBind();
                Label lblEmptyMessage = gvCreditCardList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteCreditCard(int CreditCardId)
    {
        int Result = objBACredit.DeleteCreditCard(CreditCardId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindCreditCard();
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
                    "CreditCardKey='" + SearchText +
                    "' OR CreditDescription LIKE '%" + SearchText +
                    "%' OR Convert(NumberPrefix, 'System.String') LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvCreditCardList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCreditCardList.DataSource = dr.CopyToDataTable();
                    gvCreditCardList.DataBind();
                }
                else
                {
                    gvCreditCardList.DataSource = null;
                    gvCreditCardList.DataBind();

                    Label lblEmptyMessage = gvCreditCardList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
    protected void gvCreditCardList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCreditCard();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}