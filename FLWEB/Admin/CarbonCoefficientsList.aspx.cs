using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_CarbonCoefficientsList : System.Web.UI.Page
{
    BACarbonCoefficients objBACarbon = new BACarbonCoefficients();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCarbonCoefficients();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarbonCoefficients.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindCarbonCoefficients();
    }
    protected void gvCarbonList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string CarbonId = e.CommandArgument.ToString();
            if (e.CommandName == "Edit Carbon Details")
            {

                Response.Redirect("CarbonCoefficients.aspx?CarbonId=" + CarbonId);
            }
            else if (e.CommandName == "Delete Carbon Details")
            {
                DeleteCarbonCoefficients(Convert.ToInt32(CarbonId));
                BindCarbonCoefficients();
            }
        }
        catch (Exception ex)
        {
            
             lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
             ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvCarbonList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCarbonList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindCarbonCoefficients();
    }

    #region PrivateMethods
    private void BindCarbonCoefficients()
    {
        try
        {
            gvCarbonList.PageSize = int.Parse(ViewState["ps"].ToString());
            int CarbonId = 0;
            DataSet ds = objBACarbon.GetCarbonCoefficients(CarbonId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCarbonList.DataSource = ds.Tables[0];
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
                gvCarbonList.DataBind();

            }
            else
            {
                gvCarbonList.DataSource = null;
                gvCarbonList.DataBind();

                Label lblEmptyMessage = gvCarbonList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    private void DeleteCarbonCoefficients(int CarbonId)
    {
        try
        {
            int Result = objBACarbon.DeleteCarbonCoefficients(CarbonId);
        }
        catch (Exception ex)
        {
            
           lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
           ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion PrivateMethods

   
    protected void gvCarbonList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCarbonCoefficients();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "CarbonKey='" + SearchText +
                    "' OR CarbonDesc LIKE '%" + SearchText +
                "%' OR StartDate LIKE '%" + SearchText +
                "%' OR EndDate LIKE '%" + SearchText +
               "%' OR Convert(Cofficient,'System.String') LIKE '%" + SearchText + "%'");
               
                if (dr.Count() > 0)
                {
                    gvCarbonList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCarbonList.DataSource = dr.CopyToDataTable();
                    gvCarbonList.DataBind();
                }
                else
                {
                    gvCarbonList.DataSource = null;
                    gvCarbonList.DataBind();

                    Label lblEmptyMessage = gvCarbonList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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

    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }
}