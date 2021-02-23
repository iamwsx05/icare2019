using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 价类管理
    /// 张国良   2004-9-22
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOPChargeInsSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsOPChargeInsSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //保险公司
        #region 获取保险公司列表  张国良	2004-9-22
        /// <summary>
        /// 获取保险公司列表  张国良	2004-9-22
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetINSCOMPANYDataArr(out clsInsCompany_VO[] p_objResultArr)
        {
            p_objResultArr = new clsInsCompany_VO[0];
            long lngRes = 0;
            string strSQL = @"select companyid_chr, companyname_chr, usercode_chr, remark_vchr from t_aid_inscompany order by companyid_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsInsCompany_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsInsCompany_VO();
                        p_objResultArr[i1].m_strCOMPANYID_CHR = dtbResult.Rows[i1]["COMPANYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCOMPANYNAME_CHR = dtbResult.Rows[i1]["COMPANYNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREMARK_VCHR = dtbResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
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

        #region  新增保险公司  张国良  2004-9-24

        [AutoComplete]
        public long m_lngAddNewINSCOMPANYD(clsInsCompany_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            p_strRecordID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //返回一最大的计划号
            lngRes = objHRPSvc.lngGenerateID(4, "COMPANYID_CHR", "T_AID_INSCOMPANY", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            string strSQL = "INSERT INTO T_AID_INSCOMPANY (COMPANYID_CHR,COMPANYNAME_CHR,USERCODE_CHR,REMARK_VCHR) VALUES ('" + p_strRecordID + "','" + p_objRecord.m_strCOMPANYNAME_CHR + "','" + p_objRecord.m_strUSERCODE_CHR + "','" + p_objRecord.m_strREMARK_VCHR + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region  修改保险公司信息  张国良  2004-9-24

        [AutoComplete]
        public long m_lngModifyINSCOMPANYD(clsInsCompany_VO objResult)
        {
            long lngRes = 0;
            string strSQL = "UPDate T_AID_INSCOMPANY Set  " +
                "COMPANYNAME_CHR='" + objResult.m_strCOMPANYNAME_CHR + "' " +
                ", USERCODE_CHR='" + objResult.m_strUSERCODE_CHR + "' " +
                ", REMARK_VCHR='" + objResult.m_strREMARK_VCHR + "' " +
                " Where COMPANYID_CHR='" + objResult.m_strCOMPANYID_CHR + "' ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除险公司信息	张国良	2004-9-24
        /// <summary>
        /// 删除收费项目分类类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngINSCOMPANYDel(string strID)
        {
            long lngRes = 0;
            string strSQL = "Delete T_AID_INSCOMPANY " +
                " Where COMPANYID_CHR='" + strID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //保险计划
        #region 获取保险计划列表  张国良	2004-9-24
        /// <summary>
        /// 获取保险计划列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetINSPLANDataArr(out clsInsPlan_VO[] p_objResultArr)
        {
            p_objResultArr = new clsInsPlan_VO[0];
            long lngRes = 0;
            string strSQL = @"select p.planid_chr, p.planname_chr, p.remark_vchr, p.companyid_chr, p.usercode_chr,companyname_chr from t_aid_insplan p join t_aid_inscompany c on p.companyid_chr = c.companyid_chr order by planid_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsInsPlan_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsInsPlan_VO();
                        p_objResultArr[i1].m_strPLANID_CHR = dtbResult.Rows[i1]["PLANID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPLANNAME_CHR = dtbResult.Rows[i1]["PLANNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREMARK_VCHR = dtbResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCOMPANYID_CHR = dtbResult.Rows[i1]["COMPANYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCOMPANYNAME_CHR = dtbResult.Rows[i1]["COMPANYNAME_CHR"].ToString().Trim();
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

        #region  新增保险计划  张国良  2004-9-24
        /// <summary>
        /// 新增保险计划
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="p_strRecordID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewINSPLAN(clsInsPlan_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            p_strRecordID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //返回一最大的计划号
            lngRes = objHRPSvc.lngGenerateID(4, "PLANID_CHR", "T_AID_INSPLAN", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            string strSQL = "INSERT INTO T_AID_INSPLAN (PLANID_CHR,PLANNAME_CHR,USERCODE_CHR,REMARK_VCHR,COMPANYID_CHR) VALUES ('" + p_strRecordID + "','" + p_objRecord.m_strPLANNAME_CHR + "','" + p_objRecord.m_strUSERCODE_CHR + "','" + p_objRecord.m_strREMARK_VCHR + "','" + p_objRecord.m_strCOMPANYID_CHR + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region  修改保险计划  张国良  2004-9-24
        /// <summary>
        /// 修改保险计划
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyINSPLAN(clsInsPlan_VO objResult)
        {
            long lngRes = 0;
            string strSQL = "UPDate T_AID_INSPLAN Set  " +
                "PLANNAME_CHR='" + objResult.m_strPLANNAME_CHR + "' " +
                ", USERCODE_CHR='" + objResult.m_strUSERCODE_CHR + "' " +
                ", REMARK_VCHR='" + objResult.m_strREMARK_VCHR + "' " +
                ", COMPANYID_CHR='" + objResult.m_strCOMPANYID_CHR + "' " +
                " Where PLANID_CHR='" + objResult.m_strPLANID_CHR + "' ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除险计划	张国良	2004-9-24
        /// <summary>
        /// 删除险计划
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelINSPLAN(string strID)
        {
            long lngRes = 0;
            string strSQL = "Delete T_AID_INSPLAN " +
                " Where PLANID_CHR='" + strID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //保险种类
        #region 获取保险种类列表  张国良	2004-9-27
        /// <summary>
        /// 获取保险种类列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetINSCOPAYataArr(out clsInsPay_VO[] p_objResultArr)
        {
            p_objResultArr = new clsInsPay_VO[0];
            long lngRes = 0;
            string strSQL = @"select c.copayid_chr, c.copayname_chr, c.precent_dec, c.usercode_chr, c.remark_vchr, c.planid_chr ,planname_chr from t_aid_inscopay c join  t_aid_insplan p on c.planid_chr = p.planid_chr  order by copayid_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsInsPay_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsInsPay_VO();

                        p_objResultArr[i1].m_strCOPAYID_CHR = dtbResult.Rows[i1]["COPAYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCOPAYNAME_CHR = dtbResult.Rows[i1]["COPAYNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_dblPRECENT_DEC = Convert.ToDouble(dtbResult.Rows[i1]["PRECENT_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREMARK_VCHR = dtbResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPLANID_CHR = dtbResult.Rows[i1]["PLANID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPLANNAME_CHR = dtbResult.Rows[i1]["PLANNAME_CHR"].ToString().Trim();
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

        #region  新增保险种类  张国良  2004-9-27
        /// <summary>
        /// 新增保险种类
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="p_strRecordID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewINSCOPAY(clsInsPay_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            p_strRecordID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //返回一最大的计划号
            lngRes = objHRPSvc.lngGenerateID(4, "COPAYID_CHR", "T_AID_INSCOPAY", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            string strSQL = "INSERT INTO T_AID_INSCOPAY (COPAYID_CHR,COPAYNAME_CHR,PRECENT_DEC,USERCODE_CHR,REMARK_VCHR,PLANID_CHR) VALUES ('" + p_strRecordID + "','" + p_objRecord.m_strCOPAYNAME_CHR + "','" + p_objRecord.m_dblPRECENT_DEC + "','" + p_objRecord.m_strUSERCODE_CHR + "','" + p_objRecord.m_strREMARK_VCHR + "','" + p_objRecord.m_strPLANID_CHR + "')";
            string strSQL2 = "insert into t_aid_InsChargeItem (PRECENT_DEC,ITEMID_CHR,COPAYID_CHR) " +
                            " select 100 as PRECENT_DEC, ItemID_chr,'" + p_strRecordID + "' as COPAYID_CHR from t_bse_ChargeItem ";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                lngRes = objHRPSvc.DoExcute(strSQL2);
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

        #region  修改保险种类  张国良  2004-9-27
        /// <summary>
        /// 修改保险公司计划
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyINSCOPAY(clsInsPay_VO objResult)
        {
            long lngRes = 0;
            string strSQL = "UPDate T_AID_INSCOPAY Set  " +
                "COPAYNAME_CHR='" + objResult.m_strCOPAYNAME_CHR + "' " +
                ", PRECENT_DEC='" + objResult.m_dblPRECENT_DEC + "' " +
                ", USERCODE_CHR='" + objResult.m_strUSERCODE_CHR + "' " +
                ", REMARK_VCHR='" + objResult.m_strREMARK_VCHR + "' " +
                ", PLANID_CHR='" + objResult.m_strPLANID_CHR + "' " +
                " Where COPAYID_CHR='" + objResult.m_strCOPAYID_CHR + "' ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除险种类	张国良	2004-9-27
        /// <summary>
        /// 删除险种类
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelINSCOPAY(string strID)
        {
            long lngRes = 0;
            string strSQL = "Delete T_AID_INSCOPAY " +
                " Where COPAYID_CHR='" + strID + "' ";
            string strSQL2 = "Delete t_aid_InsChargeItem " +
                " Where COPAYID_CHR='" + strID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    lngRes = objHRPSvc.DoExcute(strSQL2);
                }
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

    }
}
