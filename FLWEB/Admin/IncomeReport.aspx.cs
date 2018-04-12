using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_IncomeReport : System.Web.UI.Page
{


    BAReport objBaReport = new BAReport();
    BOUtiltiy _BOUtilities = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        IncomeReport();
       // IncomeReportBind();
    }
    protected void btnReportSubmit_Click(object sender, EventArgs e)
    {
       // IncomeReportBind();
    }

  private void IncomeReport()
    {
        try
        {
            StringBuilder htmlTable = new StringBuilder();
            int Income = 0; int Expenses = 0;
            decimal TotalIncome = 0;
            decimal TotalExpenses = 0;
            decimal NetIncome = 0;
            decimal IncomeTax = 0;
            decimal IncAfterTax = 0;

            decimal endofperiod = 0;
            decimal getTotalincome = 0;
            decimal getTotalExpe = 0;

            int comapnyId = 0;

            if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
            {
                comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
            }



            DataSet objds = objBaReport.GetIncomeLevelData(comapnyId);



            htmlTable.Append("<table style='height:50%;width:100%'>");

            if (objds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[1].Rows)
                {
                    if (getTotalincome == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[1].Rows)
                        {

                            TotalIncome = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalIncome;

                        }

                        getTotalincome = 1;
                    }

                    if (Income == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalIncome.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }


                    if (TotalIncome != 0)
                    {
                        /*Put a for loop here and repeat the below code*/

                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:0px;width:80%'>" + dtlRow["MainAccount"] + "</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;'>" +_BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                        Income = 1;


                    }
                }
            }
            else
            {
                htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income</td>");
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalIncome.ToString()) + "</td>");
                htmlTable.Append("</tr>");
            }
            if (objds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[2].Rows)
                {
                    if (getTotalExpe == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[2].Rows)
                        {

                            TotalExpenses = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalExpenses;
                        }
                        getTotalExpe = 1;

                    }



                    if (Expenses == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Expenses</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalExpenses.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                    Expenses = 1;
                }
            }
            else
            {
                htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Expenses</td>");
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalExpenses.ToString()) + "</td>");
                htmlTable.Append("</tr>");
            }

            int getTotalSusepenseamount = 0;
            decimal TotalSusAmount = 0;

            if (objds.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[4].Rows)
                {
                    if (getTotalSusepenseamount == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[4].Rows)
                        {

                            TotalSusAmount = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalSusAmount;
                        }
                        getTotalSusepenseamount = 1;

                    }



                    //if (Expenses == 0)
                    //{
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Others</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalSusAmount.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                   // }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                  //  Expenses = 1;
                }
            }
            


            NetIncome = TotalIncome - TotalExpenses;


            bool NetIncpositive = NetIncome >= 0;
            bool NetInnegative = NetIncome <= 0;

            IncAfterTax = NetIncome - IncomeTax;

            bool positive = IncAfterTax >= 0;
            bool negative = IncAfterTax <= 0;




            IncAfterTax = Math.Abs(IncAfterTax);



            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Net income before tax for the period</td>");
            if (NetIncpositive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(NetIncome.ToString()) + "</td>");
            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(Math.Abs(NetIncome).ToString()) + ")</td>");

            }
            htmlTable.Append("</tr>");

            //htmlTable.Append("<tr>");
            //htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income tax</td>");
            //htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormateNumberWithComma(IncomeTax) + "</td>");
            //htmlTable.Append("</tr>");

            int incomedata = 0;
            decimal TotalIncomeTaxes = 0;
            int incomes = 0;

            if (objds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[3].Rows)
                {
                    if (incomedata == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[3].Rows)
                        {

                            TotalIncomeTaxes = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalIncomeTaxes;
                        }
                        incomedata = 1;

                    }



                    if (incomes == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income tax</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalIncomeTaxes.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                    incomes = 1;
                }
            }

            decimal Nettincomeloss = NetIncome - TotalIncomeTaxes;

            bool Nettincomelosspostive = Nettincomeloss >= 0;
            bool Nettincomelossnegative = Nettincomeloss <= 0;


            decimal Nettincomelossafter = Math.Abs(Nettincomeloss);

            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Nett income (loss) after tax</td>");

            if (positive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(Nettincomelossafter.ToString()) + "</td>");

            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(Nettincomelossafter.ToString()) + ") </td>");
            }
            htmlTable.Append("</tr>");

            endofperiod = Nettincomeloss;

            bool endofperiodpostive = endofperiod >= 0;
            bool endofperiodnegative = endofperiod <= 0;

            endofperiod = Math.Abs(endofperiod);

            //htmlTable.Append("<tr>");
            //htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Retained income at begin of period</td>");
            //htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormateNumberWithComma(beginperiod) + "</td>");
            //htmlTable.Append("</tr>");



            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Retained income at end of period</td>");

            if (endofperiodpostive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(endofperiod.ToString()) + "</td>");
            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(endofperiod.ToString()) + ")</td>");
            }
            htmlTable.Append("</tr>");



            // htmlTable.AppendLine("</table>");



            htmlTable.AppendLine("</table>");
            ltrlctrl1.Text = htmlTable.ToString();
        }
        catch (Exception ex)
        {
             ExceptionLogging.SendExcepToDB(ex);
        }
    }

    //private void IncomeReportBind()
    //{
    //    try
    //    {
    //      //  DateTime? FromDate = (txtFromDate.Text != null) ? Convert.ToDateTime(txtFromDate.Text) : (DateTime?)null;
    //     //   DateTime? ToDate = (txtToDate.Text != null) ? Convert.ToDateTime(txtToDate.Text) : (DateTime?)null;
    //        DataSet ds = objBaReport.GetIncomeLevelData();
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvIncomeReport.DataSource = ds;
    //            gvIncomeReport.DataBind();
    //        }

    //        else
    //        {
    //            gvIncomeReport.DataSource = null;
    //            gvIncomeReport.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = _BOUtilities.ShowMessage("danger", "Danger", ex.Message);
    //    }

    //}

    protected void imgPdf_Click(object sender, ImageClickEventArgs e)
    {
        //string url = "IncomeReport.aspx" ;
        //string fullURL = "window.open('" + url + "', '_blank');";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "_blank", fullURL, true);

        IncomeReportPdf();
    }

    private void IncomeReportPdf()
    {

        try
        {
            StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/IncomeReport.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();

            StringBuilder htmlTable = new StringBuilder();
            int Income = 0;         
            int ComapanyAddress = 0;

            decimal TotalIncome = 0;
            decimal TotalExpenses = 0;
            decimal NetIncome = 0;
            decimal IncomeTax = 0;
            decimal IncAfterTax = 0;

            decimal endofperiod = 0;
            decimal getTotalincome = 0;
            decimal getTotalExpe = 0;


            int comapnyId = 0;


            if (!string.IsNullOrEmpty(Session["UserCompanyId"].ToString()))
            {
                comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
            }


            int Expenses = 0;
            DataSet objds = objBaReport.GetIncomeLevelData(comapnyId);

            if (objds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[0].Rows)
                {
                    if (ComapanyAddress == 0)
                    {

                        readFile = readFile.Replace("{CompanyName}", dtlRow["CompanyName"].ToString());
                    }
                    ComapanyAddress = 1;
                }
            }

            //htmlTable.Append("<table style='height:50%;width:100%'>");
            if (objds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[1].Rows)
                {
                    if (getTotalincome == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[1].Rows)
                        {
                          //  string amount = dtlRrow["Amount"].ToString();

                            TotalIncome = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalIncome;

                        }

                        getTotalincome = 1;
                    }

                    if (Income == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalIncome.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }


                    if (TotalIncome != 0)
                    {
                        /*Put a for loop here and repeat the below code*/

                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:0px;width:80%'>" + dtlRow["MainAccount"] + "</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                        Income = 1;


                    }
                }
            }
            if (objds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[2].Rows)
                {
                    if (getTotalExpe == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[2].Rows)
                        {

                            TotalExpenses = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalExpenses;
                        }
                        getTotalExpe = 1;

                    }



                    if (Expenses == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Expenses</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalExpenses.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                    Expenses = 1;
                }
            }
            else
            {
                htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Expenses</td>");
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalExpenses.ToString()) + "</td>");
                htmlTable.Append("</tr>");
            }

            int getTotalSusepenseamount = 0;
            decimal TotalSusAmount = 0;

            if (objds.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[4].Rows)
                {
                    if (getTotalSusepenseamount == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[4].Rows)
                        {

                            TotalSusAmount = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalSusAmount;
                        }
                        getTotalSusepenseamount = 1;

                    }



                    //if (Expenses == 0)
                    //{
                    htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                    htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Others</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalSusAmount.ToString()) + "</td>");
                    htmlTable.Append("</tr>");
                    // }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                    //  Expenses = 1;
                }
            }
            


            NetIncome = TotalIncome - TotalExpenses;


            bool NetIncpositive = NetIncome >= 0;
            bool NetInnegative = NetIncome <= 0;

            IncAfterTax = NetIncome - IncomeTax;

            bool positive = IncAfterTax >= 0;
            bool negative = IncAfterTax <= 0;




            IncAfterTax = Math.Abs(IncAfterTax);



            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Net income before tax for the period</td>");
            if (NetIncpositive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(NetIncome.ToString()) + "</td>");
            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(Math.Abs(NetIncome).ToString()) + ")</td>");

            }
            htmlTable.Append("</tr>");

            //htmlTable.Append("<tr>");
            //htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income tax</td>");
            //htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormateNumberWithComma(IncomeTax) + "</td>");
            //htmlTable.Append("</tr>");

            int incomedata = 0;
            decimal TotalIncomeTaxes = 0;
            int incomes = 0;

            if (objds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow dtlRow in objds.Tables[3].Rows)
                {
                    if (incomedata == 0)
                    {
                        foreach (DataRow dtlRrow in objds.Tables[3].Rows)
                        {

                            TotalIncomeTaxes = Convert.ToDecimal(dtlRrow["Amount"].ToString()) + TotalIncomeTaxes;
                        }
                        incomedata = 1;

                    }



                    if (incomes == 0)
                    {
                        htmlTable.Append("<tr style='background-color:#f5f5f5;'>");
                        htmlTable.Append("<td colspan='6' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Income tax</td>");
                        htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(TotalIncomeTaxes.ToString()) + "</td>");
                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("<tr >");
                    htmlTable.Append("<td colspan='6' style='border: 1px ridge black; font-weight:bold;padding:1px;width:80%'>" + dtlRow["MainAcName"] + "</td>");
                    htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormatTwoDecimal(dtlRow["Amount"].ToString()) + "</td>");



                    incomes = 1;
                }
            }

            decimal Nettincomeloss = NetIncome - TotalIncomeTaxes;

            bool Nettincomelosspostive = Nettincomeloss >= 0;
            bool Nettincomelossnegative = Nettincomeloss <= 0;


            decimal Nettincomelossafter = Math.Abs(Nettincomeloss);

            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Nett income (loss) after tax</td>");

            if (positive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(Nettincomelossafter.ToString()) + "</td>");

            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(Nettincomelossafter.ToString()) + ") </td>");
            }
            htmlTable.Append("</tr>");

            endofperiod = Nettincomeloss;

            bool endofperiodpostive = endofperiod >= 0;
            bool endofperiodnegative = endofperiod <= 0;

            endofperiod = Math.Abs(endofperiod);

            //htmlTable.Append("<tr>");
            //htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Retained income at begin of period</td>");
            //htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>" + _BOUtilities.FormateNumberWithComma(beginperiod) + "</td>");
            //htmlTable.Append("</tr>");



            htmlTable.Append("<tr>");
            htmlTable.Append("<td colspan='6' style='border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Retained income at end of period</td>");

            if (endofperiodpostive == true)
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right;color:blue;'>" + _BOUtilities.FormatTwoDecimal(endofperiod.ToString()) + "</td>");
            }
            else
            {
                htmlTable.Append("<td style='border: 1px ridge black; font-weight:bold;padding:1px;text-align:right'>(" + _BOUtilities.FormatTwoDecimal(endofperiod.ToString()) + ")</td>");
            }
            htmlTable.Append("</tr>");



            // htmlTable.AppendLine("</table>");



            htmlTable.AppendLine("</table>");
            htmlTable.Append("</tr>");



            // htmlTable.AppendLine("</table>");

            readFile = readFile.Replace("{MainRows}", htmlTable.ToString());
            string StrContent = readFile;

            GenerateHTML_TO_PDF(StrContent, true, "", false);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }


    private void GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string FileName, bool SaveFileDir)
    {
        try
        {
            string pdf_page_size = "A4";
            SelectPdf.PdfPageSize pageSize = (SelectPdf.PdfPageSize)Enum.Parse(typeof(SelectPdf.PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            SelectPdf.PdfPageOrientation pdfOrientation =
                (SelectPdf.PdfPageOrientation)Enum.Parse(typeof(SelectPdf.PdfPageOrientation),
                pdf_orientation, true);


            int webPageWidth = 1024;


            int webPageHeight = 0;




            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

            // save pdf document      

            if (!SaveFileDir)
                doc.Save(Response, ResponseShow, FileName);
            else
                doc.Save(FileName);

            doc.Close();

        }
        catch (Exception ex)
        {
          ExceptionLogging.SendExcepToDB(ex);
        }
    }



}
