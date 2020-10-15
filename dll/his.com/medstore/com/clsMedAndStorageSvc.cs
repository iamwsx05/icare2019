using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsMedAndStorage ҩƷ��ҩ��Ĺ�ϵ Create by Sam 2004-5-24
    /// </summary>

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedAndStorageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase.dll
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsMedAndStorageSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        // �����ҩƷ�ֿ��Ӧ��Ϣ
        #region �����ҩƷ�ֿ��Ӧ��Ϣ
        /// <summary>
        /// ����µ�ҩƷ�Ͳֿ��Ӧ��Ϣ
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
                //����һ����ִ����
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

        // �޸�ҩƷ�ֿ��Ӧ��Ϣ
        #region �޸�ҩƷ�ֿ��Ӧ��Ϣ
        /// <summary>
        /// �޸�ҩƷ�Ͳֿ�Ķ�Ӧ��Ϣ
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
                //����һ����ִ����
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

        // ɾ��ҩƷ�ֿ��Ӧ��Ϣ
        #region ɾ��ҩƷ�ֿ��Ӧ��Ϣ
        /// <summary>
        /// ɾ��ҩƷ�Ͳֿ��Ӧ��Ϣ
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
                //����һ����ִ����
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

        // ͨ���ֿ�ID���Ҵ���ڴ˲ֿ��ҩƷ
        #region  ͨ���ֿ�ID���Ҵ���ڴ˲ֿ��ҩƷ
        /// <summary>
        /// ͨ���ֿ�ID���Ҵ���ڴ˲ֿ��ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoID">�ֿ�ID</param>
        /// <param name="p_objResultArr">������</param>
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

        // ͨ���ֿ�ID��ҩƷID������Ŀ
        #region ͨ���ֿ�ID��ҩƷID������Ŀ
        /// <summary>
        /// ͨ���ֿ�ID��ҩƷID������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoID">�ֿ�ID</param>
        /// <param name="MedID">ҩƷID</param>
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

        //�����ѯ���
        #region ������
        /// <summary>
        /// �����ѯ���
        /// </summary>
        /// <param name="strSQL">SQL�ű�</param>
        /// <param name="p_objResultArr">������</param>
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

        #region ȡ�زֿ��б�
        /// <summary>
        /// ȡ�ÿ��б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objStorage">������</param>
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

        #region ���ҩƷ������Ϣ�б�
        /// <summary>
        /// ���ҩƷ������Ϣ�б�
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
