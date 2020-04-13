using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// Ĭ��ִ�п���ά�������м��
    /// ���ߣ� �ι���
    /// ����ʱ�䣺 2006-6-08
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOcDeptDefaultSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsOcDeptDefaultSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ȡҽ��ִ�з��������
        /// <summary>
        /// ȡҽ��ִ�з��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetOrderPerformCate(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @" select * from T_AID_BIH_ORDERPERFORMCATE order by SORT_INT";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
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

        #region ͨ��ִ�з���IDִ�п����б�
        /// <summary>
        ///  ͨ��ִ�з���IDִ�п����б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPerformDeptByOcId(string p_strOrderCateId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select ocd.SEQ_INT          as intSeq,
                                      ocd.ORDERCATEID_CHR  as strOrdercateId,
                                      ocd.CLACAREA_CHR     as strClacArea,
                                      dep.CODE_VCHR        as strDeptCode,
                                      dep.DEPTNAME_VCHR    as strDeptName
                               from 
                                    t_aid_bih_ocdeptlist ocd,
                                    t_bse_deptdesc dep
                               where ocd.CLACAREA_CHR = dep.DEPTID_CHR and ocd.ORDERCATEID_CHR = '"
                                      + p_strOrderCateId +
                                     @"' order by ocd.CLACAREA_CHR";
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

        #region ȡ�����б�
        /// <summary>
        ///  ȡ�����б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAllDept(out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select * from t_bse_deptdesc";


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

        #region ͨ����������ID��ִ�����IDȡĬ��ִ�п���
        /// <summary>
        /// ͨ����������ID��ִ�����IDȡĬ��ִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCreateAreaId">��������ID</param>
        /// <param name="p_strOrdercateId">ҽ��ִ������ID</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetDefaultPerformDeptBy(string p_strCreateAreaId, string p_strOrdercateId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"select * from t_aid_bih_ocdeptdefault where createarea_chr = '"
                              + p_strCreateAreaId + "' and ordercateid_chr = '"
                              + p_strOrdercateId + "'";
            try
            {
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

        #region ����Ĭ��ִ�п���
        /// <summary>
        /// ����Ĭ��ִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdercateId">ҽ��������Ŀ����ID</param>
        /// <param name="p_strClacAreaId">���㲡��ID</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long CreateOcDeptDefault(int p_intSeq, string p_strOrdercateId, string p_strClacAreaId, string p_strCreateAreaId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string p_strSQL = @"insert into T_AID_BIH_OCDEPTDEFAULT(SEQ_INT, ORDERCATEID_CHR, CLACAREA_CHR, CREATEAREA_CHR)
                    values(" + p_intSeq.ToString() + ", '" + p_strOrdercateId + "', '" + p_strClacAreaId + "', '" + p_strCreateAreaId + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(p_strSQL);
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

        #region ɾ��Ĭ��ִ�п���
        /// <summary>
        /// ɾ��Ĭ��ִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeq">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long RemoveOcDeptDefault(int p_intSeq)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string p_strSQL = @"delete from T_AID_BIH_OCDEPTDEFAULT where seq_int = " + p_intSeq.ToString();
            try
            {
                lngRes = objHRPSvc.DoExcute(p_strSQL);
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

        #region ȡseq_public����һ������
        /// <summary>
        /// seq_public����һ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetNextSeq(out string p_seqValue)
        {
            long lngRes = 0;
            p_seqValue = "";
            string strSQL = @" select seq_public.nextval as seqValue from dual";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                lngRes = 0;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();

                if (dtResult.Rows.Count == 1)
                {
                    p_seqValue = dtResult.Rows[0]["seqValue"].ToString();
                }
                else
                {
                    lngRes = 0;
                }

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

    }
}
