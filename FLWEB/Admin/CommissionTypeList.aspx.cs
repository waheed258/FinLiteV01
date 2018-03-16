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

public partial class Admin_CommissionTypeList : System.Web.UI.Page
{
    BACommissionType objBACommType = new BACommissionType();
    EMCommissionType objCommType = new EMCommissionType();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindCommissionTypeDetails();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CommissionType.aspx");
    }
    protected void gvCommTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Commission Type Details")
        {
           // int commId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("CommissionType.aspx?ComId=" + HttpUtility.UrlEncode(_objBOUtiltiy.Encrypts(commId,true)));
        }
        if (e.CommandName == "Delete Commission Type Details")
        {
           // int commId = Convert.ToInt32(e.CommandArgument);
            DeleteCommissionTypeDetails(Convert.ToInt32(commId));
            BindCommissionTypeDetails();
        }
    }
    protected void gvCommTypeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCommTypeList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
        //BindCommissionTypeDetails();
    }
    #region PrivateMethods
        private void BindCommissionTypeDetails()
            {
                try 
                {
                    gvCommTypeList.PageSize = int.Parse(ViewState["ps"].ToString());
                    int commid = 0;
                    DataSet ds = objBACommType.GetCommissionType(commid);
                    Session["dt"] = ds.Tables[0];
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvCommTypeList.DataSource = ds.Tables[0];
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
                        gvCommTypeList.DataBind();
                    }
                    else
                    {
                        gvCommTypeList.DataSource = null;
                        gvCommTypeList.DataBind();

                        Label lblEmptyMessage = gvCommTypeList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
                        lblEmptyMessage.Text = "Currently there are no records in System";
                    }

                }
                catch(Exception ex)
                {
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
                    ExceptionLogging.SendExcepToDB(ex);
                }
           }
        private void DeleteCommissionTypeDetails(int CommId)
          {
              try 
              {
                  int result = objBACommType.DeleteCommissionType(CommId);
              }
              catch(Exception ex)
              {
                  lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
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
                        "Comkey='" + SearchText +
                        "' OR ComDesc LIKE '%" + SearchText +
                        "%' OR CategoryName LIKE '%" + SearchText +
                        "%' OR TypeName LIKE '%" + SearchText +
                        "%' OR VatRate LIKE '%" + SearchText +
                        "%' OR ComZUTypeDesc LIKE '%" + SearchText + "%'");

                    if (dr.Count() > 0)
                    {
                        gvCommTypeList.PageSize = int.Parse(ViewState["ps"].ToString());
                        gvCommTypeList.DataSource = dr.CopyToDataTable();
                        gvCommTypeList.DataBind();
                    }
                    else
                    {
                        gvCommTypeList.DataSource = null;
                        gvCommTypeList.DataBind();

                        Label lblEmptyMessage = gvCommTypeList.Controls[0].Controls[0].FindControl("lblEmptyMessage") as Label;
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
        protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
           SearchItemFromList(txtSearch.Text.Trim());
           // BindCommissionTypeDetails();
        }
        protected void gvCommTypeList_Sorting(object sender, GridViewSortEventArgs e)
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
                BindCommissionTypeDetails();
            }
            catch (Exception ex)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
}