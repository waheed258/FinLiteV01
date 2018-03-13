using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataManager;
using BusinessManager;
using EntityManager;
using System.Data.SqlClient;

public partial class Admin_DepositTransaction : System.Web.UI.Page
{
    BALInvoice _objBALInvoice = new BALInvoice();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    DOUtility _doUtility = new DOUtility();

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

    private void BindUnbankedReceipts(int RType, int CType)
    {
        DataTable dts;

        BADepositTransaction objBADepositTransaction = new BADepositTransaction();
        DataSet objUnbankReceiptsList = objBADepositTransaction.getUnbankReceipts(RType, CType);
        if (objUnbankReceiptsList.Tables[0].Rows.Count > 0)
        {
            gvReciptData.DataSource = objUnbankReceiptsList.Tables[0];
            gvReciptData.DataBind();
            dts = objUnbankReceiptsList.Tables[0];
            ViewState["RightGridTotalRecords"] = dts;
            UnBankedReciptsCount();
            ThisDepositReciptsCount();
        }
        else
        {
            gvReciptData.DataSource = null;
            gvReciptData.DataBind();
            gvSeocondRecipts.DataSource = null;
            gvSeocondRecipts.DataBind();
            ViewState["RightGridTotalRecords"] = null;
            UnBankedReciptsCount();
            ThisDepositReciptsCount();
            lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
        }
       
    }

    protected void BindSecondRecieptsGrid()
    {
        DataTable dt = (DataTable)ViewState["getLeftGirdRecords"];
        gvSeocondRecipts.DataSource = dt;
        gvSeocondRecipts.DataBind();
    }

    private void GetRightSelectedRows()
    {
        DataTable rightGridDatatable = (DataTable)ViewState["RightGridTotalRecords"];
        DataTable leftGridDt;
        if (ViewState["getLeftGirdRecords"] != null)
            leftGridDt = (DataTable)ViewState["getLeftGirdRecords"];
        else
            leftGridDt = CreateTable();
        for (int i = 0; i < gvReciptData.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvReciptData.Rows[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked)
            {
                leftGridDt = AddGridRow(gvReciptData.Rows[i], leftGridDt);
                rightGridDatatable = RemoveRow(gvReciptData.Rows[i], rightGridDatatable);
            }
            else
            {
                leftGridDt = RemoveRow(gvReciptData.Rows[i], leftGridDt);
            }
        }
        ViewState["getLeftGirdRecords"] = leftGridDt;
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReceivedTransactionId");
        dt.Columns.Add("TransactionDate");
        dt.Columns.Add("RecieptType");
        dt.Columns.Add("ClientType");
        dt.Columns.Add("ClientAcNo");
        dt.Columns.Add("AllocatedAmount",typeof(decimal));
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
            dt.Rows[rowscount]["AllocatedAmount"] = Convert.ToDecimal( gvRow.Cells[6].Text);
            dt.Rows[rowscount]["invoiceId"] = Convert.ToInt32(gvRow.Cells[7].Text);
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
        decimal? sum = 0.0M;
        DataTable dt = (DataTable)ViewState["RightGridTotalRecords"];
        if (dt != null)
        {
            sum = dt.AsEnumerable().Sum(row => row.Field<decimal>("AllocatedAmount"));
            unBankAmount.InnerText = _objBOUtiltiy.FormatTwoDecimal(sum.ToString());
            unBankCount.InnerText =  dt.Rows.Count.ToString();
        }
        else
        {
            unBankAmount.InnerText = _objBOUtiltiy.FormatTwoDecimal(sum.ToString());
            unBankCount.InnerText = "0";
        }
    }

