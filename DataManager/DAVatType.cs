using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;
namespace DataManager
{
    public class DAVatType:DataUtilities
    {
        public int InsUpdVatType(EMVatType objEMVatType)
        {
            Hashtable htparams = new Hashtable
             {
                {"@VatId",objEMVatType.VatId},
                {"@VatKey",objEMVatType.VatKey},
                {"@VatDescription",objEMVatType.VatDesc},
                {"@VatRate",objEMVatType.VatRate},
                {"@VatAppTo",objEMVatType.VatAppTo},
                {"@VatEffectiveDate",objEMVatType.VatEffDate},
                {"@VatGIcode",objEMVatType.VatGICode},
                {"@CreatedBy",objEMVatType.CreatedBy}
             };
            int IsSuccess = ExecuteNonQuery("Vat_InsertUpdate", htparams);
           return IsSuccess;
       }
       public DataSet GetVatType(int VatId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@VatId",VatId}
           };

           return ExecuteDataSet("Vat_GetData", htparams);

       }
       public int DeleteVatType(int VatId)
       {
           Hashtable htparams = new Hashtable
           {
             {"@VatId",VatId}
           };

           return ExecuteNonQuery("vat_Delete", htparams);

       }
    }
}

