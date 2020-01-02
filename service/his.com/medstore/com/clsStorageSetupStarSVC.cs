using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
//using System.Windows.Forms;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsChangPrice 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageSetupStarSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsStorageSetupStarSVC()
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
        [AutoComplete]
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = @"select MEDICINEID_CHR,MEDICINENAME_VCHR,MEDSPEC_VCHR,OPUNIT_CHR,UNITPRICE_MNY,PYCODE_CHR,WBCODE_CHR from t_bse_medicine order by MEDICINEID_CHR";

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

        #region 获得仓库信息
        /// <summary>
        /// 获得仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArr(out DataTable dtStorage)
        {
            long lngRes = 0;
            dtStorage = null;
            string strSQL = @"select STORAGEID_CHR,STORAGENAME_VCHR,STORAGEGROSSPROFIT_DEC from t_bse_storage  order by STORAGEID_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorage);

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

        #region 获得仓库信息
        /// <summary>
        /// 获得仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strStorageFlag"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArr2(string strStorageFlag, out DataTable dtStorage)
        {
            dtStorage = null;
            if (strStorageFlag == "0")
            {
                return this.m_lngGetStorageArr(out dtStorage);
            }
            long lngRes = 0;
            dtStorage = null;
            string strSQL = @"select a.medstorename_vchr as storagename_vchr,a.medstoreid_chr as storageid_chr, 1 as STORAGEGROSSPROFIT_DEC from T_BSE_MEDSTORE a order by storageid_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorage);

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

        #region 获得仓库信息
        /// <summary>
        /// 获得仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strStorageFlag"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArr(string strStorageFlag, out DataTable dtStorage)
        {
            dtStorage = null;
            if (strStorageFlag == "0")
            {
                return this.m_lngGetStorageArr(out dtStorage);
            }
            long lngRes = 0;
            dtStorage = null;
            string strSQL = @"select a.medstorename_vchr as storagename_vchr,a.medstoreid_chr as storageid_chr, 1 as STORAGEGROSSPROFIT_DEC from T_BSE_MEDSTORE a order by storageid_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorage);

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

        #region 根据仓库ID获得该仓库所有药品的初始化信息
        /// <summary>
        /// 根据仓库ID获得该仓库所有药品的初始化信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID"></param>
        /// <param name="strStorageFlag">0,仓库。1，药房</param>
        /// <param name="dtStorageInit"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArrByID(string storageID, string strStorageFlag, out DataTable dtStorageInit, string strWhere)
        {
            long lngRes = 0;
            dtStorageInit = null;
            string strSQL = @"select a.*,b.MEDICINENAME_VCHR,b.ASSISTCODE_CHR,b.MEDSPEC_VCHR
							from t_bse_initstoragemedicine a,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and 
							 a.STORAGEID_CHR='" + storageID + "' and FLAG_INT =" + strStorageFlag + strWhere + " order by STORAGEID_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorageInit);

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

        #region 保存库存初始化信息
        /// <summary>
        /// 保存库存初始化信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="SaveRow"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveSetup(DataRow SaveRow)
        {
            clsMedStorageManage mange = new com.digitalwave.iCare.middletier.HIS.clsMedStorageManage();
            return mange.m_lnghMedInit(SaveRow["MEDICINEID_CHR"].ToString(), SaveRow["LOTNO_CHR"].ToString(),
                SaveRow["STORAGEID_CHR"].ToString(), double.Parse(SaveRow["QTY_DEC"].ToString()),
                SaveRow["UNITID_CHR"].ToString(), double.Parse(SaveRow["BUYPRICE_MNY"].ToString()),
                double.Parse(SaveRow["UNITPRICE_MNY"].ToString()), double.Parse(SaveRow["WHOLESALEUNITPRICE_MNY"].ToString()),
                SaveRow["USEFULLIFE_DAT"].ToString(), SaveRow["PRODUCTORID_CHR"].ToString(), SaveRow["SYSLOTNO_CHR"].ToString());
        }
        #endregion

        #region 保存库存初始化信息
        /// <summary>
        /// 保存库存初始化信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <param name="dtDel"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveSetup(DataTable dt, DataTable dtDel)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (dtDel != null)
            {
                for (int i = 0; i < dtDel.Rows.Count; i++)
                {
                    DataRow SaveRow = dtDel.Rows[i];
                    strSQL = @"Delete t_bse_initstoragemedicine where trim(SEQ_ID_CHR)='";
                    strSQL += SaveRow["SEQ_ID_CHR"] == null ? "" : SaveRow["SEQ_ID_CHR"].ToString().Trim() + "'";
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow SaveRow = dt.Rows[i];
                if (SaveRow.RowState == DataRowState.Deleted || SaveRow.RowState == DataRowState.Unchanged)
                {
                    continue;
                }
                if (SaveRow["PSTATUS_INT"].ToString().Trim() == "1")
                {
                    continue;
                }
                strSQL = @"Delete t_bse_initstoragemedicine where trim(SEQ_ID_CHR)='";
                strSQL += SaveRow["SEQ_ID_CHR"] == null ? "" : SaveRow["SEQ_ID_CHR"].ToString().Trim() + "'";

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
                string strTemp = DateTime.Now.ToString("yyMMdd");
                strSQL = "select nvl(max(a.seq_id_chr),'0000000000') as   seg_id_chr from t_bse_initstoragemedicine a where substr(a.seq_id_chr,1,6) = '" + strTemp + "'";
                System.Data.DataTable seqdt = new DataTable();
                objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref seqdt);
                long maxid = long.Parse("1" + seqdt.Rows[0]["seg_id_chr"].ToString().Substring(6, 4)) + 1;
                strTemp = strTemp + maxid.ToString("0000").Substring(1, 4);
                strSQL = @"insert into t_bse_initstoragemedicine(STORAGEID_CHR,MEDICINEID_CHR,LOTNO_CHR,UNITID_CHR,USEFULLIFE_DAT,
						PRODUCTORID_CHR,QTY_DEC,BUYPRICE_MNY,UNITPRICE_MNY,WHOLESALEUNITPRICE_MNY,FLAG_INT,PACKQTY_DEC,seq_id_chr)"
                    + "  values('" + SaveRow["STORAGEID_CHR"].ToString() + "','" + SaveRow["MEDICINEID_CHR"].ToString() + "','" + SaveRow["LOTNO_CHR"].ToString() + "','" + SaveRow["UNITID_CHR"].ToString() + "',To_Date('"
                    + SaveRow["USEFULLIFE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + SaveRow["PRODUCTORID_CHR"].ToString() + "'," + SaveRow["QTY_DEC"].ToString() + "," + SaveRow["BUYPRICE_MNY"].ToString() + ","
                    + SaveRow["UNITPRICE_MNY"].ToString() + "," + SaveRow["WHOLESALEUNITPRICE_MNY"].ToString()
                    + "," + SaveRow["FLAG_INT"].ToString() + "," + SaveRow["PACKQTY_DEC"].ToString()
                    + ",'" + strTemp + "')";
                try
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    SaveRow["seq_id_chr"] = strTemp;
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
        #endregion 保存库存初始化信息

        #region 审核库存初始化信息
        /// <summary>
        /// 审核库存初始化信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <param name="p_strStorageFlag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditInit(DataTable dt, string p_strStorageFlag)
        {
            clsMedStorageManage mange = new com.digitalwave.iCare.middletier.HIS.clsMedStorageManage();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow SaveRow = dt.Rows[i];
                if (SaveRow["PSTATUS_INT"].ToString().Trim() == "1")
                {
                    continue;
                }
                lngRes = mange.m_lnghMedInit(SaveRow["MEDICINEID_CHR"].ToString(), SaveRow["LOTNO_CHR"].ToString(),
                    SaveRow["STORAGEID_CHR"].ToString(), double.Parse(SaveRow["QTY_DEC"].ToString()),
                    SaveRow["UNITID_CHR"].ToString(), double.Parse(SaveRow["BUYPRICE_MNY"].ToString()),
                    double.Parse(SaveRow["UNITPRICE_MNY"].ToString()), double.Parse(SaveRow["WHOLESALEUNITPRICE_MNY"].ToString()),
                    SaveRow["USEFULLIFE_DAT"].ToString(), SaveRow["PRODUCTORID_CHR"].ToString(), p_strStorageFlag, "");
                if (lngRes < 0)
                {
                    return lngRes;
                }
                string strSQL = @"Update t_bse_initstoragemedicine set PSTATUS_INT = 1 where SEQ_ID_CHR='" + SaveRow["SEQ_ID_CHR"].ToString().Trim() + "'";
                objHRPSvc.DoExcute(strSQL);
                SaveRow["PSTATUS_INT"] = 1;
            }
            return lngRes;
        }
        #endregion  审核库存初始化信息

        #region 获得生产厂家
        /// <summary>
        /// 获得生产厂家
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtVerdor"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVerdorArr(out DataTable dtVerdor)
        {
            long lngRes = 0;
            dtVerdor = null;
            string strSQL = @"select VENDORID_CHR,VENDORNAME_VCHR,PYCODE_CHR,WBCODE_CHR,USERCODE_CHR from t_bse_vendor where VENDORTYPE_INT<>1 and PRODUCTTYPE_INT=1";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtVerdor);

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
