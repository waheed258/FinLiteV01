using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataManager;
using BusinessManager;
using System.Data;
using EntityManager;
using System.Web.Services;
public partial class MainAccounts : System.Web.UI.Page
{
    BALMainAccount _objBalMainAcc = new BALMainAccount();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    EmMainAccount _objMainAcc = new EmMainAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepartments();
            BindCurrency();
            BindDefaultCurrency();
            BindAccountType();
            BindCategory();
            ddlDefaultCurrency.Enabled = false;
        }
    }
    protected void btnMainAccount_Click(object sender, EventArgs e)
    {
        try
        {

            _objMainAcc.MainAccCode = txtMainAccCode.Text;
            _objMainAcc.MainAcName = txtMainAccName.Text;
            _objMainAcc.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());
            _objMainAcc.TranCurrency = Convert.ToInt32(ddlCurrency.SelectedValue.ToString());
            _objMainAcc.BaseCurrency = Convert.ToInt32(ddlDefaultCurrency.SelectedValue.ToString());
            _objMainAcc.AcType = Convert.ToInt32(ddlMainAcType.SelectedValue.ToString());
            _objMainAcc.CreatedBy = 0;
            _objMainAcc.CategoryId = Convert.ToInt32(DropDownCategory.SelectedValue.ToString());

            int Result = _objBalMainAcc.MainAccountInsert(_objMainAcc);

            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "MainAccount created Successfully");
                clearcontrols();
                Response.Redirect("MainAccountList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "MainAccount Details was not created plase try again");
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
    protected void btnMainAccountCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainAccountList.aspx");
    }

    private void BindDepartments()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBalMainAcc.BindDepartments();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.DataSource = ds.Tables[0];
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "Deptid";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
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
            ds = _objBalMainAcc.BindCurrency();

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
            ds = _objBalMainAcc.BindDefaultCurrency();

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
    private void BindCategory()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.GetAccountTypeOfSuppl();

            if (ds.Tables[0].Rows.Count > 0)
            {

                DropDownCategory.DataSource = ds;
                DropDownCategory.DataTextField = "CategoryName";
                DropDownCategory.DataValueField = "CategoryId";

                DropDownCategory.DataBind();
                DropDownCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }


            else
            {
                DropDownCategory.DataSource = null;
                DropDownCategory.DataBind();
                DropDownCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindAccountType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetAccountTypes();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMainAcType.DataSource = ds.Tables[0];
                ddlMainAcType.DataTextField = "AccountName";
                ddlMainAcType.DataValueField = "AccountId";

                ddlMainAcType.DataBind();
                ddlMainAcType.Items.Insert(0, new ListItem("--Select Type--", "0"));

            }
            else
            {
                ddlMainAcType.DataSource = null;
                ddlMainAcType.DataBind();
                ddlMainAcType.Items.Insert(0, new ListItem("--Select Type--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //[WebMethod]
    //public string checkAccCode(string AccCode)
    //{
    //    var Result = _objBalMainAcc.IsExitMainAccCode(AccCode);
    //    if (Result != "")
    //    {
    //        Result = " already exit";

    //        lblStatus.Text = ("Already exist");
    //        lblStatus.ForeColor = System.Drawing.Color.Red;
    //    }
    //    return Result.ToString();

    //}


    //protected void txtMainAccCode_TextChanged(object sender, EventArgs e)
    //{
    //    var Result = _objBalMainAcc.IsExitMainAccCode(txtMainAccCode.Text);
    //  //  if (Result != "")
    //  //  {      
    //        //lblStatus.Text=("Already exist");
    //       // lblStatus.ForeColor = System.Drawing.Color.Red;
    //  //  }

    //}
    protected void txtMainAccCode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _objBOUtiltiy.CheckAccCodeExitorNot(txtMainAccCode.Text, "MainAcc");

        ds.Tables.Add(dt);

        if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
        {
            lblaccnoerr.Text = "Already Exist";
            lblaccnoerr.ForeColor = System.Drawing.Color.Red;
            txtMainAccCode.Text = "";
        }
        else
        {
             lblaccnoerr.Text = "Available";
            lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }
}