    private void ThisDepositReciptsCount()
    {
        decimal? sumAmounts = 0.0M;
        DataTable dt = (DataTable)ViewState["getLeftGirdRecords"];
        if (dt != null)
        {
            sumAmounts = dt.AsEnumerable().Sum(row => row.Field<decimal>("AllocatedAmount"));
            spnThisDepositAmnt.InnerText = _objBOUtiltiy.FormatTwoDecimal(sumAmounts.ToString());
            spnCurDpstCount.InnerText = dt.Rows.Count.ToString();
        }
        else
        {
            spnThisDepositAmnt.InnerText = _objBOUtiltiy.FormatTwoDecimal(sumAmounts.ToString());
            spnCurDpstCount.InnerText = "0";
        }
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
               

            }
            else if (ddldpstReceiptType.SelectedIndex > 0 && ddlDepstClientPredfix.SelectedIndex > 0)
            {
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), Convert.ToInt32(ddlDepstClientPredfix.SelectedValue));
               
            }
            else
            {
                UnBankedReciptsCount();
                ThisDepositReciptsCount();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        GetRightSelectedRows();

        BindSecondRecieptsGrid();
        ThisDepositReciptsCount();
        UnBankedReciptsCount();

        gvReciptData.DataSource = ViewState["RightGridTotalRecords"];
        gvReciptData.DataBind();

    }
    protected void ddldpstReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldpstReceiptType.SelectedIndex > 0)
            {
                int CType = 0;
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), CType);
                

            }
            else if (ddldpstReceiptType.SelectedIndex > 0 && ddlDepstClientPredfix.SelectedIndex > 0)
            {
                BindUnbankedReceipts(Convert.ToInt32(ddldpstReceiptType.SelectedValue), Convert.ToInt32(ddlDepstClientPredfix.SelectedValue));
               
            }
            else
            {
                UnBankedReciptsCount();
                ThisDepositReciptsCount();
            }
        }
        catch (Exception ex)
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
            objEmDepositMaster.DepositClientPrefix = Convert.ToInt32(ddlDepstClientPredfix.SelectedValue);
            objEmDepositMaster.DepositComments = txtDpstComments.Text;
            objEmDepositMaster.DepositConsultant = txtDepstConsultant.Text;
            objEmDepositMaster.DepositRecieptType = Convert.ToInt32(ddldpstReceiptType.SelectedValue);
            objEmDepositMaster.DepositSourceRef = txtDpstSourceRef.Text;
            objEmDepositMaster.TotalRecieptsDeposited = Convert.ToInt32(spnCurDpstCount.InnerText);
            objEmDepositMaster.TotalDepositAmount = Convert.ToDecimal(spnThisDepositAmnt.InnerText);
            objEmDepositMaster.DepositAcId = Convert.ToInt32(ddlDepositAcoount.SelectedValue);
            BADepositTransaction objBADepositTransaction = new BADepositTransaction();
            int result = objBADepositTransaction.insertDepositMaster(objEmDepositMaster);
            if (result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");
               
              

                DataTable leftGirdRecords = (DataTable)ViewState["getLeftGirdRecords"];


                DataColumn dtc = new DataColumn("DepositTransMasterId", typeof(System.Int32));
                dtc.DefaultValue = result;
                leftGirdRecords.Columns.Add(dtc);
                DataColumn depositAcId = new DataColumn("DepositAccountId", typeof(System.Int32));
                depositAcId.DefaultValue = ddlDepositAcoount.SelectedValue;
                leftGirdRecords.Columns.Add(depositAcId);

               


                //string xmlString = string.Empty;
                //using (TextWriter writer = new StringWriter())
                //{
                //    leftGirdRecords.WriteXml(writer);
                //    xmlString = writer.ToString();
                //}
                //SqlConnection objMySqlConn = _doUtility.GetSqlConnection();
                //objMySqlConn.Open();
                //using (SqlBulkCopy SBC = new SqlBulkCopy(objMySqlConn))
                //{



                //    SBC.ColumnMappings.Add("ReceivedTransactionId", "ReceiptId");
                //    SBC.ColumnMappings.Add("TransactionDate", "RecieptDate");
                //    SBC.ColumnMappings.Add("RecieptType", "ReceiptType");
                //    // SBC.ColumnMappings.Add("ClientType", "ClientType");
                //    SBC.ColumnMappings.Add("ClientAcNo", "ReciptClient");
                //    SBC.ColumnMappings.Add("AllocatedAmount", "ReceiptAmount");
                //    SBC.ColumnMappings.Add("invoiceId", "InvoiceId");
                //    SBC.ColumnMappings.Add("DepositTransMasterId", "DepositTransMasterId");
                //    SBC.ColumnMappings.Add("DepositAccountId", "DepositAcId");
                //    SBC.DestinationTableName = "DepositTranasctionsChild";
                //    SBC.WriteToServer(leftGirdRecords);
                //}
                Response.Redirect("DepositTransactionList");





                for (int i = 0; i < leftGirdRecords.Rows.Count; i++)
                {
                    //CheckBox chk = (CheckBox)gvReciptData.Rows[i].Cells[0].FindControl("chkSelect");
                    //if (chk.Checked)
                    //{
                    EMDepositChild objEMDepositChild = new EMDepositChild();
                    objEMDepositChild.ReceiptId = Convert.ToInt32(leftGirdRecords.Rows[i]["ReceivedTransactionId"].ToString());
                    objEMDepositChild.RecieptDate = Convert.ToDateTime(leftGirdRecords.Rows[i]["TransactionDate"].ToString());
                    objEMDepositChild.ReceiptType = leftGirdRecords.Rows[i]["RecieptType"].ToString();
                    objEMDepositChild.ReciptClient = leftGirdRecords.Rows[i]["ClientAcNo"].ToString();
                    objEMDepositChild.ReceiptAmount = Convert.ToDecimal(leftGirdRecords.Rows[i]["AllocatedAmount"].ToString());
                    objEMDepositChild.InvoiceId = Convert.ToInt32(leftGirdRecords.Rows[i]["invoiceId"].ToString());
                    objEMDepositChild.DepositAcId = Convert.ToInt32(ddlDepositAcoount.SelectedValue);
                    objEMDepositChild.DepositTransMasterId = result;
                    int childResult = objBADepositTransaction.insertDepositChild(objEMDepositChild);
                    if (childResult > 0)
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Deposit Added Successfully");
                        // Response.Redirect("DepositTransactionList.aspx");
                    }
                    else
                    {
                        lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", " Child Deposit Was not Added Successfully");
                    }


                    //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                    //objEMDepositChild.RecieptDate = Convert.ToDateTime(gvReciptData.Rows[i].Cells[2].Text);
                    //}
                    //else
                    //{

                    //}
                }
                Response.Redirect("DepositTransactionList.aspx");


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
    protected void chkRightSelect_CheckedChanged(object sender, EventArgs e)
    {
        GetLeftSelectedRows();
        BindfirstRecieptsGrid();
        
        ThisDepositReciptsCount();
        UnBankedReciptsCount();
        gvSeocondRecipts.DataSource = ViewState["getLeftGirdRecords"];
        gvSeocondRecipts.DataBind();
    }

    private void GetLeftSelectedRows()
    {
        DataTable rightGridRecords = (DataTable)ViewState["RightGridTotalRecords"];
        DataTable leftGirdRecords;
        if (ViewState["getLeftGirdRecords"] != null)
            leftGirdRecords = (DataTable)ViewState["getLeftGirdRecords"];
        else
            leftGirdRecords = CreateTable();
        for (int i = 0; i < gvSeocondRecipts.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvSeocondRecipts.Rows[i].Cells[0].FindControl("chkRightSelect");
            if (chk.Checked)
            {

                rightGridRecords = AddGridRow(gvSeocondRecipts.Rows[i], rightGridRecords);
                leftGirdRecords = RemoveRow(gvSeocondRecipts.Rows[i], leftGirdRecords);
            }
            else
            {
                rightGridRecords = RemoveRow(gvSeocondRecipts.Rows[i], rightGridRecords);
            }
        }
        ViewState["getLeftGirdRecords"] = leftGirdRecords;
    }
    protected void BindfirstRecieptsGrid()
    {
        DataTable dt = (DataTable)ViewState["RightGridTotalRecords"];
        gvReciptData.DataSource = dt;
        gvReciptData.DataBind();
    }
  
}