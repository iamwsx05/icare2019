using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ִ�������Ҷ�Ӧά�������м��
    /// ���ߣ� �ι���
    /// ����ʱ�䣺 2006-6-02
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOcDeptMapSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsOcDeptMapSvc()
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

        #region ȡt_aid_bih_ocdeptlist�����м�¼
        /// <summary>
        ///  ȡt_aid_bih_ocdeptlist�����м�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAllOcDeptMaping(out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select ocd.SEQ_INT          as intSeq,
                                      ocd.ORDERCATEID_CHR  as strOrdercateId,
                                      ocd.CLACAREA_CHR     as strClacArea,
                                      dep.CODE_VCHR        as strDeptCode,
                                      dep.DEPTNAME_VCHR    as strDeptName
                               from t_aid_bih_ocdeptlist ocd,
                                    t_bse_deptdesc dep
                               where ocd.CLACAREA_CHR = dep.DEPTID_CHR 
                               order by ocd.CLACAREA_CHR";
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
            /* string strSQL = @" select ocd.SEQ_INT          as intSeq,
                                       ocd.ORDERCATEID_CHR  as strOrdercateId,
                                       dep.DEPTID_CHR       as strDeptId,
                                       dep.CODE_VCHR        as strDeptCode,
                                       dep.DEPTNAME_VCHR    as strDeptName,
                                       dep.PARENTID         as strParentId
                                from t_aid_bih_ocdeptlist ocd,
                                     t_bse_deptdesc dep
                                where dep.DEPTID_CHR = ocd.CLACAREA_CHR (+)
                                order by dep.CODE_VCHR";
             */
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

        #region ����ҽ��ִ�з���
        /// <summary>
        /// ����ҽ��ִ�з���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateName"></param>
        /// <param name="p_strOrderCateId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long CreateOrderPerformCate(string p_strOrderCateName, out string p_strOrderCateId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            objHRPSvc.m_lngGenerateNewID("T_AID_BIH_ORDERPERFORMCATE", "ORDERCATEID_CHR", out p_strOrderCateId);

            string p_strSQL = @"insert into T_AID_BIH_ORDERPERFORMCATE(ORDERCATEID_CHR, ORDERCATENAME_VCHR)
                    values('" + p_strOrderCateId.Trim() + "', '" + p_strOrderCateName + "')";
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

        #region �޸�ҽ��ִ�з���
        /// <summary>
        /// �޸�ҽ��������Ŀ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateName"></param>
        /// <param name="p_strOrderCateId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long UpdateOrderPerformCate(string p_strOrderCateName, string p_strOrderCateId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string p_strSQL = @"update T_AID_BIH_ORDERPERFORMCATE set ORDERCATENAME_VCHR = '" + p_strOrderCateName
                             + "' where ORDERCATEID_CHR = '" + p_strOrderCateId + "'";

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

        #region ɾ��ҽ��ִ�з���
        /// <summary>
        /// ɾ��ҽ��ִ�з���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long DeleteOrderPerformCate(string p_strOrderCateId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string p_strSQL = @"delete from T_AID_BIH_ORDERPERFORMCATE where ORDERCATEID_CHR = '" + p_strOrderCateId + "'";

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

        #region ����ҽ��ִ�����������
        /// <summary>
        /// ����ҽ��ִ�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSort"></param>
        /// <param name="p_strOrderCateId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long UpdateOrderPerformCateSort(string p_strOrderCateId, int p_intSort)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string p_strSQL = @"update T_AID_BIH_ORDERPERFORMCATE set SORT_INT = " + p_intSort.ToString()
                             + " where ORDERCATEID_CHR = '" + p_strOrderCateId + "'";

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

        #region ����ҽ��ִ�з���IDȡ��Ӧִ�п���
        /// <summary>
        /// ����ҽ��ִ�з���IDȡ��Ӧִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdercateId"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptMapedByOcId(string p_strOrdercateId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select ocd.SEQ_INT          as intSeq,
                                      ocd.ORDERCATEID_CHR  as strOrdercateId,
                                      ocd.CLACAREA_CHR     as strClacArea,
                                      dep.CODE_VCHR        as strDeptCode,
                                      dep.DEPTNAME_VCHR    as strDeptName
                               from t_aid_bih_ocdeptlist ocd,
                                    t_bse_deptdesc dep
                               where ocd.CLACAREA_CHR = dep.DEPTID_CHR and
                                     ocd.ORDERCATEID_CHR = '" + p_strOrdercateId + @"'
                               order by ocd.CLACAREA_CHR";
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

        #region ���Ӷ�Ӧִ�п���
        /// <summary>
        /// ���Ӷ�Ӧִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdercateId">ҽ��������Ŀ����ID</param>
        /// <param name="p_strClacAreaId">���㲡��ID</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long AddOcDeptMaping(int p_intSeq, string p_strOrdercateId, string p_strClacAreaId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string p_strSQL = @"insert into t_aid_bih_ocdeptlist(SEQ_INT, ORDERCATEID_CHR, CLACAREA_CHR)
                    values(" + p_intSeq.ToString() + ", '" + p_strOrdercateId + "', '" + p_strClacAreaId + "')";
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

        #region ɾ��ִ�п���
        /// <summary>
        /// ɾ��ִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeq">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long RemoveOcDeptMaping(int p_intSeq)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string p_strSQL = @"delete from t_aid_bih_ocdeptlist where seq_int = " + p_intSeq.ToString();
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

        /// <summary>
        /// ɾ��ִ�п���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeq">��ˮ��</param>
        /// <param name="p_strClacArea">�������</param>
        /// <param name="p_strOrdercateId">ҽ��ִ������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long RemoveOcDeptMaping(int p_intSeq, string p_strClacArea, string p_strOrdercateId)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string p_strSQL = @"delete from t_aid_bih_ocdeptlist where seq_int = " + p_intSeq.ToString();
            try
            {
                lngRes = objHRPSvc.DoExcute(p_strSQL);

                p_strSQL = "delete from t_aid_bih_ocdeptdefault t where t.ordercateid_chr = '" + p_strOrdercateId
                    + "' and t.clacarea_chr ='" + p_strClacArea + "'";

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
