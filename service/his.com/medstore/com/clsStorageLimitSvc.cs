using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsStorageMedLimitSvc:仓库药品限额
    /// 作者：Sam
    /// 时间：2004-5-25
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageLimitSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 新增一条记录
        /// <summary>
        /// 新增药品限额
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewLimit(DataRow dtbRow)
        {
            long lngRes = 0;

            string strSQL = "Insert Into t_bse_storagemedlimit (Lowlimit_dec,Highlimit_dec,Planqty_dec,Unitid_chr," +
                "Planpercent_dec,Storageid_chr,Medicineid_chr) Values " +
                "('" + dtbRow["LOWLIMIT_DEC"].ToString().Trim() + "','" + dtbRow["HIGHLIMIT_DEC"].ToString().Trim() + "','" + dtbRow["PLANQTY_DEC"].ToString().Trim() + "', " +
                "'" + dtbRow["UNITID_CHR"].ToString().Trim() + "','" + dtbRow["PLANPERCENT_DEC"].ToString().Trim() + "','" + dtbRow["STORAGEID_CHR"].ToString().Trim() + "', " +
                "'" + dtbRow["MEDICINEID_CHR"].ToString().Trim() + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);
                HRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 修改药品限额
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbRow"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdLimitByKey(DataRow dtbRow)
        {
            long lngRes = 0;
            if (dtbRow["HIGHLIMIT_DEC"] == System.DBNull.Value)
            {
                dtbRow["HIGHLIMIT_DEC"] = 0;
            }
            if (dtbRow["LOWLIMIT_DEC"] == System.DBNull.Value)
            {
                dtbRow["LOWLIMIT_DEC"] = 0;
            }
            string strSQL = "Update t_bse_storagemedlimit Set Lowlimit_dec='" + dtbRow["LOWLIMIT_DEC"] + "', " +
                "Highlimit_dec='" + dtbRow["HIGHLIMIT_DEC"] + "'," +
                "Planqty_dec='" + dtbRow["PLANQTY_DEC"] + "', " +
                "Unitid_chr='" + dtbRow["UNITID_CHR"] + "'," +
                "Planpercent_dec='" + dtbRow["PLANPERCENT_DEC"] + "', " +
                "Storageid_chr='" + dtbRow["STORAGEID_CHR"] + "'," +
                "Medicineid_chr='" + dtbRow["MEDICINEID_CHR"] + "'" +
                " Where Storageid_chr='" + dtbRow["STORAGEID_CHR"] + "' And" +
                " Medicineid_chr='" + dtbRow["MEDICINEID_CHR"] + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                long count = 0;
                lngRes = HRPSvc.DoExcuteForDelete(strSQL, ref count);
                if (count == 0)
                {
                    m_lngDoAddNewLimit(dtbRow);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 删除药品限额
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedID"></param>
        /// <param name="strStorageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteLimitByKey(string strStorageID, string strMedID)
        {
            long lngRes = 0;
            string strSQL = "Delete t_bse_storagemedlimit where STORAGEID_CHR='" + strStorageID + "' and  MEDICINEID_CHR='" + strMedID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);
                HRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 根据仓库ID查询药品限额
        /// <summary>
        /// 根据仓库ID查询药品限额
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindLimitByStoID(string strStoID, string p_strStorageFlag, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = @"Select  a.STORAGEID_CHR,a.MEDICINEID_CHR,b.MEDICINENAME_VCHR,b.PRODUCTORID_CHR,b.medspec_vchr,b.ASSISTCODE_CHR,b.PYCODE_CHR,b.WBCODE_CHR,c.LOWLIMIT_DEC,c.HIGHLIMIT_DEC,c.PLANQTY_DEC,b.OPUNIT_CHR as UNITID_CHR,c.PLANPERCENT_DEC  from T_BSE_storageandmedicine a left join  t_bse_storagemedlimit c on c.MEDICINEID_CHR=a.MEDICINEID_CHR and  c.STORAGEID_CHR=a.STORAGEID_CHR ,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR  and a.STORAGEID_CHR='" + strStoID + "  ' order by b.ASSISTCODE_CHR ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

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

        #region 获取所有的仓库信息
        [AutoComplete]
        public long m_lngGetStorageList(out DataTable dtResult2)
        {
            long lngRes = 0;
            dtResult2 = null;
            string strSQL = @"select STORAGEID_CHR,STORAGENAME_VCHR,STORAGETYPEID_CHR from T_BSE_STORAGE";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    dtResult2 = dtbResult;
                    //strStorageArr = new string[dtbResult.Rows.Count, 3];
                    //for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    //{
                    //    strStorageArr[i1, 0] = dtbResult.Rows[i1][0].ToString();
                    //    strStorageArr[i1, 1] = dtbResult.Rows[i1][1].ToString();
                    //    strStorageArr[i1, 2] = dtbResult.Rows[i1][2].ToString();
                    //}
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
        
        #region 根据类型获取药品基本资料
        [AutoComplete]
        public long m_lngGetMedListByType(string strMedTypeID, out DataTable dbtMedArr)
        {
            long lngRes = 0;
            dbtMedArr = null;
            string strSQL = @"select a.MEDICINEID_CHR,b.MEDICINENAME_VCHR,b.OPUNIT_CHR  from t_bse_storageandmedicine a, T_BSE_MEDICINE b WHERE a.MEDICINEID_CHR=b.MEDICINEID_CHR and  a.STORAGEID_CHR='" + strMedTypeID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dbtMedArr);

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

        #region 获取所有的仓库类型信息
        [AutoComplete]
        public long m_lngGetStorageTypeList(out DataTable dtResult2)
        {
            long lngRes = 0;
            dtResult2 = null;
            string strSQL = @"select STORAGETYPEID_CHR,STORAGETYPENAME_VCHR from T_AID_STORAGETYPE";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    dtResult2 = dtbResult;
                    //strStorageTypeArr = new string[dtbResult.Rows.Count, 2];
                    //for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    //{
                    //    strStorageTypeArr[i1, 0] = dtbResult.Rows[i1][0].ToString();
                    //    strStorageTypeArr[i1, 1] = dtbResult.Rows[i1][1].ToString();
                    //}
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

        #region 获取仓库警戒报告
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedWatchRpt(string p_strStorageID, out System.Data.DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 1;
            string strSQL = @"select a.lowlimit_dec,a.highlimit_dec,a.unitid_chr,c.medspec_vchr,a.storageid_chr,a.medicineid_chr,b.storagename_vchr,d.amount_dec as curqty_dec,c.medicinename_vchr 
					from t_bse_storagemedlimit a,t_bse_storage b,t_bse_medicine c,t_bse_storagemedicine d 
					where a.medicineid_chr = c.medicineid_chr 
					and a.medicineid_chr = c.medicineid_chr 
					and a.storageid_chr = b.storageid_chr(+) 
					and a.medicineid_chr = d.medicineid_chr 
					 and a.lowlimit_dec >= d.amount_dec 
					and a.storageid_chr = '" + p_strStorageID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion 获取仓库警戒报告
    }

}
