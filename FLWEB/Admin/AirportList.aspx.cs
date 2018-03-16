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
public partial class Admin_AirportList : System.Web.UI.Page
{
    EMAirport objemAirport = new EMAirport();
    BAAirport objBAAirport = new BAAirport();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindAirportList();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Airports.aspx");
    }
    protected void gvAirportList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string airportId = e.CommandArgument.ToString();
        if(e.CommandName == "Edit Air")
        {
            Response.Redirect("Airports.aspx?AirportId=" + HttpUtility.UrlEncode(_BOUtility.Encrypts(airportId,true)));
        }
        if(e.CommandName == "Delete Air")
        {
            DeleteAirport(Convert.ToInt32(airportId));
            BindAirportList();
        }
    }
    protected void gvAirportList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAirportList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindAirportList();
    }

    private void BindAirportList()
    {
        try
        {
            gvAirportList.PageSize = int.Parse(ViewState["ps"].ToString());
            int AirportId = 0;
            DataSet ds = objBAAirport.GetAirport(AirportId);
            Session["dt"] = ds.Tables[0];
             if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
             {
                 gvAirportList.DataSource = ds.Tables[0];
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
                 gvAirportList.DataBind();
             }
             else
             {
                 gvAirportList.DataSource = null;
                 gvAirportList.DataBind();
                 Label lblEmptyMessage = gvAirportList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                 lblEmptyMessage.Text = "Currently there are no records in System";
             }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteAirport(int AirportId)
    {
        int Result = objBAAirport.DeleteAirport(AirportId);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
       // BindAirportList();
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
                    "AirKey='" + SearchText +
                    "' OR AirportName LIKE '%" + SearchText +
                    "%' OR CityName LIKE '%" + SearchText +
                    "%' OR CountryName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvAirportList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvAirportList.DataSource = dr.CopyToDataTable();
                    gvAirportList.DataBind();
                }
                else
                {
                    gvAirportList.DataSource = null;
                    gvAirportList.DataBind();

                    Label lblEmptyMessage = gvAirportList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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

    protected void gvAirportList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindAirportList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
           
        }
    }



        
}