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

public partial class Admin_AgencyDebitMemo : System.Web.UI.Page
{
    DALAgencyDebitMemo objDALADM = new DALAgencyDebitMemo();
    EMAgencyDebitMemo objEMADM = new EMAgencyDebitMemo();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    DOUtility _objDOUtility = new DOUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindADMType();
            txtADMBSP.Enabled = false;
            BindADMCreditCrard();
            BindAdmSuppliers();
            DDLADMFarevat.Enabled = false;

        }

    }
    protected void ADMSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objEMADM.AdmNo = txtADMNO.Text.Trim();
            objEMADM.TicketNo = txtADMTicketNO.Text.Trim();
            objEMADM.Supplier = Convert.ToInt32(DDLADMSupplier.SelectedIndex);
            objEMADM.Type = Convert.ToInt32(DDLADMType.SelectedIndex);
            objEMADM.PassengerName = txtADMPassengerName.Text.Trim();
            string ExcluFare = string.IsNullOrEmpty(txtADMExclFire.Text.Trim()) ? "0" : txtADMExclFire.Text.Trim();
            objEMADM.ExcluFare = Convert.ToDecimal(ExcluFare);
            //string FareVat = string.IsNullOrEmpty(txtFareVat.Text.Trim()) ? "0" : txtFareVat.Text.Trim();
            //objEMAgencydebitmemo.FareVat = Convert.ToInt32(FareVat);
            objEMADM.FareVat = Convert.ToInt32(DDLADMFarevat.SelectedIndex);
            string Commission = string.IsNullOrEmpty(txtADMCommission.Text.Trim()) ? "0" : txtADMCommission.Text.Trim();
            objEMADM.Commission = Convert.ToDecimal(Commission);
            string DepatureTaxes = string.IsNullOrEmpty(txtADMDepTaxes.Text.Trim()) ? "0" : txtADMDepTaxes.Text.Trim();
            objEMADM.DepatureTaxes = Convert.ToDecimal(DepatureTaxes);
            string AgentVat = string.IsNullOrEmpty(txtADMVat.Text.Trim()) ? "0" : txtADMVat.Text.Trim();
            objEMADM.AgentVat = Convert.ToDecimal(AgentVat);
            string ClientTotal = string.IsNullOrEmpty(txtADMClientTotal.Text.Trim()) ? "0" : txtADMClientTotal.Text.Trim();
            objEMADM.ClientTotal = Convert.ToDecimal(ClientTotal);
            string BSP = string.IsNullOrEmpty(txtADMBSP.Text.Trim()) ? "0" : txtADMBSP.Text.Trim();
            objEMADM.BSP = Convert.ToDecimal(BSP);
            //string CreditCard = string.IsNullOrEmpty(txtCrediCard.Text.Trim()) ? "0" : txtCrediCard.Text.Trim();
            //objEMAgencydebitmemo.CreditCard = Convert.ToInt32(CreditCard);
            objEMADM.CreditCard = Convert.ToInt32(DDLADMCreditCard.SelectedIndex);

            int Result = objDALADM.InsertAgencydebitmemo(objEMADM);


            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Agency Debit Memo created Successfully");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Agency Debit Memo Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }


    }
    protected void Cancel_Click(object sender, EventArgs e)
    {

    }
    protected void Reset_Click(object sender, EventArgs e)
    {
        txtADMNO.Text = " ";
        txtADMExclFire.Text = "";

    }
    public void BindAdmSuppliers()
    {

        try
        {
            BALandSuppliers objBalandSuppliers = new BALandSuppliers();
            int landSupId = 0;
            DataSet datasetland = new DataSet();
            datasetland = objBalandSuppliers.GetLandSupplier(landSupId);
            if (datasetland.Tables[2].Rows.Count > 0)
            {
                DDLADMSupplier.DataSource = datasetland.Tables[2];
                DDLADMSupplier.DataTextField = "LSupplierName";
                DDLADMSupplier.DataValueField = "LSupplierId";
                DDLADMSupplier.DataBind();
                DDLADMSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
            else
            {
                DDLADMSupplier.DataSource = null;
                DDLADMSupplier.DataBind();
                DDLADMSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }

    }
    public void BindADMType()
    {
        try
        {
            DDLADMType.DataSource = objDALADM.Get_Type();
            DDLADMType.DataTextField = "TypeName";
            DDLADMType.DataValueField = "TypeName";
            DDLADMType.DataBind();
            DDLADMType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }


    }

    public void BindADMCreditCrard()
    {

        try
        {
            DDLADMCreditCard.DataSource = objDALADM.Get_CreditCard();
            DDLADMCreditCard.DataTextField = "CardDescription";
            DDLADMCreditCard.DataValueField = "CrdCardId";
            DDLADMCreditCard.DataBind();
            DDLADMCreditCard.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }

    }

    protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DDLADMFarevat.DataSource = objDALADM.Get_Vat(DDLADMType.SelectedIndex);
            DDLADMFarevat.DataTextField = "VatRate";
            DDLADMFarevat.DataValueField = "VatId";
            DDLADMFarevat.DataBind();
            DDLADMFarevat.Enabled = false;

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }
    protected void ClientTotal()
    {
        decimal vatAmount = 0;
        if (txtADMDepTaxes.Text != "" || txtADMCommission.Text != "" || txtADMVat.Text != "" || txtADMExclFire.Text != "")
        {
            if (DDLADMFarevat.SelectedValue.ToString() != "")
            {
                decimal ExclFare = Convert.ToDecimal(txtADMExclFire.Text.ToString());

                decimal farevat = Convert.ToDecimal(DDLADMFarevat.SelectedItem.Text);

                vatAmount = (ExclFare * farevat) / 100;
            }
            vatAmount = Convert.ToDecimal(string.IsNullOrEmpty(vatAmount.ToString().Trim()) ? "0" : vatAmount.ToString().Trim());

            string airPorttax = string.IsNullOrEmpty(txtADMDepTaxes.Text.Trim()) ? "0" : txtADMDepTaxes.Text.Trim();
            txtADMDepTaxes.Text = airPorttax;

            string agentVat = string.IsNullOrEmpty(txtADMVat.Text.Trim()) ? "0" : txtADMVat.Text.Trim();
            txtADMVat.Text = agentVat;

            string commision = string.IsNullOrEmpty(txtADMCommission.Text.Trim()) ? "0" : txtADMCommission.Text.Trim();
            txtADMCommission.Text = commision;

            decimal clientTotal = Convert.ToDecimal(airPorttax) + Convert.ToDecimal(agentVat) + Convert.ToDecimal(commision) + vatAmount + Convert.ToDecimal(txtADMExclFire.Text);
            txtADMClientTotal.Text = clientTotal.ToString();
            decimal Bspamount = clientTotal - Convert.ToDecimal(txtADMCommission.Text);
            txtADMBSP.Text = Bspamount.ToString();
        }
    }
    protected void txtADMExclFire_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();

        txtADMDepTaxes.Enabled = true;
        txtADMCommission.Enabled = true;
        txtADMVat.Enabled = true;
    }
    protected void txtADMCommission_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
    protected void txtADMVat_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
    protected void DDLADMFarevat_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
    protected void txtADMDepTaxes_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
}
