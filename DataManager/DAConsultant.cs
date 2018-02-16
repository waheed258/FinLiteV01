using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DAConsultant:DataUtilities
    {
        public int InsUpdConsultant(EMConsultant objConsultant)
        {
            Hashtable htparams=new Hashtable
                                            {
                                                {"@ConsultantId",objConsultant.ConsultantId},
                                                {"@KeyId",objConsultant.KeyId},
                                                {"@Name",objConsultant.Name},
                                                {"@GroupId",objConsultant.GroupId},
                                                {"@Division",objConsultant.Division},
                                                {"@Email",objConsultant.Email},
                                                {"@TelephoneNo",objConsultant.TelephoneNo},
                                                {"@CellNo",objConsultant.CellNo},
                                                {"@FaxNo",objConsultant.FaxNo},
                                                {"@ClientType",objConsultant.ClientType},
                                                {"@ClientStutus",objConsultant.ClientStutus},
                                                {"@CreatedBy",objConsultant.CreatedBy},
                                                //{"@AccCode",objConsultant.AccCode}
                                          };

            int IsSuccess = ExecuteNonQuery("Consultants_InsertUpdate", htparams, "@return");
             return IsSuccess;
        }

        public DataSet Get_ConsultantAccCode()
        {

            return ExecuteDataSet("Get_ConsultantAccCode");
        }
        public DataSet GetConsultant(int ConsultantId)
        {
            Hashtable htparams = new Hashtable
                                              {
                                                 {"@ConsultantId",ConsultantId},
                                              };
            return ExecuteDataSet("Consultant_Get", htparams);
        }

        public int DeleteConsultant(int ConsultantId)
        {
            Hashtable htparams = new Hashtable
                                              {
                                                 {"@ConsultantId",ConsultantId},
                                              };
            return ExecuteNonQuery("Consultant_Delete", htparams);
        }

        //CharteredAccounts
        public int InsUpdChartAccounts(EMConsultant objConsultant)
        {
            Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccId",objConsultant.ChartedAccId},
                                         {"@ChartedAccName",objConsultant.ChartedAccName},
                                         {"@ChartedMasterAccName",objConsultant.ChartedMasterAccName},
                                         {"@Type",objConsultant.Type},
                                         {"@AccCode",objConsultant.AccCode},
                                         {"@CompanyId",objConsultant.CompanyId},
                                         {"@BranchId",objConsultant.BranchId},
                                         {"@CreatedBy",objConsultant.CreatedBy},
                                         {"@DepartmentId",objConsultant.DepartmentId},
                                         {"@BaseCurrency",objConsultant.BaseCurrency},
                                         {"@TranCurrency",objConsultant.TranCurrency},
                                          {"@CategoryId",objConsultant.CategoryId},
                                          {"@RefType",objConsultant.RefType},
                                          {"@RefId",objConsultant.RefId},
                                    };
            int IsSuccess = ExecuteNonQuery("CharteredAccounts_Insert", htparams);
            return IsSuccess;
        }

        //CharteredAccounts
        public int UpdChartAccounts(EMConsultant objConsultant)
        {
            Hashtable htparams = new Hashtable
                                    {
                                         {"@ChartedAccName",objConsultant.ChartedAccName},
                                          {"@RefType",objConsultant.RefType},
                                          {"@RefId",objConsultant.RefId},
                                    };
            int IsSuccess = ExecuteNonQuery("CharteredAccounts_Update", htparams);
            return IsSuccess;
        }
    }
}
