using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Data;
using System.Net.NetworkInformation;
using System.Management;
using System.Data;
using System.Data.SqlClient;
using BusinessManager;
using System.ComponentModel;

public partial class Login : System.Web.UI.Page
{
    private BALogin _objBALogin = new BALogin();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["UserLoginId"] = null;
            Session["TempUniqCode"] = null;
          //  Session.Abandon();
         //   GetLanguages();
        }

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet objDs = _objBALogin.UserAuthentication(txtLoginId.Text.Trim(), txtPassword.Text.Trim());
            if (objDs.Tables[0].Rows.Count > 0)
            {

                Session["UserLoginId"] = objDs.Tables[0].Rows[0]["UserLoginId"].ToString();
                Session["UserCompanyId"] = objDs.Tables[0].Rows[0]["UserCompanyId"].ToString();
                Session["UserRole"] = objDs.Tables[0].Rows[0]["UserRole"].ToString();
                Session["UserFullName"] = objDs.Tables[0].Rows[0]["UserFullName"].ToString();
                Session["BranchId"] = objDs.Tables[0].Rows[0]["BranchId"].ToString();
                Session["UserCode"] = objDs.Tables[0].Rows[0]["UserCode"].ToString();
                Session["CompanyLogo"] = objDs.Tables[0].Rows[0]["CompanyLogo"].ToString();

              //  HttpContext.Current.Session["Culture"] = ddllanguage.SelectedItem.Value;
                Response.Redirect("Admin/Index.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", "Invalid username/password");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void cmdSend_Click(object sender, EventArgs e)
    {
        txtEmail.Text = "";
        txtLoginId2.Text = "";
        labelError2.Text = "Thank You.";
        ForgotPasswordModalPopupExtender.Show();
    }

    public void GetLanguages()
    {
        try
        {
     
            DataSet ds = _objBOUtiltiy.GetLanguages();
            if (ds.Tables.Count > 0)
            {
                ddlLanguage.Items.Add(new ListItem("-Select Language-", "0"));
                ddlLanguage.DataSource = ds.Tables[0];
                ddlLanguage.DataValueField = "LangId";
                ddlLanguage.DataTextField = "Language";
                ddlLanguage.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
       Session["LanguageId"]= ddlLanguage.SelectedItem.Value.ToString();
       Session["LanguageName"] = ddlLanguage.SelectedItem.Text.ToString();
    }
}