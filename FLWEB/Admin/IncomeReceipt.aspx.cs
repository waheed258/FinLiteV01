using BusinessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using System.Data;
public partial class IncomeReceipt : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAIncomeReceipt objBAIncomereceipt = new BAIncomeReceipt();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtPreparedBy.Text = Session["UserFullName"].ToString();
        BindAccountTypes();
        GetChartedAccounts();
        //   BindCategory();
        //ddlToAccCode.Enabled = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    private void GetChartedAccounts()
    {
        try
        {
          
            DataSet ObjDsClients = objBAIncomereceipt.GetChartedAccount();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = ObjDsClients;
                ddlCategory.DataValueField = "ChartedAccId";
                ddlCategory.DataTextField = "ChartedAccName";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));

               
            }
            else
            {
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));

              
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindAccountTypes()
    {
        try
        {
            ddlFmAccCode.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlFmAccCode.DataSource = ObjDsClients;
                ddlFmAccCode.DataValueField = "BankAcId";
                ddlFmAccCode.DataTextField = "BankName";
                ddlFmAccCode.DataBind();
                ddlFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));

                ddlToAccCode.DataSource = ObjDsClients;
                ddlToAccCode.DataValueField = "BankAcId";
                ddlToAccCode.DataTextField = "BankName";
                ddlToAccCode.DataBind();
                ddlToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlToAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
                ddlFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void InsertIncomeReceipt()
    {
        try
        {
            EMIncomeReceipt _objIncomeReceipt = new EMIncomeReceipt();

            _objIncomeReceipt.FromIncomeAccount = Convert.ToInt32(ddlFmAccCode.SelectedValue);
            _objIncomeReceipt.ToIncomeAccount = Convert.ToInt32(ddlToAccCode.SelectedValue);
            _objIncomeReceipt.IncomeAmount = Convert.ToDecimal(txtpytmAmount.Text);
            _objIncomeReceipt.createdBy = 0;
            _objIncomeReceipt.Incomedate = Convert.ToDateTime(txtDate.Text);
            _objIncomeReceipt.SourceRefNo = txtSourceRef.Text;

            _objIncomeReceipt.ToMainAccount=ddlToAccCode.Items.ToString();
            _objIncomeReceipt.FromMainAccount = ddlFmAccCode.Items.ToString();
            _objIncomeReceipt.Category=Convert.ToInt32(ddlCategory.SelectedValue.ToString());
           


            int Result = objBAIncomereceipt.InsertIncomeReceipt(_objIncomeReceipt);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "IncomeReceipt created Successfully");

                // Response.Redirect("AirportList.aspx");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "IncomeReceipt  was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertIncomeReceipt();
    }
  
    
   
}