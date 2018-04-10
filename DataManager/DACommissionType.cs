using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using System.Data.SqlClient;
namespace DataManager
{
    public class DACommissionType:DataUtilities
    {
        public int InsUpdCommissionType(EMCommissionType objCommType)
        {
            Hashtable htparams = new Hashtable
            {
                {"@ComId",objCommType.Id},
                {"@Comkey",objCommType.key},
                {"@ComDeactivate",objCommType.Deactivate},
                {"@ComDesc",objCommType.Desc},
                {"@ComCategory",objCommType.Category},
                {"@ComLSCategory",objCommType.LSCategory},
                {"@ComDType",objCommType.DType},
                {"@ComDComm",objCommType.DComm},
                {"@ComDRate",objCommType.DRate},
                {"@ComUDesc",objCommType.UDesc},
                {"@ComDVat",objCommType.DVat},
                {"@ComNTFee",objCommType.NTFee},
                {"@ComZUType",objCommType.ZUType},
                {"@ComIncome",objCommType.Income},
                {"@ComOVat",objCommType.OVat},
                {"@CreatedBy",objCommType.CreatedBy}
            };
            int IsSuccess = ExecuteNonQuery("CommissionTypes_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetCommissionType(int ComId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@ComId",ComId}
           };

            return ExecuteDataSet("CommisssionTypes_Get", htparams);

        }
        public int DeleteCommissionType(int ComId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@ComId",ComId}
           };

            return ExecuteNonQuery("CommisssionTypes_Delete", htparams);

        }
        public DataSet GetCategory()
        {

            return ExecuteDataSet("Category_Get");

        }
        public DataSet GetVAT(int vatId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@VatId",vatId}
           };
            return ExecuteDataSet("Vat_GetData",htparams);

        }
        public DataSet GetLandSubCategory()
        {

            return ExecuteDataSet("LandSubCategories_Get");

        }
    }
}
