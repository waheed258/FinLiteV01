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
    BAGeneralPayment _objBAGencategory = new BAGeneralPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtPreparedBy.Text = Session["UserFullName"].ToString();
        BindAccountTypes();
     //   BindCategory();
        ddlToAccCode.Enabled = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

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
            }
            else
            {
                ddlFmAccCode.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
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
            _objIncomeReceipt.SourceRefNo=txtSourceRef.Text;

             //_objIncomeReceipt.ToMainAccount=;
            // _objIncomeReceipt.FromMainAccount=;

            //_objEmGenPytmAc.FromAccCodeId = Convert.ToInt32(ddlFmAccCode.SelectedValue);

            //_objEmGenPytmAc.ToAccCodeId = Convert.ToInt32(ddlToAccCode.SelectedValue.ToString());
            //// _objEmGenPytmAc.ToMainAcCode = 
            //_objEmGenPytmAc.PaymentAmount = Convert.ToDecimal(txtpytmAmount.Text);
            //_objEmGenPytmAc.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            //_objEmGenPytmAc.PaymentDate = Convert.ToDateTime(txtDate.Text);
            //_objEmGenPytmAc.CreatedBy = 0;

            //DataSet ds = _objBAGencategory.Get_MainAccCode(Convert.ToInt32(ddlToAccCode.SelectedValue));
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    _objEmGenPytmAc.FmMainAcCode = ds.Tables[0].Rows[0]["MainAcName"].ToString();


            //}
            //if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            //{
            //    _objEmGenPytmAc.ToMainAcCode = ds.Tables[1].Rows[0]["ChartedAccName"].ToString();
            //}


           // int Result = _objBAGencategory.InsertGeneralPaymrnt(_objEmGenPytmAc);
            //if (Result > 0)
            //{
            //    lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "GeneralPayment created Successfully");

            //    // Response.Redirect("AirportList.aspx");

            //}
            //else
            //{
            //    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "GeneralPayment  was not created please try again");
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertIncomeReceipt();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlFmAccCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlToAccCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}