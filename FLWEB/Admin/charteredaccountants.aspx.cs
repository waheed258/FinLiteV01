using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataManager;
using BusinessManager;
using System.Data;
using EntityManager;
public partial class Finance_charteredaccountants : System.Web.UI.Page
{
    BALCharteredAccounts _objBalChartedAcc = new BALCharteredAccounts();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    EmCharteredAccounts _objChateredAcc = new EmCharteredAccounts();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMasteAccNames();
            BindCategory();
            BindCurrency();
            BindDefaultCurrency();
            ddlDefaultCurrency.Enabled = false;
            // ddlCategory.Enabled = false;
        }
    }

    protected void btnChartedAccount_Click(object sender, EventArgs e)
    {
        try
        {
            int MainAccId = Convert.ToInt32(ddlMasterAccount.SelectedValue.ToString());
            DataSet ds = _objBOUtiltiy.GetMainAccountType(MainAccId);

            _objChateredAcc.ChartedAccName = txtchartacName.Text;
            _objChateredAcc.ChartedMasterAccName = Convert.ToInt32(ddlMasterAccount.SelectedValue.ToString());
            //  _objChateredAcc.Type = txtchartAcType.Text;
            _objChateredAcc.Type = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[0].Rows[0]["AcType"].ToString();
            _objChateredAcc.AccCode = txtCharAcCommCode.Text + "" + txtCharAcountCode.Text;
            _objChateredAcc.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            _objChateredAcc.TranCurrency = Convert.ToInt32(ddlCurrency.SelectedValue.ToString());
            _objChateredAcc.BaseCurrency = Convert.ToInt32(ddlDefaultCurrency.SelectedValue.ToString());
            _objChateredAcc.CreatedBy = 0;
            _objChateredAcc.Isclient = 0;
            int Result = _objBalChartedAcc.CharteredAccountInsert(_objChateredAcc);

            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "ChartedAccount created Successfully");
                clearcontrols();
                Response.Redirect("CharteredAccountsList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "ChartedAccount Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void clearcontrols()
    {

    }
    protected void btnChartedAccountCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CharteredAccountsList", false);
    }
    protected void ddlMasterAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int AccTypeId = Convert.ToInt32(ddlMasterAccount.SelectedValue.ToString());

            DataSet ds = _objBalChartedAcc.BindMasterTypeandAccountCode(AccTypeId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtchartAcType.Text = ds.Tables[0].Rows[0]["MainAcName"].ToString();
                txtCharAcCommCode.Text = ds.Tables[0].Rows[0]["MainAccCode"].ToString();

                ddlCategory.Text = ds.Tables[0].Rows[0]["CategoryId"].ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindMasteAccNames()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBalChartedAcc.BindMasterAcNames();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMasterAccount.DataSource = ds.Tables[0];
                ddlMasterAccount.DataTextField = "MainAcName";
                ddlMasterAccount.DataValueField = "MainAccId";
                ddlMasterAccount.DataBind();
                ddlMasterAccount.Items.Insert(0, new ListItem("Select Account", "0"));

            }
            else
            {
                ddlMasterAccount.DataSource = null;
                ddlMasterAccount.DataBind();
                ddlMasterAccount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindCategory()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.GetAccountTypeOfSuppl();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlCategory.DataSource = ds;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";

                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }


            else
            {
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void BindCurrency()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBalChartedAcc.BindCurrency();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlCurrency.DataSource = ds.Tables[0];
                ddlCurrency.DataTextField = "code";
                ddlCurrency.DataValueField = "id";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("-Select Currency-", "0"));
            }
            else
            {
                ddlCurrency.DataSource = null;
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindDefaultCurrency()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBalChartedAcc.BindDefaultCurrency();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDefaultCurrency.DataSource = ds.Tables[0];
                ddlDefaultCurrency.DataTextField = "code";
                ddlDefaultCurrency.DataValueField = "id";
                ddlDefaultCurrency.DataBind();

            }
            else
            {
                ddlDefaultCurrency.DataSource = null;
                ddlDefaultCurrency.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void txtCharAcountCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AccNO = txtCharAcCommCode.Text + txtCharAcountCode.Text;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.CheckAccCodeExitorNot(AccNO, "CharteredAcc");

            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
            {
                lblaccnoerr.Text = "Already Exist";
                lblaccnoerr.ForeColor = System.Drawing.Color.Red;
                txtCharAcCommCode.Text = "";
            }
            else
            {
                lblaccnoerr.Text = "Available";
                lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
}