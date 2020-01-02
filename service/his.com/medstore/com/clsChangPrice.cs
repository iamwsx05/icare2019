using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsChangPrice 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsChangPrice : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsChangPrice()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获得所有药品资料
        /// <summary>
        /// 获得所有药品资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbMedicine"></param>
        /// <returns></returns>
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = @"select a.MEDICINEID_CHR,a.ASSISTCODE_CHR,
							a.MEDICINENAME_VCHR,a.MEDSPEC_VCHR,sum(b.amount_dec) as amount_dec,
							a.OPUNIT_CHR,a.UNITPRICE_MNY,a.PYCODE_CHR,a.WBCODE_CHR 
							from t_bse_medicine a,t_bse_storagemedicine b
							where a.medicineid_chr = b.medicineid_chr
							group by a.MEDICINEID_CHR,a.ASSISTCODE_CHR,
							a.MEDICINENAME_VCHR,a.MEDSPEC_VCHR,a.OPUNIT_CHR,
							a.UNITPRICE_MNY,a.PYCODE_CHR,a.WBCODE_CHR 
							order by ASSISTCODE_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

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

        #region 获取调价类别
        [AutoComplete]
        public long m_lngGetAppChangePriceType(out DataTable dtType)
        {
            long lngRes = 0;
            dtType = null;
            string strSQL = @"select * from t_bse_MedChageType";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtType);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion 获取调价类别 

        #region 根据ID统计库存
        /// <summary>
        /// 根据ID统计库存
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedicineID"></param>
        /// <param name="AllAmount"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCountStroage(string MedicineID, out int AllAmount)
        {
            long lngRes = 0;
            DataTable dtbAnounta = null;
            AllAmount = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select AMOUNT_DEC from t_bse_storagemedicine where MEDICINEID_CHR='" + MedicineID + "'";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbAnounta);
            if (lngRes > 0 && dtbAnounta.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbAnounta.Rows.Count; i1++)
                {
                    AllAmount += Convert.ToInt32(dtbAnounta.Rows[0]["AMOUNT_DEC"].ToString());
                }
            }
            return lngRes;
        }
        #endregion

        #region 保存调价信息
        /// <summary>
        /// 保存调价信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dsDate"></param>
        /// <param name="p_dsDel"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveChangePrice(DataSet p_dsDate, DataSet p_dsDel, out string strBillID)
        {
            long lngRes = 0;
            strBillID = "";
            clsMedStorageManage med = new clsMedStorageManage();
            try
            {
                lngRes = med.m_lngAddNewAdjustPriceInfo(p_dsDate, p_dsDel, out strBillID);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion 保存调价信息

        #region 保存调价单数据
        /// <summary>
        ///  保存调价单数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveChangPriceData(clsPriceChgeAppl objPriceChgeApplVO, clsPriceChgeApplDe[] objPriceChgeApplDe, out string strID)
        {
            string strSQL;
            strID = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            DataTable dt = new DataTable();
            strSQL = @"select * from T_OPR_MEDICINEPRICECHGAPPL where MEDICINEPRICECHGAPPLNO_CHR='" + objPriceChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0 && dt.Rows[0]["MEDICINEPRICECHGAPPLID_CHR"] != System.DBNull.Value)
            {
                return -2;
            }
            objHRPSvc.lngGenerateID(10, "MEDICINEPRICECHGAPPLID_CHR", "t_opr_medicinepricechgappl", out strID);
            strSQL = @"insert into T_OPR_MEDICINEPRICECHGAPPL(MEDICINEPRICECHGAPPLID_CHR,MEDICINEPRICECHGAPPLNO_CHR,APPLDATE_DAT,PSTATUS_INT,MEMO_VCHR,CREATORID_CHR,CREATEDATE_DAT,PERIODID_CHR)" +
                    " values('" + strID + "','" + objPriceChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR + "',To_Date('" + objPriceChgeApplVO.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),1,'" + objPriceChgeApplVO.m_strMEMO_VCHR + "','" +
                    objPriceChgeApplVO.m_strCREATORID_CHR + "',To_Date('" + objPriceChgeApplVO.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + objPriceChgeApplVO.m_strPERIODID_CHR + "')";
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
            string strDEID;
            for (int i1 = 0; i1 < objPriceChgeApplDe.Length; i1++)
            {
                objHRPSvc.lngGenerateID(10, "MEDICINEPRICECHGAPPLDEID_CHR", "T_OPR_MEDICINEPRICECHGAPPLDE", out strDEID);
                strSQL = @"insert into T_OPR_MEDICINEPRICECHGAPPLDE(MEDICINEPRICECHGAPPLDEID_CHR,MEDICINEPRICECHGAPPLID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,CURPRICE_MNY,CHANGEPRICE_MNY,TYPEID_CHR)"
                         + " values('" + strDEID + "','" + strID + "','" + objPriceChgeApplDe[i1].m_strROWNO_CHR + "','" + objPriceChgeApplDe[i1].m_strMEDICINEID_CHR +
                        "','" + objPriceChgeApplDe[i1].m_strUNITID_CHR + "'," + objPriceChgeApplDe[i1].m_dblCURPRICE_MNY + "," + objPriceChgeApplDe[i1].m_dblCHANGEPRICE_MNY + ",'" + objPriceChgeApplDe[i1].m_strtypeid + "')";
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
            }

            return lngRes;
        }
        #endregion


        #region 合并调价单

        [AutoComplete]
        public long m_lngUniteChangPriceData(System.Collections.Generic.List<string> ArrAppLID, System.Collections.Generic.List<string> ArrAppLNO, string strPriod, string strEmp, string newNO, out clsPriceChgeAppl objPriceChgeApplVO)
        {
            string strSQL;
            string strID = null;
            long lngRes = 0;
            objPriceChgeApplVO = new clsPriceChgeAppl();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objPriceChgeApplVO.m_strMEMO_VCHR = "此合并调价单是由：";
            for (int f2 = 0; f2 < ArrAppLNO.Count; f2++)
            {
                objPriceChgeApplVO.m_strMEMO_VCHR += ("[" + ArrAppLNO[f2] + "]");
            }
            objPriceChgeApplVO.m_strPERIODID_CHR = strPriod;
            objPriceChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR = newNO;
            objPriceChgeApplVO.m_strCREATORID_CHR = strEmp;
            objPriceChgeApplVO.m_strCREATEDATE_DAT = DateTime.Now.ToString();
            objPriceChgeApplVO.m_strMEMO_VCHR += "合并而成的！";
            objHRPSvc.lngGenerateID(10, "MEDICINEPRICECHGAPPLID_CHR", "t_opr_medicinepricechgappl", out strID);
            strSQL = @"insert into T_OPR_MEDICINEPRICECHGAPPL(MEDICINEPRICECHGAPPLID_CHR,MEDICINEPRICECHGAPPLNO_CHR,APPLDATE_DAT,PSTATUS_INT,MEMO_VCHR,CREATORID_CHR,CREATEDATE_DAT,PERIODID_CHR)" +
                " values('" + strID + "','" + objPriceChgeApplVO.m_strMEDICINEPRICECHGAPPLNO_CHR + "',To_Date('" + objPriceChgeApplVO.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),1,'" + objPriceChgeApplVO.m_strMEMO_VCHR + "','" +
                objPriceChgeApplVO.m_strCREATORID_CHR + "',To_Date('" + objPriceChgeApplVO.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + objPriceChgeApplVO.m_strPERIODID_CHR + "')";
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
            objPriceChgeApplVO.m_strMEDICINEPRICECHGAPPLID_CHR = strID;
            System.Collections.ArrayList arrMedID = new System.Collections.ArrayList();
            for (int i1 = 0; i1 < ArrAppLID.Count; i1++)
            {
                DataTable dt = new DataTable();
                strSQL = @"select MEDICINEID_CHR from T_OPR_MEDICINEPRICECHGAPPLDE where MEDICINEPRICECHGAPPLID_CHR='" + ArrAppLID[i1].ToString() + "'";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                if (dt.Rows.Count > 0)
                {
                    if (arrMedID.Count > 0)
                    {
                        for (int f2 = 0; f2 < dt.Rows.Count; f2++)
                        {
                            for (int k1 = 0; k1 < arrMedID.Count; k1++)
                            {
                                if (dt.Rows[f2]["MEDICINEID_CHR"].ToString() == arrMedID[k1].ToString())
                                {
                                    strSQL = @"delete  T_OPR_MEDICINEPRICECHGAPPLDE  where MEDICINEPRICECHGAPPLID_CHR='" + ArrAppLID[i1].ToString() + "' and MEDICINEID_CHR='" + dt.Rows[f2]["MEDICINEID_CHR"].ToString() + "'";
                                    try
                                    {
                                        lngRes = objHRPSvc.DoExcute(strSQL);
                                    }
                                    catch (Exception objEx)
                                    {
                                        string strTmp = objEx.Message;
                                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                    }
                                    break;
                                }
                                if (k1 == arrMedID.Count)
                                {
                                    arrMedID.Add(dt.Rows[f2]["MEDICINEID_CHR"].ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int f2 = 0; f2 < dt.Rows.Count; f2++)
                        {
                            arrMedID.Add(dt.Rows[f2]["MEDICINEID_CHR"].ToString());
                        }
                    }
                }

                strSQL = @"update T_OPR_MEDICINEPRICECHGAPPLDE set MEDICINEPRICECHGAPPLID_CHR='" + strID + "' where MEDICINEPRICECHGAPPLID_CHR='" + ArrAppLID[i1].ToString() + "'";
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
                strSQL = @"delete  T_OPR_MEDICINEPRICECHGAPPL  where MEDICINEPRICECHGAPPLID_CHR='" + ArrAppLID[i1].ToString() + "'";
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
            }

            return lngRes;
        }
        #endregion

        #region 审核调价单
        /// <summary>
        /// 审核调价单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        ///<param name="p_dsData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditChangePrice(clsPriceChgeAppl objApplVO, clsPriceChgeApplDe[] objApplDeArr)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            clsMedStorageManage med = new clsMedStorageManage();
            try
            {
                lngRes = med.m_lngAuditAdjustPriceInfo(objApplVO, objApplDeArr);
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

        #region 删除调价申请单
        /// <summary>
        /// 删除调价申请单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ChangPriceID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthDele(string ChangPriceID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"delete from t_opr_medicinepricechgappl where MEDICINEPRICECHGAPPLID_CHR='" + ChangPriceID + "'";
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
            strSQL = @"delete from t_opr_medicinepricechgapplde where MEDICINEPRICECHGAPPLDEID_CHR='" + ChangPriceID + "'";
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

        #region 获取所有的调价单数据
        /// <summary>
        /// 获取所有的调价单数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPriceChgeApplVO"></param>
        ///  <param name="nowPriod">财务期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllChgAppl(out clsPriceChgeAppl[] objPriceChgeApplVO, string nowPriod)
        {
            string strSQL;
            long lngRes = 0;
            objPriceChgeApplVO = new clsPriceChgeAppl[0];
            if (nowPriod == "")
                strSQL = @"select a.MEDICINEPRICECHGAPPLID_CHR,a.MEDICINEPRICECHGAPPLNO_CHR,a.APPLDATE_DAT,a.PSTATUS_INT,a.MEMO_VCHR,a.CREATORID_CHR,a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT," +
                       "b.LASTNAME_VCHR as CREATORNAME_CHR,c.LASTNAME_VCHR as ADUITEMPNAME_CHR  from  T_OPR_MEDICINEPRICECHGAPPL a,t_bse_employee b,t_bse_employee c where a.CREATORID_CHR=b.EMPID_CHR(+) and a.ADUITEMP_CHR=c.EMPID_CHR(+)";
            else
                strSQL = @"select a.MEDICINEPRICECHGAPPLID_CHR,a.MEDICINEPRICECHGAPPLNO_CHR,a.APPLDATE_DAT,a.PSTATUS_INT,a.MEMO_VCHR,a.CREATORID_CHR,a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT," +
                    "b.LASTNAME_VCHR as CREATORNAME_CHR,c.LASTNAME_VCHR as ADUITEMPNAME_CHR  from  T_OPR_MEDICINEPRICECHGAPPL a,t_bse_employee b,t_bse_employee c where a.CREATORID_CHR=b.EMPID_CHR(+) and a.ADUITEMP_CHR=c.EMPID_CHR(+) and PERIODID_CHR='" + nowPriod + "'";

            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    objPriceChgeApplVO = new clsPriceChgeAppl[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        objPriceChgeApplVO[i1] = new clsPriceChgeAppl();
                        objPriceChgeApplVO[i1].m_strMEDICINEPRICECHGAPPLNO_CHR = dtResult.Rows[i1]["MEDICINEPRICECHGAPPLNO_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_intPSTATUS_INT = Convert.ToInt16(dtResult.Rows[i1]["PSTATUS_INT"]);
                        objPriceChgeApplVO[i1].m_strADUITDATE_DAT = dtResult.Rows[i1]["ADUITDATE_DAT"].ToString();
                        objPriceChgeApplVO[i1].m_strADUITEMP_CHR = dtResult.Rows[i1]["ADUITEMP_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_strADUITEMPNAME_CHR = dtResult.Rows[i1]["ADUITEMPNAME_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_strAPPLDATE_DAT = dtResult.Rows[i1]["APPLDATE_DAT"].ToString();
                        objPriceChgeApplVO[i1].m_strCREATEDATE_DAT = dtResult.Rows[i1]["CREATEDATE_DAT"].ToString();
                        objPriceChgeApplVO[i1].m_strCREATORID_CHR = dtResult.Rows[i1]["CREATORID_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_strCREATORNAME_CHR = dtResult.Rows[i1]["CREATORNAME_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_strMEDICINEPRICECHGAPPLID_CHR = dtResult.Rows[i1]["MEDICINEPRICECHGAPPLID_CHR"].ToString();
                        objPriceChgeApplVO[i1].m_strMEMO_VCHR = dtResult.Rows[i1]["MEMO_VCHR"].ToString();
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

        #region 获取所有的调价单数据
        /// <summary>
        /// 获取所有的调价单数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ds"></param>
        ///  <param name="nowPriod">财务期</param>
        /// <returns></returns>
        public long m_lngGetAllChgAppl(out System.Data.DataSet ds, string nowPriod)
        {
            ds = null;
            clsMedStorageManage medmange = new clsMedStorageManage();
            long lngRes = medmange.m_lngGetAdjustPriceInfo("", "", nowPriod, "", "", "", "", "", "", "", "", out ds);
            return lngRes;

        }
        #endregion

        #region  通过单号ID获得调价单明细
        /// <summary>
        /// 通过单号ID获得调价单明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        /// <param name="PSTATUS_INT">1未审核，2已审核</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChgApplDe(string p_strID, out clsPriceChgeApplDe[] p_objResult, int PSTATUS_INT)
        {
            p_objResult = new clsPriceChgeApplDe[0];
            long lngRes = 0;
            string strSQL = @"SELECT medicinepricechgappldeid_chr, rowno_chr, medicineid_chr, curprice_mny,
       changeprice_mny, medicinepricechgapplid_chr, unitid_chr, typename_chr,
       medicinename_vchr, medspec_vchr, assistcode_chr,typeid_chr,CURQTY_DEC,
       NVL (qty_dec2, 0) AS qty_dec1
  FROM (SELECT a.medicinepricechgappldeid_chr, a.rowno_chr, a.medicineid_chr,
               a.curprice_mny, a.changeprice_mny,
               a.medicinepricechgapplid_chr, a.typeid_chr, a.unitid_chr,
               c.typename_chr, b.medicinename_vchr,
               b.packqty_dec, b.opchargeflg_int, b.medspec_vchr,
               b.assistcode_chr,
               a.CURQTY_DEC,
               (SELECT SUM (CURQTY_DEC)
                    FROM t_opr_storagemeddetail
                   WHERE medicineid_chr = b.medicineid_chr
                     AND FLAG_INT = 0) AS qty_dec2
          FROM t_opr_medicinepricechgapplde a,
               t_bse_medicine b,
               t_bse_medchagetype c
         WHERE a.medicineid_chr = b.medicineid_chr and  a.MEDICINEPRICECHGAPPLID_CHR = '" + p_strID + "' and a.TYPEID_CHR=c.TYPEID_CHR)";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsPriceChgeApplDe[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        p_objResult[i1] = new clsPriceChgeApplDe();
                        p_objResult[i1].m_strMEDICINEPRICECHGAPPLDEID_CHR = dtbResult.Rows[i1]["MEDICINEPRICECHGAPPLDEID_CHR"].ToString().Trim();
                        p_objResult[i1].m_strROWNO_CHR = dtbResult.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                        p_objResult[i1].m_strMEDICINENAME_CHR = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResult[i1].m_strMEDICINEID_CHR = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResult[i1].m_strUNITID_CHR = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResult[i1].m_dblCURPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["CURPRICE_MNY"].ToString().Trim());
                        p_objResult[i1].m_dblCHANGEPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["CHANGEPRICE_MNY"].ToString().Trim());
                        p_objResult[i1].m_strMEDSPEC_VCHR = dtbResult.Rows[i1]["MEDSPEC_VCHR"].ToString().Trim();
                        p_objResult[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE_CHR"].ToString().Trim();
                        if (PSTATUS_INT == 1)
                        {
                            if (dtbResult.Rows[i1]["QTY_DEC1"].ToString().Trim() != "")
                            {
                                p_objResult[i1].m_dblQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["QTY_DEC1"].ToString().Trim());
                            }
                            else
                            {
                                p_objResult[i1].m_dblQTY_DEC = 0;
                            }
                        }
                        else
                        {
                            if (dtbResult.Rows[i1]["CURQTY_DEC"].ToString().Trim() != "")
                            {
                                p_objResult[i1].m_dblQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["CURQTY_DEC"].ToString().Trim());
                            }
                            else
                            {
                                p_objResult[i1].m_dblQTY_DEC = 0;
                            }
                        }
                        p_objResult[i1].m_strtypeid = dtbResult.Rows[i1]["TYPEID_CHR"].ToString().Trim();
                        p_objResult[i1].m_strtypeName = dtbResult.Rows[i1]["TYPENAME_CHR"].ToString().Trim();
                        try
                        {
                            p_objResult[i1].m_dblODDSDE_MNY = (p_objResult[i1].m_dblCHANGEPRICE_MNY - p_objResult[i1].m_dblCURPRICE_MNY) * p_objResult[i1].m_dblQTY_DEC;
                        }
                        catch
                        {
                            p_objResult[i1].m_dblODDSDE_MNY = 0;
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

        #region 增加调价明细数据
        /// <summary>
        /// 增加调价明细数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDe(string p_strID, clsPriceChgeApplDe objPriceChgeApplDe)
        {
            long lngRes = 0;
            string strDEID;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.lngGenerateID(10, "MEDICINEPRICECHGAPPLDEID_CHR", "T_OPR_MEDICINEPRICECHGAPPLDE", out strDEID);
            string strSQL = @"insert into T_OPR_MEDICINEPRICECHGAPPLDE(MEDICINEPRICECHGAPPLDEID_CHR,MEDICINEPRICECHGAPPLID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,CURPRICE_MNY,CHANGEPRICE_MNY,TYPEID_CHR)"
                + " values('" + strDEID + "','" + p_strID + "','" + objPriceChgeApplDe.m_strROWNO_CHR + "','" + objPriceChgeApplDe.m_strMEDICINEID_CHR +
                "','" + objPriceChgeApplDe.m_strUNITID_CHR + "'," + objPriceChgeApplDe.m_dblCURPRICE_MNY + "," + objPriceChgeApplDe.m_dblCHANGEPRICE_MNY + ",'" + objPriceChgeApplDe.m_strtypeid + "')";
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

        #region 修改调价单
        /// <summary>
        /// 修改调价单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultDe"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMondifiy(clsPriceChgeApplDe p_objResultDe, clsPriceChgeAppl p_objResult)
        {
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (p_objResult != null)
            {
                strSQL = @"update T_OPR_MEDICINEPRICECHGAPPL set APPLDATE_DAT=To_Date('" + p_objResult.m_strAPPLDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),"
                    + "MEMO_VCHR='" + p_objResult.m_strMEMO_VCHR + "',CREATORID_CHR='" + p_objResult.m_strCREATORID_CHR + "',MEDICINEPRICECHGAPPLNO_CHR='" + p_objResult.m_strMEDICINEPRICECHGAPPLNO_CHR +
                    "' where MEDICINEPRICECHGAPPLID_CHR='" + p_objResult.m_strMEDICINEPRICECHGAPPLID_CHR + "'";
                try
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    if (lngRes <= 0)
                        return lngRes;
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            if (p_objResultDe != null)
            {
                strSQL = @"update T_OPR_MEDICINEPRICECHGAPPLDE set MEDICINEID_CHR='" + p_objResultDe.m_strMEDICINEID_CHR + "',UNITID_CHR='" + p_objResultDe.m_strUNITID_CHR +
                       "',CURPRICE_MNY=" + p_objResultDe.m_dblCURPRICE_MNY + ",CHANGEPRICE_MNY=" + p_objResultDe.m_dblCHANGEPRICE_MNY + ",TYPEID_CHR='" + p_objResultDe.m_strtypeid +
                      "' where MEDICINEPRICECHGAPPLDEID_CHR='" + p_objResultDe.m_strMEDICINEPRICECHGAPPLDEID_CHR + "'";
                try
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    if (lngRes <= 0)
                        return lngRes;
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion

        #region 删除调价单
        /// <summary>
        /// 删除调价单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleAppl(string strID)
        {
            long lngRes = 0;
            string strSQL = "";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            strSQL = @"delete T_OPR_MEDICINEPRICECHGAPPLDE WHERE MEDICINEPRICECHGAPPLID_CHR='" + strID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes <= 0)
                    return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes <= 0)
                return lngRes;
            strSQL = @"delete T_OPR_MEDICINEPRICECHGAPPL where MEDICINEPRICECHGAPPLID_CHR='" + strID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes <= 0)
                    return lngRes;
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

        #region 删除调价明细
        /// <summary>
        /// 删除调价明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDeById(string strID)
        {
            long lngRes = 0;
            string strSQL = "";
            strSQL = @"delete T_OPR_MEDICINEPRICECHGAPPLDE where MEDICINEPRICECHGAPPLDEID_CHR='" + strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes <= 0)
                    return lngRes;
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

        #region 审核调价单
        /// <summary>
        /// 审核调价单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ADUITEMPID"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngTrialChang(string ADUITEMPID, string strID)
        {
            long lngRes = 0;
            string strSQL = "";
            strSQL = @"select MEDICINEPRICECHGAPPLDEID_CHR,MEDICINEID_CHR,CURPRICE_MNY,CHANGEPRICE_MNY,QTY_DEC from t_opr_medicinepricechgapplde where MEDICINEPRICECHGAPPLNO_CHR='" + strID + "'";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                if (lngRes <= 0)
                    return lngRes;
                if (dtResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        int allAmount = 0;
                        m_lngCountStroage(dtResult.Rows[i1]["MEDICINEID_CHR"].ToString(), out allAmount);
                        if (allAmount != Convert.ToInt32(dtResult.Rows[i1]["QTY_DEC"]))
                        {
                            double oddse = (Convert.ToDouble(dtResult.Rows[i1]["CHANGEPRICE_MNY"]) - Convert.ToDouble(dtResult.Rows[i1]["CURPRICE_MNY"])) * allAmount;
                            strSQL = @"update t_opr_medicinepricechgapplde set QTY_DEC=" + allAmount + ",ODDSDE_MNY=" + oddse;
                            lngRes = objHRPSvc.DoExcute(strSQL);
                            if (lngRes <= 0)
                                return lngRes;
                        }
                        strSQL = @"update T_bse_medicine set UNITPRICE_MNY=" + dtResult.Rows[i1]["CHANGEPRICE_MNY"].ToString() + " where MEDICINEID_CHR='" + dtResult.Rows[i1]["MEDICINEID_CHR"].ToString() + "'";
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
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss");
            strSQL = @"update t_opr_medicinepricechgappl set PSTATUS_INT=2,ADUITEMP_CHR='" + ADUITEMPID + "',ADUITDATE_DAT=To_date('" + nowDate + "','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes <= 0)
                    return lngRes;
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

        #region 获取调价报告信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStartDate">yyyy-mm-dd</param>
        /// <param name="p_strEndDate">yyyy-mm-dd</param>
        /// <param name="p_outDatatable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChangePriceRpt(System.Collections.Generic.List<string> arrList, out DataTable p_outDatatable)
        {
            long lngRes = 0;
            p_outDatatable = null;
            string strWherePeriod = "";
            if (arrList.Count > 0)
            {
                for (int i1 = 0; i1 < arrList.Count; i1++)
                {

                    if (i1 == 0)
                    {
                        strWherePeriod += @" and (c.PERIODID_CHR='" + arrList[i1] + "'";
                    }
                    else
                    {
                        strWherePeriod += @" or c.PERIODID_CHR='" +  arrList[i1] + "'";
                    }
                }
                strWherePeriod += @" )";
            }
            string strSQL = @"select medicinetypeid_chr,premny,curmny,(curmny-premny) as balance from( SELECT   d.medicinetypeid_chr, SUM (a.CURQTY_DEC * a.CURPRICE_MNY) AS premny,
         SUM (a.CURQTY_DEC * a.CHANGEPRICE_MNY) AS curmny
    FROM T_OPR_MEDICINEPRICECHGAPPLDE a,
         t_opr_medicinepricechgappl c,
         t_bse_medicine d,
         t_aid_medicinetype e
   WHERE a.medicineid_chr = d.medicineid_chr
     AND (   d.medicinetypeid_chr = 1
          OR d.medicinetypeid_chr = 2
          OR d.medicinetypeid_chr = 3
         )
     AND c.medicinepricechgapplid_chr = a.medicinepricechgapplid_chr
     AND d.medicinetypeid_chr = e.medicinetypeid_chr
     and c.PSTATUS_INT=2
     AND c.medicinepricechgapplid_chr = a.medicinepricechgapplid_chr
     " + strWherePeriod + @"
GROUP BY d.medicinetypeid_chr)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_outDatatable);
            return lngRes;
        }
        #endregion 获取调价报告信息

        #region 获取最大单号
        /// <summary>
        /// 获取最大单号
        /// </summary>
        /// <returns>最大单号</returns>
        [AutoComplete]
        public string m_mthGetMaxNo(string strDate)
        {
            string ret = "";
            long lngRes = 0;
            string strSQL = @"SELECT MAX (medicinepricechgapplno_chr)
  FROM t_opr_medicinepricechgappl
 WHERE medicinepricechgapplno_chr like '" + strDate + "%'";
            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0][0].ToString().Trim();
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return ret;
        }
        #endregion
    }
}
