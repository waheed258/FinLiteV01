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
public partial class Admin_CitiesList : System.Web.UI.Page
{
    EMCitiesMaster objemCity = new EMCitiesMaster();
    BACities objBACities = new BACities();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCitiesList();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CitiesMaster.aspx");
    }
    protected void gvCitiesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CityId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit City")
        {
            Response.Redirect("CitiesMaster.aspx?Id=" + CityId);
        }
        if(e.CommandName == "Delete City")
        {
            DeleteCities(Convert.ToInt32(CityId));
            BindCitiesList();
        }

    }

    private void DeleteCities(int Id)
    {
        int Result = objBACities.DeleteCities(Id);
    }
    protected void gvCitiesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCitiesList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCitiesList();
    }

    private void BindCitiesList()
    {
        try
        {
            gvCitiesList.PageSize = int.Parse(ViewState["ps"].ToString());
            int Id = 0;
            DataSet ds = objBACities.GetCities(Id);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCitiesList.DataSource = ds.Tables[0];
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
                gvCitiesList.DataBind();
            }
            else
            {
                gvCitiesList.DataSource = null;
                gvCitiesList.DataBind();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCitiesList();
    }
    protected void gvCitiesList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCitiesList();
        }
        catch (Exception ex)
        {
            lblMsg.Text =_BOUtility.ShowMessage("danger", "Danger", ex.Message);
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
                    "CityKey='" + SearchText +
                    "' OR Name LIKE '%" + SearchText +
                    "%' OR CountryName LIKE '%" + SearchText +
                    "%' OR CityTimezoneUtc LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvCitiesList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCitiesList.DataSource = dr.CopyToDataTable();
                    gvCitiesList.DataBind();
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