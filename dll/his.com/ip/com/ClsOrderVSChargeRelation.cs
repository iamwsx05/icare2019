using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class ClsOrderVSChargeRelation : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public ClsOrderVSChargeRelation()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 取收费项目记录表数据
        /// <summary>
        /// 取收费项目记录表数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItem(out DataTable dtableCharge)
        {
            dtableCharge = new DataTable();
            long lngRes = 0;
            string strSQL = @" select * from T_BSE_CHARGEITEMCAT order by ITEMCATID_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtableCharge);
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

        #region 取诊疗项目记录表数据
        /// <summary>
        /// 取诊疗项目记录表数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetClinicItem(out DataTable dtableClinic)
        {
            dtableClinic = new DataTable();
            long lngRes = 0;
            string strSQL = @" select * from T_AID_BIH_ORDERCATE order by ORDERCATEID_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtableClinic);
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

        #region 取收费项目与诊疗项目配置关系记录
        /// <summary>
        /// 取诊疗项目记录表数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeVsOrder(out clsChargeVsOrder_VO[] p_ArrResult)
        {

            long lngRes = 0;
            p_ArrResult = new clsChargeVsOrder_VO[0];
            DataTable dtable = new DataTable();
            string strSQL = @" select a.*, c.itemcatname_vchr, b.name_chr
  from t_bse_chgcatevsordercate a,
       t_aid_bih_ordercate      b,
       t_bse_chargeitemcat      c
 where trim(a.itemcatid_chr) = trim(c.itemcatid_chr)
   and a.ordercateid_chr = b.ordercateid_chr
 order by a.seq_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtable);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtable.Rows.Count > 0)
                {
                    p_ArrResult = new clsChargeVsOrder_VO[dtable.Rows.Count];
                    for (int i1 = 0; i1 < p_ArrResult.Length; i1++)
                    {
                        p_ArrResult[i1] = new clsChargeVsOrder_VO();
                        p_ArrResult[i1].m_intSEQ_INT = Convert.ToInt32(dtable.Rows[i1]["SEQ_INT"]);
                        p_ArrResult[i1].m_strItemCatID_chr = dtable.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_ArrResult[i1].m_strItemName = dtable.Rows[i1]["itemcatname_vchr"].ToString().Trim();
                        p_ArrResult[i1].m_strOrderCardID_chr = dtable.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        p_ArrResult[i1].m_strOrderName = dtable.Rows[i1]["name_chr"].ToString().Trim();
                    }
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

        #region 增加收费项目与诊疗项目配置关系表
        /// <summary>
        /// 增加收费项目与诊疗项目配置关系表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strChargeID">收费项目分类ID</param>
        /// <param name="p_strOrderId">诊疗项目分类ID</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddChargeVsOrder(string p_strChargeID, string p_strOrderId, out int m_intSeq)
        {
            long lngRes = 0;
            m_intSeq = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            m_intSeq = objHRPSvc.intGetNewNumericID("SEQ_INT", "T_BSE_CHGCATEVSORDERCATE");
            if (m_intSeq == -1)
            {
                return -1;
            }
            string p_strSQL = @"insert into T_BSE_CHGCATEVSORDERCATE(SEQ_INT,ITEMCATID_CHR,ORDERCATEID_CHR)
                    values(" + m_intSeq + ",'" + p_strChargeID + "','" + p_strOrderId + "')";
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

        #region 删除收费项目与诊疗项目配置关系表
        /// <summary>
        /// 删除收费项目与诊疗项目配置关系表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strChargeID">收费项目分类ID</param>
        /// <param name="p_strOrderId">诊疗项目分类ID</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelChargeVsOrder(int m_intSeq)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string p_strSQL = @"delete from T_BSE_CHGCATEVSORDERCATE a where a.seq_int=" + m_intSeq;
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

    }
}
