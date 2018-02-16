using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PaymentsList : System.Web.UI.Page
{
    BALTransactions objBalTransaction = new BALTransactions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PaymentGridBind();
        }

    }
    private void PaymentGridBind()
    {
        try
        {


            DataSet ds = objBalTransaction.PaymentList();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvPaymentList.DataSource = ds;
                gvPaymentList.DataBind();
            }
            else
            {
                gvPaymentList.DataSource = null;
                gvPaymentList.DataBind();
            }



        }
        catch(Exception ex)
        {
            lblMsg.Text = "";
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
 
    protected void btnPaymentAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTransaction.aspx");
    }
}