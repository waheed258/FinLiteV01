using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityManager;
using BusinessManager;

public partial class Admin_ClientsList : System.Web.UI.Page
{
    BAClients objBAClients = new BAClients();
    EMClients objClients = new EMClients();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindClientDetails();

        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        SearchItemFromList(txtSearch.Text.Trim());
        //BindClientDetails();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Clients.aspx");
    }
    protected void gvClientsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ClientId = e.CommandArgument.ToString();

        if (e.CommandName == "Edit Client Details")
        {

            Response.Redirect("Clients.aspx?ClientId=" + ClientId);

        }
        else if (e.CommandName == "Delete Client Details")
        {
            DeleteClientsDetails(Convert.ToInt32(ClientId));
            BindClientDetails();
        }

        else if (e.CommandName == "Edit CreditCard Details")
        {
            hf_ClientId.Value = ClientId;
            BindRptCreditCardDetails(rpCreditCard, Convert.ToInt32(ClientId));
            mpCreditCard.Show();


        }
    }
    protected void gvClientsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvClientsList.PageIndex = e.NewPageIndex;
        SearchItemFromList(txtSearch.Text.Trim());
       // BindClientDetails();
    }

    #region PrivateMethods
    private void BindClientDetails()
    {
        try
        {
            gvClientsList.PageSize = int.Parse(ViewState["ps"].ToString());
            int ClientId = 0;
            DataSet ds = new DataSet();          
            ds = objBAClients.GetClients(ClientId);
            Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0)
            {
                gvClientsList.DataSource = ds.Tables[0];
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

                gvClientsList.DataBind();

            }
            else
            {
                gvClientsList.DataSource = null;
                gvClientsList.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    private void DeleteClientsDetails(int ClientId)
    {
        try
        {
            int Result = objBAClients.DeleteClients(ClientId);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods
    #region Repetors
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientsList.aspx");
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCreditCard();
    }
    private void BindCreditCardDetails()
    {
        try
        {
            DataTable dtContact = new DataTable();
            DataRow dr;
            dtContact.Columns.Add("CreditCardId");
            dtContact.Columns.Add("CreditCardCode");
            dtContact.Columns.Add("CreditCardType");
            dtContact.Columns.Add("CreditCardAccNo");
            dtContact.Columns.Add("CreditCardAccHolder");
            dtContact.Columns.Add("CreditCardExpires");

            int count = rpCreditCard.Items.Count;
            foreach (RepeaterItem row in rpCreditCard.Items)
            {
                TextBox txtCreditCardCode = (TextBox)row.FindControl("txtCreditCardCode");
                TextBox txtCreditCardType = (TextBox)row.FindControl("txtCreditCardType");
                TextBox txtCreditCardAccNo = (TextBox)row.FindControl("txtCreditCardAccNo");
                TextBox txtCreditCardAccHolder = (TextBox)row.FindControl("txtCreditCardAccHolder");
                TextBox txtDateExpires = (TextBox)row.FindControl("txtDateExpires");
                HiddenField hf_CreditCardId = (HiddenField)row.FindControl("hf_CreditCardId");

                DataRow drexist = dtContact.NewRow();
                drexist["CreditCardId"] = hf_CreditCardId.Value;
                drexist["CreditCardCode"] = txtCreditCardCode.Text;
                drexist["CreditCardType"] = txtCreditCardType.Text;
                drexist["CreditCardAccNo"] = txtCreditCardAccNo.Text;
                drexist["CreditCardAccHolder"] = txtCreditCardAccHolder.Text;
                drexist["CreditCardExpires"] = txtDateExpires.Text;
                dtContact.Rows.Add(drexist);

            }
            dr = dtContact.NewRow();
            dr["CreditCardId"] = "0";
            dr["CreditCardCode"] = "";
            dr["CreditCardType"] = "";
            dr["CreditCardAccNo"] = "";
            dr["CreditCardAccHolder"] = "";
            dr["CreditCardExpires"] = "";
            dtContact.Rows.Add(dr);
            rpCreditCard.DataSource = dtContact;
            rpCreditCard.DataBind();
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void imgbtnCreditCard_Click(object sender, ImageClickEventArgs e)
    {
        BindCreditCardDetails();
    }

    #region PrivateMethods
    private void InsertUpdateCreditCard()
    {
        try
        {

            foreach (RepeaterItem row in rpCreditCard.Items)
            {

                TextBox rtxtCreditCardCode = row.FindControl("txtCreditCardCode") as TextBox;
                TextBox rtxtCreditCardType = row.FindControl("txtCreditCardType") as TextBox;
                TextBox rtxtCreditCardAccNo = row.FindControl("txtCreditCardAccNo") as TextBox;
                TextBox rtxtCreditCardAccHolder = row.FindControl("txtCreditCardAccHolder") as TextBox;
                TextBox rtxtDateExpires = row.FindControl("txtDateExpires") as TextBox;

                HiddenField rhf_CreditCardId = (HiddenField)row.FindControl("hf_CreditCardId");

                objClients.CreditCardCode = rtxtCreditCardCode.Text;
                objClients.CreditCardType = rtxtCreditCardType.Text;
                objClients.CreditCardAccNo = rtxtCreditCardAccNo.Text;
                objClients.CreditCardAccHolder = rtxtCreditCardAccHolder.Text;
                objClients.CreditCardExpires = rtxtDateExpires.Text;
                objClients.ClientId = Convert.ToInt32(hf_ClientId.Value);
                objClients.CreditCardId = Convert.ToInt32(rhf_CreditCardId.Value);

                int rResult = objBAClients.InsUpdCreditCardDetails(objClients);
                if (rResult > 0)
                {
                    lblMsg.Text = " Added Successfully";
                }
                else
                {
                    lblMsg.Text = "Not Successfully please tryagain";
                }
            }


            lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "CreditCard Details created Successfully");

        }



        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    private void BindRptCreditCardDetails(Repeater rpt, int ClinetID)
    {
        try
        {
            DataSet objCrdDtl = objBAClients.GetClients(ClinetID);
            if (objCrdDtl.Tables[2].Rows.Count > 0)
            {
                rpt.DataSource = objCrdDtl.Tables[2];
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteCreditCard(int CreditCardId)
    {

        try
        {
            int Result = objBAClients.DeleteCreditCardDetails(CreditCardId);
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void DeleteCreditCardd(int nRowIndex)
    {
        try
        {
            DataTable dtContent = new DataTable();

            dtContent.Columns.Add("CreditCardId");
            dtContent.Columns.Add("CreditCardCode");
            dtContent.Columns.Add("CreditCardType");
            dtContent.Columns.Add("CreditCardAccNo");
            dtContent.Columns.Add("CreditCardAccHolder");
            dtContent.Columns.Add("CreditCardExpires");


            foreach (RepeaterItem row in rpCreditCard.Items)
            {
                TextBox txtCreditCardCode = (TextBox)row.FindControl("txtCreditCardCode");
                TextBox txtCreditCardType = (TextBox)row.FindControl("txtCreditCardType");
                TextBox txtCreditCardAccNo = (TextBox)row.FindControl("txtCreditCardAccNo");
                TextBox txtCreditCardAccHolder = (TextBox)row.FindControl("txtCreditCardAccHolder");
                TextBox txtDateExpires = (TextBox)row.FindControl("txtDateExpires");
                HiddenField hf_CreditCardId = (HiddenField)row.FindControl("hf_CreditCardId");

                DataRow drexist = dtContent.NewRow();
                drexist["CreditCardId"] = hf_CreditCardId.Value;
                drexist["CreditCardCode"] = txtCreditCardCode.Text;
                drexist["CreditCardType"] = txtCreditCardType.Text;
                drexist["CreditCardAccNo"] = txtCreditCardAccNo.Text;
                drexist["CreditCardAccHolder"] = txtCreditCardAccHolder.Text;
                drexist["CreditCardExpires"] = txtDateExpires.Text;
                dtContent.Rows.Add(drexist);
            }
            dtContent.Rows.RemoveAt(nRowIndex);
            dtContent.AcceptChanges();

            rpCreditCard.DataSource = dtContent;
            rpCreditCard.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Action Completed');", true);
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    #endregion PrivateMethods

    protected void rpCreditCard_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string strCommandName = string.Empty;
            string strCommandArgu = "0";
            strCommandName = e.CommandName;
            HiddenField hfContactId = (HiddenField)e.Item.FindControl("CreditCardId");
            strCommandArgu = e.CommandArgument.ToString();
            int index = e.Item.ItemIndex;
            if (strCommandName == "Delete")
            {
                if (strCommandArgu == "0")
                {
                    DeleteCreditCardd(index);
                }
                else
                {
                    DeleteCreditCard(Convert.ToInt32(strCommandArgu));
                    DeleteCreditCardd(index);
                }

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion Repetors


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
                    "ClientDesc='" + SearchText +
                    "' OR ClientTypeAccCode LIKE '%" + SearchText +
                    "%' OR ClientName LIKE '%" + SearchText +
                    "%' OR IsActive LIKE '%" + SearchText +
                    "%' OR ClientTelephone LIKE '%" + SearchText +
                    "%' OR ClientEmail LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvClientsList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvClientsList.DataSource = dr.CopyToDataTable();
                    gvClientsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvClientsList_Sorting(object sender, GridViewSortEventArgs e)
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
            BindClientDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}