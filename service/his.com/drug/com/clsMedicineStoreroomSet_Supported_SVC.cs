using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.MedicineStoreService
{
    /// <summary>
    /// �ⷿ����
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsMedicineStoreroomSet_Supported_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ȡ�ֿ�������Ϣ.
        /// <summary>
        /// ��ȡ�ֿ�������Ϣ.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedicineStoreroomInfoArr">���ؽ��.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineStoreroomInfo( out clsMS_MedicineStoreroom_VO[] p_objMedicineStoreroomInfoArr)
        {
            p_objMedicineStoreroomInfoArr = null;
            DataTable m_dtbMedicineStoreroom = null;
            long lngRes = 0;

            try
            {
                //+shortname 2008.5.14 wuchongkun
                string strSQL = @"select distinct t.medicineroomid   medicineroomid,
                t.medicineroomname medicineroomname,
                t.deptid_chr,t.shortname_chr,s.deptname_vchr
  from t_ms_medicinestoreroomset t
	left join t_bse_deptdesc s on s.deptid_chr = t.deptid_chr
 order by t.medicineroomid";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref m_dtbMedicineStoreroom);
                objHRPServ.Dispose();
                objHRPServ = null;
                int index = 0;
                if (m_dtbMedicineStoreroom.Rows.Count > 0)
                {
                    p_objMedicineStoreroomInfoArr = new clsMS_MedicineStoreroom_VO[m_dtbMedicineStoreroom.Rows.Count];

                    for (; index < m_dtbMedicineStoreroom.Rows.Count; index++)
                    {
                        p_objMedicineStoreroomInfoArr[index] = new clsMS_MedicineStoreroom_VO();
                        p_objMedicineStoreroomInfoArr[index].m_strMedicineRoomID_VCHR = m_dtbMedicineStoreroom.Rows[index]["medicineroomid"] as string;
                        p_objMedicineStoreroomInfoArr[index].m_strMedicineRoomName_VCHR = m_dtbMedicineStoreroom.Rows[index]["medicineroomname"] as string;
                        //+2008.5.14wuchongkun
                        p_objMedicineStoreroomInfoArr[index].m_strMidicineRommShortName_CHR = m_dtbMedicineStoreroom.Rows[index]["shortname_chr"] as string;
                        p_objMedicineStoreroomInfoArr[index].m_strMedicineTypeID_CHR = null;
                        p_objMedicineStoreroomInfoArr[index].m_strDEPTID_CHR = m_dtbMedicineStoreroom.Rows[index]["deptid_chr"] as string;
                        p_objMedicineStoreroomInfoArr[index].m_strDEPTNAME_CHR = m_dtbMedicineStoreroom.Rows[index]["deptname_vchr"] as string;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ������Ϣ.
        /// <summary>
        /// ��ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedicineTypeArr">���ؽ��.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineInfo( out clsMS_MedicineType_VO[] p_objMedicineTypeArr)
        {
            p_objMedicineTypeArr = null;
            DataTable m_dtbMedicineType = new DataTable();
            long lngRes = 0;

            try
            {
                string strSQL = @"select t.medicinetypeid_chr    medicinetypeid,
       t.medicinetypename_vchr medicinetypename
  from t_aid_medicinetype t";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref m_dtbMedicineType);
                objHRPServ.Dispose();
                objHRPServ = null;
                int currentRow = 0;
                clsMS_MedicineType_VO[] m_objMedicineTypeArr = new clsMS_MedicineType_VO[m_dtbMedicineType.Rows.Count];
                foreach (DataRow dr in m_dtbMedicineType.Rows)
                {
                    m_objMedicineTypeArr[currentRow] = new clsMS_MedicineType_VO();
                    m_objMedicineTypeArr[currentRow].m_strMedicineTypeID_CHR = dr["MedicineTypeID"] as string;
                    m_objMedicineTypeArr[currentRow].m_strMedicineTypeName_VCHR = dr["MedicineTypeName"] as string;
                    currentRow++;
                }
                p_objMedicineTypeArr = m_objMedicineTypeArr;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    
        #region ��ѯ�ⷿҩƷ��Ϣ.

        /// <summary>
        /// ��ѯ�ⷿҩƷ��Ϣ.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedicineStoreroomID">��ѯ����.</param>
        /// <param name="strMedicineNameArr">���ؽ��.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSelectMedicineName( string strMedicineStoreroomID, out string[] strMedicineNameArr)
        {
            strMedicineNameArr = null;
            long lngRes = 0;
            DataTable dtbMedicineName = new DataTable();
            try
            {
                string strSQL = @"select a.medicinetypename_vchr MedicineTypeName
  from t_ms_medicinestoreroomset t, t_aid_medicinetype a
 where t.medicinetypeid_chr = a.medicinetypeid_chr
   and t.medicineroomid = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strMedicineStoreroomID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbMedicineName, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                strMedicineNameArr = new string[dtbMedicineName.Rows.Count];
                int currentRow = 0;
                for (; currentRow < dtbMedicineName.Rows.Count; currentRow++)
                {
                    strMedicineNameArr[currentRow] = dtbMedicineName.Rows[currentRow]["MedicineTypeName"] as string;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡָ���ֿ������õ�ҩƷ����ID
        /// <summary>
        /// ��ȡָ���ֿ������õ�ҩƷ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStoreRoomID">�ֿ�</param>
        /// <param name="p_objType">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStoreRoomSetCheck( string p_strStoreRoomID, out clsMS_MedicineType_VO[] p_objType)
        {
            p_objType = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select b.medicinetypeid_chr, b.medicinetypename_vchr
  from t_ms_medicinestoreroomset a, t_aid_medicinetype b
 where a.medicinetypeid_chr = b.medicinetypeid_chr
   and a.medicineroomid = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStoreRoomID;
                DataTable dtbValue = null;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (lngRes > 0 && dtbValue != null)
                {
                    int intRowsCount = dtbValue.Rows.Count;
                    if (intRowsCount == 0)
                    {
                        return 0;
                    }

                    p_objType = new clsMS_MedicineType_VO[intRowsCount];
                    DataRow drTemp = null;
                    for (int iRow = 0; iRow < intRowsCount; iRow++)
                    {
                        p_objType[iRow] = new clsMS_MedicineType_VO();
                        drTemp = dtbValue.Rows[iRow];
                        p_objType[iRow].m_strMedicineTypeID_CHR = drTemp["medicinetypeid_chr"].ToString();
                        p_objType[iRow].m_strMedicineTypeName_VCHR = drTemp["medicinetypename_vchr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡָ��ҩ�������õ�ҩƷ����ID
        /// <summary>
        ///  ��ȡָ��ҩ�������õ�ҩƷ����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStoreRoomID">�ֿ�</param>
        /// <param name="p_objType">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreSetCheck( string p_strStoreRoomID, out clsMS_MedicineType_VO[] p_objType)
        {
            p_objType = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select b.medicinetypeid_chr, b.medicinetypename_vchr
  from t_ds_medstoreset a, t_aid_medicinetype b
 where a.medicinetypeid_chr = b.medicinetypeid_chr
   and a.medstoreid = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStoreRoomID;
                DataTable dtbValue = null;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (lngRes > 0 && dtbValue != null)
                {
                    int intRowsCount = dtbValue.Rows.Count;
                    if (intRowsCount == 0)
                    {
                        return 0;
                    }

                    p_objType = new clsMS_MedicineType_VO[intRowsCount];
                    DataRow drTemp = null;
                    for (int iRow = 0; iRow < intRowsCount; iRow++)
                    {
                        p_objType[iRow] = new clsMS_MedicineType_VO();
                        drTemp = dtbValue.Rows[iRow];
                        p_objType[iRow].m_strMedicineTypeID_CHR = drTemp["medicinetypeid_chr"].ToString();
                        p_objType[iRow].m_strMedicineTypeName_VCHR = drTemp["medicinetypename_vchr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}