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
public partial class Admin_VatTypeList : System.Web.UI.Page
{
    EMVatType objEMVatType = new EMVatType();
    BAVatType objBAVatType = new BAVatType();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindVatDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("VatType.aspx");
    }
    protected void gvVatList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        if (e.CommandName == "Edit Vat Details")
        {
           // int VatId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("VatType.aspx?VatId=" + HttpUtility.UrlEncode(_objBOUtiltiy.Encrypt(id)));
        }
        if (e.CommandName == "Delete Vat Details")
        {
            int VatId = Convert.ToInt32(e.CommandArgument);
            deleteBankAccDetails(VatId);
            BindVatDetails();
        }
    }
    protected void gvVatList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVatList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindVatDetails();
    }
    #region PrivateMethods
    private void BindVatDetails()
    {
        try
        {
            gvVatList.PageSize = int.Parse(ViewState["ps"].ToString());
            int VatId = 0;
            DataSet ds = objBAVatType.GetVatType(VatId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvVatList.DataSource = ds.Tables[0];
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
                gvVatList.DataBind();
            }
            else
            {
                gvVatList.DataSource = null;
                gvVatList.DataBind();
                Label lblEmptyMessage = gvVatList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void deleteBankAccDetails(int VatId)
    {
        try
        {
            int result = objBAVatType.DeleteVatType(VatId);
        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindVatDetails();
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
                    "VatKey='" + SearchText +
                    "' OR VatDescription LIKE '%" + SearchText +
                    "%' OR vatRate LIKE '%" + SearchText +
                    "%' OR VatAppTo LIKE '%" + SearchText +
                    "%' OR VatEffectivedate LIKE '%" + SearchText +
                    "%' OR VatGICode LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvVatList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvVatList.DataSource = dr.CopyToDataTable();
                    gvVatList.DataBind();
                }
                else
                {
                    gvVatList.DataSource = null;
                    gvVatList.DataBind();

                    Label lblEmptyMessage = gvVatList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvVatList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindVatDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}