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
    public class DABranch:DataUtilities
    {
        public int InsUpdBranch(EMBranch objbranch)
        {
            Hashtable htparams = new Hashtable
            {
               {"@BranchId",objbranch.BranchId},
               {"@BranchCode",objbranch.BranchCode},
               {"@BranchName",objbranch.BranchName},
               {"@DeActivate",objbranch.DeActivate},
               {"@City",objbranch.city},
               {"@Province",objbranch.Province},
               {"@CreatedBy",objbranch.CreatedBy},
               {"@ContactName",objbranch.ContactName},
               {"@TelephoneNumber",objbranch.TelephoneNumber},
               {"@FaxNumber",objbranch.FaxNumber},
               {"@CellNumber",objbranch.CellNumber},
               {"@EmailAddress",objbranch.EmailAddress},
               {"@WebAddress",objbranch.WebAddress},
               {"@DOCEX",objbranch.DOCEX},
               {"@PostalAddress",objbranch.PostalAddress},
               {"@PhysicalAddress",objbranch.PhysicalAddress},
               {"@Co_No",objbranch.Co_No},
               {"@IATA_No",objbranch.IATA_No},
               {"@Vat_No",objbranch.Vat_No},
               {"@ASATA_Member",objbranch.ASATA_Member},
               {"@Print_Doc",objbranch.Print_Doc}
           };
            int IsSuccess = ExecuteNonQuery("Branch_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet GetBranch(int BranchId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@BranchId",BranchId}
           };

            return ExecuteDataSet("Branch_Get", htparams);

        }
        public int DeleteBranch(int BranchId)
        {
            Hashtable htparams = new Hashtable
           {
             {"@BranchId",BranchId}
           };

            return ExecuteNonQuery("Branch_Delete", htparams);

        }


    }
}
