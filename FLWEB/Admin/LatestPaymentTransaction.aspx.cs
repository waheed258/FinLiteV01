﻿using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LatestPaymentTransaction : System.Web.UI.Page
{
    BALInvoice _objBALInvoice = new BALInvoice();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BALTransactions _objBALTransactions = new BALTransactions();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {

            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindClienttypes();
            txtPreparedBy.Text = Session["UserFullName"].ToString();
            gvCustomers.DataSource = null;
            gvCustomers.DataBind();
            BindPaymentTypes();
            BindDivision();
            BindAccountTypes();
             
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lblTotalAvailable.Text != "")
        {
        
            try
            {
               
                string categoryname = ddlAccType.SelectedItem.Text;


                foreach (GridViewRow row in gvCustomers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {



                        TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
                        HiddenField hfTicketId = row.FindControl("hfTicketId") as HiddenField;
                        HiddenField hfInvId = row.FindControl("hfInvId") as HiddenField;

                        string s = hfTicketId.Value;
                        string invid = hfInvId.Value;
                        string[] values = s.Split(',');
                        string[] InvidValue = invid.Split(',');
                        decimal paymentamount = 0;
                        paymentamount = Convert.ToDecimal(txtAmount.Text);

                        for (int i = 0; i < values.Length; i++)
                        {
                            for (int j = i; j < InvidValue.Length ; j++)
                            {
                                values[i] = values[i].Trim();
                                InvidValue[j] = InvidValue[j].Trim();

                                if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
                                {
                                    txtThisEntry.Text = "0";

                                }

                                if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                                {
                                    PaymentTransaction objTransactionMaster = new PaymentTransaction();
                                    int status = 0;


                                    DataSet dsobj = _objBALInvoice.GetSupplierOpeningAmount(Convert.ToInt32(values[i].ToString()), Convert.ToInt32(InvidValue[j].ToString()), status, categoryname);
                                    decimal openingAmount = 0; decimal duetoBsp = 0;
                                    if (dsobj.Tables.Count > 0 && dsobj.Tables[0].Rows.Count > 0)
                                    {
                                        openingAmount = Convert.ToDecimal(dsobj.Tables[0].Rows[0]["SupplOpeningAmount"].ToString());
                                        duetoBsp = Convert.ToDecimal(dsobj.Tables[0].Rows[0]["Bsp"].ToString());



                                        if (openingAmount <= paymentamount)
                                        {
                                            objTransactionMaster.AllocatedAmount = openingAmount;
                                            objTransactionMaster.InvoiceBalanceAmount = openingAmount - openingAmount;

                                            paymentamount = paymentamount - openingAmount;
                                            objTransactionMaster.PaymentBalanceAmount = paymentamount;
                                        }
                                        else if (Convert.ToDecimal(txtThisEntry.Text) >= paymentamount)
                                        {
                                            objTransactionMaster.AllocatedAmount = paymentamount;

                                            objTransactionMaster.InvoiceBalanceAmount = openingAmount - paymentamount;

                                            objTransactionMaster.PaymentBalanceAmount = paymentamount - paymentamount;
                                        }

                                        objTransactionMaster.TicketId = Convert.ToInt32(values[i].ToString());
                                        objTransactionMaster.InvoiceId = Convert.ToInt32(InvidValue[j].ToString());
                                        objTransactionMaster.Divission = ddlDivision.SelectedValue;
                                        objTransactionMaster.PaymentType = ddlPaymentType.SelectedValue;
                                        objTransactionMaster.AutoDepositeId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                                        objTransactionMaster.AutoDepositeAccountNo = ddlAutoDepositeAccount.SelectedItem.Text;
                                        objTransactionMaster.ClientSupplTypeId = Convert.ToInt32(ddlAccType.SelectedValue);
                                        if (Convert.ToInt32(ddlAccountNo.SelectedValue) == 0)
                                        {
                                            objTransactionMaster.ClientSupplAccountNo = " ";
                                        }
                                        else
                                        {
                                            objTransactionMaster.ClientSupplAccountNo = ddlAccountNo.SelectedItem.Text;
                                        }

                                        objTransactionMaster.ClientSupplAccountNoID = Convert.ToInt32(ddlAccountNo.SelectedValue);
                                        objTransactionMaster.PayeeDetails = txtPayeeDetails.Text;
                                        objTransactionMaster.PaymentAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                                        //objTransactionMaster.PrvClientOpenAmount = Convert.ToDecimal(lblPrvClientOpenAmount.Text);

                                        objTransactionMaster.Details = txtDetails.Text;
                                        objTransactionMaster.Messages = txtMessage.Text;
                                        objTransactionMaster.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                                        objTransactionMaster.PaymentSourceRef = txtSourceRef.Text;
                                        objTransactionMaster.categoryName = ddlAccType.SelectedItem.Text;
                                        objTransactionMaster.MainAccount = lblMainAcc.Text;
                                        objTransactionMaster.CategoryId = Convert.ToInt32(ddlAccType.SelectedValue.ToString());
                                      _objBALTransactions.PaymentTransactionInsert(objTransactionMaster);
                                    }
                                }
                            }
                        }
                    }
                }


                Transaction objTransaction = new Transaction();

                objTransaction.FmAccountNoId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedItem.Value.ToString());
                objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlAccountNo.SelectedItem.Value.ToString());
                string category = ddlAccType.SelectedItem.Text;
                DataSet ds = _objBALTransactions.Transaction_GetAccountsData(Convert.ToInt32(ddlAutoDepositeAccount.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlAccountNo.SelectedItem.Value.ToString()), "PT", category);
                string FmAcccode = "";
                string FmMainAccCode = "";
               
                string RefMainAcc = "";
                string RefAccCode = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    RefAccCode = ds.Tables[0].Rows[0]["AccCode"].ToString();
                    RefMainAcc = ds.Tables[0].Rows[0]["MainAccCode"].ToString();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    FmAcccode = ds.Tables[1].Rows[0]["BankGiAccount"].ToString();
                    FmMainAccCode = ds.Tables[1].Rows[0]["MainAccCode"].ToString();

                }


                objTransaction.CreditAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                objTransaction.FmAccountNO = FmAcccode;
                objTransaction.FmMainAccount = FmMainAccCode;
                objTransaction.ReferenceAccountNO = RefAccCode;
                objTransaction.DebitAmount = 0;
                objTransaction.ReferenceNo = txtSourceRef.Text;
                objTransaction.ToMainAccount = RefMainAcc;
                // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                // objTransaction.InvoiceNo = "";

                objTransaction.ReferenceType = "PT";
                objTransaction.CreatedBy = 0;

                objTransaction.BalanceAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                _objBALTransactions.TransactionInsert(objTransaction);


                //Transaction objTransaction = new Transaction();
                //int FmAccountNoId = 0;
                //objTransaction.FmAccountNoId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedItem.Value.ToString());
                //objTransaction.ReferenceAccountNoId = Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue);
                //string category = ddlAccType.SelectedItem.Text;
                //DataSet ds = _objBALTransactions.Transaction_GetAccountsData(FmAccountNoId, Convert.ToInt32(ddlAutoDepositeAccount.SelectedValue), "PT", category);

                //List<string> FmAcccode = new List<string>();
                //List<string> FmMainAccCode = new List<string>();
                //List<string> RefMainAcc = new List<string>();
                //List<string> RefAccCode = new List<string>();
                //List<string> AllocateAmt = new List<string>();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                //    {
                //        FmAcccode.Add(dtlRow["AccCode"].ToString());
                //        FmMainAccCode.Add(dtlRow["MainAccCode"].ToString());
                //        AllocateAmt.Add(dtlRow["AllocatedAmount"].ToString());
                //    }
                //}
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    foreach (DataRow dtlRow in ds.Tables[1].Rows)
                //    {
                //        RefAccCode.Add(dtlRow["BankAcNo"].ToString());
                //        RefMainAcc.Add(dtlRow["MainAccCode"].ToString());
                //    }
                //}
                //int count = FmAcccode.Count;

                //for (int i = 0; i <= count - 1; i++)
                //{
                //    objTransaction.DebitAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                //    objTransaction.FmAccountNO = FmAcccode[i].ToString();
                //    objTransaction.MainAccount = FmMainAccCode[i].ToString();
                //    objTransaction.ReferenceAccountNO = RefAccCode[i].ToString();
                //    objTransaction.CreditAmount = 0;
                //    objTransaction.ReferenceNo = txtSourceRef.Text;
                //    // objTransaction.InvoiceId = Convert.ToInt32(hfInvId.Value);
                //    // objTransaction.InvoiceNo = "";

                //    objTransaction.ReferenceType = "PT";
                //    objTransaction.CreatedBy = 0;

                //    objTransaction.BalanceAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
                //    _objBALTransactions.TransactionInsert(objTransaction);



                //    objTransaction.CreditAmount = Convert.ToDecimal(AllocateAmt[i].ToString());
                //    objTransaction.FmAccountNO = RefAccCode[i].ToString();
                //    objTransaction.MainAccount = RefMainAcc[i].ToString();
                //    objTransaction.ReferenceAccountNO = FmAcccode[i].ToString();
                //    objTransaction.DebitAmount = 0;
                //    objTransaction.ReferenceNo = txtSourceRef.Text;

                //    objTransaction.ReferenceType = "PT";
                //    objTransaction.CreatedBy = 0;

                //    objTransaction.BalanceAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
                //    _objBALTransactions.TransactionInsert(objTransaction);


                //}


                ReceivedTransaction objOpenAmountDetails = new ReceivedTransaction();
                objOpenAmountDetails.SupplTypeId = Convert.ToInt32(ddlAccType.SelectedValue);
                objOpenAmountDetails.SupplNameId = Convert.ToInt32(ddlAccountNo.SelectedValue);
                objOpenAmountDetails.ReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0; ;
                //  objOpenAmountDetails.PrvOpenAmount = lblPrvClientOpenAmount.Text != "" ? Convert.ToDecimal(lblPrvClientOpenAmount.Text) : 0;
                objOpenAmountDetails.AlocatedAmount = lblAllocatedAmount.Text != "" ? Convert.ToDecimal(lblAllocatedAmount.Text) : 0;
                objOpenAmountDetails.PaymentOpenAmount = lblReceiptOpenAmount.Text != "" ? Convert.ToDecimal(lblReceiptOpenAmount.Text) : 0;
                objOpenAmountDetails.SourceRef = txtSourceRef.Text;
                objOpenAmountDetails.PaymentType = ddlPaymentType.SelectedItem.Text;
                objOpenAmountDetails.FromAccount = ddlAutoDepositeAccount.SelectedItem.Text;
                objOpenAmountDetails.ToAccount = ddlAccountNo.SelectedItem.Text;
                objOpenAmountDetails.CreatedBy = Convert.ToInt32(Session["UserLoginId"]);
                int result = _objBALTransactions.OpenAmountSupplDetailsInsertUpdateMaster(objOpenAmountDetails);
                if (result > 0)
                {

                    Response.Redirect("PaymentTransactionList.aspx");
                }
                PaymentTransactionClear();

            }
                        
            catch (Exception ex)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
                ExceptionLogging.SendExcepToDB(ex);
            }    
    }
     else
        {
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(),
      "Msg", "alert('Please Allocate Current Amount');", true);
             }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        PaymentTransactionClear();
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    //StringWriter sw = new StringWriter();
    //    //HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    //StringBuilder sb = new StringBuilder();
    //    //sb.Append("<script type = 'text/javascript'>");
    //    //sb.Append("window.onload = new function(){");
    //    //sb.Append("var divToPrint=document.getElementById('print');");
    //    //sb.Append("var printWin = window.open('', '', 'left=0");
    //    //sb.Append(",top=0,width=800,height=600,status=0');");

    //    //sb.Append("printWin.document.write(divToPrint.outerHTML);");
    //    //sb.Append("printWin.document.close();");
    //    //sb.Append("printWin.focus();");
    //    //sb.Append("printWin.print();}");
    //    //sb.Append("</script>");
    //    //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    //}

    
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDecimal(lblSuppOpenAmt.Text) == Convert.ToDecimal(txtAmount.Text))
        {

            lblTotalAvailable.Text = "0.00";
            lblReceiptOpenAmount.Text = "0.00";
            lblAllocatedAmount.Text = "0.00";
            lblTotalAvailable.Text = "0.00";

            lblAllocated.Text = "Allocated(" + 0 + ")";
         //   chkSelect.Checked = false;

            decimal CurrentReceiptAmount = lblSuppOpenAmt.Text != "" ? Convert.ToDecimal(lblSuppOpenAmt.Text) : 0;
             lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal(CurrentReceiptAmount.ToString());

            lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal((Convert.ToDecimal(lblTotalAvailable.Text) - Convert.ToDecimal(lblAllocatedAmount.Text)).ToString().ToString());
           
            if(ddlAccountNo.SelectedValue != "0")
            {
                ddlAccountNo_SelectedIndexChanged(null, null);
            }



            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;

                    txtThisEntry.Text = txtAmount.Text;


                    lblTotalAvailable.Text = txtThisEntry.Text;

                   
                    decimal InvoiceOpenAmount = 0;
                    decimal ThisEntryAmount = 0;

                    if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
                    {

                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
                        InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);
                        row.Cells[7].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());

                      }


                    }

                }
            

            // ddlAccType_SelectedIndexChanged(null, null);

            //int Id = Convert.ToInt32(ddlAccType.SelectedValue.ToString());
            //string name = ddlAccType.SelectedItem.Text;

            //DataSet objDsInvLst = _objBALTransactions.PaymentTransaction_GetDataBycategory(Id, name);
            //if (objDsInvLst.Tables[0].Rows.Count > 0)
            //{
            //    decimal  paiedAmount = Convert.ToDecimal(objDsInvLst.Tables[1].Rows[0]["SupplPaiedAmount"].ToString());

            //}
        }
             else
             {
                 
                 foreach (GridViewRow row in gvCustomers.Rows)
                 {
                     if (row.RowType == DataControlRowType.DataRow)
                     {

                         TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;

                         txtThisEntry.Text ="";
                         lblTotalAvailable.Text = txtThisEntry.Text;
                     }
                 }

                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(),
      "Msg", "alert('Please Allocate Current Amount');", true);
             }
            }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAccType.SelectedIndex > 0)
            {
                BindAutoDepositeAccount(Convert.ToInt32(ddlAccType.SelectedValue.ToString()), ddlAccType.SelectedItem.Text);
            }
            else
            {
                BindAutoDepositeAccount(0,null);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }



    //protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    decimal ClientTotalAvailable = 0;
    //    decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;

    //    if (ddlAccType.SelectedIndex > 0)
    //    {
    //        int Id= Convert.ToInt32(ddlAccType.SelectedValue.ToString());
    //        string name = ddlAccType.SelectedItem.Text;

    //        DataSet objDsInvLst = _objBALTransactions.PaymentTransaction_GetDataBycategory(Id, name);

    //       if (objDsInvLst.Tables[0].Rows.Count > 0)
    //       {
    //           gvData.DataSource = objDsInvLst.Tables[0];
    //           gvData.DataBind();
    //       }
    //       else
    //       {
    //           gvData.DataSource = null;
    //           gvData.DataBind();
    //           lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
    //       }
    //       if (objDsInvLst.Tables[1].Rows.Count > 0)
    //       {
    //           lblPrvClientOpenAmount.Text = objDsInvLst.Tables[1].Rows[0]["OpenAmount"].ToString();
    //           ClientTotalAvailable = CurrentReceiptAmount + Convert.ToDecimal(lblPrvClientOpenAmount.Text);
    //       }
    //       else
    //       {
    //           ClientTotalAvailable = CurrentReceiptAmount;
    //       }
    //       lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
    //       lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
    //       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
    //        BindAutoDepositeAccount(Convert.ToInt32(ddlAccType.SelectedValue.ToString()));
    //    }
    //    else
    //    {
    //       BindAutoDepositeAccount(0);
    //    }

    //    chkSelect.Checked = false;
    //}
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvClosedInvoice.DataSource = null;
            gvClosedInvoice.DataBind();




            gvInvoiceOpen.DataSource = null;
            gvInvoiceOpen.DataBind();

            int suppltypeid = Convert.ToInt32(ddlAccountNo.SelectedValue);
            string supplname = ddlAccType.SelectedItem.Text;
            int categoryid = Convert.ToInt32(ddlAccType.SelectedValue);
            string category = ddlAccType.SelectedItem.Text;
            category = category.Substring(0, 3);
            int Status = 0;
            lblAllocatedAmount.Text = "0.00";
            BindInvoiceDetailsBySupplAndStatus(suppltypeid, supplname, categoryid, Status);
            txtPayeeDetails.Text = ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    #endregion Events

    #region PrivateMethods

    private void PaymentTransactionClear()
    {

        try
        {
            txtDate.Text = "";
            txtSourceRef.Text = "";
            txtAmount.Text = "";
            ddlDivision.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlAccType.SelectedIndex = 0;
            ddlAccountNo.SelectedIndex = 0;
            ddlAutoDepositeAccount.SelectedIndex = 0;
            txtAgeing.Text = "";
            txtPayeeDetails.Text = "";
            txtDetails.Text = "";
            lblAllocatedAmount.Text = "";
            lblReceiptOpenAmount.Text = "";
            //  lblPrvClientOpenAmount.Text = "";
            lblTotalAvailable.Text = "";
            lblOpenItemAmounFromclient.Text = "";
            Response.Redirect("LatestPaymentTransaction.aspx");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }

    private void BindDivision()
    {
        try
        {
            ddlDivision.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetDivisions();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataSource = ObjDsClients;
                ddlDivision.DataValueField = "DivisionId";
                ddlDivision.DataTextField = "DivName";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("Select Division", "0"));
            }
            else
            {
                ddlDivision.Items.Insert(0, new ListItem("Select Division", "0"));
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
            ddlAutoDepositeAccount.Items.Clear();
            int BankId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetBankAccounts(BankId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAutoDepositeAccount.DataSource = ObjDsClients;
                ddlAutoDepositeAccount.DataValueField = "BankAcId";
                ddlAutoDepositeAccount.DataTextField = "BankName";
                ddlAutoDepositeAccount.DataBind();
                ddlAutoDepositeAccount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
            else
            {
                ddlAutoDepositeAccount.Items.Insert(0, new ListItem("Select Account", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindPaymentTypes()
    {
        try
        {
            ddlPaymentType.Items.Clear();
            int pymentId = 0;
            DataSet ObjDsClients = _objBOUtiltiy.GetPaymentTypes(pymentId);
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlPaymentType.DataSource = ObjDsClients;
                ddlPaymentType.DataValueField = "PaymentId";
                ddlPaymentType.DataTextField = "PaymentName";
                ddlPaymentType.DataBind();
                ddlPaymentType.Items.Insert(0, new ListItem("Select PaymentType", "0"));
            }
            else
            {
                ddlPaymentType.Items.Insert(0, new ListItem("Select PaymentType", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    private void BindInvoiceDetailsBySupplAndStatus(int suppltypeid, string suppltypename, int categoryId, int Status)
    {
        try
        {
          
            decimal CurrentReceiptAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            if (suppltypeid != 0 && categoryId != 0)
            {
                DataSet objDsInvLst = _objBALInvoice.GetInvocieDetailsBySupplierLevel(suppltypeid, suppltypename, Status);
                if (objDsInvLst.Tables[0].Rows.Count > 0)
                {
                    gvCustomers.DataSource = objDsInvLst.Tables[0];                 
                    gvCustomers.DataBind();

                    lblSuppOpenAmt.Text = objDsInvLst.Tables[0].Rows[0]["SupplOpeningAmount"].ToString();
                }
                else
                {
                    gvCustomers.DataSource = null;
                    gvCustomers.DataBind();
                    lblSuppOpenAmt.Text = "";
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Invoice records not found for this client.");
                }
                //if (objDsInvLst.Tables[1].Rows.Count > 0)
                //{
                //    lblPrvClientOpenAmount.Text = objDsInvLst.Tables[1].Rows[0]["OpenAmount"].ToString();
                //    ClientTotalAvailable = CurrentReceiptAmount + Convert.ToDecimal(lblPrvClientOpenAmount.Text);
                //}
                //else
                //{
                //    ClientTotalAvailable = CurrentReceiptAmount;
                //}
                //lblTotalAvailable.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());
                //lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(ClientTotalAvailable.ToString());

            }
            else
            {
                gvCustomers.DataSource = null;
                gvCustomers.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindAutoDepositeAccount(int ClientType,string categoryName)
    {
        try
        {
            if (ClientType == 0)
            {
                ddlAccountNo.Items.Clear();
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account", "0"));
                return;
            }
            ddlAccountNo.Items.Clear();
            BAClients objBAClients = new BAClients();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccNoofClientandSuppl(ClientType, categoryName);
            if (ObjDsClients.Tables.Count > 0)
            {
                if (ObjDsClients.Tables[0].Rows.Count > 0)
                {
                    ddlAccountNo.DataSource = ObjDsClients;
                    ddlAccountNo.DataValueField = "id";
                    ddlAccountNo.DataTextField = "accountcode";
                    lblMainAcc.Text = ObjDsClients.Tables[0].Rows[0]["MainAcc"].ToString();
                    ddlAccountNo.DataBind();
                    ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));
                }
            }
            else
            {
                ddlAccountNo.Items.Insert(0, new ListItem("Select Account Code", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void BindClienttypes()
    {
        try
        {
            ddlAccType.Items.Clear();
            DataSet ObjDsClients = _objBOUtiltiy.GetAccountTypeOfSuppl();
            if (ObjDsClients.Tables[0].Rows.Count > 0)
            {
                ddlAccType.DataSource = ObjDsClients;
                ddlAccType.DataValueField = "CategoryId";
                ddlAccType.DataTextField = "CategoryName";
                ddlAccType.DataBind();
                ddlAccType.Items.Insert(0, new ListItem("Select  Type", "0"));
            }
            else
            {
                ddlAccType.Items.Insert(0, new ListItem("Select  Type", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkSelect.Checked == true)
    //    {
    //        if (txtAmount.Text != "")
    //        {
    //            Allocateallamount();

    //        }
    //    }

    //}

    //private void Allocateallamount()
    //{
    //    try
    //    {
    //        decimal ReceiptTotalAmount = lblTotalAvailable.Text != "" ? Convert.ToDecimal(lblTotalAvailable.Text) : 0;//txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
    //        int Allocatedcount = 0;
    //        decimal AllocatedTotalAmount = 0;
    //        decimal OpenAmount = 0;
    //        decimal allocateamt = 0;

    //        decimal ClientOpenAmount = 0;
    //        decimal InvoiceOpenAmount = 0;
    //        decimal ThisEntryAmount = 0;

    //        allocateamt = Convert.ToDecimal(txtAmount.Text);


    //        foreach (GridViewRow row in gvCustomers.Rows)
    //        {
    //            if (row.RowType == DataControlRowType.DataRow)
    //            {

    //                TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
    //                HiddenField hfAllocatedAmount = row.FindControl("hfAllocatedAmount") as HiddenField;

    //                //if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
    //                //{
    //                //    txtThisEntry.Text = "0";

    //                //}

    //                InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

    //                if (allocateamt >= InvoiceOpenAmount || allocateamt < InvoiceOpenAmount)
    //                {
    //                    if (allocateamt >= InvoiceOpenAmount)
    //                    {
    //                        txtThisEntry.Text = InvoiceOpenAmount.ToString();
    //                    }
    //                    else
    //                    {
    //                        txtThisEntry.Text = allocateamt.ToString();
    //                    }

    //                    if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
    //                    {

    //                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
    //                        // InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

    //                        OpenAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
    //                        OpenAmount = OpenAmount + Convert.ToDecimal(hfAllocatedAmount.Value);

    //                        if (ThisEntryAmount > InvoiceOpenAmount)
    //                        {
    //                            txtThisEntry.Text = "";

    //                        }
    //                        //else if (ThisEntryAmount > OpenAmount)
    //                        //{
    //                        //    txtThisEntry.Text = "";
    //                        //}
    //                        else
    //                        {
    //                            hfAllocatedAmount.Value = txtThisEntry.Text;

    //                            ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
    //                            Allocatedcount = Allocatedcount + 1;
    //                            AllocatedTotalAmount = AllocatedTotalAmount + ThisEntryAmount;

    //                            row.Cells[7].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());

    //                        }


    //                    }
    //                    allocateamt = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);
    //                    //  txtAmount.Text = allocateamt.ToString();

    //                }
    //            }
    //        }
    //        OpenAmount = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);

    //        lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
    //        lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());
    //        OpenAmount = Math.Abs(OpenAmount);
    //        lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());

    //        lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
    //        // lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
    //        ExceptionLogging.SendExcepToDB(ex);
    //    }
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTransactionList.aspx");
    }
    private void BindOrders(string SupplierId)
    {
        try
        {

            string supplname = ddlAccType.SelectedItem.Text;
            int categoryId = Convert.ToInt32(ddlAccType.SelectedValue);
            int status = 0;
            DataSet ds = _objBALInvoice.GetInvoiceDetailsBySUpplAndStatus(Convert.ToInt32(SupplierId), supplname, categoryId, status);
            if (ds.Tables[0].Rows.Count > 0)
            {

                gvClosedInvoice.DataSource = ds.Tables[0];
                gvClosedInvoice.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {

                gvInvoiceOpen.DataSource = ds.Tables[1];
                gvInvoiceOpen.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }

    }
    protected void ViewInvoiceClosed(object sender, EventArgs e)
    {
        //Grab the selected row
        GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
        string SupplierId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
        BindOrders(SupplierId);
        InvoiceclosePopExtender.Show();
    }
    protected void ViewInvocieOpen(object sender, EventArgs e)
    {
        //Grab the selected row
        GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
        string SupplierId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
        BindOrders(SupplierId);
        InvocieOpenPopExtender.Show();
    }


    #endregion PrivateMethods

    #region GridEvents
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            decimal ReceiptTotalAmount = txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
            int Allocatedcount = 0;
            decimal AllocatedTotalAmount = 0;
            decimal OpenAmount = 0;
            decimal ClientOpenAmount = 0;

            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Allocatedcount = Allocatedcount + 1;
                        AllocatedTotalAmount = AllocatedTotalAmount + Convert.ToDecimal(row.Cells[3].Text);


                    }
                    else
                    {
                        ClientOpenAmount = ClientOpenAmount + Convert.ToDecimal(row.Cells[3].Text);
                    }
                }
            }

            OpenAmount = ReceiptTotalAmount - AllocatedTotalAmount;

            lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
            lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());
            lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());
            lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
            lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //protected void txtThisEntry_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        decimal ReceiptTotalAmount = lblTotalAvailable.Text != "" ? Convert.ToDecimal(lblTotalAvailable.Text) : 0;//txtAmount.Text != "" ? Convert.ToDecimal(txtAmount.Text) : 0;
    //        int Allocatedcount = 0;
    //        decimal AllocatedTotalAmount = 0;
    //        decimal OpenAmount = 0;

    //        decimal ClientOpenAmount = 0;
    //        decimal InvoiceOpenAmount = 0;
    //        decimal ThisEntryAmount = 0;
    //        foreach (GridViewRow row in gvCustomers.Rows)
    //        {
    //            if (row.RowType == DataControlRowType.DataRow)
    //            {

    //                TextBox txtThisEntry = row.FindControl("txtThisEntry") as TextBox;
    //                HiddenField hfAllocatedAmount = row.FindControl("hfAllocatedAmount") as HiddenField;

    //                if (!_objBOUtiltiy.TryParseCheckValue(txtThisEntry.Text, "Decimal"))
    //                {
    //                    txtThisEntry.Text = "0";

    //                }

    //                if (txtThisEntry.Text != "" && txtThisEntry.Text != "0" && txtThisEntry.Text != "0.00")
    //                {

    //                    ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
    //                    InvoiceOpenAmount = Convert.ToDecimal(row.Cells[4].Text);

    //                    OpenAmount = Convert.ToDecimal(lblReceiptOpenAmount.Text);
    //                    OpenAmount = OpenAmount + Convert.ToDecimal(hfAllocatedAmount.Value);

    //                    if (ThisEntryAmount > InvoiceOpenAmount)
    //                    {
    //                        txtThisEntry.Text = "";

    //                    }
    //                    else if (ThisEntryAmount > OpenAmount)
    //                    {
    //                        txtThisEntry.Text = "";
    //                    }
    //                    else
    //                    {
    //                        hfAllocatedAmount.Value = txtThisEntry.Text;

    //                        ThisEntryAmount = Convert.ToDecimal(txtThisEntry.Text);
    //                        Allocatedcount = Allocatedcount + 1;
    //                        AllocatedTotalAmount = AllocatedTotalAmount + ThisEntryAmount;

    //                        row.Cells[7].Text = _objBOUtiltiy.FormatTwoDecimal((InvoiceOpenAmount - ThisEntryAmount).ToString());

    //                    }


    //                }

    //            }
    //        }

    //        OpenAmount = Convert.ToDecimal(ReceiptTotalAmount) - Convert.ToDecimal(AllocatedTotalAmount);

    //        lblAllocated.Text = "Allocated(" + Allocatedcount + ")";
    //        lblAllocatedAmount.Text = _objBOUtiltiy.FormatTwoDecimal(AllocatedTotalAmount.ToString());

    //        lblReceiptOpenAmount.Text = _objBOUtiltiy.FormatTwoDecimal(OpenAmount.ToString());

    //        lblOpenItemAmounFromclient.Text = _objBOUtiltiy.FormatTwoDecimal(ClientOpenAmount.ToString());
    //        // lblOpenItemsFromclient.Text = "Open Items for " + ddlAccountNo.SelectedItem.Text;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "error", ex.Message);
    //        ExceptionLogging.SendExcepToDB(ex);
    //    }
    //}

    #endregion GridEvents


    protected void txtSourceRef_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAutoDepositeAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}