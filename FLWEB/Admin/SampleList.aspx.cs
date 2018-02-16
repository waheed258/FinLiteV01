using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_SampleList : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindGrid();
        }

    }
    

   

    #region PrivateMethods
    // Write methods here 

    
    private void BindGrid()
    {
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("Heading1", typeof(int));
            table.Columns.Add("Heading2", typeof(string));
            table.Columns.Add("Heading3", typeof(string));


            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David");
            table.Rows.Add(50, "Enebrel", "Sam");
            table.Rows.Add(10, "Hydralazine", "Christoff");
            table.Rows.Add(21, "Combivent", "Janet");
            table.Rows.Add(100, "Dilantin", "Melanie");


            if (table.Rows.Count > 0)
            {
                gvSample.DataSource = table;
                gvSample.DataBind();
                _objBOUtiltiy.ShowMessage("sucess", "Success", "Find Data");
            }
            else
            {
                gvSample.DataSource = null;
                gvSample.DataBind();
                _objBOUtiltiy.ShowMessage("info", "Info", "Data not found");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }


    }
    #endregion PrivateMethods
    #region Events
    // put all events here

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    #endregion Events
}