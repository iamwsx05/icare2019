using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// Ԥ�����ѯ
    /// ���ߣ�He Guiqiu
    /// ����ʱ��:2006-06-14
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPrepayQuerySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsPrepayQuerySvc()
        {

        }

        #region ���ݴ���������ѯ
        /// <summary>
        /// ���ݴ���������ѯ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCondition"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPrepayInfoBy(string p_strCondition, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" SELECT c.INPATIENTID_CHR,   
                                     d.LASTNAME_VCHR,   
                                     d.SEX_CHR,   
                                     a.DEPTNAME_VCHR,   
                                     b.CREATE_DAT,   
                                     b.PREPAYINV_VCHR,   
                                     b.MONEY_DEC,   
                                     b.PAYTYPE_INT,
                                     b.CUYCATE_INT,
                                     b.BALANCEFLAG_INT,
                                     b.PREPAYID_CHR,
                                     b.UPTYPE_INT,   
                                     e.LASTNAME_VCHR as CREATER,
                                     f.LASTNAME_VCHR as CONFIRMEMP
                                FROM T_BSE_DEPTDESC a,   
                                     T_OPR_BIH_PREPAY b,   
                                     T_OPR_BIH_REGISTER c,   
                                     T_OPR_BIH_REGISTERDETAIL d,   
                                     T_BSE_EMPLOYEE e,
                                     T_BSE_EMPLOYEE f  
                               WHERE ( b.CREATORID_CHR = e.EMPID_CHR ) and
                                     ( b.CONFIRMEMP_CHR = f.EMPID_CHR(+) ) and  
                                     ( b.REGISTERID_CHR = c.REGISTERID_CHR ) and  
                                     ( d.REGISTERID_CHR = c.REGISTERID_CHR ) and  
                                     ( a.DEPTID_CHR = c.AREAID_CHR ) "
                                      + p_strCondition +
                                     @" order by b.CREATE_DAT";

            strSQL = @"SELECT c.INPATIENTID_CHR,
                               d.LASTNAME_VCHR,
                               d.SEX_CHR,
                               a.DEPTNAME_VCHR,
                               b.CREATE_DAT,
                               b.PREPAYINV_VCHR,
                               b.MONEY_DEC,
                               b.PAYTYPE_INT,
                               b.CUYCATE_INT,
                               b.BALANCEFLAG_INT,
                               b.PREPAYID_CHR,
                               b.UPTYPE_INT,
                               e.LASTNAME_VCHR     as CREATER,
                               f.LASTNAME_VCHR     as CONFIRMEMP,
                               r.REPPRNBILLNO_VCHR
                          FROM T_BSE_DEPTDESC            a,
                               T_OPR_BIH_PREPAY          b,
                               T_OPR_BIH_REGISTER        c,
                               T_OPR_BIH_REGISTERDETAIL  d,
                               T_BSE_EMPLOYEE            e,
                               T_BSE_EMPLOYEE            f,
                               T_OPR_BIH_BILLREPEATPRINT r
                         WHERE (b.CREATORID_CHR = e.EMPID_CHR)
                           and (b.CONFIRMEMP_CHR = f.EMPID_CHR(+))
                           and (b.REGISTERID_CHR = c.REGISTERID_CHR)
                           and (d.REGISTERID_CHR = c.REGISTERID_CHR)
                           and (a.DEPTID_CHR = c.AREAID_CHR)
                           and b.PREPAYID_CHR = r.BILLID_CHR(+)
                          -- and r.BILLTYPE_CHR = '1' 
                           {0}  
                         order by b.CREATE_DAT";
            strSQL = string.Format(strSQL, p_strCondition);

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        #endregion

        #region �޸�֧������
        /// <summary>
        ///�޸�֧������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCuycate">֧������</param>
        ///  <param name="p_strPrepayId">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long MondifyCuycate(string p_strCuycate, string p_strPrepayId)
        {
            long lngRes = 0;
            string strSQL = @" UPDATE T_OPR_BIH_PREPAY
                              SET CUYCATE_INT = ? WHERE PREPAYID_CHR = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] parameterArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameterArr);
                int cuycate;
                try
                {
                    cuycate = int.Parse(p_strCuycate);
                }
                catch
                {
                    cuycate = 0;
                }
                parameterArr[0].Value = cuycate;

                parameterArr[1].Value = p_strPrepayId;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, parameterArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡsys_setting������Ϣ
        /// <summary>
        /// ��ȡsys_setting������Ϣ
        /// </summary>
        /// 
        /// <returns></returns>
        [AutoComplete]
        public long GetSysSetting(string p_setId, out string p_setStatus)
        {
            long lngRes = 0;

            string strSQL;
            strSQL = @"SELECT SETSTATUS_INT FROM T_SYS_SETTING WHERE SETID_CHR = '" + p_setId + "'";

            System.Data.DataTable dtbResult = new System.Data.DataTable();

            p_setStatus = "0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_setStatus = dtbResult.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


    }
}