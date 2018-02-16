using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Errors : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindErrorsDetails();
        }
    }

    private void BindErrorsDetails()
    {
        try
        {
            //   gvCommTypeList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet ds = _objBOUtiltiy.GetErrorList();
            // Session["dt"] = ds.Tables[0];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVErrors.DataSource = ds;
                GVErrors.DataBind();
            }
            else
            {
                GVErrors.DataSource = null;
                GVErrors.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
}