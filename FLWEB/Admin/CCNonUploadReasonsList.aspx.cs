using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_CCNonUploadReasonsList : System.Web.UI.Page
{

    BANonUploadReasons objBAUploadReasons = new BANonUploadReasons();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindNonUpload();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CCNonUploadReasons.aspx");
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
      //  BindNonUpload();
    }
    protected void gvNonUploadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string UploadId = e.CommandArgument.ToString();
            if (e.CommandName == "Edit NonUpload Details")
            {

                Response.Redirect("CCNonUploadReasons.aspx?NonUploadId=" + UploadId);
            }
            else if (e.CommandName == "Delete NonUpload Details")
            {
                DeleteNonUpload(Convert.ToInt32(UploadId));
                BindNonUpload();
            }
        }
        catch (Exception ex)
        {
           lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
           ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvNonUploadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNonUploadList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindNonUpload();
    }

    #region PrivateMethods
    private void BindNonUpload()
    {
        try
        {
            gvNonUploadList.PageSize = int.Parse(ViewState["ps"].ToString());
            int UploadId = 0;
            DataSet ds = objBAUploadReasons.GetNonUploadReasons(UploadId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvNonUploadList.DataSource = ds.Tables[0];
                
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
                gvNonUploadList.DataBind();
            }
            else
            {
                gvNonUploadList.DataSource = null;
                gvNonUploadList.DataBind();

                Label lblEmptyMessage = gvNonUploadList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                lblEmptyMessage.Text = "Currently there are no records in System";
              
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteNonUpload(int UploadId)
    {
        try
        {
            int Result = objBAUploadReasons.DeleteUpdNonUploadReasons(UploadId);
        }
        catch (Exception ex)
        {
            
           lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
           ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion PrivateMethods

 
    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "NonUploadKey='" + SearchText +
                    "' OR NonUploadDesc LIKE '%" + SearchText + "%'");
                   
                if (dr.Count() > 0)
                {
                    gvNonUploadList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvNonUploadList.DataSource = dr.CopyToDataTable();
                    gvNonUploadList.DataBind();
                }
                else
                {
                    gvNonUploadList.DataSource = null;
                    gvNonUploadList.DataBind();

                    Label lblEmptyMessage = gvNonUploadList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                    lblEmptyMessage.Text = "Currently there are no records in" + "  '" + SearchText + "'";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text =_objBOUtiltiy.ShowMessage("danger","Danger",ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvNonUploadList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindNonUpload();
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