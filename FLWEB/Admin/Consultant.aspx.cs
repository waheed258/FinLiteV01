using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using DataManager;
using BusinessManager;
using System.Data;

public partial class Admin_Consultant : System.Web.UI.Page
{
    EMConsultant objConsultant = new EMConsultant();
    BAConsultant objBAConsultant = new BAConsultant();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDivisions();
            BindClientType();
            BindGroup();
            BindGroupMaster();
            if (!string.IsNullOrEmpty(Request.QueryString["ConsultantId"]))
            {
                int ConsultId = Convert.ToInt32(Request.QueryString["ConsultantId"]);

                cmdSubmit.Text = "Update";
                GetConsultantDetails(ConsultId);
            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateConsultant();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultantList.aspx");
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Consultant.aspx");
    }
    #region PrivateMethods
    private void InsertUpdateConsultant()
    {
        try
        {
            DataSet ds = _objBOUtiltiy.getMainAccounts();

            objConsultant.ConsultantId = Convert.ToInt32(hf_ConsultantId.Value);
            objConsultant.KeyId = txtKey.Text;
            objConsultant.Name = txtName.Text;
            objConsultant.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
            objConsultant.Division = Convert.ToInt32(ddlDivision.SelectedIndex);
            objConsultant.Email = txtEmail.Text;
            objConsultant.TelephoneNo = txtTelephoneNo.Text;
            objConsultant.FaxNo = txtFaxNo.Text;
            objConsultant.CellNo = txtCellNo.Text;
            objConsultant.ClientType = Convert.ToInt32(ddlClientType.SelectedIndex);
            objConsultant.ClientStutus = Convert.ToInt32(chkDeActivate.Checked);
            objConsultant.CreatedBy = 0;

            if (cmdSubmit.Text == "Update")
            {
                objConsultant.AccCode = hf_AccCode.Value;
                objConsultant.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objConsultant.ChartedAccName = txtName.Text + "Cash On Sale";
                objConsultant.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objConsultant.RefType = "Consultant";
                objConsultant.RefId = hf_ConsultantId.Value;
            }


            if (cmdSubmit.Text == "Submit")
            {
                objConsultant.ChartedAccName = txtName.Text + "Cash On Sale";
                objConsultant.ChartedMasterAccName = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MainAccId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MainAccId"].ToString());
                objConsultant.Type = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["AcType"].ToString()) ? "0" : ds.Tables[1].Rows[0]["AcType"].ToString();
                objConsultant.BaseCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["BaseCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["BaseCurrency"].ToString());
                objConsultant.TranCurrency = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TranCurrency"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TranCurrency"].ToString());
                objConsultant.DepartmentId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["DepartmentId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["DepartmentId"].ToString());

                //Random generator = new Random();
                //String r = generator.Next(1000, 10000).ToString();

                //objConsultant.AccCode = "136/E" + r;
                objConsultant.CategoryId = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["CategoryId"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["CategoryId"].ToString());
                objConsultant.RefType = "Consultant";


            }
            string AccCode = "";
            int Result = objBAConsultant.InsUpdConsultant(objConsultant);
            if (cmdSubmit.Text == "Submit")
            {

                DataSet objds = objBAConsultant.Get_consultantAccCode();
                if (objds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dtrow in objds.Tables[0].Rows)
                    {

                        AccCode = dtrow["accCode"].ToString();
                    }


                }

                objConsultant.AccCode = AccCode;
                objConsultant.RefId = Result.ToString();
            }
            if (cmdSubmit.Text == "Submit")
            {
                int ChartedLResult = objBAConsultant.InsUpdChartAccounts(objConsultant);
            }
            if (cmdSubmit.Text == "Update")
            {
                int AccountUpdate = objBAConsultant.UpdateChartAccounts(objConsultant);
            }

            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Consultant Details created Successfully");
                clearcontrols();
                Response.Redirect("ConsultantList.aspx");


            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Consultant Details was not created plase try again");
                Response.Redirect("ConsultantList.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void RandamNumber()
    {
        Random r = new Random();
        var x = r.Next(0, 1000000);
        string s = x.ToString("000000");
    }

    private void GetConsultantDetails(int ConsultantId)
    {
        try
        {
            objConsultant.ConsultantId = ConsultantId;
            DataSet ds = objBAConsultant.GetConsultant(ConsultantId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ConsultantId.Value = ds.Tables[0].Rows[0]["ConsultantId"].ToString();
                txtKey.Text = ds.Tables[0].Rows[0]["KeyId"].ToString();
                txtKey.Enabled = false;
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(ds.Tables[0].Rows[0]["GroupId"].ToString()));
                ddlDivision.SelectedIndex = ddlDivision.Items.IndexOf(ddlDivision.Items.FindByValue(ds.Tables[0].Rows[0]["Division"].ToString()));
                txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtTelephoneNo.Text = ds.Tables[0].Rows[0]["TelephoneNo"].ToString();
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNo"].ToString();
                txtFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                ddlClientType.SelectedIndex = ddlClientType.Items.IndexOf(ddlClientType.Items.FindByValue(ds.Tables[0].Rows[0]["ClientType"].ToString()));
                chkDeActivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ClientStutus"]);
                hf_AccCode.Value = ds.Tables[0].Rows[0]["AccCode"].ToString();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }


    #endregion PrivateMethods

    void clearcontrols()
    {
        hf_ConsultantId.Value = "0";
        txtKey.Text = "";
        txtName.Text = "";
        ddlGroup.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        txtEmail.Text = "";
        txtTelephoneNo.Text = "";
        txtCellNo.Text = "";
        txtFaxNo.Text = "";
        ddlClientType.SelectedIndex = 0;
        chkDeActivate.Checked = false;
    }

    #region PublicMethods
    public void BindGroup()
    {

        try
        {
            DataSet ds = new DataSet();
            string Type = "Consultant";
            ds = _objBOUtiltiy.GetGroup(Type);
            if (ds.Tables.Count > 0)
            {
                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
                // ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlGroup.DataSource = null;
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindDivisions()
    {
        try
        {

            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetDivisions();
            if (ds.Tables.Count > 0)
            {

                ddlDivision.DataSource = ds.Tables[0];
                ddlDivision.DataTextField = "DivName";
                ddlDivision.DataValueField = "DivisionId";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("--Please Select --", "0"));

            }
            else
            {
                ddlDivision.DataSource = null;
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    public void BindClientType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _objBOUtiltiy.GetClienttype();
            if (ds.Tables.Count > 0)
            {

                ddlClientType.DataSource = ds.Tables[0];
                ddlClientType.DataTextField = "Name";
                ddlClientType.DataValueField = "Id";
                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlClientType.DataSource = null;
                ddlClientType.DataBind();
                ddlClientType.Items.Insert(0, new ListItem("--Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    public void BindGroupMaster()
    {

        try
        {
            DataSet ds = new DataSet();
            int Id = 0;
            ds = _objBOUtiltiy.GetGroupMaster(Id);
            if (ds.Tables.Count > 0)
            {

                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Name";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
            else
            {
                ddlGroup.DataSource = null;
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("-- Please Select --", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    #endregion PublicMethods



    protected void txtKey_TextChanged(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = _objBOUtiltiy.CheckKeyCodeExitorNot(txtKey.Text, "Consultant");

        ds.Tables.Add(dt);

        if (ds.Tables[0].Rows.Count != 0 || ds.Tables[0].Rows.Count > 0)
        {
            lblaccnoerr.Text = "Already Exist";
            lblaccnoerr.ForeColor = System.Drawing.Color.Red;
            txtKey.Text = "";
        }
        else
        {
            lblaccnoerr.Text = "Available";
            lblaccnoerr.ForeColor = System.Drawing.Color.DarkBlue;

        }
    }
}

