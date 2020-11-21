using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsMedAndStorage 药品与药库的关系 Create by Sam 2004-5-24
    /// </summary>

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedAndStorageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase.dll
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsMedAndStorageSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        // 添加新药品仓库对应信息
        #region 添加新药品仓库对应信息
        /// <summary>
        /// 添加新的药品和仓库对应信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objMedAndSto"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewMedicineAndStorage(clsMedicineAndStorage objMedAndSto)
        {
            long lngRes = 0;
            string strSQL = "Insert Into T_BSE_StorageAndMedicine(StorageID_CHR,MedicineID_CHR) Values " +
                " ('" + objMedAndSto.m_objStorage.m_strStroageID + "'," +
                "'" + objMedAndSto.m_objMedicine.m_strMedicineID + "')";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {

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

        // 修改药品仓库对应信息
        #region 修改药品仓库对应信息
        /// <summary>
        /// 修改药品和仓库的对应信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objMedAndSto"></param>
        /// <param name="OldMedID"></param>
        /// <param name="OldStoID"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoUpdMedicineAndStorage(clsMedicineAndStorage objMedAndSto, string OldMedID, string OldStoID)
        {
            long lngRes = 0;
            string strSQL = "UpDate T_BSE_StorageAndMedicine Set StorageID_CHR='" + objMedAndSto.m_objStorage.m_strStroageID + "'," +
                " MedicineID_CHR='" + objMedAndSto.m_objMedicine.m_strMedicineID + "'" +
                " Where StorageID_CHR='" + OldStoID + "' And MedicineID_CHR='" + OldMedID + "'";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {

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

        // 删除药品仓库对应信息
        #region 删除药品仓库对应信息
        /// <summary>
        /// 删除药品和仓库对应信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objMedAndSto"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedicineAndStorage(clsMedicineAndStorage objMedAndSto)
        {
            long lngRes = 0;
            string strSQL = "Delete T_BSE_StorageAndMedicine " +
                " Where StorageID_CHR='" + objMedAndSto.m_objStorage.m_strStroageID + "' " +
                " And MedicineID_CHR='" + objMedAndSto.m_objMedicine.m_strMedicineID + "'";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {

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

        // 通过仓库ID查找存放在此仓库的药品
        #region  通过仓库ID查找存放在此仓库的药品
        /// <summary>
        /// 通过仓库ID查找存放在此仓库的药品
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoID">仓库ID</param>
        /// <param name="p_objResultArr">输入结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllMedicineByStorageID(string StoID, out clsMedicineAndStorage[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineAndStorage[0];
            long lngRes = 0;
            string strSQL = "Select a.*,b.MEDICINENAME_VCHR,c.STORAGENAME_VCHR From T_BSE_StorageAndMedicine a,T_BSE_MEDICINE b, " +
                " T_BSE_Storage c Where a.StorageID_CHR='" + StoID + "' And " +
                " a.MedicineID_CHR=b.MedicineID_CHR(+) And " +
                " a.StorageID_CHR=c.StorageID_CHR(+) ";

            lngRes = this.m_getResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        // 通过仓库ID和药品ID查找项目
        #region 通过仓库ID和药品ID查找项目
        /// <summary>
        /// 通过仓库ID和药品ID查找信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoID">仓库ID</param>
        /// <param name="MedID">药品ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindItemByStoIDAndMedID(string StoID, string MedID, out clsMedicineAndStorage[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineAndStorage[0];
            long lngRes = 0;
            string strSQL = "Select a.*,b.MEDICINENAME_VCHR,c.STORAGENAME_VCHR From T_BSE_StorageAndMedicine a,T_BSE_MEDICINE b, " +
                "  T_BSE_Storage c Where a.StorageID_CHR='" + StoID + "' And " +
                " a.MedicineID_CHR='" + MedID + "' And " +
                " a.MedicineID_CHR=b.MedicineID_CHR(+) And " +
                " a.StorageID_CHR=c.StorageID_CHR(+) ";

            lngRes = this.m_getResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        //输出查询结果
        #region 输出结果
        /// <summary>
        /// 输出查询结果
        /// </summary>
        /// <param name="strSQL">SQL脚本</param>
        /// <param name="p_objResultArr">输入结果</param>
        /// <returns></returns>
        private long m_getResult(string strSQL, out clsMedicineAndStorage[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicineAndStorage[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineAndStorage[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicineAndStorage();
                        p_objResultArr[i1].m_objMedicine = new clsMedicine_VO();
                        p_objResultArr[i1].m_objStorage = new clsStorage_VO();
                        p_objResultArr[i1].m_objMedicine.m_strMedicineID = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicine.m_strMedicineName = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objStorage.m_strStroageID = dtbResult.Rows[i1]["STORAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objStorage.m_strStroageName = dtbResult.Rows[i1]["STORAGENAME_VCHR"].ToString().Trim();
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

        #region 取回仓库列表
        /// <summary>
        /// 取得库列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objStorage">输出结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorage(out clsStorage_VO[] objStorage)
        {
            objStorage = new clsStorage_VO[0];
            long lngRes = 0;
            string strSQL = "Select * From T_BSE_Storage";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objStorage = new clsStorage_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objStorage.Length; i1++)
                    {
                        objStorage[i1] = new clsStorage_VO();
                        objStorage[i1].m_strStroageID = dtbResult.Rows[i1]["STORAGEID_CHR"].ToString().Trim();
                        objStorage[i1].m_strStroageName = dtbResult.Rows[i1]["STORAGENAME_VCHR"].ToString().Trim();
                        objStorage[i1].m_objStroageType = new clsStorageType_VO();
                        objStorage[i1].m_objStroageType.m_strStroageTypeID = dtbResult.Rows[i1]["STORAGETYPEID_CHR"].ToString().Trim();
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

        #region 获得药品基本信息列表
        /// <summary>
        /// 获得药品基本信息列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedList(string StoID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];
            long lngRes = 0;
            string strSQL = "Select *  FROM t_bse_medicine " +
                " Where medicineid_chr not in " +
                " (Select medicineid_chr From t_bse_storageandmedicine " +
                " Where StorageID_chr='" + StoID + "')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicine_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicine_VO();
                        p_objResultArr[i1].m_strMedicineID = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedicineName = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
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

    }
}
