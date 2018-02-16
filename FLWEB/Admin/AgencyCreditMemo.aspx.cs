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

public partial class Admin_AgencyCreditMemo : System.Web.UI.Page
{
    DALAgencyCreditMemo objDALACM = new DALAgencyCreditMemo();
    EMAgencyCreditMemo objEMACM = new EMAgencyCreditMemo();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    DOUtility _objDOUtility = new DOUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            BindAcmSuppliers();
            DDLACMFarevat.Enabled = false;

        }
    }
    protected void ACMSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objEMACM.AcmNo = txtACMNO.Text.Trim();
            objEMACM.TicketNo = txtACMTicketNO.Text.Trim();
            objEMACM.Supplier = Convert.ToInt32(DDLACMSupplier.SelectedIndex);
            objEMACM.Type = Convert.ToInt32(DDLACMType.SelectedIndex);
            objEMACM.PassengerName = txtACMPassengerName.Text.Trim();
            string ExcluFare = string.IsNullOrEmpty(txtACMExclFire.Text.Trim()) ? "0" : txtACMExclFire.Text.Trim();
            objEMACM.ExcluFare = Convert.ToDecimal(ExcluFare);
            //string FareVat = string.IsNullOrEmpty(txtFareVat.Text.Trim()) ? "0" : txtFareVat.Text.Trim();
            //objEMAgencycreditmemo.FareVat = Convert.ToInt32(FareVat);
            objEMACM.FareVat = Convert.ToInt32(DDLACMFarevat.SelectedIndex);
            string Commission = string.IsNullOrEmpty(txtACMCommission.Text.Trim()) ? "0" : txtACMCommission.Text.Trim();
            objEMACM.Commission = Convert.ToDecimal(Commission);
            string DepatureTaxes = string.IsNullOrEmpty(txtACMDepTaxes.Text.Trim()) ? "0" : txtACMDepTaxes.Text.Trim();
            objEMACM.DepatureTaxes = Convert.ToDecimal(DepatureTaxes);
            string AgentVat = string.IsNullOrEmpty(txtACMagentVat.Text.Trim()) ? "0" : txtACMagentVat.Text.Trim();
            objEMACM.AgentVat = Convert.ToDecimal(AgentVat);
            string ClientTotal = string.IsNullOrEmpty(txtACMClientTotal.Text.Trim()) ? "0" : txtACMClientTotal.Text.Trim();
            objEMACM.ClientTotal = Convert.ToDecimal(ClientTotal);
            string BSP = string.IsNullOrEmpty(txtACMBSP.Text.Trim()) ? "0" : txtACMBSP.Text.Trim();
            objEMACM.BSP = Convert.ToDecimal(BSP);
            string CreditCard = string.IsNullOrEmpty(txtACMCrediCard.Text.Trim()) ? "0" : txtACMCrediCard.Text.Trim();
            objEMACM.CreditCard = Convert.ToInt32(CreditCard);

            int Result = objDALACM.InsertAgencycreditmemo(objEMACM);


            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Agency Cedit Memo created Successfully");

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Agency Cedit Memo Details was not created plase try again");
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
        txtACMNO.Text = "";
        txtACMTicketNO.Text = "";
        txtACMPassengerName.Text = "";
        txtACMExclFire.Text = "";
        txtACMCommission.Text = "";
        txtACMagentVat.Text = "";
        txtACMClientTotal.Text = "";
        txtACMCrediCard.Text = "";
        txtACMDepTaxes.Text = "";
        txtACMBSP.Text = "";
        DDLACMSupplier.SelectedIndex = -1;
        DDLACMFarevat.Items.Clear();
        DDLACMType.SelectedIndex=-1;

    }

    public void BindType()
    {
        try
        {
            DDLACMType.DataSource = objDALACM.Get_Type();
            DDLACMType.DataTextField = "TypeName";
            DDLACMType.DataValueField = "TypeName";
            DDLACMType.DataBind();
            DDLACMType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }


    }
    protected void DDLACMType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DDLACMFarevat.DataSource = objDALACM.Get_Vat(DDLACMType.SelectedIndex);
            DDLACMFarevat.DataTextField = "VatRate";
            DDLACMFarevat.DataValueField = "VatId";
            DDLACMFarevat.DataBind();
            DDLACMFarevat.Enabled = false;

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }
    public void BindAcmSuppliers()
    {

        try
        {
            BALandSuppliers objBalandSuppliers = new BALandSuppliers();
            int landSupId = 0;
            DataSet datasetland = new DataSet();
            datasetland = objBalandSuppliers.GetLandSupplier(landSupId);
            if (datasetland.Tables[0].Rows.Count > 0)
            {
                DDLACMSupplier.DataSource = datasetland.Tables[0];
                DDLACMSupplier.DataTextField = "LSupplierName";
                DDLACMSupplier.DataValueField = "LSupplierId";
                DDLACMSupplier.DataBind();
                DDLACMSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
            else
            {
                DDLACMSupplier.DataSource = null;
                DDLACMSupplier.DataBind();
                DDLACMSupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }

    }
    protected void ClientTotal()
    {
        decimal vatAmount = 0;
        if (txtACMDepTaxes.Text != "" || txtACMCommission.Text != "" || txtACMagentVat.Text != "" || txtACMExclFire.Text != "")
        {
            if (DDLACMFarevat.SelectedValue.ToString() != "")
            {
                decimal ExclFare = Convert.ToDecimal(txtACMExclFire.Text.ToString());

                decimal farevat = Convert.ToDecimal(DDLACMFarevat.SelectedItem.Text);

                vatAmount = (ExclFare * farevat) / 100;
            }
            vatAmount = Convert.ToDecimal(string.IsNullOrEmpty(vatAmount.ToString().Trim()) ? "0" : vatAmount.ToString().Trim());

            string airPorttax = string.IsNullOrEmpty(txtACMDepTaxes.Text.Trim()) ? "0" : txtACMDepTaxes.Text.Trim();
            txtACMDepTaxes.Text = airPorttax;

            string agentVat = string.IsNullOrEmpty(txtACMagentVat.Text.Trim()) ? "0" : txtACMagentVat.Text.Trim();
            txtACMagentVat.Text = agentVat;

            string commision = string.IsNullOrEmpty(txtACMCommission.Text.Trim()) ? "0" : txtACMCommission.Text.Trim();
            txtACMCommission.Text = commision;

            decimal clientTotal = Convert.ToDecimal(airPorttax) + Convert.ToDecimal(agentVat) + Convert.ToDecimal(commision) + vatAmount + Convert.ToDecimal(txtACMExclFire.Text);
            txtACMClientTotal.Text = clientTotal.ToString();
            decimal Bspamount = clientTotal - Convert.ToDecimal(txtACMCommission.Text);
            txtACMBSP.Text = Bspamount.ToString();
        }
    }
    protected void txtACMExclFire_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();

        txtACMDepTaxes.Enabled = true;
        txtACMCommission.Enabled = true;
        txtACMagentVat.Enabled = true;
    }
    protected void txtACMCommission_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
    protected void txtACMDepTaxes_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
    protected void txtACMagentVat_TextChanged(object sender, EventArgs e)
    {
        ClientTotal();
    }
}