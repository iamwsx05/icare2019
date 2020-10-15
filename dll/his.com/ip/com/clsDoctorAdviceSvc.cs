using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 医嘱-收费项目
    /// 作者： 徐斌辉
    /// 创建时间： 2004-10-06
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDoctorAdviceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 构造函数
        public clsDoctorAdviceSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion
        //T_opr_bih_orderexecute(医嘱执行单)
        #region 查询
        /// <summary>
        /// 查询医嘱执行单-按医嘱单编号和生成执行单时间 [有效的]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderid">医嘱单编号</param>
        /// <param name="strStartCreateDate">生成执行单起始时间</param>
        /// <param name="strEndCreateDate">生成执行单结束时间</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExecute(string p_strOrderid, string strStartCreateDate, string strEndCreateDate, out clsT_opr_bih_orderexecute_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_orderexecute_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM t_opr_bih_orderexecute WHERE STATUS_INT = '1' AND orderid_chr='" + p_strOrderid.Trim() + "'";
            if (strStartCreateDate.Trim() != "")
            {
                strSQL += " AND createdate_dat>=to_date('" + strStartCreateDate.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
            }
            if (strEndCreateDate.Trim() != "")
            {
                strSQL += " AND createdate_dat<=to_date('" + strEndCreateDate.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_orderexecute_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_orderexecute_VO();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_intEXECUTETIME_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECUTETIME_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strEXECUTEDATE_VCHR = dtbResult.Rows[i1]["EXECUTEDATE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intISCHARGE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISCHARGE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intISINCEPT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISINCEPT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intISFIRST_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISFIRST_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intISRECRUIT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRECRUIT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPERATOR_CHR = dtbResult.Rows[i1]["OPERATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEACTIVATORID_CHR = dtbResult.Rows[i1]["DEACTIVATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        /// 查询医嘱执行单-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExecuteByID(string p_strID, out clsT_opr_bih_orderexecute_VO p_objResult)
        {
            p_objResult = new clsT_opr_bih_orderexecute_VO();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM t_opr_bih_orderexecute WHERE STATUS = '1' AND orderexecid_chr = '" + p_strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_bih_orderexecute_VO();
                    p_objResult.m_strORDEREXECID_CHR = dtbResult.Rows[0]["ORDEREXECID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERID_CHR = dtbResult.Rows[0]["ORDERID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATORID_CHR = dtbResult.Rows[0]["CREATORID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATOR_CHR = dtbResult.Rows[0]["CREATOR_CHR"].ToString().Trim();
                    p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intEXECUTETIME_INT = Convert.ToInt32(dtbResult.Rows[0]["EXECUTETIME_INT"].ToString().Trim());
                    p_objResult.m_strEXECUTEDATE_VCHR = dtbResult.Rows[0]["EXECUTEDATE_VCHR"].ToString().Trim();
                    p_objResult.m_intISCHARGE_INT = Convert.ToInt32(dtbResult.Rows[0]["ISCHARGE_INT"].ToString().Trim());
                    p_objResult.m_intISINCEPT_INT = Convert.ToInt32(dtbResult.Rows[0]["ISINCEPT_INT"].ToString().Trim());
                    p_objResult.m_intISFIRST_INT = Convert.ToInt32(dtbResult.Rows[0]["ISFIRST_INT"].ToString().Trim());
                    p_objResult.m_intISRECRUIT_INT = Convert.ToInt32(dtbResult.Rows[0]["ISRECRUIT_INT"].ToString().Trim());
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strOPERATORID_CHR = dtbResult.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                    p_objResult.m_strOPERATOR_CHR = dtbResult.Rows[0]["OPERATOR_CHR"].ToString().Trim();
                    p_objResult.m_strDEACTIVATORID_CHR = dtbResult.Rows[0]["DEACTIVATORID_CHR"].ToString().Trim();
                    p_objResult.m_strDEACTIVATOR_CHR = dtbResult.Rows[0]["DEACTIVATOR_CHR"].ToString().Trim();
                    p_objResult.m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        #region 新增
        /// <summary>
        /// 新增医嘱执行单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号 [out 参数]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderExecute(out string p_strRecordID, clsT_opr_bih_orderexecute_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(18, "ORDEREXECID_CHR", "t_opr_bih_orderexecute", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_bih_orderexecute (ORDEREXECID_CHR,ORDERID_CHR,CREATORID_CHR,CREATOR_CHR,CREATEDATE_DAT,EXECUTETIME_INT,EXECUTEDATE_VCHR,ISCHARGE_INT,ISINCEPT_INT,ISFIRST_INT,ISRECRUIT_INT,STATUS_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(12, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strORDERID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strCREATORID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strCREATOR_CHR;
                objLisAddItemRefArr[4].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[5].Value = p_objRecord.m_intEXECUTETIME_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strEXECUTEDATE_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intISCHARGE_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_intISINCEPT_INT;
                objLisAddItemRefArr[9].Value = p_objRecord.m_intISFIRST_INT;
                objLisAddItemRefArr[10].Value = p_objRecord.m_intISRECRUIT_INT;
                //有效标志{1、有效；0、无效；-1历史；}
                objLisAddItemRefArr[11].Value = 1;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改医嘱执行单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderExecuteByID(string p_strID, clsT_opr_bih_orderexecute_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_ORDEREXECUTE";
            strSQL += " SET";
            strSQL += "    ORDERID_CHR = '" + p_objRecord.m_strORDERID_CHR + "'";
            //生成执行单人ID	生成执行单人	生成执行单时间
            //strSQL +="  , CREATORID_CHR =  '" + p_objRecord.m_strCREATORID_CHR + "'";
            //strSQL +="  , CREATOR_CHR =  '" + p_objRecord.m_strCREATOR_CHR + "'";
            //strSQL +="  , CREATEDATE_DAT = TO_DATE('"+p_objRecord.m_strCREATEDATE_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , EXECUTETIME_INT = " + p_objRecord.m_intEXECUTETIME_INT.ToString();
            strSQL += "  , EXECUTEDATE_VCHR = '" + p_objRecord.m_strEXECUTEDATE_VCHR + "'";
            strSQL += "  , ISCHARGE_INT = " + p_objRecord.m_intISCHARGE_INT.ToString();
            strSQL += "  , ISINCEPT_INT = " + p_objRecord.m_intISINCEPT_INT.ToString();
            strSQL += "  , ISFIRST_INT = " + p_objRecord.m_intISFIRST_INT.ToString();
            strSQL += "  , ISRECRUIT_INT = " + p_objRecord.m_intISRECRUIT_INT.ToString();
            strSQL += "  , STATUS_INT = " + p_objRecord.m_intSTATUS_INT.ToString();
            strSQL += "	, OPERATORID_CHR = '" + p_objRecord.m_strOPERATORID_CHR + "'";
            strSQL += "  , OPERATOR_CHR = '" + p_objRecord.m_strOPERATOR_CHR + "'";
            strSQL += "  , DEACTIVATORID_CHR = '" + p_objRecord.m_strDEACTIVATORID_CHR + "'";
            strSQL += "  , DEACTIVATOR_CHR = '" + p_objRecord.m_strDEACTIVATOR_CHR + "'";
            strSQL += "  , DEACTIVATE_DAT = TO_DATE('" + p_objRecord.m_strDEACTIVATE_DAT + "','YYYY-MM-DD hh24:mi:ss'))";
            strSQL += " WHERE";
            strSQL += "      ORDEREXECID_CHR ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 删除医嘱执行单	[标志为无效]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_strDeactivatorID">删除人ID</param>
        /// <param name="p_strDeactivator">删除人</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderExecuteByID(string p_strID, string p_strDeactivatorID, string p_strDeactivator)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_ORDEREXECUTE";
            strSQL += " SET";
            strSQL += "  , STATUS_INT = 0";
            strSQL += "  , DEACTIVATORID_CHR = '" + p_strDeactivatorID.Trim() + "'";
            strSQL += "  , DEACTIVATOR_CHR = '" + p_strDeactivator.Trim() + "'";
            strSQL += "  , DEACTIVATE_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss'))";
            strSQL += " WHERE";
            strSQL += "      ORDEREXECID_CHR ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //T_aid_bih_ordergroup(医嘱组套[编码])
        #region 查询
        /// <summary>
        /// 获取医嘱组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult">【返回DataTable out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroup(out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            /* @update by xzf (05-11-04)
			 * 将共享类型改为{私用/公用}
			 */
            /* @remark--------------------------------------
			string strSQL = @"
					SELECT	a.* 
							,(SELECT LASTNAME_VCHR FROM t_bse_employee WHERE t_bse_employee.EMPID_CHR=a.CREATORID_CHR) CreatorName
							,Decode(a.sharetype_int,1,'本人',2,'科室',3,'完全','') ShareType
					FROM t_aid_bih_ordergroup a";
			---------------------------------------------- */
            string strSQL = @"
					SELECT	a.* 
							,(SELECT LASTNAME_VCHR FROM t_bse_employee WHERE t_bse_employee.EMPID_CHR=a.CREATORID_CHR) CreatorName
							,Decode(a.sharetype_int,1,'私用',2,'公用','') ShareType
					FROM t_aid_bih_ordergroup a";
            /* <<======================================= */
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
        /// <summary>
        /// 查询医嘱组套-查询字符串 [有效的]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupByName(string p_strName, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT groupid_chr, name_chr, des_vchr, creatorid_chr,createdate_dat, sharetype_int, wbcode_chr, pycode_chr,issamerecipeno_int ";
            strSQL += " ,(SELECT LASTNAME_VCHR FROM t_bse_employee WHERE t_bse_employee.EMPID_CHR=t_aid_bih_ordergroup.CREATORID_CHR) CreatorName";
            /* @update by xzf (05-11-04) 
			 * 将共享类型改为{私用/公用}
			 */
            // @strSQL +=" ,Decode(sharetype_int,1,'本人',2,'科室',3,'完全','') ShareType";
            strSQL += " ,Decode(sharetype_int,1,'私用',2,'公用','') ShareType";
            /* <<=============================================== */
            strSQL += " FROM t_aid_bih_ordergroup ";
            strSQL += " WHERE ";
            strSQL += "	   (LOWER(trim(NAME_CHR)) like '%" + p_strName.Trim().ToLower() + "%') ";
            strSQL += "  or (LOWER(trim(WBCODE_CHR)) like '" + p_strName.Trim().ToLower() + "%') ";
            strSQL += "  or (LOWER(trim(PYCODE_CHR)) like '" + p_strName.Trim().ToLower() + "%') ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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
        /// 查询医嘱组套-按名称 [有效的]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">医嘱组套名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupByName(string p_strName, out clsT_aid_bih_ordergroup_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_ordergroup_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT groupid_chr, name_chr, des_vchr, creatorid_chr,createdate_dat, sharetype_int, wbcode_chr, pycode_chr,issamerecipeno_int ";
            strSQL += " ,(SELECT LASTNAME_VCHR FROM t_bse_employee WHERE t_bse_employee.EMPID_CHR=t_aid_bih_ordergroup.CREATORID_CHR) CreatorName";
            /* @update by xzf (05-11-04)
			 * 将共享类型改为{私用/公用}
			 */
            // @strSQL +=" ,Decode(sharetype_int,1,'本人',2,'科室',3,'完全','') ShareType";
            strSQL += " ,Decode(sharetype_int,1,'私用',2,'公用','') ShareType";
            /* <<======================================= */
            strSQL += " FROM t_aid_bih_ordergroup ";
            strSQL += " WHERE ";
            strSQL += "	   (LOWER(trim(NAME_CHR)) like '%" + p_strName.Trim().ToLower() + "%') ";
            strSQL += "  or (LOWER(trim(WBCODE_CHR)) like '" + p_strName.Trim().ToLower() + "%') ";
            strSQL += "  or (LOWER(trim(PYCODE_CHR)) like '" + p_strName.Trim().ToLower() + "%') ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_ordergroup_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_ordergroup_VO();
                        p_objResultArr[i1].m_strGROUPID_CHR = dtbResult.Rows[i1]["GROUPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_intSHARETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SHARETYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        try { p_objResultArr[i1].m_intISSAMERECIPENO_INT = Int32.Parse(dtbResult.Rows[i1]["issamerecipeno_int"].ToString()); }
                        catch { }
                        //非字段
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();

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
        /// 查询医嘱组套-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">查询医嘱组套流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupByID(string p_strID, out clsT_aid_bih_ordergroup_VO p_objResult)
        {
            p_objResult = new clsT_aid_bih_ordergroup_VO();
            long lngRes = 0;
            string strSQL = @"SELECT groupid_chr, name_chr, des_vchr, creatorid_chr,createdate_dat, sharetype_int, wbcode_chr, pycode_chr,issamerecipeno_int ";
            strSQL += " ,(SELECT LASTNAME_VCHR FROM t_bse_employee WHERE t_bse_employee.EMPID_CHR=t_aid_bih_ordergroup.CREATORID_CHR) CreatorName";
            /* @update by xzf (05-11-04)
			 * 将共享类型改为{私用/公用}
			 */
            /* @remark---------------------
			strSQL +=" ,Decode(sharetype_int,1,'本人',2,'科室',3,'完全','') ShareType";
			------------------------------ */
            strSQL += " ,Decode(sharetype_int,1,'私用',2,'公用','') ShareType";
            /* <<======================================= */
            strSQL += " FROM t_aid_bih_ordergroup ";
            strSQL += " WHERE GROUPID_CHR = '" + p_strID.Trim() + "' ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_bih_ordergroup_VO();
                    p_objResult.m_strGROUPID_CHR = dtbResult.Rows[0]["GROUPID_CHR"].ToString().Trim();
                    p_objResult.m_strNAME_CHR = dtbResult.Rows[0]["NAME_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strCREATORID_CHR = dtbResult.Rows[0]["CREATORID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSHARETYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["SHARETYPE_INT"].ToString().Trim());
                    p_objResult.m_strWBCODE_CHR = dtbResult.Rows[0]["WBCODE_CHR"].ToString().Trim();
                    p_objResult.m_strPYCODE_CHR = dtbResult.Rows[0]["PYCODE_CHR"].ToString().Trim();
                    /* @add by xzf (05-10-26) */
                    p_objResult.m_strCreatorName = dtbResult.Rows[0]["CreatorName"].ToString().Trim();
                    /* <<========================================== */
                    try { p_objResult.m_intISSAMERECIPENO_INT = Int32.Parse(dtbResult.Rows[0]["issamerecipeno_int"].ToString()); }
                    catch { }
                    //非字段
                    p_objResult.m_strCreatorName = dtbResult.Rows[0]["CreatorName"].ToString().Trim();
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
        #region 新增
        /// <summary>
        /// 增加医嘱组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号 [out 参数]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderGroup(out string p_strRecordID, clsT_aid_bih_ordergroup_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "GROUPID_CHR", "T_aid_bih_ordergroup", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_aid_bih_ordergroup (GROUPID_CHR,NAME_CHR,DES_VCHR,CREATORID_CHR,CREATEDATE_DAT,SHARETYPE_INT,WBCODE_CHR,PYCODE_CHR,ISSAMERECIPENO_INT) VALUES (?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strNAME_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strCREATORID_CHR;
                objLisAddItemRefArr[4].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[5].Value = p_objRecord.m_intSHARETYPE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strWBCODE_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strPYCODE_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_intISSAMERECIPENO_INT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改医嘱组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderGroupByID(string p_strRecordID, clsT_aid_bih_ordergroup_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_AID_BIH_ORDERGROUP SET";
            strSQL += "    NAME_CHR = '" + p_objRecord.m_strNAME_CHR.Trim() + "'";
            strSQL += "  , DES_VCHR = '" + p_objRecord.m_strDES_VCHR.Trim() + "'";
            //strSQL += "  , CREATORID_CHR = '" + p_objRecord.m_strCREATORID_CHR.Trim() + "'";
            //strSQL += "  , CREATEDATE_DAT = TO_DATE('"+p_objRecord.m_strCREATEDATE_DAT.Trim()+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , SHARETYPE_INT = '" + p_objRecord.m_intSHARETYPE_INT.ToString().Trim() + "'";
            strSQL += "  , WBCODE_CHR = '" + p_objRecord.m_strWBCODE_CHR.Trim() + "'";
            strSQL += "  , PYCODE_CHR = '" + p_objRecord.m_strPYCODE_CHR.Trim() + "'";
            strSQL += "  , ISSAMERECIPENO_INT = " + p_objRecord.m_intISSAMERECIPENO_INT.ToString();
            strSQL += " WHERE ";
            strSQL += " (GROUPID_CHR ='" + p_strRecordID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 更改医嘱组套	[真删除]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderGroupByID(string p_strID)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM  T_AID_BIH_ORDERGROUP WHERE GROUPID_CHR ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //T_aid_bih_orderexclude(医嘱排斥项目)
        #region 查询
        /// <summary>
        /// 医嘱排斥项目-按诊疗项目名称、医嘱组套名称 [模糊查询]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderDicName">诊疗项目名称</param>
        /// <param name="p_strOrderGroupName">医嘱组套名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExcludeByName(string p_strOrderDicName, string p_strOrderGroupName, out clsT_aid_bih_orderexclude_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_orderexclude_VO[0];
            long lngRes = 0;
            string strSQL = @"";
            strSQL += " SELECT aa.* FROM";
            strSQL += " (SELECT a.*";
            strSQL += "     ,DECODE(a.excludetype_int,1,'组内排斥',2,'多组排斥',3,'全排斥','') excludetype";
            strSQL += "     ,(SELECT b.name_chr FROM t_bse_bih_orderdic b WHERE b.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     ,(SELECT c.name_chr FROM t_aid_bih_ordergroup c WHERE c.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     ,(SELECT d.name_chr FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) ExculdedicName";
            strSQL += "     ,(SELECT e.name_chr FROM t_aid_bih_ordergroup e WHERE e.groupid_chr=a.excludgroupid_chr) ExcludGroupName";
            strSQL += "     ,DECODE(a.type3excludetype_int,1,'排斥临嘱',2,'排斥长嘱',3,'排斥长临嘱','') Type3ExcludeTypeName";
            strSQL += "     ,DECODE(a.activetype_int,1,'提交时生效',2,'执行时生效','') ActiveTypeName";

            strSQL += " FROM t_aid_bih_orderexclude a) aa";
            strSQL += " WHERE ";
            strSQL += "     (LOWER(trim(aa.OrderdicName)) like '%" + p_strOrderDicName.Trim().ToLower() + "%' or LOWER(trim(aa.ExculdedicName)) like '%" + p_strOrderDicName.Trim().ToLower() + "%')";
            strSQL += "     and (LOWER(trim(aa.GroupName)) like '%" + p_strOrderGroupName.Trim().ToLower() + "%' or LOWER(trim(aa.ExcludGroupName)) like '%" + p_strOrderGroupName.Trim().ToLower() + "%')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_orderexclude_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_orderexclude_VO();
                        p_objResultArr[i1].m_strEXCULDEID_CHR = dtbResult.Rows[i1]["EXCULDEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intEXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXCLUDETYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strGROUPID_CHR = dtbResult.Rows[i1]["GROUPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXCULDEDICID_CHR = dtbResult.Rows[i1]["EXCULDEDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXCLUDGROUPID_CHR = dtbResult.Rows[i1]["EXCLUDGROUPID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intTYPE3EXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["TYPE3EXCLUDETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intACTIVETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        p_objResultArr[i1].m_strExcludGroupName = dtbResult.Rows[i1]["ExcludGroupName"].ToString().Trim();
                        p_objResultArr[i1].m_strExculdedicName = dtbResult.Rows[i1]["ExculdedicName"].ToString().Trim();
                        p_objResultArr[i1].m_strGroupName = dtbResult.Rows[i1]["GroupName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrderdicName = dtbResult.Rows[i1]["OrderdicName"].ToString().Trim();
                        p_objResultArr[i1].m_strType3ExcludeTypeName = dtbResult.Rows[i1]["Type3ExcludeTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strActiveTypeName = dtbResult.Rows[i1]["ActiveTypeName"].ToString().Trim();
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
        /// 医嘱排斥项目-按诊疗项目流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderDicID">按诊疗项目流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExcludeByOrderDic(string p_strOrderDicID, out clsT_aid_bih_orderexclude_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_orderexclude_VO[0];
            long lngRes = 0;
            string strSQL = @"";
            strSQL += " SELECT aa.* FROM";
            strSQL += " (SELECT a.*";
            strSQL += "     ,DECODE(excludetype_int,1,'组内排斥',2,'多组排斥',3,'全排斥','') excludetype";
            strSQL += "     ,(SELECT b.name_chr FROM t_bse_bih_orderdic b WHERE b.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     ,(SELECT c.name_chr FROM t_aid_bih_ordergroup c WHERE c.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     ,(SELECT d.name_chr FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) ExculdedicName";
            strSQL += "     ,(SELECT e.name_chr FROM t_aid_bih_ordergroup e WHERE e.groupid_chr=a.excludgroupid_chr) ExcludGroupName";
            strSQL += "     ,DECODE(a.type3excludetype_int,1,'排斥临嘱',2,'排斥长嘱',3,'排斥长临嘱','') Type3ExcludeTypeName";
            strSQL += "     ,DECODE(a.activetype_int,1,'提交时生效',2,'执行时生效','') ActiveTypeName";
            strSQL += " FROM t_aid_bih_orderexclude a) aa";
            strSQL += " WHERE ";
            strSQL += "     (aa.orderdicid_chr = '" + p_strOrderDicID.Trim() + "')";
            strSQL += " order by aa.exculdedicid_chr,aa.excludgroupid_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_orderexclude_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_orderexclude_VO();
                        p_objResultArr[i1].m_strEXCULDEID_CHR = dtbResult.Rows[i1]["EXCULDEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intEXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXCLUDETYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strGROUPID_CHR = dtbResult.Rows[i1]["GROUPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXCULDEDICID_CHR = dtbResult.Rows[i1]["EXCULDEDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXCLUDGROUPID_CHR = dtbResult.Rows[i1]["EXCLUDGROUPID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intTYPE3EXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["TYPE3EXCLUDETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intACTIVETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        p_objResultArr[i1].m_strExcludGroupName = dtbResult.Rows[i1]["ExcludGroupName"].ToString().Trim();
                        p_objResultArr[i1].m_strExculdedicName = dtbResult.Rows[i1]["ExculdedicName"].ToString().Trim();
                        p_objResultArr[i1].m_strGroupName = dtbResult.Rows[i1]["GroupName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrderdicName = dtbResult.Rows[i1]["OrderdicName"].ToString().Trim();
                        p_objResultArr[i1].m_strType3ExcludeTypeName = dtbResult.Rows[i1]["Type3ExcludeTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strActiveTypeName = dtbResult.Rows[i1]["ActiveTypeName"].ToString().Trim();
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
        /// 医嘱排斥项目-按诊疗项目流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderDicID">按诊疗项目流水号</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExcludeByOrderDic(string p_strOrderDicID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"";
            strSQL += " SELECT aa.* FROM";
            strSQL += " (SELECT a.*";
            strSQL += "     ,DECODE(a.excludetype_int,1,'组内排斥',2,'多组排斥',3,'全排斥','') excludetype";
            strSQL += "     ,(SELECT b.name_chr FROM t_bse_bih_orderdic b WHERE b.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     ,(SELECT c.name_chr FROM t_aid_bih_ordergroup c WHERE c.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     ,(SELECT d.USERCODE_CHR FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) USERCODE_CHR";
            /** add by xzf (05-09-27) */
            strSQL += "    ,(select trim(d.USERCODE_CHR) from t_bse_bih_orderdic d where d.orderdicid_chr=a.exculdedicid_chr) ExculedUserCode";
            /* <<================================================= */
            strSQL += "     ,(SELECT trim(d.name_chr) FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) ExculdedicName";
            strSQL += "     ,(SELECT trim(e.name_chr) FROM t_aid_bih_ordergroup e WHERE e.groupid_chr=a.excludgroupid_chr) ExcludGroupName";
            strSQL += "     ,DECODE(a.type3excludetype_int,1,'排斥临嘱',2,'排斥长嘱',3,'排斥长临嘱','') Type3ExcludeTypeName";
            strSQL += "     ,DECODE(a.activetype_int,1,'提交时生效',2,'执行时生效','') ActiveTypeName";
            strSQL += "    ,(SELECT Status_INT FROM t_bse_bih_orderdic f WHERE f.orderdicid_chr=a.orderdicid_chr) Status_INT";
            strSQL += " FROM t_aid_bih_orderexclude a) aa";
            strSQL += " WHERE ";
            strSQL += "     (aa.orderdicid_chr = '" + p_strOrderDicID.Trim() + "')";
            strSQL += " order by aa.exculdedicid_chr,aa.excludgroupid_chr";
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
        /// <summary>
        /// 医嘱排斥项目-按医嘱组套流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderGroup">按医嘱组套流水号</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExcludeByOrderGroup(string p_strOrderGroup, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"";
            strSQL += " SELECT aa.* FROM";
            strSQL += " (SELECT a.*";
            strSQL += "     ,DECODE(excludetype_int,1,'组内排斥',2,'多组排斥',3,'全排斥','') excludetype";
            strSQL += "     ,(SELECT b.name_chr FROM t_bse_bih_orderdic b WHERE b.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     ,(SELECT c.name_chr FROM t_aid_bih_ordergroup c WHERE c.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     ,(SELECT d.USERCODE_CHR FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) USERCODE_CHR";
            strSQL += "     ,(SELECT trim(d.name_chr) FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) ExculdedicName";
            strSQL += "     ,(SELECT trim(e.name_chr) FROM t_aid_bih_ordergroup e WHERE e.groupid_chr=a.excludgroupid_chr) ExcludGroupName";
            strSQL += "     ,DECODE(a.type3excludetype_int,1,'排斥临嘱',2,'排斥长嘱',3,'排斥长临嘱','') Type3ExcludeTypeName";
            strSQL += "     ,DECODE(a.activetype_int,1,'提交时生效',2,'执行时生效','') ActiveTypeName";
            strSQL += " FROM t_aid_bih_orderexclude a) aa";
            strSQL += " WHERE ";
            strSQL += "     (aa.groupid_chr = '" + p_strOrderGroup.Trim() + "')";
            strSQL += " order by aa.exculdedicid_chr,aa.excludgroupid_chr";
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
        /// <summary>
        /// 医嘱排斥项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">查询医嘱组套流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderExcludeByID(string p_strID, out clsT_aid_bih_orderexclude_VO p_objResult)
        {
            p_objResult = new clsT_aid_bih_orderexclude_VO();
            long lngRes = 0;
            string strSQL = @"";
            strSQL += " SELECT a.*";
            strSQL += "     ,DECODE(excludetype_int,1,'组内排斥',2,'多组排斥',3,'全排斥','') excludetype";
            strSQL += "     ,(SELECT b.name_chr FROM t_bse_bih_orderdic b WHERE b.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     ,(SELECT c.name_chr FROM t_aid_bih_ordergroup c WHERE c.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     ,(SELECT d.name_chr FROM t_bse_bih_orderdic d WHERE d.orderdicid_chr=a.exculdedicid_chr) ExculdedicName";
            strSQL += "     ,(SELECT e.name_chr FROM t_aid_bih_ordergroup e WHERE e.groupid_chr=a.excludgroupid_chr) ExcludGroupName";
            strSQL += "     ,DECODE(a.type3excludetype_int,1,'排斥临嘱',2,'排斥长嘱',3,'排斥长临嘱','') Type3ExcludeTypeName";
            strSQL += "     ,DECODE(a.activetype_int,1,'提交时生效',2,'执行时生效','') ActiveTypeName";
            strSQL += " FROM t_aid_bih_orderexclude a";
            strSQL += " WHERE";
            strSQL += "     a.exculdeid_chr='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_bih_orderexclude_VO();
                    p_objResult.m_strEXCULDEID_CHR = dtbResult.Rows[0]["EXCULDEID_CHR"].ToString().Trim();
                    p_objResult.m_intEXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["EXCLUDETYPE_INT"].ToString().Trim());
                    p_objResult.m_strORDERDICID_CHR = dtbResult.Rows[0]["ORDERDICID_CHR"].ToString().Trim();
                    p_objResult.m_strGROUPID_CHR = dtbResult.Rows[0]["GROUPID_CHR"].ToString().Trim();
                    p_objResult.m_strEXCULDEDICID_CHR = dtbResult.Rows[0]["EXCULDEDICID_CHR"].ToString().Trim();
                    p_objResult.m_strEXCLUDGROUPID_CHR = dtbResult.Rows[0]["EXCLUDGROUPID_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_intTYPE3EXCLUDETYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["TYPE3EXCLUDETYPE_INT"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        p_objResult.m_intACTIVETYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["ACTIVETYPE_INT"].ToString().Trim());
                    }
                    catch { }
                    //非字段
                    p_objResult.m_strExcludGroupName = dtbResult.Rows[0]["ExcludGroupName"].ToString().Trim();
                    p_objResult.m_strExculdedicName = dtbResult.Rows[0]["ExculdedicName"].ToString().Trim();
                    p_objResult.m_strGroupName = dtbResult.Rows[0]["GroupName"].ToString().Trim();
                    p_objResult.m_strOrderdicName = dtbResult.Rows[0]["OrderdicName"].ToString().Trim();
                    p_objResult.m_strType3ExcludeTypeName = dtbResult.Rows[0]["Type3ExcludeTypeName"].ToString().Trim();
                    p_objResult.m_strActiveTypeName = dtbResult.Rows[0]["ActiveTypeName"].ToString().Trim();
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
        #region 新增
        /// <summary>
        /// 新增医嘱排斥项目	[只增一条,没有考虑关联排斥]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号 [out 参数]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewOrderExclude(out string p_strRecordID, clsT_aid_bih_orderexclude_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "EXCULDEID_CHR", "t_aid_bih_orderexclude", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_aid_bih_orderexclude (EXCULDEID_CHR,EXCLUDETYPE_INT,ORDERDICID_CHR,GROUPID_CHR,EXCULDEDICID_CHR,EXCLUDGROUPID_CHR,TYPE3EXCLUDETYPE_INT,ACTIVETYPE_INT,MAXDOSAGE_INT,USAGEIDS_VCHR) VALUES (?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(10, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_intEXCLUDETYPE_INT;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strORDERDICID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strGROUPID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strEXCULDEDICID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strEXCLUDGROUPID_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intTYPE3EXCLUDETYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intACTIVETYPE_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strMAXDOSAGE_INT;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strUSAGEIDS_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改医嘱排斥项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngModifyOrderExcludeByID(string p_strRecordID, clsT_aid_bih_orderexclude_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_AID_BIH_ORDEREXCLUDE ";
            strSQL += " SET";
            strSQL += "    EXCLUDETYPE_INT = '" + p_objRecord.m_intEXCLUDETYPE_INT.ToString() + "'";
            strSQL += "  , ORDERDICID_CHR = '" + p_objRecord.m_strORDERDICID_CHR + "'";
            strSQL += "  , GROUPID_CHR = '" + p_objRecord.m_strGROUPID_CHR + "'";
            strSQL += "  , EXCULDEDICID_CHR = '" + p_objRecord.m_strEXCULDEDICID_CHR + "'";
            strSQL += "  , EXCLUDGROUPID_CHR = '" + p_objRecord.m_strEXCLUDGROUPID_CHR + "', MAXDOSAGE_INT = '" + p_objRecord.m_strMAXDOSAGE_INT + "', USAGEIDS_VCHR = '" + p_objRecord.m_strUSAGEIDS_VCHR + "'";
            int iTem = -1;
            try { iTem = p_objRecord.m_intTYPE3EXCLUDETYPE_INT; }
            catch { }
            if (iTem != -1)
            {
                strSQL += "  , TYPE3EXCLUDETYPE_INT = '" + iTem.ToString() + "'";
            }
            strSQL += "  , ACTIVETYPE_INT = '" + p_objRecord.m_intACTIVETYPE_INT.ToString() + "'";
            strSQL += " WHERE";
            strSQL += "       (EXCULDEID_CHR ='" + p_strRecordID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 医嘱排斥项目	[真删除 删除一条记录]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteOrderExcludeByID(string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE FROM  T_AID_BIH_ORDEREXCLUDE WHERE (EXCULDEID_CHR ='" + p_strID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 验证
        /// <summary>
        /// 验证此医嘱排斥是否存在
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFirstID">第一条件 [诊疗项目流水号 或 医嘱组套流水号]</param>
        /// <param name="p_strSecondID">第二条件 [诊疗项目流水号 或 医嘱组套流水号]</param>
        /// <param name="p_intType">组合类型 [0、包括1234四种情况；1、诊疗项目—诊疗项目；2、诊疗项目—组套医嘱；3、组套医嘱—诊疗项目；4、组套医嘱—组套医嘱；]</param>
        /// <param name="p_intActiveType">排斥生效类型{0=不作条件;1=提交时生效;2=执行时生效}</param>
        /// <param name="p_blnResult">验证结果 [out]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckRecordBy(string p_strFirstID, string p_strSecondID, int p_intType, int p_intActiveType, out bool p_blnResult)
        {
            p_blnResult = false;
            long lngRes = 0;
            string strSQL = "SELECT * FROM t_aid_bih_orderexclude WHERE rownum<=1 ";
            switch (p_intType)
            {
                case 0://0、包括1234四种情况；
                       //1
                    strSQL += " and (";
                    strSQL += "	  ((Trim(orderdicid_chr)='" + p_strFirstID.Trim() + "' and Trim(exculdedicid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(exculdedicid_chr)='" + p_strFirstID.Trim() + "' and Trim(orderdicid_chr)='" + p_strSecondID.Trim() + "'))";
                    //2
                    strSQL += " or ((Trim(orderdicid_chr)='" + p_strFirstID.Trim() + "' and Trim(excludgroupid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(exculdedicid_chr)='" + p_strFirstID.Trim() + "' and Trim(groupid_chr)='" + p_strSecondID.Trim() + "'))";
                    //3
                    strSQL += " or ((Trim(excludgroupid_chr)='" + p_strFirstID.Trim() + "' and Trim(orderdicid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(groupid_chr)='" + p_strFirstID.Trim() + "' and Trim(exculdedicid_chr)='" + p_strSecondID.Trim() + "'))";
                    //4
                    strSQL += " or ((Trim(excludgroupid_chr)='" + p_strFirstID.Trim() + "' and Trim(groupid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(groupid_chr)='" + p_strFirstID.Trim() + "' and Trim(excludgroupid_chr)='" + p_strSecondID.Trim() + "'))";
                    strSQL += " )";
                    break;
                case 1://诊疗项目——诊疗项目
                    strSQL += " and ((Trim(orderdicid_chr)='" + p_strFirstID.Trim() + "' and Trim(exculdedicid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(exculdedicid_chr)='" + p_strFirstID.Trim() + "' and Trim(orderdicid_chr)='" + p_strSecondID.Trim() + "'))";
                    break;
                case 2://诊疗项目——组套医嘱
                    strSQL += " and ((Trim(orderdicid_chr)='" + p_strFirstID.Trim() + "' and Trim(excludgroupid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(exculdedicid_chr)='" + p_strFirstID.Trim() + "' and Trim(groupid_chr)='" + p_strSecondID.Trim() + "'))";
                    break;
                case 3://组套医嘱——诊疗项目
                    strSQL += " and ((Trim(excludgroupid_chr)='" + p_strFirstID.Trim() + "' and Trim(orderdicid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(groupid_chr)='" + p_strFirstID.Trim() + "' and Trim(exculdedicid_chr)='" + p_strSecondID.Trim() + "'))";
                    break;
                case 4://组套医嘱——组套医嘱
                    strSQL += " and ((Trim(excludgroupid_chr)='" + p_strFirstID.Trim() + "' and Trim(groupid_chr)='" + p_strSecondID.Trim() + "')";
                    strSQL += " or (Trim(groupid_chr)='" + p_strFirstID.Trim() + "' and Trim(excludgroupid_chr)='" + p_strSecondID.Trim() + "'))";
                    break;
                default:
                    return -1;
            }
            switch (p_intActiveType)
            {
                case 0:
                    break;
                case 1:
                    strSQL += " and ACTIVETYPE_INT=1";
                    break;
                case 2:
                    strSQL += " and ACTIVETYPE_INT=2";
                    break;
                default:
                    return -1;
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_blnResult = true;
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
        /// 判断诊疗项目(包括组套)是否排斥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFirstID">第一条件 [诊疗项目流水号 或 医嘱组套流水号]</param>
        /// <param name="p_strSecondID">第二条件 [诊疗项目流水号 或 医嘱组套流水号]</param>
        /// <param name="p_intActiveType">排斥生效类型{0=不作条件;1=提交时生效;2=执行时生效}</param>
        /// <param name="p_blnResult">判断结果 [out]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeOrderExclude(string p_strFirstID, string p_strSecondID, int p_intActiveType, out bool p_blnResult)
        {
            p_blnResult = false;
            long lngRes = 0;
            if (!p_blnResult) m_lngCheckRecordBy(p_strFirstID, p_strSecondID, 0, p_intActiveType, out p_blnResult);
            return lngRes;
        }
        #endregion

        //T_aid_bih_ordergroup_detail(医嘱组套成员 [编码])
        #region 查询
        /// <summary>
        /// 查询医嘱组套成员－医嘱组套流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">医嘱组套流水号</param>
        /// <param name="p_dtResult">【返回DataTable out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupDetailByGroupID(string p_strGroupID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            /** @update by xzf (05-09-12)
			  @去除字段空格
			  */
            string strSQL = @"
						SELECT a.detailid_chr,a.groupid_chr,a.orderdicid_chr,a.freqid_chr,a.dosetype_chr 
							, (select trim(b1.name_chr) from T_aid_bih_ordergroup b1 where b1.groupid_chr=a.groupid_chr) GroupName 
							, (select trim(b2.name_chr) from t_bse_bih_orderdic b2 where b2.orderdicid_chr=a.orderdicid_chr) OrderdicName 
							, (select trim(b3.freqname_chr) from t_aid_recipefreq b3 where b3.freqid_chr=a.freqid_chr) FreqName 
							, trim(a.dosage_dec) as dosage_dec, trim(a.dosageunit_chr) as dosageunit_chr,trim(a.use_dec) as use_dec,trim(a.useunit_chr) as useunit_chr,a.get_dec,a.getunit_chr 
							, (select trim(b4.usagename_vchr) from t_bse_usagetype b4 where b4.usageid_chr=a.dosetype_chr) DosetypeName 
							, a.entrust_vchr, a.isrich_int, a.parentid_chr, a.ifparentid_int 
							, DECODE(a.isrich_int,1,'√',0,'×','') IsRich 
							, DECODE(a.ifparentid_int,1,'√',0,'×','') IfParentName 
							, (select b5.name_chr from t_bse_bih_orderdic b5 where b5.orderdicid_chr=a.parentid_chr) ParentName 
							, (SELECT TRIM (dosageviewtype) FROM t_aid_bih_ordercate WHERE TRIM (ordercateid_chr) = 
									(SELECT TRIM (ordercateid_chr) FROM t_bse_bih_orderdic WHERE TRIM (orderdicid_chr) = TRIM (a.orderdicid_chr))) DOSAGEVIEWTYPE
							, (SELECT TRIM (usageviewtype) FROM t_aid_bih_ordercate WHERE TRIM (ordercateid_chr) = 
									(SELECT TRIM (ordercateid_chr) FROM t_bse_bih_orderdic WHERE TRIM (orderdicid_chr) = TRIM (a.orderdicid_chr))) USAGEVIEWTYPE   
                            ,(select b6.status_int from  t_bse_bih_orderdic b6 where trim(b6.orderdicid_chr)=TRIM (a.orderdicid_chr))STATUS_INT   
						FROM t_aid_bih_ordergroup_detail a ";
            strSQL += " WHERE a.groupid_chr='" + p_strGroupID.Trim() + "'";
            /* <<============================== */
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
        /// <summary>
        /// 查询医嘱组套成员－医嘱组套流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">医嘱组套流水号</param>
        /// <param name="p_objResultArr">【返回VO数组 out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupDetailByGroupID(string p_strGroupID, out clsT_aid_bih_ordergroup_detail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_ordergroup_detail_VO[0];
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.detailid_chr,a.groupid_chr,a.orderdicid_chr,a.freqid_chr,a.dosetype_chr";
            strSQL += "     , (select b1.name_chr from T_aid_bih_ordergroup b1 where b1.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     , (select b2.name_chr from t_bse_bih_orderdic b2 where b2.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     , (select b3.freqname_chr from t_aid_recipefreq b3 where b3.freqid_chr=a.freqid_chr) FreqName";
            strSQL += "     , a.dosage_dec, a.dosageunit_chr,a.use_dec,a.useunit_chr,a.get_dec,a.getunit_chr";
            strSQL += "     , (select b4.usagename_vchr from t_bse_usagetype b4 where b4.usageid_chr=a.dosetype_chr) DosetypeName";
            strSQL += "     , a.entrust_vchr, a.isrich_int, a.parentid_chr, a.ifparentid_int";
            strSQL += "     , DECODE(a.isrich_int,1,'√',0,'×','') IsRich";
            strSQL += "     , DECODE(a.ifparentid_int,1,'√',0,'×','') IfParentName";
            strSQL += "     , (select b5.name_chr from t_bse_bih_orderdic b5 where b5.orderdicid_chr=a.parentid_chr) ParentName";
            strSQL += " FROM t_aid_bih_ordergroup_detail a";
            strSQL += " WHERE a.groupid_chr='" + p_strGroupID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_ordergroup_detail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_ordergroup_detail_VO();
                        p_objResultArr[i1].m_strDETAILID_CHR = dtbResult.Rows[i1]["DETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strGROUPID_CHR = dtbResult.Rows[i1]["GROUPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFREQID_CHR = dtbResult.Rows[i1]["FREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_fltDOSAGE_DEC = float.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_fltUSE_DEC = float.Parse(dtbResult.Rows[i1]["USE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strUSEUNIT_CHR = dtbResult.Rows[i1]["USEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_fltGET_DEC = float.Parse(dtbResult.Rows[i1]["GET_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strGETUNIT_CHR = dtbResult.Rows[i1]["GETUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDOSETYPE_CHR = dtbResult.Rows[i1]["DOSETYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strENTRUST_VCHR = dtbResult.Rows[i1]["ENTRUST_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strPARENTID_CHR = dtbResult.Rows[i1]["PARENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIFPARENTID_INT = Convert.ToInt32(dtbResult.Rows[i1]["IFPARENTID_INT"].ToString().Trim());
                        //非字段
                        p_objResultArr[i1].m_strDosetypeName = dtbResult.Rows[i1]["DosetypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strFreqName = dtbResult.Rows[i1]["FreqName"].ToString().Trim();
                        p_objResultArr[i1].m_strGroupName = dtbResult.Rows[i1]["GroupName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrderdicName = dtbResult.Rows[i1]["OrderdicName"].ToString().Trim();
                        p_objResultArr[i1].m_strParentName = dtbResult.Rows[i1]["ParentName"].ToString().Trim();
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
        /// 查询医嘱组套成员－医嘱组套流水号并转换为医嘱项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">医嘱组套流水号</param>
        /// <param name="p_objResultArr">【返回VO数组 out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupDetailByGroupID(string p_strGroupID, out DataTable dtbResult, bool m_bltemp)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"
            SELECT distinct 
            a.detailid_chr,
            a.groupid_chr, 
            a.orderdicid_chr, 
            a.freqid_chr,
            a.dosetype_chr,  
            (b1.name_chr) groupname,
            
            (b2.name_chr) orderdicname,
            (b3.freqname_chr) freqname,
            (a.dosage_dec)  dosage_dec,
            (a.dosageunit_chr)  dosageunit_chr,
            (a.use_dec)  use_dec,
            (a.useunit_chr)  useunit_chr,
            a.get_dec, 
            a.getunit_chr,
            (b4.usagename_vchr) dosetypename, 
            a.entrust_vchr,
            a.isrich_int, 
            a.parentid_chr, 
            a.ifparentid_int,
            DECODE (a.isrich_int, 1, '√', 0, '×', '') isrich,
            DECODE (a.ifparentid_int, 1, '√', 0, '×', '') ifparentname,
            b5.name_chr parentname,
            b6.dosageviewtype dosageviewtype,
            b6.usageviewtype usageviewtype,
            b2.status_int  status_int,
            b7.itemspec_vchr,
            b4.usageid_chr,
            b5.sampleid_vchr,
            b5.partid_vchr,
            decode(b7.IPCHARGEFLG_INT,1,Round(b7.ItemPrice_Mny/b7.PackQty_Dec,4),0,b7.ItemPrice_Mny,Round(b7.ItemPrice_Mny/b7.PackQty_Dec,4)) ItemPrice
       
            FROM 
            t_aid_bih_ordergroup_detail a ,
            t_aid_bih_ordergroup b1,
            t_bse_bih_orderdic b2,
            t_aid_recipefreq b3,
            t_bse_usagetype b4,
            t_bse_bih_orderdic b5,
            t_aid_bih_ordercate b6,
            t_bse_chargeitem b7
            WHERE 
            a.groupid_chr=b1.groupid_chr(+)
            and a.orderdicid_chr=b2.orderdicid_chr(+)
            and a.freqid_chr = b3.freqid_chr(+) 
            and a.dosetype_chr=b4.usageid_chr(+)
            and a.parentid_chr = b5.orderdicid_chr(+)
            and b2.ordercateid_chr=b6.ordercateid_chr(+)
            and b2.itemid_chr=b7.itemid_chr(+)

            and a.groupid_chr = '[groupid_chr]'";
            strSQL = strSQL.Replace("[groupid_chr]", p_strGroupID.Trim());

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// 医嘱组套成员-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">医嘱组套成员流水号</param>
        /// <param name="p_dtResult">[返回DataTable out 参数] </param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupDetailByID(string p_strID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.detailid_chr,a.groupid_chr,a.orderdicid_chr,a.freqid_chr,a.dosetype_chr";
            strSQL += "     , (select b1.name_chr from T_aid_bih_ordergroup b1 where b1.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     , (select b2.name_chr from t_bse_bih_orderdic b2 where b2.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     , (select b3.freqname_chr from t_aid_recipefreq b3 where b3.freqid_chr=a.freqid_chr) FreqName";
            strSQL += "     , a.dosage_dec, a.dosageunit_chr,a.use_dec,a.useunit_chr,a.get_dec,a.getunit_chr";
            strSQL += "     , (select b4.usagename_vchr from t_bse_usagetype b4 where b4.usageid_chr=a.dosetype_chr) DosetypeName";
            strSQL += "     , a.entrust_vchr, a.isrich_int, a.parentid_chr, a.ifparentid_int";
            strSQL += "     , DECODE(a.isrich_int,1,'√',0,'×','') IsRich";
            strSQL += "     , DECODE(a.ifparentid_int,1,'√',0,'×','') IfParentName";
            strSQL += "     , (select b5.name_chr from t_bse_bih_orderdic b5 where b5.orderdicid_chr=a.parentid_chr) ParentName";
            strSQL += " FROM t_aid_bih_ordergroup_detail a";
            strSQL += " WHERE";
            strSQL += "     a.detailid_chr='" + p_strID.Trim() + "'";
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
        /// <summary>
        /// 医嘱组套成员-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">医嘱组套成员流水号</param>
        /// <param name="p_objResult">[返回VO out 参数]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderGroupDetailByID(string p_strID, out clsT_aid_bih_ordergroup_detail_VO p_objResult)
        {
            p_objResult = new clsT_aid_bih_ordergroup_detail_VO();
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.detailid_chr,a.groupid_chr,a.orderdicid_chr,a.freqid_chr,a.dosetype_chr";
            strSQL += "     , (select b1.name_chr from T_aid_bih_ordergroup b1 where b1.groupid_chr=a.groupid_chr) GroupName";
            strSQL += "     , (select b2.name_chr from t_bse_bih_orderdic b2 where b2.orderdicid_chr=a.orderdicid_chr) OrderdicName";
            strSQL += "     , (select b3.freqname_chr from t_aid_recipefreq b3 where b3.freqid_chr=a.freqid_chr) FreqName";
            strSQL += "     , a.dosage_dec, a.dosageunit_chr,a.use_dec,a.useunit_chr,a.get_dec,a.getunit_chr";
            strSQL += "     , (select b4.usagename_vchr from t_bse_usagetype b4 where b4.usageid_chr=a.dosetype_chr) DosetypeName";
            strSQL += "     , a.entrust_vchr, a.isrich_int, a.parentid_chr, a.ifparentid_int";
            strSQL += "     , DECODE(a.isrich_int,1,'√',0,'×','') IsRich";
            strSQL += "     , DECODE(a.ifparentid_int,1,'√',0,'×','') IfParentName";
            strSQL += "     , (select b5.name_chr from t_bse_bih_orderdic b5 where b5.orderdicid_chr=a.parentid_chr) ParentName";
            strSQL += " FROM t_aid_bih_ordergroup_detail a";
            strSQL += " WHERE";
            strSQL += "     a.detailid_chr='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_bih_ordergroup_detail_VO();
                    p_objResult.m_strDETAILID_CHR = dtbResult.Rows[0]["DETAILID_CHR"].ToString().Trim();
                    p_objResult.m_strGROUPID_CHR = dtbResult.Rows[0]["GROUPID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERDICID_CHR = dtbResult.Rows[0]["ORDERDICID_CHR"].ToString().Trim();
                    p_objResult.m_strFREQID_CHR = dtbResult.Rows[0]["FREQID_CHR"].ToString().Trim();
                    p_objResult.m_fltDOSAGE_DEC = float.Parse(dtbResult.Rows[0]["DOSAGE_DEC"].ToString().Trim());
                    p_objResult.m_strDOSAGEUNIT_CHR = dtbResult.Rows[0]["DOSAGEUNIT_CHR"].ToString().Trim();
                    p_objResult.m_fltUSE_DEC = float.Parse(dtbResult.Rows[0]["USE_DEC"].ToString().Trim());
                    p_objResult.m_strUSEUNIT_CHR = dtbResult.Rows[0]["USEUNIT_CHR"].ToString().Trim();
                    p_objResult.m_fltGET_DEC = float.Parse(dtbResult.Rows[0]["GET_DEC"].ToString().Trim());
                    p_objResult.m_strGETUNIT_CHR = dtbResult.Rows[0]["GETUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strDOSETYPE_CHR = dtbResult.Rows[0]["DOSETYPE_CHR"].ToString().Trim();
                    p_objResult.m_strENTRUST_VCHR = dtbResult.Rows[0]["ENTRUST_VCHR"].ToString().Trim();
                    p_objResult.m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[0]["ISRICH_INT"].ToString().Trim());
                    p_objResult.m_strPARENTID_CHR = dtbResult.Rows[0]["PARENTID_CHR"].ToString().Trim();
                    p_objResult.m_intIFPARENTID_INT = Convert.ToInt32(dtbResult.Rows[0]["IFPARENTID_INT"].ToString().Trim());
                    //非字段
                    p_objResult.m_strDosetypeName = dtbResult.Rows[0]["DosetypeName"].ToString().Trim();
                    p_objResult.m_strFreqName = dtbResult.Rows[0]["FreqName"].ToString().Trim();
                    p_objResult.m_strGroupName = dtbResult.Rows[0]["GroupName"].ToString().Trim();
                    p_objResult.m_strOrderdicName = dtbResult.Rows[0]["OrderdicName"].ToString().Trim();
                    p_objResult.m_strParentName = dtbResult.Rows[0]["ParentName"].ToString().Trim();
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
        #region 新增
        /// <summary>
        /// 新增医嘱组套成员
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号 [out 参数]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderGroupDetail(out string p_strRecordID, clsT_aid_bih_ordergroup_detail_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "DETAILID_CHR", "T_aid_bih_ordergroup_detail", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_aid_bih_ordergroup_detail (DETAILID_CHR,GROUPID_CHR,ORDERDICID_CHR,FREQID_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,USE_DEC,USEUNIT_CHR,GET_DEC,GETUNIT_CHR,DOSETYPE_CHR,ENTRUST_VCHR,ISRICH_INT,PARENTID_CHR,IFPARENTID_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(15, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strGROUPID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strORDERDICID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strFREQID_CHR;
                if (p_objRecord.m_fltDOSAGE_DEC <= 0 && (p_objRecord.m_strDOSAGEUNIT_CHR == null || p_objRecord.m_strDOSAGEUNIT_CHR.Trim() == ""))
                    objLisAddItemRefArr[4].Value = null;
                else
                    objLisAddItemRefArr[4].Value = p_objRecord.m_fltDOSAGE_DEC;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strDOSAGEUNIT_CHR;
                if (p_objRecord.m_fltUSE_DEC <= 0 && (p_objRecord.m_strUSEUNIT_CHR == null || p_objRecord.m_strUSEUNIT_CHR.Trim() == ""))
                    objLisAddItemRefArr[6].Value = null;
                else
                    objLisAddItemRefArr[6].Value = p_objRecord.m_fltUSE_DEC;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strUSEUNIT_CHR;
                if (p_objRecord.m_fltGET_DEC <= 0 && (p_objRecord.m_strGETUNIT_CHR == null || p_objRecord.m_strGETUNIT_CHR.Trim() == ""))
                    objLisAddItemRefArr[8].Value = null;
                else
                    objLisAddItemRefArr[8].Value = p_objRecord.m_fltGET_DEC;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strGETUNIT_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDOSETYPE_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strENTRUST_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intISRICH_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strPARENTID_CHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_intIFPARENTID_INT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改医嘱组套成员
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderGroupDetailByID(string p_strRecordID, clsT_aid_bih_ordergroup_detail_VO p_objRecord)
        {
            long lngRes = 0;
            //string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_AID_BIH_ORDERGROUP_DETAIL ";
            strSQL += " SET";
            strSQL += "    GROUPID_CHR = '" + p_objRecord.m_strGROUPID_CHR + "'";
            strSQL += "  , ORDERDICID_CHR = '" + p_objRecord.m_strORDERDICID_CHR + "'";
            strSQL += "  , FREQID_CHR = '" + p_objRecord.m_strFREQID_CHR + "'";

            if (p_objRecord.m_fltDOSAGE_DEC <= 0 && (p_objRecord.m_strDOSAGEUNIT_CHR == null || p_objRecord.m_strDOSAGEUNIT_CHR.Trim() == ""))
                strSQL += "  , DOSAGE_DEC = null ";
            else
                strSQL += "  , DOSAGE_DEC = " + p_objRecord.m_fltDOSAGE_DEC.ToString() + " ";
            strSQL += "  , DOSAGEUNIT_CHR = '" + p_objRecord.m_strDOSAGEUNIT_CHR + "'";

            if (p_objRecord.m_fltUSE_DEC <= 0 && (p_objRecord.m_strUSEUNIT_CHR == null || p_objRecord.m_strUSEUNIT_CHR.Trim() == ""))
                strSQL += "  , USE_DEC = null ";
            else
                strSQL += "  , USE_DEC = " + p_objRecord.m_fltUSE_DEC.ToString() + " ";
            strSQL += "  , USEUNIT_CHR = '" + p_objRecord.m_strUSEUNIT_CHR + "'";

            if (p_objRecord.m_fltGET_DEC <= 0 && (p_objRecord.m_strGETUNIT_CHR == null || p_objRecord.m_strGETUNIT_CHR.Trim() == ""))
                strSQL += "  , GET_DEC = null ";
            else
                strSQL += "  , GET_DEC = " + p_objRecord.m_fltGET_DEC.ToString() + " ";
            strSQL += "  , GETUNIT_CHR = '" + p_objRecord.m_strGETUNIT_CHR + "'";

            strSQL += "  , DOSETYPE_CHR = '" + p_objRecord.m_strDOSETYPE_CHR + "'";
            strSQL += "  , ENTRUST_VCHR = '" + p_objRecord.m_strENTRUST_VCHR + "'";
            strSQL += "  , ISRICH_INT = " + p_objRecord.m_intISRICH_INT.ToString() + " ";
            strSQL += "  , PARENTID_CHR = '" + p_objRecord.m_strPARENTID_CHR + "'";
            strSQL += "  , IFPARENTID_INT = " + p_objRecord.m_intIFPARENTID_INT.ToString() + " ";
            strSQL += " WHERE";
            strSQL += "      (DETAILID_CHR ='" + p_strRecordID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 查询医嘱组套成员	[真删除]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderGroupDetailByID(string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE FROM T_AID_BIH_ORDERGROUP_DETAIL WHERE (DETAILID_CHR ='" + p_strID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        /// 删除组套成员	[真删除]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">组套ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderGroupGroupID(string p_strGroupID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE FROM T_AID_BIH_ORDERGROUP_DETAIL WHERE (Trim(GROUPID_CHR) ='" + p_strGroupID.Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 其他
        /// <summary>
        /// 获取用药方式
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult">【返回DataTable out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoseType(out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT usageid_chr, usagename_vchr, usercode_chr FROM t_bse_usagetype";
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
        /// <summary>
        /// 获取执行频率
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult">【返回DataTable out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFreqName(out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT * FROM t_aid_recipefreq";
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
        /// <summary>
        /// 获取剂量单位、用量单位、领量单位
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderDicID">诊疗项目流水号</param>
        /// <param name="p_dtResult">【返回DataTable out参数】</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUnitNameByOrderDicID(string p_strOrderDicID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT b.ORDERDICID_CHR,a.DOSAGEUNIT_CHR ,a.ITEMIPUNIT_CHR USEUNIT_CHR,a.ITEMIPUNIT_CHR GETUNIT_CHR";
            strSQL += " FROM t_bse_chargeitem a,t_bse_bih_orderdic b";
            strSQL += " WHERE a.itemid_chr=b.itemid_chr and b.orderdicid_chr='" + p_strOrderDicID.Trim() + "'";
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

        //事务
        #region 整体保存医嘱组套成员
        /// <summary>
        /// 整体保存医嘱组套成员
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        [AutoComplete]
        public long m_lngSaveOrderGroupDetail(DataTable p_dtDataName, ref clsT_aid_bih_ordergroup_VO p_objResult, string[] strDepArr)
        {
            long lngRes = 0;
            //增|保存 医嘱组套
            string strRecordID = "";
            if (p_objResult.m_strGROUPID_CHR == null || p_objResult.m_strGROUPID_CHR == string.Empty)
            {   //增加
                lngRes = 0;
                lngRes = m_lngAddNewOrderGroup(out strRecordID, p_objResult);
                p_objResult.m_strGROUPID_CHR = strRecordID;
            }
            else
            {   //保存
                lngRes = 0;
                lngRes = m_lngModifyOrderGroupByID(p_objResult.m_strGROUPID_CHR, p_objResult);
            }

            //编辑组套成员
            clsT_aid_bih_ordergroup_detail_VO objItem = new clsT_aid_bih_ordergroup_detail_VO();
            for (int i1 = 0; i1 < p_dtDataName.Rows.Count; i1++)
            {
                SetVo(p_dtDataName.Rows[i1], out objItem);
                objItem.m_strGROUPID_CHR = p_objResult.m_strGROUPID_CHR;
                //增加
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Added)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngAddNewOrderGroupDetail(out strRecordID, objItem);
                    }
                }
                //删除
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Deleted)
                {
                    if (lngRes > 0)
                    {
                        //lngRes =0;
                        //lngRes =m_lngDeleteOrderGroupDetailByID(p_objPrincipal,objItem.m_strDETAILID_CHR);
                    }
                }
                //修改
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Modified)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyOrderGroupDetailByID(objItem.m_strDETAILID_CHR, objItem);
                    }
                }
            }
            /* @remark by xzf (05-11-04) ===========================================
             string strSQL ="delete T_AID_BIH_ORDERGROUPDEPARTMENT where GROUPID_CHR ='"+p_objResult.m_strGROUPID_CHR+"'";
			com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
			lngRes =objHRPSvc.DoExcute(strSQL);
			for(int i =0;i<strDepArr.Length;i++)
			{
				strSQL ="insert into T_AID_BIH_ORDERGROUPDEPARTMENT(GROUPID_CHR,DEPTID_CHR) values('"+p_objResult.m_strGROUPID_CHR+"','"+strDepArr[i]+"')";
				lngRes =objHRPSvc.DoExcute(strSQL);
			}
			================================================================= */

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("操作错误!"));
            }
            return lngRes;
        }
        /// <summary>
        /// 给Vo赋值
        /// </summary>
        /// <param name="drDataRow"></param>
        /// <param name="strParentID">父诊疗项目id</param>
        /// <param name="objItem"></param>
        [AutoComplete]
        private void SetVo(DataRow drDataRow, out clsT_aid_bih_ordergroup_detail_VO objItem)
        {
            objItem = new clsT_aid_bih_ordergroup_detail_VO();
            if (drDataRow["DETAILID_CHR"] != System.DBNull.Value && drDataRow["DETAILID_CHR"] != null)
                objItem.m_strDETAILID_CHR = drDataRow["DETAILID_CHR"].ToString().Trim();
            if (drDataRow["GROUPID_CHR"] != System.DBNull.Value && drDataRow["GROUPID_CHR"] != null)
                objItem.m_strGROUPID_CHR = drDataRow["GROUPID_CHR"].ToString().Trim();
            if (drDataRow["ORDERDICID_CHR"] != System.DBNull.Value && drDataRow["ORDERDICID_CHR"] != null)
                objItem.m_strORDERDICID_CHR = drDataRow["ORDERDICID_CHR"].ToString().Trim();
            if (drDataRow["FREQID_CHR"] != System.DBNull.Value && drDataRow["FREQID_CHR"] != null)
                objItem.m_strFREQID_CHR = drDataRow["FREQID_CHR"].ToString().Trim();
            if (drDataRow["DOSAGE_DEC"] != System.DBNull.Value && drDataRow["DOSAGE_DEC"] != null)
                objItem.m_fltDOSAGE_DEC = Convert.ToSingle(drDataRow["DOSAGE_DEC"].ToString());
            if (drDataRow["DOSAGEUNIT_CHR"] != System.DBNull.Value && drDataRow["DOSAGEUNIT_CHR"] != null)
                objItem.m_strDOSAGEUNIT_CHR = drDataRow["DOSAGEUNIT_CHR"].ToString().Trim();
            if (drDataRow["USE_DEC"] != System.DBNull.Value && drDataRow["USE_DEC"] != null)
                objItem.m_fltUSE_DEC = Convert.ToSingle(drDataRow["USE_DEC"].ToString());
            if (drDataRow["USEUNIT_CHR"] != System.DBNull.Value && drDataRow["USEUNIT_CHR"] != null)
                objItem.m_strUSEUNIT_CHR = drDataRow["USEUNIT_CHR"].ToString().Trim();
            if (drDataRow["GET_DEC"] != System.DBNull.Value && drDataRow["GET_DEC"] != null)
                objItem.m_fltGET_DEC = Convert.ToSingle(drDataRow["GET_DEC"].ToString());
            if (drDataRow["GETUNIT_CHR"] != System.DBNull.Value && drDataRow["GETUNIT_CHR"] != null)
                objItem.m_strGETUNIT_CHR = drDataRow["GETUNIT_CHR"].ToString().Trim();
            if (drDataRow["DOSETYPE_CHR"] != System.DBNull.Value && drDataRow["DOSETYPE_CHR"] != null)
                objItem.m_strDOSETYPE_CHR = drDataRow["DOSETYPE_CHR"].ToString().Trim();
            if (drDataRow["ENTRUST_VCHR"] != System.DBNull.Value && drDataRow["ENTRUST_VCHR"] != null)
                objItem.m_strENTRUST_VCHR = drDataRow["ENTRUST_VCHR"].ToString().Trim();
            if (drDataRow["ISRICH_INT"] != System.DBNull.Value && drDataRow["ISRICH_INT"] != null)
                objItem.m_intISRICH_INT = Convert.ToInt32(drDataRow["ISRICH_INT"].ToString());
            //父诊疗项目id
            if (drDataRow["PARENTID_CHR"] != System.DBNull.Value && drDataRow["PARENTID_CHR"] != null)
                objItem.m_strPARENTID_CHR = drDataRow["PARENTID_CHR"].ToString().Trim();
            if (drDataRow["IFPARENTID_INT"] != System.DBNull.Value && drDataRow["IFPARENTID_INT"] != null)
                objItem.m_intIFPARENTID_INT = Convert.ToInt32(drDataRow["IFPARENTID_INT"].ToString());
        }
        #endregion

        #region 整体保存医嘱组套成员（快捷方式生成）
        /// <summary>
        /// 整体保存医嘱组套成员
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        [AutoComplete]
        public long m_lngSaveOrderGroupDetailNew(clsT_aid_bih_ordergroup_VO p_objRecord, clsT_aid_bih_ordergroup_detail_VO[] m_objGroupVoList)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            string strDateTime = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                string strSQL = "";



                if (p_objRecord.m_strGROUPID_CHR.Trim().Equals(""))
                {
                    strSQL = @" select lpad(SEQ_GROUPID.Nextval,7,'0') DETAILID_CHR,sysdate CreateDate from dual ";
                    DataTable dtbResult = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    {
                        p_strRecordID = dtbResult.Rows[0]["DETAILID_CHR"].ToString().Trim();
                        strDateTime = dtbResult.Rows[0]["CreateDate"].ToString().Trim();
                    }
                    else
                    {
                        return lngRes;
                    }
                    //新增
                    strSQL = @"
                 INSERT INTO T_aid_bih_ordergroup 
                (GROUPID_CHR,NAME_CHR,DES_VCHR,CREATORID_CHR,
                 CREATEDATE_DAT,SHARETYPE_INT,WBCODE_CHR,PYCODE_CHR,
                 ISSAMERECIPENO_INT,USERCODE_VCHR,AREAID_VCHR) 
                 VALUES (
                 ?,?,?,?,
                 ?,?,?,?,
                 ?,?,?)";

                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                    //Please change the datetime and reocrdid 
                    objLisAddItemRefArr[0].Value = p_strRecordID;
                    objLisAddItemRefArr[1].Value = p_objRecord.m_strNAME_CHR;
                    objLisAddItemRefArr[2].Value = p_objRecord.m_strDES_VCHR;
                    objLisAddItemRefArr[3].Value = p_objRecord.m_strCREATORID_CHR;
                    objLisAddItemRefArr[4].Value = DateTime.Parse(strDateTime);
                    objLisAddItemRefArr[5].Value = p_objRecord.m_intSHARETYPE_INT;
                    objLisAddItemRefArr[6].Value = p_objRecord.m_strWBCODE_CHR;
                    objLisAddItemRefArr[7].Value = p_objRecord.m_strPYCODE_CHR;
                    objLisAddItemRefArr[8].Value = p_objRecord.m_intISSAMERECIPENO_INT;
                    objLisAddItemRefArr[9].Value = p_objRecord.m_strUSERCODE_VCHR;
                    objLisAddItemRefArr[10].Value = p_objRecord.m_strAREAID_VCHR;
                    long lngRecEff = -1;
                    //往表增加记录
                    lngRes = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                }
                else
                {
                    //修改
                    strSQL = @"
                 UPDATE T_aid_bih_ordergroup 
                 SET NAME_CHR=?,DES_VCHR=?,SHARETYPE_INT=?,WBCODE_CHR=?,
                 PYCODE_CHR=?,USERCODE_VCHR=?,AREAID_VCHR=?
                 WHERE GROUPID_CHR=?
                ";

                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(8, out objLisAddItemRefArr);
                    //Please change the datetime and reocrdid 
                    int n = -1;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strNAME_CHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strDES_VCHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_intSHARETYPE_INT;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strWBCODE_CHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strPYCODE_CHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strUSERCODE_VCHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strAREAID_VCHR;
                    objLisAddItemRefArr[++n].Value = p_objRecord.m_strGROUPID_CHR;
                    p_strRecordID = p_objRecord.m_strGROUPID_CHR;
                    long lngRecEff = -1;
                    //往表增加记录
                    lngRes = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                    //删除组套明细表中的相对的明细记录
                    strSQL = @"
                 delete T_aid_bih_ordergroup_detail where GROUPID_CHR='" + p_strRecordID + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }

                if (m_objGroupVoList == null)
                {
                    return lngRes;
                }
                int lenCount = m_objGroupVoList.Length;
                #region 获取医嘱组套成员单号

                string Sql = @"select seq_detailid.nextval from dual";
                DataTable dtbResult2 = new DataTable();
                for (int i = 0; i < lenCount; i++)
                {
                    objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dtbResult2);
                    m_objGroupVoList[i].m_strDETAILID_CHR = dtbResult2.Rows[0][0].ToString().PadLeft(7, '0');
                    m_objGroupVoList[i].m_strGROUPID_CHR = p_strRecordID;
                    m_objGroupVoList[i].m_strGroupName = p_objRecord.m_strNAME_CHR;
                }

                //string strSQL2 = @"  
                //        select GETSEQ('SEQ_DETAILID',[rownum]) DETAILID_CHR,sysdate CreateDate from dual ";
                //strSQL2 = strSQL2.Replace("[rownum]", lenCount.ToString());
                //DataTable dtbResult2 = new DataTable();
                //objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtbResult2);
                //int SEQ_DETAILID_CHR = 0;
                //if (lngRes > 0)
                //{
                //    SEQ_DETAILID_CHR = int.Parse(dtbResult2.Rows[0]["DETAILID_CHR"].ToString().Trim()) - lenCount;

                //}

                //for (int i = 0; i < m_objGroupVoList.Length; i++)
                //{
                //    SEQ_DETAILID_CHR++;
                //    m_objGroupVoList[i].m_strDETAILID_CHR = SEQ_DETAILID_CHR.ToString().PadLeft(7, '0');
                //    m_objGroupVoList[i].m_strGROUPID_CHR = p_strRecordID;
                //    m_objGroupVoList[i].m_strGroupName = p_objRecord.m_strNAME_CHR;

                //}
                #endregion


                strSQL = @"
               INSERT INTO T_aid_bih_ordergroup_detail
                (
                DETAILID_CHR,GROUPID_CHR,ORDERDICID_CHR,FREQID_CHR,
                DOSAGE_DEC,DOSAGEUNIT_CHR,USE_DEC,USEUNIT_CHR,
                GET_DEC,GETUNIT_CHR,DOSETYPE_CHR,ENTRUST_VCHR,
                ISRICH_INT,PARENTID_CHR,IFPARENTID_INT,EXECUTETYPE_INT,
                OUTGETMEDDAYS_INT,ATTACHTIMES_INT,SAMPLEID_VCHR,PARTID_VCHR,
                RECIPENO_INT,RATETYPE_INT,ISNEEDFEEL,NAME_VCHR,
                SINGLEAMOUNT_DEC
                ) 
               VALUES (
               ?,?,?,?,
               ?,?,?,?,
               ?,?,?,?,
               ?,?,?,?,
               ?,?,?,?,
               ?,?,?,?,
               ?)";
                DbType[] dbTypes = new DbType[] {
                       DbType.String,DbType.String,DbType.String,DbType.String,
                       DbType.Decimal,DbType.String,DbType.Decimal,DbType.String,
                       DbType.Decimal,DbType.String,DbType.String,DbType.String,
                       DbType.Int32,DbType.String,DbType.Int32,DbType.Int32,
                       DbType.Int32,DbType.Int32,DbType.String,DbType.String,
                       DbType.Int32,DbType.Int32,DbType.Int32,DbType.String,
                       DbType.Decimal};


                object[][] objValues = new object[25][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[lenCount];//初始化
                }
                for (int k1 = 0; k1 < m_objGroupVoList.Length; k1++)
                {
                    int n = -1;

                    objValues[++n][k1] = m_objGroupVoList[k1].m_strDETAILID_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strGROUPID_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strORDERDICID_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strFREQID_CHR;


                    objValues[++n][k1] = m_objGroupVoList[k1].m_fltDOSAGE_DEC;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strDOSAGEUNIT_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_fltUSE_DEC;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strUSEUNIT_CHR;


                    objValues[++n][k1] = m_objGroupVoList[k1].m_fltGET_DEC;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strGETUNIT_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strDOSETYPE_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strENTRUST_VCHR;

                    objValues[++n][k1] = m_objGroupVoList[k1].m_intISRICH_INT;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strPARENTID_CHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_intIFPARENTID_INT;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_intEXECUTETYPE_INT;


                    objValues[++n][k1] = m_objGroupVoList[k1].m_intOUTGETMEDDAYS_INT;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_intATTACHTIMES_INT;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strSAMPLEID_VCHR;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strPARTID_VCHR;

                    objValues[++n][k1] = m_objGroupVoList[k1].m_intRecipenNo;
                    objValues[++n][k1] = m_objGroupVoList[k1].RateType;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_intISNEEDFEEL;
                    objValues[++n][k1] = m_objGroupVoList[k1].m_strOrderdicName;

                    objValues[++n][k1] = m_objGroupVoList[k1].m_dmlOneUse;


                }



                if (lenCount > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region 整体删除组套
        /// <summary>
		/// 删除医嘱组套	[连成员一起删]	[事务]
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strID">流水号</param>
		/// <param name="p_objRecord"></param>
		/// <returns></returns>
		[AutoComplete]
        public long m_lngDeleteOrderGroup(string p_strID)
        {
            long lngRes = 0;
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngDeleteOrderGroupByID(p_strID);
            }
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngDeleteOrderGroupGroupID(p_strID);
            }
            if (lngRes <= 0)
            {
                throw (new Exception("删除失败!"));
            }
            return lngRes;
        }
        #endregion

        #region 保存医嘱排斥项目	[事务]
        /// <summary>
        /// 整体保存医嘱排斥项目	[事务]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        [AutoComplete]
        public long m_lngSaveOrderExclude(DataTable p_dtDataName)
        {
            long lngRes = 0;
            string strRecordID = "";
            clsT_aid_bih_orderexclude_VO objItem = new clsT_aid_bih_orderexclude_VO();
            clsT_aid_bih_orderexclude_VO objItem2 = new clsT_aid_bih_orderexclude_VO();
            for (int i1 = 0; i1 < p_dtDataName.Rows.Count; i1++)
            {
                SetOrderExclude_VO(p_dtDataName.Rows[i1], out objItem, out objItem2);
                //增加
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Added)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngAddNewOrderExclude(out strRecordID, objItem);
                    }
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngAddNewOrderExclude(out strRecordID, objItem2);
                    }
                }
                //删除	{删除已经删掉了，此处不再处理}
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Deleted)
                {
                    if (lngRes > 0)
                    {
                        //lngRes =0;
                        //lngRes =m_lngDeleteOrderExcludeByID(p_objPrincipal,objItem.m_strEXCULDEID_CHR);
                    }
                    if (lngRes > 0)
                    {
                        //lngRes =0;
                        //lngRes =m_lngDeleteOrderExcludeByID(p_objPrincipal,objItem2.m_strEXCULDEID_CHR);
                    }
                }
                //修改
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Modified)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyOrderExcludeByID(objItem.m_strEXCULDEID_CHR, objItem);
                    }
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyOrderExcludeByID(objItem.m_strEXCULDEID_CHR, objItem2);
                    }
                }
            }

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("操作错误!"));
            }
            return lngRes;
        }
        private void SetOrderExclude_VO(DataRow drDataRow, out clsT_aid_bih_orderexclude_VO objItem, out clsT_aid_bih_orderexclude_VO objItem2)
        {
            objItem = new clsT_aid_bih_orderexclude_VO();
            objItem.m_strEXCULDEID_CHR = drDataRow["exculdeid_chr"].ToString().Trim();
            objItem.m_intEXCLUDETYPE_INT = Int32.Parse(drDataRow["excludetype_int"].ToString());
            objItem.m_strORDERDICID_CHR = drDataRow["orderdicid_chr"].ToString().Trim();
            objItem.m_strGROUPID_CHR = drDataRow["groupid_chr"].ToString().Trim();
            objItem.m_strEXCULDEDICID_CHR = drDataRow["exculdedicid_chr"].ToString().Trim();
            objItem.m_strEXCLUDGROUPID_CHR = drDataRow["excludgroupid_chr"].ToString().Trim();

            objItem2 = new clsT_aid_bih_orderexclude_VO();
            objItem2.m_strEXCULDEID_CHR = objItem.m_strEXCULDEID_CHR;
            objItem2.m_intEXCLUDETYPE_INT = objItem.m_intEXCLUDETYPE_INT;
            objItem2.m_strORDERDICID_CHR = objItem.m_strEXCULDEDICID_CHR;
            objItem2.m_strGROUPID_CHR = objItem.m_strEXCLUDGROUPID_CHR;
            objItem2.m_strEXCULDEDICID_CHR = objItem.m_strORDERDICID_CHR;
            objItem2.m_strEXCLUDGROUPID_CHR = objItem.m_strGROUPID_CHR;
        }
        #endregion
        #region 新增医嘱排斥项目	[事务]
        /// <summary>
        /// 新增医嘱排斥项目	[事务]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">流水号	[out参数]</param>
        /// <param name="objItem"></param>
        [AutoComplete]
        public long m_lngAddOrderExclude(out string p_strRecordID, clsT_aid_bih_orderexclude_VO objItem)
        {
            long lngRes = 0;
            p_strRecordID = "";
            lngRes = 0;
            lngRes = m_lngAddNewOrderExclude(out p_strRecordID, objItem);
            if (lngRes > 0 && objItem.m_intEXCLUDETYPE_INT != 3)
            {
                string strRecordID = "";
                strRecordID = objItem.m_strORDERDICID_CHR;
                objItem.m_strORDERDICID_CHR = objItem.m_strEXCULDEDICID_CHR;
                objItem.m_strEXCULDEDICID_CHR = strRecordID;
                strRecordID = objItem.m_strGROUPID_CHR;
                objItem.m_strGROUPID_CHR = objItem.m_strEXCLUDGROUPID_CHR;
                objItem.m_strEXCLUDGROUPID_CHR = strRecordID;
                lngRes = 0;
                lngRes = m_lngAddNewOrderExclude(out strRecordID, objItem);
            }
            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("新增错误!"));
            }
            return lngRes;
        }
        #endregion
        #region 修改医嘱排斥项目	[事务]
        /// <summary>
        /// 修改医嘱排斥项目	[事务]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderExclude(clsT_aid_bih_orderexclude_VO objItem)
        {
            long lngRes = 0;
            clsT_aid_bih_orderexclude_VO p_objItemOld = null;
            lngRes = 0;
            lngRes = m_lngGetOrderExcludeByID(objItem.m_strEXCULDEID_CHR, out p_objItemOld);
            string strOldRecordID = "";//关联排斥ID
            #region 找到关联排斥ID
            if (lngRes > 0 && p_objItemOld != null && p_objItemOld.m_intEXCLUDETYPE_INT != 3)
            {
                string strSQL = "SELECT EXCULDEID_CHR FROM  T_AID_BIH_ORDEREXCLUDE A WHERE ";
                strSQL += "      (A.EXCLUDETYPE_INT =" + p_objItemOld.m_intEXCLUDETYPE_INT.ToString() + ")";
                strSQL += "  AND (A.ACTIVETYPE_INT =" + p_objItemOld.m_intACTIVETYPE_INT.ToString() + ")";
                if (p_objItemOld.m_strEXCULDEDICID_CHR != string.Empty)
                {
                    strSQL += "  AND (A.ORDERDICID_CHR ='" + p_objItemOld.m_strEXCULDEDICID_CHR + "')";
                }
                else
                {
                    strSQL += "  AND (A.GROUPID_CHR ='" + p_objItemOld.m_strEXCLUDGROUPID_CHR + "')";
                }
                if (p_objItemOld.m_strORDERDICID_CHR != string.Empty)
                {
                    strSQL += "  AND (A.EXCULDEDICID_CHR ='" + p_objItemOld.m_strORDERDICID_CHR + "')";
                }
                else
                {
                    strSQL += "  AND (A.EXCLUDGROUPID_CHR ='" + p_objItemOld.m_strGROUPID_CHR + "')";
                }
                try
                {
                    DataTable dtbResult = new DataTable();
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    objHRPSvc.Dispose();
                    if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    {
                        strOldRecordID = dtbResult.Rows[0][0].ToString();
                    }
                }
                catch
                { }
            }
            #endregion
            if (lngRes > 0)
            {
                //修改本排斥
                lngRes = 0;
                lngRes = m_lngModifyOrderExcludeByID(objItem.m_strEXCULDEID_CHR, objItem);
                #region 处理关联排斥
                if (lngRes > 0)
                {
                    //if(新非全排斥 and 旧非全排斥) 修改关联排斥	{找到关联排斥ID，修改关联排斥}
                    if (objItem.m_intEXCLUDETYPE_INT != 3 && p_objItemOld.m_intEXCLUDETYPE_INT != 3)
                    {
                        string strRecordID = "";
                        strRecordID = objItem.m_strORDERDICID_CHR;
                        objItem.m_strORDERDICID_CHR = objItem.m_strEXCULDEDICID_CHR;
                        objItem.m_strEXCULDEDICID_CHR = strRecordID;
                        strRecordID = objItem.m_strGROUPID_CHR;
                        objItem.m_strGROUPID_CHR = objItem.m_strEXCLUDGROUPID_CHR;
                        lngRes = 0;
                        lngRes = m_lngModifyOrderExcludeByID(strOldRecordID, objItem);
                    }
                    //if(新非全排斥 and 旧全排斥) 新增关联排斥
                    if (objItem.m_intEXCLUDETYPE_INT != 3 && p_objItemOld.m_intEXCLUDETYPE_INT == 3)
                    {
                        string strRecordID = "";
                        strRecordID = objItem.m_strORDERDICID_CHR;
                        objItem.m_strORDERDICID_CHR = objItem.m_strEXCULDEDICID_CHR;
                        objItem.m_strEXCULDEDICID_CHR = strRecordID;
                        strRecordID = objItem.m_strGROUPID_CHR;
                        objItem.m_strGROUPID_CHR = objItem.m_strEXCLUDGROUPID_CHR;
                        lngRes = 0;
                        lngRes = m_lngAddNewOrderExclude(out strOldRecordID, objItem);
                    }
                    //if(新全排斥 and 旧非全排斥) 删除关联排斥
                    if (objItem.m_intEXCLUDETYPE_INT == 3 && p_objItemOld.m_intEXCLUDETYPE_INT != 3)
                    {
                        lngRes = 0;
                        lngRes = m_lngDeleteOrderExcludeByID(strOldRecordID);
                    }
                }
                #endregion
            }

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("修改错误!"));
            }
            return lngRes;
        }
        #endregion
        #region 删除医嘱排斥项目	[事务]
        /// <summary>
        /// 医嘱排斥项目	[真删除 删除两条对应的记录] -作为事务提交
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strExculdeID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderExclude(string p_strExculdeID)
        {
            long lngRes = 0;
            clsT_aid_bih_orderexclude_VO p_objItem = null;
            lngRes = 0;
            lngRes = m_lngGetOrderExcludeByID(p_strExculdeID, out p_objItem);
            lngRes = m_lngDeleteOrderExcludeByID(p_strExculdeID);
            if (lngRes > 0 && p_objItem != null && p_objItem.m_intEXCLUDETYPE_INT != 3)
            {
                string strSQL = "DELETE FROM  T_AID_BIH_ORDEREXCLUDE A WHERE ";
                strSQL += "      (A.EXCLUDETYPE_INT =" + p_objItem.m_intEXCLUDETYPE_INT.ToString() + ")";
                strSQL += "  AND (A.ACTIVETYPE_INT =" + p_objItem.m_intACTIVETYPE_INT.ToString() + ")";
                if (p_objItem.m_strEXCULDEDICID_CHR != string.Empty)
                {
                    strSQL += "  AND (A.ORDERDICID_CHR ='" + p_objItem.m_strEXCULDEDICID_CHR + "')";
                }
                else
                {
                    strSQL += "  AND (A.GROUPID_CHR ='" + p_objItem.m_strEXCLUDGROUPID_CHR + "')";
                }
                if (p_objItem.m_strORDERDICID_CHR != string.Empty)
                {
                    strSQL += "  AND (A.EXCULDEDICID_CHR ='" + p_objItem.m_strORDERDICID_CHR + "')";
                }
                else
                {
                    strSQL += "  AND (A.EXCLUDGROUPID_CHR ='" + p_objItem.m_strGROUPID_CHR + "')";
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch
                { }
            }

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("删除错误!"));
            }
            return lngRes;
        }
        #endregion


        [AutoComplete]
        public long m_lngGetOrderByRegisterid(string registerid_chr, out DataTable m_dtChargeList)
        {
            long lngRes = -1;
            m_dtChargeList = new DataTable();
            string strSql = "";
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                //费用明细表
                strSql = @"
                        select 
                        distinct
                        c.* ,
                        a.orderid_chr,
                        a.REGISTERID_CHR,
                        a.RECIPENO_INT,
                        d.deptname_vchr,
                        f.ITEMSRCTYPE_INT,
                        f.itemcode_vchr,
                        f.ITEMSPEC_VCHR,
                        g.IPNOQTYFLAG_INT
                        from
                        t_opr_bih_order  a,
                        t_opr_bih_register b,
                        T_OPR_BIH_ORDERCHARGEDEPT c,
                        T_BSE_DeptDesc d,
                        t_bse_chargeitem f,
                        T_BSE_MEDICINE g
                        where 
                        a.orderid_chr=c.orderid_chr
                        and
                        a.registerid_chr=b.registerid_chr
                        and
                        c.CHARGEITEMID_CHR=f.ITEMID_CHR
                        and
                        f.ITEMSRCID_VCHR=g.medicineid_chr(+)
                        and
                        c.CLACAREA_CHR=d.deptid_chr(+)
                        and
                        a.registerid_chr=?
                        order by c.orderid_chr,c.SEQ_INT  ";
                System.Data.IDataParameter[] arrParams4 = null;
                HRPService.CreateDatabaseParameter(1, out arrParams4);
                arrParams4[0].Value = registerid_chr;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeList, arrParams4);


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
        public long m_lngGetHistoryPatient(string zyh, out DataTable m_dtHistoryPatient)
        {
            long lngRes = -1;
            m_dtHistoryPatient = new DataTable();
            string strSql = "";
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                strSql = @"
                select a.registerid_chr,a.inpatientid_chr,a.INPATIENTCOUNT_INT,a.INPATIENT_DAT,b.outhospital_dat from 
                t_opr_bih_register a,
                T_Opr_Bih_Leave b
                where
                a.registerid_chr=b.registerid_chr
                and
                a.STATUS_INT=1
                and
                b.status_int=1
                and
                b.pstatus_int=1
                and
                a.pstatus_int in (2,3)
                and 
                a.inpatientid_chr=?
                   
                union all
                
               select a.registerid_chr,a.inpatientid_chr,a.INPATIENTCOUNT_INT,a.INPATIENT_DAT,null outhospital_dat from 
                t_opr_bih_register a
                where
                a.STATUS_INT=1
                and
                (a.pstatus_int <>2 and  a.pstatus_int<>3)
                and 
                a.inpatientid_chr=?
                  order by INPATIENT_DAT
                         ";
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = zyh;
                arrParams[1].Value = zyh;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtHistoryPatient, arrParams);


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
        public long m_lngGetTreeOrderTable(string m_strRegisterID, out DataTable m_arrOrderExecTable, out DataTable m_arrTreeOrderTable)
        {
            long lngRes = -1;
            m_arrOrderExecTable = new DataTable();
            m_arrTreeOrderTable = new DataTable();

            string strSql = "";
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                strSql = @"
                select a.orderexecid_chr,a.orderid_chr,a.createdate_dat,a.needconfirm_int,a.confirm_dat,a.REPARE_INT,a.ISRECRUIT_INT
                from T_Opr_Bih_OrderExecute a,t_opr_bih_order b
                where a.orderid_chr=b.orderid_chr
                and
                b.registerid_chr=?
                ";
                System.Data.IDataParameter[] arrParams2 = null;
                HRPService.CreateDatabaseParameter(1, out arrParams2);
                arrParams2[0].Value = m_strRegisterID;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_arrOrderExecTable, arrParams2);

                strSql = @"
                    select 
                    a.pchargeid_chr,a.orderid_chr,a.orderexecid_chr,a.create_dat,
                    a.CHARGEITEMNAME_CHR,a.amount_dec,a.unit_vchr,a.ORDEREXECTYPE_INT,
                    a.needconfirm_int,a.CONFIRM_DAT,a.PUTMEDICINEFLAG_INT,a.TOTALMONEY_DEC,
                    a.create_dat,b.isput_int
                    from 
                    t_opr_bih_patientcharge a,T_Bih_Opr_PutMedDetail b
                    where
                    a.pchargeid_chr=b.PCHARGEID_CHR(+)
                    and
                    a.orderid_chr is not null
                    and
                    a.registerid_chr=?
                         ";
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strRegisterID;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_arrTreeOrderTable, arrParams);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        #region 医嘱打印病人查询 
        /// <summary>
        /// 医嘱类型查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// 
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPatientListPrint(string p_strFind, DateTime m_dtStart, DateTime m_dtEnd, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            //            string strSQL = @"
            //                     select '1' tag,a.registerid_chr,a.inpatientid_chr,a.INPATIENT_DAT,b.lastname_vchr,b.sex_chr,b.BIRTH_DAT,sysdate,d.deptname_vchr,e.code_chr,c.outhospital_dat from 
            //                t_opr_bih_register a,
            //                t_opr_bih_registerdetail b,
            //                T_Opr_Bih_Leave c,
            //                T_BSE_DeptDesc d,
            //                t_bse_bed e
            //                where
            //                a.registerid_chr=b.registerid_chr
            //                and
            //                a.registerid_chr=c.registerid_chr
            //                and
            //                a.areaid_chr=d.deptid_chr(+)
            //                and
            //                a.bedid_chr=e.bedid_chr(+)
            //                and
            //                a.STATUS_INT=1
            //                and
            //                c.status_int=1
            //                and
            //                c.pstatus_int=1
            //                and
            //                a.pstatus_int in (2,3)
            //                and
            //                (c.outhospital_dat>=? and c.outhospital_dat<=?)
            //                and 
            //                a.areaid_chr=?
            //                order by e.code_chr,c.outhospital_dat
            //                   ";
            string strSQL = @"select   '1' tag, a.registerid_chr, a.inpatientid_chr, a.inpatient_dat,
                                     b.lastname_vchr, b.sex_chr, b.birth_dat, sysdate, d.deptname_vchr,
                                     e.code_chr, c.outhospital_dat
                                from t_opr_bih_register a,
                                     t_opr_bih_registerdetail b,
                                     t_opr_bih_leave c,
                                     t_bse_deptdesc d,
                                     t_bse_bed e
                               where a.registerid_chr = b.registerid_chr
                                 and a.registerid_chr = c.registerid_chr
                                 and a.areaid_chr = d.deptid_chr(+)
                                 and a.bedid_chr = e.bedid_chr(+)
                                 and a.status_int = 1
                                 and c.status_int = 1
                                 and a.pstatus_int in (2, 3)
                                 and (c.outhospital_dat >= ? and c.outhospital_dat <= ?)
                                 and a.areaid_chr = ?
                            order by e.code_chr, c.outhospital_dat ";


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(3, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = m_dtStart;
                parameters[1].Value = m_dtEnd;
                parameters[2].Value = p_strFind;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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
