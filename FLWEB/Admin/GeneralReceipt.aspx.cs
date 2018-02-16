using BusinessManager;
using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GeneralReceipt : System.Web.UI.Page
{
    DOUtility _doUtilities = new DOUtility();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BACommissionType objBACommType = new BACommissionType();
    EMGeneralReceipt EMGenReceipt = new EMGeneralReceipt();
    BALGeneralReceipt objBALGeneralReceipt = new BALGeneralReceipt();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            DDLGRDivision1.Enabled = false;
            DDLGRConsultant.Enabled = false;
            DDLGRClient.Enabled = false;
            DDLGRSupplier.Enabled = false;
            DDLGRServiceType.Enabled = false;
            txtGRMessage.Enabled = false;
            DDLGRMessageType.Enabled = false;
            txtPayDetails.Enabled = false;
            txtGRPreparedBy.Text = Session["UserFullName"].ToString();
            BindGRDivision();
            BindGRReceiptTypes();
            BindGRAccountTypes();
            BindGRConsultants();
            BindGRClients();
            BindGRVAT();
            BindGRAccounts();
            
        }

    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
      
        if (Chk.Checked==true)
        {
            DDLGRDivision1.Enabled = true;
            DDLGRConsultant.Enabled = true;
            DDLGRClient.Enabled = true;
            DDLGRSupplier.Enabled = true;
            DDLGRServiceType.Enabled = true;
            txtGRMessage.Enabled = true;
            DDLGRMessageType.Enabled = true;
        }
       else
        {
            DDLGRDivision1.Enabled = false;
            DDLGRDivision1.SelectedIndex = -1;
            DDLGRConsultant.Enabled = false;
            DDLGRConsultant.SelectedIndex = -1;
            DDLGRClient.Enabled = false;
            DDLGRClient.SelectedIndex = -1;
            DDLGRSupplier.Enabled = false;
            DDLGRSupplier.SelectedIndex = -1;
            DDLGRServiceType.Enabled = false;
            DDLGRServiceType.SelectedIndex = -1;
            txtGRMessage.Enabled = false;
            DDLGRMessageType.Enabled = false;
            DDLGRMessageType.SelectedIndex = -1;
        }

    }
    private void BindGRDivision()
    {
        try
        {
            DDLGRDivision.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetDivisions();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                DDLGRDivision.DataSource = ObjDsClients;
                DDLGRDivision.DataValueField = "DivisionId";
                DDLGRDivision.DataTextField = "DivName";
                DDLGRDivision.DataBind();
                DDLGRDivision.Items.Insert(0, new ListItem("Select Division", "0"));

                //
                DDLGRDivision1.DataSource = ObjDsClients;
                DDLGRDivision1.DataValueField = "DivisionId";
                DDLGRDivision1.DataTextField = "DivName";
                DDLGRDivision1.DataBind();
                DDLGRDivision1.Items.Insert(0, new ListItem("Select Division", "0"));
            }
            else
            {
                DDLGRDivision.Items.Insert(0, new ListItem("Select Division", "0"));
                DDLGRDivision1.Items.Insert(0, new ListItem("Select Division", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindGRReceiptTypes()
    {
        try
        {
            DDLGRReceiptType.Items.Clear();
            int ReceiptId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetReceiptTypes(ReceiptId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                DDLGRReceiptType.DataSource = ObjDsClients;
                DDLGRReceiptType.DataValueField = "ReceiptId";
                DDLGRReceiptType.DataTextField = "RecDescription";
                DDLGRReceiptType.DataBind();
                DDLGRReceiptType.Items.Insert(0, new ListItem("Select ReceiptType", "0"));
            }
            else
            {
                DDLGRReceiptType.Items.Insert(0, new ListItem("Select ReceiptType", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindGRAccountTypes()
    {
        try
        {
            DDLGRAutoDeposit.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                DDLGRAutoDeposit.DataSource = ObjDsClients;
                DDLGRAutoDeposit.DataValueField = "BankAcId";
                DDLGRAutoDeposit.DataTextField = "BankName";
                DDLGRAutoDeposit.DataBind();
                DDLGRAutoDeposit.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                DDLGRAutoDeposit.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindAutoDepositeAccount(string ClientType)
    {
        try
        {
            if (ClientType == "")
            {
                DDLGRAccountNo.Items.Clear();
                DDLGRAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
                return;
            }
            DDLGRAccountNo.Items.Clear();
            BAClients objBAClients = new BAClients();
            DataSet ObjDsClients = objBAClients.GetClientByClientType(ClientType);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                DDLGRAccountNo.DataSource = ObjDsClients;
                DDLGRAccountNo.DataValueField = "ClientId";
                DDLGRAccountNo.DataTextField = "ClientAccount";
                DDLGRAccountNo.DataBind();
                DDLGRAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                DDLGRAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void DDLGRAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtPayDetails.Text = DDLGRAccountNo.SelectedValue.ToString();
        txtPayDetails.Text = DDLGRAccountNo.SelectedItem.Text.ToString();
    }
    public void BindGRConsultants()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.GetConsultants();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLGRConsultant.DataSource = ds.Tables[0];
                DDLGRConsultant.DataTextField = "Name";
                DDLGRConsultant.DataValueField = "ConsultantId";
                DDLGRConsultant.DataBind();
                DDLGRConsultant.Items.Insert(0, new ListItem("Select Consultant", "0"));
            }
            else
            {
                DDLGRConsultant.Items.Insert(0, new ListItem("Select Consultant", "0"));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindGRClients()
    {
        try
        {
            int clientTypeId = 0;
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.InvoiceDdlBinding(clientTypeId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLGRClient.DataSource = ds.Tables[0];
                DDLGRClient.DataTextField = "Name";
                DDLGRClient.DataValueField = "ConsultantId";
                DDLGRClient.DataBind();
                DDLGRClient.Items.Insert(0, new ListItem("Select Client", "0"));
            }
            else
            {
                DDLGRClient.Items.Insert(0, new ListItem("Select Client", "0"));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindGRVAT()
    {
        try
        {
            int vatid = 0;
            DataSet ds = objBACommType.GetVAT(vatid);

            DDLGRVat.DataSource = ds;
            DDLGRVat.DataTextField = "VatRate";
            DDLGRVat.DataValueField = "VatId";
            DDLGRVat.DataBind();
            DDLGRVat.Items.Insert(0, new ListItem("Select Vat", "0"));
        }
        catch(Exception ex)
        {
            DDLGRVat.Items.Insert(0, new ListItem("Select Vat", "0"));
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindGRServiceTypes()
    {
        try
        {
            string Type = "";
            DataSet ds = new DataSet();
            ds = _doUtilities.GetServiceTypeByType(Type);

            if (ds.Tables[0].Rows.Count > 0)
            {

                DDLGRServiceType.DataSource = ds.Tables[0];
                DDLGRServiceType.DataTextField = "ComDesc";
                DDLGRServiceType.DataValueField = "ComId";
                DDLGRServiceType.DataBind();
                DDLGRServiceType.Items.Insert(0, new ListItem("--Select Service--", "0"));

            }
            else
            {
                DDLGRServiceType.DataSource = null;
                DDLGRServiceType.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    public void BindGRAccounts()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.GetGeneralReceiptAccounts();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLGRAccountNo.DataSource = ds.Tables[0];
                DDLGRAccountNo.DataTextField = "Account";
                DDLGRAccountNo.DataValueField = "ChartedAccId";
                DDLGRAccountNo.DataBind();
                DDLGRAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                DDLGRAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    protected void GenReceptSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            EMGenReceipt.GRDate = string.IsNullOrEmpty(txtGRDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtGRDate.Text);
            EMGenReceipt.GRSourceRef = string.IsNullOrEmpty(txtGRSourceRef.Text) ? "" : (txtGRSourceRef.Text);
            EMGenReceipt.GRPrepairedby = string.IsNullOrEmpty(txtGRPreparedBy.Text) ? "" :(txtGRPreparedBy.Text);
            EMGenReceipt.GRIncAmount = Convert.ToDecimal(string.IsNullOrEmpty(txtGRIncAmount.Text) ? 0 : Convert.ToDecimal(txtGRIncAmount.Text));
            EMGenReceipt.GRDivision = Convert.ToInt32(DDLGRDivision.SelectedValue.ToString());
            EMGenReceipt.GRVat = Convert.ToInt32(DDLGRVat.SelectedValue.ToString());
            EMGenReceipt.GRReceiptType = Convert.ToInt32(DDLGRReceiptType.SelectedValue.ToString());
            EMGenReceipt.GRVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(txtGRVatAmount.Text)?0:Convert.ToDecimal(txtGRVatAmount.Text));
            EMGenReceipt.GRAutoDepositto = Convert.ToInt32(DDLGRAutoDeposit.SelectedValue.ToString());
            EMGenReceipt.GRExclAmount = Convert.ToDecimal(string.IsNullOrEmpty(txtGRExclAmount.Text)?0:Convert.ToDecimal(txtGRExclAmount.Text));
            EMGenReceipt.GRAccountNo = Convert.ToInt32(DDLGRAccountNo.SelectedValue.ToString());
            EMGenReceipt.GRPayDetails = string.IsNullOrEmpty(txtPayDetails.Text) ? "" : (txtPayDetails.Text);
            EMGenReceipt.GRDetails = string.IsNullOrEmpty(txtGRDetails.Text) ? "" : (txtGRDetails.Text);

            int Result = objBALGeneralReceipt.InsertGeneralReceipt(EMGenReceipt);
            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "General receipt created Successfully");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "AgencyGeneral receipt  Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        Response.Redirect("GeneralReceiptList.aspx");
    }
}