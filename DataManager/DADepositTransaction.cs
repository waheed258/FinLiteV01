
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;

namespace DataManager
{
    public class DADepositTransaction : DataUtilities
    {

        public DataSet getUnbankReceipts(int RType,int CType)
        {
            Hashtable htparams = new Hashtable{
                                            {"@RcptType",RType},
                                             {"@ClientType",CType},

            };
            return ExecuteDataSet("Receipts_getBy_RTypeOrClientType", htparams);
        }

        public DataSet getDepositList()
        {

            return ExecuteDataSet("DepositTranasctionsMaster_DepositList");
        }

        public int insertDepositMaster(EmDepositMaster objEmDepositMaster)
        {
            Hashtable htparams = new Hashtable{
                                            {"@DepositDate",objEmDepositMaster.DepositDate},
                                             {"@DepositSourceRef",objEmDepositMaster.DepositSourceRef},
                                              {"@DepositRecieptType",objEmDepositMaster.DepositRecieptType},
                                               {"@DepositConsultant",objEmDepositMaster.DepositConsultant},
                                                {"@DepositClientPrefix",objEmDepositMaster.DepositClientPrefix},
                                                 {"@DepositComments",objEmDepositMaster.DepositComments},
                                                  {"@TotalRecieptsDeposited",objEmDepositMaster.TotalRecieptsDeposited},
                                                   {"@TotalDepositAmount",objEmDepositMaster.TotalDepositAmount},
                                                    {"@DepositAcId",objEmDepositMaster.DepositAcId},

                                                  
            };
            return ExecuteNonQuery("DepositMaster_Insert", htparams, "@return");
        }


        public int insertDepositChild(EMDepositChild objEmDepositChild)
        {
            Hashtable htparams = new Hashtable{
                                            {"@RecieptDate",objEmDepositChild.RecieptDate},
                                             {"@ReceiptType",objEmDepositChild.ReceiptType},
                                              {"@ReciptClient",objEmDepositChild.ReciptClient},
                                               {"@ReceiptAmount",objEmDepositChild.ReceiptAmount},
                                                {"@DepositTransMasterId",objEmDepositChild.DepositTransMasterId},
                                                 {"@ReceiptId",objEmDepositChild.ReceiptId},
                                                  {"@InvoiceId",objEmDepositChild.InvoiceId},
                                                    {"@DepositAcId",objEmDepositChild.DepositAcId},
                                               

            };
            return ExecuteNonQuery("DepositChild_Insert", htparams);
        }

    }
}
