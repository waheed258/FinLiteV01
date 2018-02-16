using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_CountriesList : System.Web.UI.Page
{
    BACountries objBACountries = new BACountries();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCountriesDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Countries.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindCountriesDetails();

    }
    protected void gvCountriesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         string CountryId =e.CommandArgument.ToString();
        if(e.CommandName == "Edit Countries Details")
        {
           
            Response.Redirect("Countries.aspx?Id=" +CountryId);

        }
        else if(e.CommandName == "Delete Countries Details")
        {
            DeleteCountreisDetails(Convert.ToInt32(CountryId));
            BindCountriesDetails();
        }
    }
    protected void gvCountriesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCountriesList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCountriesDetails();

    }

    #region PrivateMethods
    private void BindCountriesDetails()
    {
        try
        {
            gvCountriesList.PageSize = int.Parse(ViewState["ps"].ToString());
            int Id = 0;
            DataSet ds = new DataSet();
            ds = objBACountries.GetCountries(Id);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvCountriesList.DataSource = ds.Tables[0];
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
                gvCountriesList.DataBind();
            }
            else
            {
                gvCountriesList.DataSource = null;
                gvCountriesList.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteCountreisDetails(int Id)
    {
        try
        {
            int Result = objBACountries.DeleteCountries(Id);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods
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
                    "CountryKey='" + SearchText +
                    "' OR Name LIKE '%" + SearchText +
                    "%' OR ContinentName LIKE '%" + SearchText +
                    "%' OR TimeZoneUTC LIKE '%" + SearchText +
                    "%' OR Convert(DialCode, 'System.String') LIKE '%" + SearchText +
                    "%' OR Convert(VATOrGSTRate, 'System.String') LIKE '%" + SearchText +
                    "%' OR CategoryName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvCountriesList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvCountriesList.DataSource = dr.CopyToDataTable();
                    gvCountriesList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvCountriesList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCountriesDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}