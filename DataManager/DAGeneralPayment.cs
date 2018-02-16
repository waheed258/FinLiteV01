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
    public class DAGeneralPayment : DataUtilities
    {

        public int InsertGeneralPaymrnt(EMGeneralPayment objGeneralPymt)
        {
            Hashtable htparams = new Hashtable{
                                            {"@GeneralPymtId",objGeneralPymt.GeneralPymtId},
                                            {"@FromAccCodeId",objGeneralPymt.FromAccCodeId},
                                            {"@MainAcCode",objGeneralPymt.FmMainAcCode},
                                            {"@ToMainAcCode",objGeneralPymt.ToMainAcCode},
                                            {"@ToAccCodeId",objGeneralPymt.ToAccCodeId},
                                            {"@PaymentAmount",objGeneralPymt.PaymentAmount},
                                            {"@PaymentDate",objGeneralPymt.PaymentDate},
                                            {"@CreatedBy",objGeneralPymt.CreatedBy},
                                            {"@CategoryId",objGeneralPymt.CategoryId},
            };
            return ExecuteNonQuery("GeneralPayment_Insert", htparams);
        }
        public DataSet Get_Category()
        {
           

            return ExecuteDataSet("GeneralPayment_getCategories");
        }

        public DataSet Get_CategoryAccCode(int categoryid)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@CategoryId",categoryid},
                                            };
            return ExecuteDataSet("GeneralPayment_getCategoryTypeAccCode", htparams);
        }

        public DataSet Get_MainAccCode(int ChartedAccId)
        {
            Hashtable htparams = new Hashtable
                                            {
                                                {"@ChartedAcc",ChartedAccId},
                                            };
            return ExecuteDataSet("GeneralPayment_getMainAccCode", htparams);
        }

        public DataSet GeneralPayment_List()
        {

            return ExecuteDataSet("GeneralPayment_getList");
        }
      
    }
}
