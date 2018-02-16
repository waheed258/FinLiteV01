using EntityManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
   public class DACommission : DataUtilities
    {
       public int InsertUpdateCommission(EMCommission objCommission)
       {
           Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@CommiId",objCommission.CommiId},
                                         {"@CommiAmount",objCommission.CommiAmount},
                                         {"@TicketType",objCommission.TicketType}, 
                                         {"@Invid",objCommission.Invid},
                                         {"@InvDocumentNo",objCommission.@InvDocumentNo},
                                         {"@ChartedAccCode",objCommission.@ChartedAccCode},
                                         {"@Status",objCommission.@Status},              
                                         {"@CreatedBy",objCommission.CreatedBy},
                                          {"@TolCommiAccount",objCommission.TolCommiAccount},
                                      
                                         
                                     };
           return ExecuteNonQuery("Commission_InsertUpdate", htParams);
       }

       public DataSet GetCommiChartedAccId(string chartedaccname)
       {
           Hashtable htParams = new Hashtable
                                     {
                                       
                                         {"@chartedAccName",chartedaccname},                                     
                                         
                                     };
           return ExecuteDataSet("Commission_ChartedAccount", htParams);
       }
    }
}
