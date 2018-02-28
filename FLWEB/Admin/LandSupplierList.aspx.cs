using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using EntityManager;
using DataManager;
using BusinessManager;
using System.Data;

public partial class Admin_LandSupplierList : System.Web.UI.Page
{
    EMLandSuppliers objEmLandSupp = new EMLandSuppliers();
    BALandSuppliers objLandSuppliers = new BALandSuppliers();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindLandSupplierList();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandSuppliers.aspx");
    }
    protected void gvLandSupplierList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLandSupplierList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindLandSupplierList();
    }
    protected void gvLandSupplierList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string LsupplierId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit LSupplier")
        {
            Response.Redirect("LandSuppliers.aspx?LSupplierId=" + LsupplierId);
        }

        if (e.CommandName == "Delete LSupplier")
        {
            DeleteLSupplier(Convert.ToInt32(LsupplierId));
            BindLandSupplierList();
        }
    }

    private void BindLandSupplierList()
    {

        try
        {
            gvLandSupplierList.PageSize = int.Parse(ViewState["ps"].ToString());
            int LSupplierId = 0;
            DataSet ds = objLandSuppliers.GetLandSupplier(LSupplierId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLandSupplierList.DataSource = ds.Tables[0];
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
                gvLandSupplierList.DataBind();
            }

            else
            {
                gvLandSupplierList.DataSource = null;
                gvLandSupplierList.DataBind();

                Label lblEmptyMessage = gvLandSupplierList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteLSupplier(int LSupplier)
    {
        try
        {
            int Result = objLandSuppliers.DeleteLandSupplier(LSupplier);

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
       // BindLandSupplierList();
    }
    protected void gvLandSupplierList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindLandSupplierList();
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
                    "LSupAccountCode='" + SearchText +
                    "' OR LSupplierName LIKE '%" + SearchText +
                    "%' OR ComDesc LIKE '%" + SearchText +
                    "%' OR LTelephone LIKE '%" + SearchText +
                    "%' OR LEmail LIKE '%" + SearchText +
                    "%' OR QuickAccount LIKE '%" + SearchText +
                    "%' OR PaymentName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvLandSupplierList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvLandSupplierList.DataSource = dr.CopyToDataTable();
                    gvLandSupplierList.DataBind();
                }
                else
                {
                    gvLandSupplierList.DataSource = null;
                    gvLandSupplierList.DataBind();

                    Label lblEmptyMessage = gvLandSupplierList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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