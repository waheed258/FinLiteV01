using DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
   public  class BAReport
    {
       private DAReport _objDAReport = new DAReport();
       public DataSet getInvoiceLevelReport(DateTime? fromdate, DateTime? Todate)
        {
            return _objDAReport.GetInvoiceLevelData(fromdate, Todate);

        }
       public DataSet GetDashBoard(string FromDate, string ToDate)
       {
           return _objDAReport.GetDashBoard(FromDate, ToDate);
       }
       public DataSet getClientLevelReport(DateTime? fromdate, DateTime? Todate)
       {
           return _objDAReport.GetClientLevelData(fromdate, Todate);

       }
      // GetIncomeLevelData

       public DataSet GetIncomeLevelData(int comapnyId)
       {
           return _objDAReport.GetIncomeLevelData(comapnyId);

       }
       public DataSet GetSupplierLevelData(DateTime? fromdate, DateTime? Todate ,string CreatedBy)
       {
           return _objDAReport.GetSupplierLevelData(fromdate, Todate, CreatedBy);

       }
       public DataSet getSupplierLevelReport(DateTime? fromdate, DateTime? Todate)
       {
           return _objDAReport.GetSupplierLevelData(fromdate, Todate);

       }
       
       // Commission Report



       public DataSet GetInvoiceData()
       {
           return _objDAReport.GetInvoiceData();

       }
       public DataSet GetCommissionData(int InvoiceId)
       {
           return _objDAReport.GetCommissionData(InvoiceId);

       }



       public DataSet GetCompanyDetails(int CompanyId)
       {
           return _objDAReport.GetCompanyDetails(CompanyId);

       }
    }
}
