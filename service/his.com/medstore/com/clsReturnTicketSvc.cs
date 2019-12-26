using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsReturnTicketSvc 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsReturnTicketSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase
    {
        /// <summary>
        /// clsReturnTicketSvc 的摘要说明。
        /// </summary>
        public clsReturnTicketSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        #region 根据窗己发药及已退药的数据
        /// <summary>
        /// 根据窗己发药及已退药的数据
        /// </summary>
        [AutoComplete]
        public long m_ingGetOutDataByWindowID(string p_strID, out clsOutPatienTrecipEinv_VO[] p_objResultArr, DateTime starDate)
        {
            p_objResultArr = new clsOutPatienTrecipEinv_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.*, b.lastname_vchr,c.pstauts_int,d.PATIENTCARDID_CHR
  FROM t_opr_outpatientrecipeinv a, t_bse_employee b,T_OPR_OUTPATIENTRECIPE c,T_OPR_MEDRECIPESEND d,t_bse_patientcard d
 WHERE a.status_int != 2
    and a.OUTPATRECIPEID_CHR=c.outpatrecipeid_chr
	and c.PATIENTID_CHR=d.PATIENTID_CHR
   AND a.opremp_chr = b.empid_chr(+) and a.OUTPATRECIPEID_CHR=d.OUTPATRECIPEID_CHR and 
  d.PSTATUS_INT=2 AND d.WINDOWID_CHR='" + p_strID + "' and a.INVDATE_DAT between to_date('" + starDate.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + starDate.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsOutPatienTrecipEinv_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsOutPatienTrecipEinv_VO();
                        p_objResultArr[i1].m_strINVOICENO_VCHR = dtbResult.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOUTPATRECIPEID_CHR = dtbResult.Rows[i1]["OUTPATRECIPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["INVDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        if (dtbResult.Rows[i1]["ACCTSUM_MNY"].ToString().Trim() != "" && dtbResult.Rows[i1]["ACCTSUM_MNY"].ToString().Trim() != null)
                            p_objResultArr[i1].m_dblACCTSUM_MNY = Convert.ToDouble(dtbResult.Rows[i1]["ACCTSUM_MNY"].ToString().Trim());
                        else
                            p_objResultArr[i1].m_dblACCTSUM_MNY = 0;
                        if (dtbResult.Rows[i1]["SBSUM_MNY"].ToString().Trim() != "" && dtbResult.Rows[i1]["SBSUM_MNY"].ToString().Trim() != null)
                            p_objResultArr[i1].m_dblSBSUM_MNY = Convert.ToDouble(dtbResult.Rows[i1]["SBSUM_MNY"].ToString().Trim());
                        else
                            p_objResultArr[i1].m_dblSBSUM_MNY = Convert.ToDouble(dtbResult.Rows[i1]["SBSUM_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_strOPREMP_CHR = dtbResult.Rows[i1]["OPREMP_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPREMPNAME_CHR = dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_strRECORDEMP_CHR = dtbResult.Rows[i1]["RECORDEMP_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTCARDID_CHR = dtbResult.Rows[i1]["PATIENTCARDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRECORDDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["RECORDDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        if (dtbResult.Rows[i1]["pstauts_int"].ToString().Trim() == "4")
                            p_objResultArr[i1].m_intSTATUS_INT = 2;
                        else
                            p_objResultArr[i1].m_intSTATUS_INT = 1;
                        p_objResultArr[i1].m_strSEQID_CHR = dtbResult.Rows[i1]["SEQID_CHR"].ToString().Trim();
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

        #region 查找数据
        /// <summary>
        /// 查找数据。
        /// </summary>
        [AutoComplete]
        public long m_ingfFidData(string p_strID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new DataTable();
            string strSQL = @"SELECT a.*,b.lastname_vchr  FROM T_OPR_OUTPATIENTRECIPEINV a,t_bse_employee b WHERE a.OPREMP_CHR=b.EMPID_CHR(+) and OUTPATRECIPEID_CHR IN (SELECT OUTPATRECIPEID_CHR FROM T_OPR_MEDRECIPESEND WHERE PSTATUS_INT=2 AND WINDOWID_CHR='" + p_strID + "')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_objResultArr);

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

        #region 根据药房查询所有的窗口
        /// <summary>
        /// 根据药房查询所有的窗口
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreWinList(string medID, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            string strSQL = @" and a.MEDSTOREID_CHR='" + medID + "'";
            lngRes = m_lngGetMedStoreWinByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 查询所有药房信息
        /// <summary>
        /// 查询所有药房信息
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreList(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            string strSQL = " ";
            lngRes = m_lngGetMedStoreByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 模糊查询药房窗口信息
        /// <summary>
        /// 模糊查询药房窗口信息
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreWinByAny(string p_strSQL, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT a.*, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int
							    FROM t_bse_medstorewin a, t_bse_medstore b
							   WHERE a.medstoreid_chr = b.medstoreid_chr 
							" + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsOPMedStoreWin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsOPMedStoreWin_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_strWindowID = dtbResult.Rows[i]["windowid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strWindowName = dtbResult.Rows[i]["windowname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());

                        }
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

        #region 模糊查询药房信息
        /// <summary>
        /// 模糊查询药房信息
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreByAny(string p_strSQL, out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM t_bse_medstore
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStore_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStore_VO();
                            p_objResultArr[i].m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                        }
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

        #region 获得退药处方
        /// <summary>
        /// 获得病人处方
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strRegID">处方ID</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMainRecipe(string p_strRegID, out clsOutpatientRecipe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOutpatientRecipe_VO[0];

            DataTable dtResult = new DataTable();
            string strSQL = @"SELECT a.*, b.name_vchr, c.lastname_vchr, d.deptname_vchr,
								   e.lastname_vchr AS recordemp
							  FROM t_opr_outpatientrecipe a,
								   t_bse_patientidx b,
								   t_bse_employee c,
								   t_bse_deptbaseinfo d,
								   t_bse_employee e
							 WHERE a.patientid_chr = b.patientid_chr(+)
							   AND a.diagdr_chr = c.empid_chr(+)
							   AND a.diagdept_chr = d.deptid_chr(+)
							   AND a.recordemp_chr = e.empid_chr(+)
							";
            strSQL += @" AND a.OUTPATRECIPEID_CHR = '" + p_strRegID.Trim() + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);



                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsOutpatientRecipe_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsOutpatientRecipe_VO();
                        p_objResultArr[i1].m_strOutpatRecipeID = dtResult.Rows[i1]["outpatrecipeid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strOutpatRecipeNo = dtResult.Rows[i1]["outpatrecipeno_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_strRecordDate = dtResult.Rows[i1]["recorddate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_strRegisterID = dtResult.Rows[i1]["registerid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateDate = dtResult.Rows[i1]["createdate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp = new clsEmployeeVO();
                        p_objResultArr[i1].m_objRecordEmp.strEmpID = dtResult.Rows[i1]["recordemp_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp.strLastName = dtResult.Rows[i1]["recordemp"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient = new clsPatientVO();
                        p_objResultArr[i1].m_objPatient.strPatientID = dtResult.Rows[i1]["patientid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.strName = dtResult.Rows[i1]["name_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr = new clsEmployeeVO();
                        p_objResultArr[i1].m_objDiagDr.strEmpID = dtResult.Rows[i1]["diagdr_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr.strLastName = dtResult.Rows[i1]["lastname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept = new clsDepartmentVO();
                        p_objResultArr[i1].m_objDiagDept.strDeptID = dtResult.Rows[i1]["diagdept_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept.strDeptName = dtResult.Rows[i1]["deptname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_intPStatus = dtResult.Rows[i1]["pstauts_int"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region 通过处方ID获取处方明细
        /// <summary>
        /// 通过处方ID获取处方明细
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strOPRecID">处方ID</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPRecipeListByWinAndOpRecAndType(string p_strOPRecID, string typeID, out clsOprecipeItemDe[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsOprecipeItemDe[0];

            DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT c.windowid_chr, c.outpatrecipeid_chr, c.recipetype_int, a.itemid_chr,
       a.qty_dec, a.unitid_chr, a.price_mny, a.tolprice_mny, a.discount_dec,
       b.itemname_vchr, b.itemcode_vchr
  FROM t_opr_oprecipeitemde a,
       t_bse_chargeitem b,
       t_opr_medrecipesend c
 WHERE a.outpatrecipeid_chr = c.outpatrecipeid_chr(+)
   AND a.recipetype_int = c.recipetype_int(+)
   AND a.itemid_chr = b.itemid_chr(+)  and a.outpatrecipeid_chr = '" + p_strOPRecID.Trim() + "' and a.RECIPETYPE_INT='" + typeID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);



                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsOprecipeItemDe[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsOprecipeItemDe();
                            p_objResultArr[i].m_objItem = new clsChargeItem_VO();
                            p_objResultArr[i].m_objUnit = new clsUnit_VO();
                            p_objResultArr[i].m_strOutpatRecipeID = dtbResult.Rows[i]["outpatrecipeid_chr"].ToString().Trim();
                            if (dtbResult.Rows[i]["recipetype_int"].ToString().Trim() != "" && dtbResult.Rows[i]["recipetype_int"].ToString().Trim() != null)
                                p_objResultArr[i].m_intRecipeType = Convert.ToInt32(dtbResult.Rows[i]["recipetype_int"].ToString().Trim());
                            else
                                p_objResultArr[i].m_intRecipeType = 6;
                            p_objResultArr[i].m_objItem.m_strItemID = dtbResult.Rows[i]["itemid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objItem.m_strItemName = dtbResult.Rows[i]["itemname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objItem.m_strItemCode = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                            if (dtbResult.Rows[i]["qty_dec"].ToString().Trim() != "" && dtbResult.Rows[i]["qty_dec"].ToString().Trim() != null)
                                p_objResultArr[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty_dec"].ToString().Trim());
                            else
                                p_objResultArr[i].m_decQty = 0;
                            if (dtbResult.Rows[i]["price_mny"].ToString().Trim() != "" && dtbResult.Rows[i]["price_mny"].ToString().Trim() != null)
                                p_objResultArr[i].m_decPrice = Convert.ToDecimal(dtbResult.Rows[i]["price_mny"].ToString().Trim());
                            else
                                p_objResultArr[i].m_decPrice = 0;
                            if (dtbResult.Rows[i]["discount_dec"].ToString().Trim() != "" && dtbResult.Rows[i]["discount_dec"].ToString().Trim() != null)
                                p_objResultArr[i].m_decDiscount = Convert.ToDecimal(dtbResult.Rows[i]["discount_dec"].ToString().Trim());
                            else
                                p_objResultArr[i].m_decDiscount = 1;
                            if (dtbResult.Rows[i]["tolprice_mny"].ToString().Trim() != "" && dtbResult.Rows[i]["tolprice_mny"].ToString().Trim() != null)
                                p_objResultArr[i].m_decTolPrice = Convert.ToDecimal(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                            else
                                p_objResultArr[i].m_decTolPrice = 0;
                        }
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

        #region 把门诊处方发票表的状态改为“退票”
        [AutoComplete]
        public long m_lngChangStatus(string p_Invoiceno)
        {
            long lngRes = 0;
            string strSQL = "update T_OPR_OUTPATIENTRECIPEINV SET STATUS_INT=2 WHERE INVOICENO_VCHR='" + p_Invoiceno + "'";

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngRes = objHRP.DoExcute(strSQL);
                objHRP.Dispose();
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

        #region 把门诊处方记录的状态改为“退药”
        [AutoComplete]
        public long m_lngReturn(string p_Invoiceno)
        {
            long lngRes = 0;
            string strSQL = "update T_OPR_OUTPATIENTRECIPE SET PSTAUTS_INT=4 WHERE OUTPATRECIPEID_CHR='" + p_Invoiceno + "'";

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngRes = objHRP.DoExcute(strSQL);
                objHRP.Dispose();
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

        #region  插入西药（成药）处方明细
        [AutoComplete]
        public long m_lngAddNewPwmreciPeDe(clsOutPaticntPwmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_OPR_OUTPATIENTPWMRECIPEDE (OUTPATRECIPEID_CHR,ROWNO_CHR,ITEMID_CHR,UNITID_CHR,TOLQTY_DEC,UNITPRICE_MNY,TOLPRICE_MNY,OUTPATRECIPEDEID_CHR,RETURN_DAT,RETURNEMP_CHR) VALUES ('" + p_objRecord.m_strOUTPATRECIPEID_CHR + "','" + p_objRecord.m_strROWNO_CHR + "','" + p_objRecord.m_strITEMID_CHR + "','" + p_objRecord.m_strUNITID_CHR + "'," + p_objRecord.m_dblTOLQTY_DEC + "," + p_objRecord.m_dblUNITPRICE_MNY + "," + p_objRecord.m_dblTOLPRICE_MNY + ",'" + p_objRecord.m_strOUTPATRECIPEDEID_CHR + "',TO_DATE('" + p_objRecord.m_strRETURN_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objRecord.m_strRETURNEMP_CHR + "')";
            try
            {
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

        #region 插入中药处方明细
        [AutoComplete]
        public long m_lngAddNewCmreciPeDe(clsOutPaticntCmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "REPORT_ID_CHR", "T_OPR_OUTPATIENTCMRECIPEDE", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_OPR_OUTPATIENTCMRECIPEDE (OUTPATRECIPEID_CHR,ROWNO_CHR,ITEMID_CHR,UNITID_CHR,TOLQTY_DEC,UNITPRICE_MNY,TOLPRICE_MNY,OUTPATRECIPEDEID_CHR,RETURN_DAT,RETURNEMP_CHR) VALUES ('" + p_objRecord.m_strOUTPATRECIPEID_CHR + "','" + p_objRecord.m_strROWNO_CHR + "','" + p_objRecord.m_strITEMID_CHR + "','" + p_objRecord.m_strUNITID_CHR + "'," + p_objRecord.m_dblQTY_DEC + "," + p_objRecord.m_dblUNITPRICE_MNY + "," + p_objRecord.m_dblTOLPRICE_MNY + ",'" + p_objRecord.m_strOUTPATRECIPEDEID_CHR + "',TO_DATE('" + p_objRecord.m_strRETURN_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objRecord.m_strRETURNEMP_CHR + "')";
            try
            {
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
    }
}
