using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 默认执行科室维护——中间层
    /// 作者： 何贵球
    /// 创建时间： 2006-6-08
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOcDeptDefaultSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsOcDeptDefaultSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        #region 取医嘱执行分类表数据
        /// <summary>
        /// 取医嘱执行分类表数据
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

        #region 通过执行分类ID执行科室列表
        /// <summary>
        ///  通过执行分类ID执行科室列表
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

        #region 取科室列表
        /// <summary>
        ///  取科室列表
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

        #region 通过开单科室ID和执行类别ID取默认执行科室
        /// <summary>
        /// 通过开单科室ID和执行类别ID取默认执行科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCreateAreaId">开单科室ID</param>
        /// <param name="p_strOrdercateId">医嘱执行类型ID</param>
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

        #region 增加默认执行科室
        /// <summary>
        /// 增加默认执行科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdercateId">医嘱诊疗项目类型ID</param>
        /// <param name="p_strClacAreaId">核算病区ID</param>
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

        #region 删除默认执行科室
        /// <summary>
        /// 删除默认执行科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeq">流水号</param>
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

        #region 取seq_public的下一个主键
        /// <summary>
        /// seq_public的下一个主键
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
