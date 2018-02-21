using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using EntityManager;
using DataManager;
using BusinessManager;
using System.Data;

public partial class Admin_AirSupplierList : System.Web.UI.Page
{
    EMAirSuppliers objEMAirsupp = new EMAirSuppliers();
    BAAirSuppliers objAirSupplier = new BAAirSuppliers();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindAirSupplierList();
            LoopTextboxes(Page.Controls);
        }

    }
    protected void gvAirSupplierList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string supplierId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Supplier")
        {            
            Response.Redirect("AirSuppliers.aspx?SupplierId=" + supplierId);
        }

        if (e.CommandName == "Delete Supplier")
        {
            DeleteSupplier(Convert.ToInt32(supplierId));
            BindAirSupplierList();
        }
    }

    private void BindAirSupplierList()
    {
        
        try
        {
            gvAirSupplierList.PageSize = int.Parse(ViewState["ps"].ToString());
            int SupplierId = 0;
            DataSet ds = objAirSupplier.GetAirSuppliers(SupplierId);
            Session["dt"] = ds.Tables[0];
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvAirSupplierList.DataSource = ds.Tables[0];

                string sortDirection = "ASC", sortExpression;
                if (ViewState["so"] != null)
                {
                    sortDirection = ViewState["so"].ToString();
                }
                if (ViewState["se"] != null)
                {
                    sortExpression = ViewState["se"].ToString();
                    ds.Tables[0].DefaultView.Sort = sortExpression + " " + sortDirection;
                }

                gvAirSupplierList.DataBind();
            }

            else
            {
                gvAirSupplierList.DataSource = null;
                gvAirSupplierList.DataBind();
            }
        }

        catch(Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void DeleteSupplier(int SupplierId)
    {
        try
        {
            int Result = objAirSupplier.DeleteAirSupplier(SupplierId);

        }
        catch(Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    
    protected void gvAirSupplierList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAirSupplierList.PageIndex = e.NewPageIndex;
      //  BindAirSupplierList();
        SearchItemFromList(txtSearch.Text.Trim());

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AirSuppliers.aspx", false);
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
      //  BindAirSupplierList();
        SearchItemFromList(txtSearch.Text.Trim());
    }

    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchItemFromList(txtSearch.Text.Trim());
    }

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "SupAccountCode='" + SearchText +
                    "' OR SupplierName LIKE '%" + SearchText +
                     "%' OR ComDesc LIKE '%" + SearchText +
                    "%' OR Telephone LIKE '%" + SearchText +
                    "%' OR Email LIKE '%" + SearchText +
                    "%' OR QuickAccount LIKE '%" + SearchText +
                    "%' OR PaymentName LIKE '%" + SearchText + "%'");

                if (dr.Count() > 0)
                {
                    gvAirSupplierList.PageSize = int.Parse(ViewState["ps"].ToString());
                    gvAirSupplierList.DataSource = dr.CopyToDataTable();
                    gvAirSupplierList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void gvAirSupplierList_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["se"] = e.SortExpression;
            if (ViewState["so"] == null)
                ViewState["so"] = "ASC";
            if (ViewState["so"].ToString() == "ASC")
                ViewState["so"] = "DESC";
            else
                ViewState["so"] = "ASC";
            BindAirSupplierList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void LoopTextboxes(ControlCollection controlCollection)
    {

        int langId = Convert.ToInt32(Session["LanguageId"]);
        DataSet ds = _BOUtility.GetLanguageDescription(langId);


        foreach (Control control in controlCollection)
        {
           

            if (control is TextBox)
            {
              
               
                string text = ((TextBox)control).ID;
               string place= (((TextBox)control).FindControl(text) as TextBox).Text;


            
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {
                        
                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            string Latest = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
                        }

                    }
                }
                //((TextBox)control).Text = "بحث";
                
            }
            if (control is DropDownList)
            {

                string text = ((DropDownList)control).ID;
                string place = (((DropDownList)control).FindControl(text) as DropDownList).Text;



                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            string Latest = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
                        }

                    }
                }
                //((TextBox)control).Text = "بحث";

            }

            if (control is Button)
            {
              
                string id = ((Button)control).ID;
                string text = ((Button)control).Text;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {

                        if (dtlRow["Label"].ToString() == text)
                        {
                            string PreviousLabel = dtlRow["Label"].ToString();
                            string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                            ((Button)control).Text = LatestlabelDescrip;
                        }

                    }
                }
            }
            if (control is Label)
            {
              
                string text = ((Label)control).ID;
              
            }
            foreach (GridViewRow row in gvAirSupplierList.Rows)


                if (row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        {
                            string headerRowText = gvAirSupplierList.HeaderRow.Cells[i].Text;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dtlRow in ds.Tables[0].Rows)
                                {

                                    if (dtlRow["Label"].ToString() == headerRowText)
                                    {
                                        string PreviousLabel = dtlRow["Label"].ToString();
                                        string LatestlabelDescrip = dtlRow["LabelDescription"].ToString();

                                        gvAirSupplierList.HeaderRow.Cells[i].Text = LatestlabelDescrip;
                                    }

                                }
                            }
                              
                        }
                    }
                }

            if (control.Controls != null)
            {
                LoopTextboxes(control.Controls);
            }
        }
    }

    //public void FindTheControls()
    //{

    //    foreach (Control c in Page.Controls)
    //    {
    //        string cont = c.GetType().ToString();
    //        if (c.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
    //        {
    //            TextBox tb = (TextBox)c;
    //            if (tb != null)
    //            {
    //                string textid=tb.ID;
    //                Response.Write("Found TextBox");
    //            }
    //        }
    //        if (c.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
    //        {
    //            DropDownList ddl = (DropDownList)c;
    //            if (ddl != null)
    //            {
    //                Response.Write("Found DropDownList");
    //            }
    //        }
    //        if (c.GetType().ToString() == "System.Web.UI.WebControls.Button")
    //        {
    //            Button b = (Button)c;
    //            if (b != null)
    //            {
    //                string textid = b.ID;
    //                Response.Write("Found Button");
    //            }
    //        }
    //        if (c.GetType().ToString() == "System.Web.UI.WebControls.Label")
    //        {
    //            Label l = (Label)c;
    //            if (l != null)
    //            {
    //                string textid = l.ID;
    //                Response.Write("Found Label");
    //            }
    //        }
    //    } 
    //}
    public void GetLanguage()
    {
        
    List<WebControl> wcs = new List<WebControl>();
    GetControlList<WebControl>(Page.Controls, wcs);
    foreach (WebControl childControl in wcs)
    {
        string control = childControl.ToString();
    }

    }

    private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
