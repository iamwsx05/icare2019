using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 医保-收费关联 
    /// 作者：  
    /// 创建时间： 2005-12-08
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsMedicineYBSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsMedicineYBSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }



        #region 查询收费项目-按字符串
        /// <summary>
        /// 查询收费项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetchargeitemByName(string p_strName, out clsT_bse_chargeitem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					ITEMID_CHR,
                    ITEMNAME_VCHR,
                    ITEMCODE_VCHR
					FROM T_BSE_CHARGEITEM
					WHERE 

					LOWER(TRIM(ITEMCODE_VCHR)) LIKE '" + p_strName.Trim().ToLower() + @"%'
					OR
					LOWER(TRIM(ITEMPYCODE_CHR)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
					OR 
					LOWER(TRIM(ITEMWBCODE_CHR)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
					ORDER BY ITEMCODE_VCHR
					";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_bse_chargeitem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {

                        p_objResultArr[i1] = new clsT_bse_chargeitem_VO();
                        if (dtbResult.Rows[i1]["ITEMID_CHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ITEMNAME_VCHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ITEMCODE_VCHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();


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


        /// <summary>
        /// 查询收费项目-按收费项目代码流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strITEMID_CHR">收费项目代码流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetchargeitemByName(string m_strITEMID_CHR, out clsT_bse_chargeitem_VO p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO();
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					ITEMID_CHR,
                    ITEMNAME_VCHR,
                    ITEMCODE_VCHR
					FROM T_BSE_CHARGEITEM
				    WHERE TRIM(ITEMID_CHR)='" + m_strITEMID_CHR.ToString().Trim() + @"'
					";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["ITEMID_CHR"] != System.DBNull.Value)
                        p_objResultArr.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ITEMNAME_VCHR"] != System.DBNull.Value)
                        p_objResultArr.m_strITEMNAME_VCHR = dtbResult.Rows[0]["ITEMNAME_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ITEMCODE_VCHR"] != System.DBNull.Value)
                        p_objResultArr.m_strITEMCODE_VCHR = dtbResult.Rows[0]["ITEMCODE_VCHR"].ToString().Trim();
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

        #region 查询医保项目-按字符串
        /// <summary>
        /// 查询医保项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBitemByName(string p_strName, out clsYBGD01_VO[] p_objResultArr)
        {
            p_objResultArr = new clsYBGD01_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					XMCDEA,
					XMCDEB,
					XMDESC
					FROM YBGD01
					WHERE
				--	LOWER(TRIM(XMCDEA)) LIKE '" + p_strName.Trim().ToLower() + @"%'
				--	OR
					LOWER(TRIM(XMCDEB)) LIKE '" + p_strName.Trim().ToLower() + @"%'
					OR
					LOWER(TRIM(XMDESC)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
					ORDER BY XMCDEA
					";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsYBGD01_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {

                        p_objResultArr[i1] = new clsYBGD01_VO();
                        if (dtbResult.Rows[i1]["XMCDEA"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMCDEA = dtbResult.Rows[i1]["XMCDEA"].ToString().Trim();
                        if (dtbResult.Rows[i1]["XMCDEB"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMCDEB = dtbResult.Rows[i1]["XMCDEB"].ToString().Trim();
                        if (dtbResult.Rows[i1]["XMDESC"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMDESC = dtbResult.Rows[i1]["XMDESC"].ToString().Trim();
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strXMCDEA">医保项目分类代码</param>
        /// <param name="strXMCDEB">医保项目编码</param>
        /// <param name="strXMDESC">医保项目名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBitemByName(string strXMCDEA, string strXMCDEB, string strXMDESC, out clsYBGD01_VO p_objResultArr)
        {
            p_objResultArr = new clsYBGD01_VO();
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					XMCDEA,
					XMCDEB,
					XMDESC
					FROM YBGD01
					WHERE
				    TRIM(XMCDEA)='" + strXMCDEA.ToString().Trim() + @"'
                    AND
                    TRIM(XMCDEB)='" + strXMCDEB.ToString().Trim() + @"'
                    AND
                    TRIM(XMDESC)='" + strXMDESC.ToString().Trim() + @"'
					";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["XMCDEA"] != System.DBNull.Value)
                        p_objResultArr.m_strXMCDEA = dtbResult.Rows[0]["XMCDEA"].ToString().Trim();
                    if (dtbResult.Rows[0]["XMCDEB"] != System.DBNull.Value)
                        p_objResultArr.m_strXMCDEB = dtbResult.Rows[0]["XMCDEB"].ToString().Trim();
                    if (dtbResult.Rows[0]["XMDESC"] != System.DBNull.Value)
                        p_objResultArr.m_strXMDESC = dtbResult.Rows[0]["XMDESC"].ToString().Trim();

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

        #region 查询疾病项目-按字符串
        /// <summary>
        /// 查询医保项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIllitemByName(string p_strName, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					ZDFL,
					FLMC,
					DMZH,
					ZHSM 
					FROM 
					YBGD05 A
					LEFT JOIN 
					YBGD04 B
					ON TRIM(B.DMLB)=TRIM(A.ZDFL)
                    WHERE 
                    LOWER(TRIM(ZDFL)) LIKE '" + p_strName.Trim().ToLower() + @"%'
                    OR
                    LOWER(TRIM(FLMC)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
                    OR
                    LOWER(TRIM(DMZH)) LIKE '" + p_strName.Trim().ToLower() + @"%'
                    OR
                    LOWER(TRIM(ZHSM)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
                   
					ORDER BY ZDFL,DMZH
                    ";
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


        [AutoComplete]
        public long m_lngGetIllitemByName(string strZDFL, string strFLMC, string strDMZH, string strZHSM, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					ZDFL,
					FLMC,
					DMZH,
					ZHSM 
					FROM 
					YBGD05 A
					LEFT JOIN 
					YBGD04 B
					ON TRIM(B.DMLB)=TRIM(A.ZDFL)
                    WHERE 
                    TRIM(ZDFL)='" + strZDFL.ToString().Trim() + @"'
                    AND
                    TRIM(FLMC)='" + strFLMC.ToString().Trim() + @"'
                    AND
                    TRIM(DMZH)='" + strDMZH.ToString().Trim() + @"'
                    AND
                    TRIM(ZHSM)='" + strZHSM.ToString().Trim() + @"'
                    ";
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

        #region 搜索 收费项目-医保关联
        /// <summary>
        /// 搜索 收费项目-医保关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSearch(string p_strName, int count, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;

            string strSQL = @"
					SELECT 
					ITEMID_CHR,
                    ITEMNAME_VCHR,
                    ITEMCODE_VCHR,
                    IFSTOP_INT
					FROM T_BSE_CHARGEITEM a
				    WHERE
                    (
					LOWER(TRIM(ITEMCODE_VCHR)) LIKE '" + p_strName.Trim().ToLower() + @"%'
					OR
					LOWER(TRIM(ITEMPYCODE_CHR)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
					OR 
					LOWER(TRIM(ITEMWBCODE_CHR)) LIKE '%" + p_strName.Trim().ToLower() + @"%'
                     )
                    [MORE]
					ORDER BY ITEMCODE_VCHR
					";
            if (count == 0)
            {
                strSQL = strSQL.Replace("[MORE]", " and (select count(*) from YB_CHARGE where chargeitemid_chr=a.itemid_chr)=0 ");
            }
            else if (count == 1)
            {
                strSQL = strSQL.Replace("[MORE]", " and (select count(*) from YB_CHARGE where chargeitemid_chr=a.itemid_chr)=1 ");
            }
            else if (count > 1)
            {
                strSQL = strSQL.Replace("[MORE]", " and (select count(*) from YB_CHARGE where chargeitemid_chr=a.itemid_chr)>1 ");
            }
            else
            {
                strSQL = strSQL.Replace("[MORE]", "");
            }

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

        #region 查询 收费项目与医保关联表 (根据收费项目流水号)
        /// <summary>
        /// 查询 收费项目与医保关联表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strOrderdicID">收费项目流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYB_CHARGE_VOArr(string m_strITEMID_CHR, out YB_CHARGE_VO[] p_objResultArr)
        {
            p_objResultArr = new YB_CHARGE_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT 
					a.*,
					b.ITEMCODE_VCHR,
					b.ITEMNAME_VCHR
					FROM 
					YB_CHARGE a,
					t_bse_chargeitem b
					WHERE
					a.chargeitemid_chr=b.itemid_chr
					AND
					a.chargeitemid_chr='" + m_strITEMID_CHR.ToString().Trim() + @"'
					ORDER BY
					a.createdate_dat
					";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new YB_CHARGE_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {

                        p_objResultArr[i1] = new YB_CHARGE_VO();
                        if (dtbResult.Rows[i1]["YBCHARGEID_CHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strYBCHARGEID_CHR = dtbResult.Rows[i1]["YBCHARGEID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CHARGEITEMID_CHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["XMCDEA"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMCDEA = dtbResult.Rows[i1]["XMCDEA"].ToString().Trim();
                        if (dtbResult.Rows[i1]["XMCDEB"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMCDEB = dtbResult.Rows[i1]["XMCDEB"].ToString().Trim();
                        if (dtbResult.Rows[i1]["XMDESC"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strXMDESC = dtbResult.Rows[i1]["XMDESC"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ZDFL"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strZDFL = dtbResult.Rows[i1]["ZDFL"].ToString().Trim();
                        if (dtbResult.Rows[i1]["FLMC"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strFLMC = dtbResult.Rows[i1]["FLMC"].ToString().Trim();
                        if (dtbResult.Rows[i1]["DMZH"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strDMZH = dtbResult.Rows[i1]["DMZH"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ZHSM"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strZHSM = dtbResult.Rows[i1]["ZHSM"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATORID_CHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"] != System.DBNull.Value)
                            p_objResultArr[i1].m_dtCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd");
                        if (dtbResult.Rows[i1]["ITEMCODE_VCHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ITEMNAME_VCHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();


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

        #region 新增 收费项目与医保关联对象
        /// <summary>
        /// 新增 收费项目与医保关联对象
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveObjYB_CHARGE_VO(YB_CHARGE_VO objYB_CHARGE_VO)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc2 = new clsHRPTableService();
            lngRes = objHRPSvc2.lngGenerateID(10, "YBCHARGEID_CHR", "YB_CHARGE", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            objHRPSvc2.Dispose();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
                INSERT INTO YB_CHARGE
                (YBCHARGEID_CHR,CHARGEITEMID_CHR,XMCDEA,XMCDEB,XMDESC,ZDFL,FLMC,DMZH,ZHSM,CREATORID_CHR,CREATEDATE_DAT)
				VALUES 
				(
                '" + p_strRecordID.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strCHARGEITEMID_CHR.ToString().Trim() + @"',
				'" + objYB_CHARGE_VO.m_strXMCDEA.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strXMCDEB.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strXMDESC.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strZDFL.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strFLMC.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strDMZH.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strZHSM.ToString().Trim() + @"',
                '" + objYB_CHARGE_VO.m_strCREATORID_CHR.ToString().Trim() + @"',
                TO_DATE('" + strDateTime + @"','YYYY-MM-DD hh24:mi:ss')
                )";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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

        #region 删除 收费项目与医保关联对象
        /// <summary>
        /// 删除 收费项目与医保关联对象
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objYB_CHARGE_VOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelclsMedicineYB_VO(YB_CHARGE_VO[] objYB_CHARGE_VOArr)
        {
            long lngRes = 0;
            lngRes = 0;
            string strDel = "";
            for (int i1 = 0; i1 < objYB_CHARGE_VOArr.Length; i1++)
            {
                strDel += "'" + objYB_CHARGE_VOArr[i1].m_strYBCHARGEID_CHR.ToString().Trim() + "',";
            }
            strDel = strDel.TrimEnd(",".ToCharArray());
            if (strDel.ToString().Trim().Equals(""))
                strDel = "''";
            string strSQL = @"
                DELETE FROM  YB_CHARGE
                WHERE YBCHARGEID_CHR IN (" + strDel + @")
                ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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

        #region 新增 收费项目与医保关联对象
        /// <summary>
        /// 新增 收费项目与医保关联对象
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateObjYB_CHARGE_VO(YB_CHARGE_VO objYB_CHARGE_VO)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
                UPDATE YB_CHARGE
                SET
                CHARGEITEMID_CHR='" + objYB_CHARGE_VO.m_strCHARGEITEMID_CHR.ToString().Trim() + @"',
                XMCDEA='" + objYB_CHARGE_VO.m_strXMCDEA.ToString().Trim() + @"',
                XMCDEB='" + objYB_CHARGE_VO.m_strXMCDEB.ToString().Trim() + @"',
                XMDESC='" + objYB_CHARGE_VO.m_strXMDESC.ToString().Trim() + @"',
                ZDFL='" + objYB_CHARGE_VO.m_strZDFL.ToString().Trim() + @"',
                FLMC='" + objYB_CHARGE_VO.m_strFLMC.ToString().Trim() + @"',
                DMZH='" + objYB_CHARGE_VO.m_strDMZH.ToString().Trim() + @"',
                ZHSM='" + objYB_CHARGE_VO.m_strZHSM.ToString().Trim() + @"',
                MENDERID_CHR='" + objYB_CHARGE_VO.m_strMENDERID_CHR.ToString().Trim() + @"',
                MENDERDATE_DAT=TO_DATE('" + strDateTime + @"','YYYY-MM-DD hh24:mi:ss')
                WHERE YBCHARGEID_CHR=" + objYB_CHARGE_VO.m_strYBCHARGEID_CHR.ToString().Trim() + @"
			    ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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



    }
}
