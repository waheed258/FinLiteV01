using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DAReport : DataUtilities
    {
      public DataSet GetInvoiceLevelData(DateTime? FromDate, DateTime? ToDate)
        {
            Hashtable htparams = new Hashtable
           {
             {"@FromDate",FromDate},
             {"@ToDate",ToDate}
           };

            return ExecuteDataSet("invoiceLevel_Report", htparams);

        }

      public DataSet GetClientLevelData(DateTime? FromDate, DateTime? ToDate)
      {
          Hashtable htparams = new Hashtable
           {
             {"@FromDate",FromDate},
             {"@ToDate",ToDate}
           };

          return ExecuteDataSet("Report_ClientLevel", htparams);

      }

      public DataSet GetIncomeLevelData(int comapnyId)
      {

          Hashtable htparams = new Hashtable
           {
             {"@UsercomapnyId",comapnyId},
            
           };
          return ExecuteDataSet("Report_Income",htparams);

      }

      public DataSet GetSupplierLevelData(DateTime? FromDate, DateTime? ToDate ,string CreatedBy)
      {
          Hashtable htparams = new Hashtable
           {
             {"@FromDate",FromDate},
             {"@ToDate",ToDate},
             {"@CreatedBy",CreatedBy},
           };

          return ExecuteDataSet("Report_SupplierLevel", htparams);

      }

      public DataSet GetSupplierLevelData(DateTime? FromDate, DateTime? ToDate)
      {
          Hashtable htparams = new Hashtable
           {
             {"@FromDate",FromDate},
             {"@ToDate",ToDate}
           };

          return ExecuteDataSet("SupplierLevel_Report", htparams);

      }

      public DataSet GetDashBoard(string FromDate, string ToDate)
      {
          Hashtable htparams = new Hashtable
           {
             {"@fromdate",FromDate},
             {"@todate",ToDate}
           };

          return ExecuteDataSet("dashboard_report", htparams);
      }


      public DataSet GetTicketsAmount(string FromDate, string ToDate)
      {
          Hashtable htparams = new Hashtable
           {
             {"@fromdate",FromDate},
             {"@todate",ToDate}
           };

          return ExecuteDataSet("Dashboard_TicketAmounts", htparams);
      }
        // Commission Report
      public DataSet GetInvoiceData()
      {
          return ExecuteDataSet("CommissionReport_GetInvoiceData");

      }

      public DataSet GetCommissionData(int InvoiceId)
      {

          Hashtable htparams = new Hashtable
           {
             {"@Invid",InvoiceId},
            
           };
          return ExecuteDataSet("CommissionReport_GetCommissionData", htparams);

      }

      public DataSet GetCompanyDetails(int companyId)
      {

          Hashtable htparams = new Hashtable
           {
             {"@CompanyId",companyId},
            
           };
          return ExecuteDataSet("Company_Details", htparams);

      }


    }
}
