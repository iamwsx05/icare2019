using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 评分项目维护服务类.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGradeItemSetServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取评分项目.
        /// <summary>
        /// 获取评分项目.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objVOArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeItem(  out clsGradeItem_VO[] p_objVOArr)
        {
            p_objVOArr = null;
            long lngRes = 0;

            try
            { 
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = string.Empty;

                DataTable dtResult = null;


                //                if (p_bln)
                //                {
                //                    strSQL = @"select t.itemid_int       itemid,
                //       t.parentitemid_int parentitemid,
                //       t.itemdesc_vchar   itemdesc,
                //       t.itemtype_int     itemtype,
                //       t.deductscore_int  deductscore,
                //       t.deductgrade_int  deductgrade,
                //       t.status_int       status,
                //       t.multiitem_int    multiitem
                //  from t_emr_casegradeitem t
                // where t.status_int = 1";
                //                }
                //                else
                //                {
                strSQL = @"select t.itemid_int,
       t.parentitemid_int,
       t.itemdesc_vchar,
       t.itemtype_int,
       t.deductscore_int,
       t.deductgrade_int,
       t.status_int,
       t.multiitem_int
  from t_emr_casegradeitem t
  where t.status_int = 1";
                //}

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {



                    p_objVOArr = new clsGradeItem_VO[dtResult.Rows.Count];
                    DataRow dr;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        clsGradeItem_VO objVo = new clsGradeItem_VO();
                        dr = dtResult.Rows[iRow];
                        objVo.m_intItemId_Int = System.Convert.ToInt32(dr["itemid_int"]);
                        objVo.m_intParentItemId_Int = System.Convert.ToInt32(dr["parentitemid_int"]);
                        objVo.m_strItemDesc_Vchr = dr["itemdesc_vchar"].ToString();
                        objVo.m_intItemType_Int = System.Convert.ToInt32(dr["itemtype_int"]);
                        objVo.m_floatDeductsScore_Num = System.Convert.ToSingle(dr["deductscore_int"]);
                        objVo.m_intDeductGrade_Int = System.Convert.ToInt32(dr["deductgrade_int"]);
                        objVo.m_intStatus_Int = System.Convert.ToInt32(dr["status_int"]);
                        objVo.m_intMultItem_Int = System.Convert.ToInt32(dr["multiitem_int"]);
                        p_objVOArr[iRow] = objVo;
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取评分项目.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeItemTable( out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;

            try
            { 
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select t.itemid_int,
       t.parentitemid_int,
       t.itemdesc_vchar,
       t.itemtype_int,
       t.deductscore_int,
       t.deductgrade_int,
       t.status_int,
       t.multiitem_int
  from t_emr_casegradeitem t
  where t.status_int = 1";

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 使用树结构查询获得有效的评分项目.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objItemVOArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeItemByTree( out clsGradeItem_VO[] p_objItemVOArr)
        {
            p_objItemVOArr = null;
            long lngRes = 0;

            try
            { 
                string strSQL = @"select t.itemid_int,
       t.parentitemid_int,
       t.status_int,
       t.itemdesc_vchar,
       t.itemtype_int,
       t.deductscore_int,
       t.deductgrade_int,
       t.multiitem_int
  from t_emr_casegradeitem t
 where t.status_int = 1
connect by prior t.itemid_int = t.parentitemid_int
start with parentitemid_int = 0
 order by t.itemid_int";

                DataTable dtResult = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

                if (dtResult.Rows.Count > 0)
                {
                    p_objItemVOArr = new clsGradeItem_VO[dtResult.Rows.Count];
                    DataRow dr;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        clsGradeItem_VO objTempVO = new clsGradeItem_VO();
                        dr = dtResult.Rows[iRow];
                        objTempVO.m_intItemId_Int = Convert.ToInt32(dr["itemid_int"]);
                        objTempVO.m_intParentItemId_Int = Convert.ToInt32(dr["parentitemid_int"]);
                        objTempVO.m_strItemDesc_Vchr = Convert.ToString(dr["itemdesc_vchar"]);
                        objTempVO.m_intItemType_Int = Convert.ToInt32(dr["itemtype_int"]);
                        objTempVO.m_floatDeductsScore_Num = Convert.ToSingle(dr["deductscore_int"]);
                        objTempVO.m_intDeductGrade_Int = Convert.ToInt32(dr["deductgrade_int"]);
                        objTempVO.m_intMultItem_Int = Convert.ToInt32(dr["multiitem_int"]);

                        p_objItemVOArr[iRow] = objTempVO;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 增加一项评分项目.

        /// <summary>
        /// 增加一项评分项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objVO">增加项目.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddGradeItem( ref clsGradeItem_VO p_objVO)
        {
            if (p_objVO == null)
                return -1;

            long lngRes = 0;

            try
            { 
                // 获得一个序列号
                int MaxSeqId = 0;
                clsPublicGradeServ objServ = new clsPublicGradeServ();
                lngRes = objServ.m_lngGetSequence( out MaxSeqId);
                p_objVO.m_intItemId_Int = MaxSeqId;


                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"insert into t_emr_casegradeitem
  (itemid_int,
   parentitemid_int,
   itemdesc_vchar,
   itemtype_int,
   deductscore_int,
   deductgrade_int,
   status_int,
   multiitem_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?)";

                long lngEff = -1;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);

                objDPArr[0].Value = MaxSeqId;
                objDPArr[1].Value = p_objVO.m_intParentItemId_Int;
                objDPArr[2].Value = p_objVO.m_strItemDesc_Vchr;
                objDPArr[3].Value = p_objVO.m_intItemType_Int;
                objDPArr[4].Value = p_objVO.m_floatDeductsScore_Num;
                objDPArr[5].Value = p_objVO.m_intDeductGrade_Int;
                objDPArr[6].Value = p_objVO.m_intStatus_Int;
                objDPArr[7].Value = p_objVO.m_intMultItem_Int;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 删除评分项目

        /// <summary>
        /// 删除评分项目.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intItemID">项目ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteItem( int p_intItemID)
        {
            if (p_intItemID == 0)
                return -1;
            long lngRes = 0;
            try
            { 
                clsHRPTableService objHRPServ = new clsHRPTableService();

                //                string strSQL = @"update t_emr_casegradeitem
                //     set status_int = 0
                //   where itemid_int in (select itemid_int
                //                          from t_emr_casegradeitem
                //                         where status_int = 1
                //                        connect by prior itemid_int = parentitemid_int
                //                         start with itemid_int = ?)";

                string strSQL = @"update t_emr_casegradeitem t
   set status_int = 0
 where exists (select itemid_int
          from t_emr_casegradeitem a
         where status_int = 1
           and a.itemid_int = t.itemid_int
        connect by prior itemid_int = parentitemid_int
         start with itemid_int = ?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                long lngEff = -1;

                objDPArr[0].Value = p_intItemID;
                //objDPArr[1].Value = p_intItemID;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 更新评分项目
        /// <summary>
        /// 更新评分项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateAnItem( clsGradeItem_VO p_objVO)
        {
            if (p_objVO == null)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"update t_emr_casegradeitem
   set parentitemid_int = ?,
       itemdesc_vchar   = ?,
       itemtype_int     = ?,
       deductscore_int  = ?,
       deductgrade_int  = ?,
       status_int       = 1,
       multiitem_int    = ?
 where itemid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);

                objDPArr[0].Value = p_objVO.m_intParentItemId_Int;
                objDPArr[1].Value = p_objVO.m_strItemDesc_Vchr;
                objDPArr[2].Value = p_objVO.m_intItemType_Int;
                objDPArr[3].Value = p_objVO.m_floatDeductsScore_Num;
                objDPArr[4].Value = p_objVO.m_intDeductGrade_Int;
                objDPArr[5].Value = p_objVO.m_intMultItem_Int;
                objDPArr[6].Value = p_objVO.m_intItemId_Int;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_objVO = null;
            return lngRes;
        }
        #endregion
    }
}
