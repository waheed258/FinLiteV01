using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ImportTicketList : System.Web.UI.Page
{
    EMImportedTickets objEmImportTickets = new EMImportedTickets();
    BAImportedTickets objBAImportTickets = new BAImportedTickets();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindImportedTicket();
            Session["TempUniqCode"] = "";
        }
    }
    private void BindImportedTicket()
    {
        try
        {
           
           
            int invStatusID = 1;
            gvImportTickets.PageSize = int.Parse(ViewState["ps"].ToString());
           
            DataSet ds = objBAImportTickets.GetImportTickets(invStatusID);
            Session["dt"] = ds.Tables[0];

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvImportTickets.DataSource = ds;


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

                gvImportTickets.DataBind();
               // lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "File Import Succfully");
            }
            else
            {
                gvImportTickets.DataSource = ds;
                gvImportTickets.DataBind();
                Label lblEmptyMessage = gvImportTickets.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnAutoInvoice_Click(object sender, EventArgs e)
    {
        Button btndetails = sender as Button;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        int T5ID = Convert.ToInt32(gvImportTickets.DataKeys[gvrow.RowIndex].Value.ToString());
        Response.Redirect("AutoInvoice.aspx?id=" + T5ID);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TextFileRead.aspx", false);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindImportedTicket();
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
                    "PNR='" + SearchText +
                    "' OR Date LIKE '%" + SearchText +
                    "%' OR AirLine LIKE '%" + SearchText +
                    "%' OR Client LIKE '%" + SearchText +
                    "%' OR Convert(Fare ,'System.String') LIKE '%" + SearchText +
                    "%' OR Convert(VAT,'System.String') LIKE '%" + SearchText +
                      "%' OR Airport  LIKE '%" + SearchText +
                    //  "%' OR Convert(Airport Taxes,'System.Decimal') LIKE '%" + SearchText +
                    //"%' OR Convert(BSPAmount,'System.String') LIKE '%" + SearchText +
                     "%' OR Reference LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvImportTickets.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvImportTickets.DataSource = dr.CopyToDataTable();
                    gvImportTickets.DataBind();
                }
                else
                {
                    gvImportTickets.DataSource = null;
                    gvImportTickets.DataBind();

                    Label lblEmptyMessage = gvImportTickets.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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

    protected void gvImportTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvImportTickets.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindImportedTicket();

    }

    protected void gvImportTickets_Sorting(object sender, GridViewSortEventArgs e)
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
            BindImportedTicket();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
 
}