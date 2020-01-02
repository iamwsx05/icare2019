using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Text;

namespace Report.Com
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsHISMedTypeManage : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region m_mthGetPayTypeInfoByID
        [AutoComplete]
        public long m_mthGetPayTypeInfoByID(int intFlag, out DataTable dt)
        {
            long rec = 0;
            string Sql = string.Empty;
            dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            if(intFlag == -1)
            {
                Sql = @"SELECT DISTINCT ta.paytypeid_chr, ta.paytypename_vchr
                          FROM t_bse_patientpaytype ta
                         ORDER BY ta.paytypeid_chr";
            }
            else
            {
                Sql = @"select distinct ta.PAYTYPEID_CHR, ta.PAYTYPENAME_VCHR
                          from T_BSE_PATIENTPAYTYPE TA
                         Where TA.INTERNALFLAG_INT = {0}
                         order by ta.PAYTYPEID_CHR";
                Sql = string.Format(Sql, intFlag);
            }
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            return rec;
        }
        #endregion

        #region m_mthGetBalanceEmpInfo
        [AutoComplete]
        public long m_mthGetBalanceEmpInfo(out DataTable dt)
        {
            long rec = 0;
            string Sql = string.Empty;
            dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            Sql = @"SELECT DISTINCT te.lastname_vchr, te.empid_chr
                      FROM t_bse_employee te, t_opr_outpatientrecipeinv tp
                     WHERE te.empid_chr = tp.balanceemp_chr
                       AND tp.balanceflag_int = 1";
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            return rec;
        }
        #endregion

        #region m_mthGetStatDeptInfo
        [AutoComplete]
        public long m_mthGetStatDeptInfo(out DataTable dt)
        {
            long rec = 0;
            string Sql = string.Empty;
            dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            Sql = @"select a.deptid_chr,a.deptname_vchr from t_bse_deptdesc a where a.inpatientoroutpatient_int=0";
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            return rec; 
        }
        #endregion

        #region m_mthGetPatientPayTypeInfo
        [AutoComplete]
        public long m_mthGetPatientPayTypeInfo(out DataTable dt)
        {
            long rec = 0;
            string Sql = string.Empty;
            dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            Sql = @"SELECT DISTINCT ta.paytypeid_chr, ta.paytypename_vchr
                      FROM t_bse_patientpaytype ta
                     ORDER BY ta.paytypeid_chr";
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            return rec;
        }
        #endregion












    }
}