where T : Control
    {
        foreach (Control control in controlCollection)
        {
            //if (control.GetType() == typeof(T))
            if (control is T) // This is cleaner
                resultCollection.Add((T)control);

            if (control.HasControls())
                GetControlList(control.Controls, resultCollection);
        }
    }

//    private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
//where T : Control
//    {
//        foreach (Control control in controlCollection)
//        {
//            //if (control.GetType() == typeof(T))
//            if (control is T) // This is cleaner
//                resultCollection.Add((T)control);

//            if (control.HasControls())
//                GetControlList(control.Controls, resultCollection);

//            int langId = Convert.ToInt32(Session["LanguageId"]);

//            DataSet ds = _BOUtility.GetLanguageDescription(langId);

//            if (ds.Tables[0].Rows[0]["Label"].ToString() == control.ToString())
//            {
//                string PreviousLabel = ds.Tables[0].Rows[0]["Label"].ToString();
//                string LatestlabelDescrip = ds.Tables[0].Rows[0]["LabelDescription"].ToString();

//                string Latest = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
//            }
//        }

     

     
//    }

//    private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
//where T : Control
//    {
//        foreach (Control control in controlCollection)
//        {
//            //if (control.GetType() == typeof(T))
//            if (control is T) // This is cleaner
//                resultCollection.Add((T)control);

//            if (control.HasControls())
//                GetControlList(control.Controls, resultCollection);
//        }
//        int langId = Convert.ToInt32(Session["LanguageId"]);

//        DataSet ds = _BOUtility.GetLanguageDescription(langId);

//        List<TextBox> allControls = new List<TextBox>();
//        GetControlList<TextBox>(Page.Controls, allControls);
//        foreach (var childControl in allControls)
//        {
//            //     call for all controls of the page
//            if (ds.Tables[0].Rows[0]["Label"].ToString() == childControl.ToString())
//            {
//                string PreviousLabel = ds.Tables[0].Rows[0]["Label"].ToString();
//                string LatestlabelDescrip = ds.Tables[0].Rows[0]["LabelDescription"].ToString();

//                string Latest = PreviousLabel.Replace(PreviousLabel, LatestlabelDescrip);
//            }
//        }
//    }

}