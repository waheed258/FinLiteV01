﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataManager;
using BusinessManager;
using EntityManager;

public partial class Admin_DepositTransaction : System.Web.UI.Page
{
    BALInvoice _objBALInvoice = new BALInvoice();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindClienttypes();
            BindReciptTypes();
            txtDepstConsultant.Text = Session["UserFullName"].ToString();
            gvReciptData.DataSource = null;
            gvReciptData.DataBind();
            BindSecondRecieptsGrid();
            BindBankAccounts();
        }
    }
    #region PrivateMethods
    private void BindClienttypes()
    {
        try
        {
            ddlDepstClientPredfix.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetClienttype();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlDepstClientPredfix.DataSource = ObjDsClients;
                ddlDepstClientPredfix.DataValueField = "Id";
                ddlDepstClientPredfix.DataTextField = "Name";
                ddlDepstClientPredfix.DataBind();
                ddlDepstClientPredfix.Items.Insert(0, new ListItem("Select Client", "0"));
            }
            else
            {
                ddlDepstClientPredfix.Items.Insert(0, new ListItem("Select Client", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindBankAccounts()
    {
        try
        {
            ddlDepositAcoount.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlDepositAcoount.DataSource = ObjDsClients;
                ddlDepositAcoount.DataValueField = "BankAcId";
                ddlDepositAcoount.DataTextField = "BankName";
                ddlDepositAcoount.DataBind();
                ddlDepositAcoount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlDepositAcoount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindReciptTypes()
    {
        try
        {
            BAReceiptType objRecieptType = new BAReceiptType();
            int reciptType = 0;
            ddldpstReceiptType.Items.Clear();
            DataSet ObjDsClients = objRecieptType.GetReceiptType(reciptType);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddldpstReceiptType.DataSource = ObjDsClients;
                ddldpstReceiptType.DataValueField = "ReceiptId";
                ddldpstReceiptType.DataTextField = "RecDescription";
                ddldpstReceiptType.DataBind();
                ddldpstReceiptType.Items.Insert(0, new ListItem("Select Receipt Type", "0"));
            }
            else
            {
                ddldpstReceiptType.Items.Insert(0, new ListItem("Select Receipt Type", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    #endregion PrivateMethods

    #region gridMethods

    private void  BindUnbankedReceipts(int RType,int CType)
    {
        BADepositTransaction objBADepositTransaction = new BADepositTransaction();
        DataSet objUnbankReceiptsList= objBADepositTransaction.getUnbankReceipts(RType, CType);
        if (objUnbankReceiptsList.Tables[0].Rows.Count > 0)
        {
            gvReciptData.DataSource = objUnbankReceiptsList.Tables[0];
            gvReciptData.DataBind();
        }
        else
        {
            gvReciptData.DataSource = null;
            gvReciptData.DataBind();
            lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
        }
    }

    protected void BindSecondRecieptsGrid()
    {
        DataTable dt = (DataTable)ViewState["GetUnbankedRecords"];
        gvSeocondRecipts.DataSource = dt;
        gvSeocondRecipts.DataBind();
    }

    private void GetSelectedRows()
    {
        DataTable dt;
        if (ViewState["GetUnbankedRecords"] != null)
            dt = (DataTable)ViewState["GetUnbankedRecords"];
        else
            dt = CreateTable();
        for (int i = 0; i < gvReciptData.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvReciptData.Rows[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked)
            {
                dt = AddGridRow(gvReciptData.Rows[i], dt);
            }
            else
            {
                dt = RemoveRow(gvReciptData.Rows[i], dt);
            }
        }
        ViewState["GetUnbankedRecords"] = dt;
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReceivedTransactionId");
        dt.Columns.Add("TransactionDate");
        dt.Columns.Add("RecieptType");
        dt.Columns.Add("ClientType");
        dt.Columns.Add("ClientAcNo");
        dt.Columns.Add("AllocatedAmount");
        dt.Columns.Add("invoiceId");
        dt.AcceptChanges();
        return dt;
    }
    private DataTable AddGridRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ReceivedTransactionId = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length <= 0)
        {
            dt.Rows.Add();
            int rowscount = dt.Rows.Count - 1;
            dt.Rows[rowscount]["ReceivedTransactionId"] = gvRow.Cells[1].Text;
            dt.Rows[rowscount]["TransactionDate"] = gvRow.Cells[2].Text;
            dt.Rows[rowscount]["RecieptType"] = gvRow.Cells[3].Text;
            dt.Rows[rowscount]["ClientType"] = gvRow.Cells[4].Text;
            dt.Rows[rowscount]["ClientAcNo"] = gvRow.Cells[5].Text;
            dt.Rows[rowscount]["AllocatedAmount"] = gvRow.Cells[6].Text;
            dt.Rows[rowscount]["invoiceId"] = gvRow.Cells[7].Text;
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ReceivedTransactionId = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }


    private void UnBankedReciptsCount()
    {
        decimal sum = 0.0M;
        for (int i = 0; i < gvReciptData.Rows.Count; i++)
        {
            sum += Convert.ToDecimal(gvReciptData.Rows[i].Cells[6].Text.ToString());
        }
        unBankAmount.InnerText = _objBOUtiltiy.FormatTwoDecimal(sum.ToString());
        unBankCount.InnerText = gvReciptData.Rows.Count.ToString();
    }

    private void ThisDepositReciptsCount()
    {
        decimal sum = 0.0M;
        for (int i = 0; i < gvReciptData.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvReciptData.Rows[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked)
            {
                sum += Convert.ToDecimal(gvReciptData.Rows[i].Cells[6].Text.ToString());
            }
            
        }
        spnThisDepositAmnt.InnerText = _objBOUtiltiy.FormatTwoDecimal(sum.ToString());
        spnCurDpstCount.InnerText = gvSeocondRecipts.Rows.Count.ToString();
    }
    #endregion gridMethods

    protected void ddlDepstClientPredfix_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepstClientPredfix.SelectedIndex > 0)
            {
                int Rtype = 0;
                BindUnbankedReceipts(Rtype, Convert.ToInt32(ddlDepstClientPredfix.SelectedValue));
                UnBankedReciptsCount();
            }
            else if (ddldpstReceiptType.SelectedIndex > 0 && ddlDepstClientPredfix.SelectedIndex > 0)
            {
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), Convert.ToInt32(ddlDepstClientPredfix.SelectedValue));
                UnBankedReciptsCount();
            }
            else
            {

            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
        
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        GetSelectedRows();
       
        BindSecondRecieptsGrid();
        ThisDepositReciptsCount();
    }
    protected void ddldpstReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldpstReceiptType.SelectedIndex > 0)
            {
                int CType = 0;
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), CType);
                UnBankedReciptsCount();

            }
            else if (ddldpstReceiptType.SelectedIndex > 0 && ddlDepstClientPredfix.SelectedIndex > 0)
            {
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), Convert.ToInt32(ddlDepstClientPredfix.SelectedValue));
                UnBankedReciptsCount();
            }
            else
            {

            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnDepositSave_Click(object sender, EventArgs e)
    {
        try
        {
           
            EmDepositMaster objEmDepositMaster = new EmDepositMaster();
            objEmDepositMaster.DepositDate = Convert.ToDateTime(txtDpstDate.Text);
            objEmDepositMaster.DepositClientPrefix =  Convert.ToInt32(ddlDepstClientPredfix.SelectedValue);
            objEmDepositMaster.DepositComments =txtDpstComments.Text;
            objEmDepositMaster.DepositConsultant = txtDepstConsultant.Text;
            objEmDepositMaster.DepositRecieptType = Convert.ToInt32(ddldpstReceiptType.SelectedValue);
            objEmDepositMaster.DepositSourceRef = txtDpstSourceRef.Text;
            objEmDepositMaster.TotalRecieptsDeposited =Convert.ToInt32( spnCurDpstCount.InnerText);
            objEmDepositMaster.TotalDepositAmount = Convert.ToDecimal(spnThisDepositAmnt.InnerText);
            objEmDepositMaster.DepositAcId = Convert.ToInt32(ddlDepositAcoount.SelectedValue); 
            BADepositTransaction  objBADepositTransaction = new BADepositTransaction();
        int result =  objBADepositTransaction.insertDepositMaster(objEmDepositMaster);
        if (result > 0)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");
            // clearcontrols();
            for (int i = 0; i < gvReciptData.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvReciptData.Rows[i].Cells[0].FindControl("chkSelect");
                if (chk.Checked)
                {
                    EMDepositChild objEMDepositChild = new EMDepositChild();
                    objEMDepositChild.ReceiptId = Convert.ToInt32(gvReciptData.Rows[i].Cells[1].Text);
                    objEMDepositChild.RecieptDate = Convert.ToDateTime ( gvReciptData.Rows[i].Cells[2].Text);
                    objEMDepositChild.ReceiptType = gvReciptData.Rows[i].Cells[3].Text;
                    objEMDepositChild.ReciptClient = gvReciptData.Rows[i].Cells[4].Text;
                    objEMDepositChild.ReceiptAmount = Convert.ToDecimal(gvReciptData.Rows[i].Cells[6].Text);
                    objEMDepositChild.InvoiceId =  Convert.ToInt32(gvReciptData.Rows[i].Cells[7].Text);
                    objEMDepositChild.DepositAcId = Convert.ToInt32(ddlDepositAcoount.SelectedValue); 
                    objEMDepositChild.DepositTransMasterId = result;
                 int childResult =   objBADepositTransaction.insertDepositChild(objEMDepositChild);
                    if(childResult>0)
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");
                        Response.Redirect("DepositTransactionList");
                    }
                    else
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", " Child Deposit Was not Added Successfully");
                    }


                    //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                    //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                }
                else
                {
                    
                }
            }
          

        }
        else
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Deposit  was not Added plase try again");
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepositTransactionList", false);
    }
